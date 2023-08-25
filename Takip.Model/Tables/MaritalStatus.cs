using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takip.Model.Tables
{
    [Table("tblMaritalStatus")]
    public class MaritalStatus
    {
        public MaritalStatus()
        {
            personnel = new HashSet<Personnel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Personnel> personnel { get; set; }
    }
}
