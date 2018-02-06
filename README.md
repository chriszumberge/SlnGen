# SlnGen
SlnGen is a C# fluent API for programmatically constructing .NET project files.

## Documentation
(When available) Get started by reading through the [SlnGen Project Documentation](docs/readme.md)

SlnGen consists of one core assembly that offers base functionality with additional 
assemblies that extend its functionality to cover more advanced .NET project types.

#### SlnGen.Core
SlnGen.Core contains all the files and configuration options to template out Visual C#
project types such as .Net Framework Class Libraries, Portable Class Libraries, Shared 
Projects and Windows Forms Apps.

#### SlnGen.Web
SlnGen.Web builds on SlnGen.Core with the templates to create web based projects
such as WebAPI, Mobile Apps, and MVC applications.

#### SlnGen.Xamarin
SlnGen.Xamarin builds on SlnGen.Core with the templates to create Xamarin projects
Xamarin.iOS, Xamarin.Android, and Xamarin.Forms.


## NuGet
- (Link to NuGets)

## Build
- (Build Status)
- CI NuGet Feed (appveyor)

## Platform Support
|Platform     | Version |
|-------------|:-------:|
|.Net         | 4.5+    |
|.Net Core    | 1.0+    |

## Tests
- (Test Status)

## Setup
- Available on NuGet: (link)
- Install into .NET project

## API Usage
[SlnGen.Core API Documentation and Examples](src/SlnGen.Core/README.md)

[SlnGen.Web API Documentation and Examples](src/SlnGen.Web/README.md)

[SlnGen.Xamarin API Documentation and Examples](src/SlnGen.Xamarin/README.md)

### Developer Tips
- 

## Contribute
The project is open source so pull requests are encouraged.
- Report bugs by opening an issue
- Submit feature requests by opening an issue
- Fix bugs or add features by sending a pull request (see [Coding Conventions](#coding-conventions) below)

This project follows the [GitFlow branching model for Git](http://datasift.github.io/gitflow/IntroducingGitFlow.html)

### Known Issues
- 

### ToDos
- 

### Project Roadmap
#### New Features
- 

#### Additional Projects
##### SlnGen.Desktop
SlnGen.Desktop builds on SlnGen.Core with the templates to create WPF Apps, Windows Forms
Apps, and Windows Services.

##### SlnGen.Azure
SlnGen.Azure will build on SlnGen.Core with the templates to create Azure Services such as
Azure Functions, Azure WebJobs, and Azure Storage.

##### SlnGen.Data
Sln.Gen.Data will build on SlnGen.Core with templates to create Windows Database projects
and potentially integrate EntityFramework functions.

### Coding Conventions
In general, follow the style used by the [.Net Foundation](https://github.com/dotnet/corefx/blob/master/Documentation/coding-guidelines/coding-style.md)
with the following exceptions:
- Preference to use [expression bodied functions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members#methods)
and [properties](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members#property-get-statements)
where applicable
- Do not use the ```private``` keyword as it is the default accessibility level
- Hard tabs over spaces, *always*

Taking special note of:
- [Allman style](https://en.wikipedia.org/wiki/Indent_style#Allman_style) braces
- Use _camelCase for internal/private fields
- Use ```readonly``` where possible
- Prefix instance fields with ```_```
- Static fields start with ```s_```
- Fields should be specified at the top within type declarations
- Use ```nameof(...)``` whenever possible
- Avoid ```this.``` whenever possible
- Only use ```var``` when it's obvious what the variable type is
- Use PascalCasing to name constant variables and fields
