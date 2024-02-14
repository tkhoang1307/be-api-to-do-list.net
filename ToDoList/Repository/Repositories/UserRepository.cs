using Microsoft.EntityFrameworkCore;
using ToDoList.Entities;
using ToDoList.Model.RequestModels.User;
using ToDoList.Model.ResponseModels.User;
using ToDoList.Repository.IRepositories;

namespace ToDoList.Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<List<User>> GetAllUsers()
        {
            IQueryable<User> query = _dbSet;
            var listUsers = await query.ToListAsync();

            return listUsers;
        }

        public async Task<User?> GetUserByUserId(Guid userId)
        {
            IQueryable<User> query = _dbSet;

            User? dataUser = await query.Where(u => u.Id == userId).FirstOrDefaultAsync();

            return dataUser;
        }

        public async Task<bool> IsExistedEmail(string email)
        {
            IQueryable<User> query = _dbSet;

            User? dataUser = await query.Where(u => u.Email == email).FirstOrDefaultAsync();

            if (dataUser == null)
            {
                return false;
            }

            return true;
        }

        public async Task<User?> SignUpAccount(UserSignUpRequestModel userSignUpRequest)
        {
            var dataCallAPI = new User()
            {
                Email = userSignUpRequest.Email,
                Telephone = userSignUpRequest.Telephone,
                Fullname = userSignUpRequest.Fullname,
                Password = userSignUpRequest.Password,
            };

            _dbSet.AddAsync(dataCallAPI);
            await _context.SaveChangesAsync();

            return dataCallAPI;
        }

        public async Task<User?> UpdateUserInformation(Guid userId, UserUpdateInformationRequestModel userDataUpdate)
        {
            IQueryable<User> query = _dbSet;

            User? dataUser = await query.Where(u => u.Id == userId).FirstOrDefaultAsync();

            if (dataUser  != null)
            {
                dataUser.Telephone = userDataUpdate.Telephone;
                dataUser.Fullname = userDataUpdate.Fullname;

                await _context.SaveChangesAsync();
            }

            return dataUser;
        }
    }
}
