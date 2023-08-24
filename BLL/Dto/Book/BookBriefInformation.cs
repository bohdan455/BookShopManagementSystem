using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Book
{
    public class BookBriefInformation
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public string Author { get; set; } = default!;

        public string Genre { get; set; } = default!;

        public short Year { get; set; }

        public decimal SellingPrice { get; set; }
    }
}
