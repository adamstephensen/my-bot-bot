using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System.Threading;

namespace MyBotBot.BotAssets.Dialogs
{
    [Serializable]
    //[LuisModel(, )]
    public class LuisDialog : LuisDialog<object>
    {
        //from https://github.com/Microsoft/BotBuilder/issues/3855
        public LuisDialog() : base(new LuisService(GetLUISAttributesFromConfig()))
        {
           // customerInsightsDialog = new InsightsDialog();
        }
        //public LuisDialog() : base(new LuisService(
        // new LuisModel("9487773cba034e35be25bf4106542a2d", "78c0b845-84e7-4a32-9c58-2b18a7b7e83b")))
        //{
        //}
        public static LuisModelAttribute GetLUISAttributesFromConfig()
        {
            return new LuisModelAttribute(Constants.LuisModelId, Constants.LuisSubscriptionKey);
        }
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);

            // You can forward to QnA Dialog, and let Qna Maker handle the user's query if no intent is found
            await context.Forward(new BotQnaDialog(), ResumeAfterQnaDialog, context.Activity, CancellationToken.None);

            context.Wait(this.MessageReceived);
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


        private async Task ResumeAfterQnaDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);

        }
        private async Task ResumeAfterFormOption(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);

        }

    }
}
