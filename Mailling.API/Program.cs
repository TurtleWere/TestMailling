using Mailling.Application;

namespace Mailling.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            
            builder.Services.AddScoped<MailService>();


            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

            //app.UseRouting();
            //app.UseAuthentication();

            app.MapControllers();

            //app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}