using NoteApp.Models.common.ResponseMailer;
using System.Net.Http.Json;

namespace NoteApp.Services.MailSender
{
    public class Mailer
    {
        private HttpClient client;
        public Mailer() {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://correo.stcentral.hn")
            };
        }

        public async Task<ApiResponseMail> MailRequest(string cc, string title, string msg)
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
            catch (Exception ex) {
                Console.WriteLine("Error exp: " + ex.Message);
                return new ApiResponseMail
                {
                    StatusCode = 500,
                    Status = "Error",
                    Data = new Data
                    {
                        Msg = new List<string> { ex.Message },
                        Content = new Content { Status = false }
                    },
                    Timestamp = DateTime.Now.ToString()
                };
            }
        }
    }
}
