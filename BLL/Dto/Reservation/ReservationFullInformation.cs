using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Reservation
{
    public class ReservationFullInformation
    {
        public int Id { get; set; }

        public string FullNameOfReservator { get; set; } = string.Empty;

        public DateTime ExpirationTime { get; set; }

        public List<ReservationPartDto> ReservationParts { get; set; } = default!;

    }
}
