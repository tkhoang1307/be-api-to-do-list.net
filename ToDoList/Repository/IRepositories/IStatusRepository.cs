using ToDoList.Entities;
using ToDoList.Model.RequestModels.Status;

namespace ToDoList.Repository.IRepositories
{
    public interface IStatusRepository : IBaseRepository<Status>
    {
        Task<List<Status>> GetAllStatuses();

        Task<Status?> CreateNewStatus(StatusCreationModel statusCreationData);
        Task<Status?> UpdateStatus(Guid statusId, StatusUpdationRequestModel statusUpdationRequest);
    }
}
