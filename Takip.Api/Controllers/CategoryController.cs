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
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache) { }

        [HttpGet("AllCategories")]
        public dynamic AllCategories()
        {
            List<Category> items = repo.CategoryRepository.FindAll().ToList<Category>();
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("GetCategoryById")]
        public dynamic GetCategoryById(int id)
        {
            Category item = repo.CategoryRepository.FindByCondition(a => a.Id == id).SingleOrDefault<Category>();
            return new
            {
                success = true,
                data = item
            };
        }

        [HttpGet("AdminCategories")]
        public dynamic AdminCategories()
        {
            List<V_AdminCategories> items = repo.CategoryRepository.GetAdminCategories();
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpPost("AddCategory")]
        public dynamic Add([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            Category item = new Category()
            {
                Id = json.Id,
                Name = json.Name,
                TopCategoryId = json.TopCategoryId,
                Active = json.Active,
                Photo = json.Photo
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

            if (item.Id > 0)
                repo.CategoryRepository.Update(item);
            else
                repo.CategoryRepository.Create(item);

            repo.SaveChanges();

            return new
            {
                success = true
            };
        }

        [HttpDelete("DeleteCategory")]
        public dynamic Delete(int id)
        {
            if (id <= 0)
                return new
                {
                    success = false,
                    message = "Geçersiz Id"
                };
            repo.CategoryRepository.DeleteCategory(id);

            return new
            {
                success = true
            };
        }
    }
}
