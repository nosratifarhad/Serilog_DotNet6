using ECommerceSerilog.Services.Contract;
using ECommerceSerilog.InputModels.ProductInputModels;
using ECommerceSerilog.ViewModels.ProductViewModels;
using ECommerceSerilog.Domain.Entitys;
using ECommerceSerilog.Domain;

namespace ECommerceSerilog.Services;

public class ProductService : IProductService
{
    #region Fields

    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    #endregion Fields

    #region Ctor

    public ProductService(
        IProductReadRepository productReadRepository, 
        IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    #endregion Ctor

    #region Implement

    public async Task<ProductModel> GetProduct(int productId)
    {

        var product = await _productReadRepository.GetProduct(productId).ConfigureAwait(false);

        var productViewModel = CreateProductViewModelFromProduct(product);

        return productViewModel;
    }

    public async Task<IEnumerable<ProductModel>> GetProducts()
    {
        var products = await _productReadRepository.GetProducts().ConfigureAwait(false);
        if (products == null || products.Count() == 0)
            return Enumerable.Empty<ProductModel>();

        var productViewModels = CreateProductViewModelsFromProducts(products);

        return productViewModels;
    }

    public async Task<int> CreateProductAsync(CreateProductInputModel inputModel)
    {
        ValidateProductName(inputModel.ProductName);

        ValidateProductTitle(inputModel.ProductTitle);

        var product= CreateProductEntityFromInputModel(inputModel);

        return await _productWriteRepository.CreateProductAsync(product).ConfigureAwait(false);
    }

    public async Task UpdateProductAsync(UpdateProductInputModel inputModel)
    {
        ValidateProductName(inputModel.ProductName);

        ValidateProductTitle(inputModel.ProductTitle);

        await IsExistProduct(int.Parse(inputModel.ProductId)).ConfigureAwait(false);

        var product = CreateProductEntityFromInputModel(inputModel);

        await _productWriteRepository.UpdateProductAsync(product).ConfigureAwait(false);
    }

    public async Task DeleteProductAsync(int productId)
    {
        await _productWriteRepository.DeleteProductAsync(productId).ConfigureAwait(false);
    }

    #endregion Implement

    #region Private

    private async Task IsExistProduct(int productId)
    {
        var isExistProduct = await _productReadRepository.IsExistProduct(productId).ConfigureAwait(false);
        if (isExistProduct == false)
            throw new Exception("productId Is Not Found.");
    }

    private Product CreateProductEntityFromInputModel(CreateProductInputModel inputModel)
        => new Product(inputModel.ProductName, inputModel.ProductTitle, inputModel.ProductDescription, inputModel.MainImageName, inputModel.MainImageTitle, inputModel.MainImageUri, inputModel.IsExisting, inputModel.IsFreeDelivery, inputModel.Weight);

    private Product CreateProductEntityFromInputModel(UpdateProductInputModel inputModel)
        => new Product(int.Parse(inputModel.ProductId), inputModel.ProductName, inputModel.ProductTitle, inputModel.ProductDescription, inputModel.MainImageName, inputModel.MainImageTitle, inputModel.MainImageUri, inputModel.IsExisting, inputModel.IsFreeDelivery, inputModel.Weight);

    private ProductModel CreateProductViewModelFromProduct(Product product)
        => new ProductModel()
        {
            ProductId = product.ProductId.ToString(),
            ProductName = product.ProductName,
            ProductTitle = product.ProductTitle,
            ProductDescription = product.ProductDescription,
            MainImageName = product.MainImageName,
            MainImageTitle = product.MainImageTitle,
            MainImageUri = product.MainImageUri,
            IsExisting = product.IsExisting,
            IsFreeDelivery = product.IsFreeDelivery,
            Weight = product.Weight
        };

    private IEnumerable<ProductModel> CreateProductViewModelsFromProducts(IEnumerable<Product> products)
    {
        ICollection<ProductModel> productViewModels = new List<ProductModel>();

        foreach (var Product in products)
            productViewModels.Add(
                 new ProductModel()
                 {

                     ProductId = Product.ProductId.ToString(),
                     ProductName = Product.ProductName,
                     ProductTitle = Product.ProductTitle,
                     ProductDescription = Product.ProductDescription,
                     MainImageName = Product.MainImageName,
                     MainImageTitle = Product.MainImageTitle,
                     MainImageUri = Product.MainImageUri,
                     IsExisting = Product.IsExisting,
                     IsFreeDelivery = Product.IsFreeDelivery,
                     Weight = Product.Weight
                 });

        return productViewModels;
    }

    private void ValidateProductName(string productName)
    {
        if (string.IsNullOrEmpty(productName) || string.IsNullOrWhiteSpace(productName))
            throw new ArgumentNullException(nameof(productName), "Product Name must not be empty");
    }

    private void ValidateProductTitle(string productTitle)
    {
        if (string.IsNullOrEmpty(productTitle) || string.IsNullOrWhiteSpace(productTitle))
            throw new ArgumentNullException(nameof(productTitle), "Product Title must not be empty");
    }

    #endregion Private
}
