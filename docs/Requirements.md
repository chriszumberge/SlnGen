# SlnGen Requirements


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
- [x] Interface names must be instantiated with an accessbility level and name string
	- [x] Interface names cannot be null or empty
	- [x] Accessibility levels will default to private
	- [x] Interface names will replace spaces with underscores
- [x] Interface Name exposed as a public property
	- [x] Interface name property cannot be set to null or empty
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

### Classes (SGClass)

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

### Arguments (SGArgument)

### Class Properties (SGClassProperty)

### Class Fields (SGClassFields)

### Class Constructors (SGClassConstructor)

### Class Methods (SGClassMethod)