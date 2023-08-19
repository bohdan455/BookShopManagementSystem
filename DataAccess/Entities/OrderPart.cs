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
    public class OrderPart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Book Book { get; set; } = default!;

        [Required]
        public int BookId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public OrderDetails Order { get; set; } = default!;

        [Required]
        public int OrderId { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal TotalPrice { get; set; }
    }
}
