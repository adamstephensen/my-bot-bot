using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using MyBotBot.BotAssets.Dialogs;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Scorables;
using System;
using System.Linq;
using System.Diagnostics;

namespace MyBotBot
{
    public static class run
    {
        [FunctionName("messages")]
        public static async Task<object> Run([HttpTrigger(WebHookType = "genericJson")]HttpRequestMessage req, TraceWriter log)
        {

            // Initialize the azure bot
            using (BotService.Initialize())
            {
                log.Info($"Bot is intialised!");
                

                Conversation.UpdateContainer(builder =>
                {
                    builder.RegisterModule(new ReflectionSurrogateModule());
                    builder.RegisterModule<GlobalMessageHandlersBotModule>();
                });

                string jsonContent = await req.Content.ReadAsStringAsync();
                var activity = JsonConvert.DeserializeObject<Activity>(jsonContent);

                if (activity != null)
                {
                    // one of these will have an interface and process it
                    switch (activity.GetActivityType())
                    {
                        case ActivityTypes.Message:
                            //here is where we will navigate to root dialogue
                            Trace.TraceInformation("run.Run");
                            log.Info($"Navigate to RootDialog.");

                            await Conversation.SendAsync(activity, () => new RootDialog());

                            //var client = new ConnectorClient(new Uri(activity.ServiceUrl));
                            //var triggerReply = activity.CreateReply();
                            //triggerReply.Text = "Hey";
                            //await client.Conversations.ReplyToActivityAsync(triggerReply);

                            break;
                        case ActivityTypes.ConversationUpdate:
                            log.Info($"Conversation update.");
                            // Handle conversation state changes, like members being added and removed
                            // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                            // Not available in all channels

                            if (activity.MembersAdded.Any(o => o.Id == activity.Recipient.Id))
                            {
                                var reply = activity.CreateReply("Hi ! I'm a bot about Bots.");
                                
                                reply.AddHeroCard("Feel free to ask me a question... or you can ..", MenuHelpers.getMenuOptions("Home"));

                                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                                await connector.Conversations.ReplyToActivityAsync(reply);
                            }

                            break;

                        default:
                            log.Error($"Unknown activity type ignored: {activity.GetActivityType()}");
                            break;
                    }
                }
                return req.CreateResponse(HttpStatusCode.Accepted);
            }

        }
    }

    public class GlobalMessageHandlersBotModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //register other scorables here

            builder
                .Register(c => new CancelScorable(c.Resolve<IDialogTask>()))
                .As<IScorable<IActivity, double>>()
                .InstancePerLifetimeScope();
        }
    }

}
