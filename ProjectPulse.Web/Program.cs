using ProjectPulse.Web.Components;
using ProjectPulse.Web.Services.Accounts;
using ProjectPulse.Web.Services.Projects;
using ProjectPulse.Web.Services.Tasks;
using ProjectPulse.Web.Services.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<HttpClient>(sp =>
{
    var client = new HttpClient { BaseAddress = new Uri("http://localhost:5108/") };
    return client;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAccountsService, AccountsService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<ITasksService, TasksService>();

builder.Services.AddAuthentication("Auth")
    .AddCookie("Auth", options =>
    {            
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
        options.LoginPath = "/account/login";
    });
builder.Services.AddCascadingAuthenticationState();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();