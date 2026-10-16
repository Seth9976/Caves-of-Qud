using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020003CC RID: 972
	public interface IResolvedStyle
	{
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001C7B RID: 7291
		Align alignContent { get; }

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001C7C RID: 7292
		Align alignItems { get; }

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001C7D RID: 7293
		Align alignSelf { get; }

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001C7E RID: 7294
		Color backgroundColor { get; }

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001C7F RID: 7295
		Background backgroundImage { get; }

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001C80 RID: 7296
		BackgroundPosition backgroundPositionX { get; }

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001C81 RID: 7297
		BackgroundPosition backgroundPositionY { get; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001C82 RID: 7298
		BackgroundRepeat backgroundRepeat { get; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001C83 RID: 7299
		BackgroundSize backgroundSize { get; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001C84 RID: 7300
		Color borderBottomColor { get; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001C85 RID: 7301
		float borderBottomLeftRadius { get; }

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001C86 RID: 7302
		float borderBottomRightRadius { get; }

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001C87 RID: 7303
		float borderBottomWidth { get; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001C88 RID: 7304
		Color borderLeftColor { get; }

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001C89 RID: 7305
		float borderLeftWidth { get; }

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001C8A RID: 7306
		Color borderRightColor { get; }

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001C8B RID: 7307
		float borderRightWidth { get; }

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001C8C RID: 7308
		Color borderTopColor { get; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001C8D RID: 7309
		float borderTopLeftRadius { get; }

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001C8E RID: 7310
		float borderTopRightRadius { get; }

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001C8F RID: 7311
		float borderTopWidth { get; }

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001C90 RID: 7312
		float bottom { get; }

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001C91 RID: 7313
		Color color { get; }

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001C92 RID: 7314
		DisplayStyle display { get; }

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001C93 RID: 7315
		StyleFloat flexBasis { get; }

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001C94 RID: 7316
		FlexDirection flexDirection { get; }

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001C95 RID: 7317
		float flexGrow { get; }

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001C96 RID: 7318
		float flexShrink { get; }

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001C97 RID: 7319
		Wrap flexWrap { get; }

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001C98 RID: 7320
		float fontSize { get; }

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001C99 RID: 7321
		float height { get; }

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001C9A RID: 7322
		Justify justifyContent { get; }

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001C9B RID: 7323
		float left { get; }

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001C9C RID: 7324
		float letterSpacing { get; }

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001C9D RID: 7325
		float marginBottom { get; }

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001C9E RID: 7326
		float marginLeft { get; }

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001C9F RID: 7327
		float marginRight { get; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001CA0 RID: 7328
		float marginTop { get; }

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001CA1 RID: 7329
		StyleFloat maxHeight { get; }

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001CA2 RID: 7330
		StyleFloat maxWidth { get; }

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001CA3 RID: 7331
		StyleFloat minHeight { get; }

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001CA4 RID: 7332
		StyleFloat minWidth { get; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001CA5 RID: 7333
		float opacity { get; }

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001CA6 RID: 7334
		float paddingBottom { get; }

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001CA7 RID: 7335
		float paddingLeft { get; }

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001CA8 RID: 7336
		float paddingRight { get; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001CA9 RID: 7337
		float paddingTop { get; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001CAA RID: 7338
		Position position { get; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001CAB RID: 7339
		float right { get; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001CAC RID: 7340
		Rotate rotate { get; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001CAD RID: 7341
		Scale scale { get; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001CAE RID: 7342
		TextOverflow textOverflow { get; }

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001CAF RID: 7343
		float top { get; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001CB0 RID: 7344
		Vector3 transformOrigin { get; }

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001CB1 RID: 7345
		IEnumerable<TimeValue> transitionDelay { get; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001CB2 RID: 7346
		IEnumerable<TimeValue> transitionDuration { get; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001CB3 RID: 7347
		IEnumerable<StylePropertyName> transitionProperty { get; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001CB4 RID: 7348
		IEnumerable<EasingFunction> transitionTimingFunction { get; }

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001CB5 RID: 7349
		Vector3 translate { get; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001CB6 RID: 7350
		Color unityBackgroundImageTintColor { get; }

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001CB7 RID: 7351
		EditorTextRenderingMode unityEditorTextRenderingMode { get; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001CB8 RID: 7352
		Font unityFont { get; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001CB9 RID: 7353
		FontDefinition unityFontDefinition { get; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001CBA RID: 7354
		FontStyle unityFontStyleAndWeight { get; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001CBB RID: 7355
		float unityParagraphSpacing { get; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001CBC RID: 7356
		int unitySliceBottom { get; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001CBD RID: 7357
		int unitySliceLeft { get; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001CBE RID: 7358
		int unitySliceRight { get; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001CBF RID: 7359
		float unitySliceScale { get; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001CC0 RID: 7360
		int unitySliceTop { get; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001CC1 RID: 7361
		SliceType unitySliceType { get; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001CC2 RID: 7362
		TextAnchor unityTextAlign { get; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001CC3 RID: 7363
		TextGeneratorType unityTextGenerator { get; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001CC4 RID: 7364
		Color unityTextOutlineColor { get; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001CC5 RID: 7365
		float unityTextOutlineWidth { get; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001CC6 RID: 7366
		TextOverflowPosition unityTextOverflowPosition { get; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001CC7 RID: 7367
		Visibility visibility { get; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001CC8 RID: 7368
		WhiteSpace whiteSpace { get; }

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001CC9 RID: 7369
		float width { get; }

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001CCA RID: 7370
		float wordSpacing { get; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001CCB RID: 7371
		[Obsolete("unityBackgroundScaleMode is deprecated. Use background-* properties instead.")]
		StyleEnum<ScaleMode> unityBackgroundScaleMode { get; }
	}
}
