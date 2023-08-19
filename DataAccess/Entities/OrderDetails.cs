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
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateOfPurchase { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        public IdentityUser User { get; set; } = default!;

        [Required]
        public string UserId { get; set; } = default!;

        public List<OrderPart> OrderParts { get; set; } = default!;
    }
}
