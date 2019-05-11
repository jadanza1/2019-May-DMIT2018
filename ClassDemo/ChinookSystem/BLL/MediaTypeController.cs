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
    public class MediaTypeController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MediaType> MediaType_List()
        {
            using (var context = new ChinookSystemContext())
            {
                return context.MediaTypes.ToList();
            }
        }

        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public Album MediaType_Get(int mediaTypeId)
        //{
        //    using (var context = new ChinookSystemContext())
        //    {
        //        return context.MediaTypes.Find(mediaTypeId);
        //    }
        //}
    }
}
