namespace BookStore.UI.Repositories.Contracts;
public interface IUserService
{
    Task<Response<bool>> LoginAsync(UserLogin login);
    Task LogoutAsync();
    Task<Response<bool>> RegisterAsync(UserRegister register);
}
