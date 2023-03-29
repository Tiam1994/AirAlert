using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.HirerarchyGroupHeader.Editor
{
	#if UNITY_EDITOR
	public class HierarchyWindowGroupHeaderScriptable : ScriptableObject
	{
		[HideInInspector]
		public UnityEvent Changed;

		[Header("Main header settings:")]
		public string MainHeaderStartsWith = "---";
		public Color MainTextColor = new Color32(60, 115, 140, 255);
		public Color MainBackgroundColor = new Color32(56, 56, 56, 255);

		[Header("Additional header settings:")]
		public string AdditionalHeaderStartsWith = "----";
		public Color AdditionalTextColor = new Color32(133, 164, 89, 255);
		public Color AdditionalBackgroundColor = new Color32(56, 56, 56, 255);

		[Header("Extra header settings:")]
		public string ExtraHeaderStartsWith = "-----";
		public Color ExtraTextColor = new Color32(235, 127, 0, 255);
		public Color ExtraBackgroundColor = new Color32(56, 56, 56, 255);

		[Header("Optional1 header settings:")]
		public string Optional1HeaderStartsWith = "------";
		public Color Optional1TextColor =  new Color32(9, 227, 99, 255);
		public Color Optional1BackgroundColor = new Color32(56, 56, 56, 255);

		[Space(20)]
		[Header("Settings:")]
		public string RemoveString = "-";
		public bool IsUppercaseLabel;
		public FontStyle FontStyle = FontStyle.Bold;
		public int FontSize = 12;
		public TextAnchor Alignment = TextAnchor.MiddleCenter;

		private static HierarchyWindowGroupHeaderScriptable _instance;
		public static HierarchyWindowGroupHeaderScriptable Instance => _instance ? _instance : (_instance = LoadAsset());

		private void OnValidate()
		{
			Changed?.Invoke();
		}

		private static HierarchyWindowGroupHeaderScriptable LoadAsset()
		{
			var path = GetAssetPath();
			var asset = AssetDatabase.LoadAssetAtPath<HierarchyWindowGroupHeaderScriptable>(path);

			if (asset == null)
			{
				asset = CreateInstance<HierarchyWindowGroupHeaderScriptable>();
				Debug.Log($"Try to create asset at path: {path}");
				AssetDatabase.CreateAsset(asset, path);
				AssetDatabase.SaveAssets();
			}

			return asset;
		}

		private static string GetAssetPath([CallerFilePath] string callerFilePath = null)
		{
			var folder = Path.GetDirectoryName(callerFilePath);
			MatchCollection symbolsMatches = Regex.Matches(folder, @"Assets.*");
			if (symbolsMatches.Count > 0)
			{
				folder = symbolsMatches[0].Value;
			}

			// Debug.Log($"Folder path: {folder}");
			return Path.Combine(folder, "HierarchyWindowGroupHeaderSettings.asset");
		}
	}
    #endif
}
