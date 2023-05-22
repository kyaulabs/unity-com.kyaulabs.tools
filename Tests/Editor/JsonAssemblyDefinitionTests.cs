/**
 * ▄▄▄▄ ▄▄▄▄ ▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
 * █ ▄▄ ▄ ▄▄ ▄ ▄▄▄▄ ▄▄ ▄    ▄▄   ▄▄▄▄ ▄▄▄▄  ▄▄▄ ▀
 * █ ██ █ ██ █ ██ █ ██ █    ██   ██ █ ██ █ ██▀  █
 * ■ ██▄▀ ██▄█ ██▄█ ██ █ ▀▀ ██   ██▄█ ██▄▀ ▀██▄ ■
 * █ ██ █ ▄▄ █ ██ █ ██ █    ██▄▄ ██ █ ██ █  ▄██ █
 * ▄ ▀▀ ▀ ▀▀▀▀ ▀▀ ▀ ▀▀▀▀    ▀▀▀▀ ▀▀ ▀ ▀▀▀▀ ▀▀▀  █
 * ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀
 *
 * JsonAssemblyDefinitionTest.cs
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

namespace KYAULabs.Tools.Tests
{
    public class JsonAssemblyDefinitionTests
    {
        private JsonAssemblyDefinition _jsonAssemblyDefinition { get; set; } = null;

        [SetUp]
        public void Setup()
        {
            _jsonAssemblyDefinition = new();
        }

        [Test]
        public void JsonAssemblyDefinitionTestType()
        {
            Assert.That(_jsonAssemblyDefinition, Is.TypeOf<JsonAssemblyDefinition>());
        }

        [Test]
        public void JsonAssemblyDefinitionTestName()
        {
            Assert.That(_jsonAssemblyDefinition.name, Is.TypeOf<string>());
            Assert.That(_jsonAssemblyDefinition.name, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTestRootNamespace()
        {
            Assert.That(_jsonAssemblyDefinition.rootNamespace, Is.TypeOf<string>());
            Assert.That(_jsonAssemblyDefinition.rootNamespace, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTestReferences()
        {
            Assert.That(_jsonAssemblyDefinition.references, Is.TypeOf<string[]>());
            Assert.That(_jsonAssemblyDefinition.references, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTestIncludePlatforms()
        {
            Assert.That(_jsonAssemblyDefinition.includePlatforms, Is.TypeOf<string[]>());
            Assert.That(_jsonAssemblyDefinition.includePlatforms, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTestExcludePlatforms()
        {
            Assert.That(_jsonAssemblyDefinition.excludePlatforms, Is.TypeOf<string[]>());
            Assert.That(_jsonAssemblyDefinition.excludePlatforms, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTestAllowUnsafeCode()
        {
            Assert.That(_jsonAssemblyDefinition.allowUnsafeCode, Is.TypeOf<bool>());
            Assert.That(_jsonAssemblyDefinition.allowUnsafeCode, Is.EqualTo(false));
        }

        [Test]
        public void JsonAssemblyDefinitionTestOverrideReferences()
        {
            Assert.That(_jsonAssemblyDefinition.overrideReferences, Is.TypeOf<bool>());
            Assert.That(_jsonAssemblyDefinition.overrideReferences, Is.EqualTo(false));
        }

        [Test]
        public void JsonAssemblyDefinitionTestPrecompiledReferences()
        {
            Assert.That(_jsonAssemblyDefinition.precompiledReferences, Is.TypeOf<string[]>());
            Assert.That(_jsonAssemblyDefinition.precompiledReferences, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTestAutoReferenced()
        {
            Assert.That(_jsonAssemblyDefinition.autoReferenced, Is.TypeOf<bool>());
            Assert.That(_jsonAssemblyDefinition.autoReferenced, Is.EqualTo(false));
        }

        [Test]
        public void JsonAssemblyDefinitionTestDefineConstraints()
        {
            Assert.That(_jsonAssemblyDefinition.defineConstraints, Is.TypeOf<string[]>());
            Assert.That(_jsonAssemblyDefinition.defineConstraints, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTestVersionDefines()
        {
            Assert.That(_jsonAssemblyDefinition.versionDefines, Is.TypeOf<string[]>());
            Assert.That(_jsonAssemblyDefinition.versionDefines, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTestNoEngineReferences()
        {
            Assert.That(_jsonAssemblyDefinition.noEngineReferences, Is.TypeOf<bool>());
            Assert.That(_jsonAssemblyDefinition.noEngineReferences, Is.EqualTo(false));
        }
    }
}