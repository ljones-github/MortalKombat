using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
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
        
        protected internal IWebElement neverMissUpdate;

        protected internal IWebElement languageChoice;
        protected internal IWebElement mkSupport => TestBase.driver.FindElement(By.CssSelector(".nav-link.support"));
        protected internal IWebElement loginButton => TestBase.driver.FindElement(By.CssSelector(".player-one.d-flex"));
        protected internal SelectElement gameInfo => new SelectElement(TestBase.driver.FindElement(By.CssSelector("#navbarDropdown")));

        public MortalKombatHeader() => PageFactory.InitElements(TestBase.driver, this); 

        public void clickMKHome()
        {
            m = MethodBase.GetCurrentMethod();
            try
            {
                log.Debug("Attempting to click on element");
                mortalKombatHome.Click();
                log.Info("Button successfully clicked");
            }
            catch (Exception e)
            {
                log.Error($"Class: {m.ReflectedType.Name} || Method: {m.Name} || Error: {e}");
            }

            Assert.That(TestBase.driver.Url == "https://www.mortalkombat.com");

        }
        public void selectLanguage(String language = "English")
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
                    Assert.That(true);
                }
            }

            if(languageChoice == null)
            {
                log.Error($"Language {language} was not found.");
                Assert.That(false);
            }

            
        }

        public void selectGameInfo(int index)
        {
            m = MethodBase.GetCurrentMethod();
            gameInfo.selectOption(index);
            Assert.That(gameInfo.SelectedOption.GetAttribute("index") == index.ToString()); 
        }

        public void socialMediaCheck()
        {
            m = MethodBase.GetCurrentMethod();
            Actions s = new Actions(TestBase.driver);
            foreach(IWebElement link in socialMedia)
            {
                Assert.That(TestBase.verifyUrlConnection(link.GetAttribute("href"), log));
                s.KeyDown(Keys.Control).Click(link).Build().Perform();
            }
            IReadOnlyList<String> windows = TestBase.driver.WindowHandles;

            foreach(String window in windows)
            {
                TestBase.driver.SwitchTo().Window(window);
                System.Threading.Thread.Sleep(3000);
                TestBase._takeFullScreenshot(m.Name, TestBase.driver.Title);
            }

        }

        public void selectBirthDate(string month = "September", string day = "12", string year = "1991")
        {
            m = MethodBase.GetCurrentMethod();
            IJavaScriptExecutor je = (IJavaScriptExecutor)TestBase.driver;
            string script = "window.scrollBy(0,500)";
            je.ExecuteScript(script);
            neverMissUpdate = TestBase.driver.FindElement(By.CssSelector(".emailLaunchButton.enter"));
            Actions s = new Actions(TestBase.driver);
            TestBase.driver.Manage().Cookies.DeleteAllCookies();
            s.Click(neverMissUpdate).Build().Perform();
            SelectElement monthDropdown = new SelectElement(TestBase.driver.FindElement(By.CssSelector("#AgeGateMonth")));
            SelectElement dayDropDown = new SelectElement(TestBase.driver.FindElement(By.CssSelector("#AgeGateDay")));
            SelectElement yearDropDown = new SelectElement(TestBase.driver.FindElement(By.CssSelector("#AgeGateYear")));

            monthDropdown.SelectByText(month);
            dayDropDown.SelectByText(day);
            yearDropDown.SelectByText(year);

            int count = 0;
            do
            {
                if (TestBase.driver.FindElement(By.CssSelector(".age-denied")).Displayed)
                {
                    Console.WriteLine(TestBase.driver.FindElement(By.CssSelector(".age-denied")).Text);
                }
                else
                {
                    s.Click(neverMissUpdate).Build().Perform();
                    log.Debug("Attempting to send keys");
                    s.SendKeys(TestBase.driver.FindElement(By.CssSelector("#email")), "uyuvuuiiub").Build().Perform();
                    log.Info("Keys sent successfully");
                    s.Click(TestBase.driver.FindElement(By.XPath("//button[contains(@class,'btn-gold')]"))).Build().Perform();
                    System.Threading.Thread.Sleep(2000);

                    //Count will ensure that page has been refreshed and tested again as the choices made are saved in the cookies
                    TestBase.driver.Navigate().Refresh();
                    count++;
                }
            }
            while (count < 2);
        }
    }
}
