using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Assignment.Service2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddMassTransit(x => {
            //    x.AddConsumer<CreateSessionConsumer>();

            //    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg => {

            //        var host = cfg.Host(new Uri($"rabbitmq://{Configuration["RabbitMQHostName"]}"), hostConfig => {
            //            hostConfig.Username("guest");
            //            hostConfig.Password("guest");
            //        });

            //        cfg.ReceiveEndpoint(host, "nagpsession", ep => {
            //            ep.PrefetchCount = 16;
            //            ep.UseMessageRetry(mr => mr.Interval(2, 100));

            //            ep.ConfigureConsumer<CreateSessionConsumer>(provider);
            //        });
            //    }));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            var bus = app.ApplicationServices.GetService<IBusControl>();
            var busHandle = TaskUtil.Await(() =>
            {
                return bus.StartAsync();
            });

            lifetime.ApplicationStopping.Register(() =>
            {
                busHandle.Stop();
            });
        }
    }
}
