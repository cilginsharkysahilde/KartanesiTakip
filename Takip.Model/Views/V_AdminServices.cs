using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takip.Model.Views
{
    [Table("V_AdminServices")]
    public class V_AdminServices
    {
        public int ServiceId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ProcessTypeId { get; set; }
        public string ProcessType { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime? DueDate { get; set; }
        public string? ItemDescription { get; set; }
        public string? CustomerComplaint { get; set; }
        public string? WaterValues { get; set; }
        public int ServicePersonnelId { get; set; }
        public string ServicePersonnelName { get; set; }
        public bool? IsMaintenanceDone { get; set; }
        public int? MaintenanceMonthId { get; set; }
        public string? MaintenanceMonthName { get; set; }
        public int? MaintenancePeriodId { get; set; }
        public string? MaintenancePeriodName { get; set; }
        public int? MaintenanceYearId { get; set; }
        public string? MaintenanceYearName { get; set; }
        public string? SpecialNote { get; set; }
    }
}
