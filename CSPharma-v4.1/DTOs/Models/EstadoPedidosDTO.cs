using CSPharma_v4._1_DAL.Models;

namespace CSPharma_v4._1.DTOs.Models
{
    public class EstadoPedidosDTO
    {
        public int Id { get; set; }

        public string MdUuid { get; set; } = null!;

        public DateTime MdDate { get; set; }

        public string CodPedido { get; set; } = null!;

        public string? CodEstadoEnvio { get; set; }

        public string? CodEstadoPago { get; set; }

        public string? CodEstadoDevolucion { get; set; }

        public string? CodLinea { get; set; }

        public virtual TdcCatEstadosDevolucionPedido? CodEstadoDevolucionNavigation { get; set; }

        public virtual TdcCatEstadosEnvioPedido? CodEstadoEnvioNavigation { get; set; }

        public virtual TdcCatEstadosPagoPedido? CodEstadoPagoNavigation { get; set; }

        public virtual TdcCatLineasDistribucion? CodLineaNavigation { get; set; }
    }
}
