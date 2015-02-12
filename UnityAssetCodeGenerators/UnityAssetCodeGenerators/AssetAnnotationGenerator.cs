using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Levolution.Unity.AssetCodeGenerators
{
    /// <summary>
    /// 
    /// </summary>
    public class AssetAnnotationGenerator
    {
        public static void GenerateCode(CodeGenerationOption option)
        {
            var paths = SelectedAssetPaths(Selection.objects);
            var codes = paths.Select(x => PathToCode(x));

            foreach (var code in codes)
            {
                Debug.Log(code.Name);
                var t = code.ToString();
                var fileName = Path.Combine("Assets", code.Name + ".cs");
                File.WriteAllText(fileName, t);
            }
        }

        private static AssetTypeDefinition PathToCode(string path)
        {
            return new AssetTypeDefinition(path);
        }

        private static IEnumerable<string> SelectedAssetPaths(IEnumerable<UnityEngine.Object> selectedAssets)
        {
            return selectedAssets
                .Select(x => AssetDatabase.GetAssetPath(x))
                .Where(x => Directory.Exists(x));
        }
    }
}