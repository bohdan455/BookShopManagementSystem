using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class PromotionDto
    {
        public string Name { get; set; } = default!;

        public List<int> IncludedBooks { get; set; } = default!;

        public decimal AmountInPercent { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}
