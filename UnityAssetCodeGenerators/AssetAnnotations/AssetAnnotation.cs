using System;
using System.IO;

namespace Levolution.Unity.AssetAnnotations
{
    /// <summary>
    /// 
    /// </summary>
    public class AssetAnnotation
    {
        /// <summary>
        /// 
        /// </summary>
        public string AssetPath { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string AssetName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public AssetAnnotation(string path)
        {
            AssetPath = path;
            AssetName = Path.GetFileNameWithoutExtension(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return AssetName;
        }
    }
}
