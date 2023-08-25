using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using Takip.Model.Tables;
using Takip.Model.Views;
using Takip.Repository;

namespace Takip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : BaseController
    {
        public AddressController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache) { }

        [HttpGet("GetByUserId")]
        public dynamic GetByUserId(int id)
        {
            List<Address> items = repo.AddressRepository.FindByCondition(a => a.CustomerId == id).ToList<Address>();

            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("AdminAddressesByUserId")]
        public dynamic GetAdminAddressesByUserId(int id)
        {
            List<V_AdminAddresses> items = repo.AddressRepository.GetAdminAddressesById(id);
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("GetCities")]
        public dynamic GetCities()
        {
            List<City> items = repo.AddressRepository.GetCities();

            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("GetDistricts")]
        public dynamic GetDistricts()
        {
            List<District> items = repo.AddressRepository.GetDistricts();

            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("GetCountries")]
        public dynamic GetCountries()
        {
            List<Country> items = repo.AddressRepository.GetCountries();

            return new
            {
                success = true,
                data = items
            };
        }

        [HttpDelete("DeleteAddress")]
        public dynamic Delete(int id)
        {
            if (id <= 0)
                return new
                {
                    success = false,
                    message = "Geçersiz Id"
                };
            repo.AddressRepository.DeleteAddress(id);

            return new
            {
                success = true
            };
        }

        [HttpPost("AddAddress")]
        public dynamic AddAddress([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            Address item = new Address()
            {
                Id = json.Id,
                CustomerId = json.CustomerId,
                Name = json.Name,
                Description = json.Description,
                CountryId = json.CountryId,
                CityId = json.CityId,
                DistrictId = json.DistrictId,
                ZipCode = json.ZipCode
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
                repo.AddressRepository.Update(item);
            }
            else
            {
                repo.AddressRepository.Create(item);
            }

            repo.SaveChanges();
            return new
            {
                success = true
            };
        }
    }
}
