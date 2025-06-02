using Flower.Core;
using Flower.Core.APIResponse;
using Flower.ModelViews.CategoryModelViews;
using Flower.ModelViews.FlowerTypeModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Contract.Services.Interface
{
    public interface IFlowerTypeService
    {
        Task<ApiResult<object>> CreateFlowerType(FlowerTypeCreateModelView model);
        Task<ApiResult<BasePaginatedList<FlowerTypeModelView>>> GetPaging(int pageNumber, int pageSize, string? name, int? id);

        Task<ApiResult<List<FlowerTypeModelView>>> GetAllFlowerTypes();
        Task<ApiResult<FlowerTypeModelView>> GetFlowerTypeById(int id);
        Task<ApiResult<object>> UpdateFlowerType(FlowerTypeUpdateModelView model);
        Task<ApiResult<object>> DeleteFlowerType(int id);
    }
}
