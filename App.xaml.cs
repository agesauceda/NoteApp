using NoteApp.Controllers.Auth;

namespace NoteApp
{
    public partial class App : Application
    {
        public static Client.Client _client { get; private set; }
        public App(Client.Client client, QueueLogin queueLogin)
        {
            InitializeComponent();
            _client = client;
            MainPage = new AppShell();
        }
    }
}
