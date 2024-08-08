using Repositorys.Models;
using AplicationCore.Utils;
using Repositorys.Repositorys;
using Microsoft.Extensions.Configuration;


namespace RepositorysTest
{
    public class UserTest
    {
        private readonly IConfiguration Configuration;
        public UserTest(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [Fact]
        public async void CreateUserTest()
        {
            IRepositoryUsers repository= new RepositoryUsers(Configuration);

            var password= Cryptography.EncrypthAES("123456");
            var User = new Usuario { Email = "gpicado@sinertica.net",NombreUsuario="Gerald Picado",Password= password,ExpirationDate= DateTime.Now,Rol=1 };

            int result = await repository.Create(User);

            Assert.NotEqual<int>(0, result);

        }

        public async void DeleteUserTest()
        {
            IRepositoryUsers repository = new RepositoryUsers(Configuration);

            int expectedValue = 7;

            int result = await repository.Delete(7);

            Assert.Equal<int>(expectedValue, result);

        }

        public async void ResetPasswordTest()
        {
            IRepositoryUsers repository = new RepositoryUsers(Configuration);

            int expectedValue = 1;

            int result = await repository.ResetPassword("gerpi.2666@gmail.com","123456");

            Assert.Equal<int>(expectedValue, result);

        }
    }
}