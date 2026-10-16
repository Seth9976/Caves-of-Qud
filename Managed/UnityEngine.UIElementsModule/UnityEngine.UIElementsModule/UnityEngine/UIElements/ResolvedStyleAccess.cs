using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020003CE RID: 974
	internal class ResolvedStyleAccess : IResolvedStyle
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0006FB3D File Offset: 0x0006DD3D
		public Align alignContent
		{
			get
			{
				return this.ve.computedStyle.alignContent;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x0006FB4F File Offset: 0x0006DD4F
		public Align alignItems
		{
			get
			{
				return this.ve.computedStyle.alignItems;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x0006FB61 File Offset: 0x0006DD61
		public Align alignSelf
		{
			get
			{
				return this.ve.computedStyle.alignSelf;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x0006FB73 File Offset: 0x0006DD73
		public Color backgroundColor
		{
			get
			{
				return this.ve.computedStyle.backgroundColor;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x0006FB85 File Offset: 0x0006DD85
		public Background backgroundImage
		{
			get
			{
				return this.ve.computedStyle.backgroundImage;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x0006FB97 File Offset: 0x0006DD97
		public BackgroundPosition backgroundPositionX
		{
			get
			{
				return this.ve.computedStyle.backgroundPositionX;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0006FBA9 File Offset: 0x0006DDA9
		public BackgroundPosition backgroundPositionY
		{
			get
			{
				return this.ve.computedStyle.backgroundPositionY;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x0006FBBB File Offset: 0x0006DDBB
		public BackgroundRepeat backgroundRepeat
		{
			get
			{
				return this.ve.computedStyle.backgroundRepeat;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x0006FBCD File Offset: 0x0006DDCD
		public BackgroundSize backgroundSize
		{
			get
			{
				return this.ve.computedStyle.backgroundSize;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x0006FBDF File Offset: 0x0006DDDF
		public Color borderBottomColor
		{
			get
			{
				return this.ve.computedStyle.borderBottomColor;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x0006FBF4 File Offset: 0x0006DDF4
		public float borderBottomLeftRadius
		{
			get
			{
				return this.ve.computedStyle.borderBottomLeftRadius.value;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x0006FC1C File Offset: 0x0006DE1C
		public float borderBottomRightRadius
		{
			get
			{
				return this.ve.computedStyle.borderBottomRightRadius.value;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x0006FC41 File Offset: 0x0006DE41
		public float borderBottomWidth
		{
			get
			{
				return this.ve.layoutNode.LayoutBorderBottom;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x0006FC53 File Offset: 0x0006DE53
		public Color borderLeftColor
		{
			get
			{
				return this.ve.computedStyle.borderLeftColor;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x0006FC65 File Offset: 0x0006DE65
		public float borderLeftWidth
		{
			get
			{
				return this.ve.layoutNode.LayoutBorderLeft;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x0006FC77 File Offset: 0x0006DE77
		public Color borderRightColor
		{
			get
			{
				return this.ve.computedStyle.borderRightColor;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x0006FC89 File Offset: 0x0006DE89
		public float borderRightWidth
		{
			get
			{
				return this.ve.layoutNode.LayoutBorderRight;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x0006FC9B File Offset: 0x0006DE9B
		public Color borderTopColor
		{
			get
			{
				return this.ve.computedStyle.borderTopColor;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x0006FCB0 File Offset: 0x0006DEB0
		public float borderTopLeftRadius
		{
			get
			{
				return this.ve.computedStyle.borderTopLeftRadius.value;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x0006FCD8 File Offset: 0x0006DED8
		public float borderTopRightRadius
		{
			get
			{
				return this.ve.computedStyle.borderTopRightRadius.value;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x0006FCFD File Offset: 0x0006DEFD
		public float borderTopWidth
		{
			get
			{
				return this.ve.layoutNode.LayoutBorderTop;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x0006FD0F File Offset: 0x0006DF0F
		public float bottom
		{
			get
			{
				return this.ve.layoutNode.LayoutBottom;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x0006FD21 File Offset: 0x0006DF21
		public Color color
		{
			get
			{
				return this.ve.computedStyle.color;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x0006FD33 File Offset: 0x0006DF33
		public DisplayStyle display
		{
			get
			{
				return this.ve.computedStyle.display;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0006FD45 File Offset: 0x0006DF45
		public StyleFloat flexBasis
		{
			get
			{
				return new StyleFloat(this.ve.layoutNode.ComputedFlexBasis);
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x0006FD5C File Offset: 0x0006DF5C
		public FlexDirection flexDirection
		{
			get
			{
				return this.ve.computedStyle.flexDirection;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x0006FD6E File Offset: 0x0006DF6E
		public float flexGrow
		{
			get
			{
				return this.ve.computedStyle.flexGrow;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x0006FD80 File Offset: 0x0006DF80
		public float flexShrink
		{
			get
			{
				return this.ve.computedStyle.flexShrink;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001D92 RID: 7570 RVA: 0x0006FD92 File Offset: 0x0006DF92
		public Wrap flexWrap
		{
			get
			{
				return this.ve.computedStyle.flexWrap;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x0006FDA4 File Offset: 0x0006DFA4
		public float fontSize
		{
			get
			{
				return this.ve.computedStyle.fontSize.value;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x0006FDC9 File Offset: 0x0006DFC9
		public float height
		{
			get
			{
				return this.ve.layoutNode.LayoutHeight;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0006FDDB File Offset: 0x0006DFDB
		public Justify justifyContent
		{
			get
			{
				return this.ve.computedStyle.justifyContent;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x0006FDED File Offset: 0x0006DFED
		public float left
		{
			get
			{
				return this.ve.layoutNode.LayoutX;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0006FE00 File Offset: 0x0006E000
		public float letterSpacing
		{
			get
			{
				return this.ve.computedStyle.letterSpacing.value;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x0006FE25 File Offset: 0x0006E025
		public float marginBottom
		{
			get
			{
				return this.ve.layoutNode.LayoutMarginBottom;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x0006FE37 File Offset: 0x0006E037
		public float marginLeft
		{
			get
			{
				return this.ve.layoutNode.LayoutMarginLeft;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0006FE49 File Offset: 0x0006E049
		public float marginRight
		{
			get
			{
				return this.ve.layoutNode.LayoutMarginRight;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x0006FE5B File Offset: 0x0006E05B
		public float marginTop
		{
			get
			{
				return this.ve.layoutNode.LayoutMarginTop;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x0006FE6D File Offset: 0x0006E06D
		public StyleFloat maxHeight
		{
			get
			{
				return this.ve.ResolveLengthValue(this.ve.computedStyle.maxHeight, false);
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x0006FE8B File Offset: 0x0006E08B
		public StyleFloat maxWidth
		{
			get
			{
				return this.ve.ResolveLengthValue(this.ve.computedStyle.maxWidth, true);
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0006FEA9 File Offset: 0x0006E0A9
		public StyleFloat minHeight
		{
			get
			{
				return this.ve.ResolveLengthValue(this.ve.computedStyle.minHeight, false);
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x0006FEC7 File Offset: 0x0006E0C7
		public StyleFloat minWidth
		{
			get
			{
				return this.ve.ResolveLengthValue(this.ve.computedStyle.minWidth, true);
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x0006FEE5 File Offset: 0x0006E0E5
		public float opacity
		{
			get
			{
				return this.ve.computedStyle.opacity;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x0006FEF7 File Offset: 0x0006E0F7
		public float paddingBottom
		{
			get
			{
				return this.ve.layoutNode.LayoutPaddingBottom;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x0006FF09 File Offset: 0x0006E109
		public float paddingLeft
		{
			get
			{
				return this.ve.layoutNode.LayoutPaddingLeft;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x0006FF1B File Offset: 0x0006E11B
		public float paddingRight
		{
			get
			{
				return this.ve.layoutNode.LayoutPaddingRight;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x0006FF2D File Offset: 0x0006E12D
		public float paddingTop
		{
			get
			{
				return this.ve.layoutNode.LayoutPaddingTop;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x0006FF3F File Offset: 0x0006E13F
		public Position position
		{
			get
			{
				return this.ve.computedStyle.position;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x0006FF51 File Offset: 0x0006E151
		public float right
		{
			get
			{
				return this.ve.layoutNode.LayoutRight;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x0006FF63 File Offset: 0x0006E163
		public Rotate rotate
		{
			get
			{
				return this.ve.computedStyle.rotate;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x0006FF75 File Offset: 0x0006E175
		public Scale scale
		{
			get
			{
				return this.ve.computedStyle.scale;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x0006FF87 File Offset: 0x0006E187
		public TextOverflow textOverflow
		{
			get
			{
				return this.ve.computedStyle.textOverflow;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x0006FF99 File Offset: 0x0006E199
		public float top
		{
			get
			{
				return this.ve.layoutNode.LayoutY;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0006FFAB File Offset: 0x0006E1AB
		public Vector3 transformOrigin
		{
			get
			{
				return this.ve.ResolveTransformOrigin();
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0006FFB8 File Offset: 0x0006E1B8
		public IEnumerable<TimeValue> transitionDelay
		{
			get
			{
				return this.ve.computedStyle.transitionDelay;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x0006FFCA File Offset: 0x0006E1CA
		public IEnumerable<TimeValue> transitionDuration
		{
			get
			{
				return this.ve.computedStyle.transitionDuration;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x0006FFDC File Offset: 0x0006E1DC
		public IEnumerable<StylePropertyName> transitionProperty
		{
			get
			{
				return this.ve.computedStyle.transitionProperty;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x0006FFEE File Offset: 0x0006E1EE
		public IEnumerable<EasingFunction> transitionTimingFunction
		{
			get
			{
				return this.ve.computedStyle.transitionTimingFunction;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x00070000 File Offset: 0x0006E200
		public Vector3 translate
		{
			get
			{
				return this.ve.ResolveTranslate();
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x0007000D File Offset: 0x0006E20D
		public Color unityBackgroundImageTintColor
		{
			get
			{
				return this.ve.computedStyle.unityBackgroundImageTintColor;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x0007001F File Offset: 0x0006E21F
		public EditorTextRenderingMode unityEditorTextRenderingMode
		{
			get
			{
				return this.ve.computedStyle.unityEditorTextRenderingMode;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x00070031 File Offset: 0x0006E231
		public Font unityFont
		{
			get
			{
				return this.ve.computedStyle.unityFont;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x00070043 File Offset: 0x0006E243
		public FontDefinition unityFontDefinition
		{
			get
			{
				return this.ve.computedStyle.unityFontDefinition;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x00070055 File Offset: 0x0006E255
		public FontStyle unityFontStyleAndWeight
		{
			get
			{
				return this.ve.computedStyle.unityFontStyleAndWeight;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x00070068 File Offset: 0x0006E268
		public float unityParagraphSpacing
		{
			get
			{
				return this.ve.computedStyle.unityParagraphSpacing.value;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x0007008D File Offset: 0x0006E28D
		public int unitySliceBottom
		{
			get
			{
				return this.ve.computedStyle.unitySliceBottom;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x0007009F File Offset: 0x0006E29F
		public int unitySliceLeft
		{
			get
			{
				return this.ve.computedStyle.unitySliceLeft;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x000700B1 File Offset: 0x0006E2B1
		public int unitySliceRight
		{
			get
			{
				return this.ve.computedStyle.unitySliceRight;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001DBA RID: 7610 RVA: 0x000700C3 File Offset: 0x0006E2C3
		public float unitySliceScale
		{
			get
			{
				return this.ve.computedStyle.unitySliceScale;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x000700D5 File Offset: 0x0006E2D5
		public int unitySliceTop
		{
			get
			{
				return this.ve.computedStyle.unitySliceTop;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x000700E7 File Offset: 0x0006E2E7
		public SliceType unitySliceType
		{
			get
			{
				return this.ve.computedStyle.unitySliceType;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x000700F9 File Offset: 0x0006E2F9
		public TextAnchor unityTextAlign
		{
			get
			{
				return this.ve.computedStyle.unityTextAlign;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x0007010B File Offset: 0x0006E30B
		public TextGeneratorType unityTextGenerator
		{
			get
			{
				return this.ve.computedStyle.unityTextGenerator;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x0007011D File Offset: 0x0006E31D
		public Color unityTextOutlineColor
		{
			get
			{
				return this.ve.computedStyle.unityTextOutlineColor;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x0007012F File Offset: 0x0006E32F
		public float unityTextOutlineWidth
		{
			get
			{
				return this.ve.computedStyle.unityTextOutlineWidth;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x00070141 File Offset: 0x0006E341
		public TextOverflowPosition unityTextOverflowPosition
		{
			get
			{
				return this.ve.computedStyle.unityTextOverflowPosition;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x00070153 File Offset: 0x0006E353
		public Visibility visibility
		{
			get
			{
				return this.ve.computedStyle.visibility;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x00070165 File Offset: 0x0006E365
		public WhiteSpace whiteSpace
		{
			get
			{
				return this.ve.computedStyle.whiteSpace;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x00070177 File Offset: 0x0006E377
		public float width
		{
			get
			{
				return this.ve.layoutNode.LayoutWidth;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x0007018C File Offset: 0x0006E38C
		public float wordSpacing
		{
			get
			{
				return this.ve.computedStyle.wordSpacing.value;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x000701B1 File Offset: 0x0006E3B1
		private VisualElement ve { get; }

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000701B9 File Offset: 0x0006E3B9
		public ResolvedStyleAccess(VisualElement ve)
		{
			this.ve = ve;
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x000701CC File Offset: 0x0006E3CC
		[Obsolete("unityBackgroundScaleMode is deprecated. Use background-* properties instead.")]
		public StyleEnum<ScaleMode> unityBackgroundScaleMode
		{
			get
			{
				bool flag;
				return BackgroundPropertyHelper.ResolveUnityBackgroundScaleMode(this.ve.computedStyle.backgroundPositionX, this.ve.computedStyle.backgroundPositionY, this.ve.computedStyle.backgroundRepeat, this.ve.computedStyle.backgroundSize, out flag);
			}
		}
	}
}
