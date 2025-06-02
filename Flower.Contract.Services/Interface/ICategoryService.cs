using Flower.Core;
using Flower.Core.APIResponse;
using Flower.ModelViews.CategoryModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Contract.Services.Interface
{
    public interface ICategoryService
    {
        Task<ApiResult<object>> CreateCategory(CategoryCreateModelView model);
        Task<ApiResult<List<CategoryModelView>>> GetAllCategories();
        Task<ApiResult<BasePaginatedList<CategoryModelView>>> GetPaging(int pageNumber, int pageSize, string? name, int? id);

        Task<ApiResult<CategoryModelView>> GetCategoryById(int id);
        Task<ApiResult<object>> UpdateCategory(CategoryUpdateModelView model);
        Task<ApiResult<object>> DeleteCategory(int id);
    }
}
