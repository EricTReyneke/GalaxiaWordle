using Business.GalaxiaWordle.Interfaces;
using Newtonsoft.Json.Linq;

namespace Business.GalaxiaWordle.ApiClients
{
    public class WordleApiClient : IWordleApiClient
    {
        #region Fields
        IWordleContext _context;
        #endregion

        #region Constructors
        public WordleApiClient(IWordleContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<string> CallWordleWord()
        {
            try
            {
                using (HttpClient client = new())
                {
                    HttpResponseMessage response = await client.GetAsync(_context.RandomWordApiUrl);
                    response.EnsureSuccessStatusCode();

                    return JObject.Parse(await response.Content.ReadAsStringAsync())["word"]?.ToString() ?? string.Empty;
                }
            }
            catch
            {
                throw;
            }
        }

        public void SetWordleWordLength(int wordLength) =>
            _context.WordLength = wordLength;
        #endregion
    }
}