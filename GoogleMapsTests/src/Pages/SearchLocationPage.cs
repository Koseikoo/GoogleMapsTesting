using System.Text.RegularExpressions;
using Microsoft.Playwright;
using PlaywrightTests.Data;

namespace PlaywrightTests.Pages;

public class SearchLocationPage{

    protected readonly IPage _page;

    protected ILocator _cantFindText;
    private ILocator _acceptCookiesButton;
    private ILocator _searchBar;
    private ILocator _searchButton;
    private ILocator _closeButton;
    private ILocator _addressField;

    public SearchLocationPage(IPage page){
        _page = page;

        _acceptCookiesButton = _page.GetByRole(AriaRole.Button, new() { Name = "Alle akzeptieren" });
        _searchBar = page.GetByRole(AriaRole.Textbox).First;
        _searchButton = page.GetByLabel("Suche", new() { Exact = true });
        _closeButton = page.GetByRole(AriaRole.Button, new() { Name = "Schließen" }).First;
        _addressField = _page.GetByLabel(new Regex($"Adresse", RegexOptions.IgnoreCase)).First;
        _cantFindText = _page.GetByText(new Regex($"nichts gefunden", RegexOptions.IgnoreCase)).First;
    }

    public async Task OpenMapsAsync(){
        await _page.GotoAsync("https://www.google.com/maps/");
    }

    public async Task AcceptCookiesAsync(){
         var timeoutTask = Task.Delay(2000);
        var elementTask = _acceptCookiesButton.ClickAsync();

        var completedTask = await Task.WhenAny(timeoutTask, elementTask);
        if (completedTask == elementTask) {
            await elementTask;
        }
    }

    public virtual async Task SearchLocationAsync(string searchTerm){

        await _searchBar.FillAsync(searchTerm);
        await _searchButton.ClickAsync();
    }

    public virtual async Task CloseSearchAsync(){
        await _closeButton.ClickAsync();
    }

    public ILocator AddressField => _addressField;
    public ILocator CantFindText => _cantFindText;
}