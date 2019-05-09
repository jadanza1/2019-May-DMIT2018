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
    [Table("Tracks")]
    public class Track
    {
        private string _Composer;
        [Key]
        public int TrackId { get; set; }
        [StringLength(200,ErrorMessage = "Track Name must be less than 200 characters.")]
        public string Name { get; set; }

        public int? AlbumId { get; set; }

        public int MediaTypeId { get; set; }

        public int? GenreId { get; set; }
        [StringLength(220, ErrorMessage = "Composer Name must be less than 220 chacters.")]
        public string Composer
        {
            get
            {
                return _Composer;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _Composer = null;
                }
                else
                {
                    _Composer = value;
                }
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Track length must be more than 1 millesecond")]
        public int Milliseconds { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Must be at least 1 byte.")]
        public int? Bytes { get; set; }

        //[Range(1, decimal.MaxValue, ErrorMessage = "Must be at least 1 byte.")]
        public decimal UnitPrice { get; set; }

        //navigational properties
        public virtual Album Album { get; set; }
        public virtual MediaType MediaType { get; set; }
        public virtual Genre Genre { get; set; }

    }
}
