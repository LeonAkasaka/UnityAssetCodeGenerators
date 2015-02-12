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

            if (GUILayout.Button("OK"))
            {
                AssetAnnotationGenerator.GenerateCode(Option);
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
