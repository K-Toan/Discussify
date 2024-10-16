namespace Discussify.PostService.Interfaces;

public interface IAppUserService
{
    Task<bool> UserExistsAsync(string userId);
}