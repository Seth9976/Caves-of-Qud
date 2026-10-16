using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x02000046 RID: 70
	[ExcludeFromPreset]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ugui@2.0/manual/TextMeshPro/Settings.html")]
	[Serializable]
	public class TMP_Settings : ScriptableObject
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00011FC7 File Offset: 0x000101C7
		public static string version
		{
			get
			{
				return "1.4.0";
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00011FCE File Offset: 0x000101CE
		internal void SetAssetVersion()
		{
			this.assetVersion = TMP_Settings.s_CurrentAssetVersion;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00011FDB File Offset: 0x000101DB
		public static TextWrappingModes textWrappingMode
		{
			get
			{
				return TMP_Settings.instance.m_TextWrappingMode;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00011FE7 File Offset: 0x000101E7
		[Obsolete("The \"enableKerning\" property has been deprecated. Use the \"fontFeatures\" property to control what features are enabled by default on newly created text components.")]
		public static bool enableKerning
		{
			get
			{
				if (TMP_Settings.instance.m_ActiveFontFeatures != null)
				{
					return TMP_Settings.instance.m_ActiveFontFeatures.Contains(OTL_FeatureTag.kern);
				}
				return TMP_Settings.instance.m_enableKerning;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00012014 File Offset: 0x00010214
		public static List<OTL_FeatureTag> fontFeatures
		{
			get
			{
				return TMP_Settings.instance.m_ActiveFontFeatures;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00012020 File Offset: 0x00010220
		public static bool enableExtraPadding
		{
			get
			{
				return TMP_Settings.instance.m_enableExtraPadding;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0001202C File Offset: 0x0001022C
		public static bool enableTintAllSprites
		{
			get
			{
				return TMP_Settings.instance.m_enableTintAllSprites;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00012038 File Offset: 0x00010238
		public static bool enableParseEscapeCharacters
		{
			get
			{
				return TMP_Settings.instance.m_enableParseEscapeCharacters;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00012044 File Offset: 0x00010244
		public static bool enableRaycastTarget
		{
			get
			{
				return TMP_Settings.instance.m_EnableRaycastTarget;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00012050 File Offset: 0x00010250
		public static bool getFontFeaturesAtRuntime
		{
			get
			{
				return TMP_Settings.instance.m_GetFontFeaturesAtRuntime;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0001205C File Offset: 0x0001025C
		// (set) Token: 0x060002FD RID: 765 RVA: 0x00012068 File Offset: 0x00010268
		public static int missingGlyphCharacter
		{
			get
			{
				return TMP_Settings.instance.m_missingGlyphCharacter;
			}
			set
			{
				TMP_Settings.instance.m_missingGlyphCharacter = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00012075 File Offset: 0x00010275
		public static bool clearDynamicDataOnBuild
		{
			get
			{
				return TMP_Settings.instance.m_ClearDynamicDataOnBuild;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00012081 File Offset: 0x00010281
		public static bool warningsDisabled
		{
			get
			{
				return TMP_Settings.instance.m_warningsDisabled;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0001208D File Offset: 0x0001028D
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00012099 File Offset: 0x00010299
		public static TMP_FontAsset defaultFontAsset
		{
			get
			{
				return TMP_Settings.instance.m_defaultFontAsset;
			}
			set
			{
				TMP_Settings.instance.m_defaultFontAsset = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000302 RID: 770 RVA: 0x000120A6 File Offset: 0x000102A6
		public static string defaultFontAssetPath
		{
			get
			{
				return TMP_Settings.instance.m_defaultFontAssetPath;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000303 RID: 771 RVA: 0x000120B2 File Offset: 0x000102B2
		public static float defaultFontSize
		{
			get
			{
				return TMP_Settings.instance.m_defaultFontSize;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000304 RID: 772 RVA: 0x000120BE File Offset: 0x000102BE
		public static float defaultTextAutoSizingMinRatio
		{
			get
			{
				return TMP_Settings.instance.m_defaultAutoSizeMinRatio;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000305 RID: 773 RVA: 0x000120CA File Offset: 0x000102CA
		public static float defaultTextAutoSizingMaxRatio
		{
			get
			{
				return TMP_Settings.instance.m_defaultAutoSizeMaxRatio;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000306 RID: 774 RVA: 0x000120D6 File Offset: 0x000102D6
		public static Vector2 defaultTextMeshProTextContainerSize
		{
			get
			{
				return TMP_Settings.instance.m_defaultTextMeshProTextContainerSize;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000307 RID: 775 RVA: 0x000120E2 File Offset: 0x000102E2
		public static Vector2 defaultTextMeshProUITextContainerSize
		{
			get
			{
				return TMP_Settings.instance.m_defaultTextMeshProUITextContainerSize;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000308 RID: 776 RVA: 0x000120EE File Offset: 0x000102EE
		public static bool autoSizeTextContainer
		{
			get
			{
				return TMP_Settings.instance.m_autoSizeTextContainer;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000309 RID: 777 RVA: 0x000120FA File Offset: 0x000102FA
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00012106 File Offset: 0x00010306
		public static bool isTextObjectScaleStatic
		{
			get
			{
				return TMP_Settings.instance.m_IsTextObjectScaleStatic;
			}
			set
			{
				TMP_Settings.instance.m_IsTextObjectScaleStatic = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00012113 File Offset: 0x00010313
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0001211F File Offset: 0x0001031F
		public static List<TMP_FontAsset> fallbackFontAssets
		{
			get
			{
				return TMP_Settings.instance.m_fallbackFontAssets;
			}
			set
			{
				TMP_Settings.instance.m_fallbackFontAssets = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0001212C File Offset: 0x0001032C
		public static bool matchMaterialPreset
		{
			get
			{
				return TMP_Settings.instance.m_matchMaterialPreset;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00012138 File Offset: 0x00010338
		public static bool hideSubTextObjects
		{
			get
			{
				return TMP_Settings.instance.m_HideSubTextObjects;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00012144 File Offset: 0x00010344
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00012150 File Offset: 0x00010350
		public static TMP_SpriteAsset defaultSpriteAsset
		{
			get
			{
				return TMP_Settings.instance.m_defaultSpriteAsset;
			}
			set
			{
				TMP_Settings.instance.m_defaultSpriteAsset = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0001215D File Offset: 0x0001035D
		public static string defaultSpriteAssetPath
		{
			get
			{
				return TMP_Settings.instance.m_defaultSpriteAssetPath;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00012169 File Offset: 0x00010369
		// (set) Token: 0x06000313 RID: 787 RVA: 0x00012175 File Offset: 0x00010375
		public static bool enableEmojiSupport
		{
			get
			{
				return TMP_Settings.instance.m_enableEmojiSupport;
			}
			set
			{
				TMP_Settings.instance.m_enableEmojiSupport = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00012182 File Offset: 0x00010382
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0001218E File Offset: 0x0001038E
		public static uint missingCharacterSpriteUnicode
		{
			get
			{
				return TMP_Settings.instance.m_MissingCharacterSpriteUnicode;
			}
			set
			{
				TMP_Settings.instance.m_MissingCharacterSpriteUnicode = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0001219B File Offset: 0x0001039B
		// (set) Token: 0x06000317 RID: 791 RVA: 0x000121A7 File Offset: 0x000103A7
		public static List<TMP_Asset> emojiFallbackTextAssets
		{
			get
			{
				return TMP_Settings.instance.m_EmojiFallbackTextAssets;
			}
			set
			{
				TMP_Settings.instance.m_EmojiFallbackTextAssets = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000318 RID: 792 RVA: 0x000121B4 File Offset: 0x000103B4
		public static string defaultColorGradientPresetsPath
		{
			get
			{
				return TMP_Settings.instance.m_defaultColorGradientPresetsPath;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000319 RID: 793 RVA: 0x000121C0 File Offset: 0x000103C0
		// (set) Token: 0x0600031A RID: 794 RVA: 0x000121CC File Offset: 0x000103CC
		public static TMP_StyleSheet defaultStyleSheet
		{
			get
			{
				return TMP_Settings.instance.m_defaultStyleSheet;
			}
			set
			{
				TMP_Settings.instance.m_defaultStyleSheet = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600031B RID: 795 RVA: 0x000121D9 File Offset: 0x000103D9
		public static string styleSheetsResourcePath
		{
			get
			{
				return TMP_Settings.instance.m_StyleSheetsResourcePath;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600031C RID: 796 RVA: 0x000121E5 File Offset: 0x000103E5
		public static TextAsset leadingCharacters
		{
			get
			{
				return TMP_Settings.instance.m_leadingCharacters;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600031D RID: 797 RVA: 0x000121F1 File Offset: 0x000103F1
		public static TextAsset followingCharacters
		{
			get
			{
				return TMP_Settings.instance.m_followingCharacters;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600031E RID: 798 RVA: 0x000121FD File Offset: 0x000103FD
		public static TMP_Settings.LineBreakingTable linebreakingRules
		{
			get
			{
				if (TMP_Settings.instance.m_linebreakingRules == null)
				{
					TMP_Settings.LoadLinebreakingRules();
				}
				return TMP_Settings.instance.m_linebreakingRules;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0001221A File Offset: 0x0001041A
		// (set) Token: 0x06000320 RID: 800 RVA: 0x00012226 File Offset: 0x00010426
		public static bool useModernHangulLineBreakingRules
		{
			get
			{
				return TMP_Settings.instance.m_UseModernHangulLineBreakingRules;
			}
			set
			{
				TMP_Settings.instance.m_UseModernHangulLineBreakingRules = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00012234 File Offset: 0x00010434
		public static TMP_Settings instance
		{
			get
			{
				if (TMP_Settings.isTMPSettingsNull)
				{
					TMP_Settings.s_Instance = Resources.Load<TMP_Settings>("TMP Settings");
					if (!TMP_Settings.isTMPSettingsNull && TMP_Settings.s_Instance.m_ActiveFontFeatures.Count == 1 && TMP_Settings.s_Instance.m_ActiveFontFeatures[0] == (OTL_FeatureTag)0U)
					{
						TMP_Settings.s_Instance.m_ActiveFontFeatures.Clear();
						if (TMP_Settings.s_Instance.m_enableKerning)
						{
							TMP_Settings.s_Instance.m_ActiveFontFeatures.Add(OTL_FeatureTag.kern);
						}
					}
				}
				return TMP_Settings.s_Instance;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000322 RID: 802 RVA: 0x000122B6 File Offset: 0x000104B6
		internal static bool isTMPSettingsNull
		{
			get
			{
				return TMP_Settings.s_Instance == null;
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000122C4 File Offset: 0x000104C4
		public static TMP_Settings LoadDefaultSettings()
		{
			if (TMP_Settings.s_Instance == null)
			{
				TMP_Settings tmp_Settings = Resources.Load<TMP_Settings>("TMP Settings");
				if (tmp_Settings != null)
				{
					TMP_Settings.s_Instance = tmp_Settings;
				}
			}
			return TMP_Settings.s_Instance;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000122FD File Offset: 0x000104FD
		public static TMP_Settings GetSettings()
		{
			if (TMP_Settings.instance == null)
			{
				return null;
			}
			return TMP_Settings.instance;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00012313 File Offset: 0x00010513
		public static TMP_FontAsset GetFontAsset()
		{
			if (TMP_Settings.instance == null)
			{
				return null;
			}
			return TMP_Settings.instance.m_defaultFontAsset;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0001232E File Offset: 0x0001052E
		public static TMP_SpriteAsset GetSpriteAsset()
		{
			if (TMP_Settings.instance == null)
			{
				return null;
			}
			return TMP_Settings.instance.m_defaultSpriteAsset;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00012349 File Offset: 0x00010549
		public static TMP_StyleSheet GetStyleSheet()
		{
			if (TMP_Settings.instance == null)
			{
				return null;
			}
			return TMP_Settings.instance.m_defaultStyleSheet;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00012364 File Offset: 0x00010564
		public static void LoadLinebreakingRules()
		{
			if (TMP_Settings.instance == null)
			{
				return;
			}
			if (TMP_Settings.s_Instance.m_linebreakingRules == null)
			{
				TMP_Settings.s_Instance.m_linebreakingRules = new TMP_Settings.LineBreakingTable();
			}
			TMP_Settings.s_Instance.m_linebreakingRules.leadingCharacters = TMP_Settings.GetCharacters(TMP_Settings.s_Instance.m_leadingCharacters);
			TMP_Settings.s_Instance.m_linebreakingRules.followingCharacters = TMP_Settings.GetCharacters(TMP_Settings.s_Instance.m_followingCharacters);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000123D8 File Offset: 0x000105D8
		private static HashSet<uint> GetCharacters(TextAsset file)
		{
			HashSet<uint> hashSet = new HashSet<uint>();
			string text = file.text;
			for (int i = 0; i < text.Length; i++)
			{
				hashSet.Add((uint)text[i]);
			}
			return hashSet;
		}

		// Token: 0x0400028D RID: 653
		private static TMP_Settings s_Instance;

		// Token: 0x0400028E RID: 654
		[SerializeField]
		internal string assetVersion;

		// Token: 0x0400028F RID: 655
		internal static string s_CurrentAssetVersion = "2";

		// Token: 0x04000290 RID: 656
		[FormerlySerializedAs("m_enableWordWrapping")]
		[SerializeField]
		private TextWrappingModes m_TextWrappingMode;

		// Token: 0x04000291 RID: 657
		[SerializeField]
		private bool m_enableKerning;

		// Token: 0x04000292 RID: 658
		[SerializeField]
		private List<OTL_FeatureTag> m_ActiveFontFeatures = new List<OTL_FeatureTag> { (OTL_FeatureTag)0U };

		// Token: 0x04000293 RID: 659
		[SerializeField]
		private bool m_enableExtraPadding;

		// Token: 0x04000294 RID: 660
		[SerializeField]
		private bool m_enableTintAllSprites;

		// Token: 0x04000295 RID: 661
		[SerializeField]
		private bool m_enableParseEscapeCharacters;

		// Token: 0x04000296 RID: 662
		[SerializeField]
		private bool m_EnableRaycastTarget = true;

		// Token: 0x04000297 RID: 663
		[SerializeField]
		private bool m_GetFontFeaturesAtRuntime = true;

		// Token: 0x04000298 RID: 664
		[SerializeField]
		private int m_missingGlyphCharacter;

		// Token: 0x04000299 RID: 665
		[SerializeField]
		private bool m_ClearDynamicDataOnBuild = true;

		// Token: 0x0400029A RID: 666
		[SerializeField]
		private bool m_warningsDisabled;

		// Token: 0x0400029B RID: 667
		[SerializeField]
		private TMP_FontAsset m_defaultFontAsset;

		// Token: 0x0400029C RID: 668
		[SerializeField]
		private string m_defaultFontAssetPath;

		// Token: 0x0400029D RID: 669
		[SerializeField]
		private float m_defaultFontSize;

		// Token: 0x0400029E RID: 670
		[SerializeField]
		private float m_defaultAutoSizeMinRatio;

		// Token: 0x0400029F RID: 671
		[SerializeField]
		private float m_defaultAutoSizeMaxRatio;

		// Token: 0x040002A0 RID: 672
		[SerializeField]
		private Vector2 m_defaultTextMeshProTextContainerSize;

		// Token: 0x040002A1 RID: 673
		[SerializeField]
		private Vector2 m_defaultTextMeshProUITextContainerSize;

		// Token: 0x040002A2 RID: 674
		[SerializeField]
		private bool m_autoSizeTextContainer;

		// Token: 0x040002A3 RID: 675
		[SerializeField]
		private bool m_IsTextObjectScaleStatic;

		// Token: 0x040002A4 RID: 676
		[SerializeField]
		private List<TMP_FontAsset> m_fallbackFontAssets;

		// Token: 0x040002A5 RID: 677
		[SerializeField]
		private bool m_matchMaterialPreset;

		// Token: 0x040002A6 RID: 678
		[SerializeField]
		private bool m_HideSubTextObjects = true;

		// Token: 0x040002A7 RID: 679
		[SerializeField]
		private TMP_SpriteAsset m_defaultSpriteAsset;

		// Token: 0x040002A8 RID: 680
		[SerializeField]
		private string m_defaultSpriteAssetPath;

		// Token: 0x040002A9 RID: 681
		[SerializeField]
		private bool m_enableEmojiSupport;

		// Token: 0x040002AA RID: 682
		[SerializeField]
		private uint m_MissingCharacterSpriteUnicode;

		// Token: 0x040002AB RID: 683
		[SerializeField]
		private List<TMP_Asset> m_EmojiFallbackTextAssets;

		// Token: 0x040002AC RID: 684
		[SerializeField]
		private string m_defaultColorGradientPresetsPath;

		// Token: 0x040002AD RID: 685
		[SerializeField]
		private TMP_StyleSheet m_defaultStyleSheet;

		// Token: 0x040002AE RID: 686
		[SerializeField]
		private string m_StyleSheetsResourcePath;

		// Token: 0x040002AF RID: 687
		[SerializeField]
		private TextAsset m_leadingCharacters;

		// Token: 0x040002B0 RID: 688
		[SerializeField]
		private TextAsset m_followingCharacters;

		// Token: 0x040002B1 RID: 689
		[SerializeField]
		private TMP_Settings.LineBreakingTable m_linebreakingRules;

		// Token: 0x040002B2 RID: 690
		[SerializeField]
		private bool m_UseModernHangulLineBreakingRules;

		// Token: 0x020000B1 RID: 177
		public class LineBreakingTable
		{
			// Token: 0x04000672 RID: 1650
			public HashSet<uint> leadingCharacters;

			// Token: 0x04000673 RID: 1651
			public HashSet<uint> followingCharacters;
		}
	}
}
