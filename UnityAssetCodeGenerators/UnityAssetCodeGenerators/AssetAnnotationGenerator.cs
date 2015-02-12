using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;
using Levolution.Unity.AssetAnnotations;
using System.Text;

namespace Levolution.Unity.AssetCodeGenerators
{
    /// <summary>
    /// 
    /// </summary>
    public class AssetAnnotationGenerator
    {
        private const string Indent = "\t";

        public static void GenerateCode(CodeGenerationOption option)
        {
            var paths = SelectedAssetPaths(Selection.objects);
            var defs = paths.Select(x => PathToAssetTypeDefinition(x));

            foreach (var def in defs)
            {
                var t = GenerateCode(def, option.CodeType);
                var fileName = Path.Combine("Assets", def.Name + ".cs");
                File.WriteAllText(fileName, t);
            }
        }

        private static string GenerateCode(AssetTypeDefinition typeDefinition, CodeGenerationType type)
        {
            switch(type)
            {
                case CodeGenerationType.Class: return GenerateCodeAsClass(typeDefinition);
                case CodeGenerationType.Enum: return GenerateCodeAsEnum(typeDefinition);
            }

            throw new ArgumentException("Unknown enum value", "type");
        }

        private static string GenerateCodeAsClass(AssetTypeDefinition typeDefinition)
        {
            var at = typeof(AssetAnnotation);
            var sb = new StringBuilder();

            sb.AppendFormat("using {0};", at.Namespace); sb.AppendLine();
            sb.AppendLine();

            sb.AppendFormat("public partial class {0}", typeDefinition.Name); sb.AppendLine();
            sb.Append("{"); sb.AppendLine();

            var memberFormatText = string.Format("{0}public static readonly {1} {{0}} = new {1}(@\"{{1}}\");", Indent, at.Name);
            foreach (var m in typeDefinition.Members)
            {
                sb.AppendFormat(memberFormatText, m.Name, m.AssetPath); sb.AppendLine();
            }

            if (typeDefinition.InnerCodes.Count > 0)
            {
                sb.Append(string.Join("\r\n", typeDefinition.InnerCodes.Select(x => GenerateCodeAsClass(x)).ToArray())); sb.AppendLine();
            }

            sb.Append("}"); sb.AppendLine();

            return sb.ToString();
        }

        private static string GenerateCodeAsEnum(AssetTypeDefinition typeDefinition)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("public enum {0}", typeDefinition.Name); sb.AppendLine();
            sb.Append("{"); sb.AppendLine();

            foreach (var m in typeDefinition.Members)
            {
                sb.AppendFormat("{0}{1},{0}// {2}", Indent, m.Name, m.AssetPath); sb.AppendLine();
            }

            if (typeDefinition.InnerCodes.Count > 0)
            {
                sb.Append(string.Join("\r\n", typeDefinition.InnerCodes.Select(x => GenerateCodeAsEnum(x)).ToArray())); sb.AppendLine();
            }

            sb.Append("}"); sb.AppendLine();

            return sb.ToString();
        }

        private static AssetTypeDefinition PathToAssetTypeDefinition(string path)
        {
            return new AssetTypeDefinition(path);
        }

        private static IEnumerable<string> SelectedAssetPaths(IEnumerable<UnityEngine.Object> selectedAssets)
        {
            return selectedAssets
                .Select(x => AssetDatabase.GetAssetPath(x))
                .Where(x => Directory.Exists(x));
        }
    }
}