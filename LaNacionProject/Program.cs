using LaNacionProject.DataAccess;
using LaNacionProject.Services.ContactService;
using Microsoft.EntityFrameworkCore;

namespace LaNacionProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ContextFactory>(opt =>
                opt.UseSqlServer(builder.Configuration["ConnectionStrings:LaNacionProject"]));
            builder.Services.AddScoped<IContactServices, ContactServices>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            //https://localhost:7163/swagger/index.html
            builder.Services.AddSwaggerGen();

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

            app.Run();
        }
    }
}