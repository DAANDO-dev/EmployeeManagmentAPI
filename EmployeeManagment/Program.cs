using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using EmployeeManagment.Data;
using EmployeeManagment.Reposetories;

namespace EmployeeManagment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseInMemoryDatabase("EmployeeDb")
                );


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCors", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
         



            // add the employee repository to the DI ( dependency injection)
            builder.Services.AddScoped<IEmplyeeRepositories, EmployeeRepositories>();


            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI( c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API VA");
                    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
                }
                
                );
            }

            app.UseCors("MyCors");
            
            // app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
