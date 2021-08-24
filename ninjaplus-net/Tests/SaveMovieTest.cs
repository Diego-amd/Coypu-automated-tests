using System.Threading;
using NinjaPlus.Common;
using NinjaPlus.Models;
using NinjaPlus.Pages;
using NUnit.Framework;
using NinjaPlus.Lib;

namespace NinjaPlus.Tests
{
    public class MovieTest : BaseTest
    {
        private LoginPage _login;
        private MoviePage _movie;

        //o foco não é testar o login e sim fazer login com sucesso para cadastrar o filme
        [SetUp]
        public void Before()
        {
            _login = new LoginPage(Browser);
            _movie = new MoviePage(Browser);
            _login.With("diego.armindo@newm.com.br", "diego123");
        }

        [Test]
        public void ShouldSaveMovie()
        {
            
            // massa de teste definida no objeto, usando Models
            var movieData = new MovieModel()
            {
                Title = "Resident Evil",
                Status = "Disponível",
                Year = 2002,
                ReleaseDate = "01/05/2002",
                Cast = {"Mila Jovovich", "Ali Larter", "Ian Glen", "Shawn Roberts"},
                Plot = "A missão do esquadrão e da Alice é desligar a Rainha Vermelha e coletar dados sobre o incidente",
                Cover = CoverPath() + "resident-evil-2002.jpg" //caminho mais imagem
            };

            Database.RemoveByTitle(movieData.Title); //vai remover do banco de acordo com o título

            _movie.Add();
            _movie.Save(movieData);

            //método do Coypu que verifica se a condição é verdadeira, se for falsa, retorna exception
            Assert.That(_movie.HasMovie(movieData.Title), $"Erro ao verificar se o filme {movieData.Title} foi cadastrado."); 
        }
    }
}