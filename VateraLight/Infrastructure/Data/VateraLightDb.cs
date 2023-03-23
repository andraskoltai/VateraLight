using System.Collections.Concurrent;
using VateraLight.Domain;

namespace VateraLight.Infrastructure.Data
{
    public static class VateraLightDb
    {
        public static ConcurrentQueue<Reservation> Reservations { get; } = new();
        public static ConcurrentBag<Product> Products { get; } = new();
        public static ConcurrentBag<Reservation> ProcessedReservations { get; } = new();
    }
}
