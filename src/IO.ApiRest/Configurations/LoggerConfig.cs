                using System;
                using Elmah.Io.AspNetCore;
                using Microsoft.AspNetCore.Builder;
                using Microsoft.Extensions.Configuration;
                using Microsoft.Extensions.DependencyInjection;
                using Microsoft.AspNetCore.Diagnostics.HealthChecks;
                using Elmah.Io.AspNetCore.HealthChecks;
                using HealthChecks.MySql;
                using HealthChecks.UI.Client;

                namespace IO.ApiRest.Configurations
                {
                    public static class LoggerConfig
                    {
                        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
                        {
                            services.AddElmahIo(o =>
                            {
                                o.ApiKey = "07af0f8b106a451faf7c3d9acb538043";
                                o.LogId = new Guid("0483588c-673d-4e24-bc97-2a3f9845993e");
                            });

                            services
                                .AddHealthChecks()
                                .AddElmahIoPublisher(options =>
                                {
                                    options.ApiKey = "07af0f8b106a451faf7c3d9acb538043";
                                    options.LogId = new Guid("0483588c-673d-4e24-bc97-2a3f9845993e");
                                    options.HeartbeatId = "f3aee6be8d844319b4fc3c7c19ffabb8";

                                })
                                .AddCheck("Products", new MySqlHealthCheck(configuration.GetConnectionString("DefaultConnection")))
                                .AddMySql(configuration.GetConnectionString("DefaultConnection"), name: "MySQLDB");

                            services
                                .AddHealthChecksUI(setupSettings: setup =>
                                {
                                    //setup.AddHealthCheckEndpoint("endpoint1", "https://localhost:5001/healthz");
                                    //setup.AddHealthCheckEndpoint("endpoint2", "https://localhost:61219/healthz");

                                    // Set the maximum history entries by endpoint that will be served by the UI api middleware
                                    setup.MaximumHistoryEntriesPerEndpoint(50);
                                })
                                .AddInMemoryStorage();
                                //.AddMySqlStorage(configuration.GetConnectionString("DefaultConnection"));

                                return services;
                        }

                        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
                        {
                            app.UseElmahIo();

                            app
                                .UseEndpoints(config =>
                                {
                                    config.MapHealthChecks("/healthz", new HealthCheckOptions
                                    {
                                        Predicate = _ => true,
                                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                                    });
                                })
                              .UseEndpoints(config =>
                              {
                                  config.MapHealthChecksUI(setup =>
                                  {
                                      setup.UIPath = "/health-ui"; // this is ui path in your browser  
                                  });
                              });

                            return app;
                        }
                    }
                }