﻿namespace ToDoList.Entities
{
    public class UserStory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = new DateTime();

        public Guid CreatedBy { get; set; }
        public User User { get; set; } = null!;
        public Guid IdStatus { get; set; }
        public Status Status { get; set; } = null!;
    }
}
