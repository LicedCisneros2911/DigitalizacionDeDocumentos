using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sistema_registro_documentacion.Data;
using Sistema_registro_documentacion.Models;
using Sistema_registro_documentacion.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string conectionstring = Configuration.GetConnectionString("DefaultConnection");
            services.Configure<ApplicationDBContext>(Configuration.GetSection("DefaultConnection")); 
            services.AddDbContext<ApplicationDBContext>(options => options.UseMySql(conectionstring, ServerVersion.AutoDetect(conectionstring)));
            services.AddScoped<IGenericRepository<Documento>, DocumentoRepositoryEF>();
            services.AddScoped<IGenericRepository<Usuario>, UsuarioRepositoryEF>();
            services.AddScoped<IGenericRepository<Login>, LoginRepositoryEF>();
            services.AddScoped<IGenericRepository<Formulario_gestion>, GestionRepositoryEF>();
            services.AddScoped<IGenericRepository<Formulario_tipo>, TipoRepositoryEF>();
            services.AddScoped<IGenericRepository<Formulario_folder>, FolderRepositoryEF>();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = $"/Login/Login";
                    options.LogoutPath = $"/Login/Login";
                    options.Cookie.Name = "MyCookieNamNam";
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);

                });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
