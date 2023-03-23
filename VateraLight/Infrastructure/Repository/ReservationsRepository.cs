using VateraLight.Application.Interfaces;
using VateraLight.Domain;
using VateraLight.Infrastructure.Data;

namespace VateraLight.Infrastructure.Repository
{
    public class ReservationsRepository : IReservationsRepository
    {
        public int GetReservationCountByUser(User user)
        {
            return VateraLightDb.Reservations.Where(r => r.User == user).Count();
        }

        public Reservation AddReservation(User user, Product product)
        {
            Reservation reservation = new Reservation() { User = user, Product = product, Guid = Guid.NewGuid() };
            VateraLightDb.Reservations.Enqueue(reservation);
            return reservation;
        }

        public Reservation RemoveNextReservation()
        {
            Reservation reservation;
            while (!VateraLightDb.Reservations.TryDequeue(out reservation)) ;
            VateraLightDb.ProcessedReservations.Add(reservation);
            return reservation;
        }

        public int UnprocessedReservationCount()
        {
            return VateraLightDb.Reservations.Count;
        }
    }
}
