using Qualification.Data.IRepositories;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}
