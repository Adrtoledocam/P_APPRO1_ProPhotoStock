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
            _httpClient.MaxResponseContentBufferSize = 256000000;
            _httpClient.Timeout = TimeSpan.FromMinutes(3);
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
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}auth/register", userData);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ContractItem>> GetContractsByRoleAsync()
        {
            var token = Preferences.Get("jwt_token", "");
            var role = Preferences.Get("user_role", "").ToLower();

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            string endpoint = role switch
            {
                "client" => "contracts/my-contracts",
                "photographer" => "contracts/my-contracts",
                "admin" => "contracts/my-contracts",
                _ => ""
            };

            return await _httpClient.GetFromJsonAsync<List<ContractItem>>($"{BaseUrl}{endpoint}");
        }
        public async Task<int?> UploadPhotoAsync(object photoData)
        {
            try
            {
                var token = Preferences.Get("jwt_token", "");
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var json = System.Text.Json.JsonSerializer.Serialize(photoData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{BaseUrl}photos", content);
                if (response.IsSuccessStatusCode)
                {
                    
                    var result = await response.Content.ReadFromJsonAsync<UploadResponse>();
                    return result?.photoId;
                }
                var statusCode = (int)response.StatusCode;
                var error = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"SERVER ERROR: {error}");
                System.Diagnostics.Debug.WriteLine($"[API ERROR] Status: {statusCode}");

                return null;
            }
            catch { return null; }
        }

        public class UploadResponse { public string message { get; set; } public int photoId { get; set; } }
    }
}
