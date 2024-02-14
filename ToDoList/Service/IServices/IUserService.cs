using ToDoList.Model.RequestModels.Status;
using ToDoList.Model.RequestModels.User;
using ToDoList.Model.ResponseModels;

namespace ToDoList.Service.IServices
{
    public interface IUserService
    {
        Task<ApiResponse> GetAllUsers();
        Task<ApiResponse> GetUserByUserId(Guid userId);
        Task<ApiResponse> SignUpAccount(UserSignUpRequestModel userSignUpRequest);
        Task<ApiResponse> UpdateUserInformation(Guid userId, UserUpdateInformationRequestModel userUpdateRequest);
    }
}
