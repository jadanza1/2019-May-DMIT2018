using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
using System.ComponentModel;
using ChinookSystem.Data.POCOs;
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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public IEnumerable<AlbumArtists> Album_ListAlbumArtist()
        {
            //When bringing your Query from LINQpad, 
            // you must remember LINQpad is Linq to SQL, 
            // in this application,we use entities.
            // Therefore, we will use Linq to Entities.
            //Setup your usual transaction to your context class.
            //Reference you appropriate context DBSet<>.

            using (var context = new ChinookSystemContext())
            {
                var results = from a in context.Albums
                              orderby a.Title
                              select new AlbumArtists
                              {
                                  Title = a.Title,
                                  Year = a.ReleaseYear,
                                  AritstName = a.Artist.Name
                              };
                return results.ToList();
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
