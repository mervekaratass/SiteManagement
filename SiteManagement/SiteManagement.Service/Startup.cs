//using AutoMapper;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SiteManagement.Data;
using SiteManagement.Data.Repository;
using SiteManagement.Schema;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;

namespace SiteManagement;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers();

        //dbcontext
        var dbType = Configuration.GetConnectionString("DbType");
        if (dbType == "Sql")
        {
            var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<SiteDbContext>(opts =>
            opts.UseSqlServer(dbConfig));
        }
        //else if (dbType == "PostgreSql")
        //{
        //    var dbConfig = Configuration.GetConnectionString("PostgreSqlConnection");
        //    services.AddDbContext<SiteDbContext>(opts =>
        //      opts.UseNpgsql(dbConfig));
        //}

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.RequireHttpsMetadata = false;
             options.TokenValidationParameters = new TokenValidationParameters()
             {
                 ValidAudience = "localhost",
                 ValidIssuer = "localhost",
                 ValidateAudience = true,
                 ValidateIssuer = true,
                 ValidateLifetime = true,
                 IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String
                 ("managementmanagementmanagementma"))
             };
         });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Site Management Collection", Version = "v1" });
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Sim Management for IT Company",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {securityScheme, new string[] { }}
            });
        });

        services.AddScoped<IValidator<ApartmentRequest>, ApartmentRequestValidators>();
        services.AddScoped<IValidator<BankRequest>, BankRequestValidators>();
        services.AddScoped<IValidator<DuesBillRequest>, DuesBillRequestValidators>();
        services.AddScoped<IValidator<MessageRequest>, MessageRequestValidators>();
        services.AddScoped<IValidator<UserMessageRequest>, UserMessageRequestValidators>();
        services.AddScoped<IValidator<UserRequest>, UserRequestValidators>();

     

      

        //repositoryleri kullanmak için onları burda scope yapmam lazım
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IApartmentRepository, ApartmentRepository>();
        services.AddScoped<IBankRepository, BankRepository>();
        services.AddScoped<IDuesBillRepository, DuesBillRepository>();

        var config = new MapperConfiguration(cfg =>
        {//mapperı congif etmem lazım burda aşşağıda yaptım
            cfg.AddProfile(new MapperConfig());
        });
        services.AddSingleton(config.CreateMapper());


     


    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SiteManagement v1"));
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
