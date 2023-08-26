using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dto.Order
{
    public class OrderPartDto
    {
        public int BookId { get; set; }

        public int Quantity { get; set; }

        public decimal PriceForItem { get; set; }
    }
}
