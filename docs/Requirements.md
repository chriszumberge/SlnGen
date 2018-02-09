# SlnGen Requirements
Approved requirements for the SlnGen projects.

Checked requirements are implemented and have unit tests covering the use case.

## SlnGen.Core Requirements
### Solution (Solution Class)
- [ ] Ability to create named solutions
- [ ] Solutions can contain any number of nested folders
  - [ ] Solutions can contain any number of stand-alone files in a "Solution Items" folder
- [ ] Solutions can define and maintain a cs project build order
- [ ] Solutions can contain any number of cs projects

### C# Project (CsProj Class)
- [ ] Csproj must support references
- [ ] Csproj must support nuget packages
- [ ] Csproj must support any number of nested folders
- [ ] Csproj must support any number of files
- [ ] Csproj must allow files to define build actions

### File (SGFile Class)
- [x] Files must be instantiated with both file name and extension
  - [x] Constructor will take a file name string and extension string
	- [x] File names must support any number of nested periods
	- [x] Neither file name nor extension may be null or empty
	- [x] File name cannot start with or end in a period
	- [x] File extension cannot contain a period
  - [x] Constructor will take a single string containing the file name and extension
	- [x] Must support any number of nested periods
	- [x] Cannot be a null or empty string
	- [x] String cannot start with or end in a period
	- [x] If string contains no period, a file extension cannot be derived
- [x] Files may contain any number of using statements and namespaces
  - [x] Using statements can be added as part of a fluent API
	- [x] Using statements can be added singularly or in bulk
  - [x] Using statemetns can be added as constructor property initializers
  - [x] Namespaces can be added as part of a fluent API
	- [x] Namespaces can be added singularly or in bulk
  - [x] Namespaces can be added as constructor property initializers
- [x] Overrides ToString() method to display all assembly references then all namespaces

### Assembly Reference (SGAssemblyReference)
- [x] Using statements must be instantied with an assembly name string
  - [x] Assembly name cannot be null or empty
- [x] Assembly name will replace spaces with empty strings
- [x] Assembly name property exposed
	- [x] Assembly name property cannot be set to null or empty
- [x] Using Statements will override the ToString() method as $"using {assemblyName};"

### Namespaces (SGNamespace)
- [x] Namespaces must be instantiated with a namespace name string
  - [x] Namespace name cannot be null or empty
  - [x] Namespace names will replace spaces with underscores
- [x] Namespace Name exposed as a public property
	- [x] Namespace name property cannot be set to null or empty
- [x] Namespaces can contain any number of Interfaces
  - [x] Interfaces can be added as part of a fluent API
  - [x] Interfaces can be added as a constructor property initializer
- [x] Namespaces can contain any number of Classes
  - [x] Classes can be added as part of a fluent API
  - [x] Classes can be added as a constructor property initializer
- [x] Namespaces can contain any number of Enums
  - [x] as Fluent API
  - [x] as Constructor Property Initializer
- [x] Namespaces can contain any number of Structs
  - [x] as Fluent API
  - [x] as Constructor Property Initializer
- [x] Namespaces will override the ToString() method listing interfaces first, then classes, then enums, and then structs

### Interfaces (SGInterface)
- [x] Interfaces must be instantiated with an accessbility level and name string
	- [x] Interface names cannot be null or empty
    - [x] Interface names will replace spaces with underscores
	- [x] Accessibility levels will default to private
- [x] Interface Name exposed as a public property
	- [x] Interface name property cannot be set to null or empty
- [ ] Accessibility level exposed as a public property
    - [ ] Accessibility level can be set via a fluent api
    - [ ] Accessibliity level can be set via property
- [x] Interfaces must be able to implement other Interfaces
	- [x] Add interface implementations as strings as part of a fluent API
	- [x] Add interface implementations as strings as a constructor property initializer
	- [x] Add as SGInterface as fluent API
	- [x] Interface implementation cannot be null or empty
- [x] Interface must be able to be marked as generic
	- [x] Add multiple generic param names as String via fluent API
	- [x] Add multiple generic param names a a property initializer
	- [x] Generic param name cannot be null or empty
	- [x] Must expose an IsGeneric boolean property
- [ ] Interfaces can contain any number of method Signatures
	- [ ] Add as part of a fluent API
	- [ ] Add as a constructor property initializer
- [ ] Interfaces must be able to contain any number of Attributes

### Classes (SGClass)
- [ ] Classes must be instantiated with an accessibility level, a name string,
an abstract flag, a static flag, and a partial class flag
  - [ ] Class names cannot be null or empty
  - [ ] Class names will replace spaces with underscores
  - [ ] Accessibility levels will default to private
  - [ ] Abstract flag will default to false
  - [ ] Static flag will default to false
  - [ ] Partial flag will default to false
- [ ] Accessibility level will be exposed via public property
  - [ ] Accessibility level can be set via fluent api
  - [ ] Accessibility level can be set via property
- [ ] Class name will be exposed via public property
  - [ ] Class name cannot be set to null or empty
- [ ] Abstract Flag will be exposed as a public property
  - [ ] Abstract flag can be set via fluent api
  - [ ] Abstract flag can be set via property
- [ ] Static Flag will be exposed as a public property
  - [ ] Static flag can be set via fluent api
  - [ ] Static flag can be set via property
- [ ] Partial Flag will be exposed as a public property
  - [ ] Partial flag can be set via fluent api
  - [ ] Partial flag can be set via property
- [ ] Classes must be able to implement multiple Interfaces
- [ ] Classes must be able to be a subclass of another class
- [ ] Classes must be able to be marked as generic
  - [ ] Add mulitple generic param names as String via fluent API
  - [ ] Add multiple generic param names as a property initializer
  - [ ] Generic praam name cannot be null or empty
  - [ ] Must expose IsGeneric boolean property
- [ ] Classes must be able to contain any number of Class Properties
- [ ] Classes must be able to contain any number of Class Fields
- [ ] Classes must be able to contain any number of Class Constructors
- [ ] Classes must be able to contain any number of Class Methods
- [ ] Classes must be able to contain any number of Attributes
- [ ] Classes must be able to contain any number of nested classes
- [ ] Classes must be able to contain any number of nested interfaces
- [ ] Classes must be able to contain any number of nested enums

### Enums (SGEnum)


### Structs (SGStructs)


### Accessibility Levels (SGAccessibilityLevel)
- [x] Accessibility levels will be implemented as type safe enums
  - [x] "public" = 1 (Public)
  - [x] "private" = 2 (Private)
  - [x] "protected" = 3 (Protected)
  - [x] "internal" = 4 (Internal)
  - [x] "" = 5 (None)

### Method Signatures (SGMethodSignature)
- [ ] Method signatures must be instantiated with an accessibility level, a name string,
a string return type, a static flag, an async flag, and an override flag
  - [ ] Method Signature names cannot be null or empty
  - [ ] Method Signature names will replace spaces with underscores
  - [ ] Accessibility level will default to private
  - [ ] Return type cannot be null or empty
  - [ ] Return type will default to void
  - [ ] Return type can be of System.Type
  - [ ] Static flag will default to false
  - [ ] Async flag will default to false
  - [ ] Override flag will default to false
- [ ] Accessibility Level will be exposed via public property
  - [ ] Accessibility level can be set via fluent api
  - [ ] Accessibility level can be set via property
- [ ] Class name will be exposed via public property
  - [ ] Class name cannot be set to null or empty
- [ ] Return Type will be exposed via public property
  - [ ] Return type can be set via fluent api
  - [ ] Return type can be set via property
  - [ ] Return type can be set using System.Type
- [ ] Static Flag will be exposed as a public property
  - [ ] Static flag can be set via fluent api
  - [ ] Static flag can be set via property
- [ ] Async Flag will be exposed as a public property
  - [ ] Async flag can be set via fluent api
  - [ ] Async flag can be set via property
- [ ] Override Flag will be exposed as a public property
  - [ ] Override flag can be set via fluent api
  - [ ] Override flag can be set via property
- [ ] Method signatures must be able to be marked as generic
  - [ ] Add mulitple generic param names as String via fluent API
  - [ ] Add multiple generic param names as a property initializer
  - [ ] Generic praam name cannot be null or empty
  - [ ] Must expose IsGeneric boolean property
- [ ] Method Signatures must be able to contain any number of Arguments

### Arguments (SGArgument)


### Class Properties (SGClassProperty)


### Class Fields (SGClassFields)


### Class Constructors (SGClassConstructor)


### Class Methods (SGClassMethod)


### Attribute (SGAttribute)



## SlnGen.Web Requirements

## SlnGen.Xamarin Requirements