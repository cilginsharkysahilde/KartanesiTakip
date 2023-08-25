using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takip.Model.Views
{
    [Table("V_AdminPersonnelDetail")]
    public class V_AdminPersonnelDetail
    {
        public int Id { get; set; }
        public string PersonnelName { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string TcNumber { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int? MaritalStatusId { get; set; }
        public string? MaritalStatus { get; set; }
        public int? EducationId { get; set; }
        public string? Education { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? QuitDate { get; set; }
        public decimal? Salary { get; set; }
        public bool Active { get; set; }
    }
}
