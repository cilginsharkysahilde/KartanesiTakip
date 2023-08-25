using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takip.Model.Tables
{
    [Table("tblCategory")]
    public class Category
    {
        public Category()
        {
            SubCategories = new HashSet<Category>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? TopCategoryId { get; set; }
        public string? Photo { get; set; }
        public bool Active { get; set; }

        public virtual Category? TopCategory { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
    }
}
