using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using MyBotBot.BotAssets.Extensions;
using System.Configuration;
using System;

namespace MyBotBot.BotAssets.Dialogs
{
    [Serializable]
    class BotQnaDialog : QnAMakerDialog
    {
        public BotQnaDialog() : base(new QnAMakerService(
           new QnAMakerAttribute(
               ConfigurationManager.AppSettings[AppSettings.QnaSubscriptionKey],
               ConfigurationManager.AppSettings[AppSettings.QnaKBId])))
        {
        }

        protected override async Task DefaultWaitNextMessageAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
        {
            if (result.Answers.Count >= 1)
            {
                context.Done(message);
            }
            else
            {
                string response = $"Sorry, I did not find anything for '{message.Text}'.";

                await context.PostAsync(response); //.ToUserLocale(context) to add language translation
                context.Done(message);
            }
        }

        protected override async Task RespondFromQnAMakerResultAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
        {
            string response = result.Answers.First().Answer;
            await context.PostAsync(response); //.ToUserLocale(context) to add language translation
        }
    }
}

