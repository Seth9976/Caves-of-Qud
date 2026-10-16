using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200006E RID: 110
	internal class TextResourceManager
	{
		// Token: 0x06000364 RID: 868 RVA: 0x00025F00 File Offset: 0x00024100
		internal static void AddFontAsset(FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			bool flag = !TextResourceManager.s_FontAssetReferences.ContainsKey(instanceID);
			if (flag)
			{
				TextResourceManager.FontAssetRef fontAssetRef = new TextResourceManager.FontAssetRef(fontAsset.hashCode, fontAsset.familyNameHashCode, fontAsset.styleNameHashCode, fontAsset);
				TextResourceManager.s_FontAssetReferences.Add(instanceID, fontAssetRef);
				bool flag2 = !TextResourceManager.s_FontAssetNameReferenceLookup.ContainsKey(fontAssetRef.nameHashCode);
				if (flag2)
				{
					TextResourceManager.s_FontAssetNameReferenceLookup.Add(fontAssetRef.nameHashCode, fontAsset);
				}
				bool flag3 = !TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.ContainsKey(fontAssetRef.familyNameAndStyleHashCode);
				if (flag3)
				{
					TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Add(fontAssetRef.familyNameAndStyleHashCode, fontAsset);
				}
			}
			else
			{
				TextResourceManager.FontAssetRef fontAssetRef2 = TextResourceManager.s_FontAssetReferences[instanceID];
				bool flag4 = fontAssetRef2.nameHashCode == fontAsset.hashCode && fontAssetRef2.familyNameHashCode == fontAsset.familyNameHashCode && fontAssetRef2.styleNameHashCode == fontAsset.styleNameHashCode;
				if (!flag4)
				{
					bool flag5 = fontAssetRef2.nameHashCode != fontAsset.hashCode;
					if (flag5)
					{
						TextResourceManager.s_FontAssetNameReferenceLookup.Remove(fontAssetRef2.nameHashCode);
						fontAssetRef2.nameHashCode = fontAsset.hashCode;
						bool flag6 = !TextResourceManager.s_FontAssetNameReferenceLookup.ContainsKey(fontAssetRef2.nameHashCode);
						if (flag6)
						{
							TextResourceManager.s_FontAssetNameReferenceLookup.Add(fontAssetRef2.nameHashCode, fontAsset);
						}
					}
					bool flag7 = fontAssetRef2.familyNameHashCode != fontAsset.familyNameHashCode || fontAssetRef2.styleNameHashCode != fontAsset.styleNameHashCode;
					if (flag7)
					{
						TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(fontAssetRef2.familyNameAndStyleHashCode);
						fontAssetRef2.familyNameHashCode = fontAsset.familyNameHashCode;
						fontAssetRef2.styleNameHashCode = fontAsset.styleNameHashCode;
						fontAssetRef2.familyNameAndStyleHashCode = ((long)fontAsset.styleNameHashCode << 32) | (long)((ulong)fontAsset.familyNameHashCode);
						bool flag8 = !TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.ContainsKey(fontAssetRef2.familyNameAndStyleHashCode);
						if (flag8)
						{
							TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Add(fontAssetRef2.familyNameAndStyleHashCode, fontAsset);
						}
					}
					TextResourceManager.s_FontAssetReferences[instanceID] = fontAssetRef2;
				}
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00026108 File Offset: 0x00024308
		public static void RemoveFontAsset(FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			TextResourceManager.FontAssetRef fontAssetRef;
			bool flag = TextResourceManager.s_FontAssetReferences.TryGetValue(instanceID, out fontAssetRef);
			if (flag)
			{
				TextResourceManager.s_FontAssetNameReferenceLookup.Remove(fontAssetRef.nameHashCode);
				TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(fontAssetRef.familyNameAndStyleHashCode);
				TextResourceManager.s_FontAssetReferences.Remove(instanceID);
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00026160 File Offset: 0x00024360
		internal static bool TryGetFontAssetByName(int nameHashcode, out FontAsset fontAsset)
		{
			fontAsset = null;
			return TextResourceManager.s_FontAssetNameReferenceLookup.TryGetValue(nameHashcode, out fontAsset);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00026184 File Offset: 0x00024384
		internal static bool TryGetFontAssetByFamilyName(int familyNameHashCode, int styleNameHashCode, out FontAsset fontAsset)
		{
			fontAsset = null;
			bool flag = styleNameHashCode == 0;
			if (flag)
			{
				styleNameHashCode = TextResourceManager.k_RegularStyleHashCode;
			}
			long num = ((long)styleNameHashCode << 32) | (long)((ulong)familyNameHashCode);
			return TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.TryGetValue(num, out fontAsset);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000261BD File Offset: 0x000243BD
		public static void ClearFontAssetGlyphCache()
		{
			TextResourceManager.RebuildFontAssetCache();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000261C8 File Offset: 0x000243C8
		internal static void RebuildFontAssetCache()
		{
			foreach (KeyValuePair<int, TextResourceManager.FontAssetRef> keyValuePair in TextResourceManager.s_FontAssetReferences)
			{
				TextResourceManager.FontAssetRef value = keyValuePair.Value;
				FontAsset fontAsset = value.fontAsset;
				bool flag = fontAsset == null;
				if (flag)
				{
					TextResourceManager.s_FontAssetNameReferenceLookup.Remove(value.nameHashCode);
					TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(value.familyNameAndStyleHashCode);
					TextResourceManager.s_FontAssetRemovalList.Add(keyValuePair.Key);
				}
				else
				{
					fontAsset.InitializeCharacterLookupDictionary();
					fontAsset.AddSynthesizedCharactersAndFaceMetrics();
				}
			}
			for (int i = 0; i < TextResourceManager.s_FontAssetRemovalList.Count; i++)
			{
				TextResourceManager.s_FontAssetReferences.Remove(TextResourceManager.s_FontAssetRemovalList[i]);
			}
			TextResourceManager.s_FontAssetRemovalList.Clear();
			TextEventManager.ON_FONT_PROPERTY_CHANGED(true, null);
		}

		// Token: 0x04000469 RID: 1129
		private static readonly Dictionary<int, TextResourceManager.FontAssetRef> s_FontAssetReferences = new Dictionary<int, TextResourceManager.FontAssetRef>();

		// Token: 0x0400046A RID: 1130
		private static readonly Dictionary<int, FontAsset> s_FontAssetNameReferenceLookup = new Dictionary<int, FontAsset>();

		// Token: 0x0400046B RID: 1131
		private static readonly Dictionary<long, FontAsset> s_FontAssetFamilyNameAndStyleReferenceLookup = new Dictionary<long, FontAsset>();

		// Token: 0x0400046C RID: 1132
		private static readonly List<int> s_FontAssetRemovalList = new List<int>(16);

		// Token: 0x0400046D RID: 1133
		private static readonly int k_RegularStyleHashCode = TextUtilities.GetHashCodeCaseInSensitive("Regular");

		// Token: 0x0200006F RID: 111
		private struct FontAssetRef
		{
			// Token: 0x0600036C RID: 876 RVA: 0x000262FF File Offset: 0x000244FF
			public FontAssetRef(int nameHashCode, int familyNameHashCode, int styleNameHashCode, FontAsset fontAsset)
			{
				this.nameHashCode = ((nameHashCode != 0) ? nameHashCode : familyNameHashCode);
				this.familyNameHashCode = familyNameHashCode;
				this.styleNameHashCode = styleNameHashCode;
				this.familyNameAndStyleHashCode = ((long)styleNameHashCode << 32) | (long)((ulong)familyNameHashCode);
				this.fontAsset = fontAsset;
			}

			// Token: 0x0400046E RID: 1134
			public int nameHashCode;

			// Token: 0x0400046F RID: 1135
			public int familyNameHashCode;

			// Token: 0x04000470 RID: 1136
			public int styleNameHashCode;

			// Token: 0x04000471 RID: 1137
			public long familyNameAndStyleHashCode;

			// Token: 0x04000472 RID: 1138
			public readonly FontAsset fontAsset;
		}
	}
}
