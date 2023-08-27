
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace NibbsTown
{
    internal class Tools
    {
        internal static string CleanupText(string input)
        {
            // removes all xml tags except of <b> and </b>
            string pattern = @"<(?!/?b\b)[^>]*>";
            return Regex.Replace(input, pattern, string.Empty);
        }

        private static Dictionary<string, Shader> shaders = new Dictionary<string, Shader>();
        internal static Shader GetShader(string shaderPath)
        {
            if (!shaders.ContainsKey(shaderPath))
            {
                Shader shader = Shader.Find(shaderPath);
                shaders.Add(shaderPath, shader);
            }
            return shaders[shaderPath];
        }
    }
}
