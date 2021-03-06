﻿using NUnit.Framework;
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
        protected internal IWebElement twitch => TestBase.driver.FindElement(By.XPath("//a[contains(@href,'twitch')]"));
        protected internal IWebElement discord => TestBase.driver.FindElement(By.XPath("//a[contains(@href,'discord')]"));
        protected internal IWebElement youtube => TestBase.driver.FindElement(By.XPath("//a[contains(@href,'youtube')]"));
        protected internal IWebElement facebook => TestBase.driver.FindElement(By.XPath("//a[contains(@href,'facebook')]"));
        protected internal IWebElement twitter => TestBase.driver.FindElement(By.XPath("//a[contains(@href,'twitter')]"));
        protected internal IWebElement instagram => TestBase.driver.FindElement(By.XPath("//a[contains(@href,'gram')]"));
        protected internal IList<IWebElement> socialMedia => TestBase.driver.FindElements(By.XPath("//div[@class='social']/a"));
        
        protected internal IWebElement neverMissUpdate;

        protected internal IWebElement languageChoice;
        protected internal IWebElement mkSupport => TestBase.driver.FindElement(By.CssSelector(".nav-link.support"));
        protected internal IWebElement loginButton => TestBase.driver.FindElement(By.CssSelector(".player-one.d-flex"));
        protected internal IWebElement gameInfo => TestBase.driver.FindElement(By.CssSelector("#navbarDropdown"));


        public MortalKombatHeader() => PageFactory.InitElements(TestBase.driver, this); 

        public void clickMKHome()
        {
            m = MethodBase.GetCurrentMethod();
            try
            {
                log.Debug("Attempting to click on element");
                mortalKombatHome.Click();
                System.Threading.Thread.Sleep(2000);
                log.Info("Button successfully clicked");
            }
            catch (Exception e)
            {
                log.Error($"Class: {m.ReflectedType.Name} || Method: {m.Name} || Error: {e}");
            }
            Console.WriteLine(TestBase.driver.Url);
            Assert.That(TestBase.driver.Url == "https://www.mortalkombat.com/");

        }
        public void selectLanguage(String languageSelect = "en-US")
        {
            m = MethodBase.GetCurrentMethod();
            Actions s = new Actions(TestBase.driver);
            s.Click(language).Build().Perform();
            IList<IWebElement> languageFlags = TestBase.driver.FindElements(By.XPath("//li[@data-v-6aa7de64='']/a"));
            List<string> languages = new List<string>();
            foreach(IWebElement lang in languageFlags)
            {
                if(lang.GetAttribute("hreflang").Contains(languageSelect))
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

            try
            {
                log.Debug("Attempting to click on element");
                s.Click(languageChoice).Build().Perform();
                System.Threading.Thread.Sleep(3000);
                log.Info("Successfully clicked on element");
            }
            catch(Exception e)
            {
                log.Error($"Class: {m.ReflectedType.Name} || Method: {m.Name} || Error: {e}");
            }

            TestBase._takeFullScreenshot(m.Name);
            
        }

        public void selectGameInfo(int index)
        {
            m = MethodBase.GetCurrentMethod();
            Actions s = new Actions(TestBase.driver);
            s.Click(gameInfo).Build().Perform();
            IWebElement dropDown = TestBase.driver.FindElement(By.XPath("//div[contains(@class,'dropdown-menu')]"));
            IList<IWebElement> options = dropDown.FindElements(By.TagName("a"));
            Assert.That(options.dropDownHelp(index).GetAttribute("href").Contains("mortalkombat"));
            IWebElement option = options.dropDownHelp(index);
            option.Click();
            TestBase._takeFullScreenshot(m.Name);
            //TestBase.takeScreenshot(m.Name);
            //s.Click(option).Build().Perform();
        }

        public void socialMediaCheck()
        {
            m = MethodBase.GetCurrentMethod();
            socialMedia.openAllLinks(log);
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
            TestBase.driver.Manage().Cookies.DeleteAllCookies();
            WebDriverWait myWait = new WebDriverWait(TestBase.driver, TimeSpan.FromSeconds(5));
            myWait.Until(ExpectedConditions.ElementToBeClickable(TestBase.driver.FindElement(By.CssSelector(".emailLaunchButton.enter"))));
            neverMissUpdate = TestBase.driver.FindElement(By.CssSelector(".emailLaunchButton.enter"));
            Actions s = new Actions(TestBase.driver);
            System.Threading.Thread.Sleep(3000);
            s.Click(neverMissUpdate).Build().Perform();
            System.Threading.Thread.Sleep(3000);
            SelectElement monthDropdown = new SelectElement(TestBase.driver.FindElement(By.CssSelector("#AgeGateMonth")));
            SelectElement dayDropDown = new SelectElement(TestBase.driver.FindElement(By.CssSelector("#AgeGateDay")));
            SelectElement yearDropDown = new SelectElement(TestBase.driver.FindElement(By.CssSelector("#AgeGateYear")));
            IWebElement confirmAge = TestBase.driver.FindElement(By.XPath("//button[contains(@class, 'confirm')]"));

            monthDropdown.SelectByText(month);
            dayDropDown.SelectByText(day);
            yearDropDown.SelectByText(year);
            confirmAge.Click();

            System.Threading.Thread.Sleep(3000);

           int ageLimiter = DateTime.Now.Year - Int32.Parse(year);
            int count = 0;
            
            s.Click(neverMissUpdate).Build().Perform();

            // This condition is established so that we have a baseline for which to search in either scenario age >= 21 and age < 21
            if (ageLimiter >= 21)
            {
                log.Debug("Attempting to send keys");
                s.SendKeys(TestBase.driver.FindElement(By.CssSelector("#email")), "ouhiyoihoih").Build().Perform();
                log.Info("Keys sent successfully");
                s.Click(TestBase.driver.FindElement(By.XPath("//button[contains(@class,'btn-gold')]"))).Build().Perform();
                System.Threading.Thread.Sleep(2000);
                TestBase.takeScreenshot(m.Name);
            }
            else 
            {
                IWebElement ageDenied = TestBase.driver.FindElement(By.CssSelector(".age-denied"));
                Console.WriteLine(ageDenied.Text);
            }

        }

        public void mkRoster()
        {
            m = MethodBase.GetCurrentMethod();
            Actions s = new Actions(TestBase.driver);
            try
            {
                log.Debug("Attempting to click on element");
                s.Click(roster).Build().Perform();
                log.Info("Successfully clicked on element");
            }
            catch (Exception e)
            {
                log.Error($"Class: {m.ReflectedType.Name} Method: {m.Name} Error: {e}");
            }

            System.Threading.Thread.Sleep(2500);
            Assert.That(TestBase.driver.Url.Contains("roster"));
            TestBase._takeFullScreenshot(m.Name);
        }

        public void mkKollective()
        {
            m = MethodBase.GetCurrentMethod();
            Actions s = new Actions(TestBase.driver);
            try
            {
                log.Debug("Attempting to click on element");
                s.Click(mkCollective).Build().Perform();
                System.Threading.Thread.Sleep(2000);
                log.Info("Successfully clicked on element");
            }
            catch(Exception e)
            {
                log.Error($"Class: {m.ReflectedType.Name} || Method: {m.Name} || Error: {e}");
            }

            Assert.That(TestBase.driver.Url.Contains("kollective"));


            TestBase.takeScreenshot(m.Name);

        }

        public void mkCommunity()
        {
            m = MethodBase.GetCurrentMethod();
            Actions s = new Actions(TestBase.driver);
            try
            {
                log.Debug("Attempting to click on element");
                s.Click(community).Build().Perform();
                System.Threading.Thread.Sleep(2500);
                log.Info("Successfully clicked on element");
            }
            catch(Exception e)
            {
                log.Error($"Class: {m.DeclaringType.Name} || Method: {m.Name} || Error: {e}");
            }

            Assert.That(TestBase.driver.Url.Contains("community"));
        }

        public void mkMedia()
        {
            m = MethodBase.GetCurrentMethod();
            Actions s = new Actions(TestBase.driver);
            try
            {
                log.Debug("Attempting to click on element");
                s.Click(media).Build().Perform();
                System.Threading.Thread.Sleep(2500);
                log.Info("Successfully clicked on element");
            }
            catch (Exception e)
            {
                log.Error($"Class: {m.DeclaringType.Name} || Method: {m.Name} || Error: {e}");
            }

            Assert.That(TestBase.driver.Url.Contains("media"));
        }

        public void mkEsports()
        {
            m = MethodBase.GetCurrentMethod();
            Actions s = new Actions(TestBase.driver);
            try
            {
                log.Debug("Attempting to click on element");
                s.Click(esports).Build().Perform();
                System.Threading.Thread.Sleep(2500);
                log.Info("Successfully clicked on element");
            }
            catch (Exception e)
            {
                log.Error($"Class: {m.DeclaringType.Name} || Method: {m.Name} || Error: {e}");
            }

            Assert.That(TestBase.driver.Url.Contains("esports"));

            Dictionary<string, string> events = new Dictionary<string, string>();
            IReadOnlyCollection<IWebElement> eventDatesEle = TestBase.driver.FindElements(By.XPath("//td[@class='date']"));
            IReadOnlyCollection<IWebElement> eventLeagueEle = TestBase.driver.FindElements(By.XPath("//td[contains(@class,'scheduleEventNameCol')]"));
            List<string> eventDates = new List<string>();
            List<string> eventLeague = new List<string>();

            foreach (IWebElement e in eventDatesEle)
            {
                eventDates.Add(e.Text);
            }

            foreach(IWebElement e in eventLeagueEle)
            {
                eventLeague.Add(e.Text);
            }

            for(int i = 0; i < eventDates.Count; i++)
            {
                events.Add(eventDates.ElementAt(i), eventLeague.ElementAt(i));
            }

            log.Info("The North American Event info is as follows: ");

            foreach (KeyValuePair<string,string> dict in events)
            {
                log.Info($"Date: {dict.Key} Event: {dict.Value}");
            }
        }

        public void mkSocialMedia()
        {
            m = MethodBase.GetCurrentMethod();
            //Verifying connection for social media links
            Assert.That(TestBase.verifyUrlConnection(twitch.GetAttribute("href"), log));
            Assert.That(TestBase.verifyUrlConnection(discord.GetAttribute("href"), log));
            //Assert.That(TestBase.verifyUrlConnection(youtube.GetAttribute("href"), log));
           // Assert.That(TestBase.verifyUrlConnection(facebook.GetAttribute("href"), log));
            //Assert.That(TestBase.verifyUrlConnection(twitter.GetAttribute("href"), log));
           // Assert.That(TestBase.verifyUrlConnection(instagram.GetAttribute("href"), log));

            List<IWebElement> social = new List<IWebElement>();
            social.Add(twitch);
            social.Add(discord);
            social.Add(youtube);
            social.Add(facebook);
            social.Add(twitter);
            social.Add(instagram);

            foreach (IWebElement e in social)
            {
                System.Threading.Thread.Sleep(1000);
                e.SendKeys(Keys.Control + Keys.Return);
               // s.MoveToElement(e).KeyDown(Keys.Control).Click(e).Build().Perform();
            }

            IReadOnlyCollection<String> windows = TestBase.driver.WindowHandles;
            int count = 0;

            foreach (String window in windows)
            {
                TestBase.driver.SwitchTo().Window(window);
                TestBase._takeFullScreenshot(count.ToString());
                count++;

            }



        }
    }
}
