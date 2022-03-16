namespace BookStore.UI.Client.Repositories.User;
public interface IUserService
{
    Task<Response<bool>> LoginAsync(UserLogin userLogin);
    Task LogoutAsync();
    Task<Response<bool>> RegisterAsync(UserRegister userRegister);  
}
