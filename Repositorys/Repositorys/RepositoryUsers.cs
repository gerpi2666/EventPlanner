﻿using Repositorys.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace Repositorys.Repositorys
{
    public class RepositoryUsers : IRepositoryUsers
    {

        //IConfiguration Configuration { get; }
        protected string cadena { get; }
        public RepositoryUsers()
        {
            // Configuration = configuration;
            cadena = "Data Source=DESKTOP-EFN5H8E;Initial Catalog=EventosDB;User ID=sa;Password=123456;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30";
        }



        public async Task<int> Create(Usuario user)
        {
            try
            {
               // string cadena = Configuration.GetConnectionString("DataVoxConnection");
                int usuario= 0;

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand("InsertUserAndRole", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@FechaVencimiento",user.ExpirationDate);
                    command.Parameters.AddWithValue("@Rol",user.Rol);
                    command.Parameters.AddWithValue("@NombreUsuario", user.NombreUsuario);
                    
                                        SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        usuario = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"].ToString()) : 0;
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
               // string cadena = Configuration;
                int usuario = 0;

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand("DeleteUser", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros necesarios para el procedimiento almacenado
                    command.Parameters.AddWithValue("@Id", id);
                 

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        usuario = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"].ToString()) : 0;
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

        public async Task<List<Usuario>> GetAll()
        {
            try
            {
                List<Usuario> users = null;

               // string cadena = Configuration.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                    var command = new SqlCommand("GetUsers", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    var reader = command.ExecuteReader();
                    users = new List<Usuario>();

                    while (reader.Read())
                    {

                        Usuario usuario = new Usuario();
                        usuario.Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "";
                        usuario.Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : "";
                        usuario.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"].ToString()) : 0;
                        usuario.ExpirationDate = reader["FechaVencimiento"] != DBNull.Value ? Convert.ToDateTime(reader["FechaVencimiento"].ToString()) : DateTime.MinValue;
                        usuario.Rol = reader["Rol"] != DBNull.Value ? Convert.ToInt32(reader["Rol"].ToString()) : 0;
                        usuario.RolDescripcion = reader["DescripcionRol"] != DBNull.Value ? reader["DescripcionRol"].ToString() : "";
                        usuario.NombreUsuario= reader["NombreUsuario"] != DBNull.Value ? reader["NombreUsuario"].ToString() : "";
                        users.Add(usuario);
                    }
                }


                return users;
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

        public async Task<Usuario> GetById(int id)
        {
            try
            {
                Usuario usuario = null;
               // string cadena = Configuration.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                    var command = new SqlCommand("GetUserWithRoleDescription", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserId", id));

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        usuario= new Usuario();
                        usuario.Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() :"";
                        usuario.Password= reader["Password"] != DBNull.Value ? reader["Password"].ToString() : "";
                        usuario.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"].ToString()) : 0;
                        usuario.ExpirationDate = reader["FechaVencimiento"] != DBNull.Value ? Convert.ToDateTime(reader["FechaVencimiento"].ToString()) : DateTime.MinValue;
                        usuario.Rol = reader["Rol"] != DBNull.Value ? Convert.ToInt32(reader["Rol"].ToString()) : 0;
                        usuario.RolDescripcion = reader["DescripcionRol"] != DBNull.Value ? reader["DescripcionRol"].ToString() : "";
                        usuario.NombreUsuario= reader["NombreUsuario"] != DBNull.Value ? reader["NombreUsuario"].ToString() : "";
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

        public async Task<Usuario> GetByEmail(string email)
        {
            try
            {
                Usuario usuario = null;
              //  string cadena = Configuration.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                    var command = new SqlCommand("GetUserWithRoleDescriptionByEmail", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Email", email));

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        usuario = new Usuario();
                        usuario.Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "";
                        usuario.Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : "";
                        usuario.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"].ToString()) : 0;
                        usuario.ExpirationDate = reader["FechaVencimiento"] != DBNull.Value ? Convert.ToDateTime(reader["FechaVencimiento"].ToString()) : DateTime.MinValue;
                        usuario.Rol = reader["Rol"] != DBNull.Value ? Convert.ToInt32(reader["Rol"].ToString()) : 0;
                        usuario.RolDescripcion = reader["DescripcionRol"] != DBNull.Value ? reader["DescripcionRol"].ToString() : "";
                        usuario.Activo = reader["Activo"] != DBNull.Value ? Convert.ToBoolean(reader["Activo"].ToString()) : false;
                        usuario.NombreUsuario = reader["NombreUsuario"] != DBNull.Value ? reader["NombreUsuario"].ToString() : "";
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


        public async Task<Usuario> Update(Usuario user)
        {
            try
            {
               // string cadena = Configuration.GetConnectionString("DataVoxConnection");

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand("UpdateUser", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros necesarios para el procedimiento almacenado
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@FechaVencimiento", user.ExpirationDate);
                    command.Parameters.AddWithValue("@Rol", user.Rol);
                    command.Parameters.AddWithValue("@NombreUsuario", user.NombreUsuario);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    return  await GetById((int)user.Id);
                }
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

        public async Task<int> ResetPassword(string email, string newPassword)
        {
            try
            {
                // string cadena = Configuration.GetConnectionString("DataVoxConnection");
                int result = 0;

                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand("ResetPassword", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros necesarios para el procedimiento almacenado
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@NewPassword", newPassword);

                    // Parámetro de retorno
                    SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnParameter);

                    // Ejecutar el procedimiento almacenado
                    await command.ExecuteNonQueryAsync();

                    // Obtener el valor de retorno del procedimiento almacenado
                    result = (int)returnParameter.Value;
                }

                return result;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Error al ejecutar el procedimiento almacenado ResetPassword: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ResetPassword: " + ex.Message);
            }
        }


       
    }
}
