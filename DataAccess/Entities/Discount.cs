using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = default!;

        [Required]
        [Column(TypeName ="decimal(4,2)")] 
        public decimal AmountInPercent { get; set; }

        [Required]
        [MaxLength(255)]
        public IdentityUser User { get; set; } = default!;

        [Required]
        public string UserId { get; set; } = default!;

        [Required]
        public DateTime ExpirationTime { get; set; }

        public List<Book> Books { get; set; } = default!;
    }
}
