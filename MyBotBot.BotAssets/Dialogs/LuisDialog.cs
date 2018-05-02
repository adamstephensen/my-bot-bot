using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System.Threading;
using MyBotBot.BotAssets.Extensions;
using System.Configuration;
using Microsoft.Bot.Builder.FormFlow;
using MyBotBot.BotAssets.Forms;

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

        RegisterForm registerForm;

        // Forms.RegisterForm registerForm;

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            //You COULD put a message in here... but we handle it in QnA maker
            //string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";
            //await context.PostAsync(message);

            // You can forward to QnA Dialog, and let Qna Maker handle the user's query if no intent is found
            await context.Forward(new BotQnaDialog(), ResumeAfterQnaDialog, context.Activity, CancellationToken.None);

        }
        private async Task ResumeAfterQnaDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
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
            
            context.Call(new SuggestionDialog(), ResumeAfterSuggestion);
        }
        private async Task ResumeAfterSuggestion(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }

        [LuisIntent("Register")]
        public async Task Register(IDialogContext context, LuisResult result)
        {

            await context.PostAsync("Thanks for asking to register.");
            this.registerForm = new RegisterForm();
            var myform = new FormDialog<RegisterForm>(this.registerForm, Forms.RegisterForm.BuildForm, FormOptions.PromptInStart);
            context.Call(myform, this.ResumeAfterRegister);
        }


        private async Task ResumeAfterRegister(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Thanks for registering");
            context.Wait(MessageReceived);
        }
        
     

    }
}
