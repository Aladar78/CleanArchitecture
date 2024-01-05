﻿using Microsoft.EntityFrameworkCore;

namespace Users.Blogger.ReadPostsUserStory.ReadTask.DataAccessSocket.DataAccessPlugin;

public partial class Plugin(BlogDbContext db) : IDataAccessPlugin
{
    public async Task<List<DataModel.Post>> Read(string title, string content, CancellationToken token)
    {
        var pluginModel = await db
            .Posts
            .Where(post => 
                post.Title.Contains(title) || 
                post.Content.Contains(content))
            .ToListAsync(token);

        var dataModel = pluginModel;
        return dataModel;
    }
}