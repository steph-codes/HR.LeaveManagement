using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        //N.B <PackageReference Include="Microsoft.Extensions.Configuration" Version="6.0.0" /> and this <PackageReference Include = "Microsoft.Extensions.Options" Version="6.0.0" />
        //might throw error advice you use this  <PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="5.0.0" />
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
