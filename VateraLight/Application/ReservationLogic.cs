using VateraLight.Application.Interfaces;
using VateraLight.Domain;

namespace VateraLight.Application
{
    public class ReservationLogic : IReservationLogic
    {
        private static object monitorLock = new();
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IStockRepository _stockRepository;

        public ReservationLogic(IReservationsRepository reservationsRepository, IStockRepository stockRepository)
        {
            _reservationsRepository = reservationsRepository;
            _stockRepository = stockRepository;
        }

        public IEnumerable<Reservation> ReserveProducts(User user, int count)
        {
            List<Reservation> reservations = new();
            if (Monitor.TryEnter(monitorLock, 2000))
            {
                try
                {
                    if (ReservationPossible(user, count))
                    {
                        var reservedStock = _stockRepository.ReserveStock(count);
                        foreach (var product in reservedStock)
                        {
                            var reservation = _reservationsRepository.AddReservation(user, product);
                            reservations.Add(reservation);
                        }
                    }
                }
                finally
                {
                    Monitor.Exit(monitorLock);
                }
            }

            return reservations;
        }

        private bool ReservationPossible(User user, int count)
        {
            return _stockRepository.GetStockCount() >= count &&
                        _reservationsRepository.GetReservationCountByUser(user) + count <= 10
                        && count > 0 && count <= 2;
        }
    }
}
