using Netforemost_Todo.Dtos;

namespace Netforemost_Todo.Services
{
    public class PrioridadService
    {
        private readonly HttpClient _httpClient;

        private readonly string _apiKey;
        public PrioridadService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ApiSettings:ApiKey"]!;
        }

        public async Task<PrioridadResponse> GetPrioridadAsync(int? pageSize, int? pageNumber)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7191/api/v1.0/prioridad/?pageSize={pageSize}&pageNumber={pageNumber}");
            request.Headers.Add("X-API-KEY", _apiKey);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PrioridadResponse>() ?? new PrioridadResponse();
            }
            return new PrioridadResponse();
        }
    }
}
