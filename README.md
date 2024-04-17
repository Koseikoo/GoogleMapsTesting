Technology Stack

C#
Playwright (JavaScript)
NUnit
Additional Requirements

.NET 7.0.x (https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
PowerShell (https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7.4)

Setup with VS Code

Install .NET 7.0.x
Install PowerShell
Download the Repository
Open the project in VS Code
Setup Playwright (pwsh bin/Debug/net7.0/playwright.ps1 install)
Run the tests through one of the following options:
Option 1: Install the Playwright Test for VS Code (by Microsoft) and run the tests inside the Test Explorer GUI
Option 2: Run the tests in the terminal with (dotnet test)
Project Structure

Data: Contains the test data classes (e.g., Location.cs) and the TestData.cs class returning data for specified test cases as IEnumerable<T>

Pages: Contains page classes encapsulating the logic for creating readable test scenarios

Tests: Defines the actual tests. Use the [TestCase(<args>)] attribute to define multiple environments for a test scenario

Helper: Contains helper and extension methods to encapsulate setup (e.g., browser) and assertion logic to further improve the readability of the tests

Create a New Test

Add the Testing Page class (e.g., SaveLocationPage.cs) encapsulating the high-level interactions with the page / Enhance an existing Page class (e.g., ShowDirections(Location start, Location end) in SearchLocationPage.cs)
Add custom assertion to GoogleMapsAssertExtensions.cs to make test cases more readable
(If necessary) add new test data to TestData.cs
Create a new test:
Create the browser and the necessary pages
Run the test interactions from the pages
Assert the website state after the interactions through Playwright or the custom Google Maps assertions
Known Issues

Test cases using WebKit Desktop won’t work on Windows since the emulated WebKit window does not respond to certain actions neither manual nor through headless test cases (e.g., confirming search). If you run the tests on macOS, the WebKit test cases can be uncommented.
