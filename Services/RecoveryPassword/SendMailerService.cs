using NoteApp.Models.common.ResponseMailer;
using System.Net.Http.Json;

namespace NoteApp.Services.RecoveryPassword
{
    public class SendMailerService : SendMailerServiceInterface
    {
        private HttpClient client;
        public SendMailerService()
        {
            client = new HttpClient() {
                BaseAddress = new Uri("https://correo.stcentral.hn")
            };
        }

        public async Task<ApiResponseMail> SendMailRequest(string cc, string title, string msg)
        {
            try
            {
                var formData = new MultipartFormDataContent
                    {
                        { new StringContent(title), "titulo" },
                        { new StringContent(msg), "mensaje" },
                        { new StringContent(cc), "remitente" }
                    };

                using HttpResponseMessage response = await client.PostAsync("", formData);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ApiResponseMail>();
                }
                else
                {
                    return new ApiResponseMail
                    {
                        StatusCode = (int)response.StatusCode,
                        Status = "Error",
                        Data = new Data
                        {
                            Msg = new List<string> { "Error al enviar correo. Código de estado: " + response.StatusCode },
                            Content = new Content { Status = false }
                        },
                        Timestamp = DateTime.Now.ToString()
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error exp: " + e.Message);
                return new ApiResponseMail
                {
                    StatusCode = 500,
                    Status = "Error",
                    Data = new Data
                    {
                        Msg = new List<string> { e.Message },
                        Content = new Content { Status = false }
                    },
                    Timestamp = DateTime.Now.ToString()
                };
            }
        }
    }
}
