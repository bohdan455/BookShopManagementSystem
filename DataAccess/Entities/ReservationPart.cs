using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class ReservationPart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Reservation Reservation { get; set; } = default!;

        [Required]
        public int ReservationId { get; set; }

        [Required]
        public Book Book { get; set; } = default!;

        [Required]
        public int BookId { get; set; }

        [Required]  
        public int Quantity { get; set; }
    }
}
