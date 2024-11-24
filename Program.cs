using Microsoft.EntityFrameworkCore;
using TodoApp.Contexts;

namespace TodoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            app.MapControllerRoute("default", "{controller=Todo}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
