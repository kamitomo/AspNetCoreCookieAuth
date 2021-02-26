using AspNetCoreCookieAuth.Auth;
using AspNetCoreCookieAuth.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreCookieAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            // �N�b�L�[�F�؃T�[�r�X��ǉ�
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    // �F�؃N�b�L�[����ݒ�
                    options.Cookie.Name = "AspNetCoreCookieAuth";
                    // �o�b�N�G���h�Ń��[�U�A�J�E���g�������ɂȂ��Ă��邩���m����C�x���g��ݒ�
                    options.EventsType = typeof(CustomCookieAuthenticationEvents);
                });
            services.AddScoped<CustomCookieAuthenticationEvents>();

            // �F�T�[�r�X��ǉ��i�N���[���x�[�X�̔F�j
            services.AddAuthorization(options =>
            {
                // �Ǘ��҃N���[�����K�v�ȃ|���V�[��ǉ�
                options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
            });
            
            // MVC �T�[�r�X��ǉ�
            var mvcBuilder = services.AddControllersWithViews();

            // �J�����̂� Razor �̎��s���R���p�C����L����
            if (this._env.IsDevelopment())
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            // �p�C�v���C���ɔF�؁E�F�~�h���E�F�A��ǉ�
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
