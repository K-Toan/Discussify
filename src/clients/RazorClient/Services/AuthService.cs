namespace RazorClient.Services;
public class AuthService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://localhost:5000/api/authentication";

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<HttpResponseMessage> LoginAsync(object loginData)
    {
        return await _httpClient.PostAsJsonAsync("http://localhost:5000/api/authentication/login", loginData);
    }

    public async Task<HttpResponseMessage> RegisterAsync(object registerData)
    {
        return await _httpClient.PostAsJsonAsync("http://localhost:5000/api/authentication/register", registerData);
    }
}
