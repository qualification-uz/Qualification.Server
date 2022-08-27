using Qualification.Service.DTOs;

namespace Qualification.Service.AvloniyClient;

public interface IAvloniyClientService
{
    ValueTask<ERPResponse> IsUserRegistered(string username, string password);    
}