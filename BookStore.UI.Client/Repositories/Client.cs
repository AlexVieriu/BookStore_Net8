namespace BookStore.UI.Client.Repositories.Base;
public partial class Client : IClient
{
    public HttpClient HttpClient { get { return _httpClient; } }
}
