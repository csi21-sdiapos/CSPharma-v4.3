using CSPharma_v4._1_DAL.Models;

namespace CSPharma_v4._1.DTOs.Models
{
    public class LineasDistribucionDTO
    {
        public string CodLinea { get; set; } = null!;

        public string MdUuid { get; set; } = null!;

        public DateTime MdDate { get; set; }

        public int Id { get; set; }

        public string CodProvincia { get; set; } = null!;

        public string CodMunicipio { get; set; } = null!;

        public string CodBarrio { get; set; } = null!;

        public virtual ICollection<TdcTchEstadoPedidos>? TdcTchEstadoPedidos { get; } = new List<TdcTchEstadoPedidos>();
    }
}
