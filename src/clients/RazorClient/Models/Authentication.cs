namespace RazorClient.Models;


public record LoginDto(string UserName, string Password);
public record RegisterDto(string Email, string UserName, string Password);
