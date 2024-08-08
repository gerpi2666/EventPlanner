using Repositorys.Models;
using AplicationCore.Utils;
using Repositorys.Repositorys;
using Microsoft.Extensions.Configuration;
using EventPlannerApi.Controllers;
using EventPlannerApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace RepositorysTest
{
    public class UserTest
    {
        private readonly UsersController contoller;
        public UserTest()
        {
            contoller = new UsersController();
        }



        [Fact]
        public async void LoginSuccessTest()
        {
            LoginModel login= new LoginModel { Email= "jperez@email.com", Password="123456"};

            var result= await contoller.Login(login);


            Assert.IsType<OkObjectResult>(result);

        }


        [Fact]
        public async void CryptografyEncriptedTest()
        {

            var resultAwait = "u6QXcpElu3jBzi22FNFdrw==";

           var result= Cryptography.EncrypthAES("123456");


            Assert.Equal(resultAwait,result);

        }


        [Fact]
        public async void CryptografyDecriptedTest()
        {

            var resultAwait = "123456";

            var result = Cryptography.DecrypthAES("u6QXcpElu3jBzi22FNFdrw==");


            Assert.Equal(resultAwait, result);

        }

        [Fact]
        public async void LoginFailTest()
        {
            LoginModel login = new LoginModel { Email = "client1@example.com", Password = "1234567" };

            var result = await contoller.Login(login);


            Assert.IsType<NotFoundObjectResult>(result);

        }

        [Fact]
        public async void CreateUserTest()
        {
            IRepositoryUsers repository = new RepositoryUsers();

            var password = Cryptography.EncrypthAES("123456");
            var User = new Usuario { Email = "gpicado@OUTLOK.net", NombreUsuario = "Gerald Picado", Password = password, ExpirationDate = DateTime.Now, Rol = 1 };

            int result = await repository.Create(User);

            Assert.NotEqual<int>(0, result);

        }

        [Fact]
        public async void DeleteUserTest()
        {
            IRepositoryUsers repository = new RepositoryUsers();
            int expectedValue = 7;

            int result = await repository.Delete(7);

            Assert.Equal<int>(expectedValue, result);

        }

        [Fact]
        public async void ResetPasswordTest()
        {
            IRepositoryUsers repository = new RepositoryUsers();

            int expectedValue = 1;

            int result = await repository.ResetPassword("gpicado@OUTLOK.net", "123456");

            Assert.Equal<int>(expectedValue, result);

        }

        [Fact]
        public async void GetAllTest()
        {
            IRepositoryUsers repository = new RepositoryUsers();

            int expectedValue = 1;

            var result = await repository.GetAll();

            Assert.NotEmpty(result);


        }

    }
}