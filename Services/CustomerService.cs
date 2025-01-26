using TrainingProjectAPI.Models;
using TrainingProjectAPI.Models.DB;
using TrainingProjectAPI.Models.DTO;

namespace TrainingProjectAPI.Services
{
    public class CustomerService
    {
        private readonly ApplicationContext _context;

        public CustomerService(ApplicationContext context)
        {
            _context = context;
        }

        public List<CustomerDTO> GetListCustomer()
        {
            var datas = _context.Customers.Select(x => new CustomerDTO
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Address = x.Address,
                City = x.City,
                PhoneNumber = x.PhoneNumber,
                CreatedDate = x.CreatedDate != null ? x.CreatedDate.Value.ToString("dd/MM/yyyy H:mm:ss") : "",
                UpdatedDate = x.UpdatedDate != null ? x.UpdatedDate.Value.ToString("dd/MM/yyyy H:mm:ss") : "",

            }).ToList();
            return datas;
        }

        public CustomerDTO GetById(int customerId)
        {
            var databyId = _context.Customers.Where(x => x.Id == customerId).Select(x => new CustomerDTO
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Address = x.Address,
                City = x.City,
                PhoneNumber = x.PhoneNumber,
                CreatedDate = x.CreatedDate != null ? x.CreatedDate.Value.ToString("dd/MM/yyyy H:mm:ss") : "",
                UpdatedDate = x.UpdatedDate != null ? x.UpdatedDate.Value.ToString("dd/MM/yyyy H:mm:ss") : "",
            }).FirstOrDefault();
            return databyId;
        }

        public bool CreateCustomer(CustomerRequestDTO customer)
        {
            try
            {
                var InsertDataCustomer = new Customer
                {
                    Name = customer.Name,
                    Address = customer.Address,
                    City = customer.City,
                    PhoneNumber = customer.PhoneNumber,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                };
                _context.Customers.Add(InsertDataCustomer);

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCustomer(int Id, CustomerRequestDTO customer)
        {
            try
            {
                var customerOld = _context.Customers.Where(x => x.Id == Id).FirstOrDefault();

                if (customerOld != null)
                {
                    customerOld.Name = customer.Name;
                    customerOld.Address = customer.Address;
                    customerOld.City = customer.City;
                    customerOld.PhoneNumber = customer.PhoneNumber;
                    customerOld.UpdatedDate = DateTime.Now;

                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCustomer(int id)
        {
            try
            {
                var customerData = _context.Customers.FirstOrDefault(DEL => DEL.Id == id);

                if (customerData != null)
                {
                    _context.Customers.Remove(customerData);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}