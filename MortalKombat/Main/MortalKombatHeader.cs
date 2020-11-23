using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace MortalKombat.Main
{
    class MortalKombatHeader
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        MethodBase m;
        protected internal IWebElement mortalKombatHome => TestBase.driver.FindElement(By.CssSelector(".navbar-brand.gold"));
        protected internal IWebElement language => TestBase.driver.FindElement(By.CssSelector("#LanguageMenuBtn"));
        protected internal IWebElement warnerBros => TestBase.driver.FindElement(By.XPath("//nav[@class='global']/div/a/img"));
        protected internal IWebElement roster => TestBase.driver.FindElement(By.XPath("//div[@id='navbarSupportedContent']/ul/li[2]"));
        protected internal IWebElement mkCollective => TestBase.driver.FindElement(By.XPath("//div[@id='navbarSupportedContent']/ul/li[3]"));
        protected internal IWebElement community => TestBase.driver.FindElement(By.XPath("//div[@id='navbarSupportedContent']/ul/li[4]"));
        protected internal IWebElement media => TestBase.driver.FindElement(By.XPath("//div[@id='navbarSupportedContent']/ul/li[5]"));
        protected internal IWebElement esports => TestBase.driver.FindElement(By.XPath("//div[@id='navbarSupportedContent']/ul/li[6]"));
        protected internal IWebElement buyNowHeader => TestBase.driver.FindElement(By.XPath("//ul[@class='navbar-nav']/li/a"));
        protected internal IList<IWebElement> socialMedia => TestBase.driver.FindElements(By.XPath("//div[@class='social']/a"));

        protected internal IWebElement languageChoice;

        protected internal SelectElement gameInfo => new SelectElement(TestBase.driver.FindElement(By.CssSelector("#navbarDropdown")));

        public MortalKombatHeader(MortalKombat.Main.BrowserType browser)
        {
            TestBase.driver = TestBase.initializeDriver(browser);
        }
        public void selectLanguage(String language)
        {
            m = MethodBase.GetCurrentMethod();
            IList<IWebElement> languageFlags = TestBase.driver.FindElements(By.XPath("//i[@data-v-6aa7de64='']"));
            List<string> languages = new List<string>();
            foreach(IWebElement lang in languageFlags)
            {
                if(lang.Text.Contains(language))
                {
                    languageChoice = lang;
                    log.Info($"Language {language} was found.");
                }
            }

            if(languageChoice == null)
            {
                log.Error($"Language {language} was not found.");
            }

            
        }

        public void selectGameInfo(int index)
        {
            m = MethodBase.GetCurrentMethod();
            gameInfo.selectOption(index);
        }

        public void socialMediaCheck()
        {
            m = MethodBase.GetCurrentMethod();
            IReadOnlyList<String> windows = TestBase.driver.WindowHandles;
        }
    }
}
