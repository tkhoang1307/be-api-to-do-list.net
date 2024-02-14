namespace ToDoList.Model.RequestModels.User
{
    public class UserSignUpRequestModel
    {
        public string Email { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
