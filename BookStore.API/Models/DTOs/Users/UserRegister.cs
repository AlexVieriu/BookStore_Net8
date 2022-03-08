namespace BookStore.API.Models.DTOs.Users;
public class UserRegister : UserLogin
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public string? Role { get; set; }

}
