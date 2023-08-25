using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takip.Model.Tables;
using Takip.Model.Views;

namespace Takip.Repository
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        public CategoryRepository(RepositoryContext context) : base(context) { }

        public List<V_AdminCategories> GetAdminCategories()
        {
            return RepositoryContext.AdminCategories.ToList<V_AdminCategories>();
        }

        public void DeleteCategory(int categoryId)
        {
            RepositoryContext.Categories.Where(c => c.Id == categoryId).ExecuteDelete();
        }
    }
}
