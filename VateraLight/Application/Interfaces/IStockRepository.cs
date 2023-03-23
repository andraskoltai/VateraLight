using VateraLight.Domain;

namespace VateraLight.Application.Interfaces
{
    public interface IStockRepository
    {
        int GetStockCount();
        IEnumerable<Product> ReserveStock(int count);
        void AddStock(int count);
    }
}
