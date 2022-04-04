using System.IO;
using UnityEditor;

namespace Ro3dDevSet.Editor
{
    public static class UnityPackageExporter
    {
        private const string packageName = "Ro3dDevSet";
        private const string packagePath = "Ro3dDevSet";
        private const string exportPath = "Builds";

        public static void Export()
        {
            var exportName = $"{exportPath}/{packageName}.unitypackage";
            var directory = new FileInfo(exportName).Directory;
            if (directory is { Exists: false })
            {
                directory.Create();
            }

            AssetDatabase.ExportPackage($"Packages/{packagePath}", exportName, ExportPackageOptions.Recurse);
        }
    }
}