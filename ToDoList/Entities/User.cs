namespace ToDoList.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Fullname {  get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public List<UserStory>? UserStories { get; set; }
    }
}
