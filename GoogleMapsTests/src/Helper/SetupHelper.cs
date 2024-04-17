using Microsoft.Playwright;

namespace PlaywrightTests;

public static class SetupHelper{
    public static async Task<IBrowserContext> CreateBrowser(BrowserType browserType, DeviceType deviceType, float timeoutInSeconds = 20, bool headless = true){
        IPlaywright playwright = await Playwright.CreateAsync();

        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = headless,
        };

        string device = deviceType.ToString().Replace('_', ' ');
        var deviceConfig = playwright.Devices[device];
        deviceConfig.Geolocation = new Geolocation(){
            Latitude = 52.520008f,
            Longitude = 13.404954f,
        };
        deviceConfig.Locale = "de-DE";

        var browser = await GetBrowserType().LaunchAsync(launchOptions);
        var context = await browser.NewContextAsync(deviceConfig);

        context.SetDefaultTimeout(timeoutInSeconds * 1000);

        return context;

        IBrowserType GetBrowserType(){
            return browserType switch
            {
                BrowserType.Chromium => playwright.Chromium,
                BrowserType.Firefox => playwright.Firefox,
                BrowserType.WebKit => playwright.Webkit,
                _ => playwright.Chromium
            };
        }
    }
}