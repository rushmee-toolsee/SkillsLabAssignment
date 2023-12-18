namespace BusinessLayer.Services
{
    public interface IAccountService
    {
        int GetLoginAccountID(string emailAddress);
        string GetUsername(string emailAddress);
        int GetUserId();
    }
}
