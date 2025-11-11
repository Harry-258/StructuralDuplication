using StructuralDuplication.Util;

namespace StructuralDuplication
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (!ArgumentChecker.CheckArgs(args)) return;
            
            var path = args[0];
            var newCode = ScriptParser.ParseScript(path);
            
            File.WriteAllText(path, newCode);
        }
    }
}

