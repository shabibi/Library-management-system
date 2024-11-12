using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.Models
{
    public class Category
    {
        [Key]
        public int CID { get; set; }

        [Required]
        [MaxLength(50)]
        public string CName { get; set; }

        [Range(1, int.MaxValue)]
        public int NumberOfBooks { get; set; }

        public virtual ICollection<Book> Books { get; set;}
    }
}
