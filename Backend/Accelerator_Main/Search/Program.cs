using Data.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Search
{
    /// <summary>
    /// ������ ������
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ����� �����
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // ��� �������
            var projectName = Assembly.GetExecutingAssembly().GetName().Name;

            // ��� ���������
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // ��� ��� ��������������
            var indexName = $"{projectName?.ToLower().Replace(".", "_")}_{envName?.ToLower().Replace(".", "_")}";

            InitLogging.ConfigureLogging(indexName);

            // �������� �����
            CreateHost(args);
        }

        /// <summary>
        /// �������� �����
        /// </summary>
        /// <param name="args"></param>
        private static void CreateHost(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", ex);
                throw;
            }
        }

        /// <summary>
        /// ������ �����
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration(configuration =>
                {
                    configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    configuration.AddJsonFile(
                        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                        optional: true);
                })
                .UseSerilog();
        }
    }
}