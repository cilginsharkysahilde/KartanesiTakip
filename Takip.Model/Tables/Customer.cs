using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takip.Model.Tables
{
    [Table("tblCustomer")]
    public class Customer
    {
        public Customer()
        {
            Services = new HashSet<Service>();
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string? HomePhoneNumber { get; set; }
        public int ProductTypeId { get; set; }
        public int SalespersonId { get; set; }
        public DateTime? RegistryDate { get; set; }
        public DateTime? SaleDate { get; set; }

        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
