using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.Layout;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x0200035C RID: 860
	internal class InlineStyleAccess : StyleValueCollection, IStyle
	{
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x0006A0AC File Offset: 0x000682AC
		// (set) Token: 0x0600196C RID: 6508 RVA: 0x0006A0E0 File Offset: 0x000682E0
		StyleEnum<Align> IStyle.alignContent
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.AlignContent);
				return new StyleEnum<Align>((Align)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignContent, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.AlignContent = (LayoutAlign)this.ve.computedStyle.alignContent;
				}
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x0006A130 File Offset: 0x00068330
		// (set) Token: 0x0600196E RID: 6510 RVA: 0x0006A164 File Offset: 0x00068364
		StyleEnum<Align> IStyle.alignItems
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.AlignItems);
				return new StyleEnum<Align>((Align)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignItems, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.AlignItems = (LayoutAlign)this.ve.computedStyle.alignItems;
				}
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x0006A1B4 File Offset: 0x000683B4
		// (set) Token: 0x06001970 RID: 6512 RVA: 0x0006A1E8 File Offset: 0x000683E8
		StyleEnum<Align> IStyle.alignSelf
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.AlignSelf);
				return new StyleEnum<Align>((Align)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignSelf, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.AlignSelf = (LayoutAlign)this.ve.computedStyle.alignSelf;
				}
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x0006A238 File Offset: 0x00068438
		// (set) Token: 0x06001972 RID: 6514 RVA: 0x0006A258 File Offset: 0x00068458
		StyleColor IStyle.backgroundColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BackgroundColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x0006A28C File Offset: 0x0006848C
		// (set) Token: 0x06001974 RID: 6516 RVA: 0x0006A2AC File Offset: 0x000684AC
		StyleBackground IStyle.backgroundImage
		{
			get
			{
				return base.GetStyleBackground(StylePropertyId.BackgroundImage);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundImage, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Overflow | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x0006A2E0 File Offset: 0x000684E0
		// (set) Token: 0x06001976 RID: 6518 RVA: 0x0006A300 File Offset: 0x00068500
		StyleBackgroundPosition IStyle.backgroundPositionX
		{
			get
			{
				return base.GetStyleBackgroundPosition(StylePropertyId.BackgroundPositionX);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundPositionX, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x0006A334 File Offset: 0x00068534
		// (set) Token: 0x06001978 RID: 6520 RVA: 0x0006A354 File Offset: 0x00068554
		StyleBackgroundPosition IStyle.backgroundPositionY
		{
			get
			{
				return base.GetStyleBackgroundPosition(StylePropertyId.BackgroundPositionY);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundPositionY, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x0006A388 File Offset: 0x00068588
		// (set) Token: 0x0600197A RID: 6522 RVA: 0x0006A3A8 File Offset: 0x000685A8
		StyleBackgroundRepeat IStyle.backgroundRepeat
		{
			get
			{
				return base.GetStyleBackgroundRepeat(StylePropertyId.BackgroundRepeat);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundRepeat, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x0006A3DC File Offset: 0x000685DC
		// (set) Token: 0x0600197C RID: 6524 RVA: 0x0006A3FC File Offset: 0x000685FC
		StyleColor IStyle.borderBottomColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderBottomColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x0006A430 File Offset: 0x00068630
		// (set) Token: 0x0600197E RID: 6526 RVA: 0x0006A450 File Offset: 0x00068650
		StyleLength IStyle.borderBottomLeftRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderBottomLeftRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomLeftRadius, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x0006A484 File Offset: 0x00068684
		// (set) Token: 0x06001980 RID: 6528 RVA: 0x0006A4A4 File Offset: 0x000686A4
		StyleLength IStyle.borderBottomRightRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderBottomRightRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomRightRadius, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x0006A4D8 File Offset: 0x000686D8
		// (set) Token: 0x06001982 RID: 6530 RVA: 0x0006A4F8 File Offset: 0x000686F8
		StyleFloat IStyle.borderBottomWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderBottomWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.layoutNode.BorderBottomWidth = this.ve.computedStyle.borderBottomWidth;
				}
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x0006A54C File Offset: 0x0006874C
		// (set) Token: 0x06001984 RID: 6532 RVA: 0x0006A56C File Offset: 0x0006876C
		StyleColor IStyle.borderLeftColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderLeftColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderLeftColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001985 RID: 6533 RVA: 0x0006A5A0 File Offset: 0x000687A0
		// (set) Token: 0x06001986 RID: 6534 RVA: 0x0006A5C0 File Offset: 0x000687C0
		StyleFloat IStyle.borderLeftWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderLeftWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderLeftWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.layoutNode.BorderLeftWidth = this.ve.computedStyle.borderLeftWidth;
				}
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x0006A614 File Offset: 0x00068814
		// (set) Token: 0x06001988 RID: 6536 RVA: 0x0006A634 File Offset: 0x00068834
		StyleColor IStyle.borderRightColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderRightColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderRightColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x0006A668 File Offset: 0x00068868
		// (set) Token: 0x0600198A RID: 6538 RVA: 0x0006A688 File Offset: 0x00068888
		StyleFloat IStyle.borderRightWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderRightWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderRightWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.layoutNode.BorderRightWidth = this.ve.computedStyle.borderRightWidth;
				}
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x0006A6DC File Offset: 0x000688DC
		// (set) Token: 0x0600198C RID: 6540 RVA: 0x0006A6FC File Offset: 0x000688FC
		StyleColor IStyle.borderTopColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderTopColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x0006A730 File Offset: 0x00068930
		// (set) Token: 0x0600198E RID: 6542 RVA: 0x0006A750 File Offset: 0x00068950
		StyleLength IStyle.borderTopLeftRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderTopLeftRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopLeftRadius, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x0006A784 File Offset: 0x00068984
		// (set) Token: 0x06001990 RID: 6544 RVA: 0x0006A7A4 File Offset: 0x000689A4
		StyleLength IStyle.borderTopRightRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderTopRightRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopRightRadius, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x0006A7D8 File Offset: 0x000689D8
		// (set) Token: 0x06001992 RID: 6546 RVA: 0x0006A7F8 File Offset: 0x000689F8
		StyleFloat IStyle.borderTopWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderTopWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.layoutNode.BorderTopWidth = this.ve.computedStyle.borderTopWidth;
				}
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x0006A84C File Offset: 0x00068A4C
		// (set) Token: 0x06001994 RID: 6548 RVA: 0x0006A86C File Offset: 0x00068A6C
		StyleLength IStyle.bottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Bottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Bottom, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Bottom = this.ve.computedStyle.bottom.ToLayoutValue();
				}
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x0006A8C0 File Offset: 0x00068AC0
		// (set) Token: 0x06001996 RID: 6550 RVA: 0x0006A8E0 File Offset: 0x00068AE0
		StyleColor IStyle.color
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.Color);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Color, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x0006A914 File Offset: 0x00068B14
		// (set) Token: 0x06001998 RID: 6552 RVA: 0x0006A948 File Offset: 0x00068B48
		StyleEnum<DisplayStyle> IStyle.display
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Display);
				return new StyleEnum<DisplayStyle>((DisplayStyle)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<DisplayStyle>(StylePropertyId.Display, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Display = (LayoutDisplay)this.ve.computedStyle.display;
				}
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x0006A998 File Offset: 0x00068B98
		// (set) Token: 0x0600199A RID: 6554 RVA: 0x0006A9B8 File Offset: 0x00068BB8
		StyleLength IStyle.flexBasis
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.FlexBasis);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexBasis, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.FlexBasis = this.ve.computedStyle.flexBasis.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x0006AA0C File Offset: 0x00068C0C
		// (set) Token: 0x0600199C RID: 6556 RVA: 0x0006AA40 File Offset: 0x00068C40
		StyleEnum<FlexDirection> IStyle.flexDirection
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.FlexDirection);
				return new StyleEnum<FlexDirection>((FlexDirection)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<FlexDirection>(StylePropertyId.FlexDirection, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.FlexDirection = (LayoutFlexDirection)this.ve.computedStyle.flexDirection;
				}
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x0006AA90 File Offset: 0x00068C90
		// (set) Token: 0x0600199E RID: 6558 RVA: 0x0006AAB0 File Offset: 0x00068CB0
		StyleFloat IStyle.flexGrow
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.FlexGrow);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexGrow, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.FlexGrow = this.ve.computedStyle.flexGrow;
				}
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x0006AB00 File Offset: 0x00068D00
		// (set) Token: 0x060019A0 RID: 6560 RVA: 0x0006AB20 File Offset: 0x00068D20
		StyleFloat IStyle.flexShrink
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.FlexShrink);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexShrink, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.FlexShrink = this.ve.computedStyle.flexShrink;
				}
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x0006AB70 File Offset: 0x00068D70
		// (set) Token: 0x060019A2 RID: 6562 RVA: 0x0006ABA4 File Offset: 0x00068DA4
		StyleEnum<Wrap> IStyle.flexWrap
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.FlexWrap);
				return new StyleEnum<Wrap>((Wrap)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Wrap>(StylePropertyId.FlexWrap, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Wrap = (LayoutWrap)this.ve.computedStyle.flexWrap;
				}
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x0006ABF4 File Offset: 0x00068DF4
		// (set) Token: 0x060019A4 RID: 6564 RVA: 0x0006AC14 File Offset: 0x00068E14
		StyleLength IStyle.fontSize
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.FontSize);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FontSize, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x0006AC48 File Offset: 0x00068E48
		// (set) Token: 0x060019A6 RID: 6566 RVA: 0x0006AC68 File Offset: 0x00068E68
		StyleLength IStyle.height
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Height);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Height, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Height = this.ve.computedStyle.height.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x0006ACBC File Offset: 0x00068EBC
		// (set) Token: 0x060019A8 RID: 6568 RVA: 0x0006ACF0 File Offset: 0x00068EF0
		StyleEnum<Justify> IStyle.justifyContent
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.JustifyContent);
				return new StyleEnum<Justify>((Justify)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Justify>(StylePropertyId.JustifyContent, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.JustifyContent = (LayoutJustify)this.ve.computedStyle.justifyContent;
				}
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x0006AD40 File Offset: 0x00068F40
		// (set) Token: 0x060019AA RID: 6570 RVA: 0x0006AD60 File Offset: 0x00068F60
		StyleLength IStyle.left
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Left);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Left, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Left = this.ve.computedStyle.left.ToLayoutValue();
				}
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0006ADB4 File Offset: 0x00068FB4
		// (set) Token: 0x060019AC RID: 6572 RVA: 0x0006ADD4 File Offset: 0x00068FD4
		StyleLength IStyle.letterSpacing
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.LetterSpacing);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.LetterSpacing, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x0006AE08 File Offset: 0x00069008
		// (set) Token: 0x060019AE RID: 6574 RVA: 0x0006AE28 File Offset: 0x00069028
		StyleLength IStyle.marginBottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginBottom, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MarginBottom = this.ve.computedStyle.marginBottom.ToLayoutValue();
				}
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x0006AE7C File Offset: 0x0006907C
		// (set) Token: 0x060019B0 RID: 6576 RVA: 0x0006AE9C File Offset: 0x0006909C
		StyleLength IStyle.marginLeft
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginLeft, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MarginLeft = this.ve.computedStyle.marginLeft.ToLayoutValue();
				}
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x0006AEF0 File Offset: 0x000690F0
		// (set) Token: 0x060019B2 RID: 6578 RVA: 0x0006AF10 File Offset: 0x00069110
		StyleLength IStyle.marginRight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginRight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MarginRight = this.ve.computedStyle.marginRight.ToLayoutValue();
				}
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x0006AF64 File Offset: 0x00069164
		// (set) Token: 0x060019B4 RID: 6580 RVA: 0x0006AF84 File Offset: 0x00069184
		StyleLength IStyle.marginTop
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginTop, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MarginTop = this.ve.computedStyle.marginTop.ToLayoutValue();
				}
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x0006AFD8 File Offset: 0x000691D8
		// (set) Token: 0x060019B6 RID: 6582 RVA: 0x0006AFF8 File Offset: 0x000691F8
		StyleLength IStyle.maxHeight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MaxHeight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MaxHeight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MaxHeight = this.ve.computedStyle.maxHeight.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x0006B04C File Offset: 0x0006924C
		// (set) Token: 0x060019B8 RID: 6584 RVA: 0x0006B06C File Offset: 0x0006926C
		StyleLength IStyle.maxWidth
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MaxWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MaxWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MaxWidth = this.ve.computedStyle.maxWidth.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x0006B0C0 File Offset: 0x000692C0
		// (set) Token: 0x060019BA RID: 6586 RVA: 0x0006B0E0 File Offset: 0x000692E0
		StyleLength IStyle.minHeight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MinHeight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MinHeight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MinHeight = this.ve.computedStyle.minHeight.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x0006B134 File Offset: 0x00069334
		// (set) Token: 0x060019BC RID: 6588 RVA: 0x0006B154 File Offset: 0x00069354
		StyleLength IStyle.minWidth
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MinWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MinWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MinWidth = this.ve.computedStyle.minWidth.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x0006B1A8 File Offset: 0x000693A8
		// (set) Token: 0x060019BE RID: 6590 RVA: 0x0006B1C8 File Offset: 0x000693C8
		StyleFloat IStyle.opacity
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.Opacity);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Opacity, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Opacity);
				}
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x0006B1FC File Offset: 0x000693FC
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x0006B230 File Offset: 0x00069430
		StyleEnum<Overflow> IStyle.overflow
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Overflow);
				return new StyleEnum<Overflow>((Overflow)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<OverflowInternal> styleEnum = new StyleEnum<OverflowInternal>((OverflowInternal)value.value, value.keyword);
				bool flag = this.SetStyleValue<OverflowInternal>(StylePropertyId.Overflow, styleEnum);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Overflow);
					this.ve.layoutNode.Overflow = (LayoutOverflow)this.ve.computedStyle.overflow;
				}
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x0006B294 File Offset: 0x00069494
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x0006B2B4 File Offset: 0x000694B4
		StyleLength IStyle.paddingBottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingBottom, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.PaddingBottom = this.ve.computedStyle.paddingBottom.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x0006B308 File Offset: 0x00069508
		// (set) Token: 0x060019C4 RID: 6596 RVA: 0x0006B328 File Offset: 0x00069528
		StyleLength IStyle.paddingLeft
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingLeft, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.PaddingLeft = this.ve.computedStyle.paddingLeft.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x0006B37C File Offset: 0x0006957C
		// (set) Token: 0x060019C6 RID: 6598 RVA: 0x0006B39C File Offset: 0x0006959C
		StyleLength IStyle.paddingRight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingRight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.PaddingRight = this.ve.computedStyle.paddingRight.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x0006B3F0 File Offset: 0x000695F0
		// (set) Token: 0x060019C8 RID: 6600 RVA: 0x0006B410 File Offset: 0x00069610
		StyleLength IStyle.paddingTop
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingTop, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.PaddingTop = this.ve.computedStyle.paddingTop.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x0006B464 File Offset: 0x00069664
		// (set) Token: 0x060019CA RID: 6602 RVA: 0x0006B498 File Offset: 0x00069698
		StyleEnum<Position> IStyle.position
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Position);
				return new StyleEnum<Position>((Position)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Position>(StylePropertyId.Position, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.PositionType = (LayoutPositionType)this.ve.computedStyle.position;
				}
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x0006B4E8 File Offset: 0x000696E8
		// (set) Token: 0x060019CC RID: 6604 RVA: 0x0006B508 File Offset: 0x00069708
		StyleLength IStyle.right
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Right);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Right, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Right = this.ve.computedStyle.right.ToLayoutValue();
				}
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x0006B55C File Offset: 0x0006975C
		// (set) Token: 0x060019CE RID: 6606 RVA: 0x0006B590 File Offset: 0x00069790
		StyleEnum<TextOverflow> IStyle.textOverflow
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.TextOverflow);
				return new StyleEnum<TextOverflow>((TextOverflow)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<TextOverflow>(StylePropertyId.TextOverflow, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x0006B5C4 File Offset: 0x000697C4
		// (set) Token: 0x060019D0 RID: 6608 RVA: 0x0006B5E4 File Offset: 0x000697E4
		StyleLength IStyle.top
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Top);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Top, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Top = this.ve.computedStyle.top.ToLayoutValue();
				}
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x0006B638 File Offset: 0x00069838
		// (set) Token: 0x060019D2 RID: 6610 RVA: 0x0006B658 File Offset: 0x00069858
		StyleList<TimeValue> IStyle.transitionDelay
		{
			get
			{
				return this.GetStyleList<TimeValue>(StylePropertyId.TransitionDelay);
			}
			set
			{
				bool flag = this.SetStyleValue<TimeValue>(StylePropertyId.TransitionDelay, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.TransitionProperty);
				}
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x0006B68C File Offset: 0x0006988C
		// (set) Token: 0x060019D4 RID: 6612 RVA: 0x0006B6AC File Offset: 0x000698AC
		StyleList<TimeValue> IStyle.transitionDuration
		{
			get
			{
				return this.GetStyleList<TimeValue>(StylePropertyId.TransitionDuration);
			}
			set
			{
				bool flag = this.SetStyleValue<TimeValue>(StylePropertyId.TransitionDuration, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.TransitionProperty);
				}
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x0006B6E0 File Offset: 0x000698E0
		// (set) Token: 0x060019D6 RID: 6614 RVA: 0x0006B700 File Offset: 0x00069900
		StyleList<StylePropertyName> IStyle.transitionProperty
		{
			get
			{
				return this.GetStyleList<StylePropertyName>(StylePropertyId.TransitionProperty);
			}
			set
			{
				bool flag = this.SetStyleValue<StylePropertyName>(StylePropertyId.TransitionProperty, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.TransitionProperty);
				}
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x0006B734 File Offset: 0x00069934
		// (set) Token: 0x060019D8 RID: 6616 RVA: 0x0006B754 File Offset: 0x00069954
		StyleList<EasingFunction> IStyle.transitionTimingFunction
		{
			get
			{
				return this.GetStyleList<EasingFunction>(StylePropertyId.TransitionTimingFunction);
			}
			set
			{
				bool flag = this.SetStyleValue<EasingFunction>(StylePropertyId.TransitionTimingFunction, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles);
				}
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x0006B784 File Offset: 0x00069984
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x0006B7A4 File Offset: 0x000699A4
		StyleColor IStyle.unityBackgroundImageTintColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.UnityBackgroundImageTintColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityBackgroundImageTintColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x0006B7D8 File Offset: 0x000699D8
		// (set) Token: 0x060019DC RID: 6620 RVA: 0x0006B80C File Offset: 0x00069A0C
		StyleEnum<EditorTextRenderingMode> IStyle.unityEditorTextRenderingMode
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityEditorTextRenderingMode);
				return new StyleEnum<EditorTextRenderingMode>((EditorTextRenderingMode)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<EditorTextRenderingMode>(StylePropertyId.UnityEditorTextRenderingMode, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x0006B840 File Offset: 0x00069A40
		// (set) Token: 0x060019DE RID: 6622 RVA: 0x0006B860 File Offset: 0x00069A60
		StyleFont IStyle.unityFont
		{
			get
			{
				return base.GetStyleFont(StylePropertyId.UnityFont);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityFont, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x0006B894 File Offset: 0x00069A94
		// (set) Token: 0x060019E0 RID: 6624 RVA: 0x0006B8B4 File Offset: 0x00069AB4
		StyleFontDefinition IStyle.unityFontDefinition
		{
			get
			{
				return base.GetStyleFontDefinition(StylePropertyId.UnityFontDefinition);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityFontDefinition, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x0006B8E8 File Offset: 0x00069AE8
		// (set) Token: 0x060019E2 RID: 6626 RVA: 0x0006B91C File Offset: 0x00069B1C
		StyleEnum<FontStyle> IStyle.unityFontStyleAndWeight
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityFontStyleAndWeight);
				return new StyleEnum<FontStyle>((FontStyle)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<FontStyle>(StylePropertyId.UnityFontStyleAndWeight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x0006B950 File Offset: 0x00069B50
		// (set) Token: 0x060019E4 RID: 6628 RVA: 0x0006B984 File Offset: 0x00069B84
		StyleEnum<OverflowClipBox> IStyle.unityOverflowClipBox
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityOverflowClipBox);
				return new StyleEnum<OverflowClipBox>((OverflowClipBox)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<OverflowClipBox>(StylePropertyId.UnityOverflowClipBox, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x0006B9B8 File Offset: 0x00069BB8
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x0006B9D8 File Offset: 0x00069BD8
		StyleLength IStyle.unityParagraphSpacing
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.UnityParagraphSpacing);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityParagraphSpacing, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x0006BA0C File Offset: 0x00069C0C
		// (set) Token: 0x060019E8 RID: 6632 RVA: 0x0006BA2C File Offset: 0x00069C2C
		StyleInt IStyle.unitySliceBottom
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceBottom, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x0006BA60 File Offset: 0x00069C60
		// (set) Token: 0x060019EA RID: 6634 RVA: 0x0006BA80 File Offset: 0x00069C80
		StyleInt IStyle.unitySliceLeft
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceLeft, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x0006BAB4 File Offset: 0x00069CB4
		// (set) Token: 0x060019EC RID: 6636 RVA: 0x0006BAD4 File Offset: 0x00069CD4
		StyleInt IStyle.unitySliceRight
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceRight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x0006BB08 File Offset: 0x00069D08
		// (set) Token: 0x060019EE RID: 6638 RVA: 0x0006BB28 File Offset: 0x00069D28
		StyleFloat IStyle.unitySliceScale
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.UnitySliceScale);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceScale, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x0006BB5C File Offset: 0x00069D5C
		// (set) Token: 0x060019F0 RID: 6640 RVA: 0x0006BB7C File Offset: 0x00069D7C
		StyleInt IStyle.unitySliceTop
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceTop, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x0006BBB0 File Offset: 0x00069DB0
		// (set) Token: 0x060019F2 RID: 6642 RVA: 0x0006BBE4 File Offset: 0x00069DE4
		StyleEnum<SliceType> IStyle.unitySliceType
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnitySliceType);
				return new StyleEnum<SliceType>((SliceType)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<SliceType>(StylePropertyId.UnitySliceType, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x0006BC18 File Offset: 0x00069E18
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x0006BC4C File Offset: 0x00069E4C
		StyleEnum<TextAnchor> IStyle.unityTextAlign
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityTextAlign);
				return new StyleEnum<TextAnchor>((TextAnchor)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<TextAnchor>(StylePropertyId.UnityTextAlign, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x0006BC80 File Offset: 0x00069E80
		// (set) Token: 0x060019F6 RID: 6646 RVA: 0x0006BCB4 File Offset: 0x00069EB4
		StyleEnum<TextGeneratorType> IStyle.unityTextGenerator
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityTextGenerator);
				return new StyleEnum<TextGeneratorType>((TextGeneratorType)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<TextGeneratorType>(StylePropertyId.UnityTextGenerator, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x0006BCE8 File Offset: 0x00069EE8
		// (set) Token: 0x060019F8 RID: 6648 RVA: 0x0006BD08 File Offset: 0x00069F08
		StyleColor IStyle.unityTextOutlineColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.UnityTextOutlineColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityTextOutlineColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x0006BD3C File Offset: 0x00069F3C
		// (set) Token: 0x060019FA RID: 6650 RVA: 0x0006BD5C File Offset: 0x00069F5C
		StyleFloat IStyle.unityTextOutlineWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.UnityTextOutlineWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityTextOutlineWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x0006BD90 File Offset: 0x00069F90
		// (set) Token: 0x060019FC RID: 6652 RVA: 0x0006BDC4 File Offset: 0x00069FC4
		StyleEnum<TextOverflowPosition> IStyle.unityTextOverflowPosition
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityTextOverflowPosition);
				return new StyleEnum<TextOverflowPosition>((TextOverflowPosition)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<TextOverflowPosition>(StylePropertyId.UnityTextOverflowPosition, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x0006BDF8 File Offset: 0x00069FF8
		// (set) Token: 0x060019FE RID: 6654 RVA: 0x0006BE2C File Offset: 0x0006A02C
		StyleEnum<Visibility> IStyle.visibility
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Visibility);
				return new StyleEnum<Visibility>((Visibility)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Visibility>(StylePropertyId.Visibility, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint | VersionChangeType.Picking);
				}
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x0006BE60 File Offset: 0x0006A060
		// (set) Token: 0x06001A00 RID: 6656 RVA: 0x0006BE94 File Offset: 0x0006A094
		StyleEnum<WhiteSpace> IStyle.whiteSpace
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.WhiteSpace);
				return new StyleEnum<WhiteSpace>((WhiteSpace)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<WhiteSpace>(StylePropertyId.WhiteSpace, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x0006BEC8 File Offset: 0x0006A0C8
		// (set) Token: 0x06001A02 RID: 6658 RVA: 0x0006BEE8 File Offset: 0x0006A0E8
		StyleLength IStyle.width
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Width);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Width, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Width = this.ve.computedStyle.width.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x0006BF3C File Offset: 0x0006A13C
		// (set) Token: 0x06001A04 RID: 6660 RVA: 0x0006BF5C File Offset: 0x0006A15C
		StyleLength IStyle.wordSpacing
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.WordSpacing);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.WordSpacing, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x0006BF8D File Offset: 0x0006A18D
		// (set) Token: 0x06001A06 RID: 6662 RVA: 0x0006BF95 File Offset: 0x0006A195
		private VisualElement ve { get; set; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0006BF9E File Offset: 0x0006A19E
		public InlineStyleAccess.InlineRule inlineRule
		{
			get
			{
				return this.m_InlineRule;
			}
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0006BFA6 File Offset: 0x0006A1A6
		public InlineStyleAccess(VisualElement ve)
		{
			this.ve = ve;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0006BFB8 File Offset: 0x0006A1B8
		protected override void Finalize()
		{
			try
			{
				StyleValue styleValue = default(StyleValue);
				bool flag = base.TryGetStyleValue(StylePropertyId.BackgroundImage, ref styleValue);
				if (flag)
				{
					bool isAllocated = styleValue.resource.IsAllocated;
					if (isAllocated)
					{
						styleValue.resource.Free();
					}
				}
				bool flag2 = base.TryGetStyleValue(StylePropertyId.UnityFont, ref styleValue);
				if (flag2)
				{
					bool isAllocated2 = styleValue.resource.IsAllocated;
					if (isAllocated2)
					{
						styleValue.resource.Free();
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0006C04C File Offset: 0x0006A24C
		public void SetInlineRule(StyleSheet sheet, StyleRule rule)
		{
			this.m_InlineRule.sheet = sheet;
			this.m_InlineRule.rule = rule;
			this.m_InlineRule.propertyIds = StyleSheetCache.GetPropertyIds(rule);
			this.ApplyInlineStyles(this.ve.computedStyle);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0006C08C File Offset: 0x0006A28C
		public bool IsValueSet(StylePropertyId id)
		{
			foreach (StyleValue styleValue in this.m_Values)
			{
				bool flag = styleValue.id == id;
				if (flag)
				{
					return true;
				}
			}
			bool flag2 = this.m_ValuesManaged != null;
			if (flag2)
			{
				foreach (StyleValueManaged styleValueManaged in this.m_ValuesManaged)
				{
					bool flag3 = styleValueManaged.id == id;
					if (flag3)
					{
						return true;
					}
				}
			}
			if (id <= StylePropertyId.Cursor)
			{
				if (id == StylePropertyId.TextShadow)
				{
					return this.m_HasInlineTextShadow;
				}
				if (id == StylePropertyId.Cursor)
				{
					return this.m_HasInlineCursor;
				}
			}
			else
			{
				switch (id)
				{
				case StylePropertyId.Rotate:
					return this.m_HasInlineRotate;
				case StylePropertyId.Scale:
					return this.m_HasInlineScale;
				case StylePropertyId.TransformOrigin:
					return this.m_HasInlineTransformOrigin;
				case StylePropertyId.Translate:
					return this.m_HasInlineTranslate;
				default:
					if (id == StylePropertyId.BackgroundSize)
					{
						return this.m_HasInlineBackgroundSize;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0006C1F0 File Offset: 0x0006A3F0
		public void ApplyInlineStyles(ref ComputedStyle computedStyle)
		{
			VisualElement parent = this.ve.hierarchy.parent;
			ref ComputedStyle ptr;
			if (parent != null)
			{
				ref ComputedStyle computedStyle2 = ref parent.computedStyle;
				ptr = parent.computedStyle;
			}
			else
			{
				ptr = InitialStyle.Get();
			}
			ref ComputedStyle ptr2 = ref ptr;
			bool flag = this.m_InlineRule.sheet != null;
			if (flag)
			{
				InlineStyleAccess.s_StylePropertyReader.SetInlineContext(this.m_InlineRule.sheet, this.m_InlineRule.rule.properties, this.m_InlineRule.propertyIds, 1f);
				computedStyle.ApplyProperties(InlineStyleAccess.s_StylePropertyReader, ref ptr2);
			}
			foreach (StyleValue styleValue in this.m_Values)
			{
				computedStyle.ApplyStyleValue(styleValue, ref ptr2);
			}
			bool flag2 = this.m_ValuesManaged != null;
			if (flag2)
			{
				foreach (StyleValueManaged styleValueManaged in this.m_ValuesManaged)
				{
					computedStyle.ApplyStyleValueManaged(styleValueManaged, ref ptr2);
				}
			}
			bool flag3 = this.ve.style.cursor.keyword != StyleKeyword.Null;
			if (flag3)
			{
				computedStyle.ApplyStyleCursor(this.ve.style.cursor.value);
			}
			bool flag4 = this.ve.style.textShadow.keyword != StyleKeyword.Null;
			if (flag4)
			{
				computedStyle.ApplyStyleTextShadow(this.ve.style.textShadow.value);
			}
			bool hasInlineTransformOrigin = this.m_HasInlineTransformOrigin;
			if (hasInlineTransformOrigin)
			{
				computedStyle.ApplyStyleTransformOrigin(this.ve.style.transformOrigin.value);
			}
			bool hasInlineTranslate = this.m_HasInlineTranslate;
			if (hasInlineTranslate)
			{
				computedStyle.ApplyStyleTranslate(this.ve.style.translate.value);
			}
			bool hasInlineScale = this.m_HasInlineScale;
			if (hasInlineScale)
			{
				computedStyle.ApplyStyleScale(this.ve.style.scale.value);
			}
			bool hasInlineRotate = this.m_HasInlineRotate;
			if (hasInlineRotate)
			{
				computedStyle.ApplyStyleRotate(this.ve.style.rotate.value);
			}
			bool hasInlineBackgroundSize = this.m_HasInlineBackgroundSize;
			if (hasInlineBackgroundSize)
			{
				computedStyle.ApplyStyleBackgroundSize(this.ve.style.backgroundSize.value);
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x0006C4A8 File Offset: 0x0006A6A8
		// (set) Token: 0x06001A0E RID: 6670 RVA: 0x0006C4D8 File Offset: 0x0006A6D8
		StyleCursor IStyle.cursor
		{
			get
			{
				StyleCursor styleCursor = default(StyleCursor);
				bool flag = this.TryGetInlineCursor(ref styleCursor);
				StyleCursor styleCursor2;
				if (flag)
				{
					styleCursor2 = styleCursor;
				}
				else
				{
					styleCursor2 = StyleKeyword.Null;
				}
				return styleCursor2;
			}
			set
			{
				bool flag = this.SetInlineCursor(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles);
				}
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x0006C504 File Offset: 0x0006A704
		// (set) Token: 0x06001A10 RID: 6672 RVA: 0x0006C534 File Offset: 0x0006A734
		StyleTextShadow IStyle.textShadow
		{
			get
			{
				StyleTextShadow styleTextShadow = default(StyleTextShadow);
				bool flag = this.TryGetInlineTextShadow(ref styleTextShadow);
				StyleTextShadow styleTextShadow2;
				if (flag)
				{
					styleTextShadow2 = styleTextShadow;
				}
				else
				{
					styleTextShadow2 = StyleKeyword.Null;
				}
				return styleTextShadow2;
			}
			set
			{
				bool flag = this.SetInlineTextShadow(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x0006C560 File Offset: 0x0006A760
		// (set) Token: 0x06001A12 RID: 6674 RVA: 0x0006C590 File Offset: 0x0006A790
		StyleBackgroundSize IStyle.backgroundSize
		{
			get
			{
				StyleBackgroundSize styleBackgroundSize = default(StyleBackgroundSize);
				bool flag = this.TryGetInlineBackgroundSize(ref styleBackgroundSize);
				StyleBackgroundSize styleBackgroundSize2;
				if (flag)
				{
					styleBackgroundSize2 = styleBackgroundSize;
				}
				else
				{
					styleBackgroundSize2 = StyleKeyword.Null;
				}
				return styleBackgroundSize2;
			}
			set
			{
				bool flag = this.SetInlineBackgroundSize(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0006C5BC File Offset: 0x0006A7BC
		private StyleList<T> GetStyleList<T>(StylePropertyId id)
		{
			StyleValueManaged styleValueManaged = default(StyleValueManaged);
			bool flag = this.TryGetStyleValueManaged(id, ref styleValueManaged);
			StyleList<T> styleList;
			if (flag)
			{
				styleList = new StyleList<T>(styleValueManaged.value as List<T>, styleValueManaged.keyword);
			}
			else
			{
				styleList = StyleKeyword.Null;
			}
			return styleList;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0006C604 File Offset: 0x0006A804
		private void SetStyleValueManaged(StyleValueManaged value)
		{
			bool flag = this.m_ValuesManaged == null;
			if (flag)
			{
				this.m_ValuesManaged = new List<StyleValueManaged>();
			}
			for (int i = 0; i < this.m_ValuesManaged.Count; i++)
			{
				bool flag2 = this.m_ValuesManaged[i].id == value.id;
				if (flag2)
				{
					bool flag3 = value.keyword == StyleKeyword.Null;
					if (flag3)
					{
						this.m_ValuesManaged.RemoveAt(i);
					}
					else
					{
						this.m_ValuesManaged[i] = value;
					}
					return;
				}
			}
			this.m_ValuesManaged.Add(value);
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x0006C6A4 File Offset: 0x0006A8A4
		private bool TryGetStyleValueManaged(StylePropertyId id, ref StyleValueManaged value)
		{
			value.id = StylePropertyId.Unknown;
			bool flag = this.m_ValuesManaged == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				foreach (StyleValueManaged styleValueManaged in this.m_ValuesManaged)
				{
					bool flag3 = styleValueManaged.id == id;
					if (flag3)
					{
						value = styleValueManaged;
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x0006C730 File Offset: 0x0006A930
		// (set) Token: 0x06001A17 RID: 6679 RVA: 0x0006C760 File Offset: 0x0006A960
		StyleTransformOrigin IStyle.transformOrigin
		{
			get
			{
				StyleTransformOrigin styleTransformOrigin = default(StyleTransformOrigin);
				bool flag = this.TryGetInlineTransformOrigin(ref styleTransformOrigin);
				StyleTransformOrigin styleTransformOrigin2;
				if (flag)
				{
					styleTransformOrigin2 = styleTransformOrigin;
				}
				else
				{
					styleTransformOrigin2 = StyleKeyword.Null;
				}
				return styleTransformOrigin2;
			}
			set
			{
				bool flag = this.SetInlineTransformOrigin(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x0006C78C File Offset: 0x0006A98C
		// (set) Token: 0x06001A19 RID: 6681 RVA: 0x0006C7BC File Offset: 0x0006A9BC
		StyleTranslate IStyle.translate
		{
			get
			{
				StyleTranslate styleTranslate = default(StyleTranslate);
				bool flag = this.TryGetInlineTranslate(ref styleTranslate);
				StyleTranslate styleTranslate2;
				if (flag)
				{
					styleTranslate2 = styleTranslate;
				}
				else
				{
					styleTranslate2 = StyleKeyword.Null;
				}
				return styleTranslate2;
			}
			set
			{
				bool flag = this.SetInlineTranslate(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x0006C7E8 File Offset: 0x0006A9E8
		// (set) Token: 0x06001A1B RID: 6683 RVA: 0x0006C818 File Offset: 0x0006AA18
		StyleRotate IStyle.rotate
		{
			get
			{
				StyleRotate styleRotate = default(StyleRotate);
				bool flag = this.TryGetInlineRotate(ref styleRotate);
				StyleRotate styleRotate2;
				if (flag)
				{
					styleRotate2 = styleRotate;
				}
				else
				{
					styleRotate2 = StyleKeyword.Null;
				}
				return styleRotate2;
			}
			set
			{
				bool flag = this.SetInlineRotate(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x0006C844 File Offset: 0x0006AA44
		// (set) Token: 0x06001A1D RID: 6685 RVA: 0x0006C874 File Offset: 0x0006AA74
		StyleScale IStyle.scale
		{
			get
			{
				StyleScale styleScale = default(StyleScale);
				bool flag = this.TryGetInlineScale(ref styleScale);
				StyleScale styleScale2;
				if (flag)
				{
					styleScale2 = styleScale;
				}
				else
				{
					styleScale2 = StyleKeyword.Null;
				}
				return styleScale2;
			}
			set
			{
				bool flag = this.SetInlineScale(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0006C8A0 File Offset: 0x0006AAA0
		private bool SetStyleValue(StylePropertyId id, StyleBackgroundPosition inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.position == inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.position = inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0006C960 File Offset: 0x0006AB60
		private bool SetStyleValue(StylePropertyId id, StyleBackgroundRepeat inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.repeat == inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.repeat = inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0006CA20 File Offset: 0x0006AC20
		private bool SetStyleValue(StylePropertyId id, StyleLength inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.length == inlineValue.ToLength() && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.length = inlineValue.ToLength();
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0006CADC File Offset: 0x0006ACDC
		private bool SetStyleValue(StylePropertyId id, StyleFloat inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.number == inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.number = inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0006CB94 File Offset: 0x0006AD94
		private bool SetStyleValue(StylePropertyId id, StyleInt inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.number == (float)inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.number = (float)inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0006CC50 File Offset: 0x0006AE50
		private bool SetStyleValue(StylePropertyId id, StyleColor inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.color == inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.color = inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0006CD10 File Offset: 0x0006AF10
		private bool SetStyleValue<T>(StylePropertyId id, StyleEnum<T> inlineValue) where T : struct, IConvertible
		{
			StyleValue styleValue = default(StyleValue);
			int num = UnsafeUtility.EnumToInt<T>(inlineValue.value);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.number == (float)num && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.number = (float)num;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0006CDD0 File Offset: 0x0006AFD0
		private bool SetStyleValue(StylePropertyId id, StyleBackground inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				VectorImage vectorImage = (styleValue.resource.IsAllocated ? (styleValue.resource.Target as VectorImage) : null);
				Sprite sprite = (styleValue.resource.IsAllocated ? (styleValue.resource.Target as Sprite) : null);
				Texture2D texture2D = (styleValue.resource.IsAllocated ? (styleValue.resource.Target as Texture2D) : null);
				RenderTexture renderTexture = (styleValue.resource.IsAllocated ? (styleValue.resource.Target as RenderTexture) : null);
				bool flag2 = vectorImage == inlineValue.value.vectorImage && texture2D == inlineValue.value.texture && sprite == inlineValue.value.sprite && renderTexture == inlineValue.value.renderTexture && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
				bool isAllocated = styleValue.resource.IsAllocated;
				if (isAllocated)
				{
					styleValue.resource.Free();
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.value.vectorImage != null;
			if (flag4)
			{
				styleValue.resource = GCHandle.Alloc(inlineValue.value.vectorImage);
			}
			else
			{
				bool flag5 = inlineValue.value.sprite != null;
				if (flag5)
				{
					styleValue.resource = GCHandle.Alloc(inlineValue.value.sprite);
				}
				else
				{
					bool flag6 = inlineValue.value.texture != null;
					if (flag6)
					{
						styleValue.resource = GCHandle.Alloc(inlineValue.value.texture);
					}
					else
					{
						bool flag7 = inlineValue.value.renderTexture != null;
						if (flag7)
						{
							styleValue.resource = GCHandle.Alloc(inlineValue.value.renderTexture);
						}
						else
						{
							styleValue.resource = default(GCHandle);
						}
					}
				}
			}
			base.SetStyleValue(styleValue);
			bool flag8 = inlineValue.keyword == StyleKeyword.Null;
			bool flag9;
			if (flag8)
			{
				flag9 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				flag9 = true;
			}
			return flag9;
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x0006D088 File Offset: 0x0006B288
		private bool SetStyleValue(StylePropertyId id, StyleFontDefinition inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				Font font = (styleValue.resource.IsAllocated ? (styleValue.resource.Target as Font) : null);
				FontAsset fontAsset = (styleValue.resource.IsAllocated ? (styleValue.resource.Target as FontAsset) : null);
				bool flag2 = font == inlineValue.value.font && fontAsset == inlineValue.value.fontAsset && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
				bool isAllocated = styleValue.resource.IsAllocated;
				if (isAllocated)
				{
					styleValue.resource.Free();
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.value.font != null;
			if (flag4)
			{
				styleValue.resource = GCHandle.Alloc(inlineValue.value.font);
			}
			else
			{
				bool flag5 = inlineValue.value.fontAsset != null;
				if (flag5)
				{
					styleValue.resource = GCHandle.Alloc(inlineValue.value.fontAsset);
				}
				else
				{
					styleValue.resource = default(GCHandle);
				}
			}
			base.SetStyleValue(styleValue);
			bool flag6 = inlineValue.keyword == StyleKeyword.Null;
			bool flag7;
			if (flag6)
			{
				flag7 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				flag7 = true;
			}
			return flag7;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x0006D24C File Offset: 0x0006B44C
		private bool SetStyleValue(StylePropertyId id, StyleFont inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				Font font = (styleValue.resource.IsAllocated ? (styleValue.resource.Target as Font) : null);
				bool flag2 = font == inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
				bool isAllocated = styleValue.resource.IsAllocated;
				if (isAllocated)
				{
					styleValue.resource.Free();
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.resource = ((inlineValue.value != null) ? GCHandle.Alloc(inlineValue.value) : default(GCHandle));
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0006D370 File Offset: 0x0006B570
		private bool SetStyleValue<T>(StylePropertyId id, StyleList<T> inlineValue)
		{
			StyleValueManaged styleValueManaged = default(StyleValueManaged);
			bool flag = this.TryGetStyleValueManaged(id, ref styleValueManaged);
			if (flag)
			{
				bool flag2 = styleValueManaged.keyword == inlineValue.keyword;
				if (flag2)
				{
					bool flag3 = styleValueManaged.value == null && inlineValue.value == null;
					if (flag3)
					{
						return false;
					}
					List<T> list = styleValueManaged.value as List<T>;
					bool flag4 = list != null && inlineValue.value != null && list.SequenceEqual<T>(inlineValue.value);
					if (flag4)
					{
						return false;
					}
				}
			}
			else
			{
				bool flag5 = inlineValue.keyword == StyleKeyword.Null;
				if (flag5)
				{
					return false;
				}
			}
			styleValueManaged.id = id;
			styleValueManaged.keyword = inlineValue.keyword;
			bool flag6 = inlineValue.value != null;
			if (flag6)
			{
				bool flag7 = styleValueManaged.value == null;
				if (flag7)
				{
					styleValueManaged.value = new List<T>(inlineValue.value);
				}
				else
				{
					List<T> list2 = (List<T>)styleValueManaged.value;
					list2.Clear();
					list2.AddRange(inlineValue.value);
				}
			}
			else
			{
				styleValueManaged.value = null;
			}
			this.SetStyleValueManaged(styleValueManaged);
			bool flag8 = inlineValue.keyword == StyleKeyword.Null;
			bool flag9;
			if (flag8)
			{
				flag9 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValueManaged);
				flag9 = true;
			}
			return flag9;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0006D4D0 File Offset: 0x0006B6D0
		private bool SetInlineCursor(StyleCursor inlineValue)
		{
			StyleCursor styleCursor = default(StyleCursor);
			bool flag = this.TryGetInlineCursor(ref styleCursor);
			if (flag)
			{
				bool flag2 = styleCursor.value == inlineValue.value && styleCursor.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleCursor.value = inlineValue.value;
			styleCursor.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				this.m_HasInlineCursor = false;
				flag5 = this.RemoveInlineStyle(StylePropertyId.Cursor);
			}
			else
			{
				this.m_InlineCursor = styleCursor;
				this.m_HasInlineCursor = true;
				this.ApplyStyleCursor(styleCursor);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0006D59C File Offset: 0x0006B79C
		private void ApplyStyleCursor(StyleCursor cursor)
		{
			this.ve.computedStyle.ApplyStyleCursor(cursor.value);
			BaseVisualElementPanel elementPanel = this.ve.elementPanel;
			bool flag = ((elementPanel != null) ? elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId) : null) == this.ve;
			if (flag)
			{
				this.ve.elementPanel.cursorManager.SetCursor(cursor.value);
			}
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0006D608 File Offset: 0x0006B808
		private bool SetInlineTextShadow(StyleTextShadow inlineValue)
		{
			StyleTextShadow styleTextShadow = default(StyleTextShadow);
			bool flag = this.TryGetInlineTextShadow(ref styleTextShadow);
			if (flag)
			{
				bool flag2 = styleTextShadow.value == inlineValue.value && styleTextShadow.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleTextShadow.value = inlineValue.value;
			styleTextShadow.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				this.m_HasInlineTextShadow = false;
				flag5 = this.RemoveInlineStyle(StylePropertyId.TextShadow);
			}
			else
			{
				this.m_InlineTextShadow = styleTextShadow;
				this.m_HasInlineTextShadow = true;
				this.ApplyStyleTextShadow(styleTextShadow);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0006D6D4 File Offset: 0x0006B8D4
		private void ApplyStyleTextShadow(StyleTextShadow textShadow)
		{
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool flag = false;
			ComputedTransitionProperty computedTransitionProperty;
			bool flag2 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.TextShadow, out computedTransitionProperty);
			if (flag2)
			{
				flag = ComputedStyle.StartAnimationInlineTextShadow(this.ve, this.ve.computedStyle, textShadow, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.TextShadow);
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this.ve.computedStyle.ApplyStyleTextShadow(textShadow.value);
			}
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0006D79C File Offset: 0x0006B99C
		private bool SetInlineTransformOrigin(StyleTransformOrigin inlineValue)
		{
			StyleTransformOrigin styleTransformOrigin = default(StyleTransformOrigin);
			bool flag = this.TryGetInlineTransformOrigin(ref styleTransformOrigin);
			if (flag)
			{
				bool flag2 = styleTransformOrigin.value == inlineValue.value && styleTransformOrigin.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				this.m_HasInlineTransformOrigin = false;
				flag5 = this.RemoveInlineStyle(StylePropertyId.TransformOrigin);
			}
			else
			{
				this.m_InlineTransformOrigin = inlineValue;
				this.m_HasInlineTransformOrigin = true;
				this.ApplyStyleTransformOrigin(inlineValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0006D848 File Offset: 0x0006BA48
		private void ApplyStyleTransformOrigin(StyleTransformOrigin transformOrigin)
		{
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool flag = false;
			ComputedTransitionProperty computedTransitionProperty;
			bool flag2 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.TransformOrigin, out computedTransitionProperty);
			if (flag2)
			{
				flag = ComputedStyle.StartAnimationInlineTransformOrigin(this.ve, this.ve.computedStyle, transformOrigin, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.TransformOrigin);
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this.ve.computedStyle.ApplyStyleTransformOrigin(transformOrigin.value);
			}
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0006D910 File Offset: 0x0006BB10
		private bool SetInlineTranslate(StyleTranslate inlineValue)
		{
			StyleTranslate styleTranslate = default(StyleTranslate);
			bool flag = this.TryGetInlineTranslate(ref styleTranslate);
			if (flag)
			{
				bool flag2 = styleTranslate.value == inlineValue.value && styleTranslate.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				this.m_HasInlineTranslate = false;
				flag5 = this.RemoveInlineStyle(StylePropertyId.Translate);
			}
			else
			{
				this.m_InlineTranslateOperation = inlineValue;
				this.m_HasInlineTranslate = true;
				this.ApplyStyleTranslate(inlineValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0006D9BC File Offset: 0x0006BBBC
		private void ApplyStyleTranslate(StyleTranslate translate)
		{
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool flag = false;
			ComputedTransitionProperty computedTransitionProperty;
			bool flag2 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.Translate, out computedTransitionProperty);
			if (flag2)
			{
				flag = ComputedStyle.StartAnimationInlineTranslate(this.ve, this.ve.computedStyle, translate, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.Translate);
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this.ve.computedStyle.ApplyStyleTranslate(translate.value);
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0006DA84 File Offset: 0x0006BC84
		private bool SetInlineScale(StyleScale inlineValue)
		{
			StyleScale styleScale = default(StyleScale);
			bool flag = this.TryGetInlineScale(ref styleScale);
			if (flag)
			{
				bool flag2 = styleScale.value == inlineValue.value && styleScale.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				this.m_HasInlineScale = false;
				flag5 = this.RemoveInlineStyle(StylePropertyId.Scale);
			}
			else
			{
				this.m_InlineScale = inlineValue;
				this.m_HasInlineScale = true;
				this.ApplyStyleScale(inlineValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0006DB30 File Offset: 0x0006BD30
		private void ApplyStyleScale(StyleScale scale)
		{
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool flag = false;
			ComputedTransitionProperty computedTransitionProperty;
			bool flag2 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.Scale, out computedTransitionProperty);
			if (flag2)
			{
				flag = ComputedStyle.StartAnimationInlineScale(this.ve, this.ve.computedStyle, scale, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.Scale);
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this.ve.computedStyle.ApplyStyleScale(scale.value);
			}
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0006DBF8 File Offset: 0x0006BDF8
		private bool SetInlineRotate(StyleRotate inlineValue)
		{
			StyleRotate styleRotate = default(StyleRotate);
			bool flag = this.TryGetInlineRotate(ref styleRotate);
			if (flag)
			{
				bool flag2 = styleRotate.value == inlineValue.value && styleRotate.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				this.m_HasInlineRotate = false;
				flag5 = this.RemoveInlineStyle(StylePropertyId.Rotate);
			}
			else
			{
				this.m_InlineRotateOperation = inlineValue;
				this.m_HasInlineRotate = true;
				this.ApplyStyleRotate(inlineValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0006DCA4 File Offset: 0x0006BEA4
		private void ApplyStyleRotate(StyleRotate rotate)
		{
			VisualElement parent = this.ve.hierarchy.parent;
			if (parent != null)
			{
				ref ComputedStyle computedStyle = ref parent.computedStyle;
				ref ComputedStyle computedStyle2 = ref parent.computedStyle;
			}
			else
			{
				InitialStyle.Get();
			}
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool flag = false;
			ComputedTransitionProperty computedTransitionProperty;
			bool flag2 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.Rotate, out computedTransitionProperty);
			if (flag2)
			{
				flag = ComputedStyle.StartAnimationInlineRotate(this.ve, this.ve.computedStyle, rotate, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.Rotate);
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this.ve.computedStyle.ApplyStyleRotate(rotate.value);
			}
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0006DDA0 File Offset: 0x0006BFA0
		private bool SetInlineBackgroundSize(StyleBackgroundSize inlineValue)
		{
			StyleBackgroundSize styleBackgroundSize = default(StyleBackgroundSize);
			bool flag = this.TryGetInlineBackgroundSize(ref styleBackgroundSize);
			if (flag)
			{
				bool flag2 = styleBackgroundSize.value == inlineValue.value && styleBackgroundSize.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				this.m_HasInlineBackgroundSize = false;
				flag5 = this.RemoveInlineStyle(StylePropertyId.BackgroundSize);
			}
			else
			{
				this.m_InlineBackgroundSize = inlineValue;
				this.m_HasInlineBackgroundSize = true;
				this.ApplyStyleBackgroundSize(inlineValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0006DE4C File Offset: 0x0006C04C
		private void ApplyStyleBackgroundSize(StyleBackgroundSize backgroundSize)
		{
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool flag = false;
			ComputedTransitionProperty computedTransitionProperty;
			bool flag2 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.BackgroundSize, out computedTransitionProperty);
			if (flag2)
			{
				flag = ComputedStyle.StartAnimationInlineBackgroundSize(this.ve, this.ve.computedStyle, backgroundSize, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.TransformOrigin);
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this.ve.computedStyle.ApplyStyleBackgroundSize(backgroundSize.value);
			}
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0006DF14 File Offset: 0x0006C114
		private void ApplyStyleValue(StyleValue value)
		{
			VisualElement parent = this.ve.hierarchy.parent;
			ref ComputedStyle ptr;
			if (parent != null)
			{
				ref ComputedStyle computedStyle = ref parent.computedStyle;
				ptr = parent.computedStyle;
			}
			else
			{
				ptr = InitialStyle.Get();
			}
			ref ComputedStyle ptr2 = ref ptr;
			bool flag = false;
			bool flag2 = StylePropertyUtil.IsAnimatable(value.id);
			if (flag2)
			{
				ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
				ComputedTransitionProperty computedTransitionProperty;
				bool flag3 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(value.id, out computedTransitionProperty);
				if (flag3)
				{
					flag = ComputedStyle.StartAnimationInline(this.ve, value.id, this.ve.computedStyle, value, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
				}
				else
				{
					this.ve.styleAnimation.CancelAnimation(value.id);
				}
			}
			bool flag4 = !flag;
			if (flag4)
			{
				this.ve.computedStyle.ApplyStyleValue(value, ref ptr2);
			}
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0006E028 File Offset: 0x0006C228
		private void ApplyStyleValue(StyleValueManaged value)
		{
			VisualElement parent = this.ve.hierarchy.parent;
			ref ComputedStyle ptr;
			if (parent != null)
			{
				ref ComputedStyle computedStyle = ref parent.computedStyle;
				ptr = parent.computedStyle;
			}
			else
			{
				ptr = InitialStyle.Get();
			}
			ref ComputedStyle ptr2 = ref ptr;
			this.ve.computedStyle.ApplyStyleValueManaged(value, ref ptr2);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0006E078 File Offset: 0x0006C278
		private bool RemoveInlineStyle(StylePropertyId id)
		{
			long matchingRulesHash = this.ve.computedStyle.matchingRulesHash;
			bool flag = matchingRulesHash == 0L;
			bool flag2;
			if (flag)
			{
				this.ApplyFromComputedStyle(id, InitialStyle.Get());
				flag2 = true;
			}
			else
			{
				ComputedStyle computedStyle;
				bool flag3 = StyleCache.TryGetValue(matchingRulesHash, out computedStyle);
				if (flag3)
				{
					this.ApplyFromComputedStyle(id, ref computedStyle);
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0006E0D4 File Offset: 0x0006C2D4
		private void ApplyFromComputedStyle(StylePropertyId id, ref ComputedStyle newStyle)
		{
			bool flag = false;
			bool flag2 = StylePropertyUtil.IsAnimatable(id);
			if (flag2)
			{
				ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
				ComputedTransitionProperty computedTransitionProperty;
				bool flag3 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(id, out computedTransitionProperty);
				if (flag3)
				{
					flag = ComputedStyle.StartAnimation(this.ve, id, this.ve.computedStyle, ref newStyle, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
				}
				else
				{
					this.ve.styleAnimation.CancelAnimation(id);
				}
			}
			bool flag4 = !flag;
			if (flag4)
			{
				this.ve.computedStyle.ApplyFromComputedStyle(id, ref newStyle);
			}
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0006E1A0 File Offset: 0x0006C3A0
		public bool TryGetInlineCursor(ref StyleCursor value)
		{
			bool hasInlineCursor = this.m_HasInlineCursor;
			bool flag;
			if (hasInlineCursor)
			{
				value = this.m_InlineCursor;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x0006E1D0 File Offset: 0x0006C3D0
		public bool TryGetInlineTextShadow(ref StyleTextShadow value)
		{
			bool hasInlineTextShadow = this.m_HasInlineTextShadow;
			bool flag;
			if (hasInlineTextShadow)
			{
				value = this.m_InlineTextShadow;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0006E200 File Offset: 0x0006C400
		public bool TryGetInlineTransformOrigin(ref StyleTransformOrigin value)
		{
			bool hasInlineTransformOrigin = this.m_HasInlineTransformOrigin;
			bool flag;
			if (hasInlineTransformOrigin)
			{
				value = this.m_InlineTransformOrigin;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0006E230 File Offset: 0x0006C430
		public bool TryGetInlineTranslate(ref StyleTranslate value)
		{
			bool hasInlineTranslate = this.m_HasInlineTranslate;
			bool flag;
			if (hasInlineTranslate)
			{
				value = this.m_InlineTranslateOperation;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x0006E260 File Offset: 0x0006C460
		public bool TryGetInlineRotate(ref StyleRotate value)
		{
			bool hasInlineRotate = this.m_HasInlineRotate;
			bool flag;
			if (hasInlineRotate)
			{
				value = this.m_InlineRotateOperation;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0006E290 File Offset: 0x0006C490
		public bool TryGetInlineScale(ref StyleScale value)
		{
			bool hasInlineScale = this.m_HasInlineScale;
			bool flag;
			if (hasInlineScale)
			{
				value = this.m_InlineScale;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0006E2C0 File Offset: 0x0006C4C0
		public bool TryGetInlineBackgroundSize(ref StyleBackgroundSize value)
		{
			bool hasInlineBackgroundSize = this.m_HasInlineBackgroundSize;
			bool flag;
			if (hasInlineBackgroundSize)
			{
				value = this.m_InlineBackgroundSize;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x0006E2F0 File Offset: 0x0006C4F0
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x0006E370 File Offset: 0x0006C570
		StyleEnum<ScaleMode> IStyle.unityBackgroundScaleMode
		{
			get
			{
				bool flag;
				return new StyleEnum<ScaleMode>(BackgroundPropertyHelper.ResolveUnityBackgroundScaleMode(this.ve.style.backgroundPositionX.value, this.ve.style.backgroundPositionY.value, this.ve.style.backgroundRepeat.value, this.ve.style.backgroundSize.value, out flag));
			}
			set
			{
				this.ve.style.backgroundPositionX = BackgroundPropertyHelper.ConvertScaleModeToBackgroundPosition(value.value);
				this.ve.style.backgroundPositionY = BackgroundPropertyHelper.ConvertScaleModeToBackgroundPosition(value.value);
				this.ve.style.backgroundRepeat = BackgroundPropertyHelper.ConvertScaleModeToBackgroundRepeat(value.value);
				this.ve.style.backgroundSize = BackgroundPropertyHelper.ConvertScaleModeToBackgroundSize(value.value);
			}
		}

		// Token: 0x04000D95 RID: 3477
		private static StylePropertyReader s_StylePropertyReader = new StylePropertyReader();

		// Token: 0x04000D96 RID: 3478
		private List<StyleValueManaged> m_ValuesManaged;

		// Token: 0x04000D98 RID: 3480
		private bool m_HasInlineCursor;

		// Token: 0x04000D99 RID: 3481
		private StyleCursor m_InlineCursor;

		// Token: 0x04000D9A RID: 3482
		private bool m_HasInlineTextShadow;

		// Token: 0x04000D9B RID: 3483
		private StyleTextShadow m_InlineTextShadow;

		// Token: 0x04000D9C RID: 3484
		private bool m_HasInlineTransformOrigin;

		// Token: 0x04000D9D RID: 3485
		private StyleTransformOrigin m_InlineTransformOrigin;

		// Token: 0x04000D9E RID: 3486
		private bool m_HasInlineTranslate;

		// Token: 0x04000D9F RID: 3487
		private StyleTranslate m_InlineTranslateOperation;

		// Token: 0x04000DA0 RID: 3488
		private bool m_HasInlineRotate;

		// Token: 0x04000DA1 RID: 3489
		private StyleRotate m_InlineRotateOperation;

		// Token: 0x04000DA2 RID: 3490
		private bool m_HasInlineScale;

		// Token: 0x04000DA3 RID: 3491
		private StyleScale m_InlineScale;

		// Token: 0x04000DA4 RID: 3492
		private bool m_HasInlineBackgroundSize;

		// Token: 0x04000DA5 RID: 3493
		public StyleBackgroundSize m_InlineBackgroundSize;

		// Token: 0x04000DA6 RID: 3494
		private InlineStyleAccess.InlineRule m_InlineRule;

		// Token: 0x0200035D RID: 861
		internal struct InlineRule
		{
			// Token: 0x17000561 RID: 1377
			// (get) Token: 0x06001A45 RID: 6725 RVA: 0x0006E412 File Offset: 0x0006C612
			public StyleProperty[] properties
			{
				get
				{
					return this.rule.properties;
				}
			}

			// Token: 0x04000DA7 RID: 3495
			public StyleSheet sheet;

			// Token: 0x04000DA8 RID: 3496
			public StyleRule rule;

			// Token: 0x04000DA9 RID: 3497
			public StylePropertyId[] propertyIds;
		}
	}
}
