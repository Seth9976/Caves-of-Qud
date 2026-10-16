using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000048 RID: 72
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal class TextGenerationSettings : IEquatable<TextGenerationSettings>
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0001EDA7 File Offset: 0x0001CFA7
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0001EDAF File Offset: 0x0001CFAF
		public RenderedText renderedText
		{
			get
			{
				return this.m_RenderedText;
			}
			set
			{
				this.m_RenderedText = value;
				this.m_CachedRenderedText = null;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0001EDC0 File Offset: 0x0001CFC0
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0001EDEE File Offset: 0x0001CFEE
		public string text
		{
			get
			{
				string text;
				if ((text = this.m_CachedRenderedText) == null)
				{
					text = (this.m_CachedRenderedText = this.renderedText.CreateString());
				}
				return text;
			}
			set
			{
				this.renderedText = new RenderedText(value);
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0001EE00 File Offset: 0x0001D000
		public TextGenerationSettings()
		{
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0001EE90 File Offset: 0x0001D090
		internal TextGenerationSettings(TextGenerationSettings tgs)
		{
			this.m_RenderedText = tgs.m_RenderedText;
			this.m_CachedRenderedText = tgs.m_CachedRenderedText;
			this.screenRect = tgs.screenRect;
			this.pixelsPerPoint = tgs.pixelsPerPoint;
			this.fontAsset = tgs.fontAsset;
			this.fontStyle = tgs.fontStyle;
			this.textSettings = tgs.textSettings;
			this.textAlignment = tgs.textAlignment;
			this.overflowMode = tgs.overflowMode;
			this.shouldConvertToLinearSpace = tgs.shouldConvertToLinearSpace;
			this.fontSize = tgs.fontSize;
			this.emojiFallbackSupport = tgs.emojiFallbackSupport;
			this.richText = tgs.richText;
			this.isRightToLeft = tgs.isRightToLeft;
			this.extraPadding = tgs.extraPadding;
			this.parseControlCharacters = tgs.parseControlCharacters;
			this.isPlaceholder = tgs.isPlaceholder;
			this.characterSpacing = tgs.characterSpacing;
			this.wordSpacing = tgs.wordSpacing;
			this.paragraphSpacing = tgs.paragraphSpacing;
			this.textWrappingMode = tgs.textWrappingMode;
			this.fontWeight = tgs.fontWeight;
			this.isIMGUI = tgs.isIMGUI;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0001F034 File Offset: 0x0001D234
		public bool Equals(TextGenerationSettings other)
		{
			bool flag = other == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == other;
				flag2 = flag3 || (this.m_RenderedText.Equals(other.m_RenderedText) && this.screenRect.Equals(other.screenRect) && object.Equals(this.fontAsset, other.fontAsset) && this.fontStyle == other.fontStyle && object.Equals(this.textSettings, other.textSettings) && this.textAlignment == other.textAlignment && this.overflowMode == other.overflowMode && this.color.Equals(other.color) && this.fontSize.Equals(other.fontSize) && this.shouldConvertToLinearSpace == other.shouldConvertToLinearSpace && this.emojiFallbackSupport == other.emojiFallbackSupport && this.richText == other.richText && this.isRightToLeft == other.isRightToLeft && this.extraPadding == other.extraPadding && this.parseControlCharacters == other.parseControlCharacters && this.isPlaceholder == other.isPlaceholder && this.characterSpacing.Equals(other.characterSpacing) && this.wordSpacing.Equals(other.wordSpacing) && this.paragraphSpacing.Equals(other.paragraphSpacing) && this.textWrappingMode == other.textWrappingMode && this.fontWeight == other.fontWeight && this.isIMGUI == other.isIMGUI);
			}
			return flag2;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0001F1F4 File Offset: 0x0001D3F4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == obj;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = obj.GetType() != base.GetType();
					flag2 = !flag4 && this.Equals((TextGenerationSettings)obj);
				}
			}
			return flag2;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0001F244 File Offset: 0x0001D444
		public override int GetHashCode()
		{
			HashCode hashCode = default(HashCode);
			hashCode.Add<RenderedText>(this.m_RenderedText);
			hashCode.Add<Rect>(this.screenRect);
			hashCode.Add<FontAsset>(this.fontAsset);
			hashCode.Add<int>((int)this.fontStyle);
			hashCode.Add<TextSettings>(this.textSettings);
			hashCode.Add<int>((int)this.textAlignment);
			hashCode.Add<int>((int)this.overflowMode);
			hashCode.Add<Color>(this.color);
			hashCode.Add<bool>(this.shouldConvertToLinearSpace);
			hashCode.Add<float>(this.fontSize);
			hashCode.Add<bool>(this.emojiFallbackSupport);
			hashCode.Add<bool>(this.richText);
			hashCode.Add<bool>(this.isRightToLeft);
			hashCode.Add<float>(this.extraPadding);
			hashCode.Add<bool>(this.parseControlCharacters);
			hashCode.Add<bool>(this.isPlaceholder);
			hashCode.Add<float>(this.characterSpacing);
			hashCode.Add<float>(this.wordSpacing);
			hashCode.Add<float>(this.paragraphSpacing);
			hashCode.Add<int>((int)this.textWrappingMode);
			hashCode.Add<int>((int)this.fontWeight);
			hashCode.Add<bool>(this.isIMGUI);
			return hashCode.ToHashCode();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0001F39C File Offset: 0x0001D59C
		public static bool operator ==(TextGenerationSettings left, TextGenerationSettings right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0001F3B8 File Offset: 0x0001D5B8
		public static bool operator !=(TextGenerationSettings left, TextGenerationSettings right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0001F3D4 File Offset: 0x0001D5D4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				string.Format("{0}: {1}\n {2}: {3}\n {4}: {5}\n ", new object[] { "text", this.text, "screenRect", this.screenRect, "fontAsset", this.fontAsset }),
				string.Format("{0}: {1}\n {2}: {3}\n {4}: {5}\n {6}: {7}\n {8}: {9}\n ", new object[] { "fontStyle", this.fontStyle, "textSettings", this.textSettings, "textAlignment", this.textAlignment, "overflowMode", this.overflowMode, "textWrappingMode", this.textWrappingMode }),
				string.Format("{0}: {1}\n {2}: {3}\n {4}: {5}\n {6}: {7}\n {8}: {9}\n ", new object[] { "color", this.color, "fontSize", this.fontSize, "richText", this.richText, "isRightToLeft", this.isRightToLeft, "extraPadding", this.extraPadding }),
				string.Format("{0}: {1}\n {2}: {3}\n {4}: {5}\n {6}: {7}\n ", new object[] { "parseControlCharacters", this.parseControlCharacters, "characterSpacing", this.characterSpacing, "wordSpacing", this.wordSpacing, "paragraphSpacing", this.paragraphSpacing }),
				string.Format("{0}: {1}\n {2}: {3}\n {4}: {5}\n {6}: {7}", new object[] { "textWrappingMode", this.textWrappingMode, "fontWeight", this.fontWeight, "shouldConvertToLinearSpace", this.shouldConvertToLinearSpace, "isPlaceholder", this.isPlaceholder })
			});
		}

		// Token: 0x04000275 RID: 629
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal static Func<bool> IsEditorTextRenderingModeBitmap;

		// Token: 0x04000276 RID: 630
		private RenderedText m_RenderedText;

		// Token: 0x04000277 RID: 631
		private string m_CachedRenderedText;

		// Token: 0x04000278 RID: 632
		public Rect screenRect;

		// Token: 0x04000279 RID: 633
		public float pixelsPerPoint = 1f;

		// Token: 0x0400027A RID: 634
		public bool isEditorRenderingModeBitmap = false;

		// Token: 0x0400027B RID: 635
		public FontAsset fontAsset;

		// Token: 0x0400027C RID: 636
		public FontStyles fontStyle = FontStyles.Normal;

		// Token: 0x0400027D RID: 637
		public TextSettings textSettings;

		// Token: 0x0400027E RID: 638
		public TextAlignment textAlignment = TextAlignment.TopLeft;

		// Token: 0x0400027F RID: 639
		public TextOverflowMode overflowMode = TextOverflowMode.Overflow;

		// Token: 0x04000280 RID: 640
		public const float wordWrappingRatio = 0.4f;

		// Token: 0x04000281 RID: 641
		public Color color = Color.white;

		// Token: 0x04000282 RID: 642
		public bool shouldConvertToLinearSpace = true;

		// Token: 0x04000283 RID: 643
		public float fontSize = 18f;

		// Token: 0x04000284 RID: 644
		public const bool autoSize = false;

		// Token: 0x04000285 RID: 645
		public const float fontSizeMin = 0f;

		// Token: 0x04000286 RID: 646
		public const float fontSizeMax = 0f;

		// Token: 0x04000287 RID: 647
		internal static readonly List<OTL_FeatureTag> fontFeatures = new List<OTL_FeatureTag> { OTL_FeatureTag.kern };

		// Token: 0x04000288 RID: 648
		public bool emojiFallbackSupport = true;

		// Token: 0x04000289 RID: 649
		public bool richText;

		// Token: 0x0400028A RID: 650
		public bool isRightToLeft;

		// Token: 0x0400028B RID: 651
		public float extraPadding = 6f;

		// Token: 0x0400028C RID: 652
		public bool parseControlCharacters = true;

		// Token: 0x0400028D RID: 653
		public bool isPlaceholder = false;

		// Token: 0x0400028E RID: 654
		public const bool tagNoParsing = false;

		// Token: 0x0400028F RID: 655
		public float characterSpacing;

		// Token: 0x04000290 RID: 656
		public float wordSpacing;

		// Token: 0x04000291 RID: 657
		public const float lineSpacing = 0f;

		// Token: 0x04000292 RID: 658
		public float paragraphSpacing;

		// Token: 0x04000293 RID: 659
		public const float lineSpacingMax = 0f;

		// Token: 0x04000294 RID: 660
		public TextWrappingMode textWrappingMode = TextWrappingMode.Normal;

		// Token: 0x04000295 RID: 661
		public const int maxVisibleCharacters = 99999;

		// Token: 0x04000296 RID: 662
		public const int maxVisibleWords = 99999;

		// Token: 0x04000297 RID: 663
		public const int maxVisibleLines = 99999;

		// Token: 0x04000298 RID: 664
		public const int firstVisibleCharacter = 0;

		// Token: 0x04000299 RID: 665
		public const bool useMaxVisibleDescender = false;

		// Token: 0x0400029A RID: 666
		public TextFontWeight fontWeight = TextFontWeight.Regular;

		// Token: 0x0400029B RID: 667
		public bool isIMGUI;

		// Token: 0x0400029C RID: 668
		public const float charWidthMaxAdj = 0f;
	}
}
