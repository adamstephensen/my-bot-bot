using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBotBot.BotAssets.Dialogs
{
    public static class MenuHelpers
    {
        public static string[] getMenuOptions(string menu)
        {
            switch (menu)
            {
                case "Home":
                    return new[]
                    {
                        "Register for the Azure AU Slack",
                        "Make me smarter - add to my QnAs",
                        "Read my full list of QnAs",
                        "Get the code for this bot",
                        "Ask a question"
                    };
                
                default:
                    return null;
            };

        }
    }
}
