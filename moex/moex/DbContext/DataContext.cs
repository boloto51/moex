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


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //IConfigurationRoot configuration = new ConfigurationBuilder()
        //    //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //    //    .AddJsonFile("appsettings.json")
        //    //    .Build();
        //    //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        //    optionsBuilder.UseSqlServer(@"Server = localhost; Database = moexdb; Uid = root; Pwd = Aa12345;");
        //}
        //private const string connectionString = "user id=root;password=Aa12345;data source=localhost;port=3306;initial catalog=moexdb;charset=utf8;";

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(connectionString);
        //}
        private string connectionString;

        public DataContext() : base()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            connectionString = configuration.GetConnectionString("DefaultConnection").ToString();

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString);
        }

        public DbSet<Security> Securities { get; set; }

    }
}
