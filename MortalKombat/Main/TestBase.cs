using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace MortalKombat.Main
{
    public enum BrowserType
    {
        Firefox,
        Chrome,
        InternetExplorer,
        Safari
    }
    public static class TestBase
    {
        public static IWebDriver initializeDriver(BrowserType myBrowser)
        {
            if(myBrowser.Equals(BrowserType.Chrome))
            {
                driver = new ChromeDriver();
            }
            else if(myBrowser.Equals(BrowserType.Firefox))
            {
                driver = new FirefoxDriver();
            }
            else if(myBrowser.Equals(BrowserType.InternetExplorer))
            {
                driver = new InternetExplorerDriver();
            }
            else
            {
                driver = new SafariDriver();
            }

            return driver;
        }
        public static IWebDriver driver {
            get;
            set;
        }

        public static void takeScreenshot(string methodName)
        {
            Screenshot myScreenshot = ((ITakesScreenshot)driver).GetScreenshot();
            myScreenshot.SaveAsFile("C:\\Users\\ljone\\source\\repos\\MortalKombat\\MortalKombat\\Resources\\Screenshot(s)\\" + methodName + ".png");
        }

        public static void _takeFullScreenshot(string fileName, string additional = "")
        {
            //This is using the Noksa.WebDriver.ScreenshotsExtensions nuget package
            VerticalCombineDecorator vcd = new VerticalCombineDecorator(new ScreenshotMaker());
            driver.TakeScreenshot(vcd).ToMagickImage().ToBitmap().Save("C:\\Users\\ljone\\source\\repos\\SeleniumUno\\SeleniumUno\\Screenshot(s)\\" + fileName + "__" + additional + ".png");

            // This would direct you to the temp folder. 
            Console.WriteLine(Directory.GetCurrentDirectory());
        }

        public static Boolean verifyUrlConnection(string url, log4net.ILog log)
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            Boolean flag = false;
            // Sends the HttpWebRequest and waits for a response.
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
            {
                log.Error($"Error with url: {url}. HttpResponse was {myHttpWebResponse.StatusDescription}");
            }
            else
            {
                log.Info("Url status is OK");
                flag = true;
            }

            // Releases the resources of the response.
            myHttpWebResponse.Close();
            return flag;
        }



    }
}
