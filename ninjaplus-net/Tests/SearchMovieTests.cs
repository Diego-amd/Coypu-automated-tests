using NinjaPlus.Common;
using NinjaPlus.Lib;
using NinjaPlus.Pages;
using NUnit.Framework;

namespace Ninjaplus.Tests
{
    public class SearchMovieTests : BaseTest
    {
        private LoginPage _login;
        private MoviePage _movie;

        [SetUp]
        public void Before()
        {
            _login = new LoginPage(Browser );
            _movie = new MoviePage(Browser);

            _login.With("diego.armindo@newm.com.br", "diego123");
            Database.InsertMovies(); //roda a query de inserção no banco
        }

        //deve exibir um único filme
        [Test]
        public void ShouldFindUniqueMovie()
        {
            var target = "Coringa";

            _movie.Search(target);
            Assert.That(_movie.HasMovie(target), $"Erro ao verificar se o filme {target} foi encontrado."); //validação

            Browser.HasNoContent("Puxa! Não encontrado nada aqui :("); //mais uma validação na busca, garantindo que essa mensagem não seja exibida
            Assert.AreEqual(1, _movie.CountMovie()); //espera um filme na tr
        }

        //deve exibir vários filmes
        [Test]
        public void ShouldFindMovies()
        {
            var target = "Batman";

            _movie.Search(target);
            Assert.That(_movie.HasMovie("Batman Begins"), $"Erro ao verificar se o filme {target} foi encontrado."); //validação
            Assert.That(_movie.HasMovie("Batman O Cavaleiro das Trevas"), $"Erro ao verificar se o filme {target} foi encontrado."); //validação

            Browser.HasNoContent("Puxa! Não encontrado nada aqui :("); //mais uma validação na busca, garantindo que essa mensagem não seja exibida. Procura em toda a página
            Assert.AreEqual(2, _movie.CountMovie()); //espera um filme na tr
        }

        //deve exibir mensagem de não encontrado
        [Test]
        public void ShouldDisplayNoMovieFound()
        {
            _movie.Search("Xuxa");
            Assert.AreEqual("Puxa! não encontramos nada aqui :(", _movie.SearchAlert()); //garante que o texto apareça na div
        }
    }
}