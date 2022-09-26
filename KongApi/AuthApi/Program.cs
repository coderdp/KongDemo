using AuthApi.ViewModels;
using Libs;

namespace AuthApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<JwtOption>(builder.Configuration.GetRequiredSection("Jwt"));

            builder.Services.AddHttpContextAccessor();
            // Add services to the container.
            builder.Services.AddMvcCore();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}