using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takip.Model.Views
{
    [Table("V_AdminAddresses")]
    public class V_AdminAddresses
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string AdresAdi { get; set; }
        public string AdresAciklamasi { get; set; }
        public string SehirAdi { get; set; }
        public int SehirId { get; set; }
        public string IlceAdi { get; set; }
        public int IlceId { get; set; }
        public string UlkeAdi { get; set; }
        public int UlkeId { get; set; }
        public string PostaKodu { get; set; }
    }
}
