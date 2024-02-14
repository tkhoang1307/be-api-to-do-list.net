using ToDoList.Entities;
using ToDoList.Model.RequestModels.Status;
using ToDoList.Model.RequestModels.User;
using ToDoList.Model.ResponseModels;
using ToDoList.Model.ResponseModels.Status;
using ToDoList.Service.IServices;
using ToDoList.UnitOfWork;

namespace ToDoList.Service.Services
{
    public class StatusService : IStatusService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> GetAllStatuses()
        {
            var response = new ApiResponse();

            var dataReponse = await _unitOfWork.StatusRepository.GetAllStatuses();

            List<StatusResponseModel> listStatuses = new List<StatusResponseModel>();

            foreach (var status in dataReponse)
            {
                StatusResponseModel newStatus = new StatusResponseModel()
                {
                    Id = status.Id,
                    Name = status.Name,
                };

                listStatuses.Add(newStatus);
            }

            return response.SetOk(listStatuses);
        }

        public async Task<ApiResponse> CreateNewStatus(StatusCreationModel statusCreationData)
        {
            var response = new ApiResponse();

            var dataResponse = await _unitOfWork.StatusRepository.CreateNewStatus(statusCreationData);

            StatusResponseModel newStatus = new StatusResponseModel()
            {
                Id = dataResponse.Id,
                Name = dataResponse.Name,
            };

            response.SetOk(newStatus);

            return response;
        }

        public async Task<ApiResponse> UpdateStatus(Guid statusId, StatusUpdationRequestModel statusUpdationRequest)
        {
            var response = new ApiResponse();

            var dataResponse = await _unitOfWork.StatusRepository.UpdateStatus(statusId, statusUpdationRequest);

            StatusResponseModel newStatus = new StatusResponseModel()
            {
                Id = dataResponse.Id,
                Name = dataResponse.Name,
            };

            response.SetOk(newStatus);

            return response;
        }
    }
}
