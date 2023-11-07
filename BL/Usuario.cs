using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
       public static ML.Result GetAll()
        {
            ML.Result result=new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context=new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query!=null)
                    {
                        foreach (var row in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = row.IdUsuario;
                            usuario.Password = row.Password;
                            usuario.Nombre = row.Nombre;
                            usuario.FechaNacimiento = row.FechaNacimiento;
                            usuario.RFC = row.Rfc;
                            result.Objects.Add(usuario);
                            result.Correct = true;
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros de usuarios";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct=false;
                result.ErrorMessage=ex.Message;
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
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().Single();
                    result.Object=new List<object>();
                    if (query!=null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Password=query.Password;
                        usuario.Nombre=query.Nombre;
                        usuario.FechaNacimiento = query.FechaNacimiento;
                        usuario.RFC = query.Rfc;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result=new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context =new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Password}','{usuario.Nombre}','{usuario.Nombre}',{usuario.FechaNacimiento},'{usuario.RFC}'");
                    if (query>0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "Los datos no fueron ingresados correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex= ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result =new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context=new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario},'{usuario.Nombre}',{usuario.FechaNacimiento},'{usuario.RFC}'");
                    if (query>0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
                result.Ex=ex;
            }
            return result;
        }
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context=new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {IdUsuario}");
                    if (query>0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "No se pudo eliminar el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage=ex.Message;
                result.Ex = ex;
            }
            return result;
        }

    }
}