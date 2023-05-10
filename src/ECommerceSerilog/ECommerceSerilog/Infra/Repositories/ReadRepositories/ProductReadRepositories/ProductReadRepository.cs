using ECommerceSerilog.Domain;
using ECommerceSerilog.Domain.Entitys;

namespace ECommerceSerilog.Infra.Repositories.ReadRepositories.ProductReadRepositories;

public class ProductReadRepository : IProductReadRepository
{
    public async Task<Product> GetProduct(int productId)
    {
        await Task.Delay(100);
        return null;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        await Task.Delay(100);
        return null;
    }

    public async Task<bool> IsExistProduct(int productId)
    {
        await Task.Delay(100);
        return true;
    }
}
