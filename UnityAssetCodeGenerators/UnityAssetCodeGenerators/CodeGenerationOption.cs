
namespace Levolution.Unity.AssetCodeGenerators
{
    /// <summary>
    /// 
    /// </summary>
    public class CodeGenerationOption
    {
        /// <summary>
        /// 
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CodeGenerationType CodeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CodeGenerationOption() { OutputPath = "Assets"; }
    }
}
