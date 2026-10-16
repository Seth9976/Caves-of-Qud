using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006D3 RID: 1747
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal enum StylePropertyId
	{
		// Token: 0x0400195C RID: 6492
		Unknown,
		// Token: 0x0400195D RID: 6493
		Custom = -1,
		// Token: 0x0400195E RID: 6494
		AlignContent = 131072,
		// Token: 0x0400195F RID: 6495
		AlignItems,
		// Token: 0x04001960 RID: 6496
		AlignSelf,
		// Token: 0x04001961 RID: 6497
		All = 262144,
		// Token: 0x04001962 RID: 6498
		BackgroundColor = 458752,
		// Token: 0x04001963 RID: 6499
		BackgroundImage,
		// Token: 0x04001964 RID: 6500
		BackgroundPosition = 262145,
		// Token: 0x04001965 RID: 6501
		BackgroundPositionX = 458754,
		// Token: 0x04001966 RID: 6502
		BackgroundPositionY,
		// Token: 0x04001967 RID: 6503
		BackgroundRepeat,
		// Token: 0x04001968 RID: 6504
		BackgroundSize,
		// Token: 0x04001969 RID: 6505
		BorderBottomColor,
		// Token: 0x0400196A RID: 6506
		BorderBottomLeftRadius,
		// Token: 0x0400196B RID: 6507
		BorderBottomRightRadius,
		// Token: 0x0400196C RID: 6508
		BorderBottomWidth = 131075,
		// Token: 0x0400196D RID: 6509
		BorderColor = 262146,
		// Token: 0x0400196E RID: 6510
		BorderLeftColor = 458761,
		// Token: 0x0400196F RID: 6511
		BorderLeftWidth = 131076,
		// Token: 0x04001970 RID: 6512
		BorderRadius = 262147,
		// Token: 0x04001971 RID: 6513
		BorderRightColor = 458762,
		// Token: 0x04001972 RID: 6514
		BorderRightWidth = 131077,
		// Token: 0x04001973 RID: 6515
		BorderTopColor = 458763,
		// Token: 0x04001974 RID: 6516
		BorderTopLeftRadius,
		// Token: 0x04001975 RID: 6517
		BorderTopRightRadius,
		// Token: 0x04001976 RID: 6518
		BorderTopWidth = 131078,
		// Token: 0x04001977 RID: 6519
		BorderWidth = 262148,
		// Token: 0x04001978 RID: 6520
		Bottom = 131079,
		// Token: 0x04001979 RID: 6521
		Color = 65536,
		// Token: 0x0400197A RID: 6522
		Cursor = 196608,
		// Token: 0x0400197B RID: 6523
		Display = 131080,
		// Token: 0x0400197C RID: 6524
		Flex = 262149,
		// Token: 0x0400197D RID: 6525
		FlexBasis = 131081,
		// Token: 0x0400197E RID: 6526
		FlexDirection,
		// Token: 0x0400197F RID: 6527
		FlexGrow,
		// Token: 0x04001980 RID: 6528
		FlexShrink,
		// Token: 0x04001981 RID: 6529
		FlexWrap,
		// Token: 0x04001982 RID: 6530
		FontSize = 65537,
		// Token: 0x04001983 RID: 6531
		Height = 131086,
		// Token: 0x04001984 RID: 6532
		JustifyContent,
		// Token: 0x04001985 RID: 6533
		Left,
		// Token: 0x04001986 RID: 6534
		LetterSpacing = 65538,
		// Token: 0x04001987 RID: 6535
		Margin = 262150,
		// Token: 0x04001988 RID: 6536
		MarginBottom = 131089,
		// Token: 0x04001989 RID: 6537
		MarginLeft,
		// Token: 0x0400198A RID: 6538
		MarginRight,
		// Token: 0x0400198B RID: 6539
		MarginTop,
		// Token: 0x0400198C RID: 6540
		MaxHeight,
		// Token: 0x0400198D RID: 6541
		MaxWidth,
		// Token: 0x0400198E RID: 6542
		MinHeight,
		// Token: 0x0400198F RID: 6543
		MinWidth,
		// Token: 0x04001990 RID: 6544
		Opacity = 458766,
		// Token: 0x04001991 RID: 6545
		Overflow,
		// Token: 0x04001992 RID: 6546
		Padding = 262151,
		// Token: 0x04001993 RID: 6547
		PaddingBottom = 131097,
		// Token: 0x04001994 RID: 6548
		PaddingLeft,
		// Token: 0x04001995 RID: 6549
		PaddingRight,
		// Token: 0x04001996 RID: 6550
		PaddingTop,
		// Token: 0x04001997 RID: 6551
		Position,
		// Token: 0x04001998 RID: 6552
		Right,
		// Token: 0x04001999 RID: 6553
		Rotate = 327680,
		// Token: 0x0400199A RID: 6554
		Scale,
		// Token: 0x0400199B RID: 6555
		TextOverflow = 196609,
		// Token: 0x0400199C RID: 6556
		TextShadow = 65539,
		// Token: 0x0400199D RID: 6557
		Top = 131103,
		// Token: 0x0400199E RID: 6558
		TransformOrigin = 327682,
		// Token: 0x0400199F RID: 6559
		Transition = 262152,
		// Token: 0x040019A0 RID: 6560
		TransitionDelay = 393216,
		// Token: 0x040019A1 RID: 6561
		TransitionDuration,
		// Token: 0x040019A2 RID: 6562
		TransitionProperty,
		// Token: 0x040019A3 RID: 6563
		TransitionTimingFunction,
		// Token: 0x040019A4 RID: 6564
		Translate = 327683,
		// Token: 0x040019A5 RID: 6565
		UnityBackgroundImageTintColor = 196610,
		// Token: 0x040019A6 RID: 6566
		UnityBackgroundScaleMode = 262153,
		// Token: 0x040019A7 RID: 6567
		UnityEditorTextRenderingMode = 65540,
		// Token: 0x040019A8 RID: 6568
		UnityFont,
		// Token: 0x040019A9 RID: 6569
		UnityFontDefinition,
		// Token: 0x040019AA RID: 6570
		UnityFontStyleAndWeight,
		// Token: 0x040019AB RID: 6571
		UnityOverflowClipBox = 196611,
		// Token: 0x040019AC RID: 6572
		UnityParagraphSpacing = 65544,
		// Token: 0x040019AD RID: 6573
		UnitySliceBottom = 196612,
		// Token: 0x040019AE RID: 6574
		UnitySliceLeft,
		// Token: 0x040019AF RID: 6575
		UnitySliceRight,
		// Token: 0x040019B0 RID: 6576
		UnitySliceScale,
		// Token: 0x040019B1 RID: 6577
		UnitySliceTop,
		// Token: 0x040019B2 RID: 6578
		UnitySliceType,
		// Token: 0x040019B3 RID: 6579
		UnityTextAlign = 65545,
		// Token: 0x040019B4 RID: 6580
		UnityTextGenerator,
		// Token: 0x040019B5 RID: 6581
		UnityTextOutline = 262154,
		// Token: 0x040019B6 RID: 6582
		UnityTextOutlineColor = 65547,
		// Token: 0x040019B7 RID: 6583
		UnityTextOutlineWidth,
		// Token: 0x040019B8 RID: 6584
		UnityTextOverflowPosition = 196618,
		// Token: 0x040019B9 RID: 6585
		Visibility = 65549,
		// Token: 0x040019BA RID: 6586
		WhiteSpace,
		// Token: 0x040019BB RID: 6587
		Width = 131104,
		// Token: 0x040019BC RID: 6588
		WordSpacing = 65551
	}
}
