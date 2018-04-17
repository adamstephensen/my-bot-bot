using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace MyBotBot.BotAssets.Dialogs
{

    [Serializable]
    public class SuggestionDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            /* Wait until the first message is received from the conversation and call MessageReceviedAsync 
             *  to process that message. */            
            context.Wait(this.MessageRecievedAsync);
        }

        public virtual async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var suggestion = await result;
            //todo: actually lodge the suggestion
            await context.PostAsync($"Lodging suggestion: '{suggestion}'.");

        }
    }
}
