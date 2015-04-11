using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Levolution.Unity.AssetCodeGenerators
{
    /// <summary>
    /// 
    /// </summary>
    public class AssetAnnotationGenerationWinow : EditorWindow
    {
        /// <summary>
        /// Generation option.
        /// </summary>
        public CodeGenerationOption Option { get { return _option; } }
        private CodeGenerationOption _option = new CodeGenerationOption();

        [MenuItem("Generator/AssetAnnotation")]
        private static void ShowOptionWindow()
        {
            EditorWindow.GetWindow<AssetAnnotationGenerationWinow>();
        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Type: ");
            var values = Enum.GetValues(typeof(CodeGenerationType)).Cast<CodeGenerationType>();
            foreach (var codeType in values) { RenderCodeTypeSelector(codeType); }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Output: ");
            Option.OutputPath = GUILayout.TextArea(Option.OutputPath);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("OK"))
            {
                var files = AssetAnnotationGenerator.GenerateCode(Option);
                DebugWriteOutputFiles(files);
            }
        }

        private static void DebugWriteOutputFiles(System.Collections.Generic.IEnumerable<string> files)
        {
            foreach (var file in files)
            {
                Debug.Log(string.Format("Output: {0}", file));
            }
        }

        private void RenderCodeTypeSelector(CodeGenerationType codeType)
        {
            if (GUILayout.Toggle(Option.CodeType == codeType, codeType.ToString()))
            {
                Option.CodeType = codeType;
            }
        }
    }
}
