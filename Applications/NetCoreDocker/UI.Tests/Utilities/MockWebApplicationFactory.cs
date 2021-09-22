using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace UI.Tests.Utilities {
    public class MockWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class {


        protected override void ConfigureWebHost (IWebHostBuilder builder) {
            builder.ConfigureServices (services => {
                
                services.AddHttpClient("api", api => { 
                    api.BaseAddress = new Uri ("http://localhostmocked:8080");
                }).ConfigurePrimaryHttpMessageHandler( () => {
                    return new MockHttpMessageHandler();
                });
                // Sample for DB InMemoryTesting
                // Build the service provider.
                // var sp = services.BuildServiceProvider ();
                // Create a scope to obtain a reference to the db context
                // using (var scope = sp.CreateScope ()) {
                //     var scopedServices = scope.ServiceProvider;
                //     var db = scopedServices.GetRequiredService<ApplicationDbContext> ();
                //     var logger = scopedServices.GetRequiredService<ILogger<MockWebApplicationFactory<TStartup>>> ();
                //      // Ensure the database is created.
                //      db.Database.EnsureCreated ();
                //     try {
                //         // Seed the database with test data.
                //         //Write seeding logic later
                //         //Utilities.InitializeDbForTests(db);
                //     } catch (Exception ex) {
                //         logger.LogError (ex, $"An error occurred seeding the " +
                //             "database with test messages. Error: {ex.Message}");
                //     }
                // }
            });
        }     
    }
}