using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using temalabor_2021_todo_backend.Data;
using temalabor_2021_todo_backend.DAL;

namespace temalabor_2021_todo_backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IColumnRepository, ColumnRepository>();
            services.AddScoped<ITodoRepository, TodoRepository>();

            services.AddDbContext<AppDbContext>
                (options => options.UseSqlServer(TestConn.SqlConnectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllers();

            services.AddCors(opt =>
            {
                opt.AddPolicy("policy", builder =>
                {
                    builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(e => e.MapControllers());

        }
    }
}