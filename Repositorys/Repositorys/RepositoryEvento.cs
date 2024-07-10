using Repositorys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Xml;


namespace Repositorys.Repositorys
{
    public class RepositoryEvento : IRepositoryEvento
    {
        IConfiguration Configuration { get; }
        public RepositoryEvento(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private async Task<List<Evento>> GetAll()
        {
            try
            {
                string cadena = Configuration.GetConnectionString("DataVoxConnection");
                List<Evento> eventos = new List<Evento>();

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand("ObtenerEventosDelAnoActual", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Evento evento = new Evento
                        {
                            Id = reader["EventoId"] != DBNull.Value ? Convert.ToInt32(reader["EventoId"].ToString()) : 0,
                            Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : "",
                            Fecha = reader["Fecha"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha"]) : DateTime.MinValue,
                            Cupo = reader["Cupo"] != DBNull.Value ? Convert.ToInt32(reader["Cupo"]) : 0,
                            Imagen = reader["Imagen"] != DBNull.Value ? reader["Imagen"].ToString() : ""
                        };

                        eventos.Add(evento);
                    }
                }

                return eventos;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<int> Create(Evento evento)
        {
            try
            {
                string cadena = Configuration.GetConnectionString("DataVoxConnection");
                int usuario = 0;

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand("InsertEvent", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros necesarios para el procedimiento almacenado
                    command.Parameters.AddWithValue("@Descripcion", evento.Descripcion);
                    command.Parameters.AddWithValue("@Fecha", evento.Fecha);
                    command.Parameters.AddWithValue("@Cupo", evento.Cupo);
                    command.Parameters.AddWithValue("@Imagen", evento.Imagen);

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        usuario = reader["ResultCode"] != DBNull.Value ? Convert.ToInt32(reader["ResultCode"].ToString()) : 0;
                    }
                }

                return usuario;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Evento> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Evento>> GetEventos()
        {
            try
            {


                List<Evento> eventos = await GetAll();


                return eventos;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Evento> Uptade(Evento evento)
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertEventAsync(string descripcion, DateTime fecha, int cupo, string imagenBytes)
        {
            try
            {
                string connectionString = Configuration.GetConnectionString("DataVoxConnection");
                int result = 0;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("InsertEvent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Utilizar SqlParameter para los parámetros
                        command.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = descripcion;
                        command.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = fecha;
                        command.Parameters.Add("@Cupo", SqlDbType.Int).Value = cupo;
                        command.Parameters.Add("@Imagen", SqlDbType.Text).Value = imagenBytes; // -1 para máximo tamaño

                        // Ejecutar el comando y obtener el valor de retorno
                        var returnValue = await command.ExecuteScalarAsync();

                        // Verificar el resultado devuelto por el procedimiento almacenado
                        if (returnValue != null && returnValue != DBNull.Value)
                        {
                            result = Convert.ToInt32(returnValue);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el evento", ex);
            }
        }

       
    }

}
