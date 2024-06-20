using AplicationCore.Utils;
using ApplicationCore.Services;
using EventPlannerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorys.Models;
using System.Net;

namespace EventPlannerApi.Controllers
{
    [ApiController]
    [Route("User")]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        public UsersController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceUsuario service = new ServiceUsuario(Configuration);

                List<Usuario> users = await service.GetAll();

                if (users == null || users.Count == 0)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Eventos no encontrados";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Reporte encontrado";
                    response.Data = users;
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = e.Message;

                return StatusCode(response.StatusCode, response);
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceUsuario service = new ServiceUsuario(Configuration);

                Usuario user = await service.GetById(id);

                if (user == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Eventos no encontrados";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Reporte encontrado";
                    response.Data = user;
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = e.Message;

                return StatusCode(response.StatusCode, response);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Usuario user)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceUsuario service = new ServiceUsuario(Configuration);

                int user1 = await service.Create(user);

                if (user1 == 0)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Eventos no encontrados";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Reporte encontrado";
                    response.Data = user1;
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = e.Message;

                return StatusCode(response.StatusCode, response);
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update(Usuario user)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceUsuario service = new ServiceUsuario(Configuration);

                Usuario user1 = await service.Update(user);

                if (user1 == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Eventos no encontrados";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Reporte encontrado";
                    response.Data = user1;
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = e.Message;

                return StatusCode(response.StatusCode, response);
            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel user)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceUsuario service = new ServiceUsuario(Configuration);

                Usuario user1 = await service.GetByEmail(user.Email, user.Password);

                if (user1 == null)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Eventos no encontrados";
                }
                else
                {
                    if (user1.Password != Cryptography.EncrypthAES(user.Password))
                    {
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        response.Message = "Credenciales invalidas";
                        response.Data = null;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Message = "Credenciales validas";
                        response.Data = user1;
                    }

                }

                return Ok(response);
            }
            catch (Exception e)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = e.Message;

                return StatusCode(response.StatusCode, response);
            }
        }
    }
}
