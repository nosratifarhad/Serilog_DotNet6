using ECommerceSerilog.Services.Contract;
using ECommerceSerilog.InputModels.ProductInputModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Fields
        private readonly ILogger<ProductsController> _logger;

        private readonly IProductService _productService;
        #endregion Fields

        #region Ctor
        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            this._productService = productService;
            _logger = logger;
        }

        #endregion Ctor

        /// <summary>
        /// Get Product List
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/products")]
        public async Task<IActionResult> GetProducts()
        {
            _logger.LogInformation(nameof(GetProducts));
            var productVMs = await _productService.GetProducts().ConfigureAwait(false);
            _logger.LogInformation("GetProducts successfully");
            return Ok(productVMs);
        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("/api/product")]
        public async Task<IActionResult> CreateProduct(CreateProductInputModel inputModel)
        {
            _logger.LogInformation(nameof(CreateProduct));
            int productId = await _productService.CreateProductAsync(inputModel).ConfigureAwait(false);
            _logger.LogInformation("CreateProduct successfully");
            return CreatedAtRoute(nameof(GetProduct), new { productId = productId }, new { ProductId = productId });
        }

        /// <summary>
        /// Get Product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("/api/product/{productId:int}", Name = nameof(GetProduct))]
        public async Task<IActionResult> GetProduct(int productId)
        {
            _logger.LogInformation(nameof(CreateProduct));
            var product = await _productService.GetProduct(productId).ConfigureAwait(false);
            _logger.LogInformation("GetProduct successfully");
            return Ok(product);
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("/api/product/{productId:int}")]
        public async Task<IActionResult> UpdateProduct(int productId, UpdateProductInputModel inputModel)
        {
            if (productId.ToString() != inputModel.ProductId)
            {
                _logger.LogInformation(nameof(BadRequest));
                return BadRequest();
            }
            _logger.LogInformation(nameof(UpdateProduct));
            await _productService.UpdateProductAsync(inputModel).ConfigureAwait(false);
            _logger.LogInformation("UpdateProduct successfully");
            return NoContent();
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("/api/product/{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            _logger.LogInformation(nameof(DeleteProduct));
            await _productService.DeleteProductAsync(productId).ConfigureAwait(false);
            _logger.LogInformation("DeleteProduct successfully");
            return NoContent();
        }

    }
}
