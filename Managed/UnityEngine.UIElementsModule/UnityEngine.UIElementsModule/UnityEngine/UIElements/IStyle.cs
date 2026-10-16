using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020003CD RID: 973
	public interface IStyle
	{
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001CCC RID: 7372
		// (set) Token: 0x06001CCD RID: 7373
		StyleEnum<Align> alignContent { get; set; }

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001CCE RID: 7374
		// (set) Token: 0x06001CCF RID: 7375
		StyleEnum<Align> alignItems { get; set; }

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001CD0 RID: 7376
		// (set) Token: 0x06001CD1 RID: 7377
		StyleEnum<Align> alignSelf { get; set; }

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001CD2 RID: 7378
		// (set) Token: 0x06001CD3 RID: 7379
		StyleColor backgroundColor { get; set; }

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001CD4 RID: 7380
		// (set) Token: 0x06001CD5 RID: 7381
		StyleBackground backgroundImage { get; set; }

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001CD6 RID: 7382
		// (set) Token: 0x06001CD7 RID: 7383
		StyleBackgroundPosition backgroundPositionX { get; set; }

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001CD8 RID: 7384
		// (set) Token: 0x06001CD9 RID: 7385
		StyleBackgroundPosition backgroundPositionY { get; set; }

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001CDA RID: 7386
		// (set) Token: 0x06001CDB RID: 7387
		StyleBackgroundRepeat backgroundRepeat { get; set; }

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001CDC RID: 7388
		// (set) Token: 0x06001CDD RID: 7389
		StyleBackgroundSize backgroundSize { get; set; }

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001CDE RID: 7390
		// (set) Token: 0x06001CDF RID: 7391
		StyleColor borderBottomColor { get; set; }

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001CE0 RID: 7392
		// (set) Token: 0x06001CE1 RID: 7393
		StyleLength borderBottomLeftRadius { get; set; }

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001CE2 RID: 7394
		// (set) Token: 0x06001CE3 RID: 7395
		StyleLength borderBottomRightRadius { get; set; }

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001CE4 RID: 7396
		// (set) Token: 0x06001CE5 RID: 7397
		StyleFloat borderBottomWidth { get; set; }

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001CE6 RID: 7398
		// (set) Token: 0x06001CE7 RID: 7399
		StyleColor borderLeftColor { get; set; }

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001CE8 RID: 7400
		// (set) Token: 0x06001CE9 RID: 7401
		StyleFloat borderLeftWidth { get; set; }

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001CEA RID: 7402
		// (set) Token: 0x06001CEB RID: 7403
		StyleColor borderRightColor { get; set; }

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001CEC RID: 7404
		// (set) Token: 0x06001CED RID: 7405
		StyleFloat borderRightWidth { get; set; }

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001CEE RID: 7406
		// (set) Token: 0x06001CEF RID: 7407
		StyleColor borderTopColor { get; set; }

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001CF0 RID: 7408
		// (set) Token: 0x06001CF1 RID: 7409
		StyleLength borderTopLeftRadius { get; set; }

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001CF2 RID: 7410
		// (set) Token: 0x06001CF3 RID: 7411
		StyleLength borderTopRightRadius { get; set; }

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001CF4 RID: 7412
		// (set) Token: 0x06001CF5 RID: 7413
		StyleFloat borderTopWidth { get; set; }

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001CF6 RID: 7414
		// (set) Token: 0x06001CF7 RID: 7415
		StyleLength bottom { get; set; }

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001CF8 RID: 7416
		// (set) Token: 0x06001CF9 RID: 7417
		StyleColor color { get; set; }

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001CFA RID: 7418
		// (set) Token: 0x06001CFB RID: 7419
		StyleCursor cursor { get; set; }

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001CFC RID: 7420
		// (set) Token: 0x06001CFD RID: 7421
		StyleEnum<DisplayStyle> display { get; set; }

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001CFE RID: 7422
		// (set) Token: 0x06001CFF RID: 7423
		StyleLength flexBasis { get; set; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001D00 RID: 7424
		// (set) Token: 0x06001D01 RID: 7425
		StyleEnum<FlexDirection> flexDirection { get; set; }

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001D02 RID: 7426
		// (set) Token: 0x06001D03 RID: 7427
		StyleFloat flexGrow { get; set; }

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001D04 RID: 7428
		// (set) Token: 0x06001D05 RID: 7429
		StyleFloat flexShrink { get; set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001D06 RID: 7430
		// (set) Token: 0x06001D07 RID: 7431
		StyleEnum<Wrap> flexWrap { get; set; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001D08 RID: 7432
		// (set) Token: 0x06001D09 RID: 7433
		StyleLength fontSize { get; set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001D0A RID: 7434
		// (set) Token: 0x06001D0B RID: 7435
		StyleLength height { get; set; }

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001D0C RID: 7436
		// (set) Token: 0x06001D0D RID: 7437
		StyleEnum<Justify> justifyContent { get; set; }

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001D0E RID: 7438
		// (set) Token: 0x06001D0F RID: 7439
		StyleLength left { get; set; }

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001D10 RID: 7440
		// (set) Token: 0x06001D11 RID: 7441
		StyleLength letterSpacing { get; set; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001D12 RID: 7442
		// (set) Token: 0x06001D13 RID: 7443
		StyleLength marginBottom { get; set; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001D14 RID: 7444
		// (set) Token: 0x06001D15 RID: 7445
		StyleLength marginLeft { get; set; }

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001D16 RID: 7446
		// (set) Token: 0x06001D17 RID: 7447
		StyleLength marginRight { get; set; }

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001D18 RID: 7448
		// (set) Token: 0x06001D19 RID: 7449
		StyleLength marginTop { get; set; }

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001D1A RID: 7450
		// (set) Token: 0x06001D1B RID: 7451
		StyleLength maxHeight { get; set; }

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001D1C RID: 7452
		// (set) Token: 0x06001D1D RID: 7453
		StyleLength maxWidth { get; set; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001D1E RID: 7454
		// (set) Token: 0x06001D1F RID: 7455
		StyleLength minHeight { get; set; }

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001D20 RID: 7456
		// (set) Token: 0x06001D21 RID: 7457
		StyleLength minWidth { get; set; }

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001D22 RID: 7458
		// (set) Token: 0x06001D23 RID: 7459
		StyleFloat opacity { get; set; }

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001D24 RID: 7460
		// (set) Token: 0x06001D25 RID: 7461
		StyleEnum<Overflow> overflow { get; set; }

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001D26 RID: 7462
		// (set) Token: 0x06001D27 RID: 7463
		StyleLength paddingBottom { get; set; }

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001D28 RID: 7464
		// (set) Token: 0x06001D29 RID: 7465
		StyleLength paddingLeft { get; set; }

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001D2A RID: 7466
		// (set) Token: 0x06001D2B RID: 7467
		StyleLength paddingRight { get; set; }

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001D2C RID: 7468
		// (set) Token: 0x06001D2D RID: 7469
		StyleLength paddingTop { get; set; }

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001D2E RID: 7470
		// (set) Token: 0x06001D2F RID: 7471
		StyleEnum<Position> position { get; set; }

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001D30 RID: 7472
		// (set) Token: 0x06001D31 RID: 7473
		StyleLength right { get; set; }

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001D32 RID: 7474
		// (set) Token: 0x06001D33 RID: 7475
		StyleRotate rotate { get; set; }

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001D34 RID: 7476
		// (set) Token: 0x06001D35 RID: 7477
		StyleScale scale { get; set; }

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001D36 RID: 7478
		// (set) Token: 0x06001D37 RID: 7479
		StyleEnum<TextOverflow> textOverflow { get; set; }

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001D38 RID: 7480
		// (set) Token: 0x06001D39 RID: 7481
		StyleTextShadow textShadow { get; set; }

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001D3A RID: 7482
		// (set) Token: 0x06001D3B RID: 7483
		StyleLength top { get; set; }

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001D3C RID: 7484
		// (set) Token: 0x06001D3D RID: 7485
		StyleTransformOrigin transformOrigin { get; set; }

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001D3E RID: 7486
		// (set) Token: 0x06001D3F RID: 7487
		StyleList<TimeValue> transitionDelay { get; set; }

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001D40 RID: 7488
		// (set) Token: 0x06001D41 RID: 7489
		StyleList<TimeValue> transitionDuration { get; set; }

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001D42 RID: 7490
		// (set) Token: 0x06001D43 RID: 7491
		StyleList<StylePropertyName> transitionProperty { get; set; }

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001D44 RID: 7492
		// (set) Token: 0x06001D45 RID: 7493
		StyleList<EasingFunction> transitionTimingFunction { get; set; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001D46 RID: 7494
		// (set) Token: 0x06001D47 RID: 7495
		StyleTranslate translate { get; set; }

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001D48 RID: 7496
		// (set) Token: 0x06001D49 RID: 7497
		StyleColor unityBackgroundImageTintColor { get; set; }

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001D4A RID: 7498
		// (set) Token: 0x06001D4B RID: 7499
		StyleEnum<EditorTextRenderingMode> unityEditorTextRenderingMode { get; set; }

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001D4C RID: 7500
		// (set) Token: 0x06001D4D RID: 7501
		StyleFont unityFont { get; set; }

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001D4E RID: 7502
		// (set) Token: 0x06001D4F RID: 7503
		StyleFontDefinition unityFontDefinition { get; set; }

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001D50 RID: 7504
		// (set) Token: 0x06001D51 RID: 7505
		StyleEnum<FontStyle> unityFontStyleAndWeight { get; set; }

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001D52 RID: 7506
		// (set) Token: 0x06001D53 RID: 7507
		StyleEnum<OverflowClipBox> unityOverflowClipBox { get; set; }

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001D54 RID: 7508
		// (set) Token: 0x06001D55 RID: 7509
		StyleLength unityParagraphSpacing { get; set; }

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001D56 RID: 7510
		// (set) Token: 0x06001D57 RID: 7511
		StyleInt unitySliceBottom { get; set; }

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001D58 RID: 7512
		// (set) Token: 0x06001D59 RID: 7513
		StyleInt unitySliceLeft { get; set; }

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001D5A RID: 7514
		// (set) Token: 0x06001D5B RID: 7515
		StyleInt unitySliceRight { get; set; }

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001D5C RID: 7516
		// (set) Token: 0x06001D5D RID: 7517
		StyleFloat unitySliceScale { get; set; }

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001D5E RID: 7518
		// (set) Token: 0x06001D5F RID: 7519
		StyleInt unitySliceTop { get; set; }

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001D60 RID: 7520
		// (set) Token: 0x06001D61 RID: 7521
		StyleEnum<SliceType> unitySliceType { get; set; }

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001D62 RID: 7522
		// (set) Token: 0x06001D63 RID: 7523
		StyleEnum<TextAnchor> unityTextAlign { get; set; }

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001D64 RID: 7524
		// (set) Token: 0x06001D65 RID: 7525
		StyleEnum<TextGeneratorType> unityTextGenerator { get; set; }

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001D66 RID: 7526
		// (set) Token: 0x06001D67 RID: 7527
		StyleColor unityTextOutlineColor { get; set; }

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001D68 RID: 7528
		// (set) Token: 0x06001D69 RID: 7529
		StyleFloat unityTextOutlineWidth { get; set; }

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001D6A RID: 7530
		// (set) Token: 0x06001D6B RID: 7531
		StyleEnum<TextOverflowPosition> unityTextOverflowPosition { get; set; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001D6C RID: 7532
		// (set) Token: 0x06001D6D RID: 7533
		StyleEnum<Visibility> visibility { get; set; }

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001D6E RID: 7534
		// (set) Token: 0x06001D6F RID: 7535
		StyleEnum<WhiteSpace> whiteSpace { get; set; }

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001D70 RID: 7536
		// (set) Token: 0x06001D71 RID: 7537
		StyleLength width { get; set; }

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001D72 RID: 7538
		// (set) Token: 0x06001D73 RID: 7539
		StyleLength wordSpacing { get; set; }

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001D74 RID: 7540
		// (set) Token: 0x06001D75 RID: 7541
		[Obsolete("unityBackgroundScaleMode is deprecated. Use background-* properties instead.")]
		StyleEnum<ScaleMode> unityBackgroundScaleMode { get; set; }
	}
}
