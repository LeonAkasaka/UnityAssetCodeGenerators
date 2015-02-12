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