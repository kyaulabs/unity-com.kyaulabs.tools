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
        private JsonAssemblyDefinition _json;

        [Test]
        public void ToolsMenuCreateAssemblyTestFormat()
        {
            _json = MenuTools.CreateAssemblyDefinitionJson("Tests");
            Assert.That(_json.name, Is.EqualTo("Tests"));
            Assert.That(_json.rootNamespace, Is.EqualTo("KYAULabs"));
            Assert.That(_json.autoReferenced, Is.True);
        }

        [Test]
        public void ToolsMenuCreateAssemblyTestFormatTests()
        {
            _json = MenuTools.CreateAssemblyDefinitionJson("Testing", "Editor");
            Assert.That(_json.name, Is.EqualTo("Testing"));
            Assert.That(_json.rootNamespace, Is.EqualTo("KYAULabs.Tests"));
            Assert.That(_json.overrideReferences, Is.True);
            Assert.That(_json.references, Contains.Item("UnityEditor.TestRunner"));
            Assert.That(_json.references, Contains.Item("UnityEngine.TestRunner"));
            Assert.That(_json.precompiledReferences, Contains.Item("nunit.framework.dll"));
            Assert.That(_json.defineConstraints, Contains.Item("UNITY_INCLUDE_TESTS"));
        }

        [Test]
        public void ToolsMenuCreateAssemblyTestAssemblyTest()
        {
            MenuTools.CreateAssemblyDefinition("Tests", "TestAssembly1");
            string assetPath = $"Assets/Tests/TestAssembly1.asmdef";
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