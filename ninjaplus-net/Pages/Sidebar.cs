using Coypu;

namespace NinjaPlus.Pages
{
    public class Sidebar
    {
        private readonly BrowserSession _browser; //propriedade do Browser private, ou seja, com _

        public Sidebar(BrowserSession browser)
        {
            _browser = browser;
        }

        public string LoggedUser()
        {
            return _browser.FindCss(".user .info span").Text;
        }
    }
}