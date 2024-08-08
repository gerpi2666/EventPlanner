using Repositorys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Repositorys
{
    public interface IRepositoryUsers
    {
        Task<List<Usuario>> GetAll();
        Task<Usuario> GetById(int id);
        Task<int> Create(Usuario user);
        Task<Usuario> Update(Usuario user);
        Task<Usuario> GetByEmail(string email);
        Task<int> Delete(int id);
        Task<int> ResetPassword(string email, string newPassword);



    }
}
