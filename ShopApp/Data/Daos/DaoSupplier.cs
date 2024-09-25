using ShopApp.Data.Context;
using ShopApp.Data.Dtos;
using ShopApp.Data.Entities;
using ShopApp.Data.Exceptions;
using ShopApp.Data.Interfaces;

namespace ShopApp.Data.Daos
{
    public class DaoSupplier : ISupplier
    {
        private readonly ShopDbContext shopDb;
        private readonly ILogger<DaoSupplier> logger;

        public DaoSupplier(ShopDbContext shopDb,
                            ILogger<DaoSupplier> logger)
        {
            this.shopDb = shopDb;
            this.logger = logger;
        }

        public SupplierAddDto GetSupplierById(int supplierId)
        {
            SupplierAddDto supplierResult = new SupplierAddDto();
            try
            {
                var supplier = this.shopDb.Suppliers.Find(supplierId);

                if (supplier is null)
                {
                    throw new SupplierException("El suplidor no se encuentra registrado.");
                }

                supplierResult.SupplierId = supplier.supplierid;
                supplierResult.CompanyName = supplier.companyname;
                supplierResult.ContactName = supplier.contactname;
                supplierResult.ContactTitle = supplier.contacttitle;
                supplierResult.Address = supplier.address;
                supplierResult.City = supplier.city;
                supplierResult.Region = supplier.region;
                supplierResult.PostalCode = supplier.postalcode;
                supplierResult.Country = supplier.country;
                supplierResult.Phone = supplier.phone;
                supplierResult.Fax = supplier.fax;
                supplierResult.creation_date = supplier.creation_date;
                supplierResult.creation_user = supplier.creation_user;
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error obteniendo el suplidor", ex.ToString());
            }

            return supplierResult;
        }

        public List<SupplierAddDto> GetSuppliers()
        {
            List<SupplierAddDto> suppliers = new List<SupplierAddDto>();
            try
            {
                suppliers = (from sup in this.shopDb.Suppliers
                             where sup.deleted == false
                             orderby sup.creation_date descending
                             select new SupplierAddDto()
                             {
                                 SupplierId = sup.supplierid,
                                 CompanyName = sup.companyname,
                                 ContactName = sup.contactname,
                                 ContactTitle = sup.contacttitle,
                                 Address = sup.address,
                                 City = sup.city,
                                 Region = sup.region,
                                 PostalCode = sup.postalcode,
                                 Country = sup.country,
                                 Phone = sup.phone,
                                 Fax = sup.fax,
                                 creation_date = sup.creation_date,
                                 creation_user = sup.creation_user
                             }).ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error obteniendo los suplidores.", ex.ToString());
            }

            return suppliers;
        }

        public void RemoveSupplier(SupplierRemoveDto removeDto)
        {
            try
            {
                if (removeDto is null)
                {
                    throw new SupplierException("El objeto suplidor no puede ser nulo.");
                }

                var supplier = this.shopDb.Suppliers.Find(removeDto);

                if (supplier is null)
                {
                    throw new SupplierException("El suplidor no se encuentra registrado.");
                }

                supplier.deleted = true;
                supplier.delete_user = removeDto.delete_user;
                supplier.delete_date = removeDto.delete_date;
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error removiendo el suplidor.", ex.ToString());
            }
        }

        public void SaveSupplier(SupplierAddDto addDto)
        {
            try
            {
                if (addDto is null)
                {
                    throw new SupplierException("El objeto suplidor no puede ser nulo.");
                }

                // Lista de tuplas que contiene los campos y sus límites 
                var fieldLimits = new List<(string FieldName, string FieldValue, int MaxLength)>
                {
                    ("CompanyName", addDto.CompanyName, 40),
                    ("ContactName", addDto.ContactName, 30),
                    ("ContactTitle", addDto.ContactTitle, 30),
                    ("Address", addDto.Address, 60),
                    ("City", addDto.City, 15),
                    ("Region", addDto.Region, 15),
                    ("PostalCode", addDto.PostalCode, 10),
                    ("Country", addDto.Country, 15),
                    ("Phone", addDto.Phone, 24),
                    ("Fax", addDto.Fax, 24)
                };

                // Validar cada campo en la lista
                foreach (var field in fieldLimits)
                {
                    if (field.FieldValue.Length > field.MaxLength)
                    {
                        this.logger.LogWarning($"La longitud de {field.FieldName} sobrepasa el límite de {field.MaxLength} caracteres.");
                        throw new SupplierException($"El campo {field.FieldName} no puede exceder {field.MaxLength} caracteres.");
                    }
                }

                if (this.shopDb.Suppliers.Any(sup => sup.companyname == addDto.CompanyName))
                {
                    throw new SupplierException("El objeto suplidor no puede ser nulo.");
                }

                Supplier supplier = new Supplier()
                {
                    supplierid = addDto.SupplierId,
                    companyname = addDto.CompanyName,
                    contactname = addDto.ContactName,
                    contacttitle = addDto.ContactTitle,
                    address = addDto.Address,
                    city = addDto.City,
                    region = addDto.Region,
                    postalcode = addDto.PostalCode,
                    country = addDto.Country,
                    phone = addDto.Phone,
                    fax = addDto.Fax,
                    creation_date = addDto.creation_date,
                    creation_user = addDto.creation_user
                };
                this.shopDb.Suppliers.Add(supplier);
                this.shopDb.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error guardando el suplidor.", ex.ToString());
            }
        }

        public void UpdateSupplier(SupplierUpdateDto updateDto)
        {
            try
            {
                if (updateDto is null)
                {
                    throw new SupplierException("El objeto suplidor no puede ser nulo.");
                }

                Supplier supplier = this.shopDb.Suppliers.Find(updateDto.SupplierId);

                if (supplier is null)
                {
                    throw new SupplierException("El suplidor no se encuentra registrado.");
                }

                supplier.supplierid = updateDto.SupplierId;
                supplier.companyname = updateDto.CompanyName;
                supplier.contactname = updateDto.ContactName;
                supplier.contacttitle = updateDto.ContactTitle;
                supplier.address = updateDto.Address;
                supplier.city = updateDto.City;
                supplier.region = updateDto.Region;
                supplier.postalcode = updateDto.PostalCode;
                supplier.country = updateDto.Country;
                supplier.phone = updateDto.Phone;
                supplier.fax = updateDto.Fax;

                this.shopDb.Suppliers.Update(supplier);
                this.shopDb.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error actualizando el cliente.", ex.ToString());
            }
        }
    }
}
