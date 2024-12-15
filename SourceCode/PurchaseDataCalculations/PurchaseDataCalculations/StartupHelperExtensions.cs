﻿namespace PurchaseData.InterfaceLayer
{
    internal static class StartupHelperExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(configure =>
            {
                configure.ReturnHttpNotAcceptable = true;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder.Build();
            //builder.Services.AddScoped<IVatCalculationsService, VATCalculationsService>();

            //builder.Services.AddControllers(options =>
            //{
            //    // For example: if the client puts a header: Accept: 'application/xml' it is going to return 406 Not aceptable
            //    options.ReturnHttpNotAcceptable = true;
            //    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
            //    _ => "The field is required.");
            //})
            //.AddNewtonsoftJson(setupAction =>
            //{
            //    setupAction.SerializerSettings.ContractResolver = null;
            //    //new CamelCasePropertyNamesContractResolver();
            //});

            //builder.Services.AddControllers(options =>
            //{
            //    options.ModelMetadataDetailsProviders.Add(new NewtonsoftJsonValidationMetadataProvider());
            //}).AddNewtonsoftJson();

            //builder.Services.AddProblemDetails();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
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

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}