using ShopApp.Data.Dtos;

namespace ShopApp.Data.Interfaces
{
    public interface ISupplier
    {
        void SaveSupplier(SupplierAddDto addDto);
        void RemoveSupplier(SupplierRemoveDto removeDto);
        void UpdateSupplier(SupplierUpdateDto updateDto);
        List<SupplierAddDto> GetSuppliers();
        SupplierAddDto GetSupplierById(int supplierId);
    }
}
