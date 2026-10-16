using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200003F RID: 63
	public class TMP_ResourceManager
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x00011BD1 File Offset: 0x0000FDD1
		internal static TMP_Settings GetTextSettings()
		{
			if (TMP_ResourceManager.s_TextSettings == null)
			{
				TMP_ResourceManager.s_TextSettings = Resources.Load<TMP_Settings>("TextSettings");
			}
			return TMP_ResourceManager.s_TextSettings;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00011BF4 File Offset: 0x0000FDF4
		public static void AddFontAsset(TMP_FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			if (!TMP_ResourceManager.s_FontAssetReferences.ContainsKey(instanceID))
			{
				TMP_ResourceManager.FontAssetRef fontAssetRef = new TMP_ResourceManager.FontAssetRef(fontAsset.hashCode, fontAsset.familyNameHashCode, fontAsset.styleNameHashCode, fontAsset);
				TMP_ResourceManager.s_FontAssetReferences.Add(instanceID, fontAssetRef);
				if (!TMP_ResourceManager.s_FontAssetNameReferenceLookup.ContainsKey(fontAssetRef.nameHashCode))
				{
					TMP_ResourceManager.s_FontAssetNameReferenceLookup.Add(fontAssetRef.nameHashCode, fontAsset);
				}
				if (!TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.ContainsKey(fontAssetRef.familyNameAndStyleHashCode))
				{
					TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Add(fontAssetRef.familyNameAndStyleHashCode, fontAsset);
					return;
				}
			}
			else
			{
				TMP_ResourceManager.FontAssetRef fontAssetRef2 = TMP_ResourceManager.s_FontAssetReferences[instanceID];
				if (fontAssetRef2.nameHashCode == fontAsset.hashCode && fontAssetRef2.familyNameHashCode == fontAsset.familyNameHashCode && fontAssetRef2.styleNameHashCode == fontAsset.styleNameHashCode)
				{
					return;
				}
				if (fontAssetRef2.nameHashCode != fontAsset.hashCode)
				{
					TMP_ResourceManager.s_FontAssetNameReferenceLookup.Remove(fontAssetRef2.nameHashCode);
					fontAssetRef2.nameHashCode = fontAsset.hashCode;
					if (!TMP_ResourceManager.s_FontAssetNameReferenceLookup.ContainsKey(fontAssetRef2.nameHashCode))
					{
						TMP_ResourceManager.s_FontAssetNameReferenceLookup.Add(fontAssetRef2.nameHashCode, fontAsset);
					}
				}
				if (fontAssetRef2.familyNameHashCode != fontAsset.familyNameHashCode || fontAssetRef2.styleNameHashCode != fontAsset.styleNameHashCode)
				{
					TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(fontAssetRef2.familyNameAndStyleHashCode);
					fontAssetRef2.familyNameHashCode = fontAsset.familyNameHashCode;
					fontAssetRef2.styleNameHashCode = fontAsset.styleNameHashCode;
					fontAssetRef2.familyNameAndStyleHashCode = ((long)fontAsset.styleNameHashCode << 32) | (long)((ulong)fontAsset.familyNameHashCode);
					if (!TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.ContainsKey(fontAssetRef2.familyNameAndStyleHashCode))
					{
						TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Add(fontAssetRef2.familyNameAndStyleHashCode, fontAsset);
					}
				}
				TMP_ResourceManager.s_FontAssetReferences[instanceID] = fontAssetRef2;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00011D9C File Offset: 0x0000FF9C
		public static void RemoveFontAsset(TMP_FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			TMP_ResourceManager.FontAssetRef fontAssetRef;
			if (TMP_ResourceManager.s_FontAssetReferences.TryGetValue(instanceID, out fontAssetRef))
			{
				TMP_ResourceManager.s_FontAssetNameReferenceLookup.Remove(fontAssetRef.nameHashCode);
				TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(fontAssetRef.familyNameAndStyleHashCode);
				TMP_ResourceManager.s_FontAssetReferences.Remove(instanceID);
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00011DED File Offset: 0x0000FFED
		internal static bool TryGetFontAssetByName(int nameHashcode, out TMP_FontAsset fontAsset)
		{
			fontAsset = null;
			return TMP_ResourceManager.s_FontAssetNameReferenceLookup.TryGetValue(nameHashcode, out fontAsset);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00011E00 File Offset: 0x00010000
		internal static bool TryGetFontAssetByFamilyName(int familyNameHashCode, int styleNameHashCode, out TMP_FontAsset fontAsset)
		{
			fontAsset = null;
			if (styleNameHashCode == 0)
			{
				styleNameHashCode = TMP_ResourceManager.k_RegularStyleHashCode;
			}
			long num = ((long)styleNameHashCode << 32) | (long)((ulong)familyNameHashCode);
			return TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.TryGetValue(num, out fontAsset);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00011E2F File Offset: 0x0001002F
		public static void ClearFontAssetGlyphCache()
		{
			TMP_ResourceManager.RebuildFontAssetCache();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00011E38 File Offset: 0x00010038
		internal static void RebuildFontAssetCache()
		{
			foreach (KeyValuePair<int, TMP_ResourceManager.FontAssetRef> keyValuePair in TMP_ResourceManager.s_FontAssetReferences)
			{
				TMP_ResourceManager.FontAssetRef value = keyValuePair.Value;
				TMP_FontAsset fontAsset = value.fontAsset;
				if (fontAsset == null)
				{
					TMP_ResourceManager.s_FontAssetNameReferenceLookup.Remove(value.nameHashCode);
					TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(value.familyNameAndStyleHashCode);
					TMP_ResourceManager.s_FontAssetRemovalList.Add(keyValuePair.Key);
				}
				else
				{
					fontAsset.InitializeCharacterLookupDictionary();
					fontAsset.AddSynthesizedCharactersAndFaceMetrics();
				}
			}
			for (int i = 0; i < TMP_ResourceManager.s_FontAssetRemovalList.Count; i++)
			{
				TMP_ResourceManager.s_FontAssetReferences.Remove(TMP_ResourceManager.s_FontAssetRemovalList[i]);
			}
			TMP_ResourceManager.s_FontAssetRemovalList.Clear();
			TMPro_EventManager.ON_FONT_PROPERTY_CHANGED(true, null);
		}

		// Token: 0x040001D8 RID: 472
		private static TMP_Settings s_TextSettings;

		// Token: 0x040001D9 RID: 473
		private static readonly Dictionary<int, TMP_ResourceManager.FontAssetRef> s_FontAssetReferences = new Dictionary<int, TMP_ResourceManager.FontAssetRef>();

		// Token: 0x040001DA RID: 474
		private static readonly Dictionary<int, TMP_FontAsset> s_FontAssetNameReferenceLookup = new Dictionary<int, TMP_FontAsset>();

		// Token: 0x040001DB RID: 475
		private static readonly Dictionary<long, TMP_FontAsset> s_FontAssetFamilyNameAndStyleReferenceLookup = new Dictionary<long, TMP_FontAsset>();

		// Token: 0x040001DC RID: 476
		private static readonly List<int> s_FontAssetRemovalList = new List<int>(16);

		// Token: 0x040001DD RID: 477
		private static readonly int k_RegularStyleHashCode = TMP_TextUtilities.GetHashCode("Regular");

		// Token: 0x020000B0 RID: 176
		private struct FontAssetRef
		{
			// Token: 0x060006ED RID: 1773 RVA: 0x00043FFF File Offset: 0x000421FF
			public FontAssetRef(int nameHashCode, int familyNameHashCode, int styleNameHashCode, TMP_FontAsset fontAsset)
			{
				this.nameHashCode = ((nameHashCode != 0) ? nameHashCode : familyNameHashCode);
				this.familyNameHashCode = familyNameHashCode;
				this.styleNameHashCode = styleNameHashCode;
				this.familyNameAndStyleHashCode = ((long)styleNameHashCode << 32) | (long)((ulong)familyNameHashCode);
				this.fontAsset = fontAsset;
			}

			// Token: 0x0400066D RID: 1645
			public int nameHashCode;

			// Token: 0x0400066E RID: 1646
			public int familyNameHashCode;

			// Token: 0x0400066F RID: 1647
			public int styleNameHashCode;

			// Token: 0x04000670 RID: 1648
			public long familyNameAndStyleHashCode;

			// Token: 0x04000671 RID: 1649
			public readonly TMP_FontAsset fontAsset;
		}
	}
}
