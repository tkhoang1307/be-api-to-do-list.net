using ToDoList.Entities;
using ToDoList.Model.RequestModels.Status;
using ToDoList.Model.RequestModels.User;
using ToDoList.Model.ResponseModels;
using ToDoList.Model.ResponseModels.User;
using ToDoList.Service.IServices;
using ToDoList.UnitOfWork;

namespace ToDoList.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> GetAllUsers()
        {
            var response = new ApiResponse();

            var dataResponse = await _unitOfWork.UserRepository.GetAllUsers();

            List<UserInformationResponseModel> listDataUsers = new List<UserInformationResponseModel>();

            foreach (var user in dataResponse)
            {
                UserInformationResponseModel dataUser = new UserInformationResponseModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Telephone = user.Telephone,
                    Fullname = user.Fullname,
                };

                listDataUsers.Add(dataUser);
            }

            response.SetOk(listDataUsers);

            return response;
        }

        public async Task<ApiResponse> GetUserByUserId(Guid userId)
        {
            var response = new ApiResponse();

            var dataResponse = await _unitOfWork.UserRepository.GetUserByUserId(userId);

            if (dataResponse != null)
            {
                UserInformationResponseModel dataUser = new UserInformationResponseModel()
                {
                    Id = dataResponse.Id,
                    Email = dataResponse.Email,
                    Telephone = dataResponse.Telephone,
                    Fullname = dataResponse.Fullname,
                };

                response.SetOk(dataUser);
            }
            else
            {
                response.SetNotFound(null, "User not found");
            }

            return response;
        }

        public async Task<ApiResponse> SignUpAccount(UserSignUpRequestModel userSignUpRequest)
        {
            var response = new ApiResponse();

            bool isExistedEmail = await _unitOfWork.UserRepository.IsExistedEmail(userSignUpRequest.Email);

            if (!isExistedEmail)
            {
                var dataUser = new UserSignUpRequestModel()
                {
                    Email = userSignUpRequest.Email,
                    Telephone = userSignUpRequest.Telephone,
                    Fullname = userSignUpRequest.Fullname,
                    Password = BCrypt.Net.BCrypt.HashPassword(userSignUpRequest.Password),
                };

                var dataResponse = await _unitOfWork.UserRepository.SignUpAccount(dataUser);

                UserInformationResponseModel dataUserResponse = new UserInformationResponseModel()
                {
                    Id = dataResponse.Id,
                    Email = dataResponse.Email,
                    Telephone = dataResponse.Telephone,
                    Fullname = dataResponse.Fullname,
                };

                response.SetOk(dataUserResponse);
            }    
            else
            {
                response.SetBadRequest(null, userSignUpRequest.Email + " existed");
            }    

            return response;
        }

        public async Task<ApiResponse> UpdateUserInformation(Guid userId, UserUpdateInformationRequestModel userUpdateRequest)
        {
            var response = new ApiResponse();

            var dataResponse = await _unitOfWork.UserRepository.UpdateUserInformation(userId, userUpdateRequest);

            if (dataResponse != null)
            {
                UserInformationResponseModel dataUser = new UserInformationResponseModel()
                {
                    Id = dataResponse.Id,
                    Email = dataResponse.Email,
                    Telephone = dataResponse.Telephone,
                    Fullname = dataResponse.Fullname,
                };

                response.SetOk(dataUser);
            }
            else
            {
                response.SetNotFound(null, "User not found");
            }

            return response;
        }
    }
}
