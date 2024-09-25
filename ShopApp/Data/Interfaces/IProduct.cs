using ShopApp.Data.Dtos;

namespace ShopApp.Data.Interfaces
{
    public interface IProduct
    {
        void SaveProduct(ProductAddDto addDto);
        void RemoveProduct(ProductRemoveDto removeDto);
        void UpdateProduct(ProductUpdateDto updateDto);
        List<ProductAddDto> GetProducts();
        ProductAddDto GetProductById(int productId);
    }
}
