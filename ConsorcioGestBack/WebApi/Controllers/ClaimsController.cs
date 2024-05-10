using DataAccess.Data;
using DataAccess.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("claims")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        public readonly ConsorcioGestContext consorcioGestContext;

        public ClaimsController(ConsorcioGestContext consorcioGestContext)
        {
            this.consorcioGestContext = consorcioGestContext;
        }

        [HttpPost("guardar-imagen")]
        public async Task<IActionResult> GuardarImagen([FromBody] byte[] imagenBytes)
        {
            Imagene imagene = new Imagene();
            imagene.Nombre = "Prueba";
            imagene.DatosImagen = imagenBytes;
            consorcioGestContext.Add(imagene);
            consorcioGestContext.SaveChanges();

            return Ok(new { mensaje = "Imagen guardada exitosamente" });
        }

        [HttpGet("obtener-imagen")]
        public async Task<IActionResult> ObtenerImagen()
        {
            // Aquí recuperas los bytes de la imagen desde la base de datos
            byte[] imagenBytes = consorcioGestContext.Imagenes.Where(i => i.Id == 1).Select(i => i.DatosImagen).FirstOrDefault();

            // Devuelve los bytes de la imagen
            return Ok(imagenBytes);
        }
    }   
}
