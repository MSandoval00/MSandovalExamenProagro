using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class GeoReferencias
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context = new DL.MsandovalExamenProagroContext())
                {
                    var query = context.GeoReferencias.FromSqlRaw($"GeoReferenciasGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            ML.GeoReferencias geoReferencias = new ML.GeoReferencias();
                            geoReferencias.Estado = new ML.Estado();
                            geoReferencias.IdGeorreferencia = row.IdGeorreferencia;
                            geoReferencias.Estado.IdEstado = int.Parse(row.IdEstado.ToString());
                            geoReferencias.Latitud = row.Latitud;
                            geoReferencias.Longitud = row.Longitud;
                            result.Objects.Add(geoReferencias);
                            result.Correct = true;
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay GeorReferencias";
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
        public static ML.Result GetById(int IdGeorreferencia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context = new DL.MsandovalExamenProagroContext())
                {
                    var query = context.GeoReferencias.FromSqlRaw($"GeoReferencias {IdGeorreferencia}").AsEnumerable().Single();
                    result.Object = new List<object>();
                    if (query != null)
                    {
                        ML.GeoReferencias geoReferencias = new ML.GeoReferencias();
                        geoReferencias.Estado = new ML.Estado();
                        geoReferencias.IdGeorreferencia = query.IdGeorreferencia;
                        geoReferencias.Estado.IdEstado = query.IdEstado;
                        geoReferencias.Latitud = query.Latitud;
                        geoReferencias.Longitud = query.Longitud;

                        result.Object = geoReferencias;
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
        public static ML.Result Add(ML.GeoReferencias geoReferencias)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context = new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"GeoReferenciasAdd {geoReferencias.Estado.IdEstado},'{geoReferencias.Latitud}','{geoReferencias.Longitud}'");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo registrar la georeferencia";
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
        public static ML.Result Update(ML.GeoReferencias geoReferencias)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context = new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"GeoReferenciasUpdate {geoReferencias.IdGeorreferencia},{geoReferencias.Estado.IdEstado},'{geoReferencias.Latitud}','{geoReferencias.Longitud}'");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = true;
                        result.ErrorMessage = "No se pudo actualizar la Georeferencia";
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
        public static ML.Result Delete(int IdGeorreferencia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalExamenProagroContext context = new DL.MsandovalExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"GeoReferenciasDelete {IdGeorreferencia}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar la GeoReferencias";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
