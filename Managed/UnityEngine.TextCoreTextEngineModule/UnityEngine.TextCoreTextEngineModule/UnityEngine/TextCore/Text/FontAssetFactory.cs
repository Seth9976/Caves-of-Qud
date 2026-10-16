using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000031 RID: 49
	[NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class FontAssetFactory
	{
		// Token: 0x06000144 RID: 324 RVA: 0x000099F0 File Offset: 0x00007BF0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public static FontAsset CloneFontAssetWithBitmapRendering(FontAsset baseFontAsset, int fontSize)
		{
			FontAssetFactory.visitedFontAssets.Clear();
			return FontAssetFactory.CloneFontAssetWithBitmapRenderingInternal(baseFontAsset, fontSize);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00009A14 File Offset: 0x00007C14
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private static FontAsset CloneFontAssetWithBitmapRenderingInternal(FontAsset baseFontAsset, int fontSize)
		{
			FontAssetFactory.visitedFontAssets.Add(baseFontAsset);
			FontAsset fontAsset = FontAssetFactory.CloneFontAssetWithBitmapSettings(baseFontAsset, fontSize);
			bool flag = fontAsset != null;
			if (flag)
			{
				FontAssetFactory.ProcessFontWeights(fontAsset, baseFontAsset, fontSize);
				FontAssetFactory.ProcessFallbackFonts(fontAsset, baseFontAsset, fontSize);
			}
			return fontAsset;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009A5C File Offset: 0x00007C5C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private static FontAsset CloneFontAssetWithBitmapSettings(FontAsset source, int size)
		{
			bool flag = source.atlasRenderMode != GlyphRenderMode.SDFAA || !source.IsEditorFont || source.sourceFontFile == null;
			bool flag2 = source.atlasPopulationMode == AtlasPopulationMode.DynamicOS;
			FontAsset fontAsset;
			if (flag2)
			{
				fontAsset = FontAsset.CreateFontAsset(source.faceInfo.familyName, source.faceInfo.styleName, size, 6, GlyphRenderMode.SMOOTH_HINTED);
				bool flag3 = fontAsset != null;
				if (flag3)
				{
					FontAssetFactory.SetupFontAssetForBitmapSettings(fontAsset);
				}
			}
			else
			{
				bool flag4 = flag;
				if (flag4)
				{
					fontAsset = Object.Instantiate<FontAsset>(source);
					bool flag5 = fontAsset != null;
					if (flag5)
					{
						fontAsset.fallbackFontAssetTable = new List<FontAsset>();
						fontAsset.m_IsClone = true;
						fontAsset.IsEditorFont = true;
						FontAssetFactory.SetHideFlags(fontAsset);
					}
				}
				else
				{
					fontAsset = FontAsset.CreateFontAsset(source.sourceFontFile, size, 6, GlyphRenderMode.SMOOTH_HINTED, source.atlasWidth, source.atlasHeight, AtlasPopulationMode.Dynamic, true);
					bool flag6 = fontAsset != null;
					if (flag6)
					{
						FontAssetFactory.SetupFontAssetForBitmapSettings(fontAsset);
					}
				}
			}
			return fontAsset;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00009B60 File Offset: 0x00007D60
		private static void ProcessFontWeights(FontAsset resultFontAsset, FontAsset baseFontAsset, int fontSize)
		{
			for (int i = 0; i < baseFontAsset.fontWeightTable.Length; i++)
			{
				FontWeightPair fontWeightPair = baseFontAsset.fontWeightTable[i];
				bool flag = fontWeightPair.regularTypeface != null;
				if (flag)
				{
					resultFontAsset.fontWeightTable[i].regularTypeface = FontAssetFactory.CloneFontAssetWithBitmapSettings(fontWeightPair.regularTypeface, fontSize);
				}
				bool flag2 = fontWeightPair.italicTypeface != null;
				if (flag2)
				{
					resultFontAsset.fontWeightTable[i].italicTypeface = FontAssetFactory.CloneFontAssetWithBitmapSettings(fontWeightPair.italicTypeface, fontSize);
				}
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00009BF8 File Offset: 0x00007DF8
		private static void ProcessFallbackFonts(FontAsset resultFontAsset, FontAsset baseFontAsset, int fontSize)
		{
			bool flag = baseFontAsset.fallbackFontAssetTable == null;
			if (!flag)
			{
				foreach (FontAsset fontAsset in baseFontAsset.fallbackFontAssetTable)
				{
					bool flag2 = fontAsset != null && !FontAssetFactory.visitedFontAssets.Contains(fontAsset);
					if (flag2)
					{
						FontAssetFactory.visitedFontAssets.Add(fontAsset);
						if (resultFontAsset.fallbackFontAssetTable == null)
						{
							resultFontAsset.fallbackFontAssetTable = new List<FontAsset>();
						}
						FontAsset fontAsset2 = FontAssetFactory.CloneFontAssetWithBitmapRenderingInternal(fontAsset, fontSize);
						bool flag3 = fontAsset2;
						if (flag3)
						{
							resultFontAsset.fallbackFontAssetTable.Add(fontAsset2);
						}
					}
				}
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00009CC8 File Offset: 0x00007EC8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static FontAsset ConvertFontToFontAsset(Font font)
		{
			bool flag = font == null;
			FontAsset fontAsset;
			if (flag)
			{
				fontAsset = null;
			}
			else
			{
				FontAsset fontAsset2 = FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.DEFAULT, 1024, 1024, AtlasPopulationMode.Dynamic, true);
				bool flag2 = fontAsset2 != null;
				if (flag2)
				{
					FontAssetFactory.SetupFontAssetSettings(fontAsset2);
				}
				fontAsset = fontAsset2;
			}
			return fontAsset;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00009D18 File Offset: 0x00007F18
		internal static void SetupFontAssetSettings(FontAsset fontAsset)
		{
			bool flag = !fontAsset;
			if (!flag)
			{
				FontAssetFactory.SetHideFlags(fontAsset);
				fontAsset.isMultiAtlasTexturesEnabled = true;
				fontAsset.IsEditorFont = true;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00009D4C File Offset: 0x00007F4C
		private static void SetupFontAssetForBitmapSettings(FontAsset fontAsset)
		{
			bool flag = !fontAsset;
			if (!flag)
			{
				FontAssetFactory.SetHideFlags(fontAsset);
				fontAsset.IsEditorFont = true;
				fontAsset.isMultiAtlasTexturesEnabled = true;
				fontAsset.atlasTexture.filterMode = FilterMode.Point;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009D8C File Offset: 0x00007F8C
		public static void SetHideFlags(FontAsset fontAsset)
		{
			bool flag = !fontAsset;
			if (!flag)
			{
				fontAsset.hideFlags = HideFlags.DontSave;
				fontAsset.atlasTextures[0].hideFlags = HideFlags.DontSave;
				fontAsset.material.hideFlags = HideFlags.DontSave;
			}
		}

		// Token: 0x0400013C RID: 316
		private const GlyphRenderMode k_DefaultEditorBitmapGlyphRenderMode = GlyphRenderMode.SMOOTH_HINTED;

		// Token: 0x0400013D RID: 317
		private static readonly HashSet<FontAsset> visitedFontAssets = new HashSet<FontAsset>();
	}
}
