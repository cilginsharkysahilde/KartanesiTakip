using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takip.Model.Views
{
    [Table("V_AdminCategories")]
    public class V_AdminCategories
    {
        public int Id { get; set; }
        public string KategoriAdi { get; set; }
        public int? UstKategoriId { get; set; }
        public string? UstKategoriAdi { get; set; }
        public bool Aktif { get; set; }
    }
}
