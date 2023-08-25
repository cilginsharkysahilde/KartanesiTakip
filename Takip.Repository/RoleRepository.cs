using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takip.Model.Tables;

namespace Takip.Repository
{
    public class RoleRepository : RepositoryBase<Role>
    {
        public RoleRepository(RepositoryContext context) : base(context) { } 
        
        public void DeleteRole(int roleId)
        {
            RepositoryContext.Roles.Where(r => r.Id == roleId).ExecuteDelete();
        }
    }

   
}
