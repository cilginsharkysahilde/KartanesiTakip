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
    public class AddressRepository : RepositoryBase<Address>
    {
        public AddressRepository(RepositoryContext context) : base(context) { }

        public List<V_AdminAddresses> GetAdminAddressesById(int id)
        {
            return RepositoryContext.AdminAddresses.Where(a => a.CustomerId == id).ToList<V_AdminAddresses>();
        }

        public void DeleteAddress(int addressId)
        {
            RepositoryContext.Addresses.Where(a => a.Id == addressId).ExecuteDelete();
        }

        public List<City> GetCities()
        {
            return RepositoryContext.Cities.ToList<City>();
        }

        public List<Country> GetCountries()
        {
            return RepositoryContext.Countries.ToList<Country>();
        }

        public List<District> GetDistricts()
        {
            return RepositoryContext.Districts.ToList<District>();
        }
    }
}
