using Common.Solutions.Data.MainDB;
using Experts.Blogger;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddRazorPages();

builder
    .Services
    //.AddCoreApplication(builder.Configuration, builder.Environment.IsDevelopment())
    .AddBlogger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseDeveloperDataBase();
} else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();