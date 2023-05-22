/**
 * ▄▄▄▄ ▄▄▄▄ ▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
 * █ ▄▄ ▄ ▄▄ ▄ ▄▄▄▄ ▄▄ ▄    ▄▄   ▄▄▄▄ ▄▄▄▄  ▄▄▄ ▀
 * █ ██ █ ██ █ ██ █ ██ █    ██   ██ █ ██ █ ██▀  █
 * ■ ██▄▀ ██▄█ ██▄█ ██ █ ▀▀ ██   ██▄█ ██▄▀ ▀██▄ ■
 * █ ██ █ ▄▄ █ ██ █ ██ █    ██▄▄ ██ █ ██ █  ▄██ █
 * ▄ ▀▀ ▀ ▀▀▀▀ ▀▀ ▀ ▀▀▀▀    ▀▀▀▀ ▀▀ ▀ ▀▀▀▀ ▀▀▀  █
 * ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀
 *
 * MenuTools_CreateDirectoriesTests.cs
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
using NUnit.Framework;
using UnityEditor;

namespace KYAULabs.Tools.Tests
{
    public class MenuTools_CreateDirectoriesTests
    {
        private const string Path1 = "Assets/TestDir1";
        private const string Path2 = "Assets/TestDir2";
        [Test]
        public void ToolsMenuTestCreateDirectories()
        {
            MenuTools.CreateDirectories(null, "TestDir1", "TestDir2");
            AssetDatabase.Refresh();
            DirectoryAssert.Exists(new DirectoryInfo(Path1));
            DirectoryAssert.Exists(new DirectoryInfo(Path2));
            Assert.That(AssetDatabase.DeleteAsset(Path1), Is.True);
            Assert.That(AssetDatabase.DeleteAsset(Path2), Is.True);
        }

        [Test]
        public void ToolsMenuTestCreateDirectoriesNested()
        {
            MenuTools.CreateDirectories("TestDir1", "TestDir3");
            AssetDatabase.Refresh();
            DirectoryAssert.Exists(new DirectoryInfo(Path1 + "/TestDir3"));
            Assert.That(AssetDatabase.DeleteAsset(Path1), Is.True);
        }

        [TearDown]
        public void Teardown()
        {
            AssetDatabase.Refresh();
        }
    }
}