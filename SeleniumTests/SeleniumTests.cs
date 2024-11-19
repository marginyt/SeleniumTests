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
    public void Test_Valid_Username_and_Valid_Password()
    {
        driver.Url = "https://www.saucedemo.com";
        driver.FindElement(By.CssSelector("#user-name")).SendKeys("standard_user");
        driver.FindElement(By.CssSelector("#password")).SendKeys("secret_sauce");
        driver.FindElement(By.CssSelector("#login-button")).Click();
        Thread.Sleep(4000);


    }

    [Test]
    public void Test_Invalid_Username_and_Invalid_Password()
    {
        driver.Url = "https://www.saucedemo.com";
        driver.FindElement(By.CssSelector("#user-name")).SendKeys("invalid");
        driver.FindElement(By.CssSelector("#password")).SendKeys("invalid");
        driver.FindElement(By.CssSelector("#login-button")).Click();
        Thread.Sleep(4000);
    }

    [Test]
    public void Test_Login_Add_Two_Items_To_Cart_No_Checkout()
    {
        driver.Url = "https://www.saucedemo.com";
        driver.FindElement(By.CssSelector("#user-name")).SendKeys("standard_user");
        driver.FindElement(By.CssSelector("#password")).SendKeys("secret_sauce");
        driver.FindElement(By.CssSelector("#login-button")).Click();
        driver.FindElement(By.CssSelector("#add-to-cart-sauce-labs-backpack")).Click();
        driver.FindElement(By.CssSelector("#add-to-cart-sauce-labs-fleece-jacket")).Click();
        Thread.Sleep(4000);
    }
    [Test]
    public void Test_Login_Add_Two_Items_To_Cart_With_Checkout()
    {
        driver.Url = "https://www.saucedemo.com";
        driver.FindElement(By.CssSelector("#user-name")).SendKeys("standard_user");
        driver.FindElement(By.CssSelector("#password")).SendKeys("secret_sauce");
        driver.FindElement(By.CssSelector("#login-button")).Click();
        driver.FindElement(By.CssSelector("#add-to-cart-sauce-labs-backpack")).Click();
        driver.FindElement(By.CssSelector("#add-to-cart-sauce-labs-fleece-jacket")).Click();
        var products = driver.FindElement(By.CssSelector("#shopping_cart_container > a")).Text;
        Assert.That(products, Is.EqualTo("2"));
        driver.FindElement(By.CssSelector("#shopping_cart_container > a")).Click();
        driver.FindElement(By.CssSelector("#checkout")).Click();
        Thread.Sleep(4000);
    }
    [Test]
    public void Test_Invalid_Username_and_Invalid_Password_With_Failed_Validation()
    {
        driver.Url = "https://www.saucedemo.com";
        driver.FindElement(By.CssSelector("#user-name")).SendKeys("invalid");
        driver.FindElement(By.CssSelector("#password")).SendKeys("invalid");
        driver.FindElement(By.CssSelector("#login-button")).Click();
        Thread.Sleep(4000);
        var error = driver.FindElement(By.XPath("//*[@id='login_button_container']/div/form/div[3]/h3"));
        var errorText = error.Text;
        Assert.That(errorText, Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));
    }
}
