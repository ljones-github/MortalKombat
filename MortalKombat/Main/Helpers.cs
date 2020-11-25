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
        public static void selectOption(this SelectElement selector, int index)
        {
            if (selector.Options.Count <= index - 1 || index < 0)
            {
                index = 0;
            }
            else
            {
                selector.SelectByIndex(index);
            }
        }

        public static void openAllLinks(this IList<IWebElement> list)
        {
            Actions s = new Actions(TestBase.driver);
            foreach(IWebElement e in list)
            {
                s.KeyDown(Keys.Control).Click(e).Build().Perform();
            }
        }

        public static log4net.ILog initLog([CallerFilePath]string fileName = "")
        {
            return log4net.LogManager.GetLogger(fileName);
        }
    }
}
