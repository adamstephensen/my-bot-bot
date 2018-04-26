using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Threading.Tasks;
//using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using MyBotBot.BotAssets.Extensions;
using System.Configuration;
using QnAMakerDialog;
using QnAMakerDialog.Models;

namespace MyBotBot.BotAssets.Dialogs
{
    //Steve Pretty's qna dialog
    //https://github.com/garypretty/botframework/tree/master/QnAMakerDialog
    //[QnAMakerService("xx", "xx")]
    public class BotQnaDialog : QnAMakerDialog<bool>
    {

        public BotQnaDialog()
        {
            this.SubscriptionKey = ConfigurationManager.AppSettings[AppSettings.QnaSubscriptionKey];
            this.KnowledgeBaseId = ConfigurationManager.AppSettings[AppSettings.QnaKBId];
        }


        //NoMatch hander is added by Steve Pretty's Qna Dialog - Nice !
        public override async Task NoMatchHandler(IDialogContext context, string originalQueryText)
        {
            await context.PostAsync($"Couldn't find a qna answer for '{originalQueryText}'.");
            context.Done<bool>(false);
        }
        public override async Task DefaultMatchHandler(IDialogContext context, string originalQueryText, QnAMakerResult result)
        {
            await context.PostAsync($"Answer for '{originalQueryText}' is {result.Answers[0].Answer}.");
            context.Done<bool>(false);
        }
        [QnAMakerResponseHandler(50)]
        public async Task LowScoreHandler(IDialogContext context, string originalQueryText, QnAMakerResult result)
        {
            await context.PostAsync($"I've found an answer that might help... {result.Answers[0].Answer}.");
            context.Wait(MessageReceived);
        }
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as IMessageActivity;

            // TODO: Put logic for handling user message here

            context.Wait(MessageReceivedAsync);
        }

    }

    
}



//Original QnA maker class methods
//{
//    public BotQnaDialog() : base(new QnAMakerService(
//       new QnAMakerAttribute(
//           ConfigurationManager.AppSettings[AppSettings.QnaSubscriptionKey],
//           ConfigurationManager.AppSettings[AppSettings.QnaKBId],
//           "No match found.", .4)))
//    {
//    }

//this is how to delect no match found using the default qna dialog - ugly ! 
//https://github.com/Microsoft/BotBuilder/issues/3727
//protected override async Task RespondFromQnAMakerResultAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
//{
//    var answer = result.Answers.First().Answer;
//    Activity reply = ((Activity)context.Activity).CreateReply();
//    if (reply.Text.Equals("No Match Found"))
//    {
//        reply.Text = "No good match in FAQ";
//    }

//    await context.PostAsync(reply);
//}
