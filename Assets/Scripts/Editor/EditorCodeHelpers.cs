using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Apache.Editor
{
    public static class EditorCodeHelpers
    {
        private enum ECreationMode
        {
            ChildClass,
            CustomInspector
        }

        private class EndNameEditHandler : UnityEditor.ProjectWindowCallback.EndNameEditAction
        {
            private ECreationMode _mode;
            private string _referenceFilePath;
            private Type _referenceType;

            public void Configure(ECreationMode mode, string assetPath, Type referenceType)
            {
                _mode = mode;
                _referenceFilePath = assetPath;
                _referenceType = referenceType;
            }

            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var newFileContent = GetTemplateForCurrentMode();
                if (newFileContent.Length == 0)
                {
                    Debug.Log($"Failed to find template or template was empty");
                    return;
                }

                var newClassName = Path.GetFileNameWithoutExtension(pathName);

                // update the template
                newFileContent = newFileContent.Replace("#CLASSNAME#", newClassName);
                newFileContent = newFileContent.Replace("#PARENTCLASSNAME#", _referenceType.Name);
                
                // is this an editor class?
                if (_mode == ECreationMode.CustomInspector)
                {
                    var pathElements = pathName.Split(Path.DirectorySeparatorChar);
                    
                    // are we in an editor folder already
                    var isInEditorFolder = pathElements.Any(
                        element => element.ToLower() == "editor");
                    
                    // not in editor folder
                    if (!isInEditorFolder)
                    {
                        var basePath = Path.GetDirectoryName(pathName);
                        var fileName = Path.GetFileName(pathName);
                        
                        if (basePath == null)
                            throw new Exception("basePath is null");

                        var newPath = Path.Combine(basePath, "Editor");
                        Directory.CreateDirectory(newPath);

                        pathName = Path.Combine(newPath, fileName);
                    }
                }
                
                File.WriteAllText(pathName, newFileContent);
                
                // update the asset database
                AssetDatabase.ImportAsset(pathName);
                var newScript = AssetDatabase.LoadAssetAtPath<MonoScript>(pathName);
                ProjectWindowUtil.ShowCreatedAsset(newScript);
            }

            private string TemplateFileForMode(ECreationMode mode) => mode switch
            {
                ECreationMode.ChildClass => "CodeTemplate_ChildClass",
                ECreationMode.CustomInspector => "CodeTemplate_CustomInspector",
                _ => throw new ArgumentOutOfRangeException($"Unknown mode {mode}")
            };

            private string GetTemplateForCurrentMode()
            {
                var templateGUIDs = AssetDatabase.FindAssets($"{TemplateFileForMode(_mode)} t:TextAsset");

                if (templateGUIDs.Length != 1)
                {
                    Debug.LogError($"Found {templateGUIDs.Length} templates for {_mode}. Expected 1");
                    return string.Empty;
                }

                return File.ReadAllText(AssetDatabase.GUIDToAssetPath(templateGUIDs[0]));
            }
        }

        [MenuItem("Assets/Code Helpers/Create Child Class")]
        private static void AddChildClass()
            => PerformCreation(ECreationMode.ChildClass, "#REFCLASSNAME#Child");

        [MenuItem("Assets/Code Helpers/Create Child Class", true)]
        private static bool AddChildClassValidation()
            => Selection.activeObject && Selection.activeObject is MonoScript && Selection.assetGUIDs.Length == 1;

        [MenuItem("Assets/Code Helpers/Create Custom Inspector")]
        private static void AddCustomInspectorClass()
            => PerformCreation(ECreationMode.CustomInspector, "#REFCLASSNAME#Editor");

        [MenuItem("Assets/Code Helpers/Create Custom Inspector", true)]
        private static bool AddCustomInspectorClassValidation()
            => Selection.activeObject && Selection.activeObject is MonoScript && Selection.assetGUIDs.Length == 1;

        private static void PerformCreation(ECreationMode mode, string initialFileName)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
            var scriptAsset = Selection.activeObject as MonoScript;
            if (scriptAsset == null)
                throw new Exception("scripAsset is null");
            var fileName = initialFileName.Replace("#REFCLASSNAME#", scriptAsset.GetClass().Name);

            var endNameEditHandler = ScriptableObject.CreateInstance<EndNameEditHandler>();
            endNameEditHandler.Configure(mode, assetPath, scriptAsset.GetClass());

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                0, endNameEditHandler, $"{fileName}.cs", null, null);
        }
    }
}