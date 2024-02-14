namespace ToDoList.Model.ResponseModels.User
{
    public class UserInformationResponseModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
    }
}
