using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Get active categories only
        /// </summary>
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetActiveCategories()
        {
            var categories = await _categoryService.GetActiveCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Get category by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            return Ok(category);
        }

        /// <summary>
        /// Create a new category
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = await _categoryService.CreateCategoryAsync(createCategoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        /// <summary>
        /// Update an existing category
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(Guid id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);
            
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            return Ok(category);
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            
            if (!result)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
