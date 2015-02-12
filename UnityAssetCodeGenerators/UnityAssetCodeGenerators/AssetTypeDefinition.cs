using Levolution.Unity.AssetAnnotations;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Levolution.Unity.AssetCodeGenerators
{
    /// <summary>
    /// 
    /// </summary>
    public class AssetTypeDefinition
    {
        private const string Indent = "\t";

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string AssetPath { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<AssetMemberDefinition> Members { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<AssetTypeDefinition> InnerCodes { get { return _innerCodes; } }
        private List<AssetTypeDefinition> _innerCodes = new List<AssetTypeDefinition>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assetPath"></param>
        public AssetTypeDefinition(string assetPath)
        {
            AssetPath = assetPath;
            Name = ToTypeName(assetPath);
            Members = ToMember(assetPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var at = typeof(AssetAnnotation);
            var sb = new StringBuilder();

            sb.AppendFormat("using {0};", at.Namespace); sb.AppendLine();
            sb.AppendLine();

            sb.AppendFormat("public partial class {0}", Name); sb.AppendLine();
            sb.Append("{"); sb.AppendLine();

            var memberFormatText = string.Format("{0}public static readonly {1} {{0}} = new {1}(@\"{{1}}\");", Indent, at.Name);
            foreach (var m in Members)
            {
                sb.AppendFormat(memberFormatText, m.Name, m.AssetPath); sb.AppendLine();
            }

            if (InnerCodes.Count > 0)
            {
                sb.Append(string.Join("\r\n", InnerCodes.Select(x => x.ToString()).ToArray())); sb.AppendLine();
            }

            sb.Append("}"); sb.AppendLine();

            return sb.ToString();
        }

        private static string ToTypeName(string path)
        {
            return Path.GetFileName(path);
        }

        private static IEnumerable<AssetMemberDefinition> ToMember(string path)
        {
            return Directory.GetFiles(path)
                .Where(x => Path.GetExtension(x) != ".meta") //TODO: ignore file setting.
                .Select(x =>
                    new AssetMemberDefinition()
                    {
                        Name = Path.GetFileNameWithoutExtension(x).Replace('.', '_').Trim(),
                        AssetPath = x
                    }
                );
        }
    }
}