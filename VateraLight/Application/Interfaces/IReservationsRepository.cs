using VateraLight.Domain;

namespace VateraLight.Application.Interfaces
{
    public interface IReservationsRepository
    {
        int UnprocessedReservationCount();
        int GetReservationCountByUser(User user);
        Reservation AddReservation(User user, Product product);
        Reservation RemoveNextReservation();
    }
}
