using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takip.Model.Tables
{
    [Table("tblPersonnel")]
    public class Personnel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public string TcNumber { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? EducationalBackgroundId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? QuitDate { get; set; }
        public decimal? Salary { get; set; }
        public bool Active { get; set; }
    }
}
