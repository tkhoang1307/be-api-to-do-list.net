using Microsoft.EntityFrameworkCore;
using ToDoList.Entities;
using ToDoList.Model.RequestModels.Status;
using ToDoList.Repository.IRepositories;

namespace ToDoList.Repository.Repositories
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        public StatusRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<Status?> CreateNewStatus(StatusCreationModel statusCreationData)
        {
            var newStatus = new Status
            {
                Name = statusCreationData.Name
            };

            _dbSet.AddAsync(newStatus);
            await _context.SaveChangesAsync();

            return newStatus;
        }

        public async Task<List<Status>> GetAllStatuses()
        {
            IQueryable<Status> query = _dbSet;
            var userStoryEntity = await query.ToListAsync();

            return userStoryEntity;
        }

        public async Task<Status?> UpdateStatus(Guid statusId, StatusUpdationRequestModel statusUpdationRequest)
        {
            IQueryable<Status> query = _dbSet;

            Status? dataStatus = await query.Where(status => status.Id == statusId).FirstOrDefaultAsync();

            if (dataStatus != null)
            {
                dataStatus.Name = statusUpdationRequest.Name;

                await _context.SaveChangesAsync();
            }

            return dataStatus;
        }

        public async Task<bool> IsExistedStatus(Guid idStatus)
        {
            IQueryable<Status> query = _dbSet;

            Status? dataStatus = await query.Where(u => u.Id == idStatus).FirstOrDefaultAsync();

            if (dataStatus == null)
            {
                return false;
            }

            return true;
        }
    }
}
