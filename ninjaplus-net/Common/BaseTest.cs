using System;
using System.IO;
using Coypu;
using Coypu.Drivers.Selenium;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace NinjaPlus.Common
{
    public class BaseTest
    {
        protected BrowserSession Browser; //somente a classe BaseTest ou filhas (herdeiro) terão acesso
        
        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build(); //lendo e buildando o arquivo de configuração json

            //Aqui é o Before, oque vai ser feito primeiro
            var sessionConfig = new SessionConfiguration
            {
                AppHost = "http://ninjaplus-web",
                Port = 5000,
                SSL = false,
                Driver = typeof(SeleniumWebDriver),
                //Forma de fazer sem arquivo de config: Browser = Coypu.Drivers.Browser.Chrome, //para testar em outro navegador, é só alterar aqui, mas precisa colocar o driver do Browser
                Timeout = TimeSpan.FromSeconds(15)
            };

            if(config["browser"].Equals("chrome"))
            {
                sessionConfig.Browser = Coypu.Drivers.Browser.Chrome;
            }

            if(config["browser"].Equals("firefox"))
            {
                sessionConfig.Browser = Coypu.Drivers.Browser.Firefox;
            }

            Browser = new BrowserSession(sessionConfig);

            Browser.MaximiseWindow(); //máximiza a janela para não pegar a responsividade e elementos diferentes
        }

        public string CoverPath()
        {
            var outputPath = Environment.CurrentDirectory; //pegando o diretório de saída do projeto de teste (caminho compilado)
            return outputPath + "\\Images\\"; //caminho do projeto com a pasta images
        }

        public void TakeScreenshot()
        {
            var resultId = TestContext.CurrentContext.Test.ID;
            var shotPath = Environment.CurrentDirectory + "\\Screenshots";

            //se não existe o diretório, é criado
            if (!Directory.Exists(shotPath))
            {
                Directory.CreateDirectory(shotPath);
            }

            var screenshot = $"{shotPath}\\{resultId}.png"; //caminho

            Browser.SaveScreenshot(screenshot);
            TestContext.AddTestAttachment(screenshot); //adiciona ao relatório TRX
        }

        //este método é executado após a falha, fechando o browser. É o After
        [TearDown]
        public void Finish()
        {
            //vai tentar tirar a print, se não der, vai exibir mensagem de erro e por fim fechar o navegador - boas práticas
            try
            {
                TakeScreenshot(); //para cada teste irá executar esse método para print e evidencia do teste
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro ao capturar o screenshot :(");
                throw new Exception(e.Message);
            }
            finally
            {
                Browser.Dispose();
            }
        }
    }
}