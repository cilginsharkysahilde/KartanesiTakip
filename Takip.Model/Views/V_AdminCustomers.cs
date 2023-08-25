using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takip.Model.Views
{
    [Table("V_AdminCustomers")]
    public class V_AdminCustomers
    {
        public int MusteriId { get; set; }
        public string MusteriAdi { get; set; }
        public string Telefon { get; set; }
        public string? EvTelefon { get; set; }
        public int ProductTypeId { get; set; }
        public string UrunAdi { get; set; }
        public int SalespersonId { get; set; }
        public string SatisPersoneli { get; set; }
        public DateTime? KayitTarih { get; set; }
        public DateTime? SatisTarih { get; set; }
    }
}
