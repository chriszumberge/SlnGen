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
    - [x] Assembly name will replace spaces with empty strings
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
    - [x] Interface names will replace spaces with underscores
- [x] Accessibility level exposed as a public property
    - [x] Accessibility level can be set via a fluent api
    - [x] Accessibliity level can be set via property
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
- [x] Interfaces can contain any number of method Signatures
	- [x] Add as part of a fluent API
	- [x] Add as a constructor property initializer
    - [x] Parameter cannot be null, throws exception
- [x] Interfaces must be able to contain any number of Attributes
    - [x] Add as part of a fluent API
    - [x] Add as a constructor property initializer
    - [x] Parameter cannot be null, throws exception

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
  - [ ] Class names will replace spaces with underscores
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
- [x] Method signatures must be instantiated with an accessibility level, a name string,
a string return type, a static flag, an async flag, and an override flag
  - [x] Method Signature names cannot be null or empty
  - [x] Method Signature names will replace spaces with underscores
  - [x] Accessibility level will default to private
  - [x] Return type of null or empty sets to void
  - [x] Return type will default to void
  - [x] Return type can be of System.Type
  - [x] Static flag will default to false
  - [x] Async flag will default to false
  - [x] Override flag will default to false
- [x] Accessibility Level will be exposed via public property
  - [x] Accessibility level can be set via fluent api
  - [x] Accessibility level can be set via property
  - [x] Accessibility level cannot be set to null via property or fluent api
- [x] Method Signature name will be exposed via public property
  - [x] Method Signature name cannot be set to null or empty
  - [x] Method Signature names will replace spaces with underscores
- [x] Return Type will be exposed via public property
  - [x] Return type can be set via fluent api
  - [x] Return type can be set via property
  - [x] Return type can be set using System.Type
  - [x] Return type of null or empty sets to void
- [x] Static Flag will be exposed as a public property
  - [x] Static flag can be set via fluent api
  - [x] Static flag can be set via property
- [x] Async Flag will be exposed as a public property
  - [x] Async flag can be set via fluent api
  - [x] Async flag can be set via property
- [x] Override Flag will be exposed as a public property
  - [x] Override flag can be set via fluent api
  - [x] Override flag can be set via property
- [x] Method signatures must be able to be marked as generic
  - [x] Add mulitple generic param names as String via fluent API
  - [x] Add multiple generic param names as a property initializer
  - [x] Generic praam name cannot be null or empty
  - [x] Must expose IsGeneric boolean property
- [x] Method Signatures must be able to contain any number of Arguments
  - [x] Must expose Arguments propety
  - [x] Arguments can be added via fluent api
  - [x] Arguments can be added via property
- [ ] **Method Signatures must be able to contain any number of Attributes**

### Arguments (SGArgument)
- [x] Arguments must be instantiated with a string type value and a name string
  - [x] Type value string cannot be null or empty
  - [x] Type value can be passed as System.Type
  - [x] Argument name cannot be null or empty
  - [x] Argument name will replace spaces with underscores
- [ ] Arguments can allow string default values
  - [ ] **Default values can be of type object, evaluated as ToString**
- [x] Type value will be exposed via public property
  - [x] Type value can be set via fluent api
  - [x] Type value can be set via property
  - [x] Type value cannot be set to null or empty
- [x] Argument name will be exposed via public property
  - [x] Argument name can be set via fluent api
  - [x] Argument name can be set via property
  - [x] Argument name cannot be set to null or empty
  - [x] Argument name will replace spaces with underscores

### Class Properties (SGClassProperty)
- [x] Class Properties must be instantiated with a string type value, an accessibility 
level, a property name string, a static flag, a getter accessibility level, a setter
accesibility level
    - [x] Property name cannot be null or empty
    - [x] Property name will replace spaces with underscores
    - [x] Type value cannot be null or empty
    - [x] Type value can be passed as System.Type
    - [x] Accessibility will default to private
    - [x] Static flag will default to false
    - [x] Getter accessibility level will default to none
    - [x] Setter accessibility level will default to none
- [x] Property name will be exposed via public property
  - [x] Property name can be set via fluent api
  - [x] Property name can be set via property
  - [x] Property name cannot be set to null or empty
  - [x] Setting property name will replace spaces with underscores
- [x] Type value will be exposed via public property
  - [x] Type value can be set via fluent api
  - [x] Type value can be set via property
  - [x] Type value cannot be set to null or empty
- [x] Accessibility level will be exposed via public property
  - [x] Accessiblility level can be set via fluent api
  - [x] Accessiblility level can be set via property
- [x] Static flag will be exposed via public property
  - [x] Static flag can be set via fluent api
  - [x] Static flag can be set via property
- [x] Getter accessibility level will be exposed via public property
  - [x] Getter accessibility level can be set via fluent api
  - [x] Getter accessibility level can be set via property
- [x] Setter accessibility level will be exposed via public property
  - [x] Setter accessibility level can be set via fluent api
  - [x] Setter accessibility level can be set via property
- [x] Property can define a string initializer value
  - [x] Initializer values can be of type object, evaluated as ToString

### Class Fields (SGClassFields)
- [x] Class Fields must be instantiated with a string type value, an accessibility level,
a field name string, a static flag, a const flag, and a read only flag
  - [x] Field name cannot be null or empty
  - [x] Field name will replace spaces with underscores
  - [x] Type value cannot be null or empty
  - [x] Type value can be passed as System.Type
  - [x] Accessibility level will default to private
  - [x] Static flag will default to false
  - [x] Const flag will default to false
  - [x] Read only flag will default to false
- [x] Field name will be exposed via public property
  - [x] Field name can be set via fluent api
  - [x] Field name can be set via property
  - [x] Field name cannot be set to null or empty
  - [x] Setting field name will replace spaces with underscores
- [x] Type value will be exposed via public property
  - [x] Type value can be set via fluent api
  - [x] Type value can be set via property
  - [x] Type value cannot be set to null or empty
- [x] Accessibility level will be exposed via public property
  - [x] Accessiblility level can be set via fluent api
  - [x] Accessiblility level can be set via property
- [x] Static flag will be exposed via public property
  - [x] Static flag can be set via fluent api
  - [x] Static flag can be set via property
- [x] Const flag will be exposed via public property
  - [x] Const flag can be set via fluent api
  - [x] Const flag can be set via property
- [x] Read only flag will be exposed via public property
  - [x] Read only flag can be set via fluent api
  - [x] Read only flag can be set via property
- [x] Field can define a string initializer value
  - [x] Initializer values can be of type object, evaluated as ToString
- [ ] ToString()

### Class Constructors (SGClassConstructor)
- [ ] Class Constructors must be instantiated with an accessibility level, and a class name
  - [ ] Class name cannot be null or empty
  - [ ] Class name will replace spaces with underscores
  - [ ] Accessibility level will default to public
- [ ] Class name will be exposed via public property
  - [ ] Class name can be set via fluent api
  - [ ] Class name can be set via property
  - [ ] Class name cannot be set to null or empty
  - [ ] Setting class name will replace spaces with underscores
- [ ] Accessibility level will be exposed via public property
  - [ ] Accessibility level can be set via fluent api
  - [ ] Accessibility level can be set via property
- [ ] Class Constructor must contain any number of Method Arguments
- [ ] Class Constructor must allow the specification of Base constructor arguments
  - [ ] Class Constructor cannot also specify This constructor arguments
- [ ] Class Constructor must allow the specification of This constructor arguments
  - [ ] Class Constrcutor cannot also specify Base constructor arguments

- [ ] (Needs analysis) Allowing setting of props/fields from constructor args

### Methods (SGMethod)
- [ ] Methods must be instantiated with an accessibility level, a method name, a return
type, a static flag
  - [ ] Method name cannot be null or emtpy
  - [ ] Method name will replace spaces with underscores
  - [ ] Accessibility level will default to private
  - [ ] Return type cannot be null or empty
  - [ ] Return type will default to void
  - [ ] Return type can be of System.Type
  - [ ] Static flag will default to false
- [ ] Method name will be exposed via a public property
  - [ ] Method name can be set via fluent api
  - [ ] Method name can be set via property
  - [ ] Method name cannot bet set to null or empty
  - [ ] Setting method name will replace spaces with underscores
- [ ] Accessibility level will be exposed via public property
  - [ ] Accessibility level can be set via fluent api
  - [ ] Accessibility level can be set via property
- [ ] Return Type will be exposed via public property
  - [ ] Return type can be set via fluent api
  - [ ] Return type can be set via property
  - [ ] Return type can be set using System.Type
- [ ] Static flag will be exposed via public property
  - [ ] Static flag can be set via fluent api
  - [ ] Static flag can be set via property
- [ ] Class Constructor must contain any number of Method Arguments

### Attribute (SGAttribute)



## SlnGen.Web Requirements

## SlnGen.Xamarin Requirements