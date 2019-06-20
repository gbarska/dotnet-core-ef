using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace ProductCatalog
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddResponseCompression();

            //cria apenas uma instancia
            services.AddScoped<StoreDataContext, StoreDataContext>();
            services.AddTransient<ProductRepository, ProductRepository>();
            //criar varias intancias
            // services.AddTransient<StoreDataContext,StoreDataContext>();

            services.AddSwaggerGen(x=>
            {
            x.SwaggerDoc("v1",new Info {Title = "Product Catalog", Version = "v1"});
            });
        }
       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseMvc();
            app.UseResponseCompression();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","Product Catalog - v1");
            });
        }
    }
}
