using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Coypu;
using NinjaPlus.Models;
using OpenQA.Selenium;

namespace NinjaPlus.Pages
{
    public class MoviePage
    {
        private readonly BrowserSession _browser;
        public MoviePage(BrowserSession browser)
        {
            _browser = browser;
        }

        public void Add()
        {
            _browser.FindCss(".movie-add").Click(); //click no botão
        }

        private void SelectStatus(string status)
        {
            _browser.FindCss("input[placeholder=Status]").Click();
            var option = _browser.FindCss("ul li span", text: status);
            option.Click();
        }
        //método próprio para Lista de atores
        private void InputCast(List<string> cast)
        {
            var element = _browser.FindCss("input[placeholder$=ator]");
            // enviando os valores da lista de atores
            foreach (var actor in cast)
            {
                element.SendKeys(actor);
                element.SendKeys(Keys.Tab); //recurso puro do Selenium, necessário using. Simulando a tecla Tab
                Thread.Sleep(500); //Thinking Time - simular usuário pensando
            }
        }
        //Método de upload da imagem
        private void UploadCover(string cover)
        {
            var jsScript = "document.getElementById('upcover').classList.remove('el-upload__input');"; //código javascript
            _browser.ExecuteScript(jsScript); //execução do script para retirar a classe que esconde o elemento

            _browser.FindCss("#upcover").SendKeys(cover); //upload da imagem
        }

        // recebe objeto MovieModel para pegar as propriedades
        public void Save(MovieModel movie)
        {
            _browser.FindCss("input[name=title]").SendKeys(movie.Title);
            SelectStatus(movie.Status);
            _browser.FindCss("input[name=year]").SendKeys(movie.Year.ToString()); //coypu só recebe String, então tem que converter
            _browser.FindCss("input[name=release_date]").SendKeys(movie.ReleaseDate);
            InputCast(movie.Cast);
            _browser.FindCss("textarea[name=overview]").SendKeys(movie.Plot);
            UploadCover(movie.Cover);
            _browser.ClickButton("Cadastrar");
        }

        public void Search(string value)
        {
            _browser.FindCss("input[placeholder^=Pesquisar]").SendKeys(value);
            _browser.FindId("search-movie").Click();
        }

        public int CountMovie()
        {
            //encontra todo o CSS da página e conta a quantidade que tem desses elementos
            return _browser.FindAllCss("table tbody tr").Count();
        }

        public string SearchAlert()
        {
            return _browser.FindCss(".alert-dark").Text;
        }

        public bool HasMovie(string title)
        {
            //busca pela hierarquia CSS um tr com texto do título do filme, verificando se existe (True ou False)
            return _browser.FindCss("table tbody tr", text: title).Exists();
        }
    }
}