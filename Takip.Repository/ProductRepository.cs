using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takip.Model.Tables;

namespace Takip.Repository
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(RepositoryContext context) : base(context) { }

        public List<Product> ProductsByCategoryId(int categoryId)
        {
            List<Product> productCategories = (from p in RepositoryContext.Products
                                               join c in RepositoryContext.ProductCategories on p.Id equals c.ProductId
                                               where c.CategoryId == categoryId
                                               select p).ToList<Product>();
            return productCategories;
        }

        public List<Product> ProductsWithCategories()
        {
            List<Product> products = RepositoryContext.Products.Include(a => a.ProductCategories).ToList<Product>();
            return products;
        }

        public void DeleteProduct(int productId)
        {
            RepositoryContext.Products.Where(p => p.Id == productId).ExecuteDelete();
        }
    }
}
