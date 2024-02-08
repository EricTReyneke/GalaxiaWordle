using Business.GalaxiaWordle.Interfaces;

namespace Business.GalaxiaWordle.ApiContext
{
    public class WordleContext : IWordleContext
    {
        public int WordLength { get; set; } = 5;

        public string RandomWordApiUrl
        {
            get
            {
                return $"https://api.wordnik.com/v4/words.json/randomWord?hasDictionaryDef=true&maxCorpusCount=-1&minDictionaryCount=1&maxDictionaryCount=-1&minLength={WordLength}&maxLength={WordLength}&api_key=b9y8sdyipyklyfg1jtqg564ck3yznc2is00vgwnahoz5xpqnn";
            }
        }
    }
}