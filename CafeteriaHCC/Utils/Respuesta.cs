using CafeteriaHCC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CafeteriaHCC.Utils
{
    public static class Respuesta
    {
        public static IActionResult Mensaje(string mensaje, int codigo, int codigoEstatus)
        {
            var response = new RespuestaApi
            {
                Estatus = codigoEstatus,
                Mensaje = mensaje,
                Codigo = codigo
            };
            return new JsonResult(response) { StatusCode = codigoEstatus };
        }
    }
}
