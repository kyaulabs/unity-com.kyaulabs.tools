/**
 * ▄▄▄▄ ▄▄▄▄ ▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
 * █ ▄▄ ▄ ▄▄ ▄ ▄▄▄▄ ▄▄ ▄    ▄▄   ▄▄▄▄ ▄▄▄▄  ▄▄▄ ▀
 * █ ██ █ ██ █ ██ █ ██ █    ██   ██ █ ██ █ ██▀  █
 * ■ ██▄▀ ██▄█ ██▄█ ██ █ ▀▀ ██   ██▄█ ██▄▀ ▀██▄ ■
 * █ ██ █ ▄▄ █ ██ █ ██ █    ██▄▄ ██ █ ██ █  ▄██ █
 * ▄ ▀▀ ▀ ▀▀▀▀ ▀▀ ▀ ▀▀▀▀    ▀▀▀▀ ▀▀ ▀ ▀▀▀▀ ▀▀▀  █
 * ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀
 *
 * MenuTools_CreateDefaultsTests.cs
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

using NUnit.Framework;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace KYAULabs.Tools.Tests
{
    public class MenuTools_CreateDefaultsTests
    {
        private string[] expectedFolders;
        [SetUp]
        public void Setup()
        {
            MenuToolsDirectories.projectPath = "AssetsTest";
        }

        [Test]
        public void MenuTools_CreateDefaultFoldersTest_AssetsExist_ShouldBeTrue()
        {
            // Arrange
            expectedFolders = new[] {
                "Assets/AssetsTest/Prefabs",
                "Assets/AssetsTest/Scripts",
                "Assets/AssetsTest/Scripts/Core",
                "Assets/AssetsTest/Tests",
                "Assets/AssetsTest/Tests/EditMode",
                "Assets/AssetsTest/Tests/PlayMode"
            };

            // Act
            MenuToolsDirectories.CreateDefaultDirs();

            // Assert
            foreach (string folderPath in expectedFolders)
                DirectoryAssert.Exists(new DirectoryInfo(folderPath));
            Assert.That(AssetDatabase.LoadAssetAtPath<Object>("Assets/AssetsTest/Tests/EditMode/EditModeTests.asmdef") ? true : false, Is.True);
            Assert.That(AssetDatabase.LoadAssetAtPath<Object>("Assets/AssetsTest/Tests/PlayMode/PlayModeTests.asmdef") ? true : false, Is.True);
        }

        [Test]
        public void MenuTools_CreateAdditionalFoldersTest_AssetsExist_ShouldBeTrue()
        {
            // Arrange
            expectedFolders = new[]
            {
                "Assets/AssetsTest/Art",
                "Assets/AssetsTest/Art/Animations",
                "Assets/AssetsTest/Art/Materials",
                "Assets/AssetsTest/Art/Meshes",
                "Assets/AssetsTest/Art/Particles",
                "Assets/AssetsTest/Art/Textures",
                "Assets/AssetsTest/Audio",
                "Assets/AssetsTest/Audio/Music",
                "Assets/AssetsTest/Audio/Sounds",
                "Assets/AssetsTest/Settings",
                "Assets/AssetsTest/ThirdParty",
                "Assets/AssetsTest/UI",
                "Assets/AssetsTest/UI/Fonts",
                "Assets/AssetsTest/UI/Icons"
            };

            // Act
            MenuToolsDirectories.CreateAdditionalDirs();

            // Assert
            foreach (string folderPath in expectedFolders)
                DirectoryAssert.Exists(new DirectoryInfo(folderPath));
        }

        [TearDown]
        public void Teardown()
        {
            AssetDatabase.DeleteAsset("Assets/AssetsTest");
        }
    }
}
