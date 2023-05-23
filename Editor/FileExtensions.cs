/**
 * ▄▄▄▄ ▄▄▄▄ ▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
 * █ ▄▄ ▄ ▄▄ ▄ ▄▄▄▄ ▄▄ ▄    ▄▄   ▄▄▄▄ ▄▄▄▄  ▄▄▄ ▀
 * █ ██ █ ██ █ ██ █ ██ █    ██   ██ █ ██ █ ██▀  █
 * ■ ██▄▀ ██▄█ ██▄█ ██ █ ▀▀ ██   ██▄█ ██▄▀ ▀██▄ ■
 * █ ██ █ ▄▄ █ ██ █ ██ █    ██▄▄ ██ █ ██ █  ▄██ █
 * ▄ ▀▀ ▀ ▀▀▀▀ ▀▀ ▀ ▀▀▀▀    ▀▀▀▀ ▀▀ ▀ ▀▀▀▀ ▀▀▀  █
 * ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀
 *
 * FileExtensions.cs
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

using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;

namespace KYAULabs.Tools
{
    /// <summary>
    /// Initializes and manages file extensions in the Unity editor GUI.
    /// </summary>
    [InitializeOnLoad]
    public class FileExtensionGUI
    {
        private static GUIStyle _style;
        private static readonly StringBuilder sb = new();
        private static string _selectedGuid;
        private static readonly HashSet<string> ShowExtExclude = new()
        {
            ".asmdef"
        };

        /// <summary>
        /// Static constructor that subscribes to events.
        /// </summary>
        static FileExtensionGUI()
        {
            EditorApplication.projectWindowItemOnGUI += HandleOnGUI;
            Selection.selectionChanged += () =>
            {
                if (Selection.activeObject != null)
                    AssetDatabase.TryGetGUIDAndLocalFileIdentifier(Selection.activeObject, out _selectedGuid, out long id);
            };

        }

        private static bool ValidString(string str)
        {
            return !string.IsNullOrEmpty(str) && str.Length > 7;
        }

        private static string _lastGUID = string.Empty;

        private static void HandleOnGUI(string guid, Rect selectionRect)
        {
            if (guid == _lastGUID)
                return;
            else
                _lastGUID = guid;

            if (IsThumbnailsView)
                return;

            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (0 >= path.Length)
                return;

            var attr = File.GetAttributes(path);

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                return;

            var nameRaw = Path.GetFileNameWithoutExtension(path);
            var extRaw = Path.GetExtension(path);

            if (ShowExtExclude.Contains(extRaw))
                return;

            var selected = false;
            if (ValidString(guid) && ValidString(_selectedGuid))
                selected = string.Compare(guid, 0, _selectedGuid, 0, 6) == 0;


            sb.Clear().Append(extRaw);

            var ext = sb.ToString();

            _style ??= new GUIStyle(EditorStyles.label);

            _style.normal.textColor = selected ? new Color32(255, 255, 255, 255) : new Color32(220, 220, 220, 220);
            var extSize = _style.CalcSize(new GUIContent(ext));
            var nameSize = _style.CalcSize(new GUIContent(nameRaw));
            selectionRect.x += nameSize.x + (IsSingleColumnView ? 15 : 18);
            selectionRect.width = nameSize.x + 1 + extSize.x;


            var offsetRect = new Rect(selectionRect.position, selectionRect.size);
            EditorGUI.LabelField(offsetRect, ext, _style);
        }

        /// <summary>
        /// Checks if the project window is in the thumbnails view.
        /// </summary>
        private static bool IsThumbnailsView
        {
            get
            {
                var projectWindow = GetProjectWindow();
                var gridSize = projectWindow.GetType().GetProperty("listAreaGridSize", BindingFlags.Instance | BindingFlags.Public);
                var columnsCount = (int)projectWindow.GetType().GetField("m_ViewMode", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(projectWindow);
                return columnsCount == 1 && (float)gridSize.GetValue(projectWindow, null) > 16f;
            }
        }

        /// <summary>
        /// Checks if the project window is in the single column view.
        /// </summary>
        private static bool IsSingleColumnView
        {
            get
            {
                var projectWindow = GetProjectWindow();
                var columnsCount = (int)projectWindow.GetType().GetField("m_ViewMode", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(projectWindow);
                return columnsCount == 0;
            }
        }

        private static EditorWindow GetProjectWindow()
        {
            if (EditorWindow.focusedWindow != null && EditorWindow.focusedWindow.titleContent.text == "Project")
            {
                return EditorWindow.focusedWindow;
            }

            return GetExistingWindowByName("Project");
        }

        private static EditorWindow GetExistingWindowByName(string name)
        {
            EditorWindow[] windows = Resources.FindObjectsOfTypeAll<EditorWindow>();
            foreach (EditorWindow item in windows)
            {
                if (item.titleContent.text == name)
                {
                    return item;
                }
            }

            return default(EditorWindow);
        }
    }
}