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
    public class CustomerRepository : RepositoryBase<Customer>
    {
        public CustomerRepository(RepositoryContext context) : base(context) { }

        //public Customer CustomerById(int id)
        //{
        //    Customer customer = (from u in RepositoryContext.Customers.Include(a => a.Addresses)
        //                 where u.Id == id
        //                 select u).SingleOrDefault<Customer>();
        //    return customer;
        //}

        public List<V_AdminCustomers> AdminCustomers()
        {
            return RepositoryContext.AdminCustomers.ToList<V_AdminCustomers>();
        }

        public List<V_Personnel> GetEditPersonnels()
        {
            return RepositoryContext.V_Personnels.ToList<V_Personnel>();
        }

        public List<V_Customer> GetEditCustomers()
        {
            return RepositoryContext.V_Customers.ToList<V_Customer>();
        }


        public List<V_Product> GetEditProducts()
        {
            return RepositoryContext.V_Products.ToList<V_Product>();
        }

        public void DeleteCustomer(int customerId)
        {
            RepositoryContext.Customers.Where(c => c.Id == customerId).ExecuteDelete();
        }
        
        
    }
}
