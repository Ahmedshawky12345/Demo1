
using Demo1.Api.Common;
using Demo1.Application.Mappings;
using Demo1.Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.RateLimiting;
using System.Reflection;
using System.Threading.RateLimiting;

namespace Demo1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("conn")
                );
       
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Configure Rate Limiting
            builder.Services.AddRateLimiter(options =>
            {
                // Limit: 5 requests ??? 10 ????? ??? IP
                options.AddFixedWindowLimiter("fixed", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(20);   // ??????? ???????
                    opt.PermitLimit = 5;                     // ??? requests ??????? ????
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 0;                      // ???? ??? requests ?????? ?????? ?? ???????
                });
            });
            // add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200") // Angular app
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("AllowAngular");
            app.UseAuthorization();
            app.UseRateLimiter();   

            app.MapControllers().RequireRateLimiting("fixed");

            app.Run();
        }
    }
}
