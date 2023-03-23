using VateraLight.Application.Interfaces;
using VateraLight.Infrastructure.Repository;

namespace VateraLight.Infrastructure.BackgroundTasks
{
    public class Reserver : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromSeconds(10);
        private readonly IReservationsRepository _reservationsRepository;

        public Reserver()
        {
            _reservationsRepository = new ReservationsRepository();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (!stoppingToken.IsCancellationRequested &&
                   await timer.WaitForNextTickAsync(stoppingToken))
            {
                if (_reservationsRepository.UnprocessedReservationCount() != 0)
                {
                    var processedReservation = _reservationsRepository.RemoveNextReservation();
                    Console.WriteLine(processedReservation.ToString() + " reservation has been processed. " + DateTime.Now);
                }
            }
        }
    }
}
