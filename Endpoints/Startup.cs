using Endpoints.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Endpoints
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
            //var connectionString = Environment.GetEnvironmentVariable("\"Server=(localdb)\\\\MSSQLLocalDB;Database=TestData;Trusted_Connection=True;\"");
            var connectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_DBConnection");
            services.AddDbContext<MyDBContext>(opt => opt.UseSqlServer(connectionString));
            services.AddSwaggerGen();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Docu"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
