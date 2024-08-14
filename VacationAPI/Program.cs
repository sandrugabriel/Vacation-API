using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using VacationAPI.Data;
using VacationAPI.Repository;
using VacationAPI.Repository.interfaces;
using VacationAPI.Service;
using VacationAPI.Service.interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IRepository, RepositoryVacation>();
        builder.Services.AddScoped<ICommandService, CommandService>();
        builder.Services.AddScoped<IQueryService, QueryService>();

        builder.Services.AddDbContext<AppDbContext>(option => option.UseMySql(builder.Configuration.GetConnectionString("Default")!,
            new MySqlServerVersion(new Version(8, 0, 6))));
        builder.Services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddMySql5().WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
                .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();


        using (var scope = app.Services.CreateScope())
        {
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }



        app.Run();
    }
}