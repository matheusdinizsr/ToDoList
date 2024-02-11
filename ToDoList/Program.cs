using Microsoft.Extensions.Logging.Configuration;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Logging.SetMinimumLevel(LogLevel.Trace);
            builder.Logging.AddConfiguration();
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            var staticOptions = new StaticFileOptions();

            //staticOptions.RequestPath = new PathString("/Web");

            app.UseStaticFiles();

            app.Run();

            var a = new List<int>(new[] {10, 12});
            var b = a;
            b.Add(11);

            var x = 12;
            var y = x;
            y = 3;



            Dobra(a);
            a.Add(2);

        }

        public static void Dobra(List<int> a)
        {
            a.AddRange(a);
            Console.Write("o dobro eh :" + a);
        }
    }
}