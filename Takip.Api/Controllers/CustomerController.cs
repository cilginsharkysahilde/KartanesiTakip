using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using Takip.Model.Tables;
using Takip.Model.Views;
using Takip.Repository;
using static Takip.Model.Enums;

namespace Takip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        public CustomerController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache) { }

        [HttpGet("GetAllCustomers")]
        public dynamic GetAll()
        {
            List<Customer> items;

            if (!cache.TryGetValue("GetAllCustomers",out items))
            {
                items = repo.CustomerRepository.FindAll().ToList<Customer>();
                cache.Set("GetAllCustomers", items, DateTimeOffset.UtcNow.AddHours(1));
            }
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("GetCustomersById")]
        public dynamic GetCustomersById(int id)
        {
            Customer item = repo.CustomerRepository.FindByCondition(a => a .Id == id).SingleOrDefault<Customer>();
            return new
            {
                success = true,
                data = item
            };
        }

        [HttpPost("GetCustomersByNameOrPhoneNumberOrAddress")]
        public dynamic GetCustomersById([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());
            List<Customer> items = new List<Customer>();

            string name = json.Name;
            string phone = json.Phone;

            if ((name == null && phone != null) || (name != null && phone == null))
            {
                items = repo.CustomerRepository.FindByCondition(a => a.Name.ToLower().Contains(name) || a.PhoneNumber.ToLower().Contains(phone)).ToList<Customer>();
            }
            else if(name != null && phone != null )
            {
                items = repo.CustomerRepository.FindByCondition(a => a.Name.ToLower().Contains(name) && a.PhoneNumber.ToLower().Contains(phone)).ToList<Customer>();
            }
            return new
            {
                success = true,
                data = items
            };
        }

        [Authorize(Roles = "Admin, Sekreter")]
        [HttpPost("AddCustomer")]
        public dynamic AddCustomer([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            Customer item = new Customer()
            {
                Id = json.Id,
                Name = json.Name,
                PhoneNumber = json.PhoneNumber,
                HomePhoneNumber = json.HomePhoneNumber,
                ProductTypeId = json.ProductTypeId,
                SalespersonId = json.SalespersonId,
                RegistryDate = json.RegistryDate,
                SaleDate = json.SaleDate
            };

            if (string.IsNullOrEmpty(item.Name))
            {
                return new
                {
                    success = false,
                    message = "Ad alanı boş geçilemez"
                };
            }

            if (item.Id > 0)
            { 
                repo.CustomerRepository.Update(item); 
            }
            else
            {
                repo.CustomerRepository.Create(item);
            }

            repo.SaveChanges();

            cache.Remove("GetAllCustomers");

            return new
            {
                success = true
            };
        }

        //[HttpGet("Get/{id}")]
        //public dynamic Get(int id)
        //{
        //    Customer customer = repo.CustomerRepository.CustomerById(id);

        //    return new
        //    {
        //        success = true,
        //        data = customer
        //    };
        //}

        [HttpGet("AdminCustomers")]
        public dynamic AdminCustomers()
        {
            List<V_AdminCustomers> items = repo.CustomerRepository.AdminCustomers();
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("GetProducts")]
        public dynamic GetProducts()
        {
            List<V_Product> items = repo.CustomerRepository.GetEditProducts();
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("GetCustomers")]
        public dynamic GetCustomers()
        {
            List<V_Customer> items = repo.CustomerRepository.GetEditCustomers();
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("GetPersonnels")]
        public dynamic GetPersonnels()
        {
            List<V_Personnel> items = repo.CustomerRepository.GetEditPersonnels();
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpDelete("DeleteCustomer")]
        public dynamic Delete(int id)
        {
            if (id <= 0)
                return new
                {
                    success = false,
                    message = "Geçersiz Id"
                };
            repo.CustomerRepository.DeleteCustomer(id);

            return new
            {
                success = true
            };
        }

        //[HttpGet("CustomersWithServices")]
        //public dynamic Get()
        //{
        //    List<Customer> items = repo.CustomerRepository.GetCustomersWithService();

        //    return new
        //    {
        //        success = true,
        //        data = items
        //    };
        //}

        [HttpGet("CustomerCount")]
        public dynamic CustomerCount()
        {
            var items = repo.CustomerRepository.FindAll().Count();
            return new
            {
                success = true,
                data = items
            };
        }
    }
}

