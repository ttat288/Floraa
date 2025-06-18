using AutoMapper;
using Repository.Interfaces;
using Repository.Models;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<IEnumerable<CategoryDto>> GetActiveCategoriesAsync()
        {
            var categories = await _unitOfWork.Repository<Category>()
                .FindAsync(c => c.IsActive);
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(Guid id)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            await _unitOfWork.Repository<Category>().AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(Guid id, UpdateCategoryDto updateCategoryDto)
        {
            var existingCategory = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (existingCategory == null)
                return null;

            _mapper.Map(updateCategoryDto, existingCategory);
            _unitOfWork.Repository<Category>().Update(existingCategory);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(existingCategory);
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (category == null)
                return false;

            _unitOfWork.Repository<Category>().Remove(category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
