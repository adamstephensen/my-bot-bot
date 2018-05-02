using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBotBot.BotAssets.Forms
{
    
    [Serializable]
    public class RegisterForm
    {
        [Prompt("What is your name")]
        public string name { get; set; }

        [Prompt("What is your email address?")]
        public string emailAddress { get; set; }       
        
        public static IForm<RegisterForm> BuildForm()
        {
            return new FormBuilder<RegisterForm>()
                   .AddRemainingFields()
                   .Build();
        }
    };
}
