using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace StructuralDuplication.Util;

public static class ScriptParser
{
    /// <summary>
    ///  Parses a C# script using the syntax tree from the Roslyn API. If it finds any methods with one parameter,
    /// it adds another identical parameter ending in '2' to the method.
    /// </summary>
    /// <param name="scriptPath">The path to the script</param>
    /// <returns>String containing the new script with duplicated parameters.</returns>
    public static string ParseScript(string scriptPath)
    {
        var scriptText = File.ReadAllText(scriptPath);
        var tree = CSharpSyntaxTree.ParseText(scriptText);
        var root = tree.GetRoot();
        
        var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
        var replacedMethods = new Dictionary<MethodDeclarationSyntax, MethodDeclarationSyntax>();
        
        foreach (var method in methods)
        {
            var parameters = method.ParameterList.Parameters;
            if (parameters.Count == 1)
            {
                var parameterName = parameters[0].Identifier;
                var parameterType = parameters[0].Type;
                
                var newParameter = Parameter(Identifier(parameterName + "2")).WithType(parameterType);
                var newMethod = method.AddParameterListParameters(newParameter);
                
                replacedMethods.Add(method, newMethod);
            }
        }
        
        var newRoot = root.ReplaceNodes(replacedMethods.Keys, (oldNode, _) => replacedMethods[oldNode]);
        return newRoot.NormalizeWhitespace().ToFullString();
    }
}