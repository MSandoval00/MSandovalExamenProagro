using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Permiso
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context = new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Permisos.FromSqlRaw($"PermisosGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            ML.Permiso permisos = new ML.Permiso();
                            permisos.Usuario = new ML.Usuario();
                            permisos.Estado = new ML.Estado();
                            permisos.Usuario.IdUsuario = int.Parse(row.IdUsuario.ToString());
                            permisos.Estado.IdEstado = int.Parse(row.IdEstado.ToString());
                            result.Objects.Add(permisos);
                            result.Correct = true;
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros de permisos";
                    }
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
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context = new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Permisos.FromSqlRaw($"PermisosGetById {IdUsuario}").AsEnumerable().Single();
                    result.Object = new List<object>();
                    if (query != null)
                    {
                        ML.Permiso permisos = new ML.Permiso();
                        permisos.Usuario = new ML.Usuario();
                        permisos.Estado = new ML.Estado();
                        permisos.Usuario.IdUsuario = int.Parse(query.IdUsuario.ToString());
                        permisos.Estado.IdEstado = int.Parse(query.IdEstado.ToString());
                        result.Object = permisos;
                        result.Correct = true;
                    }
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
        public static ML.Result Add(ML.Permiso permisos)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context = new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"PermisosAdd {permisos.Usuario.IdUsuario}, {permisos.Estado.IdEstado}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo insertar los permisos";
                    }
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
        public static ML.Result Update(ML.Permiso permisos)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context = new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"PermisosUpdate {permisos.Usuario.IdUsuario},{permisos.Estado.IdEstado}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el permiso";
                    }
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
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context = new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"PermisosDelete {IdUsuario}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el permiso";
                    }
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
