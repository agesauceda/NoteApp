using NoteApp.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Services.EventPage
{
        public class EventPageDelete
        {
            private readonly HttpClient _client = App._client.getClient();

            public async Task<ApiResponse> DeleteNoteEvent(int id)
            {
                ApiResponse response = null;
                try
                {
                    using HttpResponseMessage res = await _client.DeleteAsync($"recordatorios/{id}");
                    if (res.IsSuccessStatusCode)
                    {
                        var data = await res.Content.ReadFromJsonAsync<ApiResponse>();
                        response = data;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return response;
            }
        }
    }
