using NoteApp.Models.common;
using NoteApp.Models.RegisterPage;
using System.Net.Http.Json;

namespace NoteApp.Services.RegisterPage
{
    public class RegisterPageService : RegisterPageServiceInterface
    {
        private readonly HttpClient _client;
        public RegisterPageService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<ApiResponse> RegisterUser(UserPOST? e)
        {
            using HttpResponseMessage response = await _client.PostAsJsonAsync("usuarios/register/", e);
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadFromJsonAsync<ApiResponse>();
                if (content != null) {
                    return content;
                }
            }
            return new ApiResponse { status = false, message = "Error al registrar usuario" };
        }
    }
}
