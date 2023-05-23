/**
 * ▄▄▄▄ ▄▄▄▄ ▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
 * █ ▄▄ ▄ ▄▄ ▄ ▄▄▄▄ ▄▄ ▄    ▄▄   ▄▄▄▄ ▄▄▄▄  ▄▄▄ ▀
 * █ ██ █ ██ █ ██ █ ██ █    ██   ██ █ ██ █ ██▀  █
 * ■ ██▄▀ ██▄█ ██▄█ ██ █ ▀▀ ██   ██▄█ ██▄▀ ▀██▄ ■
 * █ ██ █ ▄▄ █ ██ █ ██ █    ██▄▄ ██ █ ██ █  ▄██ █
 * ▄ ▀▀ ▀ ▀▀▀▀ ▀▀ ▀ ▀▀▀▀    ▀▀▀▀ ▀▀ ▀ ▀▀▀▀ ▀▀▀  █
 * ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀
 *
 * MenuToolsPackages.cs
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

using System.Collections.Generic;
using System;
using UnityEditor.PackageManager;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace KYAULabs.Tools
{
    /// <summary>
    /// Provides package utility methods for Tools menu.
    /// </summary>
    public static class MenuToolsPackages
    {
        private static AddAndRemoveRequest AddAndRemoveRequest;

        /// <summary>
        /// Removes unnecessary packages and adds required packages.
        /// </summary>
        /// <remarks>
        /// Add:
        /// - Code Coverage
        /// - Input System
        /// Remove:
        /// - JetBrains Rider Editor
        /// - Unity Modules: Android JNI, AssetBundle, Unity Analytics, VR, XR
        /// - Version Control
        /// - Visual Scripting
        /// - Visual Studio Code Editor
        /// </remarks>
        [MenuItem("Tools/Project Setup/Add \u2215 Remove Default Packages")]
        public static void RemovePackageBloat()
        {
            string[] packagesToRemove = new[]
            {
                "com.unity.collab-proxy",
                "com.unity.ide.rider",
                "com.unity.ide.vscode",
                "com.unity.modules.androidjni",
                "com.unity.modules.assetbundle",
                "com.unity.modules.unityanalytics",
                "com.unity.modules.unitywebrequestassetbundle",
                "com.unity.modules.unitywebrequestaudio",
                "com.unity.modules.unitywebrequesttexture",
                "com.unity.modules.vr",
                "com.unity.modules.xr",
                "com.unity.visualscripting"
            };
            string[] packagesToAdd = new[]
            {
                "com.unity.inputsystem",
                "com.unity.testtools.codecoverage"
            };

            List<string> existingPackagesToRemove = new List<string>();
            foreach (string package in packagesToRemove)
            {
                if (IsPackageInstalled(package))
                {
                    existingPackagesToRemove.Add(package);
                }
            }
            packagesToRemove = existingPackagesToRemove.ToArray();

            if (packagesToRemove.Length > 0)
            {
                AddAndRemoveRequest = Client.AddAndRemove(packagesToAdd, packagesToRemove);
                Debug.Log($"Status: {AddAndRemoveRequest.Status}");
                EditorApplication.update += OnWaitForPackageUpdates;
            }
        }

        /// <summary>
        /// Checks if a package is installed in the Unity project.
        /// </summary>
        /// <param name="packageName">The name of the package to check.</param>
        /// <returns>True if the package is installed, false otherwise.</returns>
        public static bool IsPackageInstalled(string packageName)
        {
            PackageInfo[] installedPackages = PackageInfo.GetAllRegisteredPackages();
            foreach (PackageInfo package in installedPackages)
            {
                if (package.name == packageName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Handles the update event while waiting for package updates.
        /// </summary>
        private static void OnWaitForPackageUpdates()
        {
            if (!AddAndRemoveRequest.IsCompleted)
            {
                return;
            }
            EditorApplication.update -= OnWaitForPackageUpdates;
            switch (AddAndRemoveRequest.Status)
            {
                case StatusCode.Success:
                    Debug.Log($"Packages updated successful: {AddAndRemoveRequest.Result}");
                    EndUpdate(0);
                    break;
                case StatusCode.Failure:
                    Debug.Log($"Updating package list failed! {AddAndRemoveRequest.Error}");
                    EndUpdate(201);
                    break;
                case StatusCode.InProgress:
                    Debug.Log("Retrieving package list is still in progress!");
                    EndUpdate(202);
                    break;
                default:
                    Debug.Log($"Unsupported status {AddAndRemoveRequest.Status}!");
                    EndUpdate(203);
                    break;
            }
        }

        /// <summary>
        /// Ends the package update process.
        /// </summary>
        /// <param name="returnValue">The exit code value.</param>
        private static void EndUpdate(int returnValue)
        {
            if (Application.isBatchMode)
            {
                EditorApplication.Exit(returnValue);
            }
            else
            {
                if (returnValue != 0)
                {
                    throw new Exception($"BuildScript ended with non-zero exitCode: {returnValue}");
                }
                Debug.Log($"Successfully updated packages");
            }
        }
    }
}
