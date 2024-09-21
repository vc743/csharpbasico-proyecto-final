using ShopApp.Data.Dtos;

namespace ShopApp.Data.Interfaces
{
    public interface ICategory
    {
        void SaveCategory(CategoryAddDto addDto);
        void RemoveCategory(CategoryRemoveDto removeDto);
        void UpdateCategory(CategoryUpdateDto updateDto);

        List<CategoryAddDto> GetCategories();

        CategoryAddDto GetCategoryById(int categoryId);
    }
}
