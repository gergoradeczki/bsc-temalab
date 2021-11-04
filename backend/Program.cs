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
            var tr = new TodoRepository(TestConn.SqlConnectionString);

            Console.WriteLine("Columns:");
            foreach (var c in cr.GetAll())
            {
                Console.WriteLine($"\tID: {c.ID}, Name: {c.Name}");
                if(c.Todos != null)
                    foreach (var t in c.Todos)
                        Console.WriteLine($"\t\tID: {t.ID}, ColumnID: {t.ColumnID}, Name: {t.Name}, Deadline: {t.Deadline}, Description: {t.Description}, State: {t.State}");
            }

            Console.WriteLine("Todos:");
            foreach(var t in tr.GetAll())
                Console.WriteLine($"\tID: {t.ID}, ColumnID: {t.ColumnID}, Name: {t.Name}, Deadline: {t.Deadline}, Description: {t.Description}, State: {t.State}");


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