using VateraLight.Application.Interfaces;
using VateraLight.Domain;
using VateraLight.Infrastructure.Data;
using VateraLight.Infrastructure.Repository;

namespace VateraLight.Application
{
    public class Producer : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromSeconds(10);
        private readonly IStockRepository _stockRepository;

        public Producer()
        {
            _stockRepository = new StockRepository();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);

            while (!stoppingToken.IsCancellationRequested &&
                   await timer.WaitForNextTickAsync(stoppingToken))
            {
                if (_stockRepository.GetStockCount() != 100)
                {
                    Random rnd = new Random();
                    int randomIncrease = rnd.Next(1, 11);
                    int newProductsCount = randomIncrease + _stockRepository.GetStockCount() <= 100 ?
                        randomIncrease : 100 - _stockRepository.GetStockCount();
                    _stockRepository.AddStock(newProductsCount);
                    Console.WriteLine(newProductsCount.ToString() + " new product have been added to the stock. Total stock: " +
                        _stockRepository.GetStockCount() + " product(s) " + DateTime.Now);
                }
            }
        }
    }
}
