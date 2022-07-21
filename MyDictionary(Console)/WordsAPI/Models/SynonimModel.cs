using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary_Console_.WordsAPI.Models
{
    internal class SynonimModel
    {
        public string word { get; set; }
        public List<string> synonyms { get; set; }
    }
}
