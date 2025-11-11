using StructuralDuplication.Util;

namespace StructuralDuplication.Tests.Util;

public class ArgumentCheckerTests : IDisposable
{
    private readonly string _tempScriptPath;
    private readonly string _tempDirPath;

    public ArgumentCheckerTests()
    {
        _tempDirPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(_tempDirPath);

        _tempScriptPath = Path.Combine(_tempDirPath, Path.GetRandomFileName() + ".cs");
        File.WriteAllText(_tempScriptPath, "test");
    }

    public void Dispose()
    {
        File.Delete(_tempScriptPath);
        Directory.Delete(_tempDirPath);
    }

    [Fact]
    public void ArgumentChecker_ShouldReturnTrue_WhenGivenValidArguments()
    {
        using var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);
        string[] args = [_tempScriptPath];
        var result = ArgumentChecker.CheckArgs(args);

        Assert.True(result);

        var consoleOutputString = consoleOutput.ToString();
        Assert.DoesNotContain("Usage:", consoleOutputString);
        Assert.DoesNotContain("File not found.", consoleOutputString);
        Assert.DoesNotContain("Invalid file provided.", consoleOutputString);
    }

    [Fact]
    public void ArgumentChecker_ShouldReturnFalse_WhenGivenWrongNumberOfArguments()
    {
        string[] args = ["other", _tempScriptPath, "test"];
        using var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);
        var result = ArgumentChecker.CheckArgs(args);

        Assert.False(result);

        var consoleOutputString = consoleOutput.ToString();
        Assert.Contains("Usage:", consoleOutputString);
        Assert.DoesNotContain("File not found.", consoleOutputString);
        Assert.DoesNotContain("Invalid file provided.", consoleOutputString);
    }

    [Fact]
    public void ArgumentChecker_ShouldReturnFalse_WhenFileDoesNotExist()
    {
        // The file will end with ".cs.cs", making sure that it will never be the
        // same as the one created in the setup function. 
        string[] args = [_tempScriptPath + ".cs"];
        using var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);
        var result = ArgumentChecker.CheckArgs(args);

        Assert.False(result);

        var consoleOutputString = consoleOutput.ToString();
        Assert.DoesNotContain("Usage:", consoleOutputString);
        Assert.Contains("File not found.", consoleOutputString);
        Assert.DoesNotContain("Invalid file provided.", consoleOutputString);
    }

    [Fact]
    public void ArgumentChecker_ShouldReturnFalse_WhenFileIsInvalid()
    {
        File.WriteAllText(_tempScriptPath + ".txt", "test");
        string[] args = [_tempScriptPath + ".txt"];
        using var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);
        var result = ArgumentChecker.CheckArgs(args);

        Assert.False(result);

        var consoleOutputString = consoleOutput.ToString();
        Assert.DoesNotContain("Usage:", consoleOutputString);
        Assert.DoesNotContain("File not found.", consoleOutputString);
        Assert.Contains("Invalid file provided.", consoleOutputString);

        File.Delete(_tempScriptPath + ".txt");
    }
}