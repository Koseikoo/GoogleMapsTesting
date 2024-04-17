using Microsoft.Playwright;

public static class TestHelper{
    public static async Task TryClickLocatorAsync(this ILocator locator, int timeoutInMilliseconds){
        var timeoutTask = Task.Delay(timeoutInMilliseconds);
        var elementTask = locator.ClickAsync();

        var completedTask = await Task.WhenAny(timeoutTask, elementTask);
        if (completedTask == elementTask) {
            await elementTask;
        }
    }
}