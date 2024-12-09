using NoteApp.Controllers.Auth;

namespace NoteApp
{
    public partial class App : Application
    {
        public static Client.Client _client { get; private set; }
        public static QueueLogin _queueLogin { get; private set; }
        public App(Client.Client client, QueueLogin queueLogin)
        {
            InitializeComponent();
            _client = client;
            _queueLogin = queueLogin;
            MainPage = new AppShell();
        }
    }
}
