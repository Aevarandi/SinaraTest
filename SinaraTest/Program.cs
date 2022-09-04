using Microsoft.EntityFrameworkCore;
using SinaraTest;
using SinaraTest.Data;
using SinaraTest.DataLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IActiveDirectoryService, ActiveDirectoryService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddControllers();

builder.Services.AddDbContext<InMemoryContext>(x => x.UseInMemoryDatabase("Test"));
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var userRepository = scope.ServiceProvider.GetService<IUsersRepository>();
    SampleData.InitData(userRepository);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();