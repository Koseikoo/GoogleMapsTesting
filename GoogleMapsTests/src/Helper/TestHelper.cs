using Microsoft.Playwright;
using PlaywrightTests;

public static class TestHelper{
    public static async Task TryClickLocatorAsync(this ILocator locator, int timeoutInMilliseconds){
        var timeoutTask = Task.Delay(timeoutInMilliseconds);
        var elementTask = locator.ClickAsync();

        var completedTask = await Task.WhenAny(timeoutTask, elementTask);
        if (completedTask == elementTask) {
            await elementTask;
        }
    }

    public static async Task TakeScreenshot(this IPage page, string name, DeviceType device){
        string path = $"screenshots/{name}_{device}.png";
        await page.ScreenshotAsync(new()
        {
            Path = path,
            FullPage = true,
        });
    }
}