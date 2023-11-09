using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoReferenciasController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public IActionResult Add(ML.GeoReferencias geoReferencias)
        {
            ML.Result result = BL.GeoReferencias.Add(geoReferencias);
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
        [Route("{IdGeorreferencia}")]
        public IActionResult Update(ML.GeoReferencias geoReferencias)
        {
            ML.Result result = BL.GeoReferencias.Update(geoReferencias);
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
        [Route("IdGeorreferencia")]
        public IActionResult Delete(int IdGeorreferencia)
        {
            ML.Result result = BL.GeoReferencias.Delete(IdGeorreferencia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("IdGeorreferencia")]
        public IActionResult GetById(int IdGeorreferencia)
        {
            ML.Result result = BL.GeoReferencias.GetById(IdGeorreferencia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.GeoReferencias.GetAll();
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
