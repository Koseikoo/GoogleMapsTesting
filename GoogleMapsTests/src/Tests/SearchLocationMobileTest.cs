using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Pages;
using PlaywrightTests.Data;

namespace PlaywrightTests.Tests;

[TestFixture]

public class SearchLocationMobileTests : PageTest{
    private const float timeoutInSeconds = 10;

    [SetUp]
    public void SetUp(){
    }

    [Retry(3)]
    [TestCase(PlaywrightTests.BrowserType.Chromium, DeviceType.Galaxy_S8)]
    [TestCase(PlaywrightTests.BrowserType.WebKit, DeviceType.iPhone_13)]
    public async Task SearchLocationsMobile(BrowserType browserType, DeviceType deviceType){
        IBrowserContext browser = await SetupHelper.CreateBrowser(browserType, deviceType, timeoutInSeconds);
        
        MobileSearchLocationPage page = new MobileSearchLocationPage(await browser.NewPageAsync());
        await page.OpenMapsAsync();
        await page.AcceptCookiesAsync();

        if(browserType is PlaywrightTests.BrowserType.WebKit)
            await page.CloseAppPopupIOSAsync();
        else if (browserType is PlaywrightTests.BrowserType.Chromium)
            await page.CloseAppPopupAndroidAsync();

        foreach (var location in TestData.GetLocations()){
            await SearchLocation(page, location.Geolocation, location);
        }

        foreach (var location in TestData.GetLocations()){
            await SearchLocation(page, location.Address, location);
        }

        await browser.CloseAsync();
        Assert.Pass();
    }

    private async Task SearchLocation(MobileSearchLocationPage page, string searchTerm, Location location){
        await page.SearchLocationAsync(searchTerm);
            await page.InteractWithResultAsync();

            await Expect(page.CantFindText).Not.ToBeVisibleAsync();
            await Expect(page.AddressField).ToContainAddress(location.AddressMatch);
            await page.CloseSearchAsync();
    }

    [TearDown]
    public void TearDown(){
    }    
}