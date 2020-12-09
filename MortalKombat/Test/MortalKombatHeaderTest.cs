using MortalKombat.Main;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace MortalKombat.Test
{
    class MortalKombatHeaderTest
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        MethodBase m;

        [SetUp]
        public void _setUp()
        {
            m = MethodBase.GetCurrentMethod();
            BrowserType myBrowser = BrowserType.Firefox;
            TestBase.initializeDriver(myBrowser);
            String url = "https://www.mortalkombat.com";
            log.Debug($"Attempting to navigate to url: {url}");
            TestBase.driver.Navigate().GoToUrl(url);
        }

        [Test]
        public void _mortalKombatHomeClick()
        {
            m = MethodBase.GetCurrentMethod();
            MortalKombatHeader mkh = new MortalKombatHeader();
            mkh.clickMKHome();
            
        }

        [Test]
        public void _mortalKombatBirthday()
        {
            m = MethodBase.GetCurrentMethod();
            MortalKombatHeader mkh = new MortalKombatHeader();
            mkh.selectBirthDate("January", "23", "1992");
        }

        [Test]
        public void _mortalKombatLanguage()
        {
            m = MethodBase.GetCurrentMethod();
            MortalKombatHeader mkh = new MortalKombatHeader();
            mkh.selectLanguage("es-MX");
        }

        [Test]
        public void _mortalKombatGameInfo()
        {
            m = MethodBase.GetCurrentMethod();
            MortalKombatHeader mkh = new MortalKombatHeader();
            mkh.selectGameInfo(4);
        }

        [Test]
        public void _mkRoster()
        {
            m = MethodBase.GetCurrentMethod();
            MortalKombatHeader mkh = new MortalKombatHeader();
            mkh.mkRoster();
        }

        [Test]
        public void _mkKollect()
        {
            m = MethodBase.GetCurrentMethod();
            MortalKombatHeader mkh = new MortalKombatHeader();
            mkh.mkKollective();
        }

        [TearDown]
        public void _tearDown()
        {
            m = MethodBase.GetCurrentMethod();
            log.Debug("Attempting to close the driver");
            TestBase.driver.Quit();
            log.Info("Driver closed successfully.");
        }

    }
}
