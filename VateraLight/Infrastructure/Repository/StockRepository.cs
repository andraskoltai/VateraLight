using VateraLight.Application.Interfaces;
using VateraLight.Domain;
using VateraLight.Infrastructure.Data;

namespace VateraLight.Infrastructure.Repository
{
    public class StockRepository : IStockRepository
    {
        public void AddStock(int count)
        {
            for (int i = 0; i < count; i++)
            {
                VateraLightDb.Products.Add(new Product());
            }
        }

        public int GetStockCount()
        {
            return VateraLightDb.Products.Count();
        }

        public IEnumerable<Product> ReserveStock(int count)
        {
            List<Product> reservedProducts = new List<Product>();
            while (reservedProducts.Count < count)
            {
                Product p;
                while (!VateraLightDb.Products.TryTake(out p)) ;
                reservedProducts.Add(p);
                
            }

            return reservedProducts;
        }
    }
}
