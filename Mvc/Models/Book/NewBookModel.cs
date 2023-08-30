using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Book
{
    public class NewBookModel
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; } = default!;

        [Required]
        [StringLength(255)]
        public string Author { get; set; } = default!;

        [Required]
        [StringLength(255)]
        public string Publisher { get; set; } = default!;

        [Required]
        [Range(0, 10000)]
        public int NumberOfPages { get; set; }

        [Required]
        [StringLength(255)]
        public string Genre { get; set; } = default!;

        [Required]
        public short Year { get; set; }

        [Required]
        [Range(0, 999999)]
        public decimal ProductionPrice { get; set; }

        [Required]
        [Range(0, 999999)]
        public decimal SellingPrice { get; set; }

        public int? PreviousBookId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
