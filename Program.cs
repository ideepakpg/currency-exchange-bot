using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


var botClient = new TelegramBotClient("BOT_TOKEN");


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


    //Display entered command details in console
    if (messageText.StartsWith("/"))
    {
        // Get the current date and time (to show in console)
        DateTime currentTime = DateTime.Now;

        // Display the user's chat ID, date, and time in console
        Console.WriteLine($"User with Chat ID {message.Chat.Id}, Username: @{message.Chat.Username ?? "N/A"}, sent the command '{messageText}' at {currentTime.ToLocalTime()}.");

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

            case "/help":
                var helpMessage = "To check the currency price, use this format:\n" +
                                 "<amount> <source_currency>\n" +
                                 "Example: 100 INR\n\n" +
                                 "Currently supported currencies: INR and USD";
                await botClient.SendTextMessageAsync(message.Chat.Id, helpMessage, cancellationToken: cancellationToken);
                break;

            case "/about":
                var aboutMessage = "Currency Exchange Bot\n\n" +
                                  "This bot allows you to check currency exchange rates and perform currency conversions. It currently supports both INR to USD and USD to INR conversion. To get the latest exchange rate, simply enter the amount and the target currency (e.g., 100 INR or 10 USD).\n\n" +
                                  "Developed using C# and the Telegram Bot API in .NET. For more information, visit my repo : https://github.com/ideepakpg/currency-exchange-bot\n\n" +
                                  "For help, use /help.";
                await botClient.SendTextMessageAsync(message.Chat.Id, aboutMessage, cancellationToken: cancellationToken);
                break;

            default:
                // Handle unknown commands
                var unknownCommandMessage = "Unknown command. Use /help for available commands.";
                await botClient.SendTextMessageAsync(message.Chat.Id, unknownCommandMessage, cancellationToken: cancellationToken);
                break;
        }
    }

    else if (messageText.EndsWith(" INR"))
    {
        // Handle INR to USD conversion if the message ends with " INR"
        // Extract the amount and convert it
        var amountText = messageText.Replace(" INR", "");
        if (decimal.TryParse(amountText, out decimal amountToConvert))
        {
            decimal exchangeRate = 0.012m; // 1 INR = 0.012 USD
            decimal convertedAmount = amountToConvert * exchangeRate;

            var exchangeMessage = $"Currency exchange result:\n{amountToConvert} INR = {convertedAmount} USD";
            await botClient.SendTextMessageAsync(message.Chat.Id, exchangeMessage, cancellationToken: cancellationToken);


            // Display a console message with user ID, and timestamp
            Console.WriteLine($"User ID: {message.Chat.Id}, Username: @{message.Chat.Username}, requested currency conversion: {amountToConvert} INR to USD at {DateTime.Now}");
        }
        else
        {
            var conversionErrorMessage = "Invalid amount format. Please provide a valid amount for conversion.";
            await botClient.SendTextMessageAsync(message.Chat.Id, conversionErrorMessage, cancellationToken: cancellationToken);
        }
    }

    else if (messageText.EndsWith(" USD"))
    {
        // Handle USD to INR conversion if the message ends with " USD"
        // Extract the amount and convert it
        var amountText = messageText.Replace(" USD", "");
        if (decimal.TryParse(amountText, out decimal amountToConvert))
        {
            decimal exchangeRate = 83.27m; // 1 USD = 83.13 INR
            decimal convertedAmount = amountToConvert * exchangeRate;

            var exchangeMessage = $"Currency exchange result:\n{amountToConvert} USD = {convertedAmount} INR";
            await botClient.SendTextMessageAsync(message.Chat.Id, exchangeMessage, cancellationToken: cancellationToken);


            // Display a console message with user ID, and timestamp
            Console.WriteLine($"User ID: {message.Chat.Id}, Username: @{message.Chat.Username}, requested currency conversion: {amountToConvert} USD to INR at {DateTime.Now}");
        }

    }
    else
    {
        var conversionErrorMessage = "Invalid amount format. Please provide a valid amount for conversion.";
        await botClient.SendTextMessageAsync(message.Chat.Id, conversionErrorMessage, cancellationToken: cancellationToken);
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