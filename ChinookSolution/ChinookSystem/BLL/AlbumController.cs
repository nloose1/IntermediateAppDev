using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Aditional namespaces
using System.Data.Entity;
using ChinookSystem.Data.Entities;
using ChinookSystem.DAL;
#endregion

namespace ChinookSystem.BLL
{
    public class AlbumController
    {
        //basic query: complete list of dbSet
        public List<Album> Album_List()
        {
            //setup the code block to ensure the release of the sql connection
            using (var context = new ChinookContext())
            {
                //.ToList<T> is used to convert the DbSet<T> into a List<T> collection
                return context.Albums.ToList();
            }
        }
        //basic query: return a record based on pkey
        public Album Album_FindByID(int albumid)
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.Find(albumid);
            }
        }
    }
}
