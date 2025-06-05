using Flower.Contract.Services.Interface;
using Flower.ModelViews.FlowerTypeModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowerBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowerTypesController : ControllerBase
    {
        private readonly IFlowerTypeService _flowerTypeService;

        public FlowerTypesController(IFlowerTypeService flowerTypeService)
        {
            _flowerTypeService = flowerTypeService;
        }

        /// <summary>
        ///     Create a new flower type
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<string>> Create([FromForm] FlowerTypeCreateModelView model)
        {
            try
            {
                var result = await _flowerTypeService.CreateFlowerType(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Get all flower types
        /// </summary>
        [HttpGet("get-all")]
        public async Task<ActionResult<string>> GetAll()
        {
            try
            {
                var result = await _flowerTypeService.GetAllFlowerTypes();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Get a flower type by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetById(int id)
        {
            try
            {
                var result = await _flowerTypeService.GetFlowerTypeById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }
        [HttpGet("paging")]
        public async Task<ActionResult<string>> GetPaging([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? name, [FromQuery] int? id, [FromQuery]int? categoryId, [FromQuery] string? categoryName)
        {
            try
            {
                var result = await _flowerTypeService.GetPaging(pageNumber, pageSize, name, id,categoryId,categoryName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Update a flower type
        /// </summary>
        [HttpPut("update")]
        public async Task<ActionResult<string>> Update([FromForm] FlowerTypeUpdateModelView model)
        {
            try
            {
                var result = await _flowerTypeService.UpdateFlowerType(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Delete a flower type
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var result = await _flowerTypeService.DeleteFlowerType(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
