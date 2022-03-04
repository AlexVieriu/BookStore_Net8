namespace BookStore.API.Static;
public static class ErrorMsg
{
    public static string StatusCode500(Exception ex)
    {
        return $"{ex.Message} - {ex.InnerException}";
    }
}
