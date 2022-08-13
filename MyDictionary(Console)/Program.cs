using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDictionary_Console_.LanguageScoringApi;
using MyDictionary_Console_.TelegramBot;

namespace MyDictionary_Console_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TgBot bot = new TgBot();
            bot.Start();
            Console.ReadKey();
        
        }
    }
}
