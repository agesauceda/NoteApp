﻿using NoteApp.Models.Dashboard;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace NoteApp.Services.Dashboard
{
    public class DashboardService : DashboardServiceInterface
    {
        private readonly HttpClient _client;
        public DashboardService(HttpClient client) {
            _client = client;
        }

        public async Task<ApiResponseDashboard> GetDashboard()
        {
            try
            {
                string token = Preferences.Get("token", "");
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await _client.GetFromJsonAsync<ApiResponseDashboard>("dashboard/");
            }
            catch (Exception ex) { 
                return new ApiResponseDashboard { status = false, statusCode = 500, message = ex.Message, data = null, isLogin = false, timestamp = DateTime.Now.ToString() };
            }
        }
    }
}
