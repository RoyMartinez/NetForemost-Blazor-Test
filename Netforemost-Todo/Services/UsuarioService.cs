using Netforemost_Todo.Dtos;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text;
namespace Netforemost_Todo.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;

        private readonly string _apiKey;
        public UsuarioService(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ApiSettings:ApiKey"]!;
        }

        public async Task<UsuarioResponse> GetUsuariosAsync(int? pageSize, int? pageNumber)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7191/api/v1.0/usuario?pageSize={pageSize}&pageNumber={pageNumber}");
            request.Headers.Add("X-API-KEY", _apiKey);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UsuarioResponse>() ?? new UsuarioResponse();
        }

        public async Task<bool> DeleteUsuariosAsync(int IdUsuario)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7191/api/v1.0/usuario/{IdUsuario}");
            request.Headers.Add("X-API-KEY", _apiKey);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return true;
        }
        public async Task<bool> AddUsuariosAsync(Usuario usuario)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7191/api/v1.0/usuario/");
            request.Headers.Add("X-API-KEY", _apiKey);

            // Serializa el objeto UsuarioItem a JSON
            var json = JsonSerializer.Serialize(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Establece el contenido en la solicitud
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return true;
        }
        public async Task<bool> UpdateUsuariosAsync(Usuario usuario)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7191/api/v1.0/usuario/{usuario.Id}");
            request.Headers.Add("X-API-KEY", _apiKey);

            // Serializa el objeto UsuarioItem a JSON
            var json = JsonSerializer.Serialize(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Establece el contenido en la solicitud
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
