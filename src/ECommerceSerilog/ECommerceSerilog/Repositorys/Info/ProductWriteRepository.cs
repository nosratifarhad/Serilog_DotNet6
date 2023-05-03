using ECommerceSerilog.Entitys;
using ECommerceSerilog.Repositorys.WriteRepository;

namespace ECommerceSerilog.Repositorys.Info;
public class ProductWriteRepository : IProductWriteRepository
{
    public async Task<int> CreateProductAsync(Product product)
    {
        await Task.Delay(100);
        return 1;
    }

    public async Task DeleteProductAsync(int productId)
    {
        await Task.Delay(100);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await Task.Delay(100);
    }
}