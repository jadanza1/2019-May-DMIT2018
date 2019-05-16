using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
using System.ComponentModel;
#endregion
namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController
    {
        #region Queries
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Album_List()
        {
            using (var context = new ChinookSystemContext())
            {
                return context.Albums.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Album Album_Get(int albumId)
        {
            using (var context = new ChinookSystemContext())
            {
                return context.Albums.Find(albumId);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Album_GetByArtist(int artistId)
        {
            using (var context = new ChinookSystemContext())
            {
                var resutls = from x in context.Albums
                              where x.ArtistId == artistId
                              select x;
                return resutls.ToList();
            }
        }
        #endregion

        #region Add,Update,Delete
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Album_Add(Album item)
        {
            using (var context = new ChinookSystemContext())
            {
                context.Albums.Add(item);   //stage add action 
                context.SaveChanges();      //Entity Validation is Executed.
                return item.AlbumId;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public int Album_Update(Album item)
        {
            using (var context = new ChinookSystemContext())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();   //returned is number of rows affected
            }
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int Album_Delete(Album item)
        {
            
            return Album_Delete(item.AlbumId);
        }

        public int Album_Delete(int albumId)
        {
            using (var context = new ChinookSystemContext())
            {
                var existing = context.Albums.Find(albumId);
                //OR  Album existing = context.Albums.Find(albumId);
                context.Albums.Remove(existing);    //stage delete action
                return context.SaveChanges();       
            }
        }
        #endregion
    }
}
