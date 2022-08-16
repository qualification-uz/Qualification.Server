namespace Qualification.Service.AvloniyClient;

public interface IAvloniyClientService
{
    Task<bool> IsUserRegistered(string username, string password);
    
}