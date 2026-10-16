using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000025 RID: 37
	[ExcludeFromPreset]
	[Serializable]
	public class TMP_FontAsset : TMP_Asset
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005537 File Offset: 0x00003737
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000553F File Offset: 0x0000373F
		public FontAssetCreationSettings creationSettings
		{
			get
			{
				return this.m_CreationSettings;
			}
			set
			{
				this.m_CreationSettings = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005548 File Offset: 0x00003748
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00005550 File Offset: 0x00003750
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005559 File Offset: 0x00003759
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00005561 File Offset: 0x00003761
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

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000556A File Offset: 0x0000376A
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00005590 File Offset: 0x00003790
		internal int familyNameHashCode
		{
			get
			{
				if (this.m_FamilyNameHashCode == 0)
				{
					this.m_FamilyNameHashCode = TMP_TextUtilities.GetHashCode(this.m_FaceInfo.familyName);
				}
				return this.m_FamilyNameHashCode;
			}
			set
			{
				this.m_FamilyNameHashCode = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00005599 File Offset: 0x00003799
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000055BF File Offset: 0x000037BF
		internal int styleNameHashCode
		{
			get
			{
				if (this.m_StyleNameHashCode == 0)
				{
					this.m_StyleNameHashCode = TMP_TextUtilities.GetHashCode(this.m_FaceInfo.styleName);
				}
				return this.m_StyleNameHashCode;
			}
			set
			{
				this.m_StyleNameHashCode = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000055C8 File Offset: 0x000037C8
		// (set) Token: 0x06000101 RID: 257 RVA: 0x000055D0 File Offset: 0x000037D0
		public List<Glyph> glyphTable
		{
			get
			{
				return this.m_GlyphTable;
			}
			internal set
			{
				this.m_GlyphTable = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000055D9 File Offset: 0x000037D9
		public Dictionary<uint, Glyph> glyphLookupTable
		{
			get
			{
				if (this.m_GlyphLookupDictionary == null)
				{
					this.ReadFontAssetDefinition();
				}
				return this.m_GlyphLookupDictionary;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000055EF File Offset: 0x000037EF
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000055F7 File Offset: 0x000037F7
		public List<TMP_Character> characterTable
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

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005600 File Offset: 0x00003800
		public Dictionary<uint, TMP_Character> characterLookupTable
		{
			get
			{
				if (this.m_CharacterLookupDictionary == null)
				{
					this.ReadFontAssetDefinition();
				}
				return this.m_CharacterLookupDictionary;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005616 File Offset: 0x00003816
		public Texture2D atlasTexture
		{
			get
			{
				if (this.m_AtlasTexture == null)
				{
					this.m_AtlasTexture = this.atlasTextures[0];
				}
				return this.m_AtlasTexture;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000563A File Offset: 0x0000383A
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00005649 File Offset: 0x00003849
		public Texture2D[] atlasTextures
		{
			get
			{
				Texture2D[] atlasTextures = this.m_AtlasTextures;
				return this.m_AtlasTextures;
			}
			set
			{
				this.m_AtlasTextures = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005652 File Offset: 0x00003852
		public int atlasTextureCount
		{
			get
			{
				return this.m_AtlasTextureIndex + 1;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000565C File Offset: 0x0000385C
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00005664 File Offset: 0x00003864
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000566D File Offset: 0x0000386D
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00005675 File Offset: 0x00003875
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000567E File Offset: 0x0000387E
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00005686 File Offset: 0x00003886
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000568F File Offset: 0x0000388F
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00005697 File Offset: 0x00003897
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000056A0 File Offset: 0x000038A0
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000056A8 File Offset: 0x000038A8
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000056B1 File Offset: 0x000038B1
		// (set) Token: 0x06000115 RID: 277 RVA: 0x000056B9 File Offset: 0x000038B9
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000056C2 File Offset: 0x000038C2
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000056CA File Offset: 0x000038CA
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000056D3 File Offset: 0x000038D3
		// (set) Token: 0x06000119 RID: 281 RVA: 0x000056DB File Offset: 0x000038DB
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000056E4 File Offset: 0x000038E4
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000056EC File Offset: 0x000038EC
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000056F5 File Offset: 0x000038F5
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000056FD File Offset: 0x000038FD
		public TMP_FontFeatureTable fontFeatureTable
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005706 File Offset: 0x00003906
		// (set) Token: 0x0600011F RID: 287 RVA: 0x0000570E File Offset: 0x0000390E
		public List<TMP_FontAsset> fallbackFontAssetTable
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005717 File Offset: 0x00003917
		// (set) Token: 0x06000121 RID: 289 RVA: 0x0000571F File Offset: 0x0000391F
		public TMP_FontWeightPair[] fontWeightTable
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005728 File Offset: 0x00003928
		[Obsolete("The fontInfo property and underlying type is now obsolete. Please use the faceInfo property and FaceInfo type instead.")]
		public FaceInfo_Legacy fontInfo
		{
			get
			{
				return this.m_fontInfo;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005730 File Offset: 0x00003930
		public static TMP_FontAsset CreateFontAsset(string familyName, string styleName, int pointSize = 90)
		{
			FontReference fontReference;
			if (FontEngine.TryGetSystemFontReference(familyName, styleName, out fontReference))
			{
				return TMP_FontAsset.CreateFontAsset(fontReference.filePath, fontReference.faceIndex, pointSize, 9, GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.DynamicOS, true);
			}
			Debug.Log(string.Concat(new string[] { "Unable to find a font file with the specified Family Name [", familyName, "] and Style [", styleName, "]." }));
			return null;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000057A0 File Offset: 0x000039A0
		public static TMP_FontAsset CreateFontAsset(string fontFilePath, int faceIndex, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight)
		{
			return TMP_FontAsset.CreateFontAsset(fontFilePath, faceIndex, samplingPointSize, atlasPadding, renderMode, atlasWidth, atlasHeight, AtlasPopulationMode.Dynamic, true);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000057BE File Offset: 0x000039BE
		private static TMP_FontAsset CreateFontAsset(string fontFilePath, int faceIndex, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode, bool enableMultiAtlasSupport = true)
		{
			if (FontEngine.LoadFontFace(fontFilePath, (float)samplingPointSize, faceIndex) != FontEngineError.Success)
			{
				Debug.Log("Unable to load font face from [" + fontFilePath + "].");
				return null;
			}
			TMP_FontAsset tmp_FontAsset = TMP_FontAsset.CreateFontAssetInstance(null, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
			tmp_FontAsset.m_SourceFontFilePath = fontFilePath;
			return tmp_FontAsset;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000057FA File Offset: 0x000039FA
		public static TMP_FontAsset CreateFontAsset(Font font)
		{
			return TMP_FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.Dynamic, true);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005818 File Offset: 0x00003A18
		public static TMP_FontAsset CreateFontAsset(Font font, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.Dynamic, bool enableMultiAtlasSupport = true)
		{
			return TMP_FontAsset.CreateFontAsset(font, 0, samplingPointSize, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005837 File Offset: 0x00003A37
		private static TMP_FontAsset CreateFontAsset(Font font, int faceIndex, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.Dynamic, bool enableMultiAtlasSupport = true)
		{
			if (FontEngine.LoadFontFace(font, (float)samplingPointSize, faceIndex) != FontEngineError.Success)
			{
				Debug.LogWarning("Unable to load font face for [" + font.name + "]. Make sure \"Include Font Data\" is enabled in the Font Import Settings.", font);
				return null;
			}
			return TMP_FontAsset.CreateFontAssetInstance(font, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005874 File Offset: 0x00003A74
		private static TMP_FontAsset CreateFontAssetInstance(Font font, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode, bool enableMultiAtlasSupport)
		{
			TMP_FontAsset tmp_FontAsset = ScriptableObject.CreateInstance<TMP_FontAsset>();
			tmp_FontAsset.m_Version = "1.1.0";
			tmp_FontAsset.faceInfo = FontEngine.GetFaceInfo();
			if (atlasPopulationMode == AtlasPopulationMode.Dynamic && font != null)
			{
				tmp_FontAsset.sourceFontFile = font;
			}
			tmp_FontAsset.atlasPopulationMode = atlasPopulationMode;
			tmp_FontAsset.clearDynamicDataOnBuild = TMP_Settings.clearDynamicDataOnBuild;
			tmp_FontAsset.atlasWidth = atlasWidth;
			tmp_FontAsset.atlasHeight = atlasHeight;
			tmp_FontAsset.atlasPadding = atlasPadding;
			tmp_FontAsset.atlasRenderMode = renderMode;
			tmp_FontAsset.atlasTextures = new Texture2D[1];
			TextureFormat textureFormat = (((renderMode & (GlyphRenderMode)65536) == (GlyphRenderMode)65536) ? TextureFormat.RGBA32 : TextureFormat.Alpha8);
			Texture2D texture2D = new Texture2D(1, 1, textureFormat, false);
			tmp_FontAsset.atlasTextures[0] = texture2D;
			tmp_FontAsset.isMultiAtlasTexturesEnabled = enableMultiAtlasSupport;
			int num;
			if ((renderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16)
			{
				num = 0;
				Material material;
				if (textureFormat == TextureFormat.Alpha8)
				{
					material = new Material(ShaderUtilities.ShaderRef_MobileBitmap);
				}
				else
				{
					material = new Material(Shader.Find("TextMeshPro/Sprite"));
				}
				material.SetTexture(ShaderUtilities.ID_MainTex, texture2D);
				material.SetFloat(ShaderUtilities.ID_TextureWidth, (float)atlasWidth);
				material.SetFloat(ShaderUtilities.ID_TextureHeight, (float)atlasHeight);
				tmp_FontAsset.material = material;
			}
			else
			{
				num = 1;
				Material material2 = new Material(ShaderUtilities.ShaderRef_MobileSDF);
				material2.SetTexture(ShaderUtilities.ID_MainTex, texture2D);
				material2.SetFloat(ShaderUtilities.ID_TextureWidth, (float)atlasWidth);
				material2.SetFloat(ShaderUtilities.ID_TextureHeight, (float)atlasHeight);
				material2.SetFloat(ShaderUtilities.ID_GradientScale, (float)(atlasPadding + num));
				material2.SetFloat(ShaderUtilities.ID_WeightNormal, tmp_FontAsset.normalStyle);
				material2.SetFloat(ShaderUtilities.ID_WeightBold, tmp_FontAsset.boldStyle);
				tmp_FontAsset.material = material2;
			}
			tmp_FontAsset.freeGlyphRects = new List<GlyphRect>(8)
			{
				new GlyphRect(0, 0, atlasWidth - num, atlasHeight - num)
			};
			tmp_FontAsset.usedGlyphRects = new List<GlyphRect>(8);
			tmp_FontAsset.ReadFontAssetDefinition();
			return tmp_FontAsset;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005A28 File Offset: 0x00003C28
		private void RegisterCallbackInstance(TMP_FontAsset instance)
		{
			for (int i = 0; i < TMP_FontAsset.s_CallbackInstances.Count; i++)
			{
				TMP_FontAsset tmp_FontAsset;
				if (TMP_FontAsset.s_CallbackInstances[i].TryGetTarget(out tmp_FontAsset) && tmp_FontAsset == instance)
				{
					return;
				}
			}
			for (int j = 0; j < TMP_FontAsset.s_CallbackInstances.Count; j++)
			{
				TMP_FontAsset tmp_FontAsset2;
				if (!TMP_FontAsset.s_CallbackInstances[j].TryGetTarget(out tmp_FontAsset2))
				{
					TMP_FontAsset.s_CallbackInstances[j] = new WeakReference<TMP_FontAsset>(instance);
					return;
				}
			}
			TMP_FontAsset.s_CallbackInstances.Add(new WeakReference<TMP_FontAsset>(this));
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005AB3 File Offset: 0x00003CB3
		private void OnDestroy()
		{
			this.DestroyAtlasTextures();
			global::UnityEngine.Object.DestroyImmediate(this.m_Material);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005AC8 File Offset: 0x00003CC8
		public void ReadFontAssetDefinition()
		{
			this.InitializeDictionaryLookupTables();
			this.AddSynthesizedCharactersAndFaceMetrics();
			if (this.m_FaceInfo.capLine == 0f && this.m_CharacterLookupDictionary.ContainsKey(88U))
			{
				uint glyphIndex = this.m_CharacterLookupDictionary[88U].glyphIndex;
				this.m_FaceInfo.capLine = this.m_GlyphLookupDictionary[glyphIndex].metrics.horizontalBearingY;
			}
			if (this.m_FaceInfo.meanLine == 0f && this.m_CharacterLookupDictionary.ContainsKey(120U))
			{
				uint glyphIndex2 = this.m_CharacterLookupDictionary[120U].glyphIndex;
				this.m_FaceInfo.meanLine = this.m_GlyphLookupDictionary[glyphIndex2].metrics.horizontalBearingY;
			}
			if (this.m_FaceInfo.scale == 0f)
			{
				this.m_FaceInfo.scale = 1f;
			}
			if (this.m_FaceInfo.strikethroughOffset == 0f)
			{
				this.m_FaceInfo.strikethroughOffset = this.m_FaceInfo.capLine / 2.5f;
			}
			if (this.m_AtlasPadding == 0 && base.material.HasProperty(ShaderUtilities.ID_GradientScale))
			{
				this.m_AtlasPadding = (int)base.material.GetFloat(ShaderUtilities.ID_GradientScale) - 1;
			}
			if (this.m_FaceInfo.unitsPerEM == 0 && this.atlasPopulationMode != AtlasPopulationMode.Static)
			{
				if (!JobsUtility.IsExecutingJob)
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
			base.hashCode = TMP_TextUtilities.GetHashCode(base.name);
			this.familyNameHashCode = TMP_TextUtilities.GetHashCode(this.m_FaceInfo.familyName);
			this.styleNameHashCode = TMP_TextUtilities.GetHashCode(this.m_FaceInfo.styleName);
			base.materialHashCode = TMP_TextUtilities.GetSimpleHashCode(base.name + TMP_FontAsset.s_DefaultMaterialSuffix);
			TMP_ResourceManager.AddFontAsset(this);
			this.IsFontAssetLookupTablesDirty = false;
			this.RegisterCallbackInstance(this);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005D40 File Offset: 0x00003F40
		internal void InitializeDictionaryLookupTables()
		{
			this.InitializeGlyphLookupDictionary();
			this.InitializeCharacterLookupDictionary();
			if ((this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS) && this.m_ShouldReimportFontFeatures)
			{
				this.ImportFontFeatures();
			}
			this.InitializeLigatureSubstitutionLookupDictionary();
			this.InitializeGlyphPaidAdjustmentRecordsLookupDictionary();
			this.InitializeMarkToBaseAdjustmentRecordsLookupDictionary();
			this.InitializeMarkToMarkAdjustmentRecordsLookupDictionary();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005D94 File Offset: 0x00003F94
		internal void InitializeGlyphLookupDictionary()
		{
			if (this.m_GlyphLookupDictionary == null)
			{
				this.m_GlyphLookupDictionary = new Dictionary<uint, Glyph>();
			}
			else
			{
				this.m_GlyphLookupDictionary.Clear();
			}
			if (this.m_GlyphIndexList == null)
			{
				this.m_GlyphIndexList = new List<uint>();
			}
			else
			{
				this.m_GlyphIndexList.Clear();
			}
			if (this.m_GlyphIndexListNewlyAdded == null)
			{
				this.m_GlyphIndexListNewlyAdded = new List<uint>();
			}
			else
			{
				this.m_GlyphIndexListNewlyAdded.Clear();
			}
			int count = this.m_GlyphTable.Count;
			for (int i = 0; i < count; i++)
			{
				Glyph glyph = this.m_GlyphTable[i];
				uint index = glyph.index;
				if (!this.m_GlyphLookupDictionary.ContainsKey(index))
				{
					this.m_GlyphLookupDictionary.Add(index, glyph);
					this.m_GlyphIndexList.Add(index);
				}
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005E54 File Offset: 0x00004054
		internal void InitializeCharacterLookupDictionary()
		{
			if (this.m_CharacterLookupDictionary == null)
			{
				this.m_CharacterLookupDictionary = new Dictionary<uint, TMP_Character>();
			}
			else
			{
				this.m_CharacterLookupDictionary.Clear();
			}
			for (int i = 0; i < this.m_CharacterTable.Count; i++)
			{
				TMP_Character tmp_Character = this.m_CharacterTable[i];
				uint unicode = tmp_Character.unicode;
				uint glyphIndex = tmp_Character.glyphIndex;
				if (!this.m_CharacterLookupDictionary.ContainsKey(unicode))
				{
					this.m_CharacterLookupDictionary.Add(unicode, tmp_Character);
					tmp_Character.textAsset = this;
					tmp_Character.glyph = this.m_GlyphLookupDictionary[glyphIndex];
				}
			}
			if (this.m_MissingUnicodesFromFontFile != null)
			{
				this.m_MissingUnicodesFromFontFile.Clear();
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005EFC File Offset: 0x000040FC
		internal void ClearFallbackCharacterTable()
		{
			List<uint> list = new List<uint>();
			foreach (KeyValuePair<uint, TMP_Character> keyValuePair in this.m_CharacterLookupDictionary)
			{
				if (keyValuePair.Value.textAsset != this)
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (uint num in list)
			{
				this.m_CharacterLookupDictionary.Remove(num);
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005FB4 File Offset: 0x000041B4
		internal void InitializeLigatureSubstitutionLookupDictionary()
		{
			if (this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup == null)
			{
				this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup = new Dictionary<uint, List<LigatureSubstitutionRecord>>();
			}
			else
			{
				this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.Clear();
			}
			List<LigatureSubstitutionRecord> ligatureSubstitutionRecords = this.m_FontFeatureTable.m_LigatureSubstitutionRecords;
			if (ligatureSubstitutionRecords != null)
			{
				for (int i = 0; i < ligatureSubstitutionRecords.Count; i++)
				{
					LigatureSubstitutionRecord ligatureSubstitutionRecord = ligatureSubstitutionRecords[i];
					if (ligatureSubstitutionRecord.componentGlyphIDs != null && ligatureSubstitutionRecord.componentGlyphIDs.Length != 0)
					{
						uint num = ligatureSubstitutionRecord.componentGlyphIDs[0];
						if (!this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.ContainsKey(num))
						{
							this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.Add(num, new List<LigatureSubstitutionRecord> { ligatureSubstitutionRecord });
						}
						else
						{
							this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup[num].Add(ligatureSubstitutionRecord);
						}
					}
				}
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006080 File Offset: 0x00004280
		internal void InitializeGlyphPaidAdjustmentRecordsLookupDictionary()
		{
			if (this.m_KerningTable != null && this.m_KerningTable.kerningPairs != null && this.m_KerningTable.kerningPairs.Count > 0)
			{
				this.UpgradeGlyphAdjustmentTableToFontFeatureTable();
			}
			if (this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup == null)
			{
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup = new Dictionary<uint, GlyphPairAdjustmentRecord>();
			}
			else
			{
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Clear();
			}
			List<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords = this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords;
			if (glyphPairAdjustmentRecords != null)
			{
				for (int i = 0; i < glyphPairAdjustmentRecords.Count; i++)
				{
					GlyphPairAdjustmentRecord glyphPairAdjustmentRecord = glyphPairAdjustmentRecords[i];
					uint num = (glyphPairAdjustmentRecord.secondAdjustmentRecord.glyphIndex << 16) | glyphPairAdjustmentRecord.firstAdjustmentRecord.glyphIndex;
					if (!this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(num))
					{
						this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(num, glyphPairAdjustmentRecord);
					}
				}
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000615C File Offset: 0x0000435C
		internal void InitializeMarkToBaseAdjustmentRecordsLookupDictionary()
		{
			if (this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup == null)
			{
				this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup = new Dictionary<uint, MarkToBaseAdjustmentRecord>();
			}
			else
			{
				this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.Clear();
			}
			List<MarkToBaseAdjustmentRecord> markToBaseAdjustmentRecords = this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecords;
			if (markToBaseAdjustmentRecords != null)
			{
				for (int i = 0; i < markToBaseAdjustmentRecords.Count; i++)
				{
					MarkToBaseAdjustmentRecord markToBaseAdjustmentRecord = markToBaseAdjustmentRecords[i];
					uint num = (markToBaseAdjustmentRecord.markGlyphID << 16) | markToBaseAdjustmentRecord.baseGlyphID;
					if (!this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.ContainsKey(num))
					{
						this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.Add(num, markToBaseAdjustmentRecord);
					}
				}
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000061F8 File Offset: 0x000043F8
		internal void InitializeMarkToMarkAdjustmentRecordsLookupDictionary()
		{
			if (this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup == null)
			{
				this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup = new Dictionary<uint, MarkToMarkAdjustmentRecord>();
			}
			else
			{
				this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.Clear();
			}
			List<MarkToMarkAdjustmentRecord> markToMarkAdjustmentRecords = this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecords;
			if (markToMarkAdjustmentRecords != null)
			{
				for (int i = 0; i < markToMarkAdjustmentRecords.Count; i++)
				{
					MarkToMarkAdjustmentRecord markToMarkAdjustmentRecord = markToMarkAdjustmentRecords[i];
					uint num = (markToMarkAdjustmentRecord.combiningMarkGlyphID << 16) | markToMarkAdjustmentRecord.baseMarkGlyphID;
					if (!this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.ContainsKey(num))
					{
						this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.Add(num, markToMarkAdjustmentRecord);
					}
				}
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006294 File Offset: 0x00004494
		internal void AddSynthesizedCharactersAndFaceMetrics()
		{
			bool flag = false;
			if (this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS)
			{
				flag = this.LoadFontFace() == FontEngineError.Success;
				if (!flag && !this.InternalDynamicOS && TMP_Settings.warningsDisabled)
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

		// Token: 0x06000136 RID: 310 RVA: 0x00006378 File Offset: 0x00004578
		private void AddSynthesizedCharacter(uint unicode, bool isFontFaceLoaded, bool addImmediately = false)
		{
			if (this.m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				return;
			}
			Glyph glyph;
			if (!isFontFaceLoaded || FontEngine.GetGlyphIndex(unicode) == 0U)
			{
				glyph = new Glyph(0U, new GlyphMetrics(0f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
				this.m_CharacterLookupDictionary.Add(unicode, new TMP_Character(unicode, this, glyph));
				return;
			}
			if (!addImmediately)
			{
				return;
			}
			GlyphLoadFlags glyphLoadFlags = (((this.m_AtlasRenderMode & (GlyphRenderMode)4) == (GlyphRenderMode)4) ? (GlyphLoadFlags.LOAD_NO_HINTING | GlyphLoadFlags.LOAD_NO_BITMAP) : GlyphLoadFlags.LOAD_NO_BITMAP);
			if (FontEngine.TryGetGlyphWithUnicodeValue(unicode, glyphLoadFlags, out glyph))
			{
				this.m_CharacterLookupDictionary.Add(unicode, new TMP_Character(unicode, this, glyph));
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006418 File Offset: 0x00004618
		internal void AddCharacterToLookupCache(uint unicode, TMP_Character character, FontStyles fontStyle = FontStyles.Normal, FontWeight fontWeight = FontWeight.Regular, bool isAlternativeTypeface = false)
		{
			uint num = unicode;
			if (fontStyle != FontStyles.Normal || fontWeight != FontWeight.Regular)
			{
				num = (uint)(((uint)((isAlternativeTypeface ? FontStyles.Superscript : FontStyles.Normal) | ((uint)fontStyle << 4) | (FontStyles)(fontWeight / FontWeight.Thin)) << 24) | (FontStyles)unicode);
			}
			this.m_CharacterLookupDictionary.TryAdd(num, character);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006460 File Offset: 0x00004660
		internal FontEngineError LoadFontFace()
		{
			if (this.m_AtlasPopulationMode != AtlasPopulationMode.Dynamic)
			{
				return FontEngine.LoadFontFace(this.m_FaceInfo.familyName, this.m_FaceInfo.styleName, this.m_FaceInfo.pointSize);
			}
			if (FontEngine.LoadFontFace(this.m_SourceFontFile, this.m_FaceInfo.pointSize, this.m_FaceInfo.faceIndex) == FontEngineError.Success)
			{
				return FontEngineError.Success;
			}
			if (!string.IsNullOrEmpty(this.m_SourceFontFilePath))
			{
				return FontEngine.LoadFontFace(this.m_SourceFontFilePath, this.m_FaceInfo.pointSize, this.m_FaceInfo.faceIndex);
			}
			return FontEngineError.Invalid_Face;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000064F4 File Offset: 0x000046F4
		internal void SortCharacterTable()
		{
			if (this.m_CharacterTable != null && this.m_CharacterTable.Count > 0)
			{
				this.m_CharacterTable = this.m_CharacterTable.OrderBy<TMP_Character, uint>((TMP_Character c) => c.unicode).ToList<TMP_Character>();
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000654C File Offset: 0x0000474C
		internal void SortGlyphTable()
		{
			if (this.m_GlyphTable != null && this.m_GlyphTable.Count > 0)
			{
				this.m_GlyphTable = this.m_GlyphTable.OrderBy<Glyph, uint>((Glyph c) => c.index).ToList<Glyph>();
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000065A4 File Offset: 0x000047A4
		internal void SortFontFeatureTable()
		{
			this.m_FontFeatureTable.SortGlyphPairAdjustmentRecords();
			this.m_FontFeatureTable.SortMarkToBaseAdjustmentRecords();
			this.m_FontFeatureTable.SortMarkToMarkAdjustmentRecords();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000065C7 File Offset: 0x000047C7
		internal void SortAllTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
			this.SortFontFeatureTable();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000065DB File Offset: 0x000047DB
		public bool HasCharacter(int character)
		{
			return this.characterLookupTable != null && this.m_CharacterLookupDictionary.ContainsKey((uint)character);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000065F4 File Offset: 0x000047F4
		public bool HasCharacter(char character, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			if (this.characterLookupTable == null)
			{
				return false;
			}
			if (this.m_CharacterLookupDictionary.ContainsKey((uint)character))
			{
				return true;
			}
			TMP_Character tmp_Character;
			if (tryAddCharacter && (this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS) && this.TryAddCharacterInternal((uint)character, out tmp_Character))
			{
				return true;
			}
			if (searchFallbacks)
			{
				if (TMP_FontAsset.k_SearchedFontAssetLookup == null)
				{
					TMP_FontAsset.k_SearchedFontAssetLookup = new HashSet<int>();
				}
				else
				{
					TMP_FontAsset.k_SearchedFontAssetLookup.Clear();
				}
				TMP_FontAsset.k_SearchedFontAssetLookup.Add(base.GetInstanceID());
				if (this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0)
				{
					int num = 0;
					while (num < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num] != null)
					{
						TMP_FontAsset tmp_FontAsset = this.fallbackFontAssetTable[num];
						int instanceID = tmp_FontAsset.GetInstanceID();
						if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(instanceID) && tmp_FontAsset.HasCharacter_Internal((uint)character, true, tryAddCharacter))
						{
							return true;
						}
						num++;
					}
				}
				if (TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
				{
					int num2 = 0;
					while (num2 < TMP_Settings.fallbackFontAssets.Count && TMP_Settings.fallbackFontAssets[num2] != null)
					{
						TMP_FontAsset tmp_FontAsset2 = TMP_Settings.fallbackFontAssets[num2];
						int instanceID2 = tmp_FontAsset2.GetInstanceID();
						if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(instanceID2) && tmp_FontAsset2.HasCharacter_Internal((uint)character, true, tryAddCharacter))
						{
							return true;
						}
						num2++;
					}
				}
				if (TMP_Settings.defaultFontAsset != null)
				{
					TMP_FontAsset defaultFontAsset = TMP_Settings.defaultFontAsset;
					int instanceID3 = defaultFontAsset.GetInstanceID();
					if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(instanceID3) && defaultFontAsset.HasCharacter_Internal((uint)character, true, tryAddCharacter))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006790 File Offset: 0x00004990
		private bool HasCharacter_Internal(uint character, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			if (this.m_CharacterLookupDictionary == null)
			{
				this.ReadFontAssetDefinition();
				if (this.m_CharacterLookupDictionary == null)
				{
					return false;
				}
			}
			if (this.m_CharacterLookupDictionary.ContainsKey(character))
			{
				return true;
			}
			TMP_Character tmp_Character;
			if (tryAddCharacter && (this.atlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS) && this.TryAddCharacterInternal(character, out tmp_Character))
			{
				return true;
			}
			if (searchFallbacks)
			{
				if (this.fallbackFontAssetTable == null || this.fallbackFontAssetTable.Count == 0)
				{
					return false;
				}
				int num = 0;
				while (num < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num] != null)
				{
					TMP_FontAsset tmp_FontAsset = this.fallbackFontAssetTable[num];
					int instanceID = tmp_FontAsset.GetInstanceID();
					if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(instanceID) && tmp_FontAsset.HasCharacter_Internal(character, true, tryAddCharacter))
					{
						return true;
					}
					num++;
				}
			}
			return false;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000685C File Offset: 0x00004A5C
		public bool HasCharacters(string text, out List<char> missingCharacters)
		{
			if (this.characterLookupTable == null)
			{
				missingCharacters = null;
				return false;
			}
			missingCharacters = new List<char>();
			for (int i = 0; i < text.Length; i++)
			{
				if (!this.m_CharacterLookupDictionary.ContainsKey((uint)text[i]))
				{
					missingCharacters.Add(text[i]);
				}
			}
			return missingCharacters.Count == 0;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000068BC File Offset: 0x00004ABC
		public bool HasCharacters(string text, out uint[] missingCharacters, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			missingCharacters = null;
			if (this.characterLookupTable == null)
			{
				return false;
			}
			this.s_MissingCharacterList.Clear();
			for (int i = 0; i < text.Length; i++)
			{
				bool flag = true;
				uint num = (uint)text[i];
				TMP_Character tmp_Character;
				if (!this.m_CharacterLookupDictionary.ContainsKey(num) && (!tryAddCharacter || (this.atlasPopulationMode != AtlasPopulationMode.Dynamic && this.m_AtlasPopulationMode != AtlasPopulationMode.DynamicOS) || !this.TryAddCharacterInternal(num, out tmp_Character)))
				{
					if (searchFallbacks)
					{
						if (TMP_FontAsset.k_SearchedFontAssetLookup == null)
						{
							TMP_FontAsset.k_SearchedFontAssetLookup = new HashSet<int>();
						}
						else
						{
							TMP_FontAsset.k_SearchedFontAssetLookup.Clear();
						}
						TMP_FontAsset.k_SearchedFontAssetLookup.Add(base.GetInstanceID());
						if (this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0)
						{
							int num2 = 0;
							while (num2 < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num2] != null)
							{
								TMP_FontAsset tmp_FontAsset = this.fallbackFontAssetTable[num2];
								int instanceID = tmp_FontAsset.GetInstanceID();
								if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(instanceID) && tmp_FontAsset.HasCharacter_Internal(num, true, tryAddCharacter))
								{
									flag = false;
									break;
								}
								num2++;
							}
						}
						if (flag && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
						{
							int num3 = 0;
							while (num3 < TMP_Settings.fallbackFontAssets.Count && TMP_Settings.fallbackFontAssets[num3] != null)
							{
								TMP_FontAsset tmp_FontAsset2 = TMP_Settings.fallbackFontAssets[num3];
								int instanceID2 = tmp_FontAsset2.GetInstanceID();
								if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(instanceID2) && tmp_FontAsset2.HasCharacter_Internal(num, true, tryAddCharacter))
								{
									flag = false;
									break;
								}
								num3++;
							}
						}
						if (flag && TMP_Settings.defaultFontAsset != null)
						{
							TMP_FontAsset defaultFontAsset = TMP_Settings.defaultFontAsset;
							int instanceID3 = defaultFontAsset.GetInstanceID();
							if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(instanceID3) && defaultFontAsset.HasCharacter_Internal(num, true, tryAddCharacter))
							{
								flag = false;
							}
						}
					}
					if (flag)
					{
						this.s_MissingCharacterList.Add(num);
					}
				}
			}
			if (this.s_MissingCharacterList.Count > 0)
			{
				missingCharacters = this.s_MissingCharacterList.ToArray();
				return false;
			}
			return true;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006ACC File Offset: 0x00004CCC
		public bool HasCharacters(string text)
		{
			if (this.characterLookupTable == null)
			{
				return false;
			}
			for (int i = 0; i < text.Length; i++)
			{
				if (!this.m_CharacterLookupDictionary.ContainsKey((uint)text[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006B0C File Offset: 0x00004D0C
		public static string GetCharacters(TMP_FontAsset fontAsset)
		{
			string text = string.Empty;
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				text += ((char)fontAsset.characterTable[i].unicode).ToString();
			}
			return text;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006B58 File Offset: 0x00004D58
		public static int[] GetCharactersArray(TMP_FontAsset fontAsset)
		{
			int[] array = new int[fontAsset.characterTable.Count];
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				array[i] = (int)fontAsset.characterTable[i].unicode;
			}
			return array;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006BA1 File Offset: 0x00004DA1
		internal uint GetGlyphIndex(uint unicode)
		{
			if (this.m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				return this.m_CharacterLookupDictionary[unicode].glyphIndex;
			}
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				return 0U;
			}
			return FontEngine.GetGlyphIndex(unicode);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006BD3 File Offset: 0x00004DD3
		internal uint GetGlyphVariantIndex(uint unicode, uint variantSelectorUnicode)
		{
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				return 0U;
			}
			return FontEngine.GetVariantGlyphIndex(unicode, variantSelectorUnicode);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006BE8 File Offset: 0x00004DE8
		internal static void RegisterFontAssetForFontFeatureUpdate(TMP_FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			if (TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueueLookup.Add(instanceID))
			{
				TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Add(fontAsset);
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006C14 File Offset: 0x00004E14
		internal static void UpdateFontFeaturesForFontAssetsInQueue()
		{
			int count = TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Count;
			for (int i = 0; i < count; i++)
			{
				TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueue[i].UpdateGPOSFontFeaturesForNewlyAddedGlyphs();
			}
			if (count > 0)
			{
				TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Clear();
				TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueueLookup.Clear();
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006C60 File Offset: 0x00004E60
		internal static void RegisterAtlasTextureForApply(Texture2D texture)
		{
			int instanceID = texture.GetInstanceID();
			if (TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Add(instanceID))
			{
				TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueue.Add(texture);
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006C8C File Offset: 0x00004E8C
		internal static void UpdateAtlasTexturesInQueue()
		{
			int count = TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Count;
			for (int i = 0; i < count; i++)
			{
				TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueue[i].Apply(false, false);
			}
			if (count > 0)
			{
				TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueue.Clear();
				TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Clear();
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006CDA File Offset: 0x00004EDA
		internal static void UpdateFontAssetsInUpdateQueue()
		{
			TMP_FontAsset.UpdateAtlasTexturesInQueue();
			TMP_FontAsset.UpdateFontFeaturesForFontAssetsInQueue();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006CE8 File Offset: 0x00004EE8
		public bool TryAddCharacters(uint[] unicodes, bool includeFontFeatures = false)
		{
			uint[] array;
			return this.TryAddCharacters(unicodes, out array, includeFontFeatures);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006D00 File Offset: 0x00004F00
		public bool TryAddCharacters(uint[] unicodes, out uint[] missingUnicodes, bool includeFontFeatures = false)
		{
			if (unicodes == null || unicodes.Length == 0 || this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				if (this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided Unicode list is Null or Empty.", this);
				}
				missingUnicodes = null;
				return false;
			}
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				missingUnicodes = unicodes.ToArray<uint>();
				return false;
			}
			if (this.m_CharacterLookupDictionary == null || this.m_GlyphLookupDictionary == null)
			{
				this.ReadFontAssetDefinition();
			}
			this.m_GlyphsToAdd.Clear();
			this.m_GlyphsToAddLookup.Clear();
			this.m_CharactersToAdd.Clear();
			this.m_CharactersToAddLookup.Clear();
			this.s_MissingCharacterList.Clear();
			bool flag = false;
			int num = unicodes.Length;
			for (int i = 0; i < num; i++)
			{
				uint num2 = unicodes[i];
				if (!this.m_CharacterLookupDictionary.ContainsKey(num2))
				{
					uint num3 = FontEngine.GetGlyphIndex(num2);
					if (num3 == 0U)
					{
						if (num2 != 160U)
						{
							if (num2 == 173U || num2 == 8209U)
							{
								num3 = FontEngine.GetGlyphIndex(45U);
							}
						}
						else
						{
							num3 = FontEngine.GetGlyphIndex(32U);
						}
						if (num3 == 0U)
						{
							this.s_MissingCharacterList.Add(num2);
							flag = true;
							goto IL_01BB;
						}
					}
					TMP_Character tmp_Character = new TMP_Character(num2, num3);
					if (this.m_GlyphLookupDictionary.ContainsKey(num3))
					{
						tmp_Character.glyph = this.m_GlyphLookupDictionary[num3];
						tmp_Character.textAsset = this;
						this.m_CharacterTable.Add(tmp_Character);
						this.m_CharacterLookupDictionary.Add(num2, tmp_Character);
					}
					else
					{
						if (this.m_GlyphsToAddLookup.Add(num3))
						{
							this.m_GlyphsToAdd.Add(num3);
						}
						if (this.m_CharactersToAddLookup.Add(num2))
						{
							this.m_CharactersToAdd.Add(tmp_Character);
						}
					}
				}
				IL_01BB:;
			}
			if (this.m_GlyphsToAdd.Count == 0)
			{
				missingUnicodes = unicodes;
				return false;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width <= 1 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height <= 1)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			Glyph[] array;
			bool flag2 = FontEngine.TryAddGlyphsToTexture(this.m_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out array);
			int num4 = 0;
			while (num4 < array.Length && array[num4] != null)
			{
				Glyph glyph = array[num4];
				uint index = glyph.index;
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(index, glyph);
				this.m_GlyphIndexListNewlyAdded.Add(index);
				this.m_GlyphIndexList.Add(index);
				num4++;
			}
			this.m_GlyphsToAdd.Clear();
			for (int j = 0; j < this.m_CharactersToAdd.Count; j++)
			{
				TMP_Character tmp_Character2 = this.m_CharactersToAdd[j];
				Glyph glyph2;
				if (!this.m_GlyphLookupDictionary.TryGetValue(tmp_Character2.glyphIndex, out glyph2))
				{
					this.m_GlyphsToAdd.Add(tmp_Character2.glyphIndex);
				}
				else
				{
					tmp_Character2.glyph = glyph2;
					tmp_Character2.textAsset = this;
					this.m_CharacterTable.Add(tmp_Character2);
					this.m_CharacterLookupDictionary.Add(tmp_Character2.unicode, tmp_Character2);
					this.m_CharactersToAdd.RemoveAt(j);
					j--;
				}
			}
			if (this.m_IsMultiAtlasTexturesEnabled && !flag2)
			{
				while (!flag2)
				{
					flag2 = this.TryAddGlyphsToNewAtlasTexture();
				}
			}
			if (includeFontFeatures)
			{
				this.UpdateFontFeaturesForNewlyAddedGlyphs();
			}
			for (int k = 0; k < this.m_CharactersToAdd.Count; k++)
			{
				TMP_Character tmp_Character3 = this.m_CharactersToAdd[k];
				this.s_MissingCharacterList.Add(tmp_Character3.unicode);
			}
			missingUnicodes = null;
			if (this.s_MissingCharacterList.Count > 0)
			{
				missingUnicodes = this.s_MissingCharacterList.ToArray();
			}
			return flag2 && !flag;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000710C File Offset: 0x0000530C
		public bool TryAddCharacters(string characters, bool includeFontFeatures = false)
		{
			string text;
			return this.TryAddCharacters(characters, out text, includeFontFeatures);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007124 File Offset: 0x00005324
		public bool TryAddCharacters(string characters, out string missingCharacters, bool includeFontFeatures = false)
		{
			if (string.IsNullOrEmpty(characters) || this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				if (this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided character list is Null or Empty.", this);
				}
				missingCharacters = characters;
				return false;
			}
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				missingCharacters = characters;
				return false;
			}
			if (this.m_CharacterLookupDictionary == null || this.m_GlyphLookupDictionary == null)
			{
				this.ReadFontAssetDefinition();
			}
			this.m_GlyphsToAdd.Clear();
			this.m_GlyphsToAddLookup.Clear();
			this.m_CharactersToAdd.Clear();
			this.m_CharactersToAddLookup.Clear();
			this.s_MissingCharacterList.Clear();
			bool flag = false;
			int length = characters.Length;
			for (int i = 0; i < length; i++)
			{
				uint num = (uint)characters[i];
				if (!this.m_CharacterLookupDictionary.ContainsKey(num))
				{
					uint num2 = FontEngine.GetGlyphIndex(num);
					if (num2 == 0U)
					{
						if (num != 160U)
						{
							if (num == 173U || num == 8209U)
							{
								num2 = FontEngine.GetGlyphIndex(45U);
							}
						}
						else
						{
							num2 = FontEngine.GetGlyphIndex(32U);
						}
						if (num2 == 0U)
						{
							this.s_MissingCharacterList.Add(num);
							flag = true;
							goto IL_01BE;
						}
					}
					TMP_Character tmp_Character = new TMP_Character(num, num2);
					if (this.m_GlyphLookupDictionary.ContainsKey(num2))
					{
						tmp_Character.glyph = this.m_GlyphLookupDictionary[num2];
						tmp_Character.textAsset = this;
						this.m_CharacterTable.Add(tmp_Character);
						this.m_CharacterLookupDictionary.Add(num, tmp_Character);
					}
					else
					{
						if (this.m_GlyphsToAddLookup.Add(num2))
						{
							this.m_GlyphsToAdd.Add(num2);
						}
						if (this.m_CharactersToAddLookup.Add(num))
						{
							this.m_CharactersToAdd.Add(tmp_Character);
						}
					}
				}
				IL_01BE:;
			}
			if (this.m_GlyphsToAdd.Count == 0)
			{
				missingCharacters = characters;
				return false;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width <= 1 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height <= 1)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			Glyph[] array;
			bool flag2 = FontEngine.TryAddGlyphsToTexture(this.m_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out array);
			int num3 = 0;
			while (num3 < array.Length && array[num3] != null)
			{
				Glyph glyph = array[num3];
				uint index = glyph.index;
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(index, glyph);
				this.m_GlyphIndexListNewlyAdded.Add(index);
				this.m_GlyphIndexList.Add(index);
				num3++;
			}
			this.m_GlyphsToAdd.Clear();
			for (int j = 0; j < this.m_CharactersToAdd.Count; j++)
			{
				TMP_Character tmp_Character2 = this.m_CharactersToAdd[j];
				Glyph glyph2;
				if (!this.m_GlyphLookupDictionary.TryGetValue(tmp_Character2.glyphIndex, out glyph2))
				{
					this.m_GlyphsToAdd.Add(tmp_Character2.glyphIndex);
				}
				else
				{
					tmp_Character2.glyph = glyph2;
					tmp_Character2.textAsset = this;
					this.m_CharacterTable.Add(tmp_Character2);
					this.m_CharacterLookupDictionary.Add(tmp_Character2.unicode, tmp_Character2);
					this.m_CharactersToAdd.RemoveAt(j);
					j--;
				}
			}
			if (this.m_IsMultiAtlasTexturesEnabled && !flag2)
			{
				while (!flag2)
				{
					flag2 = this.TryAddGlyphsToNewAtlasTexture();
				}
			}
			if (includeFontFeatures)
			{
				this.UpdateFontFeaturesForNewlyAddedGlyphs();
			}
			missingCharacters = string.Empty;
			for (int k = 0; k < this.m_CharactersToAdd.Count; k++)
			{
				TMP_Character tmp_Character3 = this.m_CharactersToAdd[k];
				this.s_MissingCharacterList.Add(tmp_Character3.unicode);
			}
			if (this.s_MissingCharacterList.Count > 0)
			{
				missingCharacters = this.s_MissingCharacterList.UintToString();
			}
			return flag2 && !flag;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007538 File Offset: 0x00005738
		internal bool AddGlyphInternal(uint glyphIndex)
		{
			Glyph glyph;
			return this.TryAddGlyphInternal(glyphIndex, out glyph);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007550 File Offset: 0x00005750
		internal bool TryAddGlyphInternal(uint glyphIndex, out Glyph glyph)
		{
			glyph = null;
			if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				glyph = this.m_GlyphLookupDictionary[glyphIndex];
				return true;
			}
			if (this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				return false;
			}
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				return false;
			}
			if (!this.m_AtlasTextures[this.m_AtlasTextureIndex].isReadable)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Unable to add the requested glyph to font asset [",
					base.name,
					"]'s atlas texture. Please make the texture [",
					this.m_AtlasTextures[this.m_AtlasTextureIndex].name,
					"] readable."
				}), this.m_AtlasTextures[this.m_AtlasTextureIndex]);
				return false;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width <= 1 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height <= 1)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			if (FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph))
			{
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				this.m_GlyphIndexList.Add(glyphIndex);
				this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
				if (this.m_GetFontFeatures && TMP_Settings.getFontFeaturesAtRuntime)
				{
					this.UpdateGSUBFontFeaturesForNewGlyphIndex(glyphIndex);
					TMP_FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
				}
				return true;
			}
			if (this.m_IsMultiAtlasTexturesEnabled && this.m_UsedGlyphRects.Count > 0)
			{
				this.SetupNewAtlasTexture();
				if (FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph))
				{
					glyph.atlasIndex = this.m_AtlasTextureIndex;
					this.m_GlyphTable.Add(glyph);
					this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
					this.m_GlyphIndexList.Add(glyphIndex);
					this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
					if (this.m_GetFontFeatures && TMP_Settings.getFontFeaturesAtRuntime)
					{
						this.UpdateGSUBFontFeaturesForNewGlyphIndex(glyphIndex);
						TMP_FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007794 File Offset: 0x00005994
		internal bool TryAddCharacterInternal(uint unicode, out TMP_Character character)
		{
			character = null;
			if (this.m_MissingUnicodesFromFontFile.Contains(unicode))
			{
				return false;
			}
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				return false;
			}
			uint num = FontEngine.GetGlyphIndex(unicode);
			if (num == 0U)
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
				if (num == 0U)
				{
					this.m_MissingUnicodesFromFontFile.Add(unicode);
					return false;
				}
			}
			if (this.m_GlyphLookupDictionary.ContainsKey(num))
			{
				character = new TMP_Character(unicode, this, this.m_GlyphLookupDictionary[num]);
				this.m_CharacterTable.Add(character);
				this.m_CharacterLookupDictionary.Add(unicode, character);
				return true;
			}
			Glyph glyph = null;
			if (!this.m_AtlasTextures[this.m_AtlasTextureIndex].isReadable)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Unable to add the requested character to font asset [",
					base.name,
					"]'s atlas texture. Please make the texture [",
					this.m_AtlasTextures[this.m_AtlasTextureIndex].name,
					"] readable."
				}), this.m_AtlasTextures[this.m_AtlasTextureIndex]);
				return false;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width <= 1 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height <= 1)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			if (FontEngine.TryAddGlyphToTexture(num, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph))
			{
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(num, glyph);
				character = new TMP_Character(unicode, this, glyph);
				this.m_CharacterTable.Add(character);
				this.m_CharacterLookupDictionary.Add(unicode, character);
				this.m_GlyphIndexList.Add(num);
				this.m_GlyphIndexListNewlyAdded.Add(num);
				if (this.m_GetFontFeatures && TMP_Settings.getFontFeaturesAtRuntime)
				{
					this.UpdateGSUBFontFeaturesForNewGlyphIndex(num);
					TMP_FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
				}
				return true;
			}
			if (this.m_IsMultiAtlasTexturesEnabled && this.m_UsedGlyphRects.Count > 0)
			{
				this.SetupNewAtlasTexture();
				if (FontEngine.TryAddGlyphToTexture(num, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph))
				{
					glyph.atlasIndex = this.m_AtlasTextureIndex;
					this.m_GlyphTable.Add(glyph);
					this.m_GlyphLookupDictionary.Add(num, glyph);
					character = new TMP_Character(unicode, this, glyph);
					this.m_CharacterTable.Add(character);
					this.m_CharacterLookupDictionary.Add(unicode, character);
					this.m_GlyphIndexList.Add(num);
					this.m_GlyphIndexListNewlyAdded.Add(num);
					if (this.m_GetFontFeatures && TMP_Settings.getFontFeaturesAtRuntime)
					{
						this.UpdateGSUBFontFeaturesForNewGlyphIndex(num);
						TMP_FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007A98 File Offset: 0x00005C98
		internal bool TryGetCharacter_and_QueueRenderToTexture(uint unicode, out TMP_Character character)
		{
			character = null;
			if (this.m_MissingUnicodesFromFontFile.Contains(unicode))
			{
				return false;
			}
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				return false;
			}
			uint num = FontEngine.GetGlyphIndex(unicode);
			if (num == 0U)
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
				if (num == 0U)
				{
					this.m_MissingUnicodesFromFontFile.Add(unicode);
					return false;
				}
			}
			if (this.m_GlyphLookupDictionary.ContainsKey(num))
			{
				character = new TMP_Character(unicode, this, this.m_GlyphLookupDictionary[num]);
				this.m_CharacterTable.Add(character);
				this.m_CharacterLookupDictionary.Add(unicode, character);
				return true;
			}
			GlyphLoadFlags glyphLoadFlags = ((((GlyphRenderMode)4 & this.m_AtlasRenderMode) == (GlyphRenderMode)4) ? (GlyphLoadFlags.LOAD_NO_HINTING | GlyphLoadFlags.LOAD_NO_BITMAP) : GlyphLoadFlags.LOAD_NO_BITMAP);
			Glyph glyph = null;
			if (FontEngine.TryGetGlyphWithIndexValue(num, glyphLoadFlags, out glyph))
			{
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(num, glyph);
				character = new TMP_Character(unicode, this, glyph);
				this.m_CharacterTable.Add(character);
				this.m_CharacterLookupDictionary.Add(unicode, character);
				this.m_GlyphIndexList.Add(num);
				this.m_GlyphIndexListNewlyAdded.Add(num);
				if (this.m_GetFontFeatures && TMP_Settings.getFontFeaturesAtRuntime)
				{
					this.UpdateGSUBFontFeaturesForNewGlyphIndex(num);
					TMP_FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
				}
				this.m_GlyphsToRender.Add(glyph);
				return true;
			}
			return false;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007BEC File Offset: 0x00005DEC
		internal void TryAddGlyphsToAtlasTextures()
		{
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007BF0 File Offset: 0x00005DF0
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
				TMP_Character tmp_Character = this.m_CharactersToAdd[i];
				Glyph glyph2;
				if (!this.m_GlyphLookupDictionary.TryGetValue(tmp_Character.glyphIndex, out glyph2))
				{
					this.m_GlyphsToAdd.Add(tmp_Character.glyphIndex);
				}
				else
				{
					tmp_Character.glyph = glyph2;
					tmp_Character.textAsset = this;
					this.m_CharacterTable.Add(tmp_Character);
					this.m_CharacterLookupDictionary.Add(tmp_Character.unicode, tmp_Character);
					this.m_CharactersToAdd.RemoveAt(i);
					i--;
				}
			}
			return flag;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007D40 File Offset: 0x00005F40
		private void SetupNewAtlasTexture()
		{
			this.m_AtlasTextureIndex++;
			if (this.m_AtlasTextures.Length == this.m_AtlasTextureIndex)
			{
				Array.Resize<Texture2D>(ref this.m_AtlasTextures, this.m_AtlasTextures.Length * 2);
			}
			TextureFormat textureFormat = (((this.m_AtlasRenderMode & (GlyphRenderMode)65536) == (GlyphRenderMode)65536) ? TextureFormat.RGBA32 : TextureFormat.Alpha8);
			this.m_AtlasTextures[this.m_AtlasTextureIndex] = new Texture2D(this.m_AtlasWidth, this.m_AtlasHeight, textureFormat, false);
			FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			int num = (((this.m_AtlasRenderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16) ? 0 : 1);
			this.m_FreeGlyphRects.Clear();
			this.m_FreeGlyphRects.Add(new GlyphRect(0, 0, this.m_AtlasWidth - num, this.m_AtlasHeight - num));
			this.m_UsedGlyphRects.Clear();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00007E14 File Offset: 0x00006014
		internal void UpdateAtlasTexture()
		{
			if (this.m_GlyphsToRender.Count == 0)
			{
				return;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width <= 1 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height <= 1)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			this.m_AtlasTextures[this.m_AtlasTextureIndex].Apply(false, false);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007E9E File Offset: 0x0000609E
		private void UpdateFontFeaturesForNewlyAddedGlyphs()
		{
			this.UpdateLigatureSubstitutionRecords();
			this.UpdateGlyphAdjustmentRecords();
			this.UpdateDiacriticalMarkAdjustmentRecords();
			this.m_GlyphIndexListNewlyAdded.Clear();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007EBD File Offset: 0x000060BD
		private void UpdateGPOSFontFeaturesForNewlyAddedGlyphs()
		{
			this.UpdateGlyphAdjustmentRecords();
			this.UpdateDiacriticalMarkAdjustmentRecords();
			this.m_GlyphIndexListNewlyAdded.Clear();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00007ED8 File Offset: 0x000060D8
		internal void ImportFontFeatures()
		{
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				return;
			}
			GlyphPairAdjustmentRecord[] allPairAdjustmentRecords = FontEngine.GetAllPairAdjustmentRecords();
			if (allPairAdjustmentRecords != null)
			{
				this.AddPairAdjustmentRecords(allPairAdjustmentRecords);
			}
			MarkToBaseAdjustmentRecord[] allMarkToBaseAdjustmentRecords = FontEngine.GetAllMarkToBaseAdjustmentRecords();
			if (allMarkToBaseAdjustmentRecords != null)
			{
				this.AddMarkToBaseAdjustmentRecords(allMarkToBaseAdjustmentRecords);
			}
			MarkToMarkAdjustmentRecord[] allMarkToMarkAdjustmentRecords = FontEngine.GetAllMarkToMarkAdjustmentRecords();
			if (allMarkToMarkAdjustmentRecords != null)
			{
				this.AddMarkToMarkAdjustmentRecords(allMarkToMarkAdjustmentRecords);
			}
			LigatureSubstitutionRecord[] allLigatureSubstitutionRecords = FontEngine.GetAllLigatureSubstitutionRecords();
			if (allLigatureSubstitutionRecords != null)
			{
				this.AddLigatureSubstitutionRecords(allLigatureSubstitutionRecords);
			}
			this.m_ShouldReimportFontFeatures = false;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007F38 File Offset: 0x00006138
		private void UpdateGSUBFontFeaturesForNewGlyphIndex(uint glyphIndex)
		{
			LigatureSubstitutionRecord[] ligatureSubstitutionRecords = FontEngine.GetLigatureSubstitutionRecords(glyphIndex);
			if (ligatureSubstitutionRecords != null)
			{
				this.AddLigatureSubstitutionRecords(ligatureSubstitutionRecords);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007F58 File Offset: 0x00006158
		internal void UpdateLigatureSubstitutionRecords()
		{
			LigatureSubstitutionRecord[] ligatureSubstitutionRecords = FontEngine.GetLigatureSubstitutionRecords(this.m_GlyphIndexListNewlyAdded);
			if (ligatureSubstitutionRecords != null)
			{
				this.AddLigatureSubstitutionRecords(ligatureSubstitutionRecords);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007F7C File Offset: 0x0000617C
		private void AddLigatureSubstitutionRecords(LigatureSubstitutionRecord[] records)
		{
			for (int i = 0; i < records.Length; i++)
			{
				LigatureSubstitutionRecord ligatureSubstitutionRecord = records[i];
				if (records[i].componentGlyphIDs == null || records[i].ligatureGlyphID == 0U)
				{
					return;
				}
				uint num = ligatureSubstitutionRecord.componentGlyphIDs[0];
				LigatureSubstitutionRecord ligatureSubstitutionRecord2 = new LigatureSubstitutionRecord
				{
					componentGlyphIDs = ligatureSubstitutionRecord.componentGlyphIDs,
					ligatureGlyphID = ligatureSubstitutionRecord.ligatureGlyphID
				};
				List<LigatureSubstitutionRecord> list;
				if (this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.TryGetValue(num, out list))
				{
					foreach (LigatureSubstitutionRecord ligatureSubstitutionRecord3 in list)
					{
						if (ligatureSubstitutionRecord2 == ligatureSubstitutionRecord3)
						{
							return;
						}
					}
					this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup[num].Add(ligatureSubstitutionRecord2);
				}
				else
				{
					this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.Add(num, new List<LigatureSubstitutionRecord> { ligatureSubstitutionRecord2 });
				}
				this.m_FontFeatureTable.m_LigatureSubstitutionRecords.Add(ligatureSubstitutionRecord2);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008098 File Offset: 0x00006298
		internal void UpdateGlyphAdjustmentRecords()
		{
			GlyphPairAdjustmentRecord[] pairAdjustmentRecords = FontEngine.GetPairAdjustmentRecords(this.m_GlyphIndexListNewlyAdded);
			if (pairAdjustmentRecords != null)
			{
				this.AddPairAdjustmentRecords(pairAdjustmentRecords);
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000080BC File Offset: 0x000062BC
		private void AddPairAdjustmentRecords(GlyphPairAdjustmentRecord[] records)
		{
			float num = this.m_FaceInfo.pointSize / (float)this.m_FaceInfo.unitsPerEM;
			foreach (GlyphPairAdjustmentRecord glyphPairAdjustmentRecord in records)
			{
				GlyphAdjustmentRecord firstAdjustmentRecord = glyphPairAdjustmentRecord.firstAdjustmentRecord;
				GlyphAdjustmentRecord secondAdjustmentRecord = glyphPairAdjustmentRecord.secondAdjustmentRecord;
				uint glyphIndex = firstAdjustmentRecord.glyphIndex;
				uint glyphIndex2 = secondAdjustmentRecord.glyphIndex;
				if (glyphIndex == 0U && glyphIndex2 == 0U)
				{
					return;
				}
				uint num2 = (glyphIndex2 << 16) | glyphIndex;
				if (!this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(num2))
				{
					GlyphValueRecord glyphValueRecord = firstAdjustmentRecord.glyphValueRecord;
					glyphValueRecord.xAdvance *= num;
					glyphPairAdjustmentRecord.firstAdjustmentRecord = new GlyphAdjustmentRecord(glyphIndex, glyphValueRecord);
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(glyphPairAdjustmentRecord);
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(num2, glyphPairAdjustmentRecord);
				}
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008198 File Offset: 0x00006398
		internal void UpdateGlyphAdjustmentRecords(uint[] glyphIndexes)
		{
			GlyphPairAdjustmentRecord[] glyphPairAdjustmentTable = FontEngine.GetGlyphPairAdjustmentTable(glyphIndexes);
			if (glyphPairAdjustmentTable == null || glyphPairAdjustmentTable.Length == 0)
			{
				return;
			}
			if (this.m_FontFeatureTable == null)
			{
				this.m_FontFeatureTable = new TMP_FontFeatureTable();
			}
			int num = 0;
			while (num < glyphPairAdjustmentTable.Length && glyphPairAdjustmentTable[num].firstAdjustmentRecord.glyphIndex != 0U)
			{
				uint num2 = (glyphPairAdjustmentTable[num].secondAdjustmentRecord.glyphIndex << 16) | glyphPairAdjustmentTable[num].firstAdjustmentRecord.glyphIndex;
				if (!this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(num2))
				{
					GlyphPairAdjustmentRecord glyphPairAdjustmentRecord = glyphPairAdjustmentTable[num];
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(glyphPairAdjustmentRecord);
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(num2, glyphPairAdjustmentRecord);
				}
				num++;
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000825C File Offset: 0x0000645C
		internal void UpdateDiacriticalMarkAdjustmentRecords()
		{
			MarkToBaseAdjustmentRecord[] markToBaseAdjustmentRecords = FontEngine.GetMarkToBaseAdjustmentRecords(this.m_GlyphIndexListNewlyAdded);
			if (markToBaseAdjustmentRecords != null)
			{
				this.AddMarkToBaseAdjustmentRecords(markToBaseAdjustmentRecords);
			}
			MarkToMarkAdjustmentRecord[] markToMarkAdjustmentRecords = FontEngine.GetMarkToMarkAdjustmentRecords(this.m_GlyphIndexListNewlyAdded);
			if (markToMarkAdjustmentRecords != null)
			{
				this.AddMarkToMarkAdjustmentRecords(markToMarkAdjustmentRecords);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008298 File Offset: 0x00006498
		private void AddMarkToBaseAdjustmentRecords(MarkToBaseAdjustmentRecord[] records)
		{
			float num = this.m_FaceInfo.pointSize / (float)this.m_FaceInfo.unitsPerEM;
			for (int i = 0; i < records.Length; i++)
			{
				MarkToBaseAdjustmentRecord markToBaseAdjustmentRecord = records[i];
				if (records[i].baseGlyphID == 0U || records[i].markGlyphID == 0U)
				{
					return;
				}
				uint num2 = (markToBaseAdjustmentRecord.markGlyphID << 16) | markToBaseAdjustmentRecord.baseGlyphID;
				if (!this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.ContainsKey(num2))
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

		// Token: 0x06000163 RID: 355 RVA: 0x000083F4 File Offset: 0x000065F4
		private void AddMarkToMarkAdjustmentRecords(MarkToMarkAdjustmentRecord[] records)
		{
			float num = this.m_FaceInfo.pointSize / (float)this.m_FaceInfo.unitsPerEM;
			for (int i = 0; i < records.Length; i++)
			{
				MarkToMarkAdjustmentRecord markToMarkAdjustmentRecord = records[i];
				if (records[i].baseMarkGlyphID == 0U || records[i].combiningMarkGlyphID == 0U)
				{
					return;
				}
				uint num2 = (markToMarkAdjustmentRecord.combiningMarkGlyphID << 16) | markToMarkAdjustmentRecord.baseMarkGlyphID;
				if (!this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.ContainsKey(num2))
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

		// Token: 0x06000164 RID: 356 RVA: 0x00008550 File Offset: 0x00006750
		private void CopyListDataToArray<T>(List<T> srcList, ref T[] dstArray)
		{
			int count = srcList.Count;
			if (dstArray == null)
			{
				dstArray = new T[count];
			}
			else
			{
				Array.Resize<T>(ref dstArray, count);
			}
			for (int i = 0; i < count; i++)
			{
				dstArray[i] = srcList[i];
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008594 File Offset: 0x00006794
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
			if (array.Length != 0)
			{
				this.TryAddCharacters(array, this.m_GetFontFeatures && TMP_Settings.getFontFeaturesAtRuntime);
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008614 File Offset: 0x00006814
		public void ClearFontAssetData(bool setAtlasSizeToZero = false)
		{
			this.ClearCharacterAndGlyphTables();
			this.ClearFontFeaturesTables();
			this.ClearAtlasTextures(setAtlasSizeToZero);
			this.ReadFontAssetDefinition();
			for (int i = 0; i < TMP_FontAsset.s_CallbackInstances.Count; i++)
			{
				TMP_FontAsset tmp_FontAsset;
				if (TMP_FontAsset.s_CallbackInstances[i].TryGetTarget(out tmp_FontAsset) && tmp_FontAsset != this)
				{
					tmp_FontAsset.ClearFallbackCharacterTable();
				}
			}
			TMPro_EventManager.ON_FONT_PROPERTY_CHANGED(true, this);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008679 File Offset: 0x00006879
		internal void ClearCharacterAndGlyphTablesInternal()
		{
			this.ClearCharacterAndGlyphTables();
			this.ClearAtlasTextures(true);
			this.ReadFontAssetDefinition();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000868E File Offset: 0x0000688E
		internal void ClearFontFeaturesInternal()
		{
			this.ClearFontFeaturesTables();
			this.ReadFontAssetDefinition();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000869C File Offset: 0x0000689C
		private void ClearCharacterAndGlyphTables()
		{
			if (this.m_GlyphTable != null)
			{
				this.m_GlyphTable.Clear();
			}
			if (this.m_CharacterTable != null)
			{
				this.m_CharacterTable.Clear();
			}
			if (this.m_UsedGlyphRects != null)
			{
				this.m_UsedGlyphRects.Clear();
			}
			if (this.m_FreeGlyphRects != null)
			{
				int num = (((this.m_AtlasRenderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16) ? 0 : 1);
				this.m_FreeGlyphRects.Clear();
				this.m_FreeGlyphRects.Add(new GlyphRect(0, 0, this.m_AtlasWidth - num, this.m_AtlasHeight - num));
			}
			if (this.m_GlyphsToRender != null)
			{
				this.m_GlyphsToRender.Clear();
			}
			if (this.m_GlyphsRendered != null)
			{
				this.m_GlyphsRendered.Clear();
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008750 File Offset: 0x00006950
		private void ClearFontFeaturesTables()
		{
			if (this.m_FontFeatureTable != null && this.m_FontFeatureTable.m_LigatureSubstitutionRecords != null)
			{
				this.m_FontFeatureTable.m_LigatureSubstitutionRecords.Clear();
			}
			if (this.m_FontFeatureTable != null && this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords != null)
			{
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Clear();
			}
			if (this.m_FontFeatureTable != null && this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecords != null)
			{
				this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecords.Clear();
			}
			if (this.m_FontFeatureTable != null && this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecords != null)
			{
				this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecords.Clear();
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000087F4 File Offset: 0x000069F4
		internal void ClearAtlasTextures(bool setAtlasSizeToZero = false)
		{
			this.m_AtlasTextureIndex = 0;
			if (this.m_AtlasTextures == null)
			{
				return;
			}
			Texture2D texture2D;
			for (int i = 1; i < this.m_AtlasTextures.Length; i++)
			{
				texture2D = this.m_AtlasTextures[i];
				if (!(texture2D == null))
				{
					global::UnityEngine.Object.DestroyImmediate(texture2D, true);
				}
			}
			Array.Resize<Texture2D>(ref this.m_AtlasTextures, 1);
			texture2D = (this.m_AtlasTexture = this.m_AtlasTextures[0]);
			bool isReadable = texture2D.isReadable;
			TextureFormat textureFormat = (((this.m_AtlasRenderMode & (GlyphRenderMode)65536) == (GlyphRenderMode)65536) ? TextureFormat.RGBA32 : TextureFormat.Alpha8);
			if (setAtlasSizeToZero)
			{
				texture2D.Reinitialize(1, 1, textureFormat, false);
			}
			else if (texture2D.width != this.m_AtlasWidth || texture2D.height != this.m_AtlasHeight)
			{
				texture2D.Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight, textureFormat, false);
			}
			FontEngine.ResetAtlasTexture(texture2D);
			texture2D.Apply();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000088CC File Offset: 0x00006ACC
		private void DestroyAtlasTextures()
		{
			if (this.m_AtlasTextures == null)
			{
				return;
			}
			for (int i = 0; i < this.m_AtlasTextures.Length; i++)
			{
				Texture2D texture2D = this.m_AtlasTextures[i];
				if (texture2D != null)
				{
					global::UnityEngine.Object.DestroyImmediate(texture2D);
				}
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008910 File Offset: 0x00006B10
		private void UpgradeGlyphAdjustmentTableToFontFeatureTable()
		{
			Debug.Log("Upgrading font asset [" + base.name + "] Glyph Adjustment Table.", this);
			if (this.m_FontFeatureTable == null)
			{
				this.m_FontFeatureTable = new TMP_FontFeatureTable();
			}
			int count = this.m_KerningTable.kerningPairs.Count;
			this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords = new List<GlyphPairAdjustmentRecord>(count);
			for (int i = 0; i < count; i++)
			{
				KerningPair kerningPair = this.m_KerningTable.kerningPairs[i];
				uint num = 0U;
				TMP_Character tmp_Character;
				if (this.m_CharacterLookupDictionary.TryGetValue(kerningPair.firstGlyph, out tmp_Character))
				{
					num = tmp_Character.glyphIndex;
				}
				uint num2 = 0U;
				TMP_Character tmp_Character2;
				if (this.m_CharacterLookupDictionary.TryGetValue(kerningPair.secondGlyph, out tmp_Character2))
				{
					num2 = tmp_Character2.glyphIndex;
				}
				GlyphAdjustmentRecord glyphAdjustmentRecord = new GlyphAdjustmentRecord(num, new GlyphValueRecord(kerningPair.firstGlyphAdjustments.xPlacement, kerningPair.firstGlyphAdjustments.yPlacement, kerningPair.firstGlyphAdjustments.xAdvance, kerningPair.firstGlyphAdjustments.yAdvance));
				GlyphAdjustmentRecord glyphAdjustmentRecord2 = new GlyphAdjustmentRecord(num2, new GlyphValueRecord(kerningPair.secondGlyphAdjustments.xPlacement, kerningPair.secondGlyphAdjustments.yPlacement, kerningPair.secondGlyphAdjustments.xAdvance, kerningPair.secondGlyphAdjustments.yAdvance));
				GlyphPairAdjustmentRecord glyphPairAdjustmentRecord = new GlyphPairAdjustmentRecord(glyphAdjustmentRecord, glyphAdjustmentRecord2);
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(glyphPairAdjustmentRecord);
			}
			this.m_KerningTable.kerningPairs = null;
			this.m_KerningTable = null;
		}

		// Token: 0x040000A7 RID: 167
		[SerializeField]
		internal string m_SourceFontFileGUID;

		// Token: 0x040000A8 RID: 168
		[SerializeField]
		internal FontAssetCreationSettings m_CreationSettings;

		// Token: 0x040000A9 RID: 169
		[SerializeField]
		private Font m_SourceFontFile;

		// Token: 0x040000AA RID: 170
		[SerializeField]
		private string m_SourceFontFilePath;

		// Token: 0x040000AB RID: 171
		[SerializeField]
		private AtlasPopulationMode m_AtlasPopulationMode;

		// Token: 0x040000AC RID: 172
		[SerializeField]
		internal bool InternalDynamicOS;

		// Token: 0x040000AD RID: 173
		private int m_FamilyNameHashCode;

		// Token: 0x040000AE RID: 174
		private int m_StyleNameHashCode;

		// Token: 0x040000AF RID: 175
		[SerializeField]
		internal List<Glyph> m_GlyphTable = new List<Glyph>();

		// Token: 0x040000B0 RID: 176
		internal Dictionary<uint, Glyph> m_GlyphLookupDictionary;

		// Token: 0x040000B1 RID: 177
		[SerializeField]
		internal List<TMP_Character> m_CharacterTable = new List<TMP_Character>();

		// Token: 0x040000B2 RID: 178
		internal Dictionary<uint, TMP_Character> m_CharacterLookupDictionary;

		// Token: 0x040000B3 RID: 179
		internal Texture2D m_AtlasTexture;

		// Token: 0x040000B4 RID: 180
		[SerializeField]
		internal Texture2D[] m_AtlasTextures;

		// Token: 0x040000B5 RID: 181
		[SerializeField]
		internal int m_AtlasTextureIndex;

		// Token: 0x040000B6 RID: 182
		[SerializeField]
		private bool m_IsMultiAtlasTexturesEnabled;

		// Token: 0x040000B7 RID: 183
		[SerializeField]
		private bool m_GetFontFeatures = true;

		// Token: 0x040000B8 RID: 184
		[SerializeField]
		private bool m_ClearDynamicDataOnBuild;

		// Token: 0x040000B9 RID: 185
		[SerializeField]
		internal int m_AtlasWidth;

		// Token: 0x040000BA RID: 186
		[SerializeField]
		internal int m_AtlasHeight;

		// Token: 0x040000BB RID: 187
		[SerializeField]
		internal int m_AtlasPadding;

		// Token: 0x040000BC RID: 188
		[SerializeField]
		internal GlyphRenderMode m_AtlasRenderMode;

		// Token: 0x040000BD RID: 189
		[SerializeField]
		private List<GlyphRect> m_UsedGlyphRects;

		// Token: 0x040000BE RID: 190
		[SerializeField]
		private List<GlyphRect> m_FreeGlyphRects;

		// Token: 0x040000BF RID: 191
		[SerializeField]
		internal TMP_FontFeatureTable m_FontFeatureTable = new TMP_FontFeatureTable();

		// Token: 0x040000C0 RID: 192
		[SerializeField]
		internal bool m_ShouldReimportFontFeatures;

		// Token: 0x040000C1 RID: 193
		[SerializeField]
		internal List<TMP_FontAsset> m_FallbackFontAssetTable;

		// Token: 0x040000C2 RID: 194
		[SerializeField]
		private TMP_FontWeightPair[] m_FontWeightTable = new TMP_FontWeightPair[10];

		// Token: 0x040000C3 RID: 195
		[SerializeField]
		private TMP_FontWeightPair[] fontWeights;

		// Token: 0x040000C4 RID: 196
		public float normalStyle;

		// Token: 0x040000C5 RID: 197
		public float normalSpacingOffset;

		// Token: 0x040000C6 RID: 198
		public float boldStyle = 0.75f;

		// Token: 0x040000C7 RID: 199
		public float boldSpacing = 7f;

		// Token: 0x040000C8 RID: 200
		public byte italicStyle = 35;

		// Token: 0x040000C9 RID: 201
		public byte tabSize = 10;

		// Token: 0x040000CA RID: 202
		internal bool IsFontAssetLookupTablesDirty;

		// Token: 0x040000CB RID: 203
		[SerializeField]
		private FaceInfo_Legacy m_fontInfo;

		// Token: 0x040000CC RID: 204
		[SerializeField]
		internal List<TMP_Glyph> m_glyphInfoList;

		// Token: 0x040000CD RID: 205
		[SerializeField]
		[FormerlySerializedAs("m_kerningInfo")]
		internal KerningTable m_KerningTable = new KerningTable();

		// Token: 0x040000CE RID: 206
		[SerializeField]
		private List<TMP_FontAsset> fallbackFontAssets;

		// Token: 0x040000CF RID: 207
		[SerializeField]
		public Texture2D atlas;

		// Token: 0x040000D0 RID: 208
		private static readonly List<WeakReference<TMP_FontAsset>> s_CallbackInstances = new List<WeakReference<TMP_FontAsset>>();

		// Token: 0x040000D1 RID: 209
		private static ProfilerMarker k_ReadFontAssetDefinitionMarker = new ProfilerMarker("TMP.ReadFontAssetDefinition");

		// Token: 0x040000D2 RID: 210
		private static ProfilerMarker k_AddSynthesizedCharactersMarker = new ProfilerMarker("TMP.AddSynthesizedCharacters");

		// Token: 0x040000D3 RID: 211
		private static ProfilerMarker k_TryAddGlyphMarker = new ProfilerMarker("TMP.TryAddGlyph");

		// Token: 0x040000D4 RID: 212
		private static ProfilerMarker k_TryAddCharacterMarker = new ProfilerMarker("TMP.TryAddCharacter");

		// Token: 0x040000D5 RID: 213
		private static ProfilerMarker k_TryAddCharactersMarker = new ProfilerMarker("TMP.TryAddCharacters");

		// Token: 0x040000D6 RID: 214
		private static ProfilerMarker k_UpdateLigatureSubstitutionRecordsMarker = new ProfilerMarker("TMP.UpdateLigatureSubstitutionRecords");

		// Token: 0x040000D7 RID: 215
		private static ProfilerMarker k_UpdateGlyphAdjustmentRecordsMarker = new ProfilerMarker("TMP.UpdateGlyphAdjustmentRecords");

		// Token: 0x040000D8 RID: 216
		private static ProfilerMarker k_UpdateDiacriticalMarkAdjustmentRecordsMarker = new ProfilerMarker("TMP.UpdateDiacriticalAdjustmentRecords");

		// Token: 0x040000D9 RID: 217
		private static ProfilerMarker k_ClearFontAssetDataMarker = new ProfilerMarker("TMP.ClearFontAssetData");

		// Token: 0x040000DA RID: 218
		private static ProfilerMarker k_UpdateFontAssetDataMarker = new ProfilerMarker("TMP.UpdateFontAssetData");

		// Token: 0x040000DB RID: 219
		private static string s_DefaultMaterialSuffix = " Atlas Material";

		// Token: 0x040000DC RID: 220
		private static HashSet<int> k_SearchedFontAssetLookup;

		// Token: 0x040000DD RID: 221
		private static List<TMP_FontAsset> k_FontAssets_FontFeaturesUpdateQueue = new List<TMP_FontAsset>();

		// Token: 0x040000DE RID: 222
		private static HashSet<int> k_FontAssets_FontFeaturesUpdateQueueLookup = new HashSet<int>();

		// Token: 0x040000DF RID: 223
		private static List<Texture2D> k_FontAssets_AtlasTexturesUpdateQueue = new List<Texture2D>();

		// Token: 0x040000E0 RID: 224
		private static HashSet<int> k_FontAssets_AtlasTexturesUpdateQueueLookup = new HashSet<int>();

		// Token: 0x040000E1 RID: 225
		private List<Glyph> m_GlyphsToRender = new List<Glyph>();

		// Token: 0x040000E2 RID: 226
		private List<Glyph> m_GlyphsRendered = new List<Glyph>();

		// Token: 0x040000E3 RID: 227
		private List<uint> m_GlyphIndexList = new List<uint>();

		// Token: 0x040000E4 RID: 228
		private List<uint> m_GlyphIndexListNewlyAdded = new List<uint>();

		// Token: 0x040000E5 RID: 229
		internal List<uint> m_GlyphsToAdd = new List<uint>();

		// Token: 0x040000E6 RID: 230
		internal HashSet<uint> m_GlyphsToAddLookup = new HashSet<uint>();

		// Token: 0x040000E7 RID: 231
		internal List<TMP_Character> m_CharactersToAdd = new List<TMP_Character>();

		// Token: 0x040000E8 RID: 232
		internal HashSet<uint> m_CharactersToAddLookup = new HashSet<uint>();

		// Token: 0x040000E9 RID: 233
		internal List<uint> s_MissingCharacterList = new List<uint>();

		// Token: 0x040000EA RID: 234
		internal HashSet<uint> m_MissingUnicodesFromFontFile = new HashSet<uint>();

		// Token: 0x040000EB RID: 235
		internal static uint[] k_GlyphIndexArray;
	}
}
