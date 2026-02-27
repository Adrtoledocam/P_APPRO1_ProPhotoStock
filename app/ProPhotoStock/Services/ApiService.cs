using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using ProPhotoStock.Models;


namespace ProPhotoStock.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "http://10.0.2.2:8080/api/";

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            var loginData = new { email = email, password = password };
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}auth/login", loginData);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            }
            return null;
        }

        public async Task<List<PhotoItem>> GetPhotosAsync()
            => await _httpClient.GetFromJsonAsync<List<PhotoItem>>($"{BaseUrl}photos");
        public async Task<List<PhotoItem>> GetPhotosByTagAsync(string tag) 
            => await _httpClient.GetFromJsonAsync<List<PhotoItem>>($"{BaseUrl}photos/tag/{tag}");

        public async Task<PhotoItem> GetPhotoByIdAsync(int id)
            => await _httpClient.GetFromJsonAsync<PhotoItem>($"{BaseUrl}photos/{id}");

        public async Task<bool> CreateContractAsync(ContractRequest req)
        {
            var token = Preferences.Get("jwt_token", "");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var res = await _httpClient.PostAsJsonAsync($"{BaseUrl}contracts", req);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterAsync(RegisterRequest userData)
        {
            try
            {
                // Usamos la IP 10.0.2.2 para el emulador
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}auth/register", userData);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
