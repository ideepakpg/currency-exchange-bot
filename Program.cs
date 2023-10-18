using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


var botClient = new TelegramBotClient("6664951833:AAHZtXA-4FR03Ta6sOedUax9WNNV7XyyO-o");


using CancellationTokenSource cts = new();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
};


botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);


// Fetch information about the bot's identity using GetMeAsync() and display a message to start listening for messages from the bot's username.
var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();


async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    // Only process Message updates
    if (update.Message is not { } message)
        return;
    // Only process text messages
    if (message.Text is not { } messageText)
        return;


    long chatId = update.Message.Chat.Id; // Get the chat ID from the incoming message (dynamic chatId)


    //Display user entered command details in console
    if (messageText.StartsWith("/"))
    {
        // Get the current date and time (to show in console)
        DateTime currentTime = DateTime.Now;

        // Display the user's chat ID, date, and time in console
        Console.WriteLine(
            $"User with Chat ID {message.Chat.Id} sent the command '{messageText}' at {currentTime.ToLocalTime()}.");
    }



    //This code manages various Telegram bot commands, offering responses for commands such as "/start", "/help" and "/about"
    if (messageText.StartsWith("/"))
    {
        // Handle command messages
        switch (messageText.ToLower())
        {
            case "/start":
                var startMessage = "Welcome to the Currency Exchanger Bot! This bot allows you to perform currency exchange.\n\n" +
                    "/help - Get help on using the bot\n" +
                    "/about - Learn more about the bot";
                await botClient.SendTextMessageAsync(message.Chat.Id, startMessage, cancellationToken: cancellationToken);
                break;


            default:
                // Handle unknown commands
                var unknownCommandMessage = "Unknown command. Use /help for available commands.";
                await botClient.SendTextMessageAsync(message.Chat.Id, unknownCommandMessage, cancellationToken: cancellationToken);
                break;
        }
    }


}




//Handles errors that occur during the polling process of the Telegram bot.
//Logs error messages to the console, providing detailed information about the exception.
Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}