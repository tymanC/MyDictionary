using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary_Console_.WordsAPI.Models
{
    internal class DefinitionModel
    {
        public string word { get; set; }
        public List<Definition> definitions { get; set; }
    }
    public class Definition
    {
        public string definition { get; set; }
        public string partOfSpeech { get; set; }
    }
}
