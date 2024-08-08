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
using System.Data.Entity;


namespace Repositorys.Repositorys
{
    public class RepositoryEvento : IRepositoryEvento
    {
        protected string cadena { get; }

        public RepositoryEvento()
        {
            cadena = "Data Source=DESKTOP-EFN5H8E;Initial Catalog=EventosDB;User ID=sa;Password=123456;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30";
        }

        private async Task<List<Evento>> GetAll()
        {
            try
            {
                // string cadena = Configuration.GetConnectionString("DataVoxConnection");
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
                            Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "",
                            Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : "",
                            Fecha = reader["Fecha"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha"]) : DateTime.MinValue,
                            Cupo = reader["Cupo"] != DBNull.Value ? Convert.ToInt32(reader["Cupo"]) : 0,
                            Imagen = reader["Imagen"] != DBNull.Value ? reader["Imagen"].ToString() : "",
                            Activo = Convert.ToBoolean(reader["Activo"].ToString())
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

        private List<Evento> GetEventosCustomer(int id)
        {
            try
            {
                // string cadena = Configuration.GetConnectionString("DataVoxConnection");
                List<Evento> eventos = new List<Evento>();

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("[ObtenerEventosDelAnoActualByCustomer]", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserId", id));


                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Evento evento = new Evento
                        {
                            Id = reader["EventoId"] != DBNull.Value ? Convert.ToInt32(reader["EventoId"].ToString()) : 0,
                            Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "",
                            Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : "",
                            Fecha = reader["Fecha"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha"]) : DateTime.MinValue,
                            Cupo = reader["Cupo"] != DBNull.Value ? Convert.ToInt32(reader["Cupo"]) : 0,
                            Imagen = reader["Imagen"] != DBNull.Value ? reader["Imagen"].ToString() : "",
                            Activo = Convert.ToBoolean(reader["Activo"].ToString())
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
                // string cadena = Configuration.GetConnectionString("DataVoxConnection");
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
                    command.Parameters.AddWithValue("@Name", evento.Name);


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

        public async Task<int> Delete(int id)
        {
            try
            {
                // string cadena = Configuration.GetConnectionString("DataVoxConnection");
                int usuario = 0;

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand("DeleteEvent", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros necesarios para el procedimiento almacenado

                    command.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        usuario = reader["EventId"] != DBNull.Value ? Convert.ToInt32(reader["EventId"].ToString()) : 0;
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

        public async Task<Evento> GetById(int id)
        {
            try
            {
                Evento usuario = null;
                // string cadena = Configuration.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                    var command = new SqlCommand("GetEventoById", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@EventoId", id));

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        usuario = new Evento();
                        usuario.Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "";
                        usuario.Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : "";
                        usuario.Id = reader["EventoId"] != DBNull.Value ? Convert.ToInt32(reader["EventoId"].ToString()) : 0;
                        usuario.Imagen = reader["Imagen"] != DBNull.Value ? reader["Imagen"].ToString() : "";
                        usuario.Fecha = reader["Fecha"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha"].ToString()) : DateTime.MinValue;
                        usuario.Cupo = reader["Cupo"] != DBNull.Value ? Convert.ToInt32(reader["Cupo"].ToString()) : 0;
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
        public async Task<Evento> GetByName(string id)
        {
            try
            {
                Evento usuario = null;
                // string cadena = Configuration.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                    var command = new SqlCommand("[GetEventoByName]", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Name", id));

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        usuario = new Evento();
                        usuario.Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "";
                        usuario.Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : "";
                        usuario.Id = reader["EventoId"] != DBNull.Value ? Convert.ToInt32(reader["EventoId"].ToString()) : 0;
                        usuario.Imagen = reader["Imagen"] != DBNull.Value ? reader["Imagen"].ToString() : "";
                        usuario.Fecha = reader["Fecha"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha"].ToString()) : DateTime.MinValue;
                        usuario.Cupo = reader["Cupo"] != DBNull.Value ? Convert.ToInt32(reader["Cupo"].ToString()) : 0;
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

        public async Task<List<Eventformating>> GetEventos()
        {
            try
            {

                List<Evento> eventos = await GetAll();

                // Agrupar eventos por Activo
                var eventosAgrupados = eventos
                    .GroupBy(e => e.Activo)
                    .Select(g => new Eventformating
                    {
                        Activo = g.Key,
                        Eventos = g.OrderByDescending(e => e.Fecha).ToList()
                    })
                    .ToList();

                return eventosAgrupados;
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

        public async Task<List<Eventformating>> GetEventosByCustomer(int id)
        {
            try
            {

                List<Evento> eventos = GetEventosCustomer(id);

                // Agrupar eventos por Activo
                var eventosAgrupados = eventos
                    .GroupBy(e => e.Activo)
                    .Select(g => new Eventformating
                    {
                        Activo = g.Key,
                        Eventos = g.OrderByDescending(e => e.Fecha).ToList()
                    })
                    .ToList();

                return eventosAgrupados;
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

        public async Task<int> Uptade(Evento evento)
        {
            try
            {
                //string cadena = Configuration.GetConnectionString("DataVoxConnection");
                int usuario = 0;

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand("ActualizarEvento", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros necesarios para el procedimiento almacenado
                    command.Parameters.AddWithValue("@EventoId", evento.Id);
                    command.Parameters.AddWithValue("@Name", evento.Name);
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

        public async Task<int> InsertEventAsync(string descripcion, DateTime fecha, int cupo, string imagenBytes)
        {
            try
            {
                // string connectionString = Configuration.GetConnectionString("DataVoxConnection");
                int result = 0;

                using (SqlConnection connection = new SqlConnection(cadena))
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

        public async Task<string> RegisterUserToEventAsync(int userId, int eventId)
        {
            //string _connectionString = Configuration.GetConnectionString("DataVoxConnection");

            int result = -1; // Valor por defecto para indicar que no se ha realizado la operación

            try
            {
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    using (SqlCommand command = new SqlCommand("RegistrarUsuarioEnEvento", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UsuarioId", userId);
                        command.Parameters.AddWithValue("@EventoId", eventId);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();

                        return "Registrado en el evento con éxito";
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejar errores relacionados con SQL, como problemas de conexión o errores de SQL
                return $"Error de SQL: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                // Manejar otros errores generales
                return $"Error: {ex.Message}";
            }
        }


        public async Task<List<Evento>> GetEventsByUserAsync(int userId)
        {
            //string _connectionString = Configuration.GetConnectionString("DataVoxConnection");
            var eventos = new List<Evento>();

            try
            {
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    using (SqlCommand command = new SqlCommand("ObtenerEventosPorUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UsuarioId", userId);

                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var evento = new Evento
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("EventoId")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                                    Cupo = reader.GetInt32(reader.GetOrdinal("Cupo")),
                                    Imagen = reader["Imagen"] != DBNull.Value ? reader["Imagen"].ToString() : "",
                                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                                };

                                eventos.Add(evento);
                            }
                        }
                    }
                }

                return eventos;
            }
            catch (SqlException sqlEx)
            {
                // Manejar errores relacionados con SQL, como problemas de conexión o errores de SQL
                throw new Exception($"Error de SQL: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                // Manejar otros errores generales
                throw new Exception($"Error: {ex.Message}", ex);
            }
        }


        public async Task<int> UnsubscribeFromEvent(int userId, int eventId)
        {
            try
            {
                //string connectionString = Configuration.GetConnectionString("DataVoxConnection");
                int affectedRows = 0;

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("UnsubscribeUserFromEvent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar los parámetros necesarios para el procedimiento almacenado
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@EventId", eventId);

                        // Ejecutar el procedimiento almacenado
                        affectedRows = (int)await command.ExecuteScalarAsync(); // Afecta las filas eliminadas
                    }
                }

                return affectedRows;
            }
            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de SQL
                throw new Exception($"Error de SQL: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Manejo de excepciones generales
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<List<AttendanceStatistics>> GetAttendanceStatistics()
        {
            try
            {
                //string cadena = Configuration.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(@"
 -- Calcular el total de usuarios
DECLARE @TotalUsers INT;
SELECT @TotalUsers = COUNT(*) FROM Users;

-- Consulta principal para obtener las estadísticas de asistencia por evento
SELECT
    e.Name AS EventName,
    ISNULL(SUM(CASE WHEN ue.Confirm = 1 THEN 1 ELSE 0 END), 0) AS TotalAttendance,
    CASE 
        WHEN @TotalUsers = 0 THEN 0
        ELSE (ISNULL(SUM(CASE WHEN ue.Confirm = 1 THEN 1 ELSE 0 END), 0) * 100.0 / @TotalUsers)
    END AS AverageAttendancePercentage,
    CASE 
        WHEN @TotalUsers = 0 THEN 0
        ELSE ((@TotalUsers - ISNULL(SUM(CASE WHEN ue.Confirm = 1 THEN 1 ELSE 0 END), 0)) * 100.0 / @TotalUsers)
    END AS NonAttendancePercentage,
    CASE 
        WHEN @TotalUsers = 0 THEN 0
        ELSE (@TotalUsers - ISNULL(SUM(CASE WHEN ue.Confirm = 1 THEN 1 ELSE 0 END), 0))
    END AS UsersNotAttending
FROM
    Eventos e
LEFT JOIN
    UserEvents ue ON e.EventoId = ue.IdEvent
GROUP BY
    e.Name;


            ", connection);

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    List<AttendanceStatistics> statisticsList = new List<AttendanceStatistics>();

                    while (await reader.ReadAsync())
                    {
                        var eventName = reader["EventName"].ToString();
                        var totalAttendance = reader["TotalAttendance"] != DBNull.Value ? Convert.ToInt32(reader["TotalAttendance"]) : 0;
                        var totalNOAttendance = reader["UsersNotAttending"] != DBNull.Value ? Convert.ToInt32(reader["UsersNotAttending"]) : 0;
                        var totalNOAttendancePercentage = reader["NonAttendancePercentage"] != DBNull.Value ? Convert.ToDouble(reader["NonAttendancePercentage"]) : 0.0;
                        var averageAttendancePercentage = reader["AverageAttendancePercentage"] != DBNull.Value ? Convert.ToDouble(reader["AverageAttendancePercentage"]) : 0.0;

                        statisticsList.Add(new AttendanceStatistics
                        {
                            EventName = eventName,
                            TotalAttendance = totalAttendance,
                            AverageAttendancePercentage = averageAttendancePercentage,
                            NonAttendancePercentage = totalNOAttendancePercentage,
                            TotalNoAttendence = totalNOAttendance
                        });
                    }


                    return statisticsList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las estadísticas de asistencia", ex);
            }
        }


    }

}






