using Netforemost_Todo.Dtos;
using System.Text.Json;
using System.Text;

namespace Netforemost_Todo.Services
{
    public class TareaService
    {
        private readonly HttpClient _httpClient;

        private readonly string _apiKey;
        public TareaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ApiSettings:ApiKey"]!;
        }

        public async Task<TareaResponse> GetTareasAsync(int UserId, int? pageSize, int? pageNumber)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7191/api/v1.0/usuario/{UserId}/tareas?pageSize={pageSize}&pageNumber={pageNumber}");
            request.Headers.Add("X-API-KEY", _apiKey);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode) { 
                return await response.Content.ReadFromJsonAsync<TareaResponse>() ?? new TareaResponse();
            }
            return new TareaResponse();
        }

        public async Task<bool> DeleteTareaAsync(int IdUsuario,int TareaId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7191/api/v1.0/usuario/{IdUsuario}/tareas/{TareaId}");
            request.Headers.Add("X-API-KEY", _apiKey);
            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> AddTareaAsync(int IdUsuario,Tarea tarea)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7191/api/v1.0/usuario/{IdUsuario}/tareas/");
            request.Headers.Add("X-API-KEY", _apiKey);

            //Serializa el objeto UsuarioItem a JSON
            var json = JsonSerializer.Serialize(tarea);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Establece el contenido en la solicitud
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            return true;
        }
        public async Task<bool> UpdateTareaAsync(int IdUsuario,Tarea tarea)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7191/api/v1.0/usuario/{IdUsuario}/tareas/{tarea.Id}");
            request.Headers.Add("X-API-KEY", _apiKey);

            //Serializa el objeto UsuarioItem a JSON
            var json = JsonSerializer.Serialize(tarea);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Establece el contenido en la solicitud
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            return true;
        }
    }
}
