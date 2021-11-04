using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using temalabor_2021_todo_backend.DAL;

namespace temalabor_2021_todo_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var host = CreateHostBuilder(args).Build();
            //CreateDbIfNotExists(host);

            var cr = new ColumnRepository(TestConn.SqlConnectionString);

            foreach(var c in cr.GetAll())
            {
                Console.WriteLine($"cID: {c.ID}, cName: {c.Name}");
                foreach(var t in c.Todos)
                    Console.WriteLine($"\ttID: {t.ID}, tName: {t.Name}, tDescription: {t.Description}");
            }

            //host.Run();

        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}