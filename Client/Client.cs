namespace NoteApp.Client
{
    public class Client
    {
        string baseAddress;
        private readonly HttpClient _client;

        public Client(string baseAddress) { 
            this.baseAddress = baseAddress;
            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        public HttpClient getClient() {
            return _client;
        }
    }
}
