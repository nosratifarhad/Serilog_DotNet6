
using ECommerceSerilog.InputModels.ProductInputModels;
using ECommerceSerilog.ViewModels.ProductViewModels;

namespace ECommerceSerilog.Services.Contract;

public interface IProductService
{
    Task<int> CreateProductAsync(CreateProductInputModel inputModel);

    Task UpdateProductAsync(UpdateProductInputModel inputModel);

    Task DeleteProductAsync(int productId);

    Task<ProductModel> GetProduct(int productId);

    Task<IEnumerable<ProductModel>> GetProducts();

}
