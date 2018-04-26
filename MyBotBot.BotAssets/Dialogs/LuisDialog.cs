using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System.Threading;
using MyBotBot.BotAssets.Extensions;
using System.Configuration;
using QnAMakerDialog;

namespace MyBotBot.BotAssets.Dialogs
{
    [Serializable]
    //[LuisModel(, )]
    public class LuisDialog : LuisDialog<object>
    {
        //from https://github.com/Microsoft/BotBuilder/issues/3855
        public LuisDialog() : base(new LuisService(new LuisModelAttribute(
                ConfigurationManager.AppSettings[AppSettings.LuisAppId],
                ConfigurationManager.AppSettings[AppSettings.LuisSubscriptionKey])))
        {
        }

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, IAwaitable<IMessageActivity> message, LuisResult result)
        {
            var messageToForward = await message; //or context.Activity
            var faqDialog = new BotQnaDialog();
            // You can forward to QnA Dialog, and let Qna Maker handle the user's query if no intent is found
            await context.Forward<bool>(faqDialog, ResumeAfterQnaDialog, messageToForward, CancellationToken.None);

        }


        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Hi! Try asking me things like 'I would like to make a suggestion', 'how do I do a password reset bot' or 'where can I get sample applications'");

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Suggestion")]
        public async Task Suggestion(IDialogContext context, LuisResult result)
        {
            string message = "Thanks for offering to make a suggestion. What would you like to suggest?";

            await context.PostAsync(message);

            context.Call(new SuggestionDialog(), ResumeAfterFormOption);
        }


        private async Task ResumeAfterQnaDialog(IDialogContext context, IAwaitable<bool> result)
        {
            bool qnaMessageWasHandled = await result ;

            if (!qnaMessageWasHandled)
            {
                await context.PostAsync("Sorry, we couldn't work out what you were trying to ask or do. Try typing 'Help'.");
            }

            context.Wait(MessageReceived);

        }
        private async Task ResumeAfterFormOption(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);

        }

    }
}
