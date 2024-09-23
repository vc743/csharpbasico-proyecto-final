using ShopApp.Data.Context;
using ShopApp.Data.Dtos;
using ShopApp.Data.Entities;
using ShopApp.Data.Exceptions;
using ShopApp.Data.Interfaces;

namespace ShopApp.Data.Daos
{
    public class DaoCategory : ICategory
    {
        private readonly ShopDbContext shopDb;
        private readonly ILogger<DaoCategory> logger;

        public DaoCategory(ShopDbContext shopDb, 
                            ILogger<DaoCategory> logger)
        {
            this.shopDb = shopDb;
            this.logger = logger;
        }

        public List<CategoryAddDto> GetCategories()
        {
            List<CategoryAddDto> categories = new List<CategoryAddDto>();
            try
            {
                categories = (from cate in this.shopDb.Categories
                             where cate.deleted == false
                             orderby cate.creation_date descending
                             select new CategoryAddDto()
                             {
                                 CategoryId = cate.categoryid,
                                 CategoryName = cate.categoryname,
                                 Description = cate.description,
                                 creation_date = cate.creation_date,
                                 creation_user = cate.creation_user
                             }).ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error obteniendo las categorias.", ex.ToString());
            }

            return categories;
        }

        public CategoryAddDto GetCategoryById(int categoryId)
        {
            CategoryAddDto categoryResult = new CategoryAddDto();
            try
            {
                var category = this.shopDb.Categories.Find(categoryId);

                if (category is null)
                    throw new CategoryException("La categoria no se encuentra registrada.");

                categoryResult.CategoryId = category.categoryid;
                categoryResult.CategoryName = category.categoryname;
                categoryResult.Description = category.description;
                categoryResult.creation_date = category.creation_date;
                categoryResult.creation_user = category.creation_user;

            }
            catch (Exception ex)
            {

                this.logger.LogError("Error obteniendo la categoria.", ex.ToString());
            }

            return categoryResult;
        }

        public void RemoveCategory(CategoryRemoveDto removeDto)
        {
            try
            {
                if (removeDto is null)
                {
                    throw new CategoryException("El objeto categoria no puede ser nulo.");
                }

                var category = this.shopDb.Categories.Find(removeDto);

                if (category is null)
                {
                    throw new CategoryException("La categoria no se encuentra registrada.");
                }

                category.deleted = true;
                category.delete_user = removeDto.delete_user;
                category.delete_date = removeDto.delete_date;
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error removiendo la categoria.", ex.ToString());
            }
        }

        public void SaveCategory(CategoryAddDto addDto)
        {
            try
            {
                if (addDto is null)
                {
                    throw new CategoryException("El objeto categoria no puede ser nulo.");
                }

                if (this.shopDb.Categories.Any(cate => cate.categoryname == addDto.CategoryName))
                {
                    throw new CategoryException("El objeto categoria no puede ser nulo.");
                }

                Category category = new Category()
                {
                   categoryname = addDto.CategoryName,
                   description = addDto.Description,
                   creation_date = addDto.creation_date,
                   creation_user = addDto.creation_user
                };
                this.shopDb.Categories.Add(category);
                this.shopDb.SaveChanges();

            }
            catch (Exception ex)
            {
                this.logger.LogError("Error guardando la categoria.", ex.ToString());
            }
        }

        public void UpdateCategory(CategoryUpdateDto updateDto)
        {
            try
            {
                if (updateDto is null)
                {
                    throw new CategoryException("El objeto categoria no puede ser nulo.");
                }

                Category category = this.shopDb.Categories.Find(updateDto.CategoryId);

                if (category is null) 
                {
                    throw new CategoryException("La categoria no se encuentra registrada.");
                }

                category.categoryid = updateDto.CategoryId;
                category.categoryname = updateDto.CategoryName;
                category.description = updateDto.Description;
                category.modify_user = updateDto.modify_user;
                category.modify_date = updateDto.modify_date;

                this.shopDb.Categories.Update(category);
                this.shopDb.SaveChanges();

            }
            catch (Exception ex)
            {
                this.logger.LogError("Error actualizando la categoria.", ex.ToString());
            }
        }
    }
}
