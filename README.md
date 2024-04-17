# Google Maps Testing

This is a small example project testing the Google Maps Search functionality. It also shows how the start of a library aimed to simplify the implementation of Test scenarios using Playwright with the POM pattern might look like.

## Technology Stack

- C#
- Playwright (JavaScript)
- NUnit

## Setup with VS Code

1. Install [.NET 7.0.x](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
2. Install [PowerShell](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7.4)
3. Download the Repository
4. Open the project in VS Code
5. Build Project with `dotnet build` and enter the project with `cd GoogleMapsTests`
6. Setup Playwright `pwsh bin/Debug/net7.0/playwright.ps1 install`
7. Run the tests through one of the following options:
   - Option 1: Install the Playwright Test for VS Code (by Microsoft) and run the tests inside the Test Explorer GUI
   - Option 2: Run the tests in the terminal with `dotnet test`

## Project Structure

**Data**: Contains the test data classes (e.g., `Location.cs`) and the `TestData.cs` class returning data for specified test cases as `IEnumerable<T>`

**Pages**: Contains page classes encapsulating the logic for creating readable test scenarios

**Tests**: Defines the actual tests. Use the `[TestCase(<args>)]` attribute to define multiple environments for a test scenario

**Helper**: Contains helper and extension methods to encapsulate setup (e.g., browser) and assertion logic to further improve the readability of the tests

## Create a New Test

1. Add the Testing Page class (e.g., `SaveLocationPage.cs`) encapsulating the high-level interactions with the page / Enhance an existing Page class (e.g., `ShowDirections(Location start, Location end)` in `SearchLocationPage.cs`)
2. Add custom assertion to `GoogleMapsAssertExtensions.cs` to make test cases more readable
3. (If necessary) add new test data to `TestData.cs`
4. Create a new test:
   - Create the browser and the necessary pages
   - Run the test interactions from the pages
   - Assert the website state after the interactions through Playwright or the custom Google Maps assertions

> [!TIP]
> Screenshots are saved at `GoogleMapsTests/bin/Debug/net7.0/screenshots`

> [!CAUTION]
> Test cases using WebKit Desktop won’t work on Windows since the emulated WebKit window does not respond to certain actions neither manual nor through headless test cases (e.g., confirming search).
> 
> If you run the tests on macOS, the WebKit test cases can be uncommented.
