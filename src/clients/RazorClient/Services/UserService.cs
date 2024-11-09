using RazorClient.Models;

namespace RazorClient.Services;

public class UserService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://localhost:5000/api/users/";

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<List<UserDto>> GetUsers(string keyword)
    {
        Console.WriteLine("djalwiujdilawjiladwjildawiljdawidw");
        var response = await _httpClient.GetAsync(BaseUrl + $"?keyword={keyword}");

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<List<UserDto>>();

        return null;
    }

    public async Task<UserDto> GetUserById(int id)
    {
        var response = await _httpClient.GetAsync(BaseUrl + id);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<UserDto>();

        return null;
    }

}
