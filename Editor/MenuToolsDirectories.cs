/**
 * ▄▄▄▄ ▄▄▄▄ ▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
 * █ ▄▄ ▄ ▄▄ ▄ ▄▄▄▄ ▄▄ ▄    ▄▄   ▄▄▄▄ ▄▄▄▄  ▄▄▄ ▀
 * █ ██ █ ██ █ ██ █ ██ █    ██   ██ █ ██ █ ██▀  █
 * ■ ██▄▀ ██▄█ ██▄█ ██ █ ▀▀ ██   ██▄█ ██▄▀ ▀██▄ ■
 * █ ██ █ ▄▄ █ ██ █ ██ █    ██▄▄ ██ █ ██ █  ▄██ █
 * ▄ ▀▀ ▀ ▀▀▀▀ ▀▀ ▀ ▀▀▀▀    ▀▀▀▀ ▀▀ ▀ ▀▀▀▀ ▀▀▀  █
 * ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀
 *
 * MenuToolsDirectories.cs
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

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace KYAULabs.Tools
{
    /// <summary>
    /// Provides utility methods for manipulating the default project directory structure.
    /// </summary>
    public static class MenuToolsDirectories
    {
        /// <summary>
        /// Gets or sets the project path.
        /// </summary>
        public static string projectPath = string.Empty;

        /// <summary>
        /// Creates directories at the specified root path with the given directory names.
        /// </summary>
        /// <param name="rootPath">The root path for creating directories. If null, uses the default path.</param>
        /// <param name="dirs">The directory names to create.</param>
        public static void CreateDirectories(string rootPath = null, params string[] dirs)
        {
            var fullPath = Path.Combine(Application.dataPath, rootPath ?? string.Empty);
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            
            foreach (var newDir in dirs)
                Directory.CreateDirectory(Path.Combine(fullPath, newDir));
        }

        /// <summary>
        /// Creates an <see cref="JsonAssemblyDefinition"/> object with the specified name and optional tests.
        /// </summary>
        /// <param name="name">The name of the assembly.</param>
        /// <param name="tests">The type of tests for the assembly. Optional.</param>
        /// <returns>The created <see cref="JsonAssemblyDefinition"/> object.</returns>
        public static JsonAssemblyDefinition CreateAssemblyDefinitionJson(string name, string tests = null)
        {
            JsonAssemblyDefinition jsonAssemblyDefinition = new(name, "KYAULabs");

            if (tests != null)
            {
                jsonAssemblyDefinition.rootNamespace = "KYAULabs.Tests";
                jsonAssemblyDefinition.overrideReferences = true;
                jsonAssemblyDefinition.references = new[] { "UnityEngine.TestRunner", "UnityEditor.TestRunner" };
                jsonAssemblyDefinition.precompiledReferences = new[] { "nunit.framework.dll" };
                jsonAssemblyDefinition.defineConstraints = new[] { "UNITY_INCLUDE_TESTS" };
                if (tests == "EditMode" || tests == "Editor")
                {
                    jsonAssemblyDefinition.includePlatforms = new[] { "Editor" };
                }
            }
            else
            {
                jsonAssemblyDefinition.autoReferenced = true;
            }
            return jsonAssemblyDefinition;
        }

        /// <summary>
        /// Creates an assembly definition file at the specified path with the given name and optional tests.
        /// </summary>
        /// <param name="path">The path where the assembly definition file will be created.</param>
        /// <param name="name">The name of the assembly.</param>
        /// <param name="tests">The type of tests for the assembly. Optional.</param>
        public static void CreateAssemblyDefinition(string path, string name, string tests = null)
        {
            JsonAssemblyDefinition jsonAssemblyDefinition = CreateAssemblyDefinitionJson(name, tests);
            string fullPath = Path.Combine(Application.dataPath, $"{path}/{name}.asmdef");
            string assetPath = $"Assets/{path}/{name}.asmdef";

            File.WriteAllText(fullPath, jsonAssemblyDefinition.ToJson());
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Creates the initial directory structure for the project.
        /// </summary>
        /// <remarks>
        /// - Prefabs
        /// - Scenes
        /// - Scripts
        ///   - Core
        ///     - Core.asmdef
        ///   - Game.asmdef
        /// - Tests
        ///   - EditMode
        ///     - EditModeTests.asmdef
        ///   - PlayMode
        ///     - PlayModeTests.asmdef
        /// </remarks>
        [MenuItem("Tools/Project Setup/Create Initial Directories")]
        public static void CreateDefaultDirs()
        {
            projectPath ??= string.Empty;
            CreateDirectories(projectPath, "Prefabs", "Scripts", "Tests", "ThirdParty");
            CreateDirectories(projectPath + "/Scripts", "Core");
            CreateDirectories(projectPath + "/Tests", "EditMode", "PlayMode");
            CreateAssemblyDefinition(projectPath + "/Tests/EditMode", "EditModeTests", "EditMode");
            CreateAssemblyDefinition(projectPath + "/Tests/PlayMode", "PlayModeTests", "PlayMode");
            CreateAssemblyDefinition(projectPath + "/Scripts", "Game");
            CreateAssemblyDefinition(projectPath + "/Scripts/Core", "Core");
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Creates additional directories (client-side) for the project.
        /// </summary>
        /// <remarks>
        /// - Art
        ///   - Animations
        ///   - Materials
        ///   - Meshes
        ///   - Particles
        ///   - Textures
        /// - Audio
        ///   - Music
        ///   - Sounds
        /// - Settings
        /// - ThirdParty
        /// - UI
        ///   - Fonts
        ///   - Icons
        /// </remarks>
        [MenuItem("Tools/Project Setup/Create Additional Directories")]
        public static void CreateAdditionalDirs()
        {
            projectPath ??= string.Empty;
            CreateDirectories(projectPath, "Art", "Audio", "Settings", "ThirdParty", "UI");
            CreateDirectories(projectPath + "/Art", "Animations", "Materials", "Meshes", "Particles", "Textures");
            CreateDirectories(projectPath + "/Audio", "Music", "Sounds");
            CreateDirectories(projectPath + "/UI", "Fonts", "Icons");
            AssetDatabase.Refresh();
        }
    }
}
