using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisosController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public IActionResult Add(ML.Permiso permisos)
        {
            ML.Result result = BL.Permiso.Add(permisos);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("{IdUsuario}")]
        public IActionResult Update(ML.Permiso permisos)
        {
            ML.Result result = BL.Permiso.Update(permisos);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("{IdUsuario}")]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Permiso.Delete(IdUsuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        //Pendientes GETALL y GETBYID
        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Permiso.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
