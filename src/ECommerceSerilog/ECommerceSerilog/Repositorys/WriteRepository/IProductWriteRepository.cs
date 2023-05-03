using ECommerceSerilog.Entitys;

namespace ECommerceSerilog.Repositorys.WriteRepository;

public interface IProductWriteRepository
{
    Task<int> CreateProductAsync(Product product);

    Task UpdateProductAsync(Product product);

    Task DeleteProductAsync(int productId);

}
