using StructuralDuplication.Util;

namespace StructuralDuplication.Tests.Util;

public class ScriptParserTests : IDisposable
{
    private readonly string _tempDirectoryPath;
    private readonly string _tempFilePath;

    public ScriptParserTests()
    {
        _tempDirectoryPath = Path.Join(Path.GetTempPath(), Path.GetRandomFileName());
        _tempFilePath = Path.Join(_tempDirectoryPath, Path.GetRandomFileName() + ".cs");

        Directory.CreateDirectory(_tempDirectoryPath);

        var inputText = File.ReadAllText("Util/ScriptParserTestInput.txt");

        // Check that the input doesn't contain the expected parameters
        Assert.DoesNotContain("int number2", inputText);
        Assert.DoesNotContain("bool returnThis2", inputText);
        Assert.DoesNotContain("List<int> intList2", inputText);

        File.WriteAllText(_tempFilePath, inputText);
    }

    public void Dispose()
    {
        File.Delete(_tempFilePath);
        Directory.Delete(_tempDirectoryPath, true);

        Assert.False(File.Exists(_tempFilePath));
        Assert.False(Directory.Exists(_tempDirectoryPath));
    }

    [Fact]
    public void ScriptParser_ShouldAddParameters_WhenGivenValidInput()
    {
        var result = ScriptParser.ParseScript(_tempFilePath);

        // Check that the result contains the new parameters
        Assert.Contains("int number2", result);
        Assert.Contains("bool returnThis2", result);
        Assert.Contains("List<int> intList2", result);
    }
}