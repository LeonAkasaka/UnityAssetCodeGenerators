using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class AssetInfoCodeDefinition
{
    public const string Indent = "\t";

    public string TypeName { get; private set; }

    public string AssetPath { get; private set; }

    public IEnumerable<string> Members { get; private set; }

    public IEnumerable<AssetInfoCodeDefinition> InnerCodes { get; set; }

    public AssetInfoCodeDefinition(string assetPath)
    {
        AssetPath = assetPath;
        TypeName = ToTypeName(assetPath);
        Members = ToMemberName(assetPath);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendFormat("public partial class {0}", TypeName); sb.AppendLine();
        sb.Append("{"); sb.AppendLine();

        foreach (var m in Members)
        {
            sb.AppendFormat("{0}public static readonly AssetInfo {1} = new AssetInfo({2})", Indent, m, AssetPath); sb.AppendLine();
        }

        sb.Append(string.Join("\r\n", InnerCodes.Select(x => x.ToString()).ToArray())); sb.AppendLine();

        sb.Append("}"); sb.AppendLine();

        return sb.ToString();
    }

    private static string ToTypeName(string path)
    {
        return Path.GetFileName(path);
    }

    private static IEnumerable<string> ToMemberName(string path)
    {
        return Directory.GetFiles(path)
            .Select(x =>
                 Path.GetFileNameWithoutExtension(x).Replace('.', '_').Trim()
            );
    }
}