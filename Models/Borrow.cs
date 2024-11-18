using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.Models
{
    [PrimaryKey(nameof(BID), nameof(UID))]
    public class Borrow
    {
        [Required] 
        public DateTime BDate { get; set; }

        public DateTime RDate { get; set; }
        
        public DateTime? ActualDate { get; set; }

        public bool IsReturned { get; set; }
        [Range(0,5)]
        public float? Rating { get; set; }

        [ForeignKey("Book")]
        public int BID;
        public Book Book { get; set; }

        [ForeignKey ("User")]
        public int UID { get; set; }

        public User User { get; set; }
        
    }
}
