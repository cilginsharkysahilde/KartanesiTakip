using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using Takip.Model.Tables;
using Takip.Repository;
using static Takip.Model.Enums;

namespace Takip.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        public RoleController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache) { }

        [HttpGet("AllRoles")]
        public dynamic AllRoles()
        {
            List<Role> items = repo.RoleRepository.FindAll().ToList<Role>();

            return new
            {
                success = true,
                data = items
            };
        }

        [HttpPost("AddRole")]
        public dynamic Add([FromBody] dynamic model) 
        {
            dynamic json = JObject.Parse(model.GetRawText());

            Role item = new Role()
            {
                Id = json.Id,
                Name = json.Name
            };

            if (string.IsNullOrEmpty(item.Name)) 
            {
                return new
                {
                    success = false,
                    message = "Ad alanı boş geçilemez"
                };
            }
            if (item.Name.Length > 20)
            {
                return new
                {
                    success = false,
                    message = "Ad alanı maksimum 20 karakter olabilir"
                };
                
            }

            if(item.Id > 0)
                repo.RoleRepository.Update(item);
            else
                repo.RoleRepository.Create(item);

            repo.SaveChanges();

            return new
            {
                success = true
            };
        }

        [HttpDelete("DeleteRole")]
        public dynamic Delete(int id)
        {
            if (id <= 0)
                return new
                {
                    success = false,
                    message = "Geçersiz Id"
                };
            repo.RoleRepository.DeleteRole(id);

            return new
            {
                success = true
            };
        }
    }
}
