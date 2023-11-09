using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.OleDb;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context = new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = row.IdUsuario;
                            usuario.Password = row.Password;
                            usuario.Nombre = row.Nombre;
                            usuario.FechaNacimiento = row.FechaNacimiento.ToString("yyyy-MM-dd");
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
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().Single();
                    result.Object = new List<object>();
                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Password = query.Password;
                        usuario.Nombre = query.Nombre;
                        usuario.FechaNacimiento = query.FechaNacimiento.ToString("yyyy-MM-dd");
                        usuario.RFC = query.Rfc;

                        result.Object = usuario;
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
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context = new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Password}','{usuario.Nombre}','{usuario.FechaNacimiento}','{usuario.RFC}'");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Los datos no fueron ingresados correctamente";
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
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context = new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario},'{usuario.Password}','{usuario.Nombre}','{usuario.FechaNacimiento}','{usuario.RFC}'");
                    if (query > 0)
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
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {IdUsuario}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el usuario";
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
        public static ML.Result GetByRFC(string RFC)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context=new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByRFC '{RFC}'").AsEnumerable().Single();
                    result.Object=new List<object>();
                    if (query!=null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Password = query.Password;
                        usuario.Nombre = query.Nombre;
                        usuario.FechaNacimiento = query.FechaNacimiento.ToString("yyyy-MM-dd");
                        usuario.RFC = query.Rfc;
                        result.Object=usuario;
                        result.Correct=true;
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
        public static ML.Result LeerExcel(string connectioString)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(connectioString)) 
                {
                    string query = "SELECT * FROM [Hoja1$]";//nombre del sheet
                    using (OleDbCommand cmd=new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableUsuario = new DataTable();
                        da.Fill(tableUsuario);
                        if (tableUsuario.Rows.Count>0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Password=row[0].ToString();
                                usuario.Nombre=row[1].ToString();
                                usuario.FechaNacimiento=row[2].ToString();
                                usuario.RFC=row[3].ToString();
                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                        result.Object = tableUsuario;
                        if (tableUsuario.Rows.Count>0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct=false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
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
        public static ML.Result ValidarExcel(List<object> usuarios)
        {
            ML.Result result=new ML.Result();
            try
            {
                result.Objects = new List<object>();
                int i = 1;
                foreach (ML.Usuario usuario in usuarios)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;
                    if (usuario.Password=="")
                    {
                        error.Mensaje += "Ingresar el password, ";
                    }
                    if (usuario.Nombre=="")
                    {
                        error.Mensaje += "Ingresar el nombre, ";
                    }
                    if (usuario.FechaNacimiento=="")
                    {
                        error.Mensaje += "Ingresar fecha nacimiento, ";
                    }
                    if (usuario.RFC=="")
                    {
                        error.Mensaje += "Ingresar RFC, ";
                    }
                    if (error.Mensaje!=null)
                    {
                        result.Objects.Add(error);
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct=false ;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

    }
}