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
    public class ServiceRepository : RepositoryBase<Service>
    {
        public ServiceRepository(RepositoryContext context) : base(context) { }

        public List<V_AdminServices> AdminServicesById(int id)
        {
            return RepositoryContext.V_AdminServices.Where(a => a.CustomerId == id).ToList<V_AdminServices>();
        }

        public List<V_AdminServices> AdminServices()
        {
            return RepositoryContext.V_AdminServices.ToList<V_AdminServices>();
        }

        public List<ProcessType> GetProcessTypes()
        {
            return RepositoryContext.ProcessTypes.ToList<ProcessType>();
        }


        public void DeleteService(int serviceId)
        {
            RepositoryContext.Services.Where(a => a.Id == serviceId).ExecuteDelete();
        }

    }
}
