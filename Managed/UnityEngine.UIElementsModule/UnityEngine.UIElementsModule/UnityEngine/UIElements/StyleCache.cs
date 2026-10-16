using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020005D2 RID: 1490
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class StyleCache
	{
		// Token: 0x06002BED RID: 11245 RVA: 0x0009E9B8 File Offset: 0x0009CBB8
		public static bool TryGetValue(long hash, out ComputedStyle data)
		{
			return StyleCache.s_ComputedStyleCache.TryGetValue(hash, out data);
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x0009E9D6 File Offset: 0x0009CBD6
		public static void SetValue(long hash, ref ComputedStyle data)
		{
			StyleCache.s_ComputedStyleCache[hash] = data;
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x0009E9EC File Offset: 0x0009CBEC
		public static bool TryGetValue(int hash, out StyleVariableContext data)
		{
			return StyleCache.s_StyleVariableContextCache.TryGetValue(hash, out data);
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x0009EA0A File Offset: 0x0009CC0A
		public static void SetValue(int hash, StyleVariableContext data)
		{
			StyleCache.s_StyleVariableContextCache[hash] = data;
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x0009EA1C File Offset: 0x0009CC1C
		public static bool TryGetValue(int hash, out ComputedTransitionProperty[] data)
		{
			return StyleCache.s_ComputedTransitionsCache.TryGetValue(hash, out data);
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x0009EA3A File Offset: 0x0009CC3A
		public static void SetValue(int hash, ComputedTransitionProperty[] data)
		{
			StyleCache.s_ComputedTransitionsCache[hash] = data;
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x0009EA4C File Offset: 0x0009CC4C
		public static void ClearStyleCache()
		{
			foreach (KeyValuePair<long, ComputedStyle> keyValuePair in StyleCache.s_ComputedStyleCache)
			{
				keyValuePair.Value.Release();
			}
			StyleCache.s_ComputedStyleCache.Clear();
			StyleCache.s_StyleVariableContextCache.Clear();
			StyleCache.s_ComputedTransitionsCache.Clear();
		}

		// Token: 0x04001413 RID: 5139
		private static Dictionary<long, ComputedStyle> s_ComputedStyleCache = new Dictionary<long, ComputedStyle>();

		// Token: 0x04001414 RID: 5140
		private static Dictionary<int, StyleVariableContext> s_StyleVariableContextCache = new Dictionary<int, StyleVariableContext>();

		// Token: 0x04001415 RID: 5141
		private static Dictionary<int, ComputedTransitionProperty[]> s_ComputedTransitionsCache = new Dictionary<int, ComputedTransitionProperty[]>();
	}
}
