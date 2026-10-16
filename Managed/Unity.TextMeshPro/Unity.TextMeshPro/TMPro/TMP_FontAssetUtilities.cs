using System;
using System.Collections.Generic;

namespace TMPro
{
	// Token: 0x0200002F RID: 47
	public class TMP_FontAssetUtilities
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00009264 File Offset: 0x00007464
		public static TMP_FontAssetUtilities instance
		{
			get
			{
				return TMP_FontAssetUtilities.s_Instance;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000926B File Offset: 0x0000746B
		public static TMP_Character GetCharacterFromFontAsset(uint unicode, TMP_FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface)
		{
			if (includeFallbacks)
			{
				if (TMP_FontAssetUtilities.k_SearchedAssets == null)
				{
					TMP_FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
				}
				else
				{
					TMP_FontAssetUtilities.k_SearchedAssets.Clear();
				}
			}
			return TMP_FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, sourceFontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000929C File Offset: 0x0000749C
		private static TMP_Character GetCharacterFromFontAsset_Internal(uint unicode, TMP_FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface)
		{
			isAlternativeTypeface = false;
			bool flag = (fontStyle & FontStyles.Italic) == FontStyles.Italic;
			if (flag || fontWeight != FontWeight.Regular)
			{
				uint num = (uint)(((uint)(FontStyles.Superscript | ((uint)fontStyle << 4) | (FontStyles)(fontWeight / FontWeight.Thin)) << 24) | (FontStyles)unicode);
				TMP_Character tmp_Character;
				if (sourceFontAsset.characterLookupTable.TryGetValue(num, out tmp_Character))
				{
					isAlternativeTypeface = true;
					if (tmp_Character.textAsset != null)
					{
						return tmp_Character;
					}
					sourceFontAsset.characterLookupTable.Remove(unicode);
				}
				else if (sourceFontAsset.characterLookupTable.TryGetValue(num & 2147483647U, out tmp_Character))
				{
					isAlternativeTypeface = false;
					if (tmp_Character.textAsset != null)
					{
						return tmp_Character;
					}
					sourceFontAsset.characterLookupTable.Remove(unicode);
				}
				TMP_FontWeightPair[] fontWeightTable = sourceFontAsset.fontWeightTable;
				int num2 = 4;
				if (fontWeight <= FontWeight.Regular)
				{
					if (fontWeight <= FontWeight.ExtraLight)
					{
						if (fontWeight != FontWeight.Thin)
						{
							if (fontWeight == FontWeight.ExtraLight)
							{
								num2 = 2;
							}
						}
						else
						{
							num2 = 1;
						}
					}
					else if (fontWeight != FontWeight.Light)
					{
						if (fontWeight == FontWeight.Regular)
						{
							num2 = 4;
						}
					}
					else
					{
						num2 = 3;
					}
				}
				else if (fontWeight <= FontWeight.SemiBold)
				{
					if (fontWeight != FontWeight.Medium)
					{
						if (fontWeight == FontWeight.SemiBold)
						{
							num2 = 6;
						}
					}
					else
					{
						num2 = 5;
					}
				}
				else if (fontWeight != FontWeight.Bold)
				{
					if (fontWeight != FontWeight.Heavy)
					{
						if (fontWeight == FontWeight.Black)
						{
							num2 = 9;
						}
					}
					else
					{
						num2 = 8;
					}
				}
				else
				{
					num2 = 7;
				}
				TMP_FontAsset tmp_FontAsset = (flag ? fontWeightTable[num2].italicTypeface : fontWeightTable[num2].regularTypeface);
				if (tmp_FontAsset != null)
				{
					if (tmp_FontAsset.characterLookupTable.TryGetValue(unicode, out tmp_Character))
					{
						if (tmp_Character.textAsset != null)
						{
							isAlternativeTypeface = true;
							return tmp_Character;
						}
						tmp_FontAsset.characterLookupTable.Remove(unicode);
					}
					if ((tmp_FontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic || tmp_FontAsset.atlasPopulationMode == AtlasPopulationMode.DynamicOS) && tmp_FontAsset.TryAddCharacterInternal(unicode, out tmp_Character))
					{
						isAlternativeTypeface = true;
						return tmp_Character;
					}
				}
				if (includeFallbacks && sourceFontAsset.fallbackFontAssetTable != null)
				{
					return TMP_FontAssetUtilities.SearchFallbacksForCharacter(unicode, sourceFontAsset, fontStyle, fontWeight, out isAlternativeTypeface);
				}
				return null;
			}
			else
			{
				TMP_Character tmp_Character;
				if (sourceFontAsset.characterLookupTable.TryGetValue(unicode, out tmp_Character))
				{
					if (tmp_Character.textAsset != null)
					{
						return tmp_Character;
					}
					sourceFontAsset.characterLookupTable.Remove(unicode);
				}
				if ((sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic || sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.DynamicOS) && sourceFontAsset.TryAddCharacterInternal(unicode, out tmp_Character))
				{
					return tmp_Character;
				}
				if (includeFallbacks && sourceFontAsset.fallbackFontAssetTable != null)
				{
					return TMP_FontAssetUtilities.SearchFallbacksForCharacter(unicode, sourceFontAsset, fontStyle, fontWeight, out isAlternativeTypeface);
				}
				return null;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000094D4 File Offset: 0x000076D4
		private static TMP_Character SearchFallbacksForCharacter(uint unicode, TMP_FontAsset sourceFontAsset, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface)
		{
			isAlternativeTypeface = false;
			List<TMP_FontAsset> fallbackFontAssetTable = sourceFontAsset.fallbackFontAssetTable;
			int count = fallbackFontAssetTable.Count;
			if (count == 0)
			{
				return null;
			}
			for (int i = 0; i < count; i++)
			{
				TMP_FontAsset tmp_FontAsset = fallbackFontAssetTable[i];
				if (!(tmp_FontAsset == null))
				{
					int instanceID = tmp_FontAsset.instanceID;
					if (TMP_FontAssetUtilities.k_SearchedAssets.Add(instanceID))
					{
						TMP_Character characterFromFontAsset_Internal = TMP_FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, tmp_FontAsset, true, fontStyle, fontWeight, out isAlternativeTypeface);
						if (characterFromFontAsset_Internal != null)
						{
							return characterFromFontAsset_Internal;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009544 File Offset: 0x00007744
		public static TMP_Character GetCharacterFromFontAssets(uint unicode, TMP_FontAsset sourceFontAsset, List<TMP_FontAsset> fontAssets, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface)
		{
			isAlternativeTypeface = false;
			if (fontAssets == null || fontAssets.Count == 0)
			{
				return null;
			}
			if (includeFallbacks)
			{
				if (TMP_FontAssetUtilities.k_SearchedAssets == null)
				{
					TMP_FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
				}
				else
				{
					TMP_FontAssetUtilities.k_SearchedAssets.Clear();
				}
			}
			int count = fontAssets.Count;
			for (int i = 0; i < count; i++)
			{
				TMP_FontAsset tmp_FontAsset = fontAssets[i];
				if (!(tmp_FontAsset == null))
				{
					TMP_Character characterFromFontAsset_Internal = TMP_FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, tmp_FontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface);
					if (characterFromFontAsset_Internal != null)
					{
						return characterFromFontAsset_Internal;
					}
				}
			}
			return null;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000095BC File Offset: 0x000077BC
		internal static TMP_TextElement GetTextElementFromTextAssets(uint unicode, TMP_FontAsset sourceFontAsset, List<TMP_Asset> textAssets, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface)
		{
			isAlternativeTypeface = false;
			if (textAssets == null || textAssets.Count == 0)
			{
				return null;
			}
			if (includeFallbacks)
			{
				if (TMP_FontAssetUtilities.k_SearchedAssets == null)
				{
					TMP_FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
				}
				else
				{
					TMP_FontAssetUtilities.k_SearchedAssets.Clear();
				}
			}
			int count = textAssets.Count;
			for (int i = 0; i < count; i++)
			{
				TMP_Asset tmp_Asset = textAssets[i];
				if (!(tmp_Asset == null))
				{
					if (tmp_Asset.GetType() == typeof(TMP_FontAsset))
					{
						TMP_FontAsset tmp_FontAsset = tmp_Asset as TMP_FontAsset;
						TMP_Character characterFromFontAsset_Internal = TMP_FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, tmp_FontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface);
						if (characterFromFontAsset_Internal != null)
						{
							return characterFromFontAsset_Internal;
						}
					}
					else
					{
						TMP_SpriteAsset tmp_SpriteAsset = tmp_Asset as TMP_SpriteAsset;
						TMP_SpriteCharacter spriteCharacterFromSpriteAsset_Internal = TMP_FontAssetUtilities.GetSpriteCharacterFromSpriteAsset_Internal(unicode, tmp_SpriteAsset, true);
						if (spriteCharacterFromSpriteAsset_Internal != null)
						{
							return spriteCharacterFromSpriteAsset_Internal;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00009670 File Offset: 0x00007870
		public static TMP_SpriteCharacter GetSpriteCharacterFromSpriteAsset(uint unicode, TMP_SpriteAsset spriteAsset, bool includeFallbacks)
		{
			if (spriteAsset == null)
			{
				return null;
			}
			TMP_SpriteCharacter spriteCharacterFromSpriteAsset_Internal;
			if (spriteAsset.spriteCharacterLookupTable.TryGetValue(unicode, out spriteCharacterFromSpriteAsset_Internal))
			{
				return spriteCharacterFromSpriteAsset_Internal;
			}
			if (includeFallbacks)
			{
				if (TMP_FontAssetUtilities.k_SearchedAssets == null)
				{
					TMP_FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
				}
				else
				{
					TMP_FontAssetUtilities.k_SearchedAssets.Clear();
				}
				TMP_FontAssetUtilities.k_SearchedAssets.Add(spriteAsset.instanceID);
				List<TMP_SpriteAsset> fallbackSpriteAssets = spriteAsset.fallbackSpriteAssets;
				if (fallbackSpriteAssets != null && fallbackSpriteAssets.Count > 0)
				{
					int count = fallbackSpriteAssets.Count;
					for (int i = 0; i < count; i++)
					{
						TMP_SpriteAsset tmp_SpriteAsset = fallbackSpriteAssets[i];
						if (!(tmp_SpriteAsset == null))
						{
							int instanceID = tmp_SpriteAsset.instanceID;
							if (TMP_FontAssetUtilities.k_SearchedAssets.Add(instanceID))
							{
								spriteCharacterFromSpriteAsset_Internal = TMP_FontAssetUtilities.GetSpriteCharacterFromSpriteAsset_Internal(unicode, tmp_SpriteAsset, true);
								if (spriteCharacterFromSpriteAsset_Internal != null)
								{
									return spriteCharacterFromSpriteAsset_Internal;
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009730 File Offset: 0x00007930
		private static TMP_SpriteCharacter GetSpriteCharacterFromSpriteAsset_Internal(uint unicode, TMP_SpriteAsset spriteAsset, bool includeFallbacks)
		{
			TMP_SpriteCharacter spriteCharacterFromSpriteAsset_Internal;
			if (spriteAsset.spriteCharacterLookupTable.TryGetValue(unicode, out spriteCharacterFromSpriteAsset_Internal))
			{
				return spriteCharacterFromSpriteAsset_Internal;
			}
			if (includeFallbacks)
			{
				List<TMP_SpriteAsset> fallbackSpriteAssets = spriteAsset.fallbackSpriteAssets;
				if (fallbackSpriteAssets != null && fallbackSpriteAssets.Count > 0)
				{
					int count = fallbackSpriteAssets.Count;
					for (int i = 0; i < count; i++)
					{
						TMP_SpriteAsset tmp_SpriteAsset = fallbackSpriteAssets[i];
						if (!(tmp_SpriteAsset == null))
						{
							int instanceID = tmp_SpriteAsset.instanceID;
							if (TMP_FontAssetUtilities.k_SearchedAssets.Add(instanceID))
							{
								spriteCharacterFromSpriteAsset_Internal = TMP_FontAssetUtilities.GetSpriteCharacterFromSpriteAsset_Internal(unicode, tmp_SpriteAsset, true);
								if (spriteCharacterFromSpriteAsset_Internal != null)
								{
									return spriteCharacterFromSpriteAsset_Internal;
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04000125 RID: 293
		private static readonly TMP_FontAssetUtilities s_Instance = new TMP_FontAssetUtilities();

		// Token: 0x04000126 RID: 294
		private static HashSet<int> k_SearchedAssets;
	}
}
