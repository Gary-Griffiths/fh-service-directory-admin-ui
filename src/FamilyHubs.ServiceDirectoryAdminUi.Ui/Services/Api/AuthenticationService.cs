using FamilyHubs.ServiceDirectoryAdminUi.Ui.Models;
using System.IO;
using System.Text;
using System.Text.Json;

namespace FamilyHubs.ServiceDirectoryAdminUi.Ui.Services.Api;

public interface IAuthService
{
    Task<BearerToken> Login(string username, string password);
}
public class AuthService : ApiService, IAuthService
{
    public AuthService(HttpClient client, IConfiguration configuration)
        : base(client)
    {
        client.BaseAddress = new Uri("https://localhost:7108/");
    }

    public async Task<BearerToken> Login(string username, string password)
    {
        var model = new ApiLoginModel
        {
            Username = username,
            Password = password
        };

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_client.BaseAddress + "api/Authenticate/login"),
            Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"),
        };

        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        //var r1 = await response.Content.ReadAsStreamAsync();
        //StreamReader reader = new StreamReader(r1);
        //string text = reader.ReadToEnd();
        

        var result = await JsonSerializer.DeserializeAsync<BearerToken>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new BearerToken();

        return result;

    }
}

public class ApiLoginModel
{
    public string? Username { get; set; }

    public string? Password { get; set; }
}
