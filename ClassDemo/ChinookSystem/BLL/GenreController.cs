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
    public class GenreController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Genre> Genre_List()
        {
            using (var context = new ChinookSystemContext())
            {
                return context.Genres.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Genre Genre_Get(int albumId)
        {
            using (var context = new ChinookSystemContext())
            {
                return context.Genres.Find(albumId);
            }
        }
    }
}
