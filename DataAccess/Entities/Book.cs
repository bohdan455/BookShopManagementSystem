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
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = default!;

        [Required]
        public Author Author { get; set; } = default!;

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public Publisher Publisher { get; set; } = default!;

        [Required]
        public int PublisherId { get; set; }

        [Required]
        public int NumberOfPages { get; set; }

        [Required]
        public Genre Genre { get; set; } = default!;

        [Required]
        public int GenreId { get; set; }

        [Required]
        public short Year { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal ProductionPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal SellingPrice { get; set; }

        public Book? PreviousBook { get; set; }

        public int? PreviousBookId { get; set; }

        [Required]
        public IdentityUser User { get; set; } = default!;

        [Required]
        public string UserId { get; set; } = default!;

        [Required]
        public Discount? Discount { get; set; }

        [Required]
        public int? DiscountId { get; set; }

        [Required]
        public DateTime DateOfAdding { get; set; }

        [Required]
        public int Quantity { get; set; }

        public List<OrderPart> OrderParts { get; set; } = default!;
    }
}
