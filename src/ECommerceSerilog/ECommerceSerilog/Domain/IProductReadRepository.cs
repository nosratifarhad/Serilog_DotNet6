using ECommerceSerilog.Domain.Entitys;

namespace ECommerceSerilog.Domain;

public interface IProductReadRepository
{
    Task<Product> GetProduct(int productId);

    Task<IEnumerable<Product>> GetProducts();

    Task<bool> IsExistProduct(int productId);

}
