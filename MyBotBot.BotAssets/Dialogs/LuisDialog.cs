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
    [LuisModel("ModelId", "SubscriptionKey")]
    public class LuisDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }


        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Hi! Try asking me things like 'search hotels in Seattle', 'search hotels near LAX airport' or 'show me the reviews of The Bot Resort'");

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Suggestion")]
        public async Task FindCode(IDialogContext context, LuisResult result)
        {
            string message = "Thanks for offering to make a suggestion. What would you like to suggest?";

            await context.PostAsync(message);

            context.Call(new SuggestionDialog(), ResumeAfterFormOption);
        }


        private async Task ResumeAfterFormOption(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);

        }

    }
}
