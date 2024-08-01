using Repositorys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceEmail
    {
        void SendEmailRegisterEvent(EventeInfoEmail evento, string to);
        void SendEmailCreateEvent(EventeInfoEmail evento, string to);

    }
}
