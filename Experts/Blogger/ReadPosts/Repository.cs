﻿using Common.Solutions.Data.MainDB;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Experts.Blogger.ReadPosts;


public class Repository(MainDB db) : Story.IRepository {
    public async Task<IEnumerable<global::Common.Model.Post>> Read(Story.Request request, CancellationToken token) {
        var solutionModel = await db
            .Posts
            .Include(x => x.PostTags)
            .ThenInclude(x => x.Tag)
            .Where(post => post.Title.Contains(request.Title) || post.Content.Contains(request.Content))
            .ToListAsync(token);

        var problemModel = solutionModel
            .Select(model => new Common.Model.Post() {
                Title = model.Title,
                Content = model.Content
            });

        return problemModel;
    }
}