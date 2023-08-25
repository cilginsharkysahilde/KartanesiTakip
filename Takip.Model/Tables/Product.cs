using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takip.Model.Tables
{
    [Table("tblProduct")]
    public class Product
    {
        public Product()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string StockCode { get; set; }
        public short CriticalNumber { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<ProductCategory>? ProductCategories { get; set; }

    }
}
