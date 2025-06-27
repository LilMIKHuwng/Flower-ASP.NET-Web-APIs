using AutoMapper;
using Flower.Contract.Repositories.Entity;
using Flower.Contract.Repositories.Interface;
using Flower.Contract.Services.Interface;
using Flower.Core.APIResponse;
using Flower.ModelViews.StoreModelViews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Services.Service
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResult<StoreModelView>> GetStoreByIdAsync(int id)
        {
            var storeRepo = _unitOfWork.GetRepository<Store>();
            var store = await storeRepo.Entities.FirstOrDefaultAsync(s => s.Id == id);

            if (store == null)
                return new ApiErrorResult<StoreModelView>("Không tìm thấy cửa hàng.");

            var storeVm = _mapper.Map<StoreModelView>(store);
            return new ApiSuccessResult<StoreModelView>(storeVm);
        }
    }
}
