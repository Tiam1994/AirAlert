using System;
using UnityEditor;
using UnityEngine;

namespace Utilities.HirerarchyGroupHeader.Editor
{
	#if UNITY_EDITOR
	[InitializeOnLoad]
	public static class HierarchyWindowGroupHeader
	{
		private static readonly HierarchyWindowGroupHeaderScriptable Scriptable;
		private static readonly GUIStyle style;

		static HierarchyWindowGroupHeader()
		{
			Scriptable = HierarchyWindowGroupHeaderScriptable.Instance;
			style = new GUIStyle();
			UpdateStyle();
			Scriptable.Changed.AddListener(UpdateStyle);

			EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
		}

		private static void UpdateStyle()
		{
			style.fontSize = Scriptable.FontSize;
			style.fontStyle = Scriptable.FontStyle;
			style.alignment = Scriptable.Alignment;
			style.normal.textColor = Scriptable.MainTextColor;
			EditorApplication.RepaintHierarchyWindow();
		}

		private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
		{
			var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

			// Fix for correct override right side of the background label
			selectionRect.width += 20;

			if (gameObject != null)
			{
				// Draw rect for main header
				if (gameObject.name.StartsWith(Scriptable.MainHeaderStartsWith, StringComparison.Ordinal))
				{
					EditorGUI.DrawRect(selectionRect, Scriptable.MainBackgroundColor);
					style.normal.textColor = Scriptable.MainTextColor;
					var labelText = gameObject.name.Replace(Scriptable.RemoveString, "");
					EditorGUI.LabelField(selectionRect, Scriptable.IsUppercaseLabel ? labelText.ToUpperInvariant() : labelText, style);
				}

				// Draw rect for additional header
				if (gameObject.name.StartsWith(Scriptable.AdditionalHeaderStartsWith, StringComparison.Ordinal))
				{
					EditorGUI.DrawRect(selectionRect, Scriptable.AdditionalBackgroundColor);
					style.normal.textColor = Scriptable.AdditionalTextColor;
					var labelText = gameObject.name.Replace(Scriptable.RemoveString, "");
					EditorGUI.LabelField(selectionRect, Scriptable.IsUppercaseLabel ? labelText.ToUpperInvariant() : labelText, style);
				}

				// Draw rect for extra header
				if (gameObject.name.StartsWith(Scriptable.ExtraHeaderStartsWith, StringComparison.Ordinal))
				{
					EditorGUI.DrawRect(selectionRect, Scriptable.ExtraBackgroundColor);
					style.normal.textColor = Scriptable.ExtraTextColor;
					var labelText = gameObject.name.Replace(Scriptable.RemoveString, "");
					EditorGUI.LabelField(selectionRect, Scriptable.IsUppercaseLabel ? labelText.ToUpperInvariant() : labelText, style);
				}

				// Draw rect for optional1 header
				if (gameObject.name.StartsWith(Scriptable.Optional1HeaderStartsWith, StringComparison.Ordinal))
				{
					EditorGUI.DrawRect(selectionRect, Scriptable.Optional1BackgroundColor);
					style.normal.textColor = Scriptable.Optional1TextColor;
					var labelText = gameObject.name.Replace(Scriptable.RemoveString, "");
					EditorGUI.LabelField(selectionRect, Scriptable.IsUppercaseLabel ? labelText.ToUpperInvariant() : labelText, style);
				}
			}
		}
	}
	#endif
}
