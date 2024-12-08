using NoteApp.Models.common;
using System;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace NoteApp.Controllers.Auth
{
    public class QueueLogin
    {
        private static QueueLogin _instance;
        private static readonly object _padLock = new object();
        private CancellationTokenSource _tokenSource;
        public QueueLogin() {
            _tokenSource = new CancellationTokenSource();
            StartVerificationToken();
        }
        public static QueueLogin Instance {
            get {
                lock(_padLock){
                    if (_instance == null) {
                        _instance = new QueueLogin();  
                    }
                }
                return _instance;
            }
        }

        public void Stop() {
            _tokenSource.Cancel();
        }
        private async void StartVerificationToken() {
            while (!_tokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    await VerifyStatusSession();
                    await Task.Delay(TimeSpan.FromSeconds(60), _tokenSource.Token);
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("Task canceled, stopping verification loop.");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
        private async Task VerifyStatusSession() {
            
            string token = Preferences.Get("token", "");
            if (!String.IsNullOrEmpty(token))
            {
                try {
                    HttpClient client = App._client.getClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    ApiResponse response = await client.GetFromJsonAsync<ApiResponse>("usuarios/verify/");
                    if (response != null)
                    {
                        if (response.isLogin.Value)
                        {
                            Console.WriteLine(response.message + "\n" + response.timestamps);
                        }
                        else
                        {
                            Preferences.Remove("token");
                            Console.WriteLine("Token Expirado");
                        }
                    }
                    else {
                        Console.WriteLine("Es nula la respuesta");
                    }
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            else {
                Console.WriteLine("No hay una sesion activa");
            }
        }
    }
}
