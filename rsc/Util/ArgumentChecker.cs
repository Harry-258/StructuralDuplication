namespace StructuralDuplication.Util;

public static class ArgumentChecker
{
    /// <summary>Checks if the arguments provided are valid.</summary>
    /// <param name="args">The arguments provided by the user.</param>
    /// <returns>True if the arguments are valid, false otherwise.</returns>
    public static bool CheckArgs(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: ./StructuralDuplication <path_to_file>");
            return false;
        }

        if (!File.Exists(args[0]))
        {
            Console.WriteLine("File not found.");
            return false;
        }

        if (!args[0].EndsWith(".cs"))
        {
            Console.WriteLine("Invalid file provided. File must be a C# file with the .cs extension.");
            return false;
        }

        return true;
    }
}