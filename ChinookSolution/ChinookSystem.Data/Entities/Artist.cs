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
    [Table("Artists")]
    public class Artist
    {
        private string _Name;
        //This is for non identity
        //[Key , DatabaseGenerated(DatabaseGeneratedOption.None)]

        //This is for identity field
        [Key]
        public int ArtistId { get; set; }
        
        //Check if the sql entity has any constraints
        [StringLength(120, ErrorMessage ="Artist name is limited to 120 characters")]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _Name = null;
                }
                else
                {
                    _Name = value;
                }

                //OR like this
                //_Name = string.IsNullOrEmpty(value) ? null : value;
            }
        }
        //[NotMapped] properties

        //navligational properties
        public virtual ICollection<Album> Albums { get; set; }
    }
}
