using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class GeoReferenciaController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Result result=BL.GeoReferencias.GetAll();
            ML.GeoReferencias geoReferencias=new ML.GeoReferencias();
            geoReferencias.GeoRefereciass = new List<object>();
            if (result.Correct)
            {
                geoReferencias.GeoRefereciass = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = result.ErrorMessage;
            }
            return View(geoReferencias);
        }
        [HttpGet]
        public ActionResult Form(int? IdGeorreferencia)
        {
            ML.GeoReferencias geoReferencias = new ML.GeoReferencias();
            geoReferencias.GeoRefereciass= new List<object>();
            ML.Result resultEstados = BL.Estado.GetAll();
            if (IdGeorreferencia!=null)//Update
            {
                ML.Result result = BL.GeoReferencias.GetById(IdGeorreferencia.Value);
                if (result.Correct)
                {
                    geoReferencias = (ML.GeoReferencias)result.Object;
                    geoReferencias.GeoRefereciass = resultEstados.Objects;
                }
            }
            else//add
            {
                geoReferencias.GeoRefereciass = resultEstados.Objects;
            }
            return View(geoReferencias);
        }
        [HttpPost]
        public ActionResult Form(ML.GeoReferencias geoReferencias)
        {
            if (geoReferencias.IdGeorreferencia==0)//add
            {
                ML.Result result = BL.GeoReferencias.Add(geoReferencias);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se registro correctamente la georeferencia"; 
                }
                else
                {
                    ViewBag.Mensaje = "No se registro correctamente la georeferencia";
                }
            }
            else//Update
            {
                ML.Result result = BL.GeoReferencias.Update(geoReferencias);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se actualizo correctamente la georeferencia";
                }
                else
                {
                    ViewBag.Mensaje = "No se actualizo correctamente la georeferencia";
                }
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Delete(int IdGeorreferencia)
        {
            ML.Result result = BL.GeoReferencias.Delete(IdGeorreferencia);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se elimino correctamente la georeferencias";
            }
            else
            {
                ViewBag.Mensaje = "No se elimino correctamente la georeferencias";
            }
            return PartialView("Modal");
        }
    }
}
