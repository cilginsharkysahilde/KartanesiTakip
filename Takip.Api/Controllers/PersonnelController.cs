using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.Linq;
using Takip.Model.Tables;
using Takip.Model.Views;
using Takip.Repository;

namespace Takip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : BaseController
    {
        public PersonnelController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache) { }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllPersonnels")]
        public dynamic GetAll()
        {
            List<Personnel> items;

            if (!cache.TryGetValue("GetAllPersonnels", out items))
            {
                items = repo.PersonnelRepository.FindAll().ToList<Personnel>();
                cache.Set("GetAllPersonnels", items, DateTimeOffset.UtcNow.AddHours(1));
            }
            return new
            {
                success = true,
                data = items
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetPersonnelsById")]
        public dynamic GetById(int id)
        {
            Personnel item = repo.PersonnelRepository.FindByCondition(a => a.Id == id).SingleOrDefault<Personnel>();
            return new
            {
                success = true,
                data = item
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("GetPersonnelsByNameOrPhoneNumberOrAddress")]
        public dynamic GetPersonnelsByNameOrPhone([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());
            List<Personnel> items = new List<Personnel>();

            string name = json.Name;
            string phoneNumber = json.PhoneNumber;

            if ((name == null && phoneNumber != null) || (name != null && phoneNumber == null))
            {
                items = repo.PersonnelRepository.FindByCondition(a => a.Name.ToLower().Contains(name) || a.PhoneNumber.ToLower().Contains(phoneNumber)).ToList<Personnel>();
            }
            else if (name != null && phoneNumber != null)
            {
                items = repo.PersonnelRepository.FindByCondition(a => a.Name.ToLower().Contains(name) && a.PhoneNumber.ToLower().Contains(phoneNumber)).ToList<Personnel>();
            }
            return new
            {
                success = true,
                data = items
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddPersonnel")]
        public dynamic Add([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            Personnel item = new Personnel()
            {
                Id = json.Id,
                Name = json.Name,
                RoleId = json.RoleId,
                TcNumber = json.TcNumber,
                EMail = json.EMail,
                Password = json.Password,
                PhoneNumber = json.PhoneNumber,
                MaritalStatusId = json.MaritalStatusId,
                EducationalBackgroundId = json.EducationalBackgroundId,
                StartDate = json.StartDate,
                QuitDate = json.QuitDate,
                Salary = json.Salary,
                Active = json.Active
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
                repo.PersonnelRepository.Update(item);
            }
            else
            {
                repo.PersonnelRepository.Create(item);
            }

            repo.SaveChanges();

            cache.Remove("GetAllPersonnels");

            return new
            {
                success = true
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("ActivePersonnels")]
        public dynamic ActivePersonnels()
        {
            List<V_ActivePersonnels> items = repo.PersonnelRepository.GetActivePersonnels();
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("PersonnelDetailsById")]
        public dynamic PersonnelDetailsById(int id)
        {
            List<V_AdminPersonnelDetail> items = repo.PersonnelRepository.GetPersonnelDetailsById(id);
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("PersonnelDetails")]
        public dynamic PersonnelDetails()
        {
            List<V_AdminPersonnelDetail> items = repo.PersonnelRepository.GetPersonnelDetails();
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("GetRoles")]
        public dynamic Roles()
        {
            List<Role> items = repo.PersonnelRepository.GetRoles();

            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("GetMaritalStatuses")]
        public dynamic MaritalStatuses()
        {
            List<MaritalStatus> items = repo.PersonnelRepository.GetMaritalStatuses();

            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("GetEducations")]
        public dynamic Educations()
        {
            List<EducationalBackground> items = repo.PersonnelRepository.GetEducations();

            return new
            {
                success = true,
                data = items
            };
        }

        [HttpDelete("DeletePersonnel")]
        public dynamic Delete(int id)
        {
            if (id <= 0)
                return new
                {
                    success = false,
                    message = "Geçersiz Id"
                };
            repo.PersonnelRepository.DeletePersonnel(id);

            return new
            {
                success = true
            };
        }

        [HttpGet("PersonnelCount")]
        public dynamic PersonnelCount()
        {
            var items = repo.PersonnelRepository.FindAll().Count();
            return new
            {
                success = true,
                data = items
            };
        }
    }
}
