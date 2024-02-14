using Microsoft.AspNetCore.Cors.Infrastructure;
using ToDoList.Entities;
using ToDoList.Model.RequestModels.UserStory;
using ToDoList.Model.ResponseModels;
using ToDoList.Model.ResponseModels.Status;
using ToDoList.Model.ResponseModels.User;
using ToDoList.Model.ResponseModels.UserStory;
using ToDoList.Service.IServices;
using ToDoList.UnitOfWork;

namespace ToDoList.Service.Services
{
    public class UserStoryService : IUserStoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserStoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> GetUserStoryByUserId(Guid userId)
        {
            var response = new ApiResponse();

            var dataResponse = await _unitOfWork.UserStoryRepository.GetAllUserStoriesByUserId(userId);

            if (dataResponse != null)
            {
                List<UserStoryResponseModel> listUserStories = new List<UserStoryResponseModel>();
                foreach (var us in dataResponse)
                {
                    var userStory = new UserStoryResponseModel()
                    {
                        Id = us.Id,
                        Name = us.Name,
                        Description = us.Description,
                        CreatedAt = us.CreatedAt,
                        User = new UserInformationResponseModel()
                        {
                            Id = us.User.Id,
                            Email = us.User.Email,
                            Telephone = us.User.Telephone,
                            Fullname = us.User.Fullname,
                        },
                        Status = new StatusResponseModel()
                        {
                            Id = us.Status.Id,
                            Name = us.Status.Name,
                        },
                    };

                    listUserStories.Add(userStory);
                }

                response.SetOk(listUserStories);
            }
            else
            {
                response.SetNotFound(null, "You don't have user story");
            }

            return response;
        }

        public async Task<ApiResponse> GetUserStoryByUSId(Guid usId)
        {
            var response = new ApiResponse();

            var dataResponse = await _unitOfWork.UserStoryRepository.GetUserStoryByUSId(usId);

            if (dataResponse != null)
            {
                var userStory = new UserStoryResponseModel()
                {
                    Id = dataResponse.Id,
                    Name = dataResponse.Name,
                    Description = dataResponse.Description,
                    CreatedAt = dataResponse.CreatedAt,
                    User = new UserInformationResponseModel()
                    {
                        Id = dataResponse.User.Id,
                        Email = dataResponse.User.Email,
                        Telephone = dataResponse.User.Telephone,
                        Fullname = dataResponse.User.Fullname,
                    },
                    Status = new StatusResponseModel()
                    {
                        Id = dataResponse.Status.Id,
                        Name = dataResponse.Status.Name,
                    },
                };
                response.SetOk(userStory);
            }
            else
            {
                response.SetNotFound(null, "User story not found");
            }

            return response;
        }

        public async Task<ApiResponse> CreateUserStory(UserStoryCreationRequestModel userStoryCreation)
        {
            var response = new ApiResponse();

            var dataResponse = await _unitOfWork.UserStoryRepository.CreateUserStory(userStoryCreation);

            response.SetOk(dataResponse);

            return response;
        }

        public async Task<ApiResponse> UpdateUserStory(Guid usId, UserStoryUpdationRequestModel userStoryUpdation)
        {
            var response = new ApiResponse();

            var dataResponse = await _unitOfWork.UserStoryRepository.UpdateUserStory(usId, userStoryUpdation);

            response.SetOk(dataResponse);

            return response;
        }
    }
}
