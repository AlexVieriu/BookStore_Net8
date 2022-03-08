namespace BookStore.API.Models.DTOs.Users;
public class UserLogin
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
