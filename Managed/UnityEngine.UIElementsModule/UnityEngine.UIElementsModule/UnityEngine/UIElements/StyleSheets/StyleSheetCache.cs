using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006E4 RID: 1764
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class StyleSheetCache
	{
		// Token: 0x06003312 RID: 13074 RVA: 0x000C62E9 File Offset: 0x000C44E9
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static void ClearCaches()
		{
			StyleSheetCache.s_RulePropertyIdsCache.Clear();
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x000C62F8 File Offset: 0x000C44F8
		internal static StylePropertyId[] GetPropertyIds(StyleSheet sheet, int ruleIndex)
		{
			StyleSheetCache.SheetHandleKey sheetHandleKey = new StyleSheetCache.SheetHandleKey(sheet, ruleIndex);
			StylePropertyId[] array;
			bool flag = !StyleSheetCache.s_RulePropertyIdsCache.TryGetValue(sheetHandleKey, out array);
			if (flag)
			{
				StyleRule styleRule = sheet.rules[ruleIndex];
				array = new StylePropertyId[styleRule.properties.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = StyleSheetCache.GetPropertyId(styleRule, i);
				}
				StyleSheetCache.s_RulePropertyIdsCache.Add(sheetHandleKey, array);
			}
			return array;
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x000C6378 File Offset: 0x000C4578
		internal static StylePropertyId[] GetPropertyIds(StyleRule rule)
		{
			StylePropertyId[] array = new StylePropertyId[rule.properties.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = StyleSheetCache.GetPropertyId(rule, i);
			}
			return array;
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000C63B8 File Offset: 0x000C45B8
		private static StylePropertyId GetPropertyId(StyleRule rule, int index)
		{
			StyleProperty styleProperty = rule.properties[index];
			string name = styleProperty.name;
			StylePropertyId stylePropertyId;
			bool flag = !StylePropertyUtil.s_NameToId.TryGetValue(name, out stylePropertyId);
			if (flag)
			{
				stylePropertyId = (styleProperty.isCustomProperty ? StylePropertyId.Custom : StylePropertyId.Unknown);
			}
			return stylePropertyId;
		}

		// Token: 0x04001A07 RID: 6663
		private static StyleSheetCache.SheetHandleKeyComparer s_Comparer = new StyleSheetCache.SheetHandleKeyComparer();

		// Token: 0x04001A08 RID: 6664
		private static Dictionary<StyleSheetCache.SheetHandleKey, StylePropertyId[]> s_RulePropertyIdsCache = new Dictionary<StyleSheetCache.SheetHandleKey, StylePropertyId[]>(StyleSheetCache.s_Comparer);

		// Token: 0x020006E5 RID: 1765
		private struct SheetHandleKey
		{
			// Token: 0x06003317 RID: 13079 RVA: 0x000C641B File Offset: 0x000C461B
			public SheetHandleKey(StyleSheet sheet, int index)
			{
				this.sheetInstanceID = sheet.GetInstanceID();
				this.index = index;
			}

			// Token: 0x04001A09 RID: 6665
			public readonly int sheetInstanceID;

			// Token: 0x04001A0A RID: 6666
			public readonly int index;
		}

		// Token: 0x020006E6 RID: 1766
		private class SheetHandleKeyComparer : IEqualityComparer<StyleSheetCache.SheetHandleKey>
		{
			// Token: 0x06003318 RID: 13080 RVA: 0x000C6434 File Offset: 0x000C4634
			public bool Equals(StyleSheetCache.SheetHandleKey x, StyleSheetCache.SheetHandleKey y)
			{
				return x.sheetInstanceID == y.sheetInstanceID && x.index == y.index;
			}

			// Token: 0x06003319 RID: 13081 RVA: 0x000C6468 File Offset: 0x000C4668
			public int GetHashCode(StyleSheetCache.SheetHandleKey key)
			{
				return key.sheetInstanceID.GetHashCode() ^ key.index.GetHashCode();
			}
		}
	}
}
