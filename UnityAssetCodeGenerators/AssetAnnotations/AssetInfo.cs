using System;
using System.IO;

namespace AssetAnnotations
{
    public class AssetInfo
    {
        public string AssetPath { get; private set; }

        public string AssetName { get; private set; }

        public AssetInfo(string path)
        {
            AssetPath = path;
            AssetName = Path.GetFileNameWithoutExtension(path);
        }

        public override string ToString()
        {
            return AssetName;
        }
    }
}
