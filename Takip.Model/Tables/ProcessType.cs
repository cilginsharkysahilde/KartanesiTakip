using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takip.Model.Tables
{
    [Table("tblProcessType")]
    public class ProcessType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
