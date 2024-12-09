using NoteApp.Models.common;
using NoteApp.Models.ResetPassword;
using System.Net.Http.Json;

namespace NoteApp.Services.ResetPassword
{
    public class ResetPasswordService : ResetPasswordServiceInterface
    {
        private readonly HttpClient _client;
        public ResetPasswordService(HttpClient client) {
            _client = client;
        }
        public async Task<ApiResponse> ResetPasswordRequest(ResetPasswordPOST e)
        {
            try
            {
                using HttpResponseMessage response = await _client.PostAsJsonAsync("usuarios/reset", e);
                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadFromJsonAsync<ApiResponse>();
                    if (content != null) {
                        return content;
                    }
                }
                return new ApiResponse { status = false, message = "Error al resetear contraseña" };
            }
            catch (Exception ex) { 
                return new ApiResponse { data = null, message = ex.Message, status = false };
            }
        }
    }
}
