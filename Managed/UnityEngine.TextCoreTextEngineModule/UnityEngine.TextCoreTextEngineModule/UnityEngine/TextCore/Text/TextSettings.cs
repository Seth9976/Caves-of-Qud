using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Serialization;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200003A RID: 58
	[ExcludeFromObjectFactory]
	[NativeHeader("Modules/TextCoreTextEngine/Native/TextSettings.h")]
	[ExcludeFromPreset]
	[Serializable]
	public class TextSettings : ScriptableObject
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000B40C File Offset: 0x0000960C
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000B414 File Offset: 0x00009614
		public string version
		{
			get
			{
				return this.m_Version;
			}
			internal set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000B41D File Offset: 0x0000961D
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000B425 File Offset: 0x00009625
		public FontAsset defaultFontAsset
		{
			get
			{
				return this.m_DefaultFontAsset;
			}
			set
			{
				this.m_DefaultFontAsset = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000B42E File Offset: 0x0000962E
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000B436 File Offset: 0x00009636
		public string defaultFontAssetPath
		{
			get
			{
				return this.m_DefaultFontAssetPath;
			}
			set
			{
				this.m_DefaultFontAssetPath = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000B43F File Offset: 0x0000963F
		// (set) Token: 0x06000197 RID: 407 RVA: 0x0000B447 File Offset: 0x00009647
		public List<FontAsset> fallbackFontAssets
		{
			get
			{
				return this.m_FallbackFontAssets;
			}
			set
			{
				this.m_FallbackFontAssets = value;
				this.m_IsNativeTextSettingsDirty = true;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000B458 File Offset: 0x00009658
		internal List<FontAsset> fallbackOSFontAssets
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			get
			{
				bool flag = this.GetStaticFallbackOSFontAsset() == null;
				if (flag)
				{
					this.SetStaticFallbackOSFontAsset(this.GetOSFontAssetList());
				}
				return this.GetStaticFallbackOSFontAsset();
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000B48C File Offset: 0x0000968C
		internal virtual List<FontAsset> GetStaticFallbackOSFontAsset()
		{
			return TextSettings.s_FallbackOSFontAssetInternal;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000B4A3 File Offset: 0x000096A3
		internal virtual void SetStaticFallbackOSFontAsset(List<FontAsset> fontAssets)
		{
			TextSettings.s_FallbackOSFontAssetInternal = fontAssets;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000B4AC File Offset: 0x000096AC
		internal virtual List<FontAsset> GetFallbackFontAssets(int textPixelSize = -1)
		{
			return this.fallbackFontAssets;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000B4C4 File Offset: 0x000096C4
		// (set) Token: 0x0600019D RID: 413 RVA: 0x0000B4CC File Offset: 0x000096CC
		public bool matchMaterialPreset
		{
			get
			{
				return this.m_MatchMaterialPreset;
			}
			set
			{
				this.m_MatchMaterialPreset = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000B4D5 File Offset: 0x000096D5
		// (set) Token: 0x0600019F RID: 415 RVA: 0x0000B4DD File Offset: 0x000096DD
		public int missingCharacterUnicode
		{
			get
			{
				return this.m_MissingCharacterUnicode;
			}
			set
			{
				this.m_MissingCharacterUnicode = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000B4E6 File Offset: 0x000096E6
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x0000B4EE File Offset: 0x000096EE
		public bool clearDynamicDataOnBuild
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000B4F8 File Offset: 0x000096F8
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000B510 File Offset: 0x00009710
		public bool enableEmojiSupport
		{
			get
			{
				return this.m_EnableEmojiSupport;
			}
			set
			{
				this.m_EnableEmojiSupport = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000B51A File Offset: 0x0000971A
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000B522 File Offset: 0x00009722
		public List<TextAsset> emojiFallbackTextAssets
		{
			get
			{
				return this.m_EmojiFallbackTextAssets;
			}
			set
			{
				this.m_EmojiFallbackTextAssets = value;
				this.m_IsNativeTextSettingsDirty = true;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000B533 File Offset: 0x00009733
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000B53B File Offset: 0x0000973B
		public SpriteAsset defaultSpriteAsset
		{
			get
			{
				return this.m_DefaultSpriteAsset;
			}
			set
			{
				this.m_DefaultSpriteAsset = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000B544 File Offset: 0x00009744
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x0000B54C File Offset: 0x0000974C
		public string defaultSpriteAssetPath
		{
			get
			{
				return this.m_DefaultSpriteAssetPath;
			}
			set
			{
				this.m_DefaultSpriteAssetPath = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000B555 File Offset: 0x00009755
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000B55D File Offset: 0x0000975D
		[Obsolete("The Fallback Sprite Assets list is now obsolete. Use the emojiFallbackTextAssets instead.", true)]
		public List<SpriteAsset> fallbackSpriteAssets
		{
			get
			{
				return this.m_FallbackSpriteAssets;
			}
			set
			{
				this.m_FallbackSpriteAssets = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000B56E File Offset: 0x0000976E
		// (set) Token: 0x060001AC RID: 428 RVA: 0x0000B566 File Offset: 0x00009766
		internal static SpriteAsset s_GlobalSpriteAsset { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000B575 File Offset: 0x00009775
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000B57D File Offset: 0x0000977D
		public uint missingSpriteCharacterUnicode
		{
			get
			{
				return this.m_MissingSpriteCharacterUnicode;
			}
			set
			{
				this.m_MissingSpriteCharacterUnicode = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000B586 File Offset: 0x00009786
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000B58E File Offset: 0x0000978E
		public TextStyleSheet defaultStyleSheet
		{
			get
			{
				return this.m_DefaultStyleSheet;
			}
			set
			{
				this.m_DefaultStyleSheet = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000B597 File Offset: 0x00009797
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000B59F File Offset: 0x0000979F
		public string styleSheetsResourcePath
		{
			get
			{
				return this.m_StyleSheetsResourcePath;
			}
			set
			{
				this.m_StyleSheetsResourcePath = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000B5A8 File Offset: 0x000097A8
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000B5B0 File Offset: 0x000097B0
		public string defaultColorGradientPresetsPath
		{
			get
			{
				return this.m_DefaultColorGradientPresetsPath;
			}
			set
			{
				this.m_DefaultColorGradientPresetsPath = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000B5BC File Offset: 0x000097BC
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000B5FA File Offset: 0x000097FA
		public UnicodeLineBreakingRules lineBreakingRules
		{
			get
			{
				bool flag = this.m_UnicodeLineBreakingRules == null;
				if (flag)
				{
					this.m_UnicodeLineBreakingRules = new UnicodeLineBreakingRules();
					this.m_UnicodeLineBreakingRules.LoadLineBreakingRules();
				}
				return this.m_UnicodeLineBreakingRules;
			}
			set
			{
				this.m_UnicodeLineBreakingRules = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000B603 File Offset: 0x00009803
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000B60B File Offset: 0x0000980B
		public bool displayWarnings
		{
			get
			{
				return this.m_DisplayWarnings;
			}
			set
			{
				this.m_DisplayWarnings = value;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000B614 File Offset: 0x00009814
		private void OnEnable()
		{
			this.lineBreakingRules.LoadLineBreakingRules();
			this.SetStaticFallbackOSFontAsset(null);
			bool flag = TextSettings.s_GlobalSpriteAsset == null;
			if (flag)
			{
				TextSettings.s_GlobalSpriteAsset = Resources.Load<SpriteAsset>("Sprite Assets/Default Sprite Asset");
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000B658 File Offset: 0x00009858
		private void OnDestroy()
		{
			bool flag = this.m_NativeTextSettings != IntPtr.Zero;
			if (flag)
			{
				TextSettings.DestroyNativeObject(this.m_NativeTextSettings);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000B688 File Offset: 0x00009888
		protected void InitializeFontReferenceLookup()
		{
			bool flag = this.m_FontReferences == null;
			if (flag)
			{
				this.m_FontReferences = new List<TextSettings.FontReferenceMap>();
			}
			for (int i = 0; i < this.m_FontReferences.Count; i++)
			{
				TextSettings.FontReferenceMap fontReferenceMap = this.m_FontReferences[i];
				bool flag2 = fontReferenceMap.font == null || fontReferenceMap.fontAsset == null;
				if (flag2)
				{
					Debug.LogWarning("Deleting invalid font reference.");
					this.m_FontReferences.RemoveAt(i);
					i--;
				}
				else
				{
					int hashCode = fontReferenceMap.font.GetHashCode();
					bool flag3 = !this.m_FontLookup.ContainsKey(hashCode);
					if (flag3)
					{
						this.m_FontLookup.Add(hashCode, fontReferenceMap.fontAsset);
					}
				}
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000B758 File Offset: 0x00009958
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal FontAsset GetCachedFontAsset(Font font)
		{
			bool flag = font == null;
			FontAsset fontAsset;
			if (flag)
			{
				fontAsset = null;
			}
			else
			{
				bool flag2 = this.m_FontLookup == null;
				if (flag2)
				{
					this.m_FontLookup = new Dictionary<int, FontAsset>();
					this.InitializeFontReferenceLookup();
				}
				int hashCode = font.GetHashCode();
				bool flag3 = this.m_FontLookup.ContainsKey(hashCode);
				if (flag3)
				{
					fontAsset = this.m_FontLookup[hashCode];
				}
				else
				{
					bool isExecutingJob = TextGenerator.IsExecutingJob;
					if (isExecutingJob)
					{
						fontAsset = null;
					}
					else
					{
						FontAsset fontAsset2 = FontAssetFactory.ConvertFontToFontAsset(font);
						bool flag4 = fontAsset2 != null;
						if (flag4)
						{
							this.m_FontReferences.Add(new TextSettings.FontReferenceMap(font, fontAsset2));
							this.m_FontLookup.Add(hashCode, fontAsset2);
						}
						fontAsset = fontAsset2;
					}
				}
			}
			return fontAsset;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000B810 File Offset: 0x00009A10
		private List<FontAsset> GetOSFontAssetList()
		{
			string[] osfallbacks = Font.GetOSFallbacks();
			return FontAsset.CreateFontAssetOSFallbackList(osfallbacks, 90);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000B830 File Offset: 0x00009A30
		[NativeMethod(Name = "TextSettings::Create")]
		private unsafe static IntPtr CreateNativeObject(IntPtr[] fallbacks)
		{
			Span<IntPtr> span = new Span<IntPtr>(fallbacks);
			IntPtr intPtr;
			fixed (IntPtr* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				intPtr = TextSettings.CreateNativeObject_Injected(ref managedSpanWrapper);
			}
			return intPtr;
		}

		// Token: 0x060001C0 RID: 448
		[NativeMethod(Name = "TextSettings::Destroy")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyNativeObject(IntPtr m_NativeTextSettings);

		// Token: 0x060001C1 RID: 449 RVA: 0x0000B868 File Offset: 0x00009A68
		private unsafe static void UpdateFallbacks(IntPtr ptr, IntPtr[] fallbacks)
		{
			Span<IntPtr> span = new Span<IntPtr>(fallbacks);
			fixed (IntPtr* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				TextSettings.UpdateFallbacks_Injected(ptr, ref managedSpanWrapper);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000B8A4 File Offset: 0x00009AA4
		internal IntPtr nativeTextSettings
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			get
			{
				this.UpdateNativeTextSettings();
				return this.m_NativeTextSettings;
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000B8C4 File Offset: 0x00009AC4
		private IntPtr[] GetGlobalFallbacks()
		{
			List<IntPtr> globalFontAssetFallbacks = new List<IntPtr>();
			List<FontAsset> fallbackFontAssets = this.fallbackFontAssets;
			if (fallbackFontAssets != null)
			{
				fallbackFontAssets.ForEach(delegate(FontAsset fallback)
				{
					bool flag = fallback == null;
					if (!flag)
					{
						bool flag2 = fallback.atlasPopulationMode == AtlasPopulationMode.Static && fallback.characterTable.Count > 0;
						if (flag2)
						{
							Debug.LogWarning("Advanced text system cannot use static font asset " + fallback.name + " as fallback.");
						}
						else
						{
							globalFontAssetFallbacks.Add(fallback.nativeFontAsset);
						}
					}
				});
			}
			List<FontAsset> fallbackOSFontAssets = this.fallbackOSFontAssets;
			if (fallbackOSFontAssets != null)
			{
				fallbackOSFontAssets.ForEach(delegate(FontAsset fallback)
				{
					bool flag3 = fallback == null;
					if (!flag3)
					{
						bool flag4 = fallback.atlasPopulationMode == AtlasPopulationMode.Static && fallback.characterTable.Count > 0;
						if (flag4)
						{
							Debug.LogWarning("Advanced text system cannot use static font asset " + fallback.name + " as fallback.");
						}
						else
						{
							globalFontAssetFallbacks.Add(fallback.nativeFontAsset);
						}
					}
				});
			}
			List<TextAsset> emojiFallbackTextAssets = this.emojiFallbackTextAssets;
			if (emojiFallbackTextAssets != null)
			{
				emojiFallbackTextAssets.ForEach(delegate(TextAsset fallback)
				{
					FontAsset fontAsset = fallback as FontAsset;
					bool flag5 = fontAsset != null;
					if (flag5)
					{
						bool flag6 = fontAsset == null;
						if (!flag6)
						{
							bool flag7 = fontAsset.atlasPopulationMode == AtlasPopulationMode.Static && fontAsset.characterTable.Count > 0;
							if (flag7)
							{
								Debug.LogWarning("Advanced text system cannot use static font asset " + fallback.name + " as fallback.");
							}
							else
							{
								globalFontAssetFallbacks.Add(fontAsset.nativeFontAsset);
							}
						}
					}
				});
			}
			return globalFontAssetFallbacks.ToArray();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000B94C File Offset: 0x00009B4C
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal void UpdateNativeTextSettings()
		{
			bool flag = this.m_NativeTextSettings == IntPtr.Zero;
			if (flag)
			{
				this.m_NativeTextSettings = TextSettings.CreateNativeObject(this.GetGlobalFallbacks());
				this.m_IsNativeTextSettingsDirty = false;
			}
			else
			{
				bool flag2 = this.m_IsNativeTextSettingsDirty && this.m_NativeTextSettings != IntPtr.Zero;
				if (flag2)
				{
					TextSettings.UpdateFallbacks(this.m_NativeTextSettings, this.GetGlobalFallbacks());
					this.m_IsNativeTextSettingsDirty = false;
				}
			}
		}

		// Token: 0x060001C6 RID: 454
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateNativeObject_Injected(ref ManagedSpanWrapper fallbacks);

		// Token: 0x060001C7 RID: 455
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateFallbacks_Injected(IntPtr ptr, ref ManagedSpanWrapper fallbacks);

		// Token: 0x04000161 RID: 353
		[SerializeField]
		protected string m_Version;

		// Token: 0x04000162 RID: 354
		[FormerlySerializedAs("m_defaultFontAsset")]
		[SerializeField]
		protected FontAsset m_DefaultFontAsset;

		// Token: 0x04000163 RID: 355
		[FormerlySerializedAs("m_defaultFontAssetPath")]
		[SerializeField]
		protected string m_DefaultFontAssetPath = "Fonts & Materials/";

		// Token: 0x04000164 RID: 356
		[FormerlySerializedAs("m_fallbackFontAssets")]
		[SerializeField]
		protected List<FontAsset> m_FallbackFontAssets;

		// Token: 0x04000165 RID: 357
		private static List<FontAsset> s_FallbackOSFontAssetInternal;

		// Token: 0x04000166 RID: 358
		[SerializeField]
		[FormerlySerializedAs("m_matchMaterialPreset")]
		protected bool m_MatchMaterialPreset;

		// Token: 0x04000167 RID: 359
		[SerializeField]
		[FormerlySerializedAs("m_missingGlyphCharacter")]
		protected int m_MissingCharacterUnicode;

		// Token: 0x04000168 RID: 360
		[SerializeField]
		protected bool m_ClearDynamicDataOnBuild = true;

		// Token: 0x04000169 RID: 361
		[SerializeField]
		private bool m_EnableEmojiSupport;

		// Token: 0x0400016A RID: 362
		[SerializeField]
		private List<TextAsset> m_EmojiFallbackTextAssets;

		// Token: 0x0400016B RID: 363
		[SerializeField]
		[FormerlySerializedAs("m_defaultSpriteAsset")]
		protected SpriteAsset m_DefaultSpriteAsset;

		// Token: 0x0400016C RID: 364
		[FormerlySerializedAs("m_defaultSpriteAssetPath")]
		[SerializeField]
		protected string m_DefaultSpriteAssetPath = "Sprite Assets/";

		// Token: 0x0400016D RID: 365
		[SerializeField]
		protected List<SpriteAsset> m_FallbackSpriteAssets;

		// Token: 0x0400016F RID: 367
		[SerializeField]
		protected uint m_MissingSpriteCharacterUnicode;

		// Token: 0x04000170 RID: 368
		[FormerlySerializedAs("m_defaultStyleSheet")]
		[SerializeField]
		protected TextStyleSheet m_DefaultStyleSheet;

		// Token: 0x04000171 RID: 369
		[SerializeField]
		protected string m_StyleSheetsResourcePath = "Text Style Sheets/";

		// Token: 0x04000172 RID: 370
		[FormerlySerializedAs("m_defaultColorGradientPresetsPath")]
		[SerializeField]
		protected string m_DefaultColorGradientPresetsPath = "Text Color Gradients/";

		// Token: 0x04000173 RID: 371
		[SerializeField]
		protected UnicodeLineBreakingRules m_UnicodeLineBreakingRules;

		// Token: 0x04000174 RID: 372
		[SerializeField]
		[FormerlySerializedAs("m_warningsDisabled")]
		protected bool m_DisplayWarnings = false;

		// Token: 0x04000175 RID: 373
		internal Dictionary<int, FontAsset> m_FontLookup;

		// Token: 0x04000176 RID: 374
		internal List<TextSettings.FontReferenceMap> m_FontReferences = new List<TextSettings.FontReferenceMap>();

		// Token: 0x04000177 RID: 375
		private IntPtr m_NativeTextSettings = IntPtr.Zero;

		// Token: 0x04000178 RID: 376
		private bool m_IsNativeTextSettingsDirty = true;

		// Token: 0x0200003B RID: 59
		[Serializable]
		internal struct FontReferenceMap
		{
			// Token: 0x060001C8 RID: 456 RVA: 0x0000BA2F File Offset: 0x00009C2F
			public FontReferenceMap(Font font, FontAsset fontAsset)
			{
				this.font = font;
				this.fontAsset = fontAsset;
			}

			// Token: 0x04000179 RID: 377
			public Font font;

			// Token: 0x0400017A RID: 378
			public FontAsset fontAsset;
		}
	}
}
