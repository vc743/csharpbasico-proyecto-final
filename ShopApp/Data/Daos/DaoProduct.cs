using ShopApp.Data.Context;
using ShopApp.Data.Dtos;
using ShopApp.Data.Interfaces;
using ShopApp.Data.Entities;
using ShopApp.Data.Exceptions;

namespace ShopApp.Data.Daos
{
    public class DaoProduct : IProduct
    {
        private readonly ShopDbContext shopDb;
        private readonly ILogger<DaoProduct> logger;

        public DaoProduct(ShopDbContext shopDb,
                            ILogger<DaoProduct> logger)
        {
            this.shopDb = shopDb;
            this.logger = logger;
        }

        public ProductAddDto GetProductById(int productId)
        {
            ProductAddDto productResult = new ProductAddDto();
            try
            {
                var product = this.shopDb.Products.Find(productId);

                if (product is null)
                {
                    throw new ProductException("El producto no se encuentra registrado.");
                }

                productResult.ProductId = product.productid;
                productResult.ProductName = product.productname;
                productResult.SupplierId = product.supplierid;
                productResult.CategoryId = product.categoryid;
                productResult.UnitPrice = product.unitprice;
                productResult.Discontinued = product.discontinued;
                productResult.creation_date = product.creation_date;
                productResult.creation_user = product.creation_user;
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error obteniendo el producto", ex.ToString());
            }

            return productResult;
        }

        public List<ProductAddDto> GetProducts()
        {
            List<ProductAddDto> products = new List<ProductAddDto>();
            try
            {
                products = (from pro in this.shopDb.Products
                             where pro.deleted == false
                             orderby pro.creation_date descending
                             select new ProductAddDto()
                             {
                                 ProductId = pro.productid,
                                 ProductName = pro.productname,
                                 SupplierId = pro.supplierid,
                                 CategoryId = pro.categoryid,
                                 UnitPrice = pro.unitprice,
                                 Discontinued = pro.discontinued,
                                 creation_date = pro.creation_date,
                                 creation_user = pro.creation_user
                             }).ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error obteniendo los productos.", ex.ToString());
            }

            return products;
        }

        public void RemoveProduct(ProductRemoveDto removeDto)
        {
            try
            {
                if (removeDto is null)
                {
                    throw new ProductException("El objeto producto no puede ser nulo.");
                }

                var product = this.shopDb.Products.Find(removeDto);

                if (product is null)
                {
                    throw new ProductException("El producto no se encuentra registrado.");
                }

                product.deleted = true;
                product.delete_user = removeDto.delete_user;
                product.delete_date = removeDto.delete_date;
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error removiendo el producto.", ex.ToString());
            }
        }

        public void SaveProduct(ProductAddDto addDto)
        {
            try
            {
                if (addDto is null)
                {
                    throw new ProductException("El objeto producto no puede ser nulo.");
                }

                if (this.shopDb.Products.Any(pro => pro.productname == addDto.ProductName))
                {
                    throw new ProductException("El objeto producto no puede ser nulo.");
                }

                Product product = new Product()
                {
                    productid = addDto.ProductId,
                    productname = addDto.ProductName,
                    supplierid = addDto.SupplierId,
                    categoryid = addDto.CategoryId,
                    unitprice = addDto.UnitPrice,
                    discontinued = addDto.Discontinued,
                    creation_date = addDto.creation_date,
                    creation_user = addDto.creation_user
                };
                this.shopDb.Products.Add(product);
                this.shopDb.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error guardando el producto.", ex.ToString());
            }
        }

        public void UpdateProduct(ProductUpdateDto updateDto)
        {
            try
            {
                if (updateDto is null)
                {
                    throw new ProductException("El objeto producto no puede ser nulo.");
                }

                Product product = this.shopDb.Products.Find(updateDto.ProductId);

                if (product is null)
                {
                    throw new ProductException("El producto no se encuentra registrado.");
                }

                product.productid = updateDto.ProductId;
                product.productname = updateDto.ProductName;
                product.supplierid = updateDto.SupplierId;
                product.categoryid = updateDto.CategoryId;
                product.unitprice = updateDto.UnitPrice;
                product.discontinued = updateDto.Discontinued;

                this.shopDb.Products.Update(product);
                this.shopDb.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error actualizando el producto.", ex.ToString());
            }
        }
    }
}
