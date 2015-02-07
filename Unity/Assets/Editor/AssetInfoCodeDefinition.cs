using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class AssetInfoCodeDefinition
{
    public const string Indent = "\t";

    public string TypeName { get; set; }

    public IEnumerable<string> Members { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendFormat("public enum {0}", TypeName); sb.AppendLine();
        sb.Append("{"); sb.AppendLine();

        foreach (var m in Members)
        {
            sb.AppendFormat("{0}{1},", Indent, m); sb.AppendLine();
        }

        sb.Append("}"); sb.AppendLine();

        return sb.ToString();
    }
}