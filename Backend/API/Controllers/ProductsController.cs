using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Get active products only
        /// </summary>
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetActiveProducts()
        {
            var products = await _productService.GetActiveProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Get featured products
        /// </summary>
        [HttpGet("featured")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetFeaturedProducts()
        {
            var products = await _productService.GetFeaturedProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Get products by category
        /// </summary>
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(Guid categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        /// <summary>
        /// Search products
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> SearchProducts([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var products = await _productService.SearchProductsAsync(searchTerm);
            return Ok(products);
        }

        /// <summary>
        /// Get product by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }

        /// <summary>
        /// Get product with category details
        /// </summary>
        [HttpGet("{id}/with-category")]
        public async Task<ActionResult<ProductDto>> GetProductWithCategory(Guid id)
        {
            var product = await _productService.GetProductWithCategoryAsync(id);
            
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createProductDto)
        {
            var product = await _productService.CreateProductAsync(createProductDto);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        /// <summary>
        /// Update an existing product
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(Guid id, UpdateProductDto updateProductDto)
        {
            var product = await _productService.UpdateProductAsync(id, updateProductDto);
            
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _productService.DeleteProductAsync(id);
            
            if (!result)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
