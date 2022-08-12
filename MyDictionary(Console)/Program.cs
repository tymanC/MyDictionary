using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDictionary_Console_.LanguageScoringApi;

namespace MyDictionary_Console_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LangScoringClient client = new LangScoringClient();
            string word = "coincide";
            Console.WriteLine(client.GetWordScore(word).Result.Ten_degree);
        }
    }
}
