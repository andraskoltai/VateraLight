using System.Collections.Generic;
using VateraLight.Domain;

namespace VateraLight.Application.Interfaces
{
    public interface IReservationLogic
    {
        IEnumerable<Reservation> ReserveProducts(User user, int count);
    }
}
