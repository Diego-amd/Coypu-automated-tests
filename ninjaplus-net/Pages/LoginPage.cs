using Coypu;

namespace NinjaPlus.Pages
{
    public class LoginPage
    {
        private readonly BrowserSession _browser; //propriedade do Browser private, ou seja, com _

        public LoginPage(BrowserSession browser)
        {
            _browser = browser;
        }

        public void Load()
        {
            _browser.Visit("/login");
        }
        public void With(string email, string pass)
        {
            this.Load();
            _browser.FillIn("email").With(email);
            _browser.FindCss("#passId").SendKeys(pass);
            _browser.ClickButton("Entrar");
        }

        public string AlertMessage()
        {
            return _browser.FindCss(".alert").Text;
        }        
    }
}