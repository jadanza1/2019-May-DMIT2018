using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion


// Entity Validation kicks in on the .SaveChanges() command.

namespace ChinookClassData.Entities
{
    [Table("Albums")]
    public class Album
    {
        private string _ReleaseLabel;

        [Key]
        public int AlbumId { get; set; }
        [Required(ErrorMessage ="Title Is Required")]
        [StringLength(160,ErrorMessage ="Title is Limited To 160 characters")]
        public string Title { get; set; }
        public int ArtistId { get; set; }
        [Range(1950,2019,ErrorMessage ="Release Year Must be Between 1950 and Today")] // will be fixed. should be 1950 - Datetime.Today.Now
        public int ReleaseYear { get; set; }
        [StringLength(50, ErrorMessage = "Release Label Limited to 50 characters")]
        public string ReleaseLabel
        {
            get
            {
                return _ReleaseLabel;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _ReleaseLabel = null;
                }
                else
                {
                    _ReleaseLabel = value;
                }
            }
        }
    }
}
