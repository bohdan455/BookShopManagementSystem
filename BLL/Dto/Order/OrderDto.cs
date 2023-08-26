namespace BLL.Dto.Order
{
    public class OrderDto
    {
        public string UserId { get; set; } = default!;

        public List<OrderPartDto> OrderParts { get; set; } = default!;
    }
}
