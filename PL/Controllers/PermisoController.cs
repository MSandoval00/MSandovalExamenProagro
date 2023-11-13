using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PermisoController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Permiso.GetAll();
            ML.Permiso permiso = new ML.Permiso();
            permiso.Permisos = new List<object>();
            if (result.Correct)
            {
                permiso.Permisos = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = result.ErrorMessage;
            }
            return View(permiso);
        }
        [HttpGet]
        public IActionResult Form(int? IdUsusario)
        {
            ML.Permiso permiso = new ML.Permiso();
            permiso.Permisos = new List<object>();
            ML.Result resultEstado = BL.Estado.GetAll();
            if (IdUsusario != null)//Update
            {
                ML.Result result = BL.Usuario.GetById(IdUsusario.Value);
                if (result.Correct)
                {
                    permiso = (ML.Permiso)result.Object;
                    permiso.Permisos = resultEstado.Objects;
                }
            }
            else//Add
            {
                permiso.Permisos = resultEstado.Objects;
            }
            return View(permiso);
        }
        [HttpPost]
        public IActionResult Form(ML.Permiso permiso)
        {
            permiso.Usuario = new ML.Usuario();
            if (permiso.Usuario.IdUsuario == 0)
            {
                ML.Result result = BL.Permiso.Add(permiso);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se registro correctamente el permiso";
                }
                else
                {
                    ViewBag.Mensaje = "No se registro correctamente el permiso";
                }
            }
            else
            {
                ML.Result result = BL.Permiso.Update(permiso);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se actualizo correctamente el permiso";
                }
                else
                {
                    ViewBag.Mensaje = "No se actualizo correctamente el permiso";
                }
            }
            return PartialView("Modal");
        }
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Permiso.Delete(IdUsuario);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se elimino correctamente el permiso";
            }
            else
            {
                ViewBag.Mensaje = "No se elimino correctamente el permiso";
            }
            return PartialView("Modal");
        }
    }
}
