using EmployeeDetails.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.Configure((context, app) =>
                {
                    var builder = WebApplication.CreateBuilder(args);

                    // Add services to the container.
                    builder.Services.AddControllersWithViews();
                    builder.Services.AddDbContext<ConnectionStringProvider>(options =>
                    {
                        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                        options.UseSqlServer(connectionString);
                    });

                    // Add Swagger
                    builder.Services.AddEndpointsApiExplorer();
                    builder.Services.AddSwaggerGen();

                    // Add CORS
                    builder.Services.AddCors(options =>
                    {
                        options.AddDefaultPolicy(policyBuilder =>
                        {
                            policyBuilder.AllowAnyOrigin()
                                         .AllowAnyHeader()
                                         .AllowAnyMethod();
                        });
                    });

                    var appBuilder = builder.Build();

                    // Configure the HTTP request pipeline.
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        appBuilder.UseDeveloperExceptionPage();
                        appBuilder.UseSwagger();
                        appBuilder.UseSwaggerUI(c =>
                        {
                            c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeDetails API V1");
                        });
                    }
                    else
                    {
                        appBuilder.UseExceptionHandler("/Home/Error");
                        appBuilder.UseHsts();
                    }

                    appBuilder.UseHttpsRedirection();
                    appBuilder.UseStaticFiles();
                    appBuilder.UseRouting();
                    appBuilder.UseCors();
                    appBuilder.UseAuthorization();

                    appBuilder.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });

                    appBuilder.Run();
                });
            });
}
