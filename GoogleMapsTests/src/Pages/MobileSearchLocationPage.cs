using System.Text.RegularExpressions;
using Microsoft.Playwright;
using PlaywrightTests.Data;

namespace PlaywrightTests.Pages;

public class MobileSearchLocationPage : SearchLocationPage
{
    private ILocator _searchButton;
    private ILocator _closeButton;
    private ILocator _searchBar;
    private ILocator _addressHeader;
    private ILocator _returnButton;
    private ILocator _stayOnWebsiteButton;
    private ILocator _noThanksButton;

    public MobileSearchLocationPage(IPage page) : base(page)
    {
        _cantFindText = _page.GetByText(new Regex("keine Ergebnisse", RegexOptions.IgnoreCase)).First;
        _searchButton = page.GetByRole(AriaRole.Button, new() { Name = "Ort finden" });
        _searchBar = page.GetByLabel("Ort finden");
        _closeButton = page.GetByLabel("Löschen");
        _addressHeader = _page.GetByRole(AriaRole.Heading).First;
        _returnButton = _page.GetByRole(AriaRole.Button, new() { Name = "Zurück" });
        _stayOnWebsiteButton = _page.GetByRole(AriaRole.Button, new() { Name = "Auf Website bleiben" });
        _noThanksButton = _page.GetByRole(AriaRole.Button, new() { Name = "Nein danke" });
    }

    public async Task CloseAppPopupAndroidAsync(){
        await _stayOnWebsiteButton.TryClickLocatorAsync(2000);
    }

    public async Task CloseAppPopupIOSAsync(){
        await _noThanksButton.TryClickLocatorAsync(2000);
    }

    public override async Task SearchLocationAsync(string searchTerm){

        await _searchButton.ClickAsync();
        await _searchBar.FillAsync(searchTerm);
        await _page.Keyboard.PressAsync("Enter");
        
    }

    public async Task InteractWithResultAsync(){
        await _addressHeader.ClickAsync();
    }

    public override async Task CloseSearchAsync(){
        await _returnButton.ClickAsync();
        await _closeButton.ClickAsync();
    }
}