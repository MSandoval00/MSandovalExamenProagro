using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        public IActionResult Cargar()
        {
            ML.Result result=new ML.Result();
            result.Objects=new List<object>();
            return View(result);
        }
        [HttpPost]
        public IActionResult Cargar(ML.Result result)
        {
            HttpPostedFileBase file = Request.Files["Excel"];
            if (SessionExtensions["pathExcel"]==null)
            {
                if (file!=null)
                {
                    string extensionArchivo = Path.GetExtension(file.FileName).toLower();
                    string extensionValida = ConfigurationManager.AppSettings["TipoExcel"];
                   
                    if (extensionArchivo == extensionValida)
                    {
                        string rutaproyecto = Server.MapPath("~/CargaMasiva/");
                        string filePath = rutaproyecto + Path.GetFileNameWithoutExtension(file.FileName) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        if (!System.IO.File.Exists(filePath))
                        {
                            file.SaveAS(filePath);
                            string connectionString=ConfigurationManager
                        }
                    }
                }
            }
        }
    }
}
