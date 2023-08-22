using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class BookFullInformation
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

        public BookBriefInformation? PreviousBook { get; set; }

        public int Quantity { get; set; }
    }
}
