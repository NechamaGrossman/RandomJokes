﻿using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ReactJokesHw.Data
{
    public class JokesContextFactory : IDesignTimeDbContextFactory<JokesContext>
    {
        public JokesContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}ReactJokesHw.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new JokesContext(config.GetConnectionString("ConStr"));
        }
    }
}

