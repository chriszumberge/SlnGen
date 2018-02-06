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
- [ ] Using statements must be instantied with an assembly name string
  - [ ] Assembly name cannot be null or empty
- [ ] Assembly name will replace spaces with empty strings
- [ ] Using Statements will override the ToString() method as "$using {assemblyName};"

### Namespaces (SGNamespace)
- [ ] Namespaces must be instantiated with a namespace name string
  - [ ] Namespace name cannot be null or empty
  - [ ] Namespace names will replace spaces with periods
- [ ] Namespaces can contain any number of Interfaces
  - [ ] Interfaces can be added as part of a fluent API
  - [ ] Interfaces can be added as a constructor property initializer
- [ ] Namespaces can contain any number of Classes
  - [ ] Classes can be added as part of a fluent API
  - [ ] Classes can be added as a constructor property initializer
- [ ] Namespaces can contain any number of Enums
  - [ ] as Fluent API
  - [ ] as Constructor Property Initializer
- [ ] Namespaces can contain any number of Structs
  - [ ] as Fluent API
  - [ ] as Constructor Property Initializer
- [ ] Namespaces will override the ToString() method listing interfaces first, then classes, then enums, and then structs

### Interfaces (SGInterface)

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