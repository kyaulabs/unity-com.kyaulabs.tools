/**
* ▄▄▄▄ ▄▄▄▄ ▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
* █ ▄▄ ▄ ▄▄ ▄ ▄▄▄▄ ▄▄ ▄    ▄▄   ▄▄▄▄ ▄▄▄▄  ▄▄▄ ▀
* █ ██ █ ██ █ ██ █ ██ █    ██   ██ █ ██ █ ██▀  █
* ■ ██▄▀ ██▄█ ██▄█ ██ █ ▀▀ ██   ██▄█ ██▄▀ ▀██▄ ■
* █ ██ █ ▄▄ █ ██ █ ██ █    ██▄▄ ██ █ ██ █  ▄██ █
* ▄ ▀▀ ▀ ▀▀▀▀ ▀▀ ▀ ▀▀▀▀    ▀▀▀▀ ▀▀ ▀ ▀▀▀▀ ▀▀▀  █
* ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀
*
* JsonAssemblyDefinition.cs
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
using UnityEngine;

namespace KYAULabs.Tools
{
    /// <summary>
    /// Represents a JSON assembly definition.
    /// </summary>
    [Serializable]
    public sealed class JsonAssemblyDefinition
    {
        /// <summary>
        /// Gets or sets the name of the assembly.
        /// </summary>
        public string name = string.Empty;

        /// <summary>
        /// Gets or sets the root namespace of the assembly.
        /// </summary>
        public string rootNamespace = string.Empty;

        /// <summary>
        /// Gets or sets the references of the assembly.
        /// </summary>
        public string[] references = Array.Empty<string>();

        /// <summary>
        /// Gets or sets the include platforms of the assembly.
        /// </summary>
        public string[] includePlatforms = Array.Empty<string>();

        /// <summary>
        /// Gets or sets the exclude platforms of the assembly.
        /// </summary>
        public string[] excludePlatforms = Array.Empty<string>();

        /// <summary>
        /// Gets or sets whether unsafe code is allowed in the assembly.
        /// </summary>
        public bool allowUnsafeCode;

        /// <summary>
        /// Gets or sets whether references should be overridden in the assembly.
        /// </summary>
        public bool overrideReferences;

        /// <summary>
        /// Gets or sets the precompiled references of the assembly.
        /// </summary>
        public string[] precompiledReferences = Array.Empty<string>();

        /// <summary>
        /// Gets or sets whether the assembly is auto-referenced.
        /// </summary>
        public bool autoReferenced;

        /// <summary>
        /// Gets or sets the define constraints of the assembly.
        /// </summary>
        public string[] defineConstraints = Array.Empty<string>();

        /// <summary>
        /// Gets or sets the version defines of the assembly.
        /// </summary>
        public string[] versionDefines = Array.Empty<string>();

        /// <summary>
        /// Gets or sets whether the assembly has no engine references.
        /// </summary>
        public bool noEngineReferences;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonAssemblyDefinition"/> class with the specified name.
        /// </summary>
        /// <param name="_name">The name of the assembly.</param>
        public JsonAssemblyDefinition(string _name)
        {
            name = _name ?? throw new ArgumentNullException(nameof(_name));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonAssemblyDefinition"/> class with the specified name and root namespace.
        /// </summary>
        /// <param name="_name">The name of the assembly.</param>
        /// <param name="_rootNamespace">The root namespace of the assembly.</param>
        public JsonAssemblyDefinition(string _name, string _rootNamespace)
        {
            name = _name ?? throw new ArgumentNullException(nameof(_name));
            rootNamespace = _rootNamespace ?? throw new ArgumentNullException(nameof(_rootNamespace));
        }

        /// <summary>
        /// Converts the <see cref="JsonAssemblyDefinition"/> object to a JSON string.
        /// </summary>
        /// <returns>The JSON representation of the object.</returns>
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}