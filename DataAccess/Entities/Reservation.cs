using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullNameOfReservator { get; set; } = string.Empty;
        
        [Required]
        public DateTime ExpirationTime { get; set; }

        [Required]
        public List<ReservationPart> ReservationParts { get; set; } = default!;

        [Required]
        public IdentityUser User { get; set; } = default!;

        [Required]
        public string UserId { get; set; } = default!;
    }
}
