using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Promotion
{
    public class PromotionBriefInformation
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public decimal Percent { get; set; }
    }
}
