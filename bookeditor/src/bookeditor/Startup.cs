﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace bookeditor;
public class Startup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDataProtection();
        services.AddAuthorization();
        services.AddWebEncoders();

        services.AddDotVVM<DotvvmStartup>();
        services.AddSingleton(new XmlLibrary("./books", ["*"]));
        services.AddSingleton(new InMemoryNotificationsQueue());
        services.AddSingleton(new EditorStateCache()); 
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
        // Configure the HTTP request pipeline.
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHttpsRedirection();
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            app.UseSession();
        }

        // uncomment if you want to add MVC, SignalR or other technology which uses ASP.NET Core endpoint routing
        //app.UseRouting();

        // uncomment to enable authorization
        //app.UseAuthorization();

        // use DotVVM
        var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);

        // use static files
        app.UseStaticFiles();
    }
}
