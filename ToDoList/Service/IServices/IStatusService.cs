using ToDoList.Model.RequestModels.Status;
using ToDoList.Model.ResponseModels;

namespace ToDoList.Service.IServices
{
    public interface IStatusService
    {
        Task<ApiResponse> GetAllStatuses();

        Task<ApiResponse> CreateNewStatus(StatusCreationModel statusCreationData);
        Task<ApiResponse> UpdateStatus(Guid statusId, StatusUpdationRequestModel statusUpdationRequest);
    }
}
