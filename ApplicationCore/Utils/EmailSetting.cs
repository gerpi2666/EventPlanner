using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Utils
{
    public class EmailSetting
    {
        public  string Username;
        public  string Password ;
        public  int Port;

        //public EmailService(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

    }
}
