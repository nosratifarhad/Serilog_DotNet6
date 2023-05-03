using ECommerceSerilog.Dtos;

namespace ECommerceSerilog.Repositorys.ReadRepository;

public interface IProductReadRepository
{
    Task<ProductDto> GetProduct(int productId);

    Task<IEnumerable<ProductDto>> GetProducts();

    Task<bool> IsExistProduct(int productId);

}
