using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using System.IO;

public class AssetInfoGenerator : EditorWindow
{
    [MenuItem("Generator/AssetInfo")]
    public static void Open()
    {
        var paths = SelectedAssetPaths(Selection.objects);
        var codes = paths.Select(x => PathToCode(x));

        foreach (var code in codes)
        {
            Debug.Log(code.TypeName);
            var t = code.ToString();
            var fileName = Path.Combine("Assets", code.TypeName + ".cs");
            File.WriteAllText(fileName, t);
        }
    }

    private static AssetInfoCodeDefinition PathToCode(string path)
    {
        return new AssetInfoCodeDefinition(path);
    }

    private static IEnumerable<string> SelectedAssetPaths(IEnumerable<UnityEngine.Object> selectedAssets)
    {
        return selectedAssets
            .Select(x => AssetDatabase.GetAssetPath(x))
            .Where(x => Directory.Exists(x));
    }
}
