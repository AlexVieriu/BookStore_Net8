namespace BookStore.API.Models.EF;
public class ApiUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
