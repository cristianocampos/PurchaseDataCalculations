﻿using Newtonsoft.Json.Serialization;
using PurchaseData.ApplicationCore.BusinessServices;
using PurchaseData.ApplicationCore.Context;
using PurchaseData.ApplicationCore.Interfaces;

namespace PurchaseData.InterfaceLayer;

internal static class StartupHelperExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(configure =>
        {
            configure.ReturnHttpNotAcceptable = true;
        })
        .AddNewtonsoftJson((options) =>
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        });

        builder.Services.AddLogging(logbuilder =>
            logbuilder.AddFile(builder.Configuration.GetSection("FileLogging"))
        );

        builder.Services.AddScoped<IVatCalculationsService, VatCalculationsService>();
        builder.Services.AddScoped<IVatRateServiceContext, VatRateServiceContext>();
        builder.Services.AddScoped<IVatRateService, AustriaVatRateService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //builder.Services.AddGlobalErr

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // A custom exception Response when API is in production
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(
                        "An unexpected fault happened. Try again later.");
                });
            });
        }

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}

//default behaviour is to write exception to string, good for debugging, local.
