using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.Models
{
    public class User
    {

        [Key]
        public int UID { get; set; }

        [Required]
        [MaxLength(50)]
        public string UName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Passcode { get; set; }

        public enum Gender { male, female }
        public Gender gender { get; set; }
    }
}
