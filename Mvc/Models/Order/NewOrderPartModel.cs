using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Order
{
    public class NewOrderPartModel
    {
        [Range(0, int.MaxValue)]
        public int BookId { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
