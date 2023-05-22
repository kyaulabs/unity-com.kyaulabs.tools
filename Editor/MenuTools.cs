/**
 * ▄▄▄▄ ▄▄▄▄ ▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
 * █ ▄▄ ▄ ▄▄ ▄ ▄▄▄▄ ▄▄ ▄    ▄▄   ▄▄▄▄ ▄▄▄▄  ▄▄▄ ▀
 * █ ██ █ ██ █ ██ █ ██ █    ██   ██ █ ██ █ ██▀  █
 * ■ ██▄▀ ██▄█ ██▄█ ██ █ ▀▀ ██   ██▄█ ██▄▀ ▀██▄ ■
 * █ ██ █ ▄▄ █ ██ █ ██ █    ██▄▄ ██ █ ██ █  ▄██ █
 * ▄ ▀▀ ▀ ▀▀▀▀ ▀▀ ▀ ▀▀▀▀    ▀▀▀▀ ▀▀ ▀ ▀▀▀▀ ▀▀▀  █
 * ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀
 *
 * MenuTools.cs
 * Copyright (C) 2023 KYAU Labs (https://kyaulabs.com)
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System.IO;
using UnityEditor;
using UnityEngine;

namespace KYAULabs.Tools
{
    public static class MenuTools
    {
        public static string projectPath = string.Empty;
        public static void CreateDirectories(string rootPath = null, params string[] dirs)
        {
            var fullPath = Path.Combine(Application.dataPath, rootPath ?? string.Empty);
            if (!Directory.Exists(fullPath))
            {
                //Debug.Log($"Creating: {fullPath}");
                Directory.CreateDirectory(fullPath);
            }
            
            foreach (var newDirectory in dirs)
            {
                var newDir = Path.Combine(fullPath, newDirectory);
                //Debug.Log($"Also Creating: {newDir}");
                Directory.CreateDirectory(Path.Combine(fullPath, newDirectory));
            }
        }

        public static string GetAssetsDirectory()
        {
            return Application.dataPath;
        }

        public static JsonAssemblyDefinition CreateAssemblyDefinitionJson(string name, string tests = null)
        {
            JsonAssemblyDefinition json = new()
            {
                name = name,
                rootNamespace = "KYAULabs"
            };

            if (tests != null)
            {
                json.rootNamespace = "KYAULabs.Tests";
                json.overrideReferences = true;
                json.references = new[] { "UnityEngine.TestRunner", "UnityEditor.TestRunner" };
                json.precompiledReferences = new[] { "nunit.framework.dll" };
                json.defineConstraints = new[] { "UNITY_INCLUDE_TESTS" };
                if (tests == "EditMode" || tests == "Editor")
                {
                    json.includePlatforms = new[] { "Editor" };
                }
            }
            else
            {
                json.autoReferenced = true;
            }
            return json;
        }

        public static void CreateAssemblyDefinition(string path, string name, string tests = null)
        {
            JsonAssemblyDefinition json = CreateAssemblyDefinitionJson(name, tests);

            string text = JsonUtility.ToJson(json);
            string fullPath = Path.Combine(Application.dataPath, $"{path}/{name}.asmdef");
            string assetPath = $"Assets/{path}/{name}.asmdef";
            //Debug.Log($"Assembly Full Path: {fullPath}");

            File.WriteAllText(fullPath, text);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        [MenuItem("Tools/Project Setup/Default Folders")]
        public static void CreateDefaultFolders()
        {
            projectPath = projectPath ?? string.Empty;
            /*
             * Prefabs
             * Scenes
             * Scripts
             * - Core
             *   - Core.asmdef
             * - Game.asmdef
             * Tests
             * - EditMode
             *   - EditModeTests.asmdef
             * - PlayMode
             *   - PlayModeTests.asmdef
             */

            CreateDirectories(projectPath, "Prefabs", "Scripts", "Tests", "ThirdParty");
            CreateDirectories(projectPath + "/Scripts", "Core");
            CreateDirectories(projectPath + "/Tests", "EditMode", "PlayMode");
            CreateAssemblyDefinition(projectPath + "/Tests/EditMode", "EditModeTests", "EditMode");
            CreateAssemblyDefinition(projectPath + "/Tests/PlayMode", "PlayModeTests", "PlayMode");
            CreateAssemblyDefinition(projectPath + "/Scripts", "Game");
            CreateAssemblyDefinition(projectPath + "/Scripts/Core", "Core");
            AssetDatabase.Refresh();
        }
        [MenuItem("Tools/Project Setup/Additional Folders")]
        public static void CreateAdditionalFolders()
        {
            projectPath = projectPath ?? string.Empty;
            /*
             * Art
             * - Animations
             * - Materials
             * - Meshes
             * - Particles
             * - Textures
             * Audio
             * - Music
             * - Sounds
             * Settings
             * ThirdParty
             * UI
             * - Fonts
             * - Icons
             */

            CreateDirectories(projectPath, "Art", "Audio", "Settings", "ThirdParty", "UI");
            CreateDirectories(projectPath + "/Art", "Animations", "Materials", "Meshes", "Particles", "Textures");
            CreateDirectories(projectPath + "/Audio", "Music", "Sounds");
            CreateDirectories(projectPath + "/UI", "Fonts", "Icons");
            AssetDatabase.Refresh();
        }
    }
}
