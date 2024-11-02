using System.Net.NetworkInformation;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
public class Tests
    {
#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
    IWebDriver driver;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
    [SetUp]
        public void Setup()
        {
        ChromeOptions options = new ChromeOptions();
        options.AddArguments("--start-maximized");
        driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
    }
    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }


    [Test]
    public void Test_Valid_Login_And_Title()
    {
        driver.Url = "https://www.demo.guru99.com/V4/";
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(1) > td:nth-child(2) > input[type=text]")).SendKeys("mngr600014");
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(2) > td:nth-child(2) > input[type=password]")).SendKeys("sAdEtat");
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(3) > td:nth-child(2) > input[type=submit]:nth-child(1)")).Click();
        var title = driver.Title;
        Assert.That(title, Is.EqualTo("Guru99 Bank Manager HomePage"));
  

    }

    [Test]
    public void Test_Valid_Login_And_Manager_Id()
    {
        string username = "mngr600014";
        driver.Url = "https://www.demo.guru99.com/V4/";
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(1) > td:nth-child(2) > input[type=text]")).SendKeys(username);
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(2) > td:nth-child(2) > input[type=password]")).SendKeys("sAdEtat");
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(3) > td:nth-child(2) > input[type=submit]:nth-child(1)")).Click();
        var managerId = driver.FindElements(By.CssSelector("body > table > tbody > tr > td > table > tbody > tr.heading3 > td"))[0].Text;
        Assert.That(managerId, Is.EqualTo("Manger Id : " + username));
        var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "screenshot.png");
        screenshot.SaveAsFile(filePath);


    }

    [Test]
    public void Test_Invalid_Username_and_Valid_Password()
    {
        driver.Url = "https://www.demo.guru99.com/V4/";
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(1) > td:nth-child(2) > input[type=text]")).SendKeys("invalid");
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(2) > td:nth-child(2) > input[type=password]")).SendKeys("sAdEtat");
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(3) > td:nth-child(2) > input[type=submit]:nth-child(1)")).Click();
        string expectedAlert = "User or Password is not valid";
        Thread.Sleep(1000);
        IAlert alert = driver.SwitchTo().Alert();
        string alertText = alert.Text;
        Assert.That(expectedAlert, Is.EqualTo(alertText));
        alert.Accept();
        var title = driver.Title;
        Assert.That(title, Is.EqualTo("Guru99 Bank Home Page"));


    }

    [Test]
    public void Test_Valid_Username_and_Invalid_Password()
    {
        driver.Url = "https://www.demo.guru99.com/V4/";
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(1) > td:nth-child(2) > input[type=text]")).SendKeys("mngr600014");
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(2) > td:nth-child(2) > input[type=password]")).SendKeys("invalid");
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(3) > td:nth-child(2) > input[type=submit]:nth-child(1)")).Click();
        string expectedAlert = "User or Password is not valid";
        Thread.Sleep(1000);
        IAlert alert = driver.SwitchTo().Alert();
        string alertText = alert.Text;
        Assert.That(expectedAlert, Is.EqualTo(alertText));
        alert.Accept();
        var title = driver.Title;
        Assert.That(title, Is.EqualTo("Guru99 Bank Home Page"));


    }

    [Test]
    public void Test_Invalid_Username_and_Invalid_Password()
    {
        driver.Url = "https://www.demo.guru99.com/V4/";
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(1) > td:nth-child(2) > input[type=text]")).SendKeys("invalid");
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(2) > td:nth-child(2) > input[type=password]")).SendKeys("invalid");
        driver.FindElement(By.CssSelector("body > form > table > tbody > tr:nth-child(3) > td:nth-child(2) > input[type=submit]:nth-child(1)")).Click();
        string expectedAlert = "User or Password is not valid";
        Thread.Sleep(1000);
        IAlert alert = driver.SwitchTo().Alert();
        string alertText = alert.Text;
        Assert.That(expectedAlert, Is.EqualTo(alertText));
        alert.Accept();
        var title = driver.Title;
        Assert.That(title, Is.EqualTo("Guru99 Bank Home Page"));


    }

}
