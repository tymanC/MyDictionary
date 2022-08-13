using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using MyDictionary_Console_.Constants;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;
using MyDictionary_Console_.WordsAPI.Clients;
using MyDictionary_Console_.WordsAPI.Models;

namespace MyDictionary_Console_.TelegramBot
{
    internal class TgBot
    {
        TelegramBotClient botClient = new TelegramBotClient(Constant.BotToken);
        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };

        WordClients WordsClient = new WordClients();
        //start bot

        string ChooseAction;
        string Word;
        public async Task Start()
        {
            botClient.StartReceiving(HandlerUpdateAsync, HandlerError, receiverOptions, cancellationToken);
            var botMe = await botClient.GetMeAsync();
            Console.WriteLine($"Бот {botMe.Username} почав працювати");
            Console.ReadKey();
        }

        private Task HandlerError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Помилка в телеграм бот АПІ:\n{apiRequestException.ErrorCode}" +
                $"{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                //запуск методу, що обробляє текст
                await HandlerMessageAsync(botClient, update.Message);
            }
        }
        private async Task HandlerMessageAsync(ITelegramBotClient botClient, Message message)
        {
            
            bool ErrorMessage=false;
            switch (message.Text)
            {
                case "/start":
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Write '/help' to see, what this bot can do.\nWrtire '/keyboard' to use this bot's abilities");
                    break;
                case "/keyboard":
                    ReplyKeyboardMarkup replyKeyboardMarkup = new
                        (
                            new[]
                            {
                                new KeyboardButton[]{"Definition","Examples"},
                                new KeyboardButton[]{ "Synonims"},
                                new KeyboardButton[]{"Text score", "Word score"},
                                new KeyboardButton[]{"Add to db"},
                            }
                        )
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Choose an one item", replyMarkup: replyKeyboardMarkup);
            
                    break;
                case "Definition":
                    ChooseAction = "Definition";
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Write a word:");
                    break;
                case "Example":
                    ChooseAction = "Example";
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Write a word:");
                    break;
                case "Synonims":
                    ChooseAction = "Synonims";
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Write a word:");
                    break;
                case "Text score":
                    ChooseAction = "Text score";
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Enter text:");
                    break;
                case "Word score":
                    ChooseAction = "Word score";
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Write a word:");
                    break;
                case "Add to db":
                    ChooseAction = "Add to db";
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Enter a word:");
                    break;
                default:
                    ErrorMessage = true;
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Error. Try again");
                    break;
            }
            if(ErrorMessage==false)
            {
                _ = ButtonAbilities(botClient, message);
                return;
            }
        }
        private async Task ButtonAbilities(ITelegramBotClient botClient, Message message)
        {
            string Definition;
            string PartOfSpeech;
            if(ChooseAction=="Definition")
            {
                Word = message.Text;
                int count = WordsClient.GetDefinition(Word).Result.definitions.Count;
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        Definition = WordsClient.GetDefinition(Word).Result.definitions[i].definition;
                        PartOfSpeech = WordsClient.GetDefinition(Word).Result.definitions[i].partOfSpeech;
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"{Word}\nDefinition:\n{Definition}\nPart of speech:\n{PartOfSpeech}");
                    }
                }
                catch
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Error. Check inputed word and try again.");
                }
            }
        }

    }
}
