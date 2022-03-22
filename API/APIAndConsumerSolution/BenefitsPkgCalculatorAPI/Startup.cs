using BPCalcApi.Entities.Interfaces;
using BPCalcAPI.Entities;
using BPCalcAPI.Mapper.Interfaces;
using BPCalcAPI.Mappers;
using BPCalcAPI.Rule.Interfaces;
using BPCalcAPI.Rules;
using BPCalcAPI.Task.Interfaces;
using BPCalcAPI.Tasks;
using BPCalcAPI.Workflow.Interfaces;
using BPCalcAPI.Workflows;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsPkgCalculatorAPI
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

            services.AddControllers();

            services.AddCors(options => {

                options.AddPolicy("CustomPolicyForRazorApp",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BenefitsPkgCalculatorAPI", Version = "v1" });
            });
            //mappers
            services.Add(new ServiceDescriptor(typeof(IExternalRequestToWorkFlowMapper), typeof(ExternalRequestToWorkFlowMapper), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IWorkFlowToExternReturnResponseMapper), typeof(WorkFlowToExternReturnResponseMapper), ServiceLifetime.Transient));
            
            //workflows
            services.Add(new ServiceDescriptor(typeof(ICalculateBenefitsCostWF), typeof(CalculateBenefitsCostWF), ServiceLifetime.Transient));

            //tasks
            services.Add(new ServiceDescriptor(typeof(ICalculateBPCostForAMemberTask), typeof(CalculateBPCostForAMemberTask), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(ICalcSummaryLevelAmtsForEmployeeTask), typeof(CalcSummaryLevelAmtsForEmployeeTask), ServiceLifetime.Transient));

            //rules
            //IGetBaseAmountForMemberTypeRule
            services.Add(new ServiceDescriptor(typeof(IGetCostOfBenefitPerPayCheckPerMemberTypeRule), typeof(GetCostOfBenefitPerPayCheckPerMemberTypeRule), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IDiscountForNameStarsWithARRule), typeof(DiscountForNameStarsWithARRule), ServiceLifetime.Transient));
            //rule engines

            //Business Entities
            services.Add(new ServiceDescriptor(typeof(IBpCalculationAttributes), typeof(BpCalculationAttributes), ServiceLifetime.Singleton));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BenefitsPkgCalculatorAPI v1"));
            }

            app.UseRouting();
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
