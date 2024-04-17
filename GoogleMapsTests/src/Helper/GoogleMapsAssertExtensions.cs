using Microsoft.Playwright;

public static class GoogleMapsAssertExtensions{

    public static Task ToContainAddress(this ILocatorAssertions assertion, string address){
        return assertion.ToContainTextAsync(address);
    }
}