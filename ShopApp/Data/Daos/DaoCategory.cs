using ShopApp.Data.Context;
using ShopApp.Data.Dtos;
using ShopApp.Data.Entities;
using ShopApp.Data.Interfaces;

namespace ShopApp.Data.Daos
{
    public class DaoCategory : ICategory
    {
        private readonly ShopDbContext shopDb;
        private readonly ILogger<DaoCategory> logger;

        public DaoCategory(ShopDbContext shopDb, ILogger<DaoCategory> logger)
        {
            this.shopDb=shopDb;
            this.logger = logger;
        }

        public List<CategoryAddDto> GetCategories()
        {
            List<CategoryAddDto> categories = new List<CategoryAddDto>();
            try
            {
                var query = (from cate in this.shopDb.Categories
                             where cate.Deleted == false
                             select new CategoryAddDto()
                             {
                                 CategoryName = cate.categoryname,
                                 Description = cate.description,
                                 CreationDate = cate.CreationDate,
                                 CreationUser = cate.CreationUser
                             }).ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error obteniendo las categorias", ex.ToString());
            }

            return categories;
        }

        public CategoryAddDto GetCategoryById(int categoryId)
        {
            CategoryAddDto categoryResult = new CategoryAddDto();
            try
            {
                var category = this.shopDb.Categories.Find(categoryId);
            }
            catch (Exception ex)
            {

                this.logger.LogError("Error obteniendo la categoria", ex.ToString());
            }

            return categoryResult;
        }

        public void RemoveCategory(CategoryRemoveDto removeDto)
        {
            throw new NotImplementedException();
        }

        public void SaveCatedory(CategoryAddDto addDto)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(CategoryUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
