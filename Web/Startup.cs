using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NuScien.Security;
using Trivial.Security;

namespace NuScien.Sample.Web
{
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the Startup class.
        /// </summary>
        /// <param name="configuration">The configuration instance.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Setup database and resource access client.
            ResourceAccessClients.Setup(() =>
            {
                var context = new AccountDbContext(UseSqlServer, "Server=.;Database=NuScien5;Integrated Security=True;");
                var readonlyContext = context;
                return Task.FromResult<IAccountDataProvider>(new AccountDbSetProvider(context));
            });
            OnPremisesBusinessContext.Factory = (client, isReadonly) => isReadonly
                ? new OnPremisesBusinessContext(client, UseSqlServer, "Server=.;Database=NuScien5;Integrated Security=True;")
                : new OnPremisesBusinessContext(client, UseSqlServer, "Server=.;Database=NuScien5;Integrated Security=True;");
        }

        /// <summary>
        /// Gets the configuration instance.
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static DbContextOptionsBuilder UseSqlServer(DbContextOptionsBuilder builder, string conn) => SqlServerDbContextOptionsExtensions.UseSqlServer(builder, conn);
    }
}
