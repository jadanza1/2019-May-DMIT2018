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
    public class GenreController
    {
   
            [DataObjectMethod(DataObjectMethodType.Select, false)]
            public List<SelectionList> List_GenreNames()
            {
                using (var context = new ChinookSystemContext())
                {
                    var results = from x in context.Genres
                                  orderby x.Name
                                  select new SelectionList
                                  {
                                      IDValueField = x.GenreId,
                                      DisplayText = x.Name
                                  };
                    return results.ToList();
                }
            }
        

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
