using CafeteriaHCC.Conexion;
using CafeteriaHCC.Models;
using CafeteriaHCC.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CafeteriaHCC.Controllers
{
    [ApiController]
    [Route("api")]
    public class OrdenControlador : ControllerBase
    {
        private AppDBConexion conexion;

        public OrdenControlador(AppDBConexion context)
        {
            conexion = context;
        }

        //Obtener el número de ordenes y la mesa donde se encuentran
        [HttpGet]
        [Route("obtenerOrdenes")]
        public async Task<IActionResult> obtenerOrdenesAsync()
        {
            try
            {
                var ordenesConMesas = await conexion.Ordenes
                    .Where(o => o.estatus == 1)
                    .Join(conexion.Mesas,
                          orden => orden.mesaId,
                          mesa => mesa.id,
                          (orden, mesa) => new
                          {
                              OrdenId = orden.id,
                              fecha = orden.fechaInicio,
                              Mesa = mesa.id,
                              Lugares = mesa.lugares,
                              MesaDisponible = (mesa.disponible == 1) ? "SI" : "NO"
                          })
                    .ToListAsync();

                if (ordenesConMesas == null || !ordenesConMesas.Any())
                {
                    return Respuesta.Mensaje("No Se encontraron ordenes", -1, 500);
                }
                var resultadoJson = JsonSerializer.Serialize(ordenesConMesas);
                return Respuesta.Mensaje(resultadoJson, 1, 200);
            }
            catch (Exception ex)
            {
                return Respuesta.Mensaje($"Ocurrio un error en el servidor :{ex.Message}", -1, 500);
            }
        }
        [HttpGet]
        [Route("obtenesMesasDisponibles")]
        public async Task<IActionResult> mesasDisponibles()
        {
            try
            {
                var mesas = await conexion.Mesas
                    .Where(m => m.estatus == 1 && m.disponible == 1)
                          .Select(m => new
                          {
                              MesaId = m.id,
                              Lugares= m.lugares,
                              MesaDisponible = (m.disponible==1)?"SI":"NO"
                          })
                    .ToListAsync();

                if (mesas == null)
                {
                    return Respuesta.Mensaje("No Se encontraron mesas", -1, 500);
                } else if (mesas.Count() == 0)
                {
                    return Respuesta.Mensaje("Por el momento no hay mesas disponibles", -1, 200);
                }
                var resultadoJson = JsonSerializer.Serialize(mesas);
                return Respuesta.Mensaje(resultadoJson, 1, 200);
            }
            catch (Exception ex)
            {
                return Respuesta.Mensaje($"Ocurrio un error en el servidor :{ex.Message}", -1, 500);
            }
        }

        [HttpPost]
        [Route("crearNuevaOrden")]
        public async Task<IActionResult> nuevaOrden(int mesaId,string nombre , DateTime fechaInicio, List<Detalles> detalle)
        {
            try
            {
                var nuevoCat = new CatEstatusOrden
                {
                    estatus = 1,//Empieza con estatus 1
                    nombre = nombre
                };

                conexion.CatEstatusOrdenes.Add(nuevoCat);
                await conexion.SaveChangesAsync();

                // Obtener el ID de la nueva orden creada
                int nuevoCatEstatusOr = nuevoCat.id;

                var nuevaOrden = new Orden
                {
                    mesaId = mesaId,
                    catOrdId = nuevoCatEstatusOr,
                    fechaInicio = fechaInicio,
                    estatus = 1 //al inicio simpre sera 1
                };

                // Agregar la nueva orden a la base de datos
                conexion.Ordenes.Add(nuevaOrden);
                await conexion.SaveChangesAsync();

                // Obtener el ID de la nueva orden creada
                int nuevaOrdenId = nuevaOrden.id;

                // Crear los detalles de la orden
                foreach (var item in detalle)
                {
                    var nuevoDetalle = new OrdenDetalle
                    {
                        ordenId = nuevaOrdenId,
                        proId = item.idProducto,
                        cantidad = item.cantidad,
                        estatus = 1 // se inicia con estatus 1
                    };

                    // Agregar cada detalle a la base de datos
                    conexion.OrdenDetalles.Add(nuevoDetalle);
                }

                // Guardar los cambios de los detalles de la orden en la base de datos
                await conexion.SaveChangesAsync();

                return Respuesta.Mensaje("Orden creada exitosamente", nuevaOrdenId, 200);
            }
            catch (Exception ex)
            {
                return Respuesta.Mensaje($"Ocurrio un error en el servidor :{ex.Message}", -1, 500);
            }
        }

        [HttpDelete]
        [Route("eliminarOrden")]
        public async Task<IActionResult> eliminarOrden(int ordenId)
        {
            try
            {
                var orden = await conexion.Ordenes.FirstOrDefaultAsync(o => o.id == ordenId);

                if (orden == null)
                {
                    return Respuesta.Mensaje("Orden no encontrada", -1, 500);
                }

                // Marcar la orden como inactiva (borrado lógico)
                orden.estatus = 0;
                conexion.Ordenes.Update(orden);

                // Buscar y marcar como inactivos los detalles asociados a la orden
                var detalles = await conexion.OrdenDetalles.Where(d => d.ordenId == ordenId).ToListAsync();
                foreach (var detalle in detalles)
                {
                    detalle.estatus = 0;
                    conexion.OrdenDetalles.Update(detalle);
                }

                await conexion.SaveChangesAsync();

                return Respuesta.Mensaje("Orden eliminada exitosamente", 1, 200);
            }
            catch (Exception ex)
            {
                return Respuesta.Mensaje($"Ocurrió un error en el servidor: {ex.Message}", -1, 500);
            }
        }

        [HttpPut]
        [Route("actualizarOrden")]
        public async Task<IActionResult> actualizarOrden(int ordenId,List<Detalles> ordenDetalles)
        {
            try
            {
                var detalles = await conexion.OrdenDetalles.Where(d => d.ordenId == ordenId).ToListAsync();
                // Crear los detalles de la orden
                foreach (var item in ordenDetalles)
                {
                    var nuevoDetalle = new OrdenDetalle
                    {
                        ordenId = ordenId,
                        proId = item.idProducto,
                        cantidad = item.cantidad,
                        estatus = 1 // se inicia con estatus 1
                    };

                    // Agregar cada detalle a la base de datos
                    conexion.OrdenDetalles.Add(nuevoDetalle);
                }

                await conexion.SaveChangesAsync();

                return Respuesta.Mensaje("Orden actualizada exitosamente", 1, 200);

            }
            catch(Exception ex)
            {
                return Respuesta.Mensaje($"Ocurrió un error en el servidor: {ex.Message}", -1, 500);
            }
        }
    }
}
