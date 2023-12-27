using Core.Application;
using Core.Application.Plugins.TaskTry;
using Core.Enterprise;
using Users.Blogger;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddRazorPages();

builder.Services.AddCore();
builder.Services.AddCommon(config);
builder.Services.AddBlogger();
builder.Services.AddSingleton<Game>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
} else
{
    app.UseDataBase();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
