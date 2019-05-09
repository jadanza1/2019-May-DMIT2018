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
    [Table("Genres")]
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        public string Name { get; set; }

    }
}
