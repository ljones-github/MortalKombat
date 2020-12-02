using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MortalKombat.Main
{
    public static class Helpers
    {
        public static IWebElement dropDownHelp(this IList<IWebElement> dropdown, int index)
        {
            IWebElement returnElement = null;
            if (dropdown.Count <= index - 1 || index < 0)
            {
                index = 0;
                returnElement = dropdown.ElementAt(index);
            }
            else
            {
                dropdown.ElementAt(index);
            }

            return returnElement;
        }
        
        public static void openAllLinks(this IList<IWebElement> list, log4net.ILog log)
        {
            Actions s = new Actions(TestBase.driver);

            foreach (IWebElement link in list)
            {
                Assert.That(TestBase.verifyUrlConnection(link.GetAttribute("href"), log));
                s.KeyDown(Keys.Control).Click(link).Build().Perform();
            }
        }

        public static log4net.ILog initLog([CallerFilePath]string fileName = "")
        {
            return log4net.LogManager.GetLogger(fileName);
        }
    }
}
