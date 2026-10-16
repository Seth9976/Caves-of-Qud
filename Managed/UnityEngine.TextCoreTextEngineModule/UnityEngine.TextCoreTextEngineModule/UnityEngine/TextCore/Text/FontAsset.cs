using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Bindings;
using UnityEngine.Serialization;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200002E RID: 46
	[ExcludeFromPreset]
	[NativeHeader("Modules/TextCoreTextEngine/Native/FontAsset.h")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class FontAsset : TextAsset
	{
		// Token: 0x06000092 RID: 146 RVA: 0x0000532C File Offset: 0x0000352C
		private static void EnsureAdditionalCapacity<T>(List<T> container, int additionalCapacity)
		{
			int num = container.Count + additionalCapacity;
			bool flag = container.Capacity < num;
			if (flag)
			{
				container.Capacity = num;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005358 File Offset: 0x00003558
		private static void EnsureAdditionalCapacity<TKey, TValue>(Dictionary<TKey, TValue> container, int additionalCapacity)
		{
			int num = container.Count + additionalCapacity;
			container.EnsureCapacity(num);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00005378 File Offset: 0x00003578
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00005390 File Offset: 0x00003590
		public FontAssetCreationEditorSettings fontAssetCreationEditorSettings
		{
			get
			{
				return this.m_fontAssetCreationEditorSettings;
			}
			set
			{
				this.m_fontAssetCreationEditorSettings = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000539C File Offset: 0x0000359C
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000053B4 File Offset: 0x000035B4
		public Font sourceFontFile
		{
			get
			{
				return this.m_SourceFontFile;
			}
			internal set
			{
				this.m_SourceFontFile = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000053C0 File Offset: 0x000035C0
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000053D8 File Offset: 0x000035D8
		public AtlasPopulationMode atlasPopulationMode
		{
			get
			{
				return this.m_AtlasPopulationMode;
			}
			set
			{
				this.m_AtlasPopulationMode = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000053E4 File Offset: 0x000035E4
		// (set) Token: 0x0600009B RID: 155 RVA: 0x000053FC File Offset: 0x000035FC
		public FaceInfo faceInfo
		{
			get
			{
				return this.m_FaceInfo;
			}
			set
			{
				this.m_FaceInfo = value;
				bool flag = this.m_NativeFontAsset != IntPtr.Zero;
				if (flag)
				{
					this.UpdateFaceInfo();
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00005430 File Offset: 0x00003630
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000546B File Offset: 0x0000366B
		internal int familyNameHashCode
		{
			get
			{
				bool flag = this.m_FamilyNameHashCode == 0;
				if (flag)
				{
					this.m_FamilyNameHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_FaceInfo.familyName);
				}
				return this.m_FamilyNameHashCode;
			}
			set
			{
				this.m_FamilyNameHashCode = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005474 File Offset: 0x00003674
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000054AF File Offset: 0x000036AF
		internal int styleNameHashCode
		{
			get
			{
				bool flag = this.m_StyleNameHashCode == 0;
				if (flag)
				{
					this.m_StyleNameHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_FaceInfo.styleName);
				}
				return this.m_StyleNameHashCode;
			}
			set
			{
				this.m_StyleNameHashCode = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000054B8 File Offset: 0x000036B8
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000054D0 File Offset: 0x000036D0
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public List<Glyph> glyphTable
		{
			[NullableContext(1)]
			get
			{
				return this.m_GlyphTable;
			}
			[NullableContext(1)]
			internal set
			{
				this.m_GlyphTable = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000054DC File Offset: 0x000036DC
		public Dictionary<uint, Glyph> glyphLookupTable
		{
			get
			{
				bool flag = this.m_GlyphLookupDictionary == null;
				if (flag)
				{
					this.ReadFontAssetDefinition();
				}
				return this.m_GlyphLookupDictionary;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005508 File Offset: 0x00003708
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00005520 File Offset: 0x00003720
		public List<Character> characterTable
		{
			get
			{
				return this.m_CharacterTable;
			}
			internal set
			{
				this.m_CharacterTable = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000552C File Offset: 0x0000372C
		public Dictionary<uint, Character> characterLookupTable
		{
			get
			{
				bool flag = this.m_CharacterLookupDictionary == null;
				if (flag)
				{
					this.ReadFontAssetDefinition();
				}
				return this.m_CharacterLookupDictionary;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00005558 File Offset: 0x00003758
		public Texture2D atlasTexture
		{
			get
			{
				bool flag = this.m_AtlasTexture == null;
				if (flag)
				{
					this.m_AtlasTexture = this.atlasTextures[0];
				}
				return this.m_AtlasTexture;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00005590 File Offset: 0x00003790
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00005598 File Offset: 0x00003798
		public Texture2D[] atlasTextures
		{
			get
			{
				return this.m_AtlasTextures;
			}
			set
			{
				this.m_AtlasTextures = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000055A4 File Offset: 0x000037A4
		public int atlasTextureCount
		{
			get
			{
				return this.m_AtlasTextureIndex + 1;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000055C0 File Offset: 0x000037C0
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000055D8 File Offset: 0x000037D8
		public bool isMultiAtlasTexturesEnabled
		{
			get
			{
				return this.m_IsMultiAtlasTexturesEnabled;
			}
			set
			{
				this.m_IsMultiAtlasTexturesEnabled = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000055E4 File Offset: 0x000037E4
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000055FC File Offset: 0x000037FC
		public bool getFontFeatures
		{
			get
			{
				return this.m_GetFontFeatures;
			}
			set
			{
				this.m_GetFontFeatures = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00005608 File Offset: 0x00003808
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00005620 File Offset: 0x00003820
		internal bool clearDynamicDataOnBuild
		{
			get
			{
				return this.m_ClearDynamicDataOnBuild;
			}
			set
			{
				this.m_ClearDynamicDataOnBuild = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000562C File Offset: 0x0000382C
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00005644 File Offset: 0x00003844
		public int atlasWidth
		{
			get
			{
				return this.m_AtlasWidth;
			}
			internal set
			{
				this.m_AtlasWidth = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00005650 File Offset: 0x00003850
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00005668 File Offset: 0x00003868
		public int atlasHeight
		{
			get
			{
				return this.m_AtlasHeight;
			}
			internal set
			{
				this.m_AtlasHeight = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00005674 File Offset: 0x00003874
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000568C File Offset: 0x0000388C
		public int atlasPadding
		{
			get
			{
				return this.m_AtlasPadding;
			}
			internal set
			{
				this.m_AtlasPadding = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00005698 File Offset: 0x00003898
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000056B0 File Offset: 0x000038B0
		public GlyphRenderMode atlasRenderMode
		{
			get
			{
				return this.m_AtlasRenderMode;
			}
			internal set
			{
				this.m_AtlasRenderMode = value;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000056BA File Offset: 0x000038BA
		internal bool IsBitmap()
		{
			return ((GlyphRasterModes)this.m_AtlasRenderMode).HasFlag(GlyphRasterModes.RASTER_MODE_BITMAP) && !((GlyphRasterModes)this.m_AtlasRenderMode).HasFlag(GlyphRasterModes.RASTER_MODE_COLOR);
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000056F8 File Offset: 0x000038F8
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00005710 File Offset: 0x00003910
		internal List<GlyphRect> usedGlyphRects
		{
			get
			{
				return this.m_UsedGlyphRects;
			}
			set
			{
				this.m_UsedGlyphRects = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000571C File Offset: 0x0000391C
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00005734 File Offset: 0x00003934
		internal List<GlyphRect> freeGlyphRects
		{
			get
			{
				return this.m_FreeGlyphRects;
			}
			set
			{
				this.m_FreeGlyphRects = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00005740 File Offset: 0x00003940
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00005758 File Offset: 0x00003958
		public FontFeatureTable fontFeatureTable
		{
			get
			{
				return this.m_FontFeatureTable;
			}
			internal set
			{
				this.m_FontFeatureTable = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00005764 File Offset: 0x00003964
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000577C File Offset: 0x0000397C
		public List<FontAsset> fallbackFontAssetTable
		{
			get
			{
				return this.m_FallbackFontAssetTable;
			}
			set
			{
				this.m_FallbackFontAssetTable = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005788 File Offset: 0x00003988
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000057A0 File Offset: 0x000039A0
		public FontWeightPair[] fontWeightTable
		{
			get
			{
				return this.m_FontWeightTable;
			}
			internal set
			{
				this.m_FontWeightTable = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000057AC File Offset: 0x000039AC
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000057C4 File Offset: 0x000039C4
		public float regularStyleWeight
		{
			get
			{
				return this.m_RegularStyleWeight;
			}
			set
			{
				this.m_RegularStyleWeight = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000057D0 File Offset: 0x000039D0
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000057E8 File Offset: 0x000039E8
		public float regularStyleSpacing
		{
			get
			{
				return this.m_RegularStyleSpacing;
			}
			set
			{
				this.m_RegularStyleSpacing = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000057F4 File Offset: 0x000039F4
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x0000580C File Offset: 0x00003A0C
		public float boldStyleWeight
		{
			get
			{
				return this.m_BoldStyleWeight;
			}
			set
			{
				this.m_BoldStyleWeight = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005818 File Offset: 0x00003A18
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00005830 File Offset: 0x00003A30
		public float boldStyleSpacing
		{
			get
			{
				return this.m_BoldStyleSpacing;
			}
			set
			{
				this.m_BoldStyleSpacing = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000583C File Offset: 0x00003A3C
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00005854 File Offset: 0x00003A54
		public byte italicStyleSlant
		{
			get
			{
				return this.m_ItalicStyleSlant;
			}
			set
			{
				this.m_ItalicStyleSlant = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005860 File Offset: 0x00003A60
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00005878 File Offset: 0x00003A78
		public byte tabMultiple
		{
			get
			{
				return this.m_TabMultiple;
			}
			set
			{
				this.m_TabMultiple = value;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005884 File Offset: 0x00003A84
		public static FontAsset CreateFontAsset(string familyName, string styleName, int pointSize = 90)
		{
			FontAsset fontAsset = FontAsset.CreateFontAssetInternal(familyName, styleName, pointSize);
			bool flag = fontAsset == null;
			FontAsset fontAsset2;
			if (flag)
			{
				Debug.Log(string.Concat(new string[] { "Unable to find a font file with the specified Family Name [", familyName, "] and Style [", styleName, "]." }));
				fontAsset2 = null;
			}
			else
			{
				fontAsset2 = fontAsset;
			}
			return fontAsset2;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000058E4 File Offset: 0x00003AE4
		[NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static FontAsset CreateFontAssetInternal(string familyName, string styleName, int pointSize = 90)
		{
			FontReference fontReference;
			bool flag = FontEngine.TryGetSystemFontReference(familyName, styleName, out fontReference);
			FontAsset fontAsset;
			if (flag)
			{
				fontAsset = FontAsset.CreateFontAsset(fontReference.filePath, fontReference.faceIndex, pointSize, 9, GlyphRenderMode.DEFAULT, 1024, 1024, AtlasPopulationMode.DynamicOS, true);
			}
			else
			{
				fontAsset = null;
			}
			return fontAsset;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005928 File Offset: 0x00003B28
		[NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static FontAsset CreateFontAsset(string familyName, string styleName, int pointSize, int padding, GlyphRenderMode renderMode)
		{
			FontReference fontReference;
			bool flag = FontEngine.TryGetSystemFontReference(familyName, styleName, out fontReference);
			FontAsset fontAsset;
			if (flag)
			{
				fontAsset = FontAsset.CreateFontAsset(fontReference.filePath, fontReference.faceIndex, pointSize, padding, renderMode, 1024, 1024, AtlasPopulationMode.DynamicOS, true);
			}
			else
			{
				fontAsset = null;
			}
			return fontAsset;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000596C File Offset: 0x00003B6C
		internal static List<FontAsset> CreateFontAssetOSFallbackList(string[] fallbacksFamilyNames, int pointSize = 90)
		{
			List<FontAsset> list = new List<FontAsset>();
			foreach (string text in fallbacksFamilyNames)
			{
				FontAsset fontAsset = FontAsset.CreateFontAssetFromFamilyName(text, pointSize);
				bool flag = fontAsset == null;
				if (!flag)
				{
					list.Add(fontAsset);
				}
			}
			return list;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000059C0 File Offset: 0x00003BC0
		internal static FontAsset CreateFontAssetWithOSFallbackList(string[] fallbacksFamilyNames, int pointSize = 90)
		{
			FontAsset fontAsset = null;
			foreach (string text in fallbacksFamilyNames)
			{
				FontAsset fontAsset2 = FontAsset.CreateFontAssetFromFamilyName(text, pointSize);
				bool flag = fontAsset2 == null;
				if (!flag)
				{
					bool flag2 = fontAsset == null;
					if (flag2)
					{
						fontAsset = fontAsset2;
					}
					bool flag3 = fontAsset.fallbackFontAssetTable == null;
					if (flag3)
					{
						fontAsset.fallbackFontAssetTable = new List<FontAsset>();
					}
					fontAsset.fallbackFontAssetTable.Add(fontAsset2);
				}
			}
			return fontAsset;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005A40 File Offset: 0x00003C40
		private static FontAsset CreateFontAssetFromFamilyName(string familyName, int pointSize = 90)
		{
			FontAsset fontAsset = null;
			FontReference fontReference;
			bool flag = FontEngine.TryGetSystemFontReference(familyName, null, out fontReference);
			if (flag)
			{
				fontAsset = FontAsset.CreateFontAsset(fontReference.filePath, fontReference.faceIndex, pointSize, 9, GlyphRenderMode.DEFAULT, 1024, 1024, AtlasPopulationMode.DynamicOS, true);
			}
			bool flag2 = fontAsset == null;
			FontAsset fontAsset2;
			if (flag2)
			{
				fontAsset2 = null;
			}
			else
			{
				FontAssetFactory.SetHideFlags(fontAsset);
				fontAsset.isMultiAtlasTexturesEnabled = true;
				fontAsset.InternalDynamicOS = true;
				fontAsset2 = fontAsset;
			}
			return fontAsset2;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005AAC File Offset: 0x00003CAC
		public static FontAsset CreateFontAsset(string fontFilePath, int faceIndex, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight)
		{
			return FontAsset.CreateFontAsset(fontFilePath, faceIndex, samplingPointSize, atlasPadding, renderMode, atlasWidth, atlasHeight, AtlasPopulationMode.Dynamic, true);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005AD0 File Offset: 0x00003CD0
		private static FontAsset CreateFontAsset(string fontFilePath, int faceIndex, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode, bool enableMultiAtlasSupport = true)
		{
			bool flag = FontEngine.LoadFontFace(fontFilePath, (float)samplingPointSize, faceIndex) > FontEngineError.Success;
			FontAsset fontAsset;
			if (flag)
			{
				Debug.Log("Unable to load font face from [" + fontFilePath + "].");
				fontAsset = null;
			}
			else
			{
				FontAsset fontAsset2 = FontAsset.CreateFontAssetInstance(null, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
				bool flag2 = fontAsset2;
				if (flag2)
				{
					fontAsset2.m_SourceFontFilePath = fontFilePath;
				}
				fontAsset = fontAsset2;
			}
			return fontAsset;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005B34 File Offset: 0x00003D34
		public static FontAsset CreateFontAsset(Font font)
		{
			return FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.Dynamic, true);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005B64 File Offset: 0x00003D64
		public static FontAsset CreateFontAsset(Font font, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.Dynamic, bool enableMultiAtlasSupport = true)
		{
			return FontAsset.CreateFontAsset(font, 0, samplingPointSize, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005B88 File Offset: 0x00003D88
		private static FontAsset CreateFontAsset(Font font, int faceIndex, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.Dynamic, bool enableMultiAtlasSupport = true)
		{
			bool flag = font.name == "LegacyRuntime";
			if (flag)
			{
				string[] osfallbacks = Font.GetOSFallbacks();
				bool flag2 = FontEngine.LoadFontFace(font, (float)samplingPointSize, faceIndex) == FontEngineError.Success;
				if (flag2)
				{
					FontAsset fontAsset = FontAsset.CreateFontAssetInstance(font, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
					List<FontAsset> list = FontAsset.CreateFontAssetOSFallbackList(osfallbacks, samplingPointSize);
					fontAsset.fallbackFontAssetTable = list;
					return fontAsset;
				}
				FontAsset fontAsset2 = FontAsset.CreateFontAssetWithOSFallbackList(osfallbacks, samplingPointSize);
				bool flag3 = fontAsset2 != null;
				if (flag3)
				{
					return fontAsset2;
				}
			}
			bool flag4 = FontEngine.LoadFontFace(font, (float)samplingPointSize, faceIndex) > FontEngineError.Success;
			FontAsset fontAsset4;
			if (flag4)
			{
				FontAsset fontAsset3 = FontAsset.CreateFontAsset(font.name, "Regular", 90);
				bool flag5 = fontAsset3 != null;
				if (flag5)
				{
					fontAsset4 = fontAsset3;
				}
				else
				{
					Debug.LogWarning("Unable to load font face for [" + font.name + "]. Make sure \"Include Font Data\" is enabled in the Font Import Settings.", font);
					fontAsset4 = null;
				}
			}
			else
			{
				fontAsset4 = FontAsset.CreateFontAssetInstance(font, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
			}
			return fontAsset4;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005C80 File Offset: 0x00003E80
		private static FontAsset CreateFontAssetInstance(Font font, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode, bool enableMultiAtlasSupport)
		{
			FontAsset fontAsset = ScriptableObject.CreateInstance<FontAsset>();
			fontAsset.m_Version = "1.1.0";
			fontAsset.faceInfo = FontEngine.GetFaceInfo();
			bool flag = renderMode == GlyphRenderMode.DEFAULT;
			if (flag)
			{
				renderMode = (FontEngine.IsColorFontFace() ? GlyphRenderMode.COLOR : GlyphRenderMode.SDFAA);
			}
			bool flag2 = atlasPopulationMode == AtlasPopulationMode.Dynamic && font != null;
			if (flag2)
			{
				fontAsset.sourceFontFile = font;
			}
			fontAsset.atlasPopulationMode = atlasPopulationMode;
			fontAsset.atlasWidth = atlasWidth;
			fontAsset.atlasHeight = atlasHeight;
			fontAsset.atlasPadding = atlasPadding;
			fontAsset.atlasRenderMode = renderMode;
			fontAsset.atlasTextures = new Texture2D[1];
			TextureFormat textureFormat = (((renderMode & (GlyphRenderMode)65536) == (GlyphRenderMode)65536) ? TextureFormat.RGBA32 : TextureFormat.Alpha8);
			Texture2D texture2D = new Texture2D(1, 1, textureFormat, false);
			fontAsset.atlasTextures[0] = texture2D;
			fontAsset.isMultiAtlasTexturesEnabled = enableMultiAtlasSupport;
			bool flag3 = (renderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16;
			int num;
			if (flag3)
			{
				num = 0;
				bool flag4 = textureFormat == TextureFormat.Alpha8;
				Material material;
				if (flag4)
				{
					bool flag5 = TextShaderUtilities.ShaderRef_MobileBitmap;
					if (!flag5)
					{
						return null;
					}
					material = new Material(TextShaderUtilities.ShaderRef_MobileBitmap);
				}
				else
				{
					bool flag6 = TextShaderUtilities.ShaderRef_Sprite;
					if (!flag6)
					{
						return null;
					}
					material = new Material(TextShaderUtilities.ShaderRef_Sprite);
				}
				material.SetTexture(TextShaderUtilities.ID_MainTex, texture2D);
				material.SetFloat(TextShaderUtilities.ID_TextureWidth, (float)atlasWidth);
				material.SetFloat(TextShaderUtilities.ID_TextureHeight, (float)atlasHeight);
				fontAsset.material = material;
			}
			else
			{
				num = 1;
				Material material2 = new Material(TextShaderUtilities.ShaderRef_MobileSDF);
				material2.SetTexture(TextShaderUtilities.ID_MainTex, texture2D);
				material2.SetFloat(TextShaderUtilities.ID_TextureWidth, (float)atlasWidth);
				material2.SetFloat(TextShaderUtilities.ID_TextureHeight, (float)atlasHeight);
				material2.SetFloat(TextShaderUtilities.ID_GradientScale, (float)(atlasPadding + num));
				material2.SetFloat(TextShaderUtilities.ID_WeightNormal, fontAsset.regularStyleWeight);
				material2.SetFloat(TextShaderUtilities.ID_WeightBold, fontAsset.boldStyleWeight);
				fontAsset.material = material2;
			}
			fontAsset.freeGlyphRects = new List<GlyphRect>(8)
			{
				new GlyphRect(0, 0, atlasWidth - num, atlasHeight - num)
			};
			fontAsset.usedGlyphRects = new List<GlyphRect>(8);
			fontAsset.ReadFontAssetDefinition();
			return fontAsset;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005EB4 File Offset: 0x000040B4
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static FontAsset GetFontAssetByID(int id)
		{
			return FontAsset.kFontAssetByInstanceId[id];
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005ED4 File Offset: 0x000040D4
		private void RegisterCallbackInstance(FontAsset instance)
		{
			for (int i = 0; i < FontAsset.s_CallbackInstances.Count; i++)
			{
				FontAsset fontAsset;
				bool flag = FontAsset.s_CallbackInstances[i].TryGetTarget(out fontAsset) && fontAsset == instance;
				if (flag)
				{
					return;
				}
			}
			for (int j = 0; j < FontAsset.s_CallbackInstances.Count; j++)
			{
				FontAsset fontAsset2;
				bool flag2 = !FontAsset.s_CallbackInstances[j].TryGetTarget(out fontAsset2);
				if (flag2)
				{
					FontAsset.s_CallbackInstances[j] = new WeakReference<FontAsset>(instance);
					return;
				}
			}
			FontAsset.s_CallbackInstances.Add(new WeakReference<FontAsset>(this));
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005F88 File Offset: 0x00004188
		private void OnDestroy()
		{
			FontAsset.kFontAssetByInstanceId.Remove(base.instanceID);
			bool flag = !this.m_IsClone;
			if (flag)
			{
				this.DestroyAtlasTextures();
				bool flag2 = this.m_Material;
				if (flag2)
				{
					Object.Destroy(this.m_Material);
				}
				this.m_Material = null;
			}
			bool flag3 = this.m_NativeFontAsset != IntPtr.Zero;
			if (flag3)
			{
				FontAsset.Destroy(this.m_NativeFontAsset);
				this.m_NativeFontAsset = IntPtr.Zero;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006010 File Offset: 0x00004210
		public void ReadFontAssetDefinition()
		{
			this.InitializeDictionaryLookupTables();
			this.AddSynthesizedCharactersAndFaceMetrics();
			Character character;
			bool flag = this.m_FaceInfo.capLine == 0f && this.m_CharacterLookupDictionary.TryGetValue(88U, out character);
			if (flag)
			{
				uint glyphIndex = character.glyphIndex;
				this.m_FaceInfo.capLine = this.m_GlyphLookupDictionary[glyphIndex].metrics.horizontalBearingY;
			}
			bool flag2 = this.m_FaceInfo.meanLine == 0f && this.m_CharacterLookupDictionary.TryGetValue(88U, out character);
			if (flag2)
			{
				uint glyphIndex2 = character.glyphIndex;
				this.m_FaceInfo.meanLine = this.m_GlyphLookupDictionary[glyphIndex2].metrics.horizontalBearingY;
			}
			bool flag3 = this.m_FaceInfo.scale == 0f;
			if (flag3)
			{
				this.m_FaceInfo.scale = 1f;
			}
			bool flag4 = this.m_FaceInfo.strikethroughOffset == 0f;
			if (flag4)
			{
				this.m_FaceInfo.strikethroughOffset = this.m_FaceInfo.capLine / 2.5f;
			}
			bool flag5 = this.m_AtlasPadding == 0;
			if (flag5)
			{
				bool flag6 = base.material.HasProperty(TextShaderUtilities.ID_GradientScale);
				if (flag6)
				{
					this.m_AtlasPadding = (int)base.material.GetFloat(TextShaderUtilities.ID_GradientScale) - 1;
				}
			}
			bool flag7 = this.m_FaceInfo.unitsPerEM == 0 && this.atlasPopulationMode > AtlasPopulationMode.Static;
			if (flag7)
			{
				bool flag8 = !JobsUtility.IsExecutingJob;
				if (flag8)
				{
					this.m_FaceInfo.unitsPerEM = FontEngine.GetFaceInfo().unitsPerEM;
					Debug.Log(string.Concat(new string[]
					{
						"Font Asset [",
						base.name,
						"] Units Per EM set to ",
						this.m_FaceInfo.unitsPerEM.ToString(),
						". Please commit the newly serialized value."
					}));
				}
				else
				{
					Debug.LogError(string.Concat(new string[] { "Font Asset [", base.name, "] is missing Units Per EM. Please select the 'Reset FaceInfo' menu item on Font Asset [", base.name, "] to ensure proper serialization." }));
				}
			}
			base.hashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name);
			this.familyNameHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_FaceInfo.familyName);
			this.styleNameHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_FaceInfo.styleName);
			base.materialHashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name + FontAsset.s_DefaultMaterialSuffix);
			TextResourceManager.AddFontAsset(this);
			this.IsFontAssetLookupTablesDirty = false;
			this.RegisterCallbackInstance(this);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000062C0 File Offset: 0x000044C0
		internal void InitializeDictionaryLookupTables()
		{
			this.InitializeGlyphLookupDictionary();
			this.InitializeCharacterLookupDictionary();
			bool flag = (this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS) && this.m_ShouldReimportFontFeatures;
			if (flag)
			{
				this.ImportFontFeatures();
			}
			this.InitializeLigatureSubstitutionLookupDictionary();
			this.InitializeGlyphPairAdjustmentRecordsLookupDictionary();
			this.InitializeMarkToBaseAdjustmentRecordsLookupDictionary();
			this.InitializeMarkToMarkAdjustmentRecordsLookupDictionary();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006320 File Offset: 0x00004520
		private static void InitializeLookup<T>(ICollection source, ref Dictionary<uint, T> lookup, int defaultCapacity = 16)
		{
			int num = ((source != null) ? source.Count : defaultCapacity);
			bool flag = lookup == null;
			if (flag)
			{
				lookup = new Dictionary<uint, T>(num);
			}
			else
			{
				lookup.Clear();
				lookup.EnsureCapacity(num);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006364 File Offset: 0x00004564
		private static void InitializeList<T>(ICollection source, ref List<T> list, int defaultCapacity = 16)
		{
			int num = ((source != null) ? source.Count : defaultCapacity);
			bool flag = list == null;
			if (flag)
			{
				list = new List<T>(num);
			}
			else
			{
				list.Clear();
				list.Capacity = num;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000063A8 File Offset: 0x000045A8
		internal void InitializeGlyphLookupDictionary()
		{
			FontAsset.InitializeLookup<Glyph>(this.m_GlyphTable, ref this.m_GlyphLookupDictionary, 16);
			FontAsset.InitializeList<uint>(this.m_GlyphTable, ref this.m_GlyphIndexList, 16);
			FontAsset.InitializeList<uint>(null, ref this.m_GlyphIndexListNewlyAdded, 16);
			foreach (Glyph glyph in this.m_GlyphTable)
			{
				uint index = glyph.index;
				bool flag = this.m_GlyphLookupDictionary.TryAdd(index, glyph);
				if (flag)
				{
					this.m_GlyphIndexList.Add(index);
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006458 File Offset: 0x00004658
		internal void InitializeCharacterLookupDictionary()
		{
			FontAsset.InitializeLookup<Character>(this.m_CharacterTable, ref this.m_CharacterLookupDictionary, 16);
			foreach (Character character in this.m_CharacterTable)
			{
				uint unicode = character.unicode;
				uint glyphIndex = character.glyphIndex;
				bool flag = this.m_CharacterLookupDictionary.TryAdd(unicode, character);
				if (flag)
				{
					character.textAsset = this;
					character.glyph = this.m_GlyphLookupDictionary[glyphIndex];
				}
			}
			HashSet<uint> missingUnicodesFromFontFile = this.m_MissingUnicodesFromFontFile;
			if (missingUnicodesFromFontFile != null)
			{
				missingUnicodesFromFontFile.Clear();
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006510 File Offset: 0x00004710
		internal void ClearFallbackCharacterTable()
		{
			List<uint> list = new List<uint>();
			foreach (KeyValuePair<uint, Character> keyValuePair in this.m_CharacterLookupDictionary)
			{
				Character value = keyValuePair.Value;
				bool flag = value.textAsset != this;
				if (flag)
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (uint num in list)
			{
				this.m_CharacterLookupDictionary.Remove(num);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000065DC File Offset: 0x000047DC
		internal void InitializeLigatureSubstitutionLookupDictionary()
		{
			List<LigatureSubstitutionRecord> ligatureSubstitutionRecords = this.m_FontFeatureTable.m_LigatureSubstitutionRecords;
			FontAsset.InitializeLookup<List<LigatureSubstitutionRecord>>(ligatureSubstitutionRecords, ref this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup, 16);
			bool flag = ligatureSubstitutionRecords == null;
			if (!flag)
			{
				foreach (LigatureSubstitutionRecord ligatureSubstitutionRecord in ligatureSubstitutionRecords)
				{
					bool flag2 = ligatureSubstitutionRecord.componentGlyphIDs == null || ligatureSubstitutionRecord.componentGlyphIDs.Length == 0;
					if (!flag2)
					{
						uint num = ligatureSubstitutionRecord.componentGlyphIDs[0];
						List<LigatureSubstitutionRecord> list;
						bool flag3 = this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.TryGetValue(num, out list);
						if (flag3)
						{
							list.Add(ligatureSubstitutionRecord);
						}
						else
						{
							this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.Add(num, new List<LigatureSubstitutionRecord> { ligatureSubstitutionRecord });
						}
					}
				}
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000066C8 File Offset: 0x000048C8
		internal void InitializeGlyphPairAdjustmentRecordsLookupDictionary()
		{
			List<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords = this.m_FontFeatureTable.glyphPairAdjustmentRecords;
			FontAsset.InitializeLookup<GlyphPairAdjustmentRecord>(glyphPairAdjustmentRecords, ref this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup, 16);
			bool flag = glyphPairAdjustmentRecords == null;
			if (!flag)
			{
				foreach (GlyphPairAdjustmentRecord glyphPairAdjustmentRecord in glyphPairAdjustmentRecords)
				{
					uint num = (glyphPairAdjustmentRecord.secondAdjustmentRecord.glyphIndex << 16) | glyphPairAdjustmentRecord.firstAdjustmentRecord.glyphIndex;
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryAdd(num, glyphPairAdjustmentRecord);
				}
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006778 File Offset: 0x00004978
		internal void InitializeMarkToBaseAdjustmentRecordsLookupDictionary()
		{
			List<MarkToBaseAdjustmentRecord> markToBaseAdjustmentRecords = this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecords;
			FontAsset.InitializeLookup<MarkToBaseAdjustmentRecord>(markToBaseAdjustmentRecords, ref this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup, 16);
			bool flag = markToBaseAdjustmentRecords == null;
			if (!flag)
			{
				foreach (MarkToBaseAdjustmentRecord markToBaseAdjustmentRecord in markToBaseAdjustmentRecords)
				{
					uint num = (markToBaseAdjustmentRecord.markGlyphID << 16) | markToBaseAdjustmentRecord.baseGlyphID;
					this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryAdd(num, markToBaseAdjustmentRecord);
				}
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006818 File Offset: 0x00004A18
		internal void InitializeMarkToMarkAdjustmentRecordsLookupDictionary()
		{
			List<MarkToMarkAdjustmentRecord> markToMarkAdjustmentRecords = this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecords;
			FontAsset.InitializeLookup<MarkToMarkAdjustmentRecord>(markToMarkAdjustmentRecords, ref this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup, 16);
			bool flag = markToMarkAdjustmentRecords == null;
			if (!flag)
			{
				foreach (MarkToMarkAdjustmentRecord markToMarkAdjustmentRecord in markToMarkAdjustmentRecords)
				{
					uint num = (markToMarkAdjustmentRecord.combiningMarkGlyphID << 16) | markToMarkAdjustmentRecord.baseMarkGlyphID;
					this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.TryAdd(num, markToMarkAdjustmentRecord);
				}
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000068B8 File Offset: 0x00004AB8
		internal void AddSynthesizedCharactersAndFaceMetrics()
		{
			bool flag = false;
			bool flag2 = this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS;
			if (flag2)
			{
				flag = this.LoadFontFace() == FontEngineError.Success;
				bool flag3 = !flag && !this.InternalDynamicOS;
				if (flag3)
				{
					Debug.LogWarning("Unable to load font face for [" + base.name + "] font asset.", this);
				}
			}
			this.AddSynthesizedCharacter(3U, flag, true);
			this.AddSynthesizedCharacter(9U, flag, true);
			this.AddSynthesizedCharacter(10U, flag, false);
			this.AddSynthesizedCharacter(11U, flag, false);
			this.AddSynthesizedCharacter(13U, flag, false);
			this.AddSynthesizedCharacter(1564U, flag, false);
			this.AddSynthesizedCharacter(8203U, flag, false);
			this.AddSynthesizedCharacter(8206U, flag, false);
			this.AddSynthesizedCharacter(8207U, flag, false);
			this.AddSynthesizedCharacter(8232U, flag, false);
			this.AddSynthesizedCharacter(8233U, flag, false);
			this.AddSynthesizedCharacter(8288U, flag, false);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000069B4 File Offset: 0x00004BB4
		private void AddSynthesizedCharacter(uint unicode, bool isFontFaceLoaded, bool addImmediately = false)
		{
			bool flag = this.m_CharacterLookupDictionary.ContainsKey(unicode);
			if (!flag)
			{
				Glyph glyph;
				Character character;
				if (isFontFaceLoaded)
				{
					bool flag2 = FontEngine.GetGlyphIndex(unicode) > 0U;
					if (flag2)
					{
						bool flag3 = !addImmediately;
						if (flag3)
						{
							return;
						}
						GlyphLoadFlags glyphLoadFlags = (((this.m_AtlasRenderMode & (GlyphRenderMode)4) == (GlyphRenderMode)4) ? (GlyphLoadFlags.LOAD_NO_HINTING | GlyphLoadFlags.LOAD_NO_BITMAP) : GlyphLoadFlags.LOAD_NO_BITMAP);
						bool flag4 = FontEngine.TryGetGlyphWithUnicodeValue(unicode, glyphLoadFlags, out glyph);
						if (flag4)
						{
							character = new Character(unicode, this, glyph);
							foreach (object obj in Enum.GetValues(typeof(TextFontWeight)))
							{
								TextFontWeight textFontWeight = (TextFontWeight)obj;
								this.m_CharacterLookupDictionary.Add(this.CreateCompositeKey(unicode, FontStyles.Normal, textFontWeight), character);
								this.m_CharacterLookupDictionary.Add(this.CreateCompositeKey(unicode, FontStyles.Italic, textFontWeight), character);
							}
						}
						return;
					}
				}
				glyph = new Glyph(0U, new GlyphMetrics(0f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
				character = new Character(unicode, this, glyph);
				foreach (object obj2 in Enum.GetValues(typeof(TextFontWeight)))
				{
					TextFontWeight textFontWeight2 = (TextFontWeight)obj2;
					this.m_CharacterLookupDictionary.Add(this.CreateCompositeKey(unicode, FontStyles.Normal, textFontWeight2), character);
					this.m_CharacterLookupDictionary.Add(this.CreateCompositeKey(unicode, FontStyles.Italic, textFontWeight2), character);
				}
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006B80 File Offset: 0x00004D80
		internal void AddCharacterToLookupCache(uint unicode, Character character)
		{
			this.AddCharacterToLookupCache(unicode, character, FontStyles.Normal, TextFontWeight.Regular);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006B94 File Offset: 0x00004D94
		internal void AddCharacterToLookupCache(uint unicode, Character character, FontStyles fontStyle, TextFontWeight fontWeight)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			if (flag)
			{
				this.ReadFontAssetDefinition();
			}
			this.m_CharacterLookupDictionary.TryAdd(this.CreateCompositeKey(unicode, fontStyle, fontWeight), character);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006BD0 File Offset: 0x00004DD0
		internal bool GetCharacterInLookupCache(uint unicode, FontStyles fontStyle, TextFontWeight fontWeight, out Character character)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			if (flag)
			{
				this.ReadFontAssetDefinition();
			}
			return this.m_CharacterLookupDictionary.TryGetValue(this.CreateCompositeKey(unicode, fontStyle, fontWeight), out character);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006C0C File Offset: 0x00004E0C
		internal void RemoveCharacterInLookupCache(uint unicode, FontStyles fontStyle, TextFontWeight fontWeight)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			if (flag)
			{
				this.ReadFontAssetDefinition();
			}
			this.m_CharacterLookupDictionary.Remove(this.CreateCompositeKey(unicode, fontStyle, fontWeight));
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006C44 File Offset: 0x00004E44
		internal bool ContainsCharacterInLookupCache(uint unicode, FontStyles fontStyle, TextFontWeight fontWeight)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			if (flag)
			{
				this.ReadFontAssetDefinition();
			}
			return this.m_CharacterLookupDictionary.ContainsKey(this.CreateCompositeKey(unicode, fontStyle, fontWeight));
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006C80 File Offset: 0x00004E80
		private uint CreateCompositeKey(uint unicode, FontStyles fontStyle = FontStyles.Normal, TextFontWeight fontWeight = TextFontWeight.Regular)
		{
			bool flag = fontStyle == FontStyles.Normal && fontWeight == TextFontWeight.Regular;
			uint num;
			if (flag)
			{
				num = unicode;
			}
			else
			{
				bool flag2 = (fontStyle & FontStyles.Italic) == FontStyles.Italic;
				int num2 = 0;
				bool flag3 = fontWeight != TextFontWeight.Regular;
				if (flag3)
				{
					num2 = TextUtilities.GetTextFontWeightIndex(fontWeight);
				}
				uint num3 = unicode & 2097151U;
				uint num4 = (uint)((uint)(num2 & 15) << 21);
				uint num5 = (flag2 ? 33554432U : 0U);
				uint num6 = num3 | num4 | num5;
				num = num6;
			}
			return num;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006CF8 File Offset: 0x00004EF8
		internal FontEngineError LoadFontFace()
		{
			bool flag = this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic;
			FontEngineError fontEngineError;
			if (flag)
			{
				bool flag2 = FontEngine.LoadFontFace(this.m_SourceFontFile, this.m_FaceInfo.pointSize, this.m_FaceInfo.faceIndex) == FontEngineError.Success;
				if (flag2)
				{
					fontEngineError = FontEngineError.Success;
				}
				else
				{
					bool flag3 = !string.IsNullOrEmpty(this.m_SourceFontFilePath);
					if (flag3)
					{
						fontEngineError = FontEngine.LoadFontFace(this.m_SourceFontFilePath, this.m_FaceInfo.pointSize, this.m_FaceInfo.faceIndex);
					}
					else
					{
						fontEngineError = FontEngineError.Invalid_Face;
					}
				}
			}
			else
			{
				fontEngineError = FontEngine.LoadFontFace(this.m_FaceInfo.familyName, this.m_FaceInfo.styleName, this.m_FaceInfo.pointSize);
			}
			return fontEngineError;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006DA8 File Offset: 0x00004FA8
		internal void SortCharacterTable()
		{
			bool flag = this.m_CharacterTable != null && this.m_CharacterTable.Count > 0;
			if (flag)
			{
				this.m_CharacterTable = this.m_CharacterTable.OrderBy<Character, uint>((Character c) => c.unicode).ToList<Character>();
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006E08 File Offset: 0x00005008
		internal void SortGlyphTable()
		{
			bool flag = this.m_GlyphTable != null && this.m_GlyphTable.Count > 0;
			if (flag)
			{
				this.m_GlyphTable = this.m_GlyphTable.OrderBy<Glyph, uint>((Glyph c) => c.index).ToList<Glyph>();
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006E68 File Offset: 0x00005068
		internal void SortFontFeatureTable()
		{
			this.m_FontFeatureTable.SortGlyphPairAdjustmentRecords();
			this.m_FontFeatureTable.SortMarkToBaseAdjustmentRecords();
			this.m_FontFeatureTable.SortMarkToMarkAdjustmentRecords();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006E8F File Offset: 0x0000508F
		internal void SortAllTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
			this.SortFontFeatureTable();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006EA8 File Offset: 0x000050A8
		public bool HasCharacter(int character)
		{
			bool flag = this.characterLookupTable == null;
			return !flag && this.m_CharacterLookupDictionary.ContainsKey((uint)character);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006ED8 File Offset: 0x000050D8
		public bool HasCharacter(char character, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			return this.HasCharacter((uint)character, searchFallbacks, tryAddCharacter);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006EF4 File Offset: 0x000050F4
		public bool HasCharacter(uint character, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			bool flag = this.characterLookupTable == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.m_CharacterLookupDictionary.ContainsKey(character);
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = tryAddCharacter && (this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS);
					if (flag4)
					{
						Character character2;
						bool flag5 = this.TryAddCharacterInternal(character, FontStyles.Normal, TextFontWeight.Regular, out character2, true);
						if (flag5)
						{
							return true;
						}
					}
					if (searchFallbacks)
					{
						bool flag6 = FontAsset.k_SearchedFontAssetLookup == null;
						if (flag6)
						{
							FontAsset.k_SearchedFontAssetLookup = new HashSet<int>();
						}
						else
						{
							FontAsset.k_SearchedFontAssetLookup.Clear();
						}
						FontAsset.k_SearchedFontAssetLookup.Add(base.GetInstanceID());
						bool flag7 = this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0;
						if (flag7)
						{
							int num = 0;
							while (num < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num] != null)
							{
								FontAsset fontAsset = this.fallbackFontAssetTable[num];
								int instanceID = fontAsset.GetInstanceID();
								bool flag8 = FontAsset.k_SearchedFontAssetLookup.Add(instanceID);
								if (flag8)
								{
									bool flag9 = fontAsset.HasCharacter_Internal(character, FontStyles.Normal, TextFontWeight.Regular, true, tryAddCharacter);
									if (flag9)
									{
										return true;
									}
								}
								num++;
							}
						}
					}
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00007054 File Offset: 0x00005254
		private bool HasCharacterWithStyle_Internal(uint character, FontStyles fontStyle, TextFontWeight fontWeight, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			return this.HasCharacter_Internal(character, fontStyle, fontWeight, searchFallbacks, tryAddCharacter);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007074 File Offset: 0x00005274
		private bool HasCharacter_Internal(uint character, FontStyles fontStyle = FontStyles.Normal, TextFontWeight fontWeight = TextFontWeight.Regular, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			if (flag)
			{
				this.ReadFontAssetDefinition();
				bool flag2 = this.m_CharacterLookupDictionary == null;
				if (flag2)
				{
					return false;
				}
			}
			bool flag3 = this.ContainsCharacterInLookupCache(character, fontStyle, fontWeight);
			bool flag4;
			if (flag3)
			{
				flag4 = true;
			}
			else
			{
				bool flag5 = tryAddCharacter && (this.atlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS);
				if (flag5)
				{
					Character character2;
					bool flag6 = this.TryAddCharacterInternal(character, fontStyle, fontWeight, out character2, true);
					if (flag6)
					{
						return true;
					}
				}
				if (searchFallbacks)
				{
					bool flag7 = this.fallbackFontAssetTable == null || this.fallbackFontAssetTable.Count == 0;
					if (flag7)
					{
						return false;
					}
					int num = 0;
					while (num < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num] != null)
					{
						FontAsset fontAsset = this.fallbackFontAssetTable[num];
						int instanceID = fontAsset.GetInstanceID();
						bool flag8 = FontAsset.k_SearchedFontAssetLookup.Add(instanceID);
						if (flag8)
						{
							bool flag9 = fontAsset.HasCharacter_Internal(character, fontStyle, fontWeight, true, tryAddCharacter);
							if (flag9)
							{
								return true;
							}
						}
						num++;
					}
				}
				flag4 = false;
			}
			return flag4;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000071B0 File Offset: 0x000053B0
		public bool HasCharacters(string text, out List<char> missingCharacters)
		{
			bool flag = this.characterLookupTable == null;
			bool flag2;
			if (flag)
			{
				missingCharacters = null;
				flag2 = false;
			}
			else
			{
				missingCharacters = new List<char>();
				for (int i = 0; i < text.Length; i++)
				{
					uint codePoint = FontAssetUtilities.GetCodePoint(text, ref i);
					bool flag3 = !this.m_CharacterLookupDictionary.ContainsKey(codePoint);
					if (flag3)
					{
						missingCharacters.Add((char)codePoint);
					}
				}
				bool flag4 = missingCharacters.Count == 0;
				flag2 = flag4;
			}
			return flag2;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00007234 File Offset: 0x00005434
		public bool HasCharacters(string text, out uint[] missingCharacters, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			missingCharacters = null;
			bool flag = this.characterLookupTable == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				this.s_MissingCharacterList.Clear();
				for (int i = 0; i < text.Length; i++)
				{
					bool flag3 = true;
					uint codePoint = FontAssetUtilities.GetCodePoint(text, ref i);
					bool flag4 = this.m_CharacterLookupDictionary.ContainsKey(codePoint);
					if (!flag4)
					{
						bool flag5 = tryAddCharacter && (this.atlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS);
						if (flag5)
						{
							Character character;
							bool flag6 = this.TryAddCharacterInternal(codePoint, FontStyles.Normal, TextFontWeight.Regular, out character, true);
							if (flag6)
							{
								goto IL_0190;
							}
						}
						if (searchFallbacks)
						{
							bool flag7 = FontAsset.k_SearchedFontAssetLookup == null;
							if (flag7)
							{
								FontAsset.k_SearchedFontAssetLookup = new HashSet<int>();
							}
							else
							{
								FontAsset.k_SearchedFontAssetLookup.Clear();
							}
							FontAsset.k_SearchedFontAssetLookup.Add(base.GetInstanceID());
							bool flag8 = this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0;
							if (flag8)
							{
								int num = 0;
								while (num < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num] != null)
								{
									FontAsset fontAsset = this.fallbackFontAssetTable[num];
									int instanceID = fontAsset.GetInstanceID();
									bool flag9 = FontAsset.k_SearchedFontAssetLookup.Add(instanceID);
									if (flag9)
									{
										bool flag10 = !fontAsset.HasCharacter_Internal(codePoint, FontStyles.Normal, TextFontWeight.Regular, true, tryAddCharacter);
										if (!flag10)
										{
											flag3 = false;
											break;
										}
									}
									num++;
								}
							}
						}
						bool flag11 = flag3;
						if (flag11)
						{
							this.s_MissingCharacterList.Add(codePoint);
						}
					}
					IL_0190:;
				}
				bool flag12 = this.s_MissingCharacterList.Count > 0;
				if (flag12)
				{
					missingCharacters = this.s_MissingCharacterList.ToArray();
					flag2 = false;
				}
				else
				{
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00007414 File Offset: 0x00005614
		public bool HasCharacters(string text)
		{
			bool flag = this.characterLookupTable == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < text.Length; i++)
				{
					uint codePoint = FontAssetUtilities.GetCodePoint(text, ref i);
					bool flag3 = !this.m_CharacterLookupDictionary.ContainsKey(codePoint);
					if (flag3)
					{
						return false;
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007474 File Offset: 0x00005674
		public static string GetCharacters(FontAsset fontAsset)
		{
			string text = string.Empty;
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				text += ((char)fontAsset.characterTable[i].unicode).ToString();
			}
			return text;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000074CC File Offset: 0x000056CC
		public static int[] GetCharactersArray(FontAsset fontAsset)
		{
			int[] array = new int[fontAsset.characterTable.Count];
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				array[i] = (int)fontAsset.characterTable[i].unicode;
			}
			return array;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007520 File Offset: 0x00005720
		internal uint GetGlyphIndex(uint unicode)
		{
			bool flag;
			return this.GetGlyphIndex(unicode, out flag);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000753C File Offset: 0x0000573C
		internal uint GetGlyphIndex(uint unicode, out bool success)
		{
			success = true;
			Character character;
			bool flag = this.characterLookupTable.TryGetValue(unicode, out character);
			uint num;
			if (flag)
			{
				num = character.glyphIndex;
			}
			else
			{
				bool isExecutingJob = TextGenerator.IsExecutingJob;
				if (isExecutingJob)
				{
					success = false;
					num = 0U;
				}
				else
				{
					num = ((this.LoadFontFace() == FontEngineError.Success) ? FontEngine.GetGlyphIndex(unicode) : 0U);
				}
			}
			return num;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00007590 File Offset: 0x00005790
		internal uint GetGlyphVariantIndex(uint unicode, uint variantSelectorUnicode)
		{
			return (this.LoadFontFace() == FontEngineError.Success) ? FontEngine.GetVariantGlyphIndex(unicode, variantSelectorUnicode) : 0U;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000075B4 File Offset: 0x000057B4
		internal void UpdateFontAssetData()
		{
			uint[] array = new uint[this.m_CharacterTable.Count];
			for (int i = 0; i < this.m_CharacterTable.Count; i++)
			{
				array[i] = this.m_CharacterTable[i].unicode;
			}
			this.ClearCharacterAndGlyphTables();
			this.ClearFontFeaturesTables();
			this.ClearAtlasTextures(true);
			this.ReadFontAssetDefinition();
			bool flag = array.Length != 0;
			if (flag)
			{
				this.TryAddCharacters(array, this.m_GetFontFeatures);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007638 File Offset: 0x00005838
		public void ClearFontAssetData(bool setAtlasSizeToZero = false)
		{
			using (FontAsset.k_ClearFontAssetDataMarker.Auto())
			{
				this.ClearCharacterAndGlyphTables();
				this.ClearFontFeaturesTables();
				this.ClearAtlasTextures(setAtlasSizeToZero);
				this.ReadFontAssetDefinition();
				for (int i = 0; i < FontAsset.s_CallbackInstances.Count; i++)
				{
					FontAsset fontAsset;
					bool flag = FontAsset.s_CallbackInstances[i].TryGetTarget(out fontAsset) && fontAsset != this;
					if (flag)
					{
						fontAsset.ClearFallbackCharacterTable();
					}
				}
				TextEventManager.ON_FONT_PROPERTY_CHANGED(true, this);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000076E0 File Offset: 0x000058E0
		internal void ClearCharacterAndGlyphTablesInternal()
		{
			this.ClearCharacterAndGlyphTables();
			this.ClearAtlasTextures(true);
			this.ReadFontAssetDefinition();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000076FC File Offset: 0x000058FC
		private void ClearCharacterAndGlyphTables()
		{
			bool flag = this.m_GlyphTable != null;
			if (flag)
			{
				this.m_GlyphTable.Clear();
			}
			bool flag2 = this.m_CharacterTable != null;
			if (flag2)
			{
				this.m_CharacterTable.Clear();
			}
			bool flag3 = this.m_UsedGlyphRects != null;
			if (flag3)
			{
				this.m_UsedGlyphRects.Clear();
			}
			bool flag4 = this.m_FreeGlyphRects != null;
			if (flag4)
			{
				int num = (((this.m_AtlasRenderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16) ? 0 : 1);
				this.m_FreeGlyphRects.Clear();
				this.m_FreeGlyphRects.Add(new GlyphRect(0, 0, this.m_AtlasWidth - num, this.m_AtlasHeight - num));
			}
			bool flag5 = this.m_GlyphsToRender != null;
			if (flag5)
			{
				this.m_GlyphsToRender.Clear();
			}
			bool flag6 = this.m_GlyphsRendered != null;
			if (flag6)
			{
				this.m_GlyphsRendered.Clear();
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000077E0 File Offset: 0x000059E0
		private void ClearFontFeaturesTables()
		{
			bool flag = this.m_FontFeatureTable != null && this.m_FontFeatureTable.m_LigatureSubstitutionRecords != null;
			if (flag)
			{
				this.m_FontFeatureTable.m_LigatureSubstitutionRecords.Clear();
			}
			bool flag2 = this.m_FontFeatureTable != null && this.m_FontFeatureTable.glyphPairAdjustmentRecords != null;
			if (flag2)
			{
				this.m_FontFeatureTable.glyphPairAdjustmentRecords.Clear();
			}
			bool flag3 = this.m_FontFeatureTable != null && this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecords != null;
			if (flag3)
			{
				this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecords.Clear();
			}
			bool flag4 = this.m_FontFeatureTable != null && this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecords != null;
			if (flag4)
			{
				this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecords.Clear();
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000078A8 File Offset: 0x00005AA8
		internal void ClearAtlasTextures(bool setAtlasSizeToZero = false)
		{
			this.m_AtlasTextureIndex = 0;
			bool flag = this.m_AtlasTextures == null;
			if (!flag)
			{
				Texture2D texture2D;
				for (int i = 1; i < this.m_AtlasTextures.Length; i++)
				{
					texture2D = this.m_AtlasTextures[i];
					bool flag2 = !texture2D;
					if (!flag2)
					{
						Object.Destroy(texture2D);
					}
				}
				Array.Resize<Texture2D>(ref this.m_AtlasTextures, 1);
				texture2D = (this.m_AtlasTexture = this.m_AtlasTextures[0]);
				bool flag3 = !texture2D.isReadable;
				if (flag3)
				{
				}
				TextureFormat textureFormat = (((this.m_AtlasRenderMode & (GlyphRenderMode)65536) == (GlyphRenderMode)65536) ? TextureFormat.RGBA32 : TextureFormat.Alpha8);
				if (setAtlasSizeToZero)
				{
					texture2D.Reinitialize(1, 1, textureFormat, false);
				}
				else
				{
					bool flag4 = texture2D.width != this.m_AtlasWidth || texture2D.height != this.m_AtlasHeight;
					if (flag4)
					{
						texture2D.Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight, textureFormat, false);
					}
				}
				FontEngine.ResetAtlasTexture(texture2D);
				texture2D.Apply();
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000079BC File Offset: 0x00005BBC
		private void DestroyAtlasTextures()
		{
			this.m_AtlasTexture = null;
			this.m_AtlasTextureIndex = -1;
			bool flag = this.m_AtlasTextures == null;
			if (!flag)
			{
				foreach (Texture2D texture2D in this.m_AtlasTextures)
				{
					bool flag2 = texture2D != null;
					if (flag2)
					{
						Object.Destroy(texture2D);
					}
				}
				this.m_AtlasTextures = null;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007A20 File Offset: 0x00005C20
		internal static void RegisterFontAssetForFontFeatureUpdate(FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			bool flag = FontAsset.k_FontAssets_FontFeaturesUpdateQueueLookup.Add(instanceID);
			if (flag)
			{
				FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Add(fontAsset);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007A50 File Offset: 0x00005C50
		internal static void RegisterFontAssetForKerningUpdate(FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			bool flag = FontAsset.k_FontAssets_KerningUpdateQueueLookup.Add(instanceID);
			if (flag)
			{
				FontAsset.k_FontAssets_KerningUpdateQueue.Add(fontAsset);
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007A80 File Offset: 0x00005C80
		internal static void UpdateFontFeaturesForFontAssetsInQueue()
		{
			int num = FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Count;
			for (int i = 0; i < num; i++)
			{
				FontAsset.k_FontAssets_FontFeaturesUpdateQueue[i].UpdateGPOSFontFeaturesForNewlyAddedGlyphs();
			}
			bool flag = num > 0;
			if (flag)
			{
				FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Clear();
				FontAsset.k_FontAssets_FontFeaturesUpdateQueueLookup.Clear();
			}
			num = FontAsset.k_FontAssets_KerningUpdateQueue.Count;
			for (int j = 0; j < num; j++)
			{
				FontAsset.k_FontAssets_KerningUpdateQueue[j].UpdateGlyphAdjustmentRecordsForNewGlyphs();
			}
			bool flag2 = num > 0;
			if (flag2)
			{
				FontAsset.k_FontAssets_KerningUpdateQueue.Clear();
				FontAsset.k_FontAssets_KerningUpdateQueueLookup.Clear();
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007B34 File Offset: 0x00005D34
		internal static void RegisterAtlasTextureForApply(Texture2D texture)
		{
			int instanceID = texture.GetInstanceID();
			bool flag = FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Add(instanceID);
			if (flag)
			{
				FontAsset.k_FontAssets_AtlasTexturesUpdateQueue.Add(texture);
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007B64 File Offset: 0x00005D64
		internal static void UpdateAtlasTexturesInQueue()
		{
			int count = FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Count;
			for (int i = 0; i < count; i++)
			{
				FontAsset.k_FontAssets_AtlasTexturesUpdateQueue[i].Apply(false, false);
			}
			bool flag = count > 0;
			if (flag)
			{
				FontAsset.k_FontAssets_AtlasTexturesUpdateQueue.Clear();
				FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Clear();
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007BC0 File Offset: 0x00005DC0
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal static void UpdateFontAssetsInUpdateQueue()
		{
			FontAsset.UpdateAtlasTexturesInQueue();
			FontAsset.UpdateFontFeaturesForFontAssetsInQueue();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007BD0 File Offset: 0x00005DD0
		public bool TryAddCharacters(uint[] unicodes, bool includeFontFeatures = false)
		{
			uint[] array;
			return this.TryAddCharacters(unicodes, out array, includeFontFeatures);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007BEC File Offset: 0x00005DEC
		public bool TryAddCharacters(uint[] unicodes, out uint[] missingUnicodes, bool includeFontFeatures = false)
		{
			bool flag3;
			using (FontAsset.k_TryAddCharactersMarker.Auto())
			{
				bool flag = unicodes == null || unicodes.Length == 0 || this.m_AtlasPopulationMode == AtlasPopulationMode.Static;
				if (flag)
				{
					bool flag2 = this.m_AtlasPopulationMode == AtlasPopulationMode.Static;
					if (flag2)
					{
						Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
					}
					else
					{
						Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided Unicode list is Null or Empty.", this);
					}
					missingUnicodes = null;
					flag3 = false;
				}
				else
				{
					bool flag4 = this.LoadFontFace() > FontEngineError.Success;
					if (flag4)
					{
						missingUnicodes = new uint[unicodes.Length];
						int num = 0;
						foreach (uint num2 in unicodes)
						{
							missingUnicodes[num++] = num2;
						}
						flag3 = false;
					}
					else
					{
						bool flag5 = this.m_CharacterLookupDictionary == null || this.m_GlyphLookupDictionary == null;
						if (flag5)
						{
							this.ReadFontAssetDefinition();
						}
						Dictionary<uint, Character> characterLookupDictionary = this.m_CharacterLookupDictionary;
						Dictionary<uint, Glyph> glyphLookupDictionary = this.m_GlyphLookupDictionary;
						this.m_GlyphsToAdd.Clear();
						this.m_GlyphsToAddLookup.Clear();
						this.m_CharactersToAdd.Clear();
						this.m_CharactersToAddLookup.Clear();
						this.s_MissingCharacterList.Clear();
						bool flag6 = false;
						int num3 = unicodes.Length;
						for (int j = 0; j < num3; j++)
						{
							uint codePoint = FontAssetUtilities.GetCodePoint(unicodes, ref j);
							bool flag7 = characterLookupDictionary.ContainsKey(codePoint);
							if (!flag7)
							{
								uint num4 = FontEngine.GetGlyphIndex(codePoint);
								bool flag8 = num4 == 0U;
								if (flag8)
								{
									uint num5 = codePoint;
									uint num6 = num5;
									if (num6 != 160U)
									{
										if (num6 == 173U || num6 == 8209U)
										{
											num4 = FontEngine.GetGlyphIndex(45U);
										}
									}
									else
									{
										num4 = FontEngine.GetGlyphIndex(32U);
									}
									bool flag9 = num4 == 0U;
									if (flag9)
									{
										this.s_MissingCharacterList.Add(codePoint);
										flag6 = true;
										goto IL_0266;
									}
								}
								Character character = new Character(codePoint, num4);
								Glyph glyph;
								bool flag10 = glyphLookupDictionary.TryGetValue(num4, out glyph);
								if (flag10)
								{
									character.glyph = glyph;
									character.textAsset = this;
									this.m_CharacterTable.Add(character);
									characterLookupDictionary.Add(codePoint, character);
								}
								else
								{
									bool flag11 = this.m_GlyphsToAddLookup.Add(num4);
									if (flag11)
									{
										this.m_GlyphsToAdd.Add(num4);
									}
									bool flag12 = this.m_CharactersToAddLookup.Add(codePoint);
									if (flag12)
									{
										this.m_CharactersToAdd.Add(character);
									}
								}
							}
							IL_0266:;
						}
						bool flag13 = this.m_GlyphsToAdd.Count == 0;
						if (flag13)
						{
							missingUnicodes = unicodes;
							flag3 = !flag6;
						}
						else
						{
							bool flag14 = this.m_AtlasTextures[this.m_AtlasTextureIndex].width <= 1 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height <= 1;
							if (flag14)
							{
								this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
								FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
							}
							Glyph[] array;
							bool flag15 = FontEngine.TryAddGlyphsToTexture(this.m_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out array);
							int num7 = array.Length;
							FontAsset.EnsureAdditionalCapacity<Glyph>(this.m_GlyphTable, num7);
							FontAsset.EnsureAdditionalCapacity<uint, Glyph>(glyphLookupDictionary, num7);
							FontAsset.EnsureAdditionalCapacity<uint>(this.m_GlyphIndexListNewlyAdded, num7);
							FontAsset.EnsureAdditionalCapacity<uint>(this.m_GlyphIndexList, num7);
							int num8 = 0;
							while (num8 < array.Length && array[num8] != null)
							{
								Glyph glyph2 = array[num8];
								uint index = glyph2.index;
								glyph2.atlasIndex = this.m_AtlasTextureIndex;
								this.m_GlyphTable.Add(glyph2);
								glyphLookupDictionary.Add(index, glyph2);
								this.m_GlyphIndexListNewlyAdded.Add(index);
								this.m_GlyphIndexList.Add(index);
								num8++;
							}
							this.m_GlyphsToAdd.Clear();
							int count = this.m_CharactersToAdd.Count;
							FontAsset.EnsureAdditionalCapacity<uint>(this.m_GlyphsToAdd, count);
							FontAsset.EnsureAdditionalCapacity<Character>(this.m_CharacterTable, count);
							FontAsset.EnsureAdditionalCapacity<uint, Character>(characterLookupDictionary, count);
							for (int k = 0; k < this.m_CharactersToAdd.Count; k++)
							{
								Character character2 = this.m_CharactersToAdd[k];
								Glyph glyph3;
								bool flag16 = !glyphLookupDictionary.TryGetValue(character2.glyphIndex, out glyph3);
								if (flag16)
								{
									this.m_GlyphsToAdd.Add(character2.glyphIndex);
								}
								else
								{
									character2.glyph = glyph3;
									character2.textAsset = this;
									this.m_CharacterTable.Add(character2);
									characterLookupDictionary.Add(character2.unicode, character2);
									this.m_CharactersToAdd.RemoveAt(k);
									k--;
								}
							}
							bool flag17 = this.m_IsMultiAtlasTexturesEnabled && !flag15;
							if (flag17)
							{
								while (!flag15)
								{
									flag15 = this.TryAddGlyphsToNewAtlasTexture();
								}
							}
							if (includeFontFeatures)
							{
								this.UpdateFontFeaturesForNewlyAddedGlyphs();
							}
							foreach (Character character3 in this.m_CharactersToAdd)
							{
								this.s_MissingCharacterList.Add(character3.unicode);
							}
							missingUnicodes = null;
							bool flag18 = this.s_MissingCharacterList.Count > 0;
							if (flag18)
							{
								missingUnicodes = this.s_MissingCharacterList.ToArray();
							}
							flag3 = flag15 && !flag6;
						}
					}
				}
			}
			return flag3;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000081D4 File Offset: 0x000063D4
		public bool TryAddCharacters(string characters, bool includeFontFeatures = false)
		{
			string text;
			return this.TryAddCharacters(characters, out text, includeFontFeatures);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000081F0 File Offset: 0x000063F0
		public bool TryAddCharacters(string characters, out string missingCharacters, bool includeFontFeatures = false)
		{
			uint[] array = new uint[characters.Length];
			for (int i = 0; i < characters.Length; i++)
			{
				array[i] = (uint)characters[i];
			}
			uint[] array2;
			bool flag = this.TryAddCharacters(array, out array2, includeFontFeatures);
			bool flag2 = array2 == null || array2.Length == 0;
			bool flag3;
			if (flag2)
			{
				missingCharacters = null;
				flag3 = flag;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder(array2.Length);
				foreach (uint num in array2)
				{
					stringBuilder.Append((char)num);
				}
				missingCharacters = stringBuilder.ToString();
				flag3 = flag;
			}
			return flag3;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000829C File Offset: 0x0000649C
		internal bool TryAddGlyphVariantIndexInternal(uint unicode, uint nextCharacter, uint variantGlyphIndex)
		{
			return this.m_VariantGlyphIndexes.TryAdd(new ValueTuple<uint, uint>(unicode, nextCharacter), variantGlyphIndex);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000082C4 File Offset: 0x000064C4
		internal bool TryGetGlyphVariantIndexInternal(uint unicode, uint nextCharacter, out uint variantGlyphIndex)
		{
			return this.m_VariantGlyphIndexes.TryGetValue(new ValueTuple<uint, uint>(unicode, nextCharacter), out variantGlyphIndex);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000082EC File Offset: 0x000064EC
		internal bool TryAddGlyphInternal(uint glyphIndex, out Glyph glyph)
		{
			bool flag2;
			using (FontAsset.k_TryAddGlyphMarker.Auto())
			{
				glyph = null;
				bool flag = this.glyphLookupTable.TryGetValue(glyphIndex, out glyph);
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					bool flag3 = this.LoadFontFace() > FontEngineError.Success;
					if (flag3)
					{
						flag2 = false;
					}
					else
					{
						flag2 = this.TryAddGlyphToAtlas(glyphIndex, out glyph, true);
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000835C File Offset: 0x0000655C
		internal bool TryAddCharacterInternal(uint unicode, out Character character)
		{
			return this.TryAddCharacterInternal(unicode, FontStyles.Normal, TextFontWeight.Regular, out character, true);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00008380 File Offset: 0x00006580
		internal bool TryAddCharacterInternal(uint unicode, FontStyles fontStyle, TextFontWeight fontWeight, out Character character, bool populateLigatures = true)
		{
			bool flag2;
			using (FontAsset.k_TryAddCharacterMarker.Auto())
			{
				character = null;
				bool flag = this.m_MissingUnicodesFromFontFile.Contains(unicode);
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					bool flag3 = this.LoadFontFace() > FontEngineError.Success;
					if (flag3)
					{
						flag2 = false;
					}
					else
					{
						uint num = FontEngine.GetGlyphIndex(unicode);
						bool flag4 = num == 0U;
						if (flag4)
						{
							if (unicode != 160U)
							{
								if (unicode == 173U || unicode == 8209U)
								{
									num = FontEngine.GetGlyphIndex(45U);
								}
							}
							else
							{
								num = FontEngine.GetGlyphIndex(32U);
							}
							bool flag5 = num == 0U;
							if (flag5)
							{
								this.m_MissingUnicodesFromFontFile.Add(unicode);
								return false;
							}
						}
						bool flag6 = this.glyphLookupTable.ContainsKey(num);
						if (flag6)
						{
							character = this.CreateCharacterAndAddToCache(unicode, this.m_GlyphLookupDictionary[num], fontStyle, fontWeight);
							flag2 = true;
						}
						else
						{
							Glyph glyph = null;
							bool flag7 = this.TryAddGlyphToAtlas(num, out glyph, populateLigatures);
							if (flag7)
							{
								character = this.CreateCharacterAndAddToCache(unicode, glyph, fontStyle, fontWeight);
								flag2 = true;
							}
							else
							{
								flag2 = false;
							}
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000084B8 File Offset: 0x000066B8
		private bool TryAddGlyphToAtlas(uint glyphIndex, out Glyph glyph, bool populateLigatures = true)
		{
			glyph = null;
			bool flag = !this.m_AtlasTextures[this.m_AtlasTextureIndex].isReadable;
			bool flag2;
			if (flag)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Unable to add the requested glyph to font asset [",
					base.name,
					"]'s atlas texture. Please make the texture [",
					this.m_AtlasTextures[this.m_AtlasTextureIndex].name,
					"] readable."
				}), this.m_AtlasTextures[this.m_AtlasTextureIndex]);
				flag2 = false;
			}
			else
			{
				bool flag3 = this.m_AtlasTextures[this.m_AtlasTextureIndex].width <= 1 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height <= 1;
				if (flag3)
				{
					this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
					FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
				}
				FontEngine.SetTextureUploadMode(false);
				bool flag4 = this.TryAddGlyphToTexture(glyphIndex, out glyph, populateLigatures);
				if (flag4)
				{
					flag2 = true;
				}
				else
				{
					bool flag5 = this.m_IsMultiAtlasTexturesEnabled && this.m_UsedGlyphRects.Count > 0;
					if (flag5)
					{
						this.SetupNewAtlasTexture();
						FontEngine.SetTextureUploadMode(false);
						bool flag6 = this.TryAddGlyphToTexture(glyphIndex, out glyph, populateLigatures);
						if (flag6)
						{
							return true;
						}
					}
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00008608 File Offset: 0x00006808
		private bool TryAddGlyphToTexture(uint glyphIndex, out Glyph glyph, bool populateLigatures = true)
		{
			bool flag = FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph);
			bool flag2;
			if (flag)
			{
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				this.m_GlyphIndexList.Add(glyphIndex);
				this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
				bool getFontFeatures = this.m_GetFontFeatures;
				if (getFontFeatures)
				{
					if (populateLigatures)
					{
						this.UpdateGSUBFontFeaturesForNewGlyphIndex(glyphIndex);
						FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
					}
					else
					{
						FontAsset.RegisterFontAssetForKerningUpdate(this);
					}
				}
				FontAsset.RegisterAtlasTextureForApply(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
				FontEngine.SetTextureUploadMode(true);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000086E0 File Offset: 0x000068E0
		private bool TryAddGlyphsToNewAtlasTexture()
		{
			this.SetupNewAtlasTexture();
			Glyph[] array;
			bool flag = FontEngine.TryAddGlyphsToTexture(this.m_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out array);
			int num = 0;
			while (num < array.Length && array[num] != null)
			{
				Glyph glyph = array[num];
				uint index = glyph.index;
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(index, glyph);
				this.m_GlyphIndexListNewlyAdded.Add(index);
				this.m_GlyphIndexList.Add(index);
				num++;
			}
			this.m_GlyphsToAdd.Clear();
			for (int i = 0; i < this.m_CharactersToAdd.Count; i++)
			{
				Character character = this.m_CharactersToAdd[i];
				Glyph glyph2;
				bool flag2 = !this.m_GlyphLookupDictionary.TryGetValue(character.glyphIndex, out glyph2);
				if (flag2)
				{
					this.m_GlyphsToAdd.Add(character.glyphIndex);
				}
				else
				{
					character.glyph = glyph2;
					character.textAsset = this;
					this.m_CharacterTable.Add(character);
					this.m_CharacterLookupDictionary.Add(character.unicode, character);
					this.m_CharactersToAdd.RemoveAt(i);
					i--;
				}
			}
			return flag;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00008860 File Offset: 0x00006A60
		private void SetupNewAtlasTexture()
		{
			this.m_AtlasTextureIndex++;
			bool flag = this.m_AtlasTextures.Length == this.m_AtlasTextureIndex;
			if (flag)
			{
				Array.Resize<Texture2D>(ref this.m_AtlasTextures, this.m_AtlasTextures.Length * 2);
			}
			TextureFormat textureFormat = (((this.m_AtlasRenderMode & (GlyphRenderMode)65536) == (GlyphRenderMode)65536) ? TextureFormat.RGBA32 : TextureFormat.Alpha8);
			this.m_AtlasTextures[this.m_AtlasTextureIndex] = new Texture2D(this.m_AtlasWidth, this.m_AtlasHeight, textureFormat, false);
			this.m_AtlasTextures[this.m_AtlasTextureIndex].hideFlags = this.m_AtlasTextures[0].hideFlags;
			FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			int num = (((this.m_AtlasRenderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16) ? 0 : 1);
			this.m_FreeGlyphRects.Clear();
			this.m_FreeGlyphRects.Add(new GlyphRect(0, 0, this.m_AtlasWidth - num, this.m_AtlasHeight - num));
			this.m_UsedGlyphRects.Clear();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00008960 File Offset: 0x00006B60
		private Character CreateCharacterAndAddToCache(uint unicode, Glyph glyph, FontStyles fontStyle, TextFontWeight fontWeight)
		{
			Character character;
			bool flag = !this.m_CharacterLookupDictionary.TryGetValue(unicode, out character);
			if (flag)
			{
				character = new Character(unicode, this, glyph);
				this.m_CharacterTable.Add(character);
				this.AddCharacterToLookupCache(unicode, character, FontStyles.Normal, TextFontWeight.Regular);
			}
			bool flag2 = fontStyle != FontStyles.Normal || fontWeight != TextFontWeight.Regular;
			if (flag2)
			{
				this.AddCharacterToLookupCache(unicode, character, fontStyle, fontWeight);
			}
			return character;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000089D1 File Offset: 0x00006BD1
		private void UpdateFontFeaturesForNewlyAddedGlyphs()
		{
			this.UpdateLigatureSubstitutionRecords();
			this.UpdateGlyphAdjustmentRecords();
			this.UpdateDiacriticalMarkAdjustmentRecords();
			this.m_GlyphIndexListNewlyAdded.Clear();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000089F5 File Offset: 0x00006BF5
		private void UpdateGlyphAdjustmentRecordsForNewGlyphs()
		{
			this.UpdateGlyphAdjustmentRecords();
			this.m_GlyphIndexListNewlyAdded.Clear();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008A0B File Offset: 0x00006C0B
		private void UpdateGPOSFontFeaturesForNewlyAddedGlyphs()
		{
			this.UpdateGlyphAdjustmentRecords();
			this.UpdateDiacriticalMarkAdjustmentRecords();
			this.m_GlyphIndexListNewlyAdded.Clear();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00008A28 File Offset: 0x00006C28
		internal void ImportFontFeatures()
		{
			bool flag = this.LoadFontFace() > FontEngineError.Success;
			if (!flag)
			{
				GlyphPairAdjustmentRecord[] allPairAdjustmentRecords = FontEngine.GetAllPairAdjustmentRecords();
				bool flag2 = allPairAdjustmentRecords != null;
				if (flag2)
				{
					this.AddPairAdjustmentRecords(allPairAdjustmentRecords);
				}
				MarkToBaseAdjustmentRecord[] allMarkToBaseAdjustmentRecords = FontEngine.GetAllMarkToBaseAdjustmentRecords();
				bool flag3 = allMarkToBaseAdjustmentRecords != null;
				if (flag3)
				{
					this.AddMarkToBaseAdjustmentRecords(allMarkToBaseAdjustmentRecords);
				}
				MarkToMarkAdjustmentRecord[] allMarkToMarkAdjustmentRecords = FontEngine.GetAllMarkToMarkAdjustmentRecords();
				bool flag4 = allMarkToMarkAdjustmentRecords != null;
				if (flag4)
				{
					this.AddMarkToMarkAdjustmentRecords(allMarkToMarkAdjustmentRecords);
				}
				LigatureSubstitutionRecord[] allLigatureSubstitutionRecords = FontEngine.GetAllLigatureSubstitutionRecords();
				bool flag5 = allLigatureSubstitutionRecords != null;
				if (flag5)
				{
					this.AddLigatureSubstitutionRecords(allLigatureSubstitutionRecords);
				}
				this.m_ShouldReimportFontFeatures = false;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008AB0 File Offset: 0x00006CB0
		private void UpdateGSUBFontFeaturesForNewGlyphIndex(uint glyphIndex)
		{
			LigatureSubstitutionRecord[] ligatureSubstitutionRecords = FontEngine.GetLigatureSubstitutionRecords(glyphIndex);
			bool flag = ligatureSubstitutionRecords != null;
			if (flag)
			{
				this.AddLigatureSubstitutionRecords(ligatureSubstitutionRecords);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00008AD8 File Offset: 0x00006CD8
		internal void UpdateLigatureSubstitutionRecords()
		{
			LigatureSubstitutionRecord[] ligatureSubstitutionRecords = FontEngine.GetLigatureSubstitutionRecords(this.m_GlyphIndexListNewlyAdded);
			bool flag = ligatureSubstitutionRecords != null;
			if (flag)
			{
				this.AddLigatureSubstitutionRecords(ligatureSubstitutionRecords);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00008B04 File Offset: 0x00006D04
		private void AddLigatureSubstitutionRecords(LigatureSubstitutionRecord[] records)
		{
			Dictionary<uint, List<LigatureSubstitutionRecord>> ligatureSubstitutionRecordLookup = this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup;
			List<LigatureSubstitutionRecord> ligatureSubstitutionRecords = this.m_FontFeatureTable.m_LigatureSubstitutionRecords;
			FontAsset.EnsureAdditionalCapacity<uint, List<LigatureSubstitutionRecord>>(ligatureSubstitutionRecordLookup, records.Length);
			FontAsset.EnsureAdditionalCapacity<LigatureSubstitutionRecord>(ligatureSubstitutionRecords, records.Length);
			foreach (LigatureSubstitutionRecord ligatureSubstitutionRecord in records)
			{
				bool flag = ligatureSubstitutionRecord.componentGlyphIDs == null || ligatureSubstitutionRecord.ligatureGlyphID == 0U;
				if (flag)
				{
					break;
				}
				uint num = ligatureSubstitutionRecord.componentGlyphIDs[0];
				LigatureSubstitutionRecord ligatureSubstitutionRecord2 = new LigatureSubstitutionRecord
				{
					componentGlyphIDs = ligatureSubstitutionRecord.componentGlyphIDs,
					ligatureGlyphID = ligatureSubstitutionRecord.ligatureGlyphID
				};
				List<LigatureSubstitutionRecord> list;
				bool flag2 = ligatureSubstitutionRecordLookup.TryGetValue(num, out list);
				if (flag2)
				{
					foreach (LigatureSubstitutionRecord ligatureSubstitutionRecord3 in list)
					{
						bool flag3 = ligatureSubstitutionRecord2 == ligatureSubstitutionRecord3;
						if (flag3)
						{
							return;
						}
					}
					ligatureSubstitutionRecordLookup[num].Add(ligatureSubstitutionRecord2);
				}
				else
				{
					ligatureSubstitutionRecordLookup.Add(num, new List<LigatureSubstitutionRecord> { ligatureSubstitutionRecord2 });
				}
				ligatureSubstitutionRecords.Add(ligatureSubstitutionRecord2);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00008C50 File Offset: 0x00006E50
		internal void UpdateGlyphAdjustmentRecords()
		{
			this.LoadFontFace();
			GlyphPairAdjustmentRecord[] pairAdjustmentRecords = FontEngine.GetPairAdjustmentRecords(this.m_GlyphIndexListNewlyAdded);
			bool flag = pairAdjustmentRecords != null;
			if (flag)
			{
				this.AddPairAdjustmentRecords(pairAdjustmentRecords);
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00008C84 File Offset: 0x00006E84
		private void AddPairAdjustmentRecords(GlyphPairAdjustmentRecord[] records)
		{
			float num = this.m_FaceInfo.pointSize / (float)this.m_FaceInfo.unitsPerEM;
			List<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords = this.m_FontFeatureTable.glyphPairAdjustmentRecords;
			Dictionary<uint, GlyphPairAdjustmentRecord> glyphPairAdjustmentRecordLookup = this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup;
			FontAsset.EnsureAdditionalCapacity<uint, GlyphPairAdjustmentRecord>(glyphPairAdjustmentRecordLookup, records.Length);
			FontAsset.EnsureAdditionalCapacity<GlyphPairAdjustmentRecord>(glyphPairAdjustmentRecords, records.Length);
			foreach (GlyphPairAdjustmentRecord glyphPairAdjustmentRecord in records)
			{
				GlyphAdjustmentRecord firstAdjustmentRecord = glyphPairAdjustmentRecord.firstAdjustmentRecord;
				GlyphAdjustmentRecord secondAdjustmentRecord = glyphPairAdjustmentRecord.secondAdjustmentRecord;
				uint glyphIndex = firstAdjustmentRecord.glyphIndex;
				uint glyphIndex2 = secondAdjustmentRecord.glyphIndex;
				bool flag = glyphIndex == 0U && glyphIndex2 == 0U;
				if (flag)
				{
					break;
				}
				uint num2 = (glyphIndex2 << 16) | glyphIndex;
				GlyphPairAdjustmentRecord glyphPairAdjustmentRecord2 = glyphPairAdjustmentRecord;
				GlyphValueRecord glyphValueRecord = firstAdjustmentRecord.glyphValueRecord;
				glyphValueRecord.xAdvance *= num;
				glyphPairAdjustmentRecord2.firstAdjustmentRecord = new GlyphAdjustmentRecord(glyphIndex, glyphValueRecord);
				bool flag2 = glyphPairAdjustmentRecordLookup.TryAdd(num2, glyphPairAdjustmentRecord2);
				if (flag2)
				{
					glyphPairAdjustmentRecords.Add(glyphPairAdjustmentRecord2);
				}
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00008D88 File Offset: 0x00006F88
		internal void UpdateDiacriticalMarkAdjustmentRecords()
		{
			using (FontAsset.k_UpdateDiacriticalMarkAdjustmentRecordsMarker.Auto())
			{
				MarkToBaseAdjustmentRecord[] markToBaseAdjustmentRecords = FontEngine.GetMarkToBaseAdjustmentRecords(this.m_GlyphIndexListNewlyAdded);
				bool flag = markToBaseAdjustmentRecords != null;
				if (flag)
				{
					this.AddMarkToBaseAdjustmentRecords(markToBaseAdjustmentRecords);
				}
				MarkToMarkAdjustmentRecord[] markToMarkAdjustmentRecords = FontEngine.GetMarkToMarkAdjustmentRecords(this.m_GlyphIndexListNewlyAdded);
				bool flag2 = markToMarkAdjustmentRecords != null;
				if (flag2)
				{
					this.AddMarkToMarkAdjustmentRecords(markToMarkAdjustmentRecords);
				}
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00008E00 File Offset: 0x00007000
		private void AddMarkToBaseAdjustmentRecords(MarkToBaseAdjustmentRecord[] records)
		{
			float num = this.m_FaceInfo.pointSize / (float)this.m_FaceInfo.unitsPerEM;
			foreach (MarkToBaseAdjustmentRecord markToBaseAdjustmentRecord in records)
			{
				bool flag = markToBaseAdjustmentRecord.baseGlyphID == 0U || markToBaseAdjustmentRecord.markGlyphID == 0U;
				if (flag)
				{
					break;
				}
				uint num2 = (markToBaseAdjustmentRecord.markGlyphID << 16) | markToBaseAdjustmentRecord.baseGlyphID;
				bool flag2 = this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.ContainsKey(num2);
				if (!flag2)
				{
					MarkToBaseAdjustmentRecord markToBaseAdjustmentRecord2 = new MarkToBaseAdjustmentRecord
					{
						baseGlyphID = markToBaseAdjustmentRecord.baseGlyphID,
						baseGlyphAnchorPoint = new GlyphAnchorPoint
						{
							xCoordinate = markToBaseAdjustmentRecord.baseGlyphAnchorPoint.xCoordinate * num,
							yCoordinate = markToBaseAdjustmentRecord.baseGlyphAnchorPoint.yCoordinate * num
						},
						markGlyphID = markToBaseAdjustmentRecord.markGlyphID,
						markPositionAdjustment = new MarkPositionAdjustment
						{
							xPositionAdjustment = markToBaseAdjustmentRecord.markPositionAdjustment.xPositionAdjustment * num,
							yPositionAdjustment = markToBaseAdjustmentRecord.markPositionAdjustment.yPositionAdjustment * num
						}
					};
					this.m_FontFeatureTable.MarkToBaseAdjustmentRecords.Add(markToBaseAdjustmentRecord2);
					this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.Add(num2, markToBaseAdjustmentRecord2);
				}
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008F7C File Offset: 0x0000717C
		private void AddMarkToMarkAdjustmentRecords(MarkToMarkAdjustmentRecord[] records)
		{
			float num = this.m_FaceInfo.pointSize / (float)this.m_FaceInfo.unitsPerEM;
			for (int i = 0; i < records.Length; i++)
			{
				MarkToMarkAdjustmentRecord markToMarkAdjustmentRecord = records[i];
				bool flag = records[i].baseMarkGlyphID == 0U || records[i].combiningMarkGlyphID == 0U;
				if (flag)
				{
					break;
				}
				uint num2 = (markToMarkAdjustmentRecord.combiningMarkGlyphID << 16) | markToMarkAdjustmentRecord.baseMarkGlyphID;
				bool flag2 = this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.ContainsKey(num2);
				if (!flag2)
				{
					MarkToMarkAdjustmentRecord markToMarkAdjustmentRecord2 = new MarkToMarkAdjustmentRecord
					{
						baseMarkGlyphID = markToMarkAdjustmentRecord.baseMarkGlyphID,
						baseMarkGlyphAnchorPoint = new GlyphAnchorPoint
						{
							xCoordinate = markToMarkAdjustmentRecord.baseMarkGlyphAnchorPoint.xCoordinate * num,
							yCoordinate = markToMarkAdjustmentRecord.baseMarkGlyphAnchorPoint.yCoordinate * num
						},
						combiningMarkGlyphID = markToMarkAdjustmentRecord.combiningMarkGlyphID,
						combiningMarkPositionAdjustment = new MarkPositionAdjustment
						{
							xPositionAdjustment = markToMarkAdjustmentRecord.combiningMarkPositionAdjustment.xPositionAdjustment * num,
							yPositionAdjustment = markToMarkAdjustmentRecord.combiningMarkPositionAdjustment.yPositionAdjustment * num
						}
					};
					this.m_FontFeatureTable.MarkToMarkAdjustmentRecords.Add(markToMarkAdjustmentRecord2);
					this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.Add(num2, markToMarkAdjustmentRecord2);
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00009100 File Offset: 0x00007300
		internal IntPtr nativeFontAsset
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			get
			{
				this.EnsureNativeFontAssetIsCreated();
				return this.m_NativeFontAsset;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00009120 File Offset: 0x00007320
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal void EnsureNativeFontAssetIsCreated()
		{
			bool flag = this.m_NativeFontAsset != IntPtr.Zero;
			if (!flag)
			{
				IntPtr[] fallbacks = this.GetFallbacks();
				ValueTuple<IntPtr[], IntPtr[]> weightFallbacks = this.GetWeightFallbacks();
				FontAsset.kFontAssetByInstanceId.TryAdd(base.instanceID, this);
				Font font = null;
				this.m_NativeFontAsset = FontAsset.Create(this.faceInfo, this.sourceFontFile, font, this.m_SourceFontFilePath, base.instanceID, fallbacks, weightFallbacks.Item1, weightFallbacks.Item2);
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00009197 File Offset: 0x00007397
		internal void UpdateFallbacks()
		{
			FontAsset.UpdateFallbacks(this.nativeFontAsset, this.GetFallbacks());
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000091AC File Offset: 0x000073AC
		internal void UpdateWeightFallbacks()
		{
			ValueTuple<IntPtr[], IntPtr[]> weightFallbacks = this.GetWeightFallbacks();
			FontAsset.UpdateWeightFallbacks(this.nativeFontAsset, weightFallbacks.Item1, weightFallbacks.Item2);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000091D9 File Offset: 0x000073D9
		internal void UpdateFaceInfo()
		{
			FontAsset.UpdateFaceInfo(this.nativeFontAsset, this.faceInfo);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000091F0 File Offset: 0x000073F0
		internal IntPtr[] GetFallbacks()
		{
			List<IntPtr> list = new List<IntPtr>();
			bool flag = this.fallbackFontAssetTable == null;
			IntPtr[] array;
			if (flag)
			{
				array = list.ToArray();
			}
			else
			{
				foreach (FontAsset fontAsset in this.fallbackFontAssetTable)
				{
					bool flag2 = fontAsset == null;
					if (!flag2)
					{
						bool flag3 = fontAsset.atlasPopulationMode == AtlasPopulationMode.Static && fontAsset.characterTable.Count > 0;
						if (flag3)
						{
							Debug.LogWarning("Advanced text system cannot use static font asset " + fontAsset.name + " as fallback.");
						}
						else
						{
							bool flag4 = this.HasRecursion(fontAsset);
							if (flag4)
							{
								Debug.LogWarning("Circular reference detected. Cannot add " + fontAsset.name + " to the fallbacks.");
							}
							else
							{
								list.Add(fontAsset.nativeFontAsset);
							}
						}
					}
				}
				array = list.ToArray();
			}
			return array;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000092FC File Offset: 0x000074FC
		private bool HasRecursion(FontAsset fontAsset)
		{
			FontAsset.visitedFontAssets.Clear();
			return this.HasRecursionInternal(fontAsset);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009320 File Offset: 0x00007520
		private bool HasRecursionInternal(FontAsset fontAsset)
		{
			bool flag = FontAsset.visitedFontAssets.Contains(fontAsset.instanceID);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				FontAsset.visitedFontAssets.Add(fontAsset.instanceID);
				bool flag3 = fontAsset.fallbackFontAssetTable != null;
				if (flag3)
				{
					foreach (FontAsset fontAsset2 in fontAsset.fallbackFontAssetTable)
					{
						bool flag4 = this.HasRecursionInternal(fontAsset2);
						if (flag4)
						{
							return true;
						}
					}
				}
				for (int i = 0; i < fontAsset.fontWeightTable.Length; i++)
				{
					FontWeightPair fontWeightPair = fontAsset.fontWeightTable[i];
					bool flag5 = fontWeightPair.regularTypeface != null;
					if (flag5)
					{
						bool flag6 = this.HasRecursionInternal(fontWeightPair.regularTypeface);
						if (flag6)
						{
							return true;
						}
					}
					bool flag7 = fontWeightPair.italicTypeface != null;
					if (flag7)
					{
						bool flag8 = this.HasRecursionInternal(fontWeightPair.italicTypeface);
						if (flag8)
						{
							return true;
						}
					}
				}
				FontAsset.visitedFontAssets.Remove(fontAsset.instanceID);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009468 File Offset: 0x00007668
		private ValueTuple<IntPtr[], IntPtr[]> GetWeightFallbacks()
		{
			IntPtr[] array = new IntPtr[10];
			IntPtr[] array2 = new IntPtr[10];
			int i = 0;
			while (i < this.fontWeightTable.Length)
			{
				FontWeightPair fontWeightPair = this.fontWeightTable[i];
				bool flag = fontWeightPair.regularTypeface != null;
				if (!flag)
				{
					goto IL_00D2;
				}
				bool flag2 = fontWeightPair.regularTypeface.atlasPopulationMode == AtlasPopulationMode.Static && fontWeightPair.regularTypeface.characterTable.Count > 0;
				if (flag2)
				{
					Debug.LogWarning("Advanced text system cannot use static font asset " + fontWeightPair.regularTypeface.name + " as fallback.");
				}
				else
				{
					bool flag3 = this.HasRecursion(fontWeightPair.regularTypeface);
					if (!flag3)
					{
						array[i] = fontWeightPair.regularTypeface.nativeFontAsset;
						goto IL_00D2;
					}
					Debug.LogWarning("Circular reference detected. Cannot add " + fontWeightPair.regularTypeface.name + " to the fallbacks.");
				}
				IL_0179:
				i++;
				continue;
				IL_00D2:
				bool flag4 = fontWeightPair.italicTypeface != null;
				if (flag4)
				{
					bool flag5 = fontWeightPair.italicTypeface.atlasPopulationMode == AtlasPopulationMode.Static && fontWeightPair.italicTypeface.characterTable.Count > 0;
					if (flag5)
					{
						Debug.LogWarning("Advanced text system cannot use static font asset " + fontWeightPair.italicTypeface.name + " as fallback.");
					}
					else
					{
						bool flag6 = this.HasRecursion(fontWeightPair.italicTypeface);
						if (flag6)
						{
							Debug.LogWarning("Circular reference detected. Cannot add " + fontWeightPair.italicTypeface.name + " to the fallbacks.");
						}
						else
						{
							array2[i] = fontWeightPair.italicTypeface.nativeFontAsset;
						}
					}
				}
				goto IL_0179;
			}
			return new ValueTuple<IntPtr[], IntPtr[]>(array, array2);
		}

		// Token: 0x06000133 RID: 307
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CreateHbFaceIfNeeded();

		// Token: 0x06000134 RID: 308 RVA: 0x00009614 File Offset: 0x00007814
		private unsafe static void UpdateFallbacks(IntPtr ptr, IntPtr[] fallbacks)
		{
			Span<IntPtr> span = new Span<IntPtr>(fallbacks);
			fixed (IntPtr* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				FontAsset.UpdateFallbacks_Injected(ptr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00009650 File Offset: 0x00007850
		private unsafe static void UpdateWeightFallbacks(IntPtr ptr, IntPtr[] regularFallbacks, IntPtr[] italicFallbacks)
		{
			Span<IntPtr> span = new Span<IntPtr>(regularFallbacks);
			fixed (IntPtr* ptr2 = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr2, span.Length);
				Span<IntPtr> span2 = new Span<IntPtr>(italicFallbacks);
				fixed (IntPtr* pinnableReference = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span2.Length);
					FontAsset.UpdateWeightFallbacks_Injected(ptr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr2 = null;
				}
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000096B4 File Offset: 0x000078B4
		private unsafe static IntPtr Create(FaceInfo faceInfo, Font sourceFontFile, Font sourceFont_EditorRef, string sourceFontFilePath, int fontInstanceID, IntPtr[] fallbacks, IntPtr[] weightFallbacks, IntPtr[] italicFallbacks)
		{
			IntPtr intPtr3;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Font>(sourceFontFile);
				IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<Font>(sourceFont_EditorRef);
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(sourceFontFilePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = sourceFontFilePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Span<IntPtr> span = new Span<IntPtr>(fallbacks);
				fixed (IntPtr* ptr2 = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, span.Length);
					Span<IntPtr> span2 = new Span<IntPtr>(weightFallbacks);
					fixed (IntPtr* ptr3 = span2.GetPinnableReference())
					{
						ManagedSpanWrapper managedSpanWrapper3 = new ManagedSpanWrapper((void*)ptr3, span2.Length);
						Span<IntPtr> span3 = new Span<IntPtr>(italicFallbacks);
						fixed (IntPtr* ptr4 = span3.GetPinnableReference())
						{
							ManagedSpanWrapper managedSpanWrapper4 = new ManagedSpanWrapper((void*)ptr4, span3.Length);
							intPtr3 = FontAsset.Create_Injected(ref faceInfo, intPtr, intPtr2, ref managedSpanWrapper, fontInstanceID, ref managedSpanWrapper2, ref managedSpanWrapper3, ref managedSpanWrapper4);
						}
					}
				}
			}
			finally
			{
				char* ptr = null;
				IntPtr* ptr2 = null;
				IntPtr* ptr3 = null;
				IntPtr* ptr4 = null;
			}
			return intPtr3;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000979C File Offset: 0x0000799C
		private static void UpdateFaceInfo(IntPtr ptr, FaceInfo faceInfo)
		{
			FontAsset.UpdateFaceInfo_Injected(ptr, ref faceInfo);
		}

		// Token: 0x06000138 RID: 312
		[FreeFunction("FontAsset::Destroy")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr ptr);

		// Token: 0x0600013B RID: 315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateFallbacks_Injected(IntPtr ptr, ref ManagedSpanWrapper fallbacks);

		// Token: 0x0600013C RID: 316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateWeightFallbacks_Injected(IntPtr ptr, ref ManagedSpanWrapper regularFallbacks, ref ManagedSpanWrapper italicFallbacks);

		// Token: 0x0600013D RID: 317
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected([In] ref FaceInfo faceInfo, IntPtr sourceFontFile, IntPtr sourceFont_EditorRef, ref ManagedSpanWrapper sourceFontFilePath, int fontInstanceID, ref ManagedSpanWrapper fallbacks, ref ManagedSpanWrapper weightFallbacks, ref ManagedSpanWrapper italicFallbacks);

		// Token: 0x0600013E RID: 318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateFaceInfo_Injected(IntPtr ptr, [In] ref FaceInfo faceInfo);

		// Token: 0x040000F1 RID: 241
		private static Dictionary<int, FontAsset> kFontAssetByInstanceId = new Dictionary<int, FontAsset>();

		// Token: 0x040000F2 RID: 242
		[SerializeField]
		internal string m_SourceFontFileGUID;

		// Token: 0x040000F3 RID: 243
		[SerializeField]
		internal FontAssetCreationEditorSettings m_fontAssetCreationEditorSettings;

		// Token: 0x040000F4 RID: 244
		[SerializeField]
		private Font m_SourceFontFile;

		// Token: 0x040000F5 RID: 245
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[SerializeField]
		internal string m_SourceFontFilePath;

		// Token: 0x040000F6 RID: 246
		[SerializeField]
		private AtlasPopulationMode m_AtlasPopulationMode;

		// Token: 0x040000F7 RID: 247
		[SerializeField]
		internal bool InternalDynamicOS;

		// Token: 0x040000F8 RID: 248
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal bool IsEditorFont = false;

		// Token: 0x040000F9 RID: 249
		[SerializeField]
		internal FaceInfo m_FaceInfo;

		// Token: 0x040000FA RID: 250
		private int m_FamilyNameHashCode;

		// Token: 0x040000FB RID: 251
		private int m_StyleNameHashCode;

		// Token: 0x040000FC RID: 252
		[SerializeField]
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal List<Glyph> m_GlyphTable = new List<Glyph>();

		// Token: 0x040000FD RID: 253
		internal Dictionary<uint, Glyph> m_GlyphLookupDictionary;

		// Token: 0x040000FE RID: 254
		[SerializeField]
		internal List<Character> m_CharacterTable = new List<Character>();

		// Token: 0x040000FF RID: 255
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal Dictionary<uint, Character> m_CharacterLookupDictionary;

		// Token: 0x04000100 RID: 256
		internal Texture2D m_AtlasTexture;

		// Token: 0x04000101 RID: 257
		[SerializeField]
		internal Texture2D[] m_AtlasTextures;

		// Token: 0x04000102 RID: 258
		[SerializeField]
		internal int m_AtlasTextureIndex;

		// Token: 0x04000103 RID: 259
		[SerializeField]
		private bool m_IsMultiAtlasTexturesEnabled;

		// Token: 0x04000104 RID: 260
		[SerializeField]
		private bool m_GetFontFeatures = true;

		// Token: 0x04000105 RID: 261
		[SerializeField]
		private bool m_ClearDynamicDataOnBuild;

		// Token: 0x04000106 RID: 262
		[SerializeField]
		internal int m_AtlasWidth;

		// Token: 0x04000107 RID: 263
		[SerializeField]
		internal int m_AtlasHeight;

		// Token: 0x04000108 RID: 264
		[SerializeField]
		internal int m_AtlasPadding;

		// Token: 0x04000109 RID: 265
		[SerializeField]
		internal GlyphRenderMode m_AtlasRenderMode;

		// Token: 0x0400010A RID: 266
		[SerializeField]
		private List<GlyphRect> m_UsedGlyphRects;

		// Token: 0x0400010B RID: 267
		[SerializeField]
		private List<GlyphRect> m_FreeGlyphRects;

		// Token: 0x0400010C RID: 268
		[SerializeField]
		internal FontFeatureTable m_FontFeatureTable = new FontFeatureTable();

		// Token: 0x0400010D RID: 269
		[SerializeField]
		internal bool m_ShouldReimportFontFeatures;

		// Token: 0x0400010E RID: 270
		[SerializeField]
		internal List<FontAsset> m_FallbackFontAssetTable;

		// Token: 0x0400010F RID: 271
		[SerializeField]
		private FontWeightPair[] m_FontWeightTable = new FontWeightPair[10];

		// Token: 0x04000110 RID: 272
		[FormerlySerializedAs("normalStyle")]
		[SerializeField]
		internal float m_RegularStyleWeight = 0f;

		// Token: 0x04000111 RID: 273
		[FormerlySerializedAs("normalSpacingOffset")]
		[SerializeField]
		internal float m_RegularStyleSpacing = 0f;

		// Token: 0x04000112 RID: 274
		[SerializeField]
		[FormerlySerializedAs("boldStyle")]
		internal float m_BoldStyleWeight = 0.75f;

		// Token: 0x04000113 RID: 275
		[FormerlySerializedAs("boldSpacing")]
		[SerializeField]
		internal float m_BoldStyleSpacing = 7f;

		// Token: 0x04000114 RID: 276
		[FormerlySerializedAs("italicStyle")]
		[SerializeField]
		internal byte m_ItalicStyleSlant = 35;

		// Token: 0x04000115 RID: 277
		[FormerlySerializedAs("tabSize")]
		[SerializeField]
		internal byte m_TabMultiple = 10;

		// Token: 0x04000116 RID: 278
		internal bool IsFontAssetLookupTablesDirty;

		// Token: 0x04000117 RID: 279
		private IntPtr m_NativeFontAsset = IntPtr.Zero;

		// Token: 0x04000118 RID: 280
		private List<Glyph> m_GlyphsToRender = new List<Glyph>();

		// Token: 0x04000119 RID: 281
		private List<Glyph> m_GlyphsRendered = new List<Glyph>();

		// Token: 0x0400011A RID: 282
		private List<uint> m_GlyphIndexList = new List<uint>();

		// Token: 0x0400011B RID: 283
		private List<uint> m_GlyphIndexListNewlyAdded = new List<uint>();

		// Token: 0x0400011C RID: 284
		internal List<uint> m_GlyphsToAdd = new List<uint>();

		// Token: 0x0400011D RID: 285
		internal HashSet<uint> m_GlyphsToAddLookup = new HashSet<uint>();

		// Token: 0x0400011E RID: 286
		internal List<Character> m_CharactersToAdd = new List<Character>();

		// Token: 0x0400011F RID: 287
		internal HashSet<uint> m_CharactersToAddLookup = new HashSet<uint>();

		// Token: 0x04000120 RID: 288
		internal List<uint> s_MissingCharacterList = new List<uint>();

		// Token: 0x04000121 RID: 289
		internal HashSet<uint> m_MissingUnicodesFromFontFile = new HashSet<uint>();

		// Token: 0x04000122 RID: 290
		internal Dictionary<ValueTuple<uint, uint>, uint> m_VariantGlyphIndexes = new Dictionary<ValueTuple<uint, uint>, uint>();

		// Token: 0x04000123 RID: 291
		internal bool m_IsClone;

		// Token: 0x04000124 RID: 292
		private static readonly List<WeakReference<FontAsset>> s_CallbackInstances = new List<WeakReference<FontAsset>>();

		// Token: 0x04000125 RID: 293
		private static ProfilerMarker k_ReadFontAssetDefinitionMarker = new ProfilerMarker("FontAsset.ReadFontAssetDefinition");

		// Token: 0x04000126 RID: 294
		private static ProfilerMarker k_AddSynthesizedCharactersMarker = new ProfilerMarker("FontAsset.AddSynthesizedCharacters");

		// Token: 0x04000127 RID: 295
		private static ProfilerMarker k_TryAddGlyphMarker = new ProfilerMarker("FontAsset.TryAddGlyph");

		// Token: 0x04000128 RID: 296
		private static ProfilerMarker k_TryAddCharacterMarker = new ProfilerMarker("FontAsset.TryAddCharacter");

		// Token: 0x04000129 RID: 297
		private static ProfilerMarker k_TryAddCharactersMarker = new ProfilerMarker("FontAsset.TryAddCharacters");

		// Token: 0x0400012A RID: 298
		private static ProfilerMarker k_UpdateLigatureSubstitutionRecordsMarker = new ProfilerMarker("FontAsset.UpdateLigatureSubstitutionRecords");

		// Token: 0x0400012B RID: 299
		private static ProfilerMarker k_UpdateGlyphAdjustmentRecordsMarker = new ProfilerMarker("FontAsset.UpdateGlyphAdjustmentRecords");

		// Token: 0x0400012C RID: 300
		private static ProfilerMarker k_UpdateDiacriticalMarkAdjustmentRecordsMarker = new ProfilerMarker("FontAsset.UpdateDiacriticalAdjustmentRecords");

		// Token: 0x0400012D RID: 301
		private static ProfilerMarker k_ClearFontAssetDataMarker = new ProfilerMarker("FontAsset.ClearFontAssetData");

		// Token: 0x0400012E RID: 302
		private static ProfilerMarker k_UpdateFontAssetDataMarker = new ProfilerMarker("FontAsset.UpdateFontAssetData");

		// Token: 0x0400012F RID: 303
		private static string s_DefaultMaterialSuffix = " Atlas Material";

		// Token: 0x04000130 RID: 304
		private static HashSet<int> k_SearchedFontAssetLookup;

		// Token: 0x04000131 RID: 305
		private static List<FontAsset> k_FontAssets_FontFeaturesUpdateQueue = new List<FontAsset>();

		// Token: 0x04000132 RID: 306
		private static HashSet<int> k_FontAssets_FontFeaturesUpdateQueueLookup = new HashSet<int>();

		// Token: 0x04000133 RID: 307
		private static List<FontAsset> k_FontAssets_KerningUpdateQueue = new List<FontAsset>();

		// Token: 0x04000134 RID: 308
		private static HashSet<int> k_FontAssets_KerningUpdateQueueLookup = new HashSet<int>();

		// Token: 0x04000135 RID: 309
		private static List<Texture2D> k_FontAssets_AtlasTexturesUpdateQueue = new List<Texture2D>();

		// Token: 0x04000136 RID: 310
		private static HashSet<int> k_FontAssets_AtlasTexturesUpdateQueueLookup = new HashSet<int>();

		// Token: 0x04000137 RID: 311
		internal static uint[] k_GlyphIndexArray;

		// Token: 0x04000138 RID: 312
		private static HashSet<int> visitedFontAssets = new HashSet<int>();

		// Token: 0x0200002F RID: 47
		internal static class BindingsMarshaller
		{
			// Token: 0x0600013F RID: 319 RVA: 0x000099CB File Offset: 0x00007BCB
			public static IntPtr ConvertToNative(FontAsset fontAsset)
			{
				return fontAsset.m_NativeFontAsset;
			}
		}
	}
}
