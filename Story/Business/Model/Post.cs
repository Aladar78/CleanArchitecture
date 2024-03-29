﻿namespace Common.Business.Model;

public record Post {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Tag> Tags { get; set; } = [];
}
