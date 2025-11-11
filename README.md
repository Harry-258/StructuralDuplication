# StructuralDuplication

This is a script that finds all the methods that have a single parameter in a C# script, and duplicates that parameter. It gives it the same name, but ended with '2'.

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