using System;
using System.Collections.Generic;
using DataGathererRobot.WebsiteInformationGatherer.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace DataGathererRobot.WebsiteInformationGatherer
{
    public class MicrowavesInformationGatherer : IInformationGatherer
    {
        private const string Url = "http://onliner.by";
        private const string XpathCatalogue = "//div[@id='container']/div/div/header/div[2]/div/nav/ul/li/a/span";
        private const string XpathAppliances = "//div[@id='container']/div/div[2]/div/div/div/ul/li[4]";
        private const string XpathCooking = "//div[@id='container']/div/div[2]/div/div/div/div[3]/div/div[3]/div/div/div[6]/div";
        private const string XpathMicrowaves = "//div[@id='container']/div/div[2]/div/div/div/div[3]/div/div[3]/div/div/div[6]/div[2]/div/a";
        private const string XpathShowOptions = "//div[@id='schema-order']/a/span[2]";
        private const string XpathWithReviews = "//div[@id='schema-order']/div[2]/div/div[5]/span";
        private const string CssSelectorProducts = ".schema-product__group";
        private const string CssSelectorName = ".schema-product__title span";
        private const string CssSelectorLink = ".schema-product__title a";
        private const string CssSelectorPrice = ".schema-product__price a";
        private const string PrefixMicrowave = "Микроволновая печь ";

        private readonly IWebDriver _driver;
        private readonly List<Microwave> _microwaves;

        public MicrowavesInformationGatherer()
        {
            _driver = new ChromeDriver();
            _microwaves = new List<Microwave>();
        }

        public List<T> GatherData<T>()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.FindElement(By.XPath(XpathCatalogue)).Click();
            _driver.FindElement(By.XPath(XpathAppliances)).Click();
            _driver.FindElement(By.XPath(XpathCooking)).Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(XpathMicrowaves)));
            _driver.FindElement(By.XPath(XpathMicrowaves)).Click();
            _driver.FindElement(By.XPath(XpathShowOptions)).Click();
            _driver.FindElement(By.XPath(XpathWithReviews)).Click();

            for (int i = 0; i < 40; i++)
            {
                try
                {
                    var microwave = new Microwave
                    {
                        Name = _driver.FindElement(By.CssSelector($"{CssSelectorProducts}:nth-child({i}) {CssSelectorName}"))
                                      .Text.Substring(PrefixMicrowave.Length),
                        Price = _driver.FindElement(By.CssSelector($"{CssSelectorProducts}:nth-child({i}) {CssSelectorPrice}")).Text,
                        Link = _driver.FindElement(By.CssSelector($"{CssSelectorProducts}:nth-child({i}) {CssSelectorLink}")).GetAttribute("href"),
                    };
                    _microwaves.Add(microwave);
                }
                catch (NoSuchElementException)
                {
                }
            }

            _driver.Quit();

            return (List<T>)Convert.ChangeType(_microwaves, typeof(List<T>));
        }
    }
}
