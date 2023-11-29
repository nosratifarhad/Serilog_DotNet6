using ECommerceSerilog.Domain;
using ECommerceSerilog.Domain.Entitys;

namespace ECommerceSerilog.Infra.Repositories.ReadRepositories.ProductReadRepositories;

public class ProductReadRepository : IProductReadRepository
{
    public async Task<Product> GetProduct(int productId)
    {
        await Task.Delay(100);

        return new Product(string.Empty, string.Empty, null, null, null, null, false, false, null);
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        await Task.Delay(100);

        return new List<Product>()
        {
            new Product(string.Empty, string.Empty, null, null, null, null, false, false, null),
            new Product(string.Empty, string.Empty, null, null, null, null, false, false, null),
            new Product(string.Empty, string.Empty, null, null, null, null, false, false, null)
        };
    }

    public async Task<bool> IsExistProduct(int productId)
    {
        await Task.Delay(100);

        return true;
    }
}
