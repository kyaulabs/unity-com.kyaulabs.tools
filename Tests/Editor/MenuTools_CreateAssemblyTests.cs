/**
 * ▄▄▄▄ ▄▄▄▄ ▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
 * █ ▄▄ ▄ ▄▄ ▄ ▄▄▄▄ ▄▄ ▄    ▄▄   ▄▄▄▄ ▄▄▄▄  ▄▄▄ ▀
 * █ ██ █ ██ █ ██ █ ██ █    ██   ██ █ ██ █ ██▀  █
 * ■ ██▄▀ ██▄█ ██▄█ ██ █ ▀▀ ██   ██▄█ ██▄▀ ▀██▄ ■
 * █ ██ █ ▄▄ █ ██ █ ██ █    ██▄▄ ██ █ ██ █  ▄██ █
 * ▄ ▀▀ ▀ ▀▀▀▀ ▀▀ ▀ ▀▀▀▀    ▀▀▀▀ ▀▀ ▀ ▀▀▀▀ ▀▀▀  █
 * ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀
 *
 * MenuTools_CreateAssemblyTests.cs
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
using NUnit.Framework.Internal;
using UnityEditor;
using UnityEngine;

namespace KYAULabs.Tools.Tests
{
    public class MenuTools_CreateAssemblyTests
    {
        private JsonAssemblyDefinition NewAssemblyDefinition { get; set; } = null;

        [Test]
        public void MenuTools_CreateAssemblyTest_ObjectFormat()
        {
            // Arrange
            string name = "Tests";

            // Act
            NewAssemblyDefinition = MenuTools.CreateAssemblyDefinitionJson(name);

            // Assert
            Assert.That(NewAssemblyDefinition.name, Is.EqualTo(name));
            Assert.That(NewAssemblyDefinition.rootNamespace, Is.EqualTo("KYAULabs"));
            Assert.That(NewAssemblyDefinition.autoReferenced, Is.True);
        }

        [Test]
        public void MenuTools_CreateAssemblyTest_TestObjectFormat()
        {
            // Arrange
            string name = "Testing";

            // Act
            NewAssemblyDefinition = MenuTools.CreateAssemblyDefinitionJson(name, "Editor");

            // Assert
            Assert.That(NewAssemblyDefinition.name, Is.EqualTo(name));
            Assert.That(NewAssemblyDefinition.rootNamespace, Is.EqualTo("KYAULabs.Tests"));
            Assert.That(NewAssemblyDefinition.overrideReferences, Is.True);
            Assert.That(NewAssemblyDefinition.references, Contains.Item("UnityEditor.TestRunner"));
            Assert.That(NewAssemblyDefinition.references, Contains.Item("UnityEngine.TestRunner"));
            Assert.That(NewAssemblyDefinition.precompiledReferences, Contains.Item("nunit.framework.dll"));
            Assert.That(NewAssemblyDefinition.defineConstraints, Contains.Item("UNITY_INCLUDE_TESTS"));
        }

        [Test]
        public void MenuTools_CreateAssemblyTest_AssetExists_ShouldBeTrue()
        {
            // Arrange
            string assetPath = $"Assets/Tests/TestAssembly1.asmdef";

            // Act
            MenuTools.CreateAssemblyDefinition("Tests", "TestAssembly1");

            // Assert
            FileAssert.Exists(assetPath);
            Assert.That(AssetDatabase.LoadAssetAtPath<Object>(assetPath) ? true : false, Is.True);
            Assert.That(AssetDatabase.DeleteAsset(assetPath), Is.True);
        }

        [TearDown]
        public void Teardown()
        {
            AssetDatabase.Refresh();
        }
    }
}