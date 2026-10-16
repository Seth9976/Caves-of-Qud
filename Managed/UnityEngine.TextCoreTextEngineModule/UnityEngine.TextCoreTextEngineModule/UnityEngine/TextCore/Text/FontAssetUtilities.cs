using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000032 RID: 50
	internal static class FontAssetUtilities
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00009DDC File Offset: 0x00007FDC
		internal static Character GetCharacterFromFontAsset(uint unicode, FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface, bool populateLigatures)
		{
			if (includeFallbacks)
			{
				bool flag = FontAssetUtilities.k_SearchedAssets == null;
				if (flag)
				{
					FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
				}
				else
				{
					FontAssetUtilities.k_SearchedAssets.Clear();
				}
			}
			return FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, sourceFontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, populateLigatures);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00009E28 File Offset: 0x00008028
		private static Character GetCharacterFromFontAsset_Internal(uint unicode, FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface, bool populateLigatures)
		{
			bool flag = !TextGenerator.IsExecutingJob;
			isAlternativeTypeface = false;
			Character character = null;
			bool flag2 = (fontStyle & FontStyles.Italic) == FontStyles.Italic;
			bool flag3 = flag2 || fontWeight != TextFontWeight.Regular;
			if (flag3)
			{
				bool flag4 = !flag && sourceFontAsset.m_CharacterLookupDictionary == null;
				if (flag4)
				{
					return null;
				}
				bool characterInLookupCache = sourceFontAsset.GetCharacterInLookupCache(unicode, fontStyle, fontWeight, out character);
				if (characterInLookupCache)
				{
					bool flag5 = character.textAsset != null;
					if (flag5)
					{
						return character;
					}
					bool flag6 = !flag;
					if (flag6)
					{
						return null;
					}
					sourceFontAsset.RemoveCharacterInLookupCache(unicode, fontStyle, fontWeight);
				}
				FontWeightPair[] fontWeightTable = sourceFontAsset.fontWeightTable;
				int textFontWeightIndex = TextUtilities.GetTextFontWeightIndex(fontWeight);
				FontAsset fontAsset = (flag2 ? fontWeightTable[textFontWeightIndex].italicTypeface : fontWeightTable[textFontWeightIndex].regularTypeface);
				bool flag7 = fontAsset != null;
				if (flag7)
				{
					bool flag8 = !flag && fontAsset.m_CharacterLookupDictionary == null;
					if (flag8)
					{
						return null;
					}
					bool characterInLookupCache2 = fontAsset.GetCharacterInLookupCache(unicode, fontStyle, fontWeight, out character);
					if (characterInLookupCache2)
					{
						bool flag9 = character.textAsset != null;
						if (flag9)
						{
							isAlternativeTypeface = true;
							return character;
						}
						bool flag10 = !flag;
						if (flag10)
						{
							return null;
						}
						fontAsset.RemoveCharacterInLookupCache(unicode, fontStyle, fontWeight);
					}
					bool flag11 = fontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic || fontAsset.atlasPopulationMode == AtlasPopulationMode.DynamicOS;
					if (flag11)
					{
						bool flag12 = !flag;
						if (flag12)
						{
							bool flag13 = !fontAsset.m_MissingUnicodesFromFontFile.Contains(unicode);
							if (flag13)
							{
								return null;
							}
						}
						else
						{
							bool flag14 = fontAsset.TryAddCharacterInternal(unicode, fontStyle, fontWeight, out character, populateLigatures);
							if (flag14)
							{
								isAlternativeTypeface = true;
								return character;
							}
						}
					}
					else
					{
						bool characterInLookupCache3 = fontAsset.GetCharacterInLookupCache(unicode, FontStyles.Normal, TextFontWeight.Regular, out character);
						if (characterInLookupCache3)
						{
							bool flag15 = character.textAsset != null;
							if (flag15)
							{
								isAlternativeTypeface = true;
								return character;
							}
							bool flag16 = !flag;
							if (flag16)
							{
								return null;
							}
							fontAsset.RemoveCharacterInLookupCache(unicode, fontStyle, fontWeight);
						}
					}
					bool flag17 = fontAsset.fallbackFontAssetTable != null && fontAsset.fallbackFontAssetTable.Count > 0;
					if (flag17)
					{
						for (int i = 0; i < fontAsset.fallbackFontAssetTable.Count; i++)
						{
							FontAsset fontAsset2 = fontAsset.fallbackFontAssetTable[i];
							bool flag18 = fontAsset2 == null;
							if (!flag18)
							{
								character = FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, fontAsset2, false, fontStyle, fontWeight, out isAlternativeTypeface, populateLigatures);
								bool flag19 = character != null;
								if (flag19)
								{
									isAlternativeTypeface = true;
									return character;
								}
							}
						}
					}
				}
			}
			bool flag20 = !flag && sourceFontAsset.m_CharacterLookupDictionary == null;
			Character character2;
			if (flag20)
			{
				character2 = null;
			}
			else
			{
				bool characterInLookupCache4 = sourceFontAsset.GetCharacterInLookupCache(unicode, FontStyles.Normal, TextFontWeight.Regular, out character);
				if (characterInLookupCache4)
				{
					bool flag21 = character.textAsset != null;
					if (flag21)
					{
						return character;
					}
					bool flag22 = !flag;
					if (flag22)
					{
						return null;
					}
					sourceFontAsset.RemoveCharacterInLookupCache(unicode, FontStyles.Normal, TextFontWeight.Regular);
				}
				bool flag23 = sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic || sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.DynamicOS;
				if (flag23)
				{
					bool flag24 = !flag;
					if (flag24)
					{
						return null;
					}
					bool flag25 = sourceFontAsset.TryAddCharacterInternal(unicode, FontStyles.Normal, TextFontWeight.Regular, out character, populateLigatures);
					if (flag25)
					{
						return character;
					}
				}
				bool flag26 = character == null && !flag;
				if (flag26)
				{
					character2 = null;
				}
				else
				{
					bool flag27 = character == null && includeFallbacks && sourceFontAsset.fallbackFontAssetTable != null;
					if (flag27)
					{
						List<FontAsset> fallbackFontAssetTable = sourceFontAsset.fallbackFontAssetTable;
						int count = fallbackFontAssetTable.Count;
						bool flag28 = count == 0;
						if (flag28)
						{
							return null;
						}
						for (int j = 0; j < count; j++)
						{
							FontAsset fontAsset3 = fallbackFontAssetTable[j];
							bool flag29 = fontAsset3 == null;
							if (!flag29)
							{
								int hashCode = fontAsset3.GetHashCode();
								bool flag30 = !FontAssetUtilities.k_SearchedAssets.Add(hashCode);
								if (!flag30)
								{
									character = FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, fontAsset3, true, fontStyle, fontWeight, out isAlternativeTypeface, populateLigatures);
									bool flag31 = character != null;
									if (flag31)
									{
										return character;
									}
								}
							}
						}
					}
					character2 = null;
				}
			}
			return character2;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000A268 File Offset: 0x00008468
		public static Character GetCharacterFromFontAssets(uint unicode, FontAsset sourceFontAsset, List<FontAsset> fontAssets, List<FontAsset> OSFallbackList, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface)
		{
			return FontAssetUtilities.GetCharacterFromFontAssetsInternal(unicode, sourceFontAsset, fontAssets, OSFallbackList, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, true);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000A28C File Offset: 0x0000848C
		internal static Character GetCharacterFromFontAssetsInternal(uint unicode, FontAsset sourceFontAsset, List<FontAsset> fontAssets, List<FontAsset> OSFallbackList, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface, bool populateLigatures = true)
		{
			isAlternativeTypeface = false;
			if (includeFallbacks)
			{
				bool flag = FontAssetUtilities.k_SearchedAssets == null;
				if (flag)
				{
					FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
				}
				else
				{
					FontAssetUtilities.k_SearchedAssets.Clear();
				}
			}
			Character characterFromFontAssetsInternal = FontAssetUtilities.GetCharacterFromFontAssetsInternal(unicode, fontAssets, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, populateLigatures);
			bool flag2 = characterFromFontAssetsInternal != null;
			Character character;
			if (flag2)
			{
				character = characterFromFontAssetsInternal;
			}
			else
			{
				character = FontAssetUtilities.GetCharacterFromFontAssetsInternal(unicode, OSFallbackList, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, populateLigatures);
			}
			return character;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000A300 File Offset: 0x00008500
		private static Character GetCharacterFromFontAssetsInternal(uint unicode, List<FontAsset> fontAssets, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface, bool populateLigatures = true)
		{
			isAlternativeTypeface = false;
			bool flag = fontAssets == null || fontAssets.Count == 0;
			Character character;
			if (flag)
			{
				character = null;
			}
			else
			{
				if (includeFallbacks)
				{
					bool flag2 = FontAssetUtilities.k_SearchedAssets == null;
					if (flag2)
					{
						FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
					}
					else
					{
						FontAssetUtilities.k_SearchedAssets.Clear();
					}
				}
				int count = fontAssets.Count;
				for (int i = 0; i < count; i++)
				{
					FontAsset fontAsset = fontAssets[i];
					bool flag3 = fontAsset == null;
					if (!flag3)
					{
						Character characterFromFontAsset_Internal = FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, fontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, populateLigatures);
						bool flag4 = characterFromFontAsset_Internal != null;
						if (flag4)
						{
							return characterFromFontAsset_Internal;
						}
					}
				}
				character = null;
			}
			return character;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000A3B8 File Offset: 0x000085B8
		internal static TextElement GetTextElementFromTextAssets(uint unicode, FontAsset sourceFontAsset, List<TextAsset> textAssets, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface, bool populateLigatures)
		{
			isAlternativeTypeface = false;
			bool flag = textAssets == null || textAssets.Count == 0;
			TextElement textElement;
			if (flag)
			{
				textElement = null;
			}
			else
			{
				if (includeFallbacks)
				{
					bool flag2 = FontAssetUtilities.k_SearchedAssets == null;
					if (flag2)
					{
						FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
					}
					else
					{
						FontAssetUtilities.k_SearchedAssets.Clear();
					}
				}
				int count = textAssets.Count;
				for (int i = 0; i < count; i++)
				{
					TextAsset textAsset = textAssets[i];
					bool flag3 = textAsset == null;
					if (!flag3)
					{
						bool flag4 = textAsset.GetType() == typeof(FontAsset);
						if (flag4)
						{
							FontAsset fontAsset = textAsset as FontAsset;
							Character characterFromFontAsset_Internal = FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, fontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, populateLigatures);
							bool flag5 = characterFromFontAsset_Internal != null;
							if (flag5)
							{
								return characterFromFontAsset_Internal;
							}
						}
						else
						{
							SpriteAsset spriteAsset = textAsset as SpriteAsset;
							SpriteCharacter spriteCharacterFromSpriteAsset_Internal = FontAssetUtilities.GetSpriteCharacterFromSpriteAsset_Internal(unicode, spriteAsset, true);
							bool flag6 = spriteCharacterFromSpriteAsset_Internal != null;
							if (flag6)
							{
								return spriteCharacterFromSpriteAsset_Internal;
							}
						}
					}
				}
				textElement = null;
			}
			return textElement;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A4C8 File Offset: 0x000086C8
		public static SpriteCharacter GetSpriteCharacterFromSpriteAsset(uint unicode, SpriteAsset spriteAsset, bool includeFallbacks)
		{
			bool flag = !TextGenerator.IsExecutingJob;
			bool flag2 = spriteAsset == null;
			SpriteCharacter spriteCharacter;
			if (flag2)
			{
				spriteCharacter = null;
			}
			else
			{
				SpriteCharacter spriteCharacterFromSpriteAsset_Internal;
				bool flag3 = spriteAsset.spriteCharacterLookupTable.TryGetValue(unicode, out spriteCharacterFromSpriteAsset_Internal);
				if (flag3)
				{
					spriteCharacter = spriteCharacterFromSpriteAsset_Internal;
				}
				else
				{
					if (includeFallbacks)
					{
						bool flag4 = FontAssetUtilities.k_SearchedAssets == null;
						if (flag4)
						{
							FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
						}
						else
						{
							FontAssetUtilities.k_SearchedAssets.Clear();
						}
						FontAssetUtilities.k_SearchedAssets.Add(spriteAsset.GetHashCode());
						List<SpriteAsset> fallbackSpriteAssets = spriteAsset.fallbackSpriteAssets;
						bool flag5 = fallbackSpriteAssets != null && fallbackSpriteAssets.Count > 0;
						if (flag5)
						{
							int count = fallbackSpriteAssets.Count;
							for (int i = 0; i < count; i++)
							{
								SpriteAsset spriteAsset2 = fallbackSpriteAssets[i];
								bool flag6 = spriteAsset2 == null;
								if (!flag6)
								{
									int hashCode = spriteAsset2.GetHashCode();
									bool flag7 = !FontAssetUtilities.k_SearchedAssets.Add(hashCode);
									if (!flag7)
									{
										spriteCharacterFromSpriteAsset_Internal = FontAssetUtilities.GetSpriteCharacterFromSpriteAsset_Internal(unicode, spriteAsset2, true);
										bool flag8 = spriteCharacterFromSpriteAsset_Internal != null;
										if (flag8)
										{
											return spriteCharacterFromSpriteAsset_Internal;
										}
									}
								}
							}
						}
					}
					spriteCharacter = null;
				}
			}
			return spriteCharacter;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A5EC File Offset: 0x000087EC
		private static SpriteCharacter GetSpriteCharacterFromSpriteAsset_Internal(uint unicode, SpriteAsset spriteAsset, bool includeFallbacks)
		{
			SpriteCharacter spriteCharacterFromSpriteAsset_Internal;
			bool flag = spriteAsset.spriteCharacterLookupTable.TryGetValue(unicode, out spriteCharacterFromSpriteAsset_Internal);
			SpriteCharacter spriteCharacter;
			if (flag)
			{
				spriteCharacter = spriteCharacterFromSpriteAsset_Internal;
			}
			else
			{
				if (includeFallbacks)
				{
					List<SpriteAsset> fallbackSpriteAssets = spriteAsset.fallbackSpriteAssets;
					bool flag2 = fallbackSpriteAssets != null && fallbackSpriteAssets.Count > 0;
					if (flag2)
					{
						int count = fallbackSpriteAssets.Count;
						for (int i = 0; i < count; i++)
						{
							SpriteAsset spriteAsset2 = fallbackSpriteAssets[i];
							bool flag3 = spriteAsset2 == null;
							if (!flag3)
							{
								int hashCode = spriteAsset2.GetHashCode();
								bool flag4 = !FontAssetUtilities.k_SearchedAssets.Add(hashCode);
								if (!flag4)
								{
									spriteCharacterFromSpriteAsset_Internal = FontAssetUtilities.GetSpriteCharacterFromSpriteAsset_Internal(unicode, spriteAsset2, true);
									bool flag5 = spriteCharacterFromSpriteAsset_Internal != null;
									if (flag5)
									{
										return spriteCharacterFromSpriteAsset_Internal;
									}
								}
							}
						}
					}
				}
				spriteCharacter = null;
			}
			return spriteCharacter;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A6B8 File Offset: 0x000088B8
		public static uint GetCodePoint(string text, ref int index)
		{
			char c = text[index];
			bool flag = char.IsHighSurrogate(c) && index + 1 < text.Length && char.IsLowSurrogate(text[index + 1]);
			uint num2;
			if (flag)
			{
				uint num = (uint)char.ConvertToUtf32(c, text[index + 1]);
				index++;
				num2 = num;
			}
			else
			{
				num2 = (uint)c;
			}
			return num2;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A71C File Offset: 0x0000891C
		public static uint GetCodePoint(uint[] codesPoints, ref int index)
		{
			char c = (char)codesPoints[index];
			bool flag = char.IsHighSurrogate(c) && index + 1 < codesPoints.Length && char.IsLowSurrogate((char)codesPoints[index + 1]);
			uint num2;
			if (flag)
			{
				uint num = (uint)char.ConvertToUtf32(c, (char)codesPoints[index + 1]);
				index++;
				num2 = num;
			}
			else
			{
				num2 = (uint)c;
			}
			return num2;
		}

		// Token: 0x0400013E RID: 318
		private static HashSet<int> k_SearchedAssets;
	}
}
