using ECommerceSerilog.Domain.Entitys;

namespace ECommerceSerilog.Domain;

public interface IProductWriteRepository
{
    Task<int> CreateProductAsync(Product product);

    Task UpdateProductAsync(Product product);

    Task DeleteProductAsync(int productId);

}
