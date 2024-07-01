using AplicationCore.Utils;
using Microsoft.Extensions.Configuration;
using Repositorys.Models;
using Repositorys.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        private IRepositoryUsers repository;
        public ServiceUsuario(IConfiguration configuration)
        {
            repository = new RepositoryUsers(configuration);
        }

        public async Task<int> Create(Usuario user)
        {
            string crytpPasswd = Cryptography.EncrypthAES(user.Password);
            user.Password = crytpPasswd;

            return await repository.Create(user);
        }

        public async Task<int> Delete(int id)
        {
            return await repository.Delete(id);
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await repository.GetAll();
        }

        public async Task<Usuario> GetByEmail(string email,string pass)
        {
            string password= Cryptography.EncrypthAES(pass);
            return await repository.GetByEmail(email, password);
        }

        public async Task<Usuario> GetById(int id)
        {
            return await repository.GetById(id);
        }

        public async Task<int> ResetPassword(string email, string newPassword)
        {
          
            return await repository.ResetPassword(email, newPassword);
        }

        public async  Task<Usuario> Update(Usuario user)
        {
            return await repository.Update(user);
        }
    }
}
