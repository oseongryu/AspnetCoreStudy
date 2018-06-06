using AspnetCoreStudy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreStudy.DataContext
{
    public class AspnetNoteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //절대경로//
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\cpc\Documents\GitHub\AspnetCoreStudy\infomation.txt");

            string serverHost = lines[0];
            string databaseName = lines[1];
            string userId = lines[2];
            string userPassword = lines[3];

            optionsBuilder.UseSqlServer(@"Server = "+serverHost+"; Database = "+ databaseName + "; User Id = "+ userId + ";Password = "+ userPassword + ";");
        }
    }
}
