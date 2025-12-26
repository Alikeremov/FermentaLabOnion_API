using FermentaLabOnion.Domain.Entities;
using FermentaLabOnion.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using FermentaLabOnion.Application.Abstraction.Repositories;
using FermentaLabOnion.Persistence.Implementations.Repositories;
using FermentaLabOnion.Application.Abstraction.Services;
using FermentaLabOnion.Persistence.Implementations.Services;

namespace FermentaLabOnion.Persistence.ServiceRegistrations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 8;

                opt.User.RequireUniqueEmail = true;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.AllowedForNewUsers = false;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUrlHelper>(x => x.GetRequiredService<IUrlHelperFactory>().GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext));
            //Registraion of Repositories
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<ICategoryTranslateRepo, CategoryTranslateRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IProductImageRepo, ProductImageRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IProductTranslateRepo, ProductTranslateRepo>();
            services.AddScoped<ITagRepo, TagRepo>();
            services.AddScoped<ITagTranslateRepo, TagTranslateRepo>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IWishlistRepo, WishlistRepo>();
            services.AddScoped<IWishlistItemRepo, WishlistItemRepo>();
            services.AddScoped<IAppRepo, AppRepo>();
            services.AddScoped<IAppTranslateRepo, AppTranslateRepo>();
            services.AddScoped<IChineseWisdomRepo, ChineseWisdomRepo>();
            services.AddScoped<IChineseWisdomTranslateRepo, ChineseWisdomTranslateRepo>();
            services.AddScoped<IHeritageProcessRepo, HeritageProcessRepo>();
            services.AddScoped<IHeritageProcessTranslateRepo, HeritageProcessTranslateRepo>();
            services.AddScoped<IInformationRepo, InformationRepo>();
            services.AddScoped<IInformationTranslateRepo, InformationTranslateRepo>();
            services.AddScoped<IQuestionRepo, QuestionRepo>();
            services.AddScoped<IQuestionTranslateRepo, QuestionTranslateRepo>();
            services.AddScoped<IShareSpecialRepo, ShareSpecialRepo>();
            services.AddScoped<IShareSpecialTranslateRepo, ShareSpecialTranslateRepo>();
            //Registration of Services
            services.AddScoped<IAutenticationService, AutenticationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryTranslateService, CategoryTranslateService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ITagTranslateService, TagTranslateService>();
            services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductTranslateService, ProductTranslateService>();
            services.AddScoped<IWishlistService, WishlistService>();
            services.AddScoped<IAppService, AppService>();
            services.AddScoped<IAppTranslateService, AppTranslateService>();
            services.AddScoped<IChineseWisdomService, ChineseWisdomService>();
            services.AddScoped<IChineseWisdomTranslateService, ChineseWisdomTranslateService>();
            services.AddScoped<IHeritageProcessService, HeritageProcessService>();
            services.AddScoped<IHeritageProcessTranslateService, HeritageProcessTranslateService>();
            services.AddScoped<IInformationService, InformationService>();
            services.AddScoped<IInformationTranslateService, InformationTranslateService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IQuestionTranslateService, QuestionTranslateService>();
            services.AddScoped<IShareSpecialService, ShareSpecialService>();
            services.AddScoped<IShareSpecialTranslateService, ShareSpecialTranslateService>();



            return services;
        }
    }
    }
