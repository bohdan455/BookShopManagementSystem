using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public string Author { get; set; } = default!;

        public string Publisher { get; set; } = default!;

        public int NumberOfPages { get; set; }

        public string Genre { get; set; } = default!;

        public short Year { get; set; }

        public decimal ProductionPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int? PreviousBookId { get; set; }

        public string UserId { get; set; } = default!;

        public int Quantity { get; set; }
    }
}
