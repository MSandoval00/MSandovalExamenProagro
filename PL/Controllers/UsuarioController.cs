using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Result result=BL.Usuario.GetAll();
            ML.Usuario usuario=new ML.Usuario();
            usuario.Usuarios = new List<object>();
            if (result.Correct)
            {
                result.Objects = usuario.Usuarios;
            }
            else
            {
                ViewBag.Mensaje = result.ErrorMessage;
            }
            return View(usuario);
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario=new ML.Usuario();
            if (IdUsuario!=null)//Update
            {
                ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
                if (result.Correct)
                {
                    usuario=(ML.Usuario)result.Object;
                }
            }
            else//Add
            {

            }
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (usuario.IdUsuario==0)//Add
            {
                ML.Result result=BL.Usuario.Add(usuario);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se registro correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo registar correctamente";
                }
            }
            else
            {
                ML.Result result = BL.Usuario.Update(usuario);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se actualizo correctamente el usuario";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo actualizar el usuario";
                }
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.Delete(IdUsuario);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se elimino el usuario correctamente";
            }
            else
            {
                ViewBag.Mensaje = "No se pudo eliminar el usuario";
            }
            return PartialView("Modal");
        }
    }
}
