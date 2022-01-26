using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditorInternal;
using UnityEngine;

public class Testing
{
    [MenuItem("Tools/Assembly")]
    public static void AssemblyTest()
    {
        Debug.Log("Logging assemblies");
        var assemblies = CompilationPipeline.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{assembly.name}");
            var refs = assembly.allReferences;
            foreach (var reference in refs)
            {
                if (!reference.Contains("Unity/Hub"))
                {
                    sb.AppendLine($"--{reference}");
                }
            }
            Debug.Log(sb);
        }
    }

    // TODO: make config to customize what we see
    // - ignore packages
    [MenuItem("Tools/Asmdef")]
    public static void AsmDef()
    {
        var guidToAsmdef = new Dictionary<string, string>();
        var guids = AssetDatabase.FindAssets("t:asmdef");
        foreach (var guid in guids)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(assetPath);
            guidToAsmdef[guid] = asset.name;
            var split = asset.text.Split('\n');
            var idx = -1;
            for (int i = 0; i < split.Length; ++i)
            {
                if (split[i].Contains("references"))
                {
                    idx = i;
                    break;
                }
            }
            if (idx != -1)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"{asset.name} references");
                var substr = split[++idx];
                while (idx < split.Length && !substr.Contains("],"))
                {
                    var subsplit = substr.Split(':');
                    string path;
                    if (subsplit.Length > 1)
                    {
                        var refGuid = subsplit[1];
                        var stop = refGuid.IndexOf('"');
                        refGuid = refGuid.Substring(0, stop);
                        path = AssetDatabase.GUIDToAssetPath(refGuid).Trim();
                    }
                    else
                    {
                        path = subsplit[0].Trim();
                    }
                    
                    sb.AppendLine($"--{path}");
                    substr = split[++idx];
                }

                Debug.Log(sb);
            }
        }
    }
}
