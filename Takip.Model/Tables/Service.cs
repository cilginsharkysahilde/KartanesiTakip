using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takip.Model.Tables
{
    [Table("tblService")]
    public class Service
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProcessTypeId { get; set; }
        public int ProductId { get; set; }
        public DateTime? DueDate { get; set; }
        public string? ItemDescription { get; set; }
        public string? CustomerComplaint { get; set; }
        public string? WaterValues { get; set; }
        public int ServicePersonnelId { get; set; }
        public bool? IsMaintenanceDone { get; set; }
        public int? MaintenancePeriodId { get; set; }
        public int? MaintenanceMonthId { get; set; }
        public int? MaintenanceYearId { get; set; }
        public string? SpecialNote { get; set; }

    }
}
