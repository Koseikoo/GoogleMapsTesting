using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Pages;
using PlaywrightTests.Data;

namespace PlaywrightTests.Tests;

[TestFixture]

public class SearchLocationTests : PageTest{
    private const float timeoutInSeconds = 10;

    IPage _browserPage;
    DeviceType _deviceType;

    [SetUp]
    public void SetUp(){
    }

    [Retry(3)]
    [TestCase(PlaywrightTests.BrowserType.Chromium, DeviceType.Desktop_Chrome)]
    [TestCase(PlaywrightTests.BrowserType.Firefox, DeviceType.Desktop_Firefox)]
    //[TestCase(PlaywrightTests.BrowserType.WebKit, DeviceType.Desktop_Safari)]
    public async Task SearchLocations(BrowserType browserType, DeviceType deviceType){
        IBrowserContext browser = await SetupHelper.CreateBrowser(browserType, deviceType, timeoutInSeconds, false);
        _browserPage = await browser.NewPageAsync();
        _deviceType = deviceType;
        SearchLocationPage page = new SearchLocationPage(_browserPage);
        
        await page.OpenMapsAsync();
        await page.AcceptCookiesAsync();

        foreach (var location in TestData.GetLocations()){
            await SearchLocation(page, location.Geolocation, location);
        }

        foreach (var location in TestData.GetLocations()){
            await SearchLocation(page, location.Address, location);
        }

        await _browserPage.CloseAsync();
        await browser.CloseAsync();
        Assert.Pass();
    }

    private async Task SearchLocation(SearchLocationPage page, string searchTerm, Location location){
        await page.SearchLocationAsync(searchTerm);
        await Expect(page.CantFindText).Not.ToBeVisibleAsync();
        await Expect(page.AddressField).ToContainAddress(location.AddressMatch);
        await _browserPage.TakeScreenshot(searchTerm, _deviceType);
        await page.CloseSearchAsync();
    }

    [TearDown]
    public void TearDown(){
    }    
}