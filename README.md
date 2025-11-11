# StructuralDuplication

This is a script that that analyzes a C# source file, finds all methods with a single parameter, and duplicates that parameter. The duplicated parameter has the same type, and the name is the original parameter name suffixed by 2. It uses the Roslyn API to find the parameters and change the code.

## Getting started

### Clone the repository

```bash
git clone git@github.com:Harry-258/StructuralDuplication.git
```

### Install the dependencies

From the repository root:

```bash
cd rsc
npm install
```

### Run the script:

In the `rsc` directory:

```bash
dotnet run -- <path-to-script>
```

### Run the tests

From the repository root:

```bash
dotnet test
```