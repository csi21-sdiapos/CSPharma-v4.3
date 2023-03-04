using CSPharma_v4._1.DTOs.Models;
using CSPharma_v4._1.Tools;
using CSPharma_v4._1_DAL.Models;

namespace CSPharma_v4._1.DTOs.Converters
{
    public class DTOtoDAO
    {
        #region Conversores individuales

        public static TdcCatLineasDistribucion LineasDistribucionDTOtoDAO(LineasDistribucionDTO lineasDistribucionDTO)
        {
            TdcCatLineasDistribucion lineasDistribucion = new TdcCatLineasDistribucion();

            lineasDistribucion.Id = lineasDistribucionDTO.Id;
            lineasDistribucion.MdUuid = Guid.NewGuid().ToString();
            lineasDistribucion.MdDate = DateTime.Now;
            lineasDistribucion.CodLinea = lineasDistribucionDTO.CodLinea;
            lineasDistribucion.CodBarrio = lineasDistribucionDTO.CodBarrio;
            lineasDistribucion.CodProvincia = lineasDistribucionDTO.CodProvincia;
            lineasDistribucion.CodMunicipio = lineasDistribucionDTO.CodMunicipio;

            return lineasDistribucion;
        }
        
        public static TdcCatEstadosEnvioPedido EstadosEnvioPedidoDTOtoDAO(EstadosEnvioPedidoDTO estadosEnvioPedidoDTO)
        {
            TdcCatEstadosEnvioPedido estadosEnvioPedido = new TdcCatEstadosEnvioPedido();

            estadosEnvioPedido.Id = estadosEnvioPedidoDTO.Id;
            estadosEnvioPedido.MdUuid = Guid.NewGuid().ToString();
            estadosEnvioPedido.MdDate = DateTime.Now;
            estadosEnvioPedido.DesEstadoEnvio = estadosEnvioPedidoDTO.DesEstadoEnvio;
            estadosEnvioPedido.CodEstadoEnvio = estadosEnvioPedidoDTO.CodEstadoEnvio;

            return estadosEnvioPedido;
        }
        
        public static TdcCatEstadosPagoPedido EstadosPagoPedidoDTOtoDAO(EstadosPagoPedidoDTO estadosPagoPedidoDTO)
        {
            TdcCatEstadosPagoPedido estadosPagoPedido = new TdcCatEstadosPagoPedido();

            estadosPagoPedido.Id = estadosPagoPedidoDTO.Id;
            estadosPagoPedido.MdUuid = Guid.NewGuid().ToString();
            estadosPagoPedido.MdDate = DateTime.Now;
            estadosPagoPedido.DesEstadoPago = estadosPagoPedidoDTO.DesEstadoPago;
            estadosPagoPedido.CodEstadoPago = estadosPagoPedidoDTO.DesEstadoPago;

            return estadosPagoPedido;
        }

        public static TdcCatEstadosDevolucionPedido EstadosDevolucionPedidoDTOtoDAO(EstadosDevolucionPedidoDTO estadosDevolucionPedidoDTO)
        {
            TdcCatEstadosDevolucionPedido estadosDevolucionPedido = new TdcCatEstadosDevolucionPedido();

            estadosDevolucionPedido.Id = estadosDevolucionPedidoDTO.Id;
            estadosDevolucionPedido.MdUuid = Guid.NewGuid().ToString();
            estadosDevolucionPedido.MdDate = DateTime.Now;
            estadosDevolucionPedido.CodEstadoDevolucion = estadosDevolucionPedidoDTO.CodEstadoDevolucion;
            estadosDevolucionPedido.DesEstadoDevolucion = estadosDevolucionPedidoDTO.DesEstadoDevolucion;

            return estadosDevolucionPedido;
        }

        public static TdcTchEstadoPedidos EstadoPedidosDTOtoDAO(EstadoPedidosDTO estadoPedidosDTO)
        {
            TdcTchEstadoPedidos estadoPedidos = new TdcTchEstadoPedidos();

            estadoPedidos.Id = estadoPedidosDTO.Id;
            estadoPedidos.MdUuid = Guid.NewGuid().ToString();
            estadoPedidos.MdDate = DateTime.Now;
            estadoPedidos.CodLinea = estadoPedidosDTO.CodLinea;
            estadoPedidos.CodEstadoPago = estadoPedidosDTO.CodEstadoPago;
            estadoPedidos.CodEstadoEnvio = estadoPedidosDTO.CodEstadoEnvio;
            estadoPedidos.CodPedido = estadoPedidosDTO.CodPedido;
            estadoPedidos.CodEstadoDevolucion = estadoPedidosDTO.CodEstadoDevolucion;

            return estadoPedidos;
        }

        #endregion

        #region Conversores de listas

        public static List<TdcCatLineasDistribucion> ListaLineasDistribucionDTOtoDAO(ICollection<LineasDistribucionDTO> lineasDTO)
        {
            List<TdcCatLineasDistribucion> listaLineasDistribucion = new List<TdcCatLineasDistribucion>();

            foreach (LineasDistribucionDTO lineaDTO in lineasDTO)
            {
                listaLineasDistribucion.Add(LineasDistribucionDTOtoDAO(lineaDTO));
            }

            return listaLineasDistribucion;
        }

        public static List<TdcCatEstadosEnvioPedido> ListaEstadosEnvioPedidoDTOtoDAO(ICollection<EstadosEnvioPedidoDTO> enviosDTO)
        {
            List<TdcCatEstadosEnvioPedido> listaEstadosEnvioPedido = new List<TdcCatEstadosEnvioPedido>();

            foreach (EstadosEnvioPedidoDTO envioDTO in enviosDTO)
            {
                listaEstadosEnvioPedido.Add(EstadosEnvioPedidoDTOtoDAO(envioDTO));
            }

            return listaEstadosEnvioPedido;
        }

        public static List<TdcCatEstadosPagoPedido> ListaEstadosPagoPedidoDTOtoDAO(ICollection<EstadosPagoPedidoDTO> pagosDTO)
        {
            List<TdcCatEstadosPagoPedido> listaEstadosPagoPedido = new List<TdcCatEstadosPagoPedido>();

            foreach (EstadosPagoPedidoDTO pagoDTO in pagosDTO)
            {
                listaEstadosPagoPedido.Add(EstadosPagoPedidoDTOtoDAO(pagoDTO));
            }

            return listaEstadosPagoPedido;
        }

        public static List<TdcCatEstadosDevolucionPedido> ListaEstadosDevolucionPedidoDTOtoDAO(ICollection<EstadosDevolucionPedidoDTO> devolucionesDTO)
        {
            List<TdcCatEstadosDevolucionPedido> listaEstadosDevolucionPedido = new List<TdcCatEstadosDevolucionPedido>();

            foreach (EstadosDevolucionPedidoDTO devolucionDTO in devolucionesDTO)
            {
                listaEstadosDevolucionPedido.Add(EstadosDevolucionPedidoDTOtoDAO(devolucionDTO));
            }

            return listaEstadosDevolucionPedido;
        }

        public static List<TdcTchEstadoPedidos> ListaEstadoPedidosDTOtoDAO(ICollection<EstadoPedidosDTO> pedidosDTO)
        {
            List<TdcTchEstadoPedidos> listaEstadoPedidos = new List<TdcTchEstadoPedidos>();

            foreach (EstadoPedidosDTO pedidoDTO in pedidosDTO)
            {
                listaEstadoPedidos.Add(EstadoPedidosDTOtoDAO(pedidoDTO));
            }

            return listaEstadoPedidos;
        }

        #endregion 
    }
}