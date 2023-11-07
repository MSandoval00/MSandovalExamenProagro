using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BL
{
    public class Estado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context=new DL.MsandovalExamenProagroContext())
                {
                    var query=context.Estados.FromSqlRaw($"EstadoGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query!=null)
                    {
                        foreach (var row in query)
                        {
                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = row.IdEstado;
                            estado.NombreEstado=row.NombreEstado;
                            estado.Siglas=row.Siglas;
                            result.Objects.Add(estado);
                            result.Correct = true;
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay estados registrados";
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
    }
}
