using Microsoft.EntityFrameworkCore;
using TerroscapeApp.Database;
using TerroscapeApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TerroscapeStatsContext>(options => 
    options.UseNpgsql(
        connectionString, 
        o => {
                o.MapEnum<DBEnums.GameNameEnum>("game_name_enum");
                o.MapEnum<DBEnums.SurvivorStateEnum>("survivor_state_enum");
                o.MapEnum<DBEnums.SurvivorWinEnum>("survivor_win_enum");
                o.MapEnum<DBEnums.KillerWinEnum>("killer_win_enum");
             }
        ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
