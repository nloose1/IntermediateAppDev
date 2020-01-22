using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Aditional namespaces
using System.Data.Entity;
using ChinookSystem.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel; //ODS
using DMIT2018Common.UserControls;
#endregion

namespace ChinookSystem.BLL
    //LinQ query sintax
        //From X in entity dataset
        //Where conditions
        //Select X {;} or Select new className{ propertyname = X.propery,
        //                                      propertyname = X.parentnavproperty.property}
{
    [DataObject]
    public class AlbumController
    {
        private List<string> reasons = new List<string>();
        #region Queries
        [DataObjectMethod(DataObjectMethodType.Select,false)]
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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        //basic query: return a record based on pkey
        public Album Album_FindByID(int albumid)
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.Find(albumid);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Album_FindByArtist(int artistid)
        {
            using(var context = new ChinookContext())
            {
                var results = from albumrow in context.Albums
                              where albumrow.ArtistId == artistid
                              select albumrow;
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Album_FindByTitle(string title)
        {
            using (var context = new ChinookContext())
            {
                var results = from albumrow in context.Albums
                              where albumrow.Title.Contains(title)
                              select albumrow;
                return results.ToList();
            }
        }
        #endregion

        #region Add, Update, Delete
        [DataObjectMethod(DataObjectMethodType.Insert,false)]
        public int Album_Add(Album item)
        {
            using (var context = new ChinookContext())
            {
                if (CheckReleaseYear(item))
                {
                    //any additional logic
                    item.ReleaseLabel = string.IsNullOrEmpty(item.ReleaseLabel) ? null : item.ReleaseLabel;
                    context.Albums.Add(item); //staging
                    context.SaveChanges(); //Commit to database
                    return item.AlbumId; //return the new identity value of the pkey
                }
                else
                {
                    throw new BusinessRuleException("Validation Error", reasons);
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public int Album_Update(Album item)
        {
            using (var context = new ChinookContext())
            {
                if (CheckReleaseYear(item))
                {
                    //any additional logic
                    item.ReleaseLabel = string.IsNullOrEmpty(item.ReleaseLabel) ? null : item.ReleaseLabel;

                    context.Entry(item).State = System.Data.Entity.EntityState.Modified; //staging
                    return context.SaveChanges(); //Commit to database


                }
                else
                {
                    throw new BusinessRuleException("Validation Error", reasons);
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete,false)]
        public int Album_Delete(Album item)
        {
            return Album_Delete(item.AlbumId);
        }
        public int Album_Delete(int albumid)
        {
            using (var context = new ChinookContext())
            {
                //physical delete
                var existing = context.Albums.Find(albumid);
                context.Albums.Remove(existing);
                return context.SaveChanges();
            }
        }
        #endregion

        #region support Methods
        private bool CheckReleaseYear(Album item)
        {
            bool isValid = true;
            int releaseyear;
            if(string.IsNullOrEmpty(item.ReleaseYear.ToString()))
            {

                isValid = false;
                reasons.Add("Release year is required");
            }
            else if(!int.TryParse(item.ReleaseYear.ToString(), out releaseyear))
            {
                isValid = false;
                reasons.Add("Release year is not a numeric year(YYYY)");
            }
            else if (releaseyear <1950 || releaseyear > DateTime.Today.Year)
            {
                isValid = false;
                reasons.Add(string.Format("Release year of {0} invalid. Year must be between 1950 and today", releaseyear));
            }
            return isValid;
        }
        #endregion
    }
}
