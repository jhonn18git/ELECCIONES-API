using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ClienteEleccion;

public static class Api
{
    static readonly HttpClient http = new() { BaseAddress = new Uri("http://127.0.0.1:8000/api/") };

    public static async Task<bool> Login(string email, string password)
    {
        var resp = await http.PostAsJsonAsync("login", new { email, password });
        if (!resp.IsSuccessStatusCode) return false;

        var datos = await resp.Content.ReadFromJsonAsync<RespuestaLogin>();
        // desde aqui todas las peticiones viajan con el token
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", datos!.Token);
        return true;
    }

    public static async Task<List<Candidato>> Listar() =>
        await http.GetFromJsonAsync<List<Candidato>>("candidatos") ?? new();

    public static Task<HttpResponseMessage> Crear(Candidato c) =>
        http.PostAsJsonAsync("candidatos", c);

    public static Task<HttpResponseMessage> Actualizar(Candidato c) =>
        http.PutAsJsonAsync($"candidatos/{c.Id}", c);

    public static Task<HttpResponseMessage> Eliminar(int id) =>
        http.DeleteAsync($"candidatos/{id}");
}

public record RespuestaLogin(string Token);
