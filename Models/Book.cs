using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.Models
{
    public class Book
    {
        [Key]
        public int BID { get; set; }

        [Required]
        [MaxLength(50)]
        public string BTitle { get; set; }

        [Required]
        [MaxLength(50)]
        public string Author { get; set; }

        [Range (1,int.MaxValue)]
        public double Price { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalCopies { get; set; }

        [Range(0,int.MaxValue)]
        public int BorrowedCopies { get; set; }

        [Range(1, int.MaxValue)]
        public int BorrowingPeriod { get; set; }

        [ForeignKey ("Category")]
        public int CID { get; set; }

        public Category Category { get; set; }

        public virtual ICollection<Borrow> Borrows { get; set; }


    
    }
}
