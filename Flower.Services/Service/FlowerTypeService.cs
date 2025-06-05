using AutoMapper;
using Flower.Contract.Repositories.Entity;
using Flower.Contract.Repositories.Interface;
using Flower.Contract.Services.Interface;
using Flower.Core;
using Flower.Core.APIResponse;
using Flower.ModelViews.FlowerTypeModelViews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Services.Service
{
    public class FlowerTypeService : IFlowerTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FlowerTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResult<object>> CreateFlowerType(FlowerTypeCreateModelView model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name))
                    return new ApiErrorResult<object>("Flower type name is required.");

                if (model.Price < 0)
                    return new ApiErrorResult<object>("Price cannot be negative.");

                if (model.Stock < 0)
                    return new ApiErrorResult<object>("Stock cannot be negative.");

                if (model.CategoryID.HasValue)
                {
                    var category = await _unitOfWork.GetRepository<Category>().Entities.FirstOrDefaultAsync(x => x.Id == model.CategoryID.Value && x.DeletedTime == null);
                    if (category == null)
                        return new ApiErrorResult<object>("Category not found.");
                }

                var imageUrls = new List<string>();
                if (model.ImageURLs != null && model.ImageURLs.Any())
                {
                    foreach (var image in model.ImageURLs)
                    {
                        var uploadResult = await Flower.Core.Firebase.ImageHelper.Upload(image);
                        if (uploadResult != null)
                            imageUrls.Add(uploadResult.ToString());
                    }
                }

                var flowerType = new FlowerType
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Stock = model.Stock,
                    ImageURLs = imageUrls,
                    CategoryID = model.CategoryID,
                    CreatedTime = DateTimeOffset.Now,
                    LastUpdatedTime = DateTimeOffset.Now
                };

                await _unitOfWork.GetRepository<FlowerType>().InsertAsync(flowerType);
                await _unitOfWork.SaveAsync();

                return new ApiSuccessResult<object>("Flower type created successfully.");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<object>(ex.Message);
            }
        }   

        public async Task<ApiResult<List<FlowerTypeModelView>>> GetAllFlowerTypes()
        {
            try
            {
                var flowerTypes = await _unitOfWork.GetRepository<FlowerType>().Entities.Where(x => x.DeletedTime == null).ToListAsync();
                var result = _mapper.Map<List<FlowerTypeModelView>>(flowerTypes);
                return new ApiSuccessResult<List<FlowerTypeModelView>>(result, "Flower types retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<List<FlowerTypeModelView>>(ex.Message);
            }
        }

        public async Task<ApiResult<FlowerTypeModelView>> GetFlowerTypeById(int id)
        {
            try
            {
                var flowerType = await _unitOfWork.GetRepository<FlowerType>().Entities.FirstOrDefaultAsync(x => x.Id == id && x.DeletedTime == null);
                if (flowerType == null)
                    return new ApiErrorResult<FlowerTypeModelView>("Flower type not found.");

                var result = _mapper.Map<FlowerTypeModelView>(flowerType);
                return new ApiSuccessResult<FlowerTypeModelView>(result, "Flower type retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<FlowerTypeModelView>(ex.Message);
            }
        }

        public async Task<ApiResult<BasePaginatedList<FlowerTypeModelView>>> GetPaging(int pageNumber, int pageSize, string? name, int? id, int? categoryId, string? categoryName)
        {
            try
            {
                var query = _unitOfWork.GetRepository<FlowerType>().Entities.Where(x => x.DeletedTime == null).AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(x => x.Id == id.Value);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
                }
                if(!string.IsNullOrEmpty(categoryName))
                {
                    query = query.Where(x => x.Category.CategoryName.ToLower().Contains(categoryName.ToLower()));
                }
                if(categoryId.HasValue)
                {
                    query = query.Where(x => x.CategoryID == categoryId.Value);
                }

                var totalRecords = await query.CountAsync();
                var currentPage = pageNumber  != 0 ?  pageNumber : 1;
                var pageSizeR = pageSize != 0 ? pageSize : 10;
                var data = await query
                    .Skip((currentPage - 1) * pageSizeR)
                    .Take(pageSizeR)
                    .ToListAsync();

                var result = _mapper.Map<List<FlowerTypeModelView>>(data);
                var response = new BasePaginatedList<FlowerTypeModelView>(result, totalRecords, currentPage, pageSizeR);

                return new ApiSuccessResult<BasePaginatedList<FlowerTypeModelView>>(response, "Flower types retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<BasePaginatedList<FlowerTypeModelView>>(ex.Message);
            }
        }

        public async Task<ApiResult<object>> UpdateFlowerType(FlowerTypeUpdateModelView model)
        {
            try
            {
                var flowerType = await _unitOfWork.GetRepository<FlowerType>().Entities.FirstOrDefaultAsync(x => x.Id == model.Id && x.DeletedTime == null);
                if (flowerType == null)
                    return new ApiErrorResult<object>("Flower type not found.");

                if (string.IsNullOrWhiteSpace(model.Name))
                    return new ApiErrorResult<object>("Flower type name is required.");

                if (model.Price < 0)
                    return new ApiErrorResult<object>("Price cannot be negative.");

                if (model.Stock < 0)
                    return new ApiErrorResult<object>("Stock cannot be negative.");

                if (model.CategoryID.HasValue)
                {
                    var category = await _unitOfWork.GetRepository<Category>().Entities.FirstOrDefaultAsync(x => x.Id == model.CategoryID.Value && x.DeletedTime == null);
                    if (category == null)
                        return new ApiErrorResult<object>("Category not found.");
                }

                var imageUrls = flowerType.ImageURLs ?? new List<string>();
                if (model.ImageURLs != null && model.ImageURLs.Any())
                {
                    foreach (var image in model.ImageURLs)
                    {
                        var uploadResult = await Flower.Core.Firebase.ImageHelper.Upload(image);
                        if (uploadResult != null)
                            imageUrls.Add(uploadResult.ToString());
                    }
                }

                flowerType.Name = model.Name;
                flowerType.Description = model.Description;
                flowerType.Price = model.Price;
                flowerType.Stock = model.Stock;
                flowerType.ImageURLs = imageUrls;
                flowerType.CategoryID = model.CategoryID;
                flowerType.LastUpdatedTime = DateTimeOffset.Now;

                await _unitOfWork.GetRepository<FlowerType>().UpdateAsync(flowerType);
                await _unitOfWork.SaveAsync();

                return new ApiSuccessResult<object>("Flower type updated successfully.");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<object>(ex.Message);
            }
        }

        public async Task<ApiResult<object>> DeleteFlowerType(int id)
        {
            try
            {
                var flowerType = await _unitOfWork.GetRepository<FlowerType>().Entities.FirstOrDefaultAsync(x => x.Id == id && x.DeletedTime == null);
                if (flowerType == null)
                    return new ApiErrorResult<object>("Flower type not found.");

                flowerType.DeletedTime = DateTimeOffset.Now;
                await _unitOfWork.GetRepository<FlowerType>().UpdateAsync(flowerType);
                await _unitOfWork.SaveAsync();
                return new ApiSuccessResult<object>("Flower type deleted successfully.");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<object>(ex.Message);
            }
        }
    }
}
