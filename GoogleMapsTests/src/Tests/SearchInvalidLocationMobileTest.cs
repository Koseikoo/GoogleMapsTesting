using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Pages;
using PlaywrightTests.Data;

namespace PlaywrightTests.Tests;

[TestFixture]

public class SearchInvalidLocationMobileTests : PageTest{
    private const float timeoutInSeconds = 10;

    [SetUp]
    public void SetUp(){
    }

    [Retry(3)]
    [TestCase(PlaywrightTests.BrowserType.Chromium, DeviceType.Galaxy_S8)]
    [TestCase(PlaywrightTests.BrowserType.WebKit, DeviceType.iPhone_13)]
    public async Task SearchInvalidGeolocationMobile(BrowserType browserType, DeviceType deviceType){
        IBrowserContext browser = await SetupHelper.CreateBrowser(browserType, deviceType, timeoutInSeconds);
        var page = new MobileSearchLocationPage(await browser.NewPageAsync());

        await page.OpenMapsAsync();
        await page.AcceptCookiesAsync();

        if(browserType is PlaywrightTests.BrowserType.WebKit)
            await page.CloseAppPopupIOSAsync();
        else if (browserType is PlaywrightTests.BrowserType.Chromium)
            await page.CloseAppPopupAndroidAsync();

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