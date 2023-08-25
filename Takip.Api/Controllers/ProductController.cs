using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using Takip.Model;
using Takip.Model.Tables;
using Takip.Repository;
using static Takip.Model.Enums;

namespace Takip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache) { }

        [HttpGet("AllProducts")]
        public dynamic AllProducts()
        {
            List<Product> items;

            if (!cache.TryGetValue("AllProducts", out items))
            {
                items = repo.ProductRepository.FindAll().ToList<Product>();
                cache.Set("AllProducts", items, DateTimeOffset.UtcNow.AddHours(1));
            }
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("ProductsByCategoryId")]
        public dynamic ProductsByCategoryId(int categoryId)
        {
            List<Product> items = repo.ProductRepository.ProductsByCategoryId(categoryId);
            return new
            {
                success = true,
                data = items
            };
        }

        [HttpGet("ProductsWithCategories")]
        public dynamic GetProductsWithCategories()
        {
            List<Product> items = repo.ProductRepository.ProductsWithCategories();
            return new
            {
                success = true,
                data = items
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddProduct")]
        public dynamic Add([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            Product item = new Product()
            {
                Id = json.Id,
                Name = json.Name,
                StockCode = json.StockCode,
                CriticalNumber = json.CriticalNumber,
                Active = json.Active
            };

            if (item.Id > 0)
                repo.ProductRepository.Update(item);
            else
            {
                //foreach (var categoryId in json.Categories)
                //{
                //    item.ProductCategories.Add(new ProductCategory() { CategoryId = categoryId });
                //}
                repo.ProductRepository.Create(item);
            }

            repo.SaveChanges();
            cache.Remove("AllProducts");

            return new
            {
                success = true
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteProduct")]
        public dynamic Delete(int id)
        {
            if (id <= 0)
                return new
                {
                    success = false,
                    message = "Geçersiz Id"
                };
            repo.ProductRepository.DeleteProduct(id);
            cache.Remove("AllProducts");

            return new
            {
                success = true
            };
        }

        [HttpGet("ProductCount")]
        public dynamic ProductCount()
        {
            var items = repo.ProductRepository.FindAll().Count();
            return new
            {
                success = true,
                data = items
            };
        }
    }
}
