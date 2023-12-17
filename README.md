# Currency Exchange Bot

This is a Telegram bot designed to assist users in checking currency exchange rates and performing currency conversions. The bot currently provides support for both INR (Indian Rupee) and USD (United States Dollar) currency conversions. You can use this bot to quickly get the latest exchange rate or perform currency conversions within your Telegram chats.

## Prerequisites
 - [C#](https://dotnet.microsoft.com/en-us/learn/csharp)
 - [A guide to .NET Telegram.Bot API](https://telegrambots.github.io/book/)

## How to Use the Bot

Discover the bot on [Currency Exchange Bot](https://t.me/inrcurrency_exchange_bot)

# Getting Started

## Installation

1. Clone this repository:
   ```sh
   git clone https://github.com/ideepakpg/currency-exchange-bot.git

2. Open the project in your favorite C# IDE (e.g., Visual Studio, Visual Studio Code).

3. Replace ```BOT_TOKEN``` in the [Program.cs](https://github.com/ideepakpg/currency-exchange-bot/blob/main/Program.cs) file with your Telegram Bot API token.

    Unsure about obtaining a bot token from Telegram? Follow these steps:

4. ## Creating Your Own Telegram Bot

- **To create your own Telegram bot, you can follow these steps:**
  
  - Open Telegram and search for "[BotFather](https://t.me/BotFather)"
  - Start a chat with [BotFather](https://t.me/BotFather).
  - Use the ```/newbot``` command to create a new bot.
  - Follow the instructions to choose a name and username for your bot.
  - BotFather will provide you with an API token. Copy and use this token in your code, replacing the placeholder where ```BOT_TOKEN``` is written.

5. Build and run the project
   ```sh
   dotnet build
   dotnet run


## Commands

To interact with the Currency Exchange Bot, you can use various commands and currency conversion requests:

- `/start`: Start using the Currency Exchange Bot. It provides information about how to use the bot and the available commands.

- `/help`: Get help on using the bot. This command explains the format for currency conversion requests and lists the currently supported currencies.

- `/about`: Learn more about the bot. This command provides information about the bot's development and source code.

## Currency Conversion
You can request currency conversions by entering an amount and the source currency (either INR or USD) in the following format:

`<amount> <source_currency>`

For example:

- `100 INR` to convert 100 Indian Rupees to United States Dollars.
- `10 USD` to convert 10 United States Dollars to Indian Rupees.
- The bot will provide you with the converted amount and the latest exchange rate.

## Screenshots

![Screenshot (28)](https://github.com/ideepakpg/currency-exchange-bot/assets/123804790/4db89ed5-7a35-4655-932d-98ab9c51b760)
![Screenshot (29)](https://github.com/ideepakpg/currency-exchange-bot/assets/123804790/b8f18173-5db5-477a-95eb-c16730a9c8c2)
![Screenshot (30)](https://github.com/ideepakpg/currency-exchange-bot/assets/123804790/dc85d796-7f2d-426f-859e-16171e7b8b10)


## Supported Currencies
Currently, the bot supports the following currencies for conversion:

- Indian Rupee (INR)
- United States Dollar (USD)

I am continuously working to enhance the capabilities of this bot, and I plan to add support for additional currencies in the near future. Stay tuned for updates as I expand the range of supported currencies to better serve your currency exchange needs.

## Acknowledgement
The Currency Exchange Bot was developed using [C#](https://dotnet.microsoft.com/en-us/learn/csharp) and the [Telegram Bot API in .NET](https://github.com/TelegramBots/Telegram.Bot).
