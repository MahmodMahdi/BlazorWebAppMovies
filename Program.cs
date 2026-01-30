using BlazorWebAppMovies;
using BlazorWebAppMovies.Components;
using BlazorWebAppMovies.Data;
using BlazorWebAppMovies.Middlewares;
using BlazorWebAppMovies.Models;
using BlazorWebAppMovies.Services.AuthenticationService;
using BlazorWebAppMovies.Services.AuthService;
using BlazorWebAppMovies.Services.GenreService;
using BlazorWebAppMovies.Services.FileService;
using BlazorWebAppMovies.Services.MovieService;
using BlazorWebAppMovies.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<BlazorWebAppMoviesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlazorWebAppMoviesContext") ?? throw new InvalidOperationException("Connection string 'BlazorWebAppMoviesContext' not found.")));

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<BlazorWebAppMoviesContext>()
.AddDefaultTokenProviders();

// to match auth with componants
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddDbContext<BlazorWebAppMovieContext>(option =>
//option.UseSqlServer(builder.Configuration.GetConnectionString("DB")));


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();
