using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL.Data;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Assignment2.Hubs;

namespace Assignment2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSignalR();

            // Cấu hình Session
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session
                options.Cookie.HttpOnly = true; // Bảo mật cookie
                options.Cookie.IsEssential = true; // Luôn sử dụng session
            });

            // Đăng ký IHttpContextAccessor để sử dụng Session trong PageModel
            builder.Services.AddHttpContextAccessor();

            // Tạo và cấu hình DbContext với SQL Server
            builder.Services.AddDbContext<NewsContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Đăng ký AutoMapper để ánh xạ giữa các lớp DTO và Entity
            // Thành (chỉ định assembly chứa MappingProfile)
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            // Hoặc nếu MappingProfile nằm trong BLL
            builder.Services.AddAutoMapper(typeof(BLL.MappingProfile).Assembly);

            // Cấu hình Dependency Injection cho các repository và service
            builder.Services.AddScoped<ISystemAccountRepository, SystemAccountRepository>();
            builder.Services.AddScoped<ISystemAccountService, SystemAccountService>();

            //------------------------------------- Category -------------------------------------
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            //------------------------------------- NewsArticle -------------------------------------
            builder.Services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
            builder.Services.AddScoped<INewsArticleService, NewsArticleService>();

            //------------------------------------- Tag -------------------------------------
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<ITagService, TagService>();
            //-------------------------------------- Report---------------------------------------------------------------------
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<IReportRepository, ReportRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Thêm middleware Session vào pipeline (QUAN TRỌNG)
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapHub<Assignment2.Hubs.NewsHub>("/newsHub");
            app.MapHub<Assignment2.Hubs.CategoryHub>("/categoryHub");

            app.Run();
        }
    }
}