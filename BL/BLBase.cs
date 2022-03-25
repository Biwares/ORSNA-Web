using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BL

{
    public class BLBase
    {
        public string con { get; set; }

        protected const string AUDITSAVE = "Save";
        protected const string AUDITDELETE = "Delete";

        protected int cantidadElementosPorPagina = 15;

        protected string userId;

        public BLBase(string stringConnection)
        {
            con = stringConnection;
        }

        public BLBase(string stringConnection,string userId)
        {
            con = stringConnection;
            this.userId = userId;
        }
    }

    public static class AppSettingsConfig
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
           .Build();
    }
}
