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
        private JsonAssemblyDefinition NewAssemblyDefinition { get; set; } = null;

        [SetUp]
        public void Setup()
        {
            NewAssemblyDefinition = new("Testing");
        }

        [Test]
        public void JsonAssemblyDefinitionTest_Type()
        {
            Assert.That(NewAssemblyDefinition, Is.TypeOf<JsonAssemblyDefinition>());
        }

        [Test]
        public void JsonAssemblyDefinitionTest_name_ShouldBeString()
        {
            Assert.That(NewAssemblyDefinition.name, Is.TypeOf<string>());
        }
        [Test]
        public void JsonAssemblyDefinitionTest_name_ShouldBeEqualToTesting()
        {
            Assert.That(NewAssemblyDefinition.name, Is.EqualTo("Testing"));
        }

        [Test]
        public void JsonAssemblyDefinitionTest_rootNamespace_ShouldBeString()
        {
            Assert.That(NewAssemblyDefinition.rootNamespace, Is.TypeOf<string>());
        }
        [Test]
        public void JsonAssemblyDefinitionTest_rootNamespace_ShouldBeEmpty()
        {
            Assert.That(NewAssemblyDefinition.rootNamespace, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTest_references_ShouldBeStringArray()
        {
            Assert.That(NewAssemblyDefinition.references, Is.TypeOf<string[]>());
        }
        [Test]
        public void JsonAssemblyDefinitionTest_references_ShouldBeEmpty()
        {
            Assert.That(NewAssemblyDefinition.references, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTest_includePlatforms_ShouldBeStringArray()
        {
            Assert.That(NewAssemblyDefinition.includePlatforms, Is.TypeOf<string[]>());
        }
        [Test]
        public void JsonAssemblyDefinitionTest_includePlatforms_ShouldBeempty()
        {
            Assert.That(NewAssemblyDefinition.includePlatforms, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTest_excludePlatforms_ShouldBeStringArray()
        {
            Assert.That(NewAssemblyDefinition.excludePlatforms, Is.TypeOf<string[]>());
        }
        [Test]
        public void JsonAssemblyDefinitionTest_excludePlatforms_ShouldBeEmpty()
        {
            Assert.That(NewAssemblyDefinition.excludePlatforms, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTest_precompiledReferences_ShouldBeStringArray()
        {
            Assert.That(NewAssemblyDefinition.precompiledReferences, Is.TypeOf<string[]>());
        }
        [Test]
        public void JsonAssemblyDefinitionTest_precompiledReferences_ShouldBeEmpty()
        {
            Assert.That(NewAssemblyDefinition.precompiledReferences, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTest_defineConstraints_ShouldBeString()
        {
            Assert.That(NewAssemblyDefinition.defineConstraints, Is.TypeOf<string[]>());
        }
        [Test]
        public void JsonAssemblyDefinitionTest_defineConstraints_ShouldBeEmpty()
        {
            Assert.That(NewAssemblyDefinition.defineConstraints, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTest_versionDefines_ShouldBeString()
        {
            Assert.That(NewAssemblyDefinition.versionDefines, Is.TypeOf<string[]>());
        }
        [Test]
        public void JsonAssemblyDefinitionTest_versionDefines_ShouldBeEmpty()
        {
            Assert.That(NewAssemblyDefinition.versionDefines, Is.Empty);
        }

        [Test]
        public void JsonAssemblyDefinitionTest_allowUnsafeCode_ShouldBeBool()
        {
            Assert.That(NewAssemblyDefinition.allowUnsafeCode, Is.TypeOf<bool>());
        }
        [Test]
        public void JsonAssemblyDefinitionTest_allowUnsafeCode_ShouldNotBeNull()
        {
            Assert.That(NewAssemblyDefinition.allowUnsafeCode, Is.Not.Null);
        }
        [Test]
        public void JsonAssemblyDefinitionTest_allowUnsafeCode_ShouldBeFalse()
        {
            Assert.That(NewAssemblyDefinition.allowUnsafeCode, Is.False);
        }

        [Test]
        public void JsonAssemblyDefinitionTest_overrideReferences_ShouldBeBool()
        {
            Assert.That(NewAssemblyDefinition.overrideReferences, Is.TypeOf<bool>());
        }
        [Test]
        public void JsonAssemblyDefinitionTest_overrideReferences_ShouldNotBeNull()
        {
            Assert.That(NewAssemblyDefinition.overrideReferences, Is.Not.Null);
        }
        [Test]
        public void JsonAssemblyDefinitionTest_overrideReferences_ShouldBeFalse()
        {
            Assert.That(NewAssemblyDefinition.overrideReferences, Is.False);
        }

        [Test]
        public void JsonAssemblyDefinitionTest_autoReferenced_ShouldBeBool()
        {
            Assert.That(NewAssemblyDefinition.autoReferenced, Is.TypeOf<bool>());
        }
        [Test]
        public void JsonAssemblyDefinitionTest_autoReferenced_ShouldNotBeNull()
        {
            Assert.That(NewAssemblyDefinition.autoReferenced, Is.Not.Null);
        }
        [Test]
        public void JsonAssemblyDefinitionTest_autoReferenced_ShouldBeFalse()
        {
            Assert.That(NewAssemblyDefinition.autoReferenced, Is.False);
        }

        [Test]
        public void JsonAssemblyDefinitionTest_noEngineReferences_ShouldBeBool()
        {
            Assert.That(NewAssemblyDefinition.noEngineReferences, Is.TypeOf<bool>());
        }
        [Test]
        public void JsonAssemblyDefinitionTest_noEngineReferences_ShouldNotBeNull()
        {
            Assert.That(NewAssemblyDefinition.noEngineReferences, Is.Not.Null);
        }
        [Test]
        public void JsonAssemblyDefinitionTest_noEngineReferences_ShouldBeFalse()
        {
            Assert.That(NewAssemblyDefinition.noEngineReferences, Is.False);
        }

        [Test]
        public void JsonAssemblyDefinitionTest_name_InstantiateWithNull_ThrowException()
        {
            Assert.That(() => new JsonAssemblyDefinition(null), Throws.ArgumentNullException);
        }

        [Test]
        public void JsonAssemblyDefinitionTest_rootNamespace_InstantiateWithNull_ThrowException()
        {
            Assert.That(() => new JsonAssemblyDefinition("Test",null), Throws.ArgumentNullException);
        }
    }
}