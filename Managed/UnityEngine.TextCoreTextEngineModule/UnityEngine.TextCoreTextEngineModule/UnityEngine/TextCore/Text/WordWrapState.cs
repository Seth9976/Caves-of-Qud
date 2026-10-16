using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000059 RID: 89
	internal struct WordWrapState
	{
		// Token: 0x04000311 RID: 785
		public int previousWordBreak;

		// Token: 0x04000312 RID: 786
		public int totalCharacterCount;

		// Token: 0x04000313 RID: 787
		public int visibleCharacterCount;

		// Token: 0x04000314 RID: 788
		public int visibleSpaceCount;

		// Token: 0x04000315 RID: 789
		public int visibleSpriteCount;

		// Token: 0x04000316 RID: 790
		public int visibleLinkCount;

		// Token: 0x04000317 RID: 791
		public int firstCharacterIndex;

		// Token: 0x04000318 RID: 792
		public int firstVisibleCharacterIndex;

		// Token: 0x04000319 RID: 793
		public int lastCharacterIndex;

		// Token: 0x0400031A RID: 794
		public int lastVisibleCharIndex;

		// Token: 0x0400031B RID: 795
		public int lineNumber;

		// Token: 0x0400031C RID: 796
		public float maxCapHeight;

		// Token: 0x0400031D RID: 797
		public float maxAscender;

		// Token: 0x0400031E RID: 798
		public float maxDescender;

		// Token: 0x0400031F RID: 799
		public float maxLineAscender;

		// Token: 0x04000320 RID: 800
		public float maxLineDescender;

		// Token: 0x04000321 RID: 801
		public float startOfLineAscender;

		// Token: 0x04000322 RID: 802
		public float xAdvance;

		// Token: 0x04000323 RID: 803
		public float preferredWidth;

		// Token: 0x04000324 RID: 804
		public float preferredHeight;

		// Token: 0x04000325 RID: 805
		public float previousLineScale;

		// Token: 0x04000326 RID: 806
		public float pageAscender;

		// Token: 0x04000327 RID: 807
		public int wordCount;

		// Token: 0x04000328 RID: 808
		public FontStyles fontStyle;

		// Token: 0x04000329 RID: 809
		public float fontScale;

		// Token: 0x0400032A RID: 810
		public float fontScaleMultiplier;

		// Token: 0x0400032B RID: 811
		public int italicAngle;

		// Token: 0x0400032C RID: 812
		public float currentFontSize;

		// Token: 0x0400032D RID: 813
		public float baselineOffset;

		// Token: 0x0400032E RID: 814
		public float lineOffset;

		// Token: 0x0400032F RID: 815
		public TextInfo textInfo;

		// Token: 0x04000330 RID: 816
		public LineInfo lineInfo;

		// Token: 0x04000331 RID: 817
		public Color32 vertexColor;

		// Token: 0x04000332 RID: 818
		public Color32 underlineColor;

		// Token: 0x04000333 RID: 819
		public Color32 strikethroughColor;

		// Token: 0x04000334 RID: 820
		public Color32 highlightColor;

		// Token: 0x04000335 RID: 821
		public HighlightState highlightState;

		// Token: 0x04000336 RID: 822
		public FontStyleStack basicStyleStack;

		// Token: 0x04000337 RID: 823
		public TextProcessingStack<int> italicAngleStack;

		// Token: 0x04000338 RID: 824
		public TextProcessingStack<Color32> colorStack;

		// Token: 0x04000339 RID: 825
		public TextProcessingStack<Color32> underlineColorStack;

		// Token: 0x0400033A RID: 826
		public TextProcessingStack<Color32> strikethroughColorStack;

		// Token: 0x0400033B RID: 827
		public TextProcessingStack<Color32> highlightColorStack;

		// Token: 0x0400033C RID: 828
		public TextProcessingStack<HighlightState> highlightStateStack;

		// Token: 0x0400033D RID: 829
		public TextProcessingStack<TextColorGradient> colorGradientStack;

		// Token: 0x0400033E RID: 830
		public TextProcessingStack<float> sizeStack;

		// Token: 0x0400033F RID: 831
		public TextProcessingStack<float> indentStack;

		// Token: 0x04000340 RID: 832
		public TextProcessingStack<TextFontWeight> fontWeightStack;

		// Token: 0x04000341 RID: 833
		public TextProcessingStack<int> styleStack;

		// Token: 0x04000342 RID: 834
		public TextProcessingStack<float> baselineStack;

		// Token: 0x04000343 RID: 835
		public TextProcessingStack<int> actionStack;

		// Token: 0x04000344 RID: 836
		public TextProcessingStack<MaterialReference> materialReferenceStack;

		// Token: 0x04000345 RID: 837
		public TextProcessingStack<TextAlignment> lineJustificationStack;

		// Token: 0x04000346 RID: 838
		public int lastBaseGlyphIndex;

		// Token: 0x04000347 RID: 839
		public int spriteAnimationId;

		// Token: 0x04000348 RID: 840
		public FontAsset currentFontAsset;

		// Token: 0x04000349 RID: 841
		public SpriteAsset currentSpriteAsset;

		// Token: 0x0400034A RID: 842
		public Material currentMaterial;

		// Token: 0x0400034B RID: 843
		public int currentMaterialIndex;

		// Token: 0x0400034C RID: 844
		public Extents meshExtents;

		// Token: 0x0400034D RID: 845
		public bool tagNoParsing;

		// Token: 0x0400034E RID: 846
		public bool isNonBreakingSpace;

		// Token: 0x0400034F RID: 847
		public bool isDrivenLineSpacing;

		// Token: 0x04000350 RID: 848
		public Vector3 fxScale;

		// Token: 0x04000351 RID: 849
		public Quaternion fxRotation;
	}
}
