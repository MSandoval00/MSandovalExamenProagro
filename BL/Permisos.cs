using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Permisos
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context=new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Permisos.FromSqlRaw($"PermisosGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query!=null)
                    {
                        foreach (var row in query)
                        {
                            ML.Permisos permisos = new ML.Permisos();
                            permisos.Usuario = new ML.Usuario();
                            permisos.Estado=new ML.Estado();
                            permisos.Usuario.IdUsuario = query.IdUsuario;
                            permisos.Estado.IdEstado = query.IdEstado;
                            result.Objects.Add(permisos);
                            result.Correct = true;
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "No hay registros de permisos";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex=ex;
            }
            return result;
        }
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context=new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Permisos.FromSqlRaw($"PermisosGetById {IdUsuario}").AsEnumerable().Single();
                    result.Object=new List<object>();
                    if (query!=null)
                    {
                        ML.Permisos permisos = new ML.Permisos();
                        permisos.Usuario=new ML.Usuario();
                        permisos.Estado=new ML.Estado();
                        permisos.Usuario.IdUsuario = query.IdUsuario;
                        permisos.Estado.IdEstado = query.IdEstado;
                        result.Object=permisos;
                        result.Correct=true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct=false;
                result.ErrorMessage = ex.Message;
                result.Ex=ex;
            }
            return result;
        }
        public static ML.Result Add(ML.Permisos permisos)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context=new DL.MsandovalExamenProagroContext())
                {
                    var query=context.Database.ExecuteSqlRaw($"")
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
