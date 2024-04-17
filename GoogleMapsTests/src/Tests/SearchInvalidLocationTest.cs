using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Pages;
using PlaywrightTests.Data;

namespace PlaywrightTests.Tests;

[TestFixture, Parallelizable(ParallelScope.Children)]

public class SearchInvalidLocationTests : PageTest{
    private const float timeoutInSeconds = 10;

    [SetUp]
    public void SetUp(){
    }

    [Retry(3)]
    [TestCase(PlaywrightTests.BrowserType.Chromium, DeviceType.Desktop_Chrome)]
    [TestCase(PlaywrightTests.BrowserType.Firefox, DeviceType.Desktop_Firefox)]
    [TestCase(PlaywrightTests.BrowserType.WebKit, DeviceType.Desktop_Safari)]
    public async Task SearchInvalidGeolocation(BrowserType browserType, DeviceType deviceType){
        IBrowserContext browser = await SetupHelper.CreateBrowser(browserType, deviceType, timeoutInSeconds);
        var page = new SearchLocationPage(await browser.NewPageAsync());

        await page.OpenMapsAsync();
        await page.AcceptCookiesAsync();

        foreach (var location in TestData.GetInvalidLocations())
        {
            await page.SearchLocationAsync(location.Geolocation);
            await Expect(page.CantFindText).ToBeVisibleAsync();
        }

        await browser.CloseAsync();
    }

    [TearDown]
    public void TearDown(){
    }    
}