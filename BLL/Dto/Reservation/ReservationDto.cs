using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Reservation
{
    public class ReservationDto
    {
        public string ReservatorFullName { get; set; } = default!;

        public DateTime ExpirationTime { get; set; }

        public string UserId { get; set; } = default!;

        public List<ReservationPartDto> ReservationParts { get; set; } = default!;
    }
}
