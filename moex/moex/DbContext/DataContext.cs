using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using moex.Models;

namespace moex.DbContext
{
    public class DataContext : Microsoft.EntityFrameworkCore.DbContext
    {
        //public DataContext()
        //{
        //}

        //public DataContext(DbContextOptions<DataContext> options) : base(options)
        //{
        //}

        public DbSet<Security> Securities { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //IConfigurationRoot configuration = new ConfigurationBuilder()
        //    //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //    //    .AddJsonFile("appsettings.json")
        //    //    .Build();
        //    //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        //    optionsBuilder.UseSqlServer(@"Server = localhost; Database = moexdb; Uid = root; Pwd = Aa12345;");
        //}
        private const string connectionString = "Server=localhost;Database=moexdb; Uid = root; Pwd = Aa12345;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
