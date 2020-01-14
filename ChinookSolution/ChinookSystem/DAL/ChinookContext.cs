using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Aditional namespaces
using System.Data.Entity;
using ChinookSystem.Data.Entities;
#endregion

namespace ChinookSystem.DAL
{
    //restrict access to the database interface to within the project
    //inherit the Entity framework interface DbContext
    internal class ChinookContext:DbContext
    {
        //constructor will pass the connection string name to the database to EntityFramework via DbContext
        public ChinookContext() : base("ChinookDB")
        {
        }

        //create a DbSet<T> for each Enity
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
    }
}
