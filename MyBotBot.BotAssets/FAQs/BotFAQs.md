## What is a bot ? 
A bot is an app that users interact with in a conversational way. Bots can communicate conversationally with text, cards, or speech.
For more info check out https://docs.microsoft.com/en-us/azure/bot-service/bot-service-overview-introduction 

## Can I see some good demos ? 
Sure can. Check out the following: 
1. The Microsoft Health Bot http://aka.ms/ai/healthbot
2. 'Sam' - a public-facing chatbot for DHS https://www.humanservices.gov.au/individuals/students-and-trainees

## Where can I find documentation ?
There is lots of great documentation on bots.
- QnA Maker https://qnamaker.ai
- LUIS https://www.luis.ai/
- Azure Bot Service Documentation https://docs.microsoft.com/en-us/azure/bot-service/
- The bot framework developer portal https://dev.botframework.com/
- Bot Builder SDK for .NET https://docs.microsoft.com/en-us/azure/bot-service/dotnet/bot-builder-dotnet-overview

## Where can I get sample applications ? 
There are heaps of awesome samples here https://github.com/Microsoft/BotBuilder-Samples

## Where can I learn about navigation ? 
Navigation is easy once you understand using dialogs. Check this out... 
https://github.com/Microsoft/BotBuilder-Samples/blob/master/CSharp/core-MultiDialogs/README.md

## What templates are available for building bots.
There are 5 templates out of the box.
https://docs.microsoft.com/en-gb/azure/bot-service/bot-service-concept-templates

## How do I get app settings into my bot?
The trick is to put them into local.settings.json on your dev machine and then to read them from App Settings in Azure.
Check out https://docs.microsoft.com/en-us/azure/azure-functions/functions-reference-csharp#environment-variables for more info. 

## Can I use LUIS and QnAMaker together?
Luis and QnA maker love hanging out together.
Check this out http://www.garypretty.co.uk/2017/03/26/forwarding-activities-messages-to-other-dialogs-in-microsoft-bot-framework/

## Can I add a message whent he bot loads ? 
Even though no-one shows you how to do it... of course you can add a message when the bot loads. 
Here are some instructions. https://www.davidezordan.net/blog/?p=8119

## Can I leverage Ai ?
To learn about leveraging AI with bots check out https://docs.microsoft.com/en-us/azure/bot-service/bot-service-concept-intelligence

## Is there a commerce demo ? 
For a commerce demo check out https://docs.microsoft.com/en-us/azure/bot-service/bot-service-scenario-commerce

## How do I do a password reset bot?
To build a password reset bot check out https://docs.microsoft.com/en-us/azure/bot-service/bot-service-design-pattern-task-automation

## Can I do Human hand off ?
You can configure your bot to connect a user with a real person using the human hand off pattern. Check out https://docs.microsoft.com/en-us/azure/bot-service/bot-service-design-pattern-handoff-human
https://blogs.msdn.microsoft.com/jamiedalton/2017/08/10/microsoft-bot-framework-handing-off-to-a-human-for-agentssupervisors-with-c-and-the-botbuilder-sdk/

## How do I go to Kudu for by bot ? 
Just go to https://<your-bot-name->.scm.azurewebsites.net

## How do I create a LUIS endpoint key ?
Easy. Check this out https://docs.microsoft.com/en-us/azure/cognitive-services/luis/azureibizasubscription

## How do I handle global messages ?
You implement global messages like this https://docs.microsoft.com/en-us/azure/bot-service/dotnet/bot-builder-dotnet-global-handlers

## How can I learn about async await for asynchronous programming ? 
Check out this reference for async / await https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/await?

## Can you help me understand the dialog lifecycle and navigating dialogs ?
You might need to read this a few times.. but it's important.
- When a dialog is invoked, it takes control of the conversation flow. Every new message will be subject to processing by that dialog until it either closes or redirects to another dialog.
- In C#, you can use context.Wait() to specify the callback to invoke the next time the user sends a message. To close a dialog and remove it from the stack (thereby sending the user back to the prior dialog in the stack), use context.Done(). You must end every dialog method with context.Wait(), context.Fail(), context.Done(), or some redirection directive such as context.Forward() or context.Call(). A dialog method that does not end with one of these will result in an error (because the framework does not know what action to take the next time the user sends a message).
- From https://docs.microsoft.com/en-us/azure/bot-service/dotnet/bot-builder-dotnet-manage-conversation-flow
For more info

There are a few key methods. For a sample application that demonstrates this well check out
https://github.com/Microsoft/BotBuilder-Samples/blob/master/CSharp/core-MultiDialogs/README.md
Look for the implementations of 
- context.Call and context.Done  
- context.Forward 
- context.Wait
- context.Fail

Check out the following links about navigating and building dialogues: 
- https://docs.microsoft.com/en-gb/bot-framework/bot-service-design-conversation-flow
- https://blog.botframework.com/2017/11/10/dialog-management-qna-luis-scorables/
- http://www.garypretty.co.uk/2017/03/26/forwarding-activities-messages-to-other-dialogs-in-microsoft-bot-framework/

Watch out for some pitfalls with bot design navigation
- https://docs.microsoft.com/en-gb/bot-framework/bot-service-design-navigation

## How can I get app settings into my bot?
The trick is to put them into local.settings.json on your dev machine and then to read them from App Settings in Azure.
Check out https://docs.microsoft.com/en-us/azure/azure-functions/functions-reference-csharp#environment-variables for more info. 

## What are the common mistakes ? 
There are a lot of common mistakes made with bots.
1. Bad navigation https://docs.microsoft.com/en-gb/bot-framework/bot-service-design-navigation
2. Having a bot that tries to do too much. You are better off having several small bots that do a specific task very well.
3. Trying to make your bot too 'smart'. The best bots aren't the smartest bots. The best bots are the ones that solve an issue that users have. A simple bot that guides a user to a solution will be well utilised. A 'smart' bot that is hard to use will not.

## Can I get a list of all the questions or FAQs ?
All the FAQs in this bot can be found here https://github.com/adamstephensen/my-bot-bot/blob/master/MyBotBot.BotAssets/FAQs/BotFAQs.md

## How do I setup my machine for bot development ? 
I'm still working on this… check back soon.

## What are the options for deploying my bot using ci/cd and devops ? 
I'm still working on this… check back soon.

## What are the pros and cons of function bots over web bots
What are the benefits of going for a Web Bot
1. Web bot is always on - No Warm up time
Function bot can take a few seconds to warm up if it hasn't been hit for a while
2. More control over how the bot scales / Predictable costing
What a web bot you control how much the bot scales.

Here are the benefits of going for a Function Bot
1. Pay as you use - per second billing
If no-one is using your bot, you will not pay for it
2. Infitinte scale without any need to configure scaling rules.
Scaling just comes in with the platform
3. Easy integration with other Azure services.
Function bots easy integrate with other Azure services like Logic Apps, Service Bus, Cosmos DB, Table Storage, Storage Queues.
4. Simplification of Development
You don't need to host your application on the MVC framework.
You have your run.csx.. and it executes your bot.

## Should my dialogues hand off from LUIS to Qna or from Qna To LUIS
It depends.
Some people like to make LUIS the master and if no LUIS intent is identified hand off to QnA maker to return a QnA match.
Others prefer to capture as much as possible in QnA and then pass to LUIS.
One consideration is that calls to QnA Maker are currently Free (as at 24 April 2018) where you pay a very small amount for every LUIS request. 

## How do I build a multi-lingual bot ? 
Check out this link https://desflanagan.wordpress.com/2016/10/25/build-a-multi-language-bot/ 
