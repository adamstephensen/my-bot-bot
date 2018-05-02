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
                log.Info($"Webhook was triggered! - messages");

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
                            await Conversation.SendAsync(activity, () => new RootDialog());

                            //var client = new ConnectorClient(new Uri(activity.ServiceUrl));
                            //var triggerReply = activity.CreateReply();
                            //triggerReply.Text = "Hey";
                            //await client.Conversations.ReplyToActivityAsync(triggerReply);

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
