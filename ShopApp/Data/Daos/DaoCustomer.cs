using ShopApp.Data.Context;
using ShopApp.Data.Dtos;
using ShopApp.Data.Entities;
using ShopApp.Data.Exceptions;
using ShopApp.Data.Interfaces;

namespace ShopApp.Data.Daos
{
    public class DaoCustomer : ICustomer
    {
        private readonly ShopDbContext shopDb;
        private readonly ILogger<DaoCustomer> logger;

        public DaoCustomer(ShopDbContext shopDb,
                            ILogger<DaoCustomer> logger)
        {
            this.shopDb = shopDb;
            this.logger = logger;
        }

        public CustomerAddDto GetCustomerById(int customerId)
        {
            CustomerAddDto customerResult = new CustomerAddDto();
            try
            {
                var customer = this.shopDb.Customers.Find(customerId);

                if (customer is null)
                {
                    throw new CustomerException("El cliente no se encuentra registrado.");
                }

                customerResult.CustId = customer.custid;
                customerResult.CompanyName = customer.companyname;
                customerResult.ContactName = customer.contactname;
                customerResult.ContactTitle = customer.contacttitle;
                customerResult.Address = customer.address;
                customerResult.Email = customer.email;
                customerResult.City = customer.city;
                customerResult.Region = customer.region;
                customerResult.PostalCode = customer.postalcode;
                customerResult.Country = customer.country;
                customerResult.Phone = customer.phone;
                customerResult.Fax = customer.fax;
                customerResult.creation_date = customer.creation_date;
                customerResult.creation_user = customer.creation_user;
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error obteniendo el cliente", ex.ToString());
            }

            return customerResult;
        }

        public List<CustomerAddDto> GetCustomers()
        {
            List<CustomerAddDto> customers = new List<CustomerAddDto>();
            try
            {
                customers = (from cust in this.shopDb.Customers
                             where cust.deleted == false
                             orderby cust.creation_date descending
                             select new CustomerAddDto()
                             {
                                 CustId = cust.custid,
                                 CompanyName = cust.companyname,
                                 ContactName = cust.contactname,
                                 ContactTitle = cust.contacttitle,
                                 Address = cust.address,
                                 Email = cust.email,
                                 City = cust.city,
                                 Region = cust.region,
                                 PostalCode = cust.postalcode,
                                 Country = cust.country,
                                 Phone = cust.phone,
                                 Fax = cust.fax,
                                 creation_date = cust.creation_date,
                                 creation_user = cust.creation_user
                             }).ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error obteniendo los clientes.", ex.ToString());
            }

            return customers;
        }

        public void RemoveCustomer(CustomerRemoveDto removeDto)
        {
            try
            {
                if (removeDto is null)
                {
                    throw new CustomerException("El objeto cliente no puede ser nulo.");
                }

                var customer = this.shopDb.Customers.Find(removeDto);

                if (customer is null)
                {
                    throw new CustomerException("El cliente no se encuentra registrado.");
                }

                customer.deleted = true;
                customer.delete_user = removeDto.delete_user;
                customer.delete_date = removeDto.delete_date;
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error removiendo el cliente.", ex.ToString());
            }
        }

        public void SaveCustomer(CustomerAddDto addDto)
        {
            try
            {
                if (addDto is null)
                {
                    throw new CustomerException("El objeto cliente no puede ser nulo.");
                }

                // Lista de tuplas que contiene los campos y sus límites 
                var fieldLimits = new List<(string FieldName, string FieldValue, int MaxLength)>
                {
                    ("CompanyName", addDto.CompanyName, 40),
                    ("ContactName", addDto.ContactName, 30),
                    ("ContactTitle", addDto.ContactTitle, 30),
                    ("Address", addDto.Address, 60),
                    ("Email", addDto.Email, 50),
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
                        throw new CustomerException($"El campo {field.FieldName} no puede exceder {field.MaxLength} caracteres.");
                    }
                }


                if (this.shopDb.Customers.Any(cust => cust.companyname == addDto.CompanyName))
                {
                    throw new CustomerException("El objeto cliente no puede ser nulo.");
                }

                Customer customer = new Customer()
                {
                    custid = addDto.CustId,
                    companyname = addDto.CompanyName,
                    contactname = addDto.ContactName,
                    contacttitle = addDto.ContactTitle,
                    address = addDto.Address,
                    email = addDto.Email,
                    city = addDto.City,
                    region = addDto.Region,
                    postalcode = addDto.PostalCode,
                    country = addDto.Country,
                    phone = addDto.Phone,
                    fax = addDto.Fax,
                    creation_date = addDto.creation_date,
                    creation_user = addDto.creation_user
                };
                this.shopDb.Customers.Add(customer);
                this.shopDb.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error guardando el cliente.", ex.ToString());
            }
        }

        public void UpdateCustomer(CustomerUpdateDto updateDto)
        {
            try
            {
                if (updateDto is null)
                {
                    throw new CustomerException("El objeto cliente no puede ser nulo.");
                }

                Customer customer = this.shopDb.Customers.Find(updateDto.CustId);

                if (customer is null)
                {
                    throw new CustomerException("El cliente no se encuentra registrado.");
                }

                customer.custid = updateDto.CustId;
                customer.companyname = updateDto.CompanyName;
                customer.contactname = updateDto.ContactName;
                customer.contacttitle = updateDto.ContactTitle;
                customer.address = updateDto.Address;
                customer.email = updateDto.Email;
                customer.city = updateDto.City;
                customer.region = updateDto.Region;
                customer.postalcode = updateDto.PostalCode;
                customer.country = updateDto.Country;
                customer.phone = updateDto.Phone;
                customer.fax = updateDto.Fax;

                this.shopDb.Customers.Update(customer);
                this.shopDb.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error actualizando el cliente.", ex.ToString());
            }
        }
    }
}
