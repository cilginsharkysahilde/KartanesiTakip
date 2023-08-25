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
    public class ServiceController : BaseController
    {
        public ServiceController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache) { }

        [HttpGet("GetAdminServices")]
        public dynamic Get(int id)
        {
            List<V_AdminServices> items = repo.ServiceRepository.AdminServicesById(id);
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("GetAllAdminServices")]
        public dynamic GetAll()
        {
            List<V_AdminServices> items = repo.ServiceRepository.AdminServices();
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpPost("AddService")]
        public dynamic AddService([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            Service item = new Service()
            {
                Id = json.Id,
                CustomerId = json.CustomerId,
                ProcessTypeId = json.ProcessTypeId,
                ProductId = json.ProductId,
                DueDate = json.DueDate,
                ItemDescription = json.ItemDescription,
                CustomerComplaint = json.CustomerComplaint,
                WaterValues = json.WaterValues,
                ServicePersonnelId = json.ServicePersonnelId,
                IsMaintenanceDone = json.IsMaintenanceDone,
                MaintenancePeriodId = json.MaintenancePeriodId,
                MaintenanceMonthId = json.MaintenanceMonthId,
                MaintenanceYearId = json.MaintenanceYearId,
                SpecialNote = json.SpecialNote
            };

            if (item.Id > 0)
            {
                repo.ServiceRepository.Update(item);
            }
            else
            {
                repo.ServiceRepository.Create(item);
            }

            repo.SaveChanges();
            return new
            {
                success = true
            };
        }

        [HttpGet("GetProcessTypes")]
        public dynamic GetProcessTypes()
        {
            List<ProcessType> items = repo.ServiceRepository.GetProcessTypes();

            return new
            {
                success = true,
                data = items
            };
        }

        [HttpDelete("DeleteService")]
        public dynamic Delete(int id)
        {
            if (id <= 0)
                return new
                {
                    success = false,
                    message = "Geçersiz Id"
                };
            repo.ServiceRepository.DeleteService(id);

            return new
            {
                success = true
            };
        }

        [HttpGet("ServiceCount")]
        public dynamic ServiceCount()
        {
            var items = repo.ServiceRepository.FindAll().Count();
            return new
            {
                success = true,
                data = items
            };
        }
    }
}
