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

            // クッキー認証サービスを追加
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    // 認証クッキー名を設定
                    options.Cookie.Name = "AspNetCoreCookieAuth";
                    // バックエンドでユーザアカウントが無効になっているか検知するイベントを設定
                    options.EventsType = typeof(CustomCookieAuthenticationEvents);
                });
            services.AddScoped<CustomCookieAuthenticationEvents>();

            // 認可サービスを追加（クレームベースの認可）
            services.AddAuthorization(options =>
            {
                // 管理者クレームが必要なポリシーを追加
                options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
            });
            
            // MVC サービスを追加
            var mvcBuilder = services.AddControllersWithViews();

            // 開発時のみ Razor の実行時コンパイルを有効化
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

            // パイプラインに認証・認可ミドルウェアを追加
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
