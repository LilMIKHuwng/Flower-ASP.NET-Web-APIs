using AutoMapper;
using Azure.Core;
using Flower.Contract.Repositories.Entity;
using Flower.Contract.Repositories.Interface;
using Flower.Contract.Services.Interface;
using Flower.Core;
using Flower.Core.APIResponse;
using Flower.ModelViews.CategoryModelViews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Services.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ApiResult<object>> CreateCategory(CategoryCreateModelView model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.CategoryName))
                    return new ApiErrorResult<object>("Category name is required.");

                var category = new Category
                {
                    CategoryName = model.CategoryName,
                    CreatedTime = DateTimeOffset.Now,
                    LastUpdatedTime = DateTimeOffset.Now
                };

                await _unitOfWork.GetRepository<Category>().InsertAsync(category);
                await _unitOfWork.SaveAsync();

                return new ApiSuccessResult<object>("Category created successfully.");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<object>(ex.Message);
            }
        }

        public async Task<ApiResult<List<CategoryModelView>>> GetAllCategories()
        {
            try
            {
                var categories = await _unitOfWork.GetRepository<Category>().Entities.Where(x => x.DeletedTime == null).ToListAsync();
                var rs = _mapper.Map<List<CategoryModelView>>(categories);
                return new ApiSuccessResult<List<CategoryModelView>>(rs,"Categories retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<List<CategoryModelView>>(ex.Message);
            }
        }

        public async Task<ApiResult<CategoryModelView>> GetCategoryById(int id)
        {
            try
            {
                var category = await  _unitOfWork.GetRepository<Category>().Entities.FirstOrDefaultAsync(x => x.Id == id && x.DeletedTime == null);
                if (category == null)
                    return new ApiErrorResult<CategoryModelView>("Category not found.");

                var rs = _mapper.Map<CategoryModelView>(category);
                return new ApiSuccessResult<CategoryModelView>(rs,"Category retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<CategoryModelView>(ex.Message);
            }
        }

        public async Task<ApiResult<object>> UpdateCategory(CategoryUpdateModelView model)
        {
            try
            {
                var category = await _unitOfWork.GetRepository<Category>().Entities.FirstOrDefaultAsync(x => x.Id == model.Id && x.DeletedTime == null);
                if (category == null)
                    return new ApiErrorResult<object>("Category not found.");

                if (string.IsNullOrWhiteSpace(model.CategoryName))
                    return new ApiErrorResult<object>("Category name is required.");

                category.CategoryName = model.CategoryName;
                category.LastUpdatedTime = DateTimeOffset.Now;

                await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
                await _unitOfWork.SaveAsync();

                return new ApiSuccessResult<object>("Category updated successfully.");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<object>(ex.Message);
            }
        }

        public async Task<ApiResult<object>> DeleteCategory(int id)
        {
            try
            {
                var category = await _unitOfWork.GetRepository<Category>().Entities.FirstOrDefaultAsync(x => x.Id == id && x.DeletedTime == null);
                if (category == null)
                    return new ApiErrorResult<object>("Category not found.");

                category.DeletedTime = DateTimeOffset.Now;
                await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
                await _unitOfWork.SaveAsync();

                return new ApiSuccessResult<object>("Category deleted successfully.");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<object>(ex.Message);
            }
        }


        public async Task<ApiResult<BasePaginatedList<CategoryModelView>>> GetPaging(int pageNumber, int pageSize, string? name, int? id)
        {
            var query = _unitOfWork.GetRepository<Category>().Entities.Where(x => x.DeletedTime == null).AsQueryable();
            if (id != null)
            {
                query = query.Where(x => (x.Id == id));
            }

            // 1. Áp dụng bộ lọc (Filtering)
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.CategoryName.ToLower().Contains(name.ToLower())
                                        );
            }
          

            // 3. Tổng số bản ghi
            var totalRecords = await query.CountAsync();

            var currentPage = pageNumber != 0 ? pageNumber : 1;
            var pageSizeR = pageSize != 0 ? pageSize : 10;
            var total = await query.CountAsync();
            // 4. Áp dụng phân trang (Pagination)
            var data = await query
                .Skip((currentPage - 1) * pageSizeR)
                .Take(pageSizeR)
                .ToListAsync();




            var res = _mapper.Map<List<CategoryModelView>>(data);

            var response = new BasePaginatedList<CategoryModelView>(res, total, currentPage, pageSizeR);
            // return to client
            return new ApiSuccessResult<BasePaginatedList<CategoryModelView>>(response);
        }

  
    }
}
