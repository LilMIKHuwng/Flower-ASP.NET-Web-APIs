using Flower.Contract.Services.Interface;
using Flower.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlowerBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _storeService.GetStoreByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }

        }
    }
}
