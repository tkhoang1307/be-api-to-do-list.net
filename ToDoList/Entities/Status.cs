namespace ToDoList.Entities
{
    public class Status
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public List<UserStory>? UserStories { get; set; }
    }
}
