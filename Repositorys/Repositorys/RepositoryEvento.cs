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
                            Fecha = reader["Fecha"] != DBNull.Value? Convert.ToDateTime(reader["Fecha"]): DateTime.MinValue,
                            Cupo = reader["Cupo"] != DBNull.Value ? Convert.ToInt32(reader["Cupo"]):0,
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


        public Task<Evento> Create(Evento evento)
        {
            throw new NotImplementedException();
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
    }
}
