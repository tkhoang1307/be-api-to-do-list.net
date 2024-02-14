using ToDoList.Entities;
using ToDoList.Model.RequestModels.Status;
using ToDoList.Model.RequestModels.User;
using ToDoList.Model.ResponseModels.User;

namespace ToDoList.Repository.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> SignUpAccount(UserSignUpRequestModel userSignUpRequest);

        Task<List<User>> GetAllUsers();

        Task<User?> GetUserByUserId(Guid userId);

        Task<bool> IsExistedEmail(string email);

        Task<User?> UpdateUserInformation(Guid userId, UserUpdateInformationRequestModel userDataUpdate);
    }
}
