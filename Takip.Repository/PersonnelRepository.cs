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
    public class PersonnelRepository : RepositoryBase<Personnel>
    {
        public PersonnelRepository(RepositoryContext context) : base(context) { }

        public List<V_ActivePersonnels> GetActivePersonnels()
        {
            return RepositoryContext.ActivePersonnels.ToList<V_ActivePersonnels>();
        }

        public List<V_AdminPersonnelDetail> GetPersonnelDetails()
        {
            return RepositoryContext.V_AdminPersonnelDetails.ToList<V_AdminPersonnelDetail>();
        }

        public List<V_AdminPersonnelDetail> GetPersonnelDetailsById(int personnelId)
        {
            return RepositoryContext.V_AdminPersonnelDetails.Where(a => a.Id == personnelId).ToList<V_AdminPersonnelDetail>();
        }

        public List<Role> GetRoles()
        {
            return RepositoryContext.Roles.ToList<Role>();
        }

        public List<MaritalStatus> GetMaritalStatuses()
        {
            return RepositoryContext.MaritalStatuses.ToList<MaritalStatus>();
        }

        public List<EducationalBackground> GetEducations()
        {
            return RepositoryContext.EducationalBackgrounds.ToList<EducationalBackground>();
        }

        public void DeletePersonnel(int personnelId)
        {
            RepositoryContext.Personnel.Where(a => a.Id == personnelId).ExecuteDelete();
        }


    }
}
