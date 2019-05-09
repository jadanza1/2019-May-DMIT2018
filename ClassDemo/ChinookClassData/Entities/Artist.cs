using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
#endregion

namespace ChinookSystem.Data.Entities
{
    //default class permisison is private
    //annotation to link this class to the sql table
    [Table("Artists")]
    public class Artist
    {
        //[Key DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Key, Column(Order = )]

        private string _Name;

        [Key]
        public int ArtistId { get; set; }
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    _Name = null;
                }
                else
                {
                    _Name = value;
                }
            }
        }



    }
}
