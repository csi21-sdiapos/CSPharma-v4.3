using CSPharma_v4._1.DTOs.Models;
using CSPharma_v4._1.Tools;
using CSPharma_v4._1_DAL.Models;

namespace CSPharma_v4._1.DTOs.Converters
{
    public class DAOtoDTO
    {
        #region Conversores individuales

        public static LineasDistribucionDTO LineasDistribucionToDTO(TdcCatLineasDistribucion lineasDistribucion)
        {
            LineasDistribucionDTO lineasDistribucionDTO = new LineasDistribucionDTO();

            lineasDistribucionDTO.Id = lineasDistribucion.Id;
            lineasDistribucionDTO.CodBarrio = lineasDistribucion.CodBarrio;
            lineasDistribucionDTO.CodLinea = lineasDistribucion.CodLinea;
            lineasDistribucionDTO.CodMunicipio = lineasDistribucion.CodMunicipio;
            lineasDistribucionDTO.CodProvincia = lineasDistribucion.CodProvincia;

            return lineasDistribucionDTO;
        }

        public static EstadosEnvioPedidoDTO EstadosEnvioPedidoToDTO(TdcCatEstadosEnvioPedido estadosEnvioPedido)
        {
            EstadosEnvioPedidoDTO estadosEnvioPedidoDTO = new EstadosEnvioPedidoDTO();

            estadosEnvioPedidoDTO.Id = estadosEnvioPedido.Id;
            estadosEnvioPedidoDTO.CodEstadoEnvio = estadosEnvioPedido.CodEstadoEnvio;
            estadosEnvioPedidoDTO.DesEstadoEnvio = estadosEnvioPedido.CodEstadoEnvio;

            return estadosEnvioPedidoDTO;
        }

        public static EstadosPagoPedidoDTO EstadosPagoPedidoToDTO(TdcCatEstadosPagoPedido estadosPagoPedido)
        {
            EstadosPagoPedidoDTO estadosPagoPedidoDTO = new EstadosPagoPedidoDTO();

            estadosPagoPedidoDTO.Id = estadosPagoPedido.Id;
            estadosPagoPedidoDTO.CodEstadoPago = estadosPagoPedido.CodEstadoPago;
            estadosPagoPedidoDTO.DesEstadoPago = estadosPagoPedido.DesEstadoPago;

            return estadosPagoPedidoDTO;
        }

        public static EstadosDevolucionPedidoDTO EstadosDevolucionPedidoToDTO(TdcCatEstadosDevolucionPedido estadosDevolucionPedido)
        {
            EstadosDevolucionPedidoDTO estadosDevolucionPedidoDTO = new EstadosDevolucionPedidoDTO();

            estadosDevolucionPedidoDTO.Id = estadosDevolucionPedido.Id;
            estadosDevolucionPedidoDTO.CodEstadoDevolucion = estadosDevolucionPedido.CodEstadoDevolucion;
            estadosDevolucionPedidoDTO.DesEstadoDevolucion = estadosDevolucionPedido.DesEstadoDevolucion;

            return estadosDevolucionPedidoDTO;
        }

        public static EstadoPedidosDTO EstadoPedidosToDTO(TdcTchEstadoPedidos estadoPedidos)
        {
            EstadoPedidosDTO estadoPedidosDTO = new EstadoPedidosDTO();

            estadoPedidosDTO.Id = estadoPedidos.Id;
            estadoPedidosDTO.CodPedido = estadoPedidos.CodPedido;
            estadoPedidosDTO.CodEstadoPago = estadoPedidos.CodEstadoPago;
            estadoPedidosDTO.CodEstadoEnvio = estadoPedidos.CodEstadoEnvio;
            estadoPedidosDTO.CodLinea = estadoPedidos.CodLinea;
            estadoPedidosDTO.CodEstadoDevolucion = estadoPedidos.CodEstadoDevolucion;

            return estadoPedidosDTO;
        }

        #endregion



        #region Conversores de listas

        public static List<LineasDistribucionDTO> ListaLineasDistribucionToDTO(ICollection<TdcCatLineasDistribucion> lineas)
        {
            List<LineasDistribucionDTO> lineasDistribucionDTO = new List<LineasDistribucionDTO>();

            foreach (TdcCatLineasDistribucion linea in lineas)
                lineasDistribucionDTO.Add(LineasDistribucionToDTO(linea));

            return lineasDistribucionDTO;
        }

        public static List<EstadosEnvioPedidoDTO> ListaEstadosEnvioPedidoToDTO(ICollection<TdcCatEstadosEnvioPedido> envios)
        {
            List<EstadosEnvioPedidoDTO> estadosEnvioPedidoDTO = new List<EstadosEnvioPedidoDTO>();

            foreach (TdcCatEstadosEnvioPedido envio in envios)
                estadosEnvioPedidoDTO.Add(EstadosEnvioPedidoToDTO(envio));

            return estadosEnvioPedidoDTO;
        }

        public static List<EstadosPagoPedidoDTO> ListaEstadosPagoPedidoToDTO(ICollection<TdcCatEstadosPagoPedido> pagos)
        {
            List<EstadosPagoPedidoDTO> estadosPagoPedidoDTO = new List<EstadosPagoPedidoDTO>();

            foreach (TdcCatEstadosPagoPedido pago in pagos)
                estadosPagoPedidoDTO.Add(EstadosPagoPedidoToDTO(pago));

            return estadosPagoPedidoDTO;
        }

        public static List<EstadosDevolucionPedidoDTO> ListaEstadosDevolucionPedidoToDTO(ICollection<TdcCatEstadosDevolucionPedido> devoluciones)
        {
            List<EstadosDevolucionPedidoDTO> listaEstadosDevolucionPedidoDTO = new List<EstadosDevolucionPedidoDTO>();

            foreach (TdcCatEstadosDevolucionPedido devolucion in devoluciones)
                listaEstadosDevolucionPedidoDTO.Add(EstadosDevolucionPedidoToDTO(devolucion));

            return listaEstadosDevolucionPedidoDTO;

        }

        public static List<EstadoPedidosDTO> ListEstadoPedidoToDto(ICollection<TdcTchEstadoPedidos> pedidos)
        {
            List<EstadoPedidosDTO> estadoPedidosDTO = new List<EstadoPedidosDTO>();

            foreach (TdcTchEstadoPedidos estadoPedido in pedidos)
                estadoPedidosDTO.Add(EstadoPedidosToDTO(estadoPedido));

            return estadoPedidosDTO;
        }

        #endregion
    }
}
