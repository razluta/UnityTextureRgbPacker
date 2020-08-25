using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityTextureRgbPacker
{
    public static class ProjectUtilities
    {
        private const string AllAssetsFolderRelativePath = "Assets";
        
        public static string GetApplicationDataPath()
        {
            return Application.dataPath.ToString();
        }

        public static bool IsPathInProject(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                return false;
            }
            
            var uniformPath = Path.GetFullPath(path);
            var uniformProjectPath = Path.GetFullPath(GetApplicationDataPath());

            return uniformPath.Contains(uniformProjectPath);
        }

        public static string GetRelativeDirectoryPath(string absoluteDirectoryPath)
        {
            var relativeDirectoryPath = string.Empty;
            
            if (!IsPathInProject(absoluteDirectoryPath))
            {
                return relativeDirectoryPath;
            }
            
            relativeDirectoryPath = absoluteDirectoryPath.Replace(
                GetApplicationDataPath(), AllAssetsFolderRelativePath);
            return relativeDirectoryPath;
        }

        public static string GetRelativeAssetPath(string absoluteAssetPath)
        {
            var relativeAssetPath = string.Empty;

            if (!File.Exists(absoluteAssetPath))
            {
                return relativeAssetPath;
            }

            var uniformAbsolutePath = Path.GetFullPath(absoluteAssetPath);
            var uniformProjectPath = Path.GetFullPath(GetApplicationDataPath());

            if (!uniformAbsolutePath.Contains(uniformProjectPath))
            {
                return relativeAssetPath;
            }

            relativeAssetPath = uniformAbsolutePath.Replace(
                uniformProjectPath, AllAssetsFolderRelativePath);
            return relativeAssetPath;
        }
        
        public static List<string> GetAllFileAssetRelativePaths()
        {
            var allAssetGuids = GetAllFileAssetGuids();
            var allAssetsRelativePaths = new List<string>();
            
            foreach (var assetGuid in allAssetGuids)
            {
                var relativeFilePath = AssetDatabase.GUIDToAssetPath(assetGuid);
                
                var unityProjectRootPath = System.IO.Directory.GetParent(
                    ProjectUtilities.GetApplicationDataPath()).ToString();
                var absoluteFilePath = Path.Combine(unityProjectRootPath, relativeFilePath);

                if (!System.IO.File.Exists(absoluteFilePath))
                {
                    continue;
                }

                allAssetsRelativePaths.Add(relativeFilePath);
            }

            return allAssetsRelativePaths;
        }

        private static string[] GetAllFileAssetGuids()
        {
            return AssetDatabase.FindAssets(null, new [] {AllAssetsFolderRelativePath});
        }
    }
}