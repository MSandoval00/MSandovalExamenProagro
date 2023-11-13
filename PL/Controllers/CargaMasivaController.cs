using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        private readonly IConfiguration _configuration;

        public CargaMasivaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Cargar()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            return View(result);
        }
        [HttpPost]
        public IActionResult Cargar(ML.Result result)
        {
            var file = Request.Form.Files["Excel"];

            if (HttpContext.Session.GetString("pathExcel") == null)
            {
                if (file != null)
                {

                    string extensionArchivo = Path.GetExtension(file.FileName).ToLower();
                    string extesionValida = _configuration.GetSection("AppSettings")["TipoExcel"];//Inyección de dependencias

                    if (extensionArchivo == extesionValida)
                    {
                        var rutaproyecto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CargaMasiva");
                        var filePath = Path.Combine(rutaproyecto, $"{Path.GetFileNameWithoutExtension(file.FileName)}-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");

                        if (!System.IO.File.Exists(filePath))
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            var connectionString = _configuration.GetConnectionString("OleDbConnection") + filePath;
                            ML.Result resultUsuarios = BL.Usuario.LeerExcel(connectionString);
       

                            if (resultUsuarios.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultUsuarios.Objects);

                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("pathExcel", filePath);
                                }

                                return View(resultValidacion);
                            }
                            else
                            {
                                ViewBag.Message = "El excel no contiene registros";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Favor de seleccionar un archivo .xlsx, Verifique su archivo";
                    }
                }
                else
                {
                    ViewBag.Message = "No selecciono ningun archivo, Seleccione uno correctamente";
                }
                return View(result);
            }
            else
            {
                string filepath = HttpContext.Session.GetString("pathExcel");

                if (filepath != null)
                {
                    var connectionString = _configuration.GetConnectionString("OleDbConnection") + filepath;
                   
                    ML.Result resultUsuarios = BL.Usuario.LeerExcel(connectionString);

                    if (resultUsuarios.Correct)
                    {
                        ML.Result resultErrores = new ML.Result();//Instanciar antes de entrar al flujo 
                        resultErrores.Objects = new List<object>();

                        foreach (ML.Usuario usuario in resultUsuarios.Objects)
                        {
                            ML.Result result1 = BL.Usuario.AddCargaMasiva(usuario);
                            if (!result1.Correct)
                            {
                                string error = "Ocurrio un problema al insertar los datos: " + usuario.RFC + " con este error" + resultErrores.ErrorMessage;
                                resultErrores.Objects.Add(error);
                            }
                            HttpContext.Session.Remove("pathExcel");
                        }
                        if (resultErrores.Objects.Count > 0)
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "logErrores");
                            var filePath = Path.Combine(path, $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt");

                            //string path = Server.MapPath(@"~\Files\logErrores");
                            //string filePath = path + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

                            using (StreamWriter writer = new StreamWriter(filePath))
                            {
                                foreach (string linea in resultErrores.Objects)
                                {

                                    writer.WriteLine(linea);
                                }
                            }
                        }
                    }
                }
            }
            return View(result);
        }
    }
}