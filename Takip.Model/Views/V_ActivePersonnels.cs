using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takip.Model.Views
{
    [Table("V_ActivePersonnels")]
    public class V_ActivePersonnels
    {
        public int Id { get; set; }
        public string PersonelAdi { get; set; }
        public string Rol { get; set; }
        public DateTime BaslamaTarihi { get; set; }

    }
}
