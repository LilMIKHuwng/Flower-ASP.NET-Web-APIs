using Flower.Core.APIResponse;
using Flower.ModelViews.StoreModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Contract.Services.Interface
{
    public interface IStoreService
    {
        Task<ApiResult<StoreModelView>> GetStoreByIdAsync(int id);
    }
}
