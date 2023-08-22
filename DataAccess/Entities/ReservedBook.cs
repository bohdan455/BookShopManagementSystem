using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class ReservedBook
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullNameOfReservator { get; set; } = string.Empty;

        [Required]
        public Book Book { get; set; } = default!;

        [Required]
        public int BookId { get; set; }

        [Required]
        public DateTime ExpirationTime { get; set; }
    }
}
