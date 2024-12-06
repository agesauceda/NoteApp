using NoteApp.Models.common;
using System;
using System.Net.Http.Json;
using System.Threading;

namespace NoteApp.Controllers.Auth
{
    public class QueueLogin
    {
        private Thread _thread;
        private readonly HttpClient _client;
        public QueueLogin(Thread thread) {
            this._thread = thread;
            this._client = App._client.getClient();
        }

        public void Start() {
        }

        private async Task VerifyStatusSession() {
            string token = Preferences.Get("token", "");
            ApiResponse response = await _client.GetFromJsonAsync<ApiResponse>($"usuarios/verify?token={token}");
            if (response != null)
            {
                if (response.isLogin.Value) { }
                else { 
                    Preferences.Remove("token");
                    Console.WriteLine("Token Expirado Negro");
                }

            }
        }
    }
}
