using Flower.Contract.Services.Interface;
using Flower.ModelViews.CategoryModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowerBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        ///     Create a new category
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<string>> Create([FromBody] CategoryCreateModelView model)
        {
            try
            {
                var result = await _categoryService.CreateCategory(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Get all categories
        /// </summary>
        [HttpGet("get-all")]
        public async Task<ActionResult<string>> GetAll()
        {
            try
            {
                var result = await _categoryService.GetAllCategories();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }
        [HttpGet("paging")]
        public async Task<ActionResult<string>> GetPaging([FromQuery]int pageNumber, [FromQuery] int pageSize, [FromQuery] string? name, [FromQuery] int? id)
        {
            try
            {
                var result = await _categoryService.GetPaging(pageNumber,pageSize,name,id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }


        /// <summary>
        ///     Get a category by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetById(int id)
        {
            try
            {
                var result = await _categoryService.GetCategoryById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Update a category
        /// </summary>
        [HttpPut("update")]
        public async Task<ActionResult<string>> Update([FromBody] CategoryUpdateModelView model)
        {
            try
            {
                var result = await _categoryService.UpdateCategory(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Delete a category
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategory(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
