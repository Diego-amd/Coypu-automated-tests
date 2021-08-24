using NUnit.Framework;
using NinjaPlus.Pages;
using NinjaPlus.Common;

namespace NinjaPlus.Tests
{
    public class LoginTests : BaseTest
    {
        private LoginPage _login;
        private Sidebar _side;

        [SetUp]
        public void Start()
        {
            _login = new LoginPage(Browser); //instanciando a classe LoginPage
            _side = new Sidebar(Browser); //instanciando a classe Sidebar
        }

        [Test]
        [Category("Critical")]
        public void ShouldSeeLoggedUser()
        {
            _login.With("diego.armindo@newm.com.br", "diego123");
            Assert.AreEqual("Diego", _side.LoggedUser()); //pega o texto que é retornado no elemento
        }

        //DDT => Teste Orientado a Dados (Data Driven Testing)
        //irá executar esses testes
        [TestCase("diego.armindo@newm.com.br", "123456", "Usuário e/ou senha inválidos")]
        [TestCase("404@newm.com.br", "diego123", "Usuário e/ou senha inválidos")]
        [TestCase("", "diego123", "Opps. Cadê o email?")]
        [TestCase("diego.armindo@newm.com.br", "", "Opps. Cadê a senha?")]
        public void ShouldSeeAlertMessage(string email, string pass, string expectMessage)
        {
            _login.With(email, pass);
            Assert.AreEqual(expectMessage, _login.AlertMessage());
        }
    }
}