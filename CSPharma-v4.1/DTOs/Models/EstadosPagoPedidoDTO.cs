using CSPharma_v4._1_DAL.Models;

namespace CSPharma_v4._1.DTOs.Models
{
    public class EstadosPagoPedidoDTO
    {
        public string CodEstadoPago { get; set; } = null!;

        public string MdUuid { get; set; } = null!;

        public DateTime MdDate { get; set; }

        public int Id { get; set; }

        public string? DesEstadoPago { get; set; }

        public virtual ICollection<TdcTchEstadoPedidos>? TdcTchEstadoPedidos { get; } = new List<TdcTchEstadoPedidos>();
    }
}
