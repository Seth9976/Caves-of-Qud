using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x0200035E RID: 862
	internal class InlineStyleAccessPropertyBag : PropertyBag<InlineStyleAccess>, INamedProperties<InlineStyleAccess>
	{
		// Token: 0x06001A46 RID: 6726 RVA: 0x0006E420 File Offset: 0x0006C620
		public InlineStyleAccessPropertyBag()
		{
			this.m_PropertiesList = new List<IProperty<InlineStyleAccess>>(84);
			this.m_PropertiesHash = new Dictionary<string, IProperty<InlineStyleAccess>>(252);
			this.AddProperty<StyleEnum<Align>, Align>(new InlineStyleAccessPropertyBag.AlignContentProperty());
			this.AddProperty<StyleEnum<Align>, Align>(new InlineStyleAccessPropertyBag.AlignItemsProperty());
			this.AddProperty<StyleEnum<Align>, Align>(new InlineStyleAccessPropertyBag.AlignSelfProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.BackgroundColorProperty());
			this.AddProperty<StyleBackground, Background>(new InlineStyleAccessPropertyBag.BackgroundImageProperty());
			this.AddProperty<StyleBackgroundPosition, BackgroundPosition>(new InlineStyleAccessPropertyBag.BackgroundPositionXProperty());
			this.AddProperty<StyleBackgroundPosition, BackgroundPosition>(new InlineStyleAccessPropertyBag.BackgroundPositionYProperty());
			this.AddProperty<StyleBackgroundRepeat, BackgroundRepeat>(new InlineStyleAccessPropertyBag.BackgroundRepeatProperty());
			this.AddProperty<StyleBackgroundSize, BackgroundSize>(new InlineStyleAccessPropertyBag.BackgroundSizeProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.BorderBottomColorProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.BorderBottomLeftRadiusProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.BorderBottomRightRadiusProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.BorderBottomWidthProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.BorderLeftColorProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.BorderLeftWidthProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.BorderRightColorProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.BorderRightWidthProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.BorderTopColorProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.BorderTopLeftRadiusProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.BorderTopRightRadiusProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.BorderTopWidthProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.BottomProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.ColorProperty());
			this.AddProperty<StyleCursor, Cursor>(new InlineStyleAccessPropertyBag.CursorProperty());
			this.AddProperty<StyleEnum<DisplayStyle>, DisplayStyle>(new InlineStyleAccessPropertyBag.DisplayProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.FlexBasisProperty());
			this.AddProperty<StyleEnum<FlexDirection>, FlexDirection>(new InlineStyleAccessPropertyBag.FlexDirectionProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.FlexGrowProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.FlexShrinkProperty());
			this.AddProperty<StyleEnum<Wrap>, Wrap>(new InlineStyleAccessPropertyBag.FlexWrapProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.FontSizeProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.HeightProperty());
			this.AddProperty<StyleEnum<Justify>, Justify>(new InlineStyleAccessPropertyBag.JustifyContentProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.LeftProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.LetterSpacingProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MarginBottomProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MarginLeftProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MarginRightProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MarginTopProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MaxHeightProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MaxWidthProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MinHeightProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.MinWidthProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.OpacityProperty());
			this.AddProperty<StyleEnum<Overflow>, Overflow>(new InlineStyleAccessPropertyBag.OverflowProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.PaddingBottomProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.PaddingLeftProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.PaddingRightProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.PaddingTopProperty());
			this.AddProperty<StyleEnum<Position>, Position>(new InlineStyleAccessPropertyBag.PositionProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.RightProperty());
			this.AddProperty<StyleRotate, Rotate>(new InlineStyleAccessPropertyBag.RotateProperty());
			this.AddProperty<StyleScale, Scale>(new InlineStyleAccessPropertyBag.ScaleProperty());
			this.AddProperty<StyleEnum<TextOverflow>, TextOverflow>(new InlineStyleAccessPropertyBag.TextOverflowProperty());
			this.AddProperty<StyleTextShadow, TextShadow>(new InlineStyleAccessPropertyBag.TextShadowProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.TopProperty());
			this.AddProperty<StyleTransformOrigin, TransformOrigin>(new InlineStyleAccessPropertyBag.TransformOriginProperty());
			this.AddProperty<StyleList<TimeValue>, List<TimeValue>>(new InlineStyleAccessPropertyBag.TransitionDelayProperty());
			this.AddProperty<StyleList<TimeValue>, List<TimeValue>>(new InlineStyleAccessPropertyBag.TransitionDurationProperty());
			this.AddProperty<StyleList<StylePropertyName>, List<StylePropertyName>>(new InlineStyleAccessPropertyBag.TransitionPropertyProperty());
			this.AddProperty<StyleList<EasingFunction>, List<EasingFunction>>(new InlineStyleAccessPropertyBag.TransitionTimingFunctionProperty());
			this.AddProperty<StyleTranslate, Translate>(new InlineStyleAccessPropertyBag.TranslateProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.UnityBackgroundImageTintColorProperty());
			this.AddProperty<StyleEnum<EditorTextRenderingMode>, EditorTextRenderingMode>(new InlineStyleAccessPropertyBag.UnityEditorTextRenderingModeProperty());
			this.AddProperty<StyleFont, Font>(new InlineStyleAccessPropertyBag.UnityFontProperty());
			this.AddProperty<StyleFontDefinition, FontDefinition>(new InlineStyleAccessPropertyBag.UnityFontDefinitionProperty());
			this.AddProperty<StyleEnum<FontStyle>, FontStyle>(new InlineStyleAccessPropertyBag.UnityFontStyleAndWeightProperty());
			this.AddProperty<StyleEnum<OverflowClipBox>, OverflowClipBox>(new InlineStyleAccessPropertyBag.UnityOverflowClipBoxProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.UnityParagraphSpacingProperty());
			this.AddProperty<StyleInt, int>(new InlineStyleAccessPropertyBag.UnitySliceBottomProperty());
			this.AddProperty<StyleInt, int>(new InlineStyleAccessPropertyBag.UnitySliceLeftProperty());
			this.AddProperty<StyleInt, int>(new InlineStyleAccessPropertyBag.UnitySliceRightProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.UnitySliceScaleProperty());
			this.AddProperty<StyleInt, int>(new InlineStyleAccessPropertyBag.UnitySliceTopProperty());
			this.AddProperty<StyleEnum<SliceType>, SliceType>(new InlineStyleAccessPropertyBag.UnitySliceTypeProperty());
			this.AddProperty<StyleEnum<TextAnchor>, TextAnchor>(new InlineStyleAccessPropertyBag.UnityTextAlignProperty());
			this.AddProperty<StyleEnum<TextGeneratorType>, TextGeneratorType>(new InlineStyleAccessPropertyBag.UnityTextGeneratorProperty());
			this.AddProperty<StyleColor, Color>(new InlineStyleAccessPropertyBag.UnityTextOutlineColorProperty());
			this.AddProperty<StyleFloat, float>(new InlineStyleAccessPropertyBag.UnityTextOutlineWidthProperty());
			this.AddProperty<StyleEnum<TextOverflowPosition>, TextOverflowPosition>(new InlineStyleAccessPropertyBag.UnityTextOverflowPositionProperty());
			this.AddProperty<StyleEnum<Visibility>, Visibility>(new InlineStyleAccessPropertyBag.VisibilityProperty());
			this.AddProperty<StyleEnum<WhiteSpace>, WhiteSpace>(new InlineStyleAccessPropertyBag.WhiteSpaceProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.WidthProperty());
			this.AddProperty<StyleLength, Length>(new InlineStyleAccessPropertyBag.WordSpacingProperty());
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0006E844 File Offset: 0x0006CA44
		private void AddProperty<TStyleValue, TValue>(InlineStyleAccessPropertyBag.InlineStyleProperty<TStyleValue, TValue> property) where TStyleValue : IStyleValue<TValue>, new()
		{
			this.m_PropertiesList.Add(property);
			this.m_PropertiesHash.Add(property.Name, property);
			bool flag = string.CompareOrdinal(property.Name, property.ussName) != 0;
			if (flag)
			{
				this.m_PropertiesHash.Add(property.ussName, property);
			}
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0006E89D File Offset: 0x0006CA9D
		public override PropertyCollection<InlineStyleAccess> GetProperties()
		{
			return new PropertyCollection<InlineStyleAccess>(this.m_PropertiesList);
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0006E89D File Offset: 0x0006CA9D
		public override PropertyCollection<InlineStyleAccess> GetProperties(ref InlineStyleAccess container)
		{
			return new PropertyCollection<InlineStyleAccess>(this.m_PropertiesList);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0006E8AA File Offset: 0x0006CAAA
		public bool TryGetProperty(ref InlineStyleAccess container, string name, out IProperty<InlineStyleAccess> property)
		{
			return this.m_PropertiesHash.TryGetValue(name, out property);
		}

		// Token: 0x04000DAA RID: 3498
		private readonly List<IProperty<InlineStyleAccess>> m_PropertiesList;

		// Token: 0x04000DAB RID: 3499
		private readonly Dictionary<string, IProperty<InlineStyleAccess>> m_PropertiesHash;

		// Token: 0x0200035F RID: 863
		private class AlignContentProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Align>
		{
			// Token: 0x17000562 RID: 1378
			// (get) Token: 0x06001A4B RID: 6731 RVA: 0x0006E8B9 File Offset: 0x0006CAB9
			public override string Name
			{
				get
				{
					return "alignContent";
				}
			}

			// Token: 0x17000563 RID: 1379
			// (get) Token: 0x06001A4C RID: 6732 RVA: 0x0006E8C0 File Offset: 0x0006CAC0
			public override string ussName
			{
				get
				{
					return "align-content";
				}
			}

			// Token: 0x17000564 RID: 1380
			// (get) Token: 0x06001A4D RID: 6733 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001A4E RID: 6734 RVA: 0x0006E8C7 File Offset: 0x0006CAC7
			public override StyleEnum<Align> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).alignContent;
			}

			// Token: 0x06001A4F RID: 6735 RVA: 0x0006E8D0 File Offset: 0x0006CAD0
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Align> value)
			{
				((IStyle)container).alignContent = value;
			}
		}

		// Token: 0x02000360 RID: 864
		private class AlignItemsProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Align>
		{
			// Token: 0x17000565 RID: 1381
			// (get) Token: 0x06001A51 RID: 6737 RVA: 0x0006E8E4 File Offset: 0x0006CAE4
			public override string Name
			{
				get
				{
					return "alignItems";
				}
			}

			// Token: 0x17000566 RID: 1382
			// (get) Token: 0x06001A52 RID: 6738 RVA: 0x0006E8EB File Offset: 0x0006CAEB
			public override string ussName
			{
				get
				{
					return "align-items";
				}
			}

			// Token: 0x17000567 RID: 1383
			// (get) Token: 0x06001A53 RID: 6739 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001A54 RID: 6740 RVA: 0x0006E8F2 File Offset: 0x0006CAF2
			public override StyleEnum<Align> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).alignItems;
			}

			// Token: 0x06001A55 RID: 6741 RVA: 0x0006E8FB File Offset: 0x0006CAFB
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Align> value)
			{
				((IStyle)container).alignItems = value;
			}
		}

		// Token: 0x02000361 RID: 865
		private class AlignSelfProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Align>
		{
			// Token: 0x17000568 RID: 1384
			// (get) Token: 0x06001A57 RID: 6743 RVA: 0x0006E906 File Offset: 0x0006CB06
			public override string Name
			{
				get
				{
					return "alignSelf";
				}
			}

			// Token: 0x17000569 RID: 1385
			// (get) Token: 0x06001A58 RID: 6744 RVA: 0x0006E90D File Offset: 0x0006CB0D
			public override string ussName
			{
				get
				{
					return "align-self";
				}
			}

			// Token: 0x1700056A RID: 1386
			// (get) Token: 0x06001A59 RID: 6745 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001A5A RID: 6746 RVA: 0x0006E914 File Offset: 0x0006CB14
			public override StyleEnum<Align> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).alignSelf;
			}

			// Token: 0x06001A5B RID: 6747 RVA: 0x0006E91D File Offset: 0x0006CB1D
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Align> value)
			{
				((IStyle)container).alignSelf = value;
			}
		}

		// Token: 0x02000362 RID: 866
		private class BackgroundColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x1700056B RID: 1387
			// (get) Token: 0x06001A5D RID: 6749 RVA: 0x0006E928 File Offset: 0x0006CB28
			public override string Name
			{
				get
				{
					return "backgroundColor";
				}
			}

			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x06001A5E RID: 6750 RVA: 0x0006E92F File Offset: 0x0006CB2F
			public override string ussName
			{
				get
				{
					return "background-color";
				}
			}

			// Token: 0x1700056D RID: 1389
			// (get) Token: 0x06001A5F RID: 6751 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001A60 RID: 6752 RVA: 0x0006E936 File Offset: 0x0006CB36
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).backgroundColor;
			}

			// Token: 0x06001A61 RID: 6753 RVA: 0x0006E93F File Offset: 0x0006CB3F
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).backgroundColor = value;
			}
		}

		// Token: 0x02000363 RID: 867
		private class BackgroundImageProperty : InlineStyleAccessPropertyBag.InlineStyleBackgroundProperty
		{
			// Token: 0x1700056E RID: 1390
			// (get) Token: 0x06001A63 RID: 6755 RVA: 0x0006E953 File Offset: 0x0006CB53
			public override string Name
			{
				get
				{
					return "backgroundImage";
				}
			}

			// Token: 0x1700056F RID: 1391
			// (get) Token: 0x06001A64 RID: 6756 RVA: 0x0006E95A File Offset: 0x0006CB5A
			public override string ussName
			{
				get
				{
					return "background-image";
				}
			}

			// Token: 0x17000570 RID: 1392
			// (get) Token: 0x06001A65 RID: 6757 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001A66 RID: 6758 RVA: 0x0006E961 File Offset: 0x0006CB61
			public override StyleBackground GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).backgroundImage;
			}

			// Token: 0x06001A67 RID: 6759 RVA: 0x0006E96A File Offset: 0x0006CB6A
			public override void SetValue(ref InlineStyleAccess container, StyleBackground value)
			{
				((IStyle)container).backgroundImage = value;
			}
		}

		// Token: 0x02000364 RID: 868
		private class BackgroundPositionXProperty : InlineStyleAccessPropertyBag.InlineStyleBackgroundPositionProperty
		{
			// Token: 0x17000571 RID: 1393
			// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0006E97E File Offset: 0x0006CB7E
			public override string Name
			{
				get
				{
					return "backgroundPositionX";
				}
			}

			// Token: 0x17000572 RID: 1394
			// (get) Token: 0x06001A6A RID: 6762 RVA: 0x0006E985 File Offset: 0x0006CB85
			public override string ussName
			{
				get
				{
					return "background-position-x";
				}
			}

			// Token: 0x17000573 RID: 1395
			// (get) Token: 0x06001A6B RID: 6763 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001A6C RID: 6764 RVA: 0x0006E98C File Offset: 0x0006CB8C
			public override StyleBackgroundPosition GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).backgroundPositionX;
			}

			// Token: 0x06001A6D RID: 6765 RVA: 0x0006E995 File Offset: 0x0006CB95
			public override void SetValue(ref InlineStyleAccess container, StyleBackgroundPosition value)
			{
				((IStyle)container).backgroundPositionX = value;
			}
		}

		// Token: 0x02000365 RID: 869
		private class BackgroundPositionYProperty : InlineStyleAccessPropertyBag.InlineStyleBackgroundPositionProperty
		{
			// Token: 0x17000574 RID: 1396
			// (get) Token: 0x06001A6F RID: 6767 RVA: 0x0006E9A9 File Offset: 0x0006CBA9
			public override string Name
			{
				get
				{
					return "backgroundPositionY";
				}
			}

			// Token: 0x17000575 RID: 1397
			// (get) Token: 0x06001A70 RID: 6768 RVA: 0x0006E9B0 File Offset: 0x0006CBB0
			public override string ussName
			{
				get
				{
					return "background-position-y";
				}
			}

			// Token: 0x17000576 RID: 1398
			// (get) Token: 0x06001A71 RID: 6769 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001A72 RID: 6770 RVA: 0x0006E9B7 File Offset: 0x0006CBB7
			public override StyleBackgroundPosition GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).backgroundPositionY;
			}

			// Token: 0x06001A73 RID: 6771 RVA: 0x0006E9C0 File Offset: 0x0006CBC0
			public override void SetValue(ref InlineStyleAccess container, StyleBackgroundPosition value)
			{
				((IStyle)container).backgroundPositionY = value;
			}
		}

		// Token: 0x02000366 RID: 870
		private class BackgroundRepeatProperty : InlineStyleAccessPropertyBag.InlineStyleBackgroundRepeatProperty
		{
			// Token: 0x17000577 RID: 1399
			// (get) Token: 0x06001A75 RID: 6773 RVA: 0x0006E9CB File Offset: 0x0006CBCB
			public override string Name
			{
				get
				{
					return "backgroundRepeat";
				}
			}

			// Token: 0x17000578 RID: 1400
			// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0006E9D2 File Offset: 0x0006CBD2
			public override string ussName
			{
				get
				{
					return "background-repeat";
				}
			}

			// Token: 0x17000579 RID: 1401
			// (get) Token: 0x06001A77 RID: 6775 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001A78 RID: 6776 RVA: 0x0006E9D9 File Offset: 0x0006CBD9
			public override StyleBackgroundRepeat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).backgroundRepeat;
			}

			// Token: 0x06001A79 RID: 6777 RVA: 0x0006E9E2 File Offset: 0x0006CBE2
			public override void SetValue(ref InlineStyleAccess container, StyleBackgroundRepeat value)
			{
				((IStyle)container).backgroundRepeat = value;
			}
		}

		// Token: 0x02000367 RID: 871
		private class BackgroundSizeProperty : InlineStyleAccessPropertyBag.InlineStyleBackgroundSizeProperty
		{
			// Token: 0x1700057A RID: 1402
			// (get) Token: 0x06001A7B RID: 6779 RVA: 0x0006E9F6 File Offset: 0x0006CBF6
			public override string Name
			{
				get
				{
					return "backgroundSize";
				}
			}

			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x06001A7C RID: 6780 RVA: 0x0006E9FD File Offset: 0x0006CBFD
			public override string ussName
			{
				get
				{
					return "background-size";
				}
			}

			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x06001A7D RID: 6781 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001A7E RID: 6782 RVA: 0x0006EA04 File Offset: 0x0006CC04
			public override StyleBackgroundSize GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).backgroundSize;
			}

			// Token: 0x06001A7F RID: 6783 RVA: 0x0006EA0D File Offset: 0x0006CC0D
			public override void SetValue(ref InlineStyleAccess container, StyleBackgroundSize value)
			{
				((IStyle)container).backgroundSize = value;
			}
		}

		// Token: 0x02000368 RID: 872
		private class BorderBottomColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x1700057D RID: 1405
			// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0006EA21 File Offset: 0x0006CC21
			public override string Name
			{
				get
				{
					return "borderBottomColor";
				}
			}

			// Token: 0x1700057E RID: 1406
			// (get) Token: 0x06001A82 RID: 6786 RVA: 0x0006EA28 File Offset: 0x0006CC28
			public override string ussName
			{
				get
				{
					return "border-bottom-color";
				}
			}

			// Token: 0x1700057F RID: 1407
			// (get) Token: 0x06001A83 RID: 6787 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001A84 RID: 6788 RVA: 0x0006EA2F File Offset: 0x0006CC2F
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderBottomColor;
			}

			// Token: 0x06001A85 RID: 6789 RVA: 0x0006EA38 File Offset: 0x0006CC38
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).borderBottomColor = value;
			}
		}

		// Token: 0x02000369 RID: 873
		private class BorderBottomLeftRadiusProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x17000580 RID: 1408
			// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0006EA43 File Offset: 0x0006CC43
			public override string Name
			{
				get
				{
					return "borderBottomLeftRadius";
				}
			}

			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x06001A88 RID: 6792 RVA: 0x0006EA4A File Offset: 0x0006CC4A
			public override string ussName
			{
				get
				{
					return "border-bottom-left-radius";
				}
			}

			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x06001A89 RID: 6793 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001A8A RID: 6794 RVA: 0x0006EA51 File Offset: 0x0006CC51
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderBottomLeftRadius;
			}

			// Token: 0x06001A8B RID: 6795 RVA: 0x0006EA5A File Offset: 0x0006CC5A
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).borderBottomLeftRadius = value;
			}
		}

		// Token: 0x0200036A RID: 874
		private class BorderBottomRightRadiusProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x06001A8D RID: 6797 RVA: 0x0006EA6E File Offset: 0x0006CC6E
			public override string Name
			{
				get
				{
					return "borderBottomRightRadius";
				}
			}

			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x06001A8E RID: 6798 RVA: 0x0006EA75 File Offset: 0x0006CC75
			public override string ussName
			{
				get
				{
					return "border-bottom-right-radius";
				}
			}

			// Token: 0x17000585 RID: 1413
			// (get) Token: 0x06001A8F RID: 6799 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001A90 RID: 6800 RVA: 0x0006EA7C File Offset: 0x0006CC7C
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderBottomRightRadius;
			}

			// Token: 0x06001A91 RID: 6801 RVA: 0x0006EA85 File Offset: 0x0006CC85
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).borderBottomRightRadius = value;
			}
		}

		// Token: 0x0200036B RID: 875
		private class BorderBottomWidthProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x17000586 RID: 1414
			// (get) Token: 0x06001A93 RID: 6803 RVA: 0x0006EA90 File Offset: 0x0006CC90
			public override string Name
			{
				get
				{
					return "borderBottomWidth";
				}
			}

			// Token: 0x17000587 RID: 1415
			// (get) Token: 0x06001A94 RID: 6804 RVA: 0x0006EA97 File Offset: 0x0006CC97
			public override string ussName
			{
				get
				{
					return "border-bottom-width";
				}
			}

			// Token: 0x17000588 RID: 1416
			// (get) Token: 0x06001A95 RID: 6805 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001A96 RID: 6806 RVA: 0x0006EA9E File Offset: 0x0006CC9E
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderBottomWidth;
			}

			// Token: 0x06001A97 RID: 6807 RVA: 0x0006EAA7 File Offset: 0x0006CCA7
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).borderBottomWidth = value;
			}
		}

		// Token: 0x0200036C RID: 876
		private class BorderLeftColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x17000589 RID: 1417
			// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0006EABB File Offset: 0x0006CCBB
			public override string Name
			{
				get
				{
					return "borderLeftColor";
				}
			}

			// Token: 0x1700058A RID: 1418
			// (get) Token: 0x06001A9A RID: 6810 RVA: 0x0006EAC2 File Offset: 0x0006CCC2
			public override string ussName
			{
				get
				{
					return "border-left-color";
				}
			}

			// Token: 0x1700058B RID: 1419
			// (get) Token: 0x06001A9B RID: 6811 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001A9C RID: 6812 RVA: 0x0006EAC9 File Offset: 0x0006CCC9
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderLeftColor;
			}

			// Token: 0x06001A9D RID: 6813 RVA: 0x0006EAD2 File Offset: 0x0006CCD2
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).borderLeftColor = value;
			}
		}

		// Token: 0x0200036D RID: 877
		private class BorderLeftWidthProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x1700058C RID: 1420
			// (get) Token: 0x06001A9F RID: 6815 RVA: 0x0006EADD File Offset: 0x0006CCDD
			public override string Name
			{
				get
				{
					return "borderLeftWidth";
				}
			}

			// Token: 0x1700058D RID: 1421
			// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x0006EAE4 File Offset: 0x0006CCE4
			public override string ussName
			{
				get
				{
					return "border-left-width";
				}
			}

			// Token: 0x1700058E RID: 1422
			// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001AA2 RID: 6818 RVA: 0x0006EAEB File Offset: 0x0006CCEB
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderLeftWidth;
			}

			// Token: 0x06001AA3 RID: 6819 RVA: 0x0006EAF4 File Offset: 0x0006CCF4
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).borderLeftWidth = value;
			}
		}

		// Token: 0x0200036E RID: 878
		private class BorderRightColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x1700058F RID: 1423
			// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x0006EAFF File Offset: 0x0006CCFF
			public override string Name
			{
				get
				{
					return "borderRightColor";
				}
			}

			// Token: 0x17000590 RID: 1424
			// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x0006EB06 File Offset: 0x0006CD06
			public override string ussName
			{
				get
				{
					return "border-right-color";
				}
			}

			// Token: 0x17000591 RID: 1425
			// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001AA8 RID: 6824 RVA: 0x0006EB0D File Offset: 0x0006CD0D
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderRightColor;
			}

			// Token: 0x06001AA9 RID: 6825 RVA: 0x0006EB16 File Offset: 0x0006CD16
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).borderRightColor = value;
			}
		}

		// Token: 0x0200036F RID: 879
		private class BorderRightWidthProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x17000592 RID: 1426
			// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0006EB21 File Offset: 0x0006CD21
			public override string Name
			{
				get
				{
					return "borderRightWidth";
				}
			}

			// Token: 0x17000593 RID: 1427
			// (get) Token: 0x06001AAC RID: 6828 RVA: 0x0006EB28 File Offset: 0x0006CD28
			public override string ussName
			{
				get
				{
					return "border-right-width";
				}
			}

			// Token: 0x17000594 RID: 1428
			// (get) Token: 0x06001AAD RID: 6829 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001AAE RID: 6830 RVA: 0x0006EB2F File Offset: 0x0006CD2F
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderRightWidth;
			}

			// Token: 0x06001AAF RID: 6831 RVA: 0x0006EB38 File Offset: 0x0006CD38
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).borderRightWidth = value;
			}
		}

		// Token: 0x02000370 RID: 880
		private class BorderTopColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x17000595 RID: 1429
			// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0006EB43 File Offset: 0x0006CD43
			public override string Name
			{
				get
				{
					return "borderTopColor";
				}
			}

			// Token: 0x17000596 RID: 1430
			// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x0006EB4A File Offset: 0x0006CD4A
			public override string ussName
			{
				get
				{
					return "border-top-color";
				}
			}

			// Token: 0x17000597 RID: 1431
			// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001AB4 RID: 6836 RVA: 0x0006EB51 File Offset: 0x0006CD51
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderTopColor;
			}

			// Token: 0x06001AB5 RID: 6837 RVA: 0x0006EB5A File Offset: 0x0006CD5A
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).borderTopColor = value;
			}
		}

		// Token: 0x02000371 RID: 881
		private class BorderTopLeftRadiusProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x17000598 RID: 1432
			// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0006EB65 File Offset: 0x0006CD65
			public override string Name
			{
				get
				{
					return "borderTopLeftRadius";
				}
			}

			// Token: 0x17000599 RID: 1433
			// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x0006EB6C File Offset: 0x0006CD6C
			public override string ussName
			{
				get
				{
					return "border-top-left-radius";
				}
			}

			// Token: 0x1700059A RID: 1434
			// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001ABA RID: 6842 RVA: 0x0006EB73 File Offset: 0x0006CD73
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderTopLeftRadius;
			}

			// Token: 0x06001ABB RID: 6843 RVA: 0x0006EB7C File Offset: 0x0006CD7C
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).borderTopLeftRadius = value;
			}
		}

		// Token: 0x02000372 RID: 882
		private class BorderTopRightRadiusProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x1700059B RID: 1435
			// (get) Token: 0x06001ABD RID: 6845 RVA: 0x0006EB87 File Offset: 0x0006CD87
			public override string Name
			{
				get
				{
					return "borderTopRightRadius";
				}
			}

			// Token: 0x1700059C RID: 1436
			// (get) Token: 0x06001ABE RID: 6846 RVA: 0x0006EB8E File Offset: 0x0006CD8E
			public override string ussName
			{
				get
				{
					return "border-top-right-radius";
				}
			}

			// Token: 0x1700059D RID: 1437
			// (get) Token: 0x06001ABF RID: 6847 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001AC0 RID: 6848 RVA: 0x0006EB95 File Offset: 0x0006CD95
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderTopRightRadius;
			}

			// Token: 0x06001AC1 RID: 6849 RVA: 0x0006EB9E File Offset: 0x0006CD9E
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).borderTopRightRadius = value;
			}
		}

		// Token: 0x02000373 RID: 883
		private class BorderTopWidthProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x1700059E RID: 1438
			// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x0006EBA9 File Offset: 0x0006CDA9
			public override string Name
			{
				get
				{
					return "borderTopWidth";
				}
			}

			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x0006EBB0 File Offset: 0x0006CDB0
			public override string ussName
			{
				get
				{
					return "border-top-width";
				}
			}

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001AC6 RID: 6854 RVA: 0x0006EBB7 File Offset: 0x0006CDB7
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).borderTopWidth;
			}

			// Token: 0x06001AC7 RID: 6855 RVA: 0x0006EBC0 File Offset: 0x0006CDC0
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).borderTopWidth = value;
			}
		}

		// Token: 0x02000374 RID: 884
		private class BottomProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005A1 RID: 1441
			// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x0006EBCB File Offset: 0x0006CDCB
			public override string Name
			{
				get
				{
					return "bottom";
				}
			}

			// Token: 0x170005A2 RID: 1442
			// (get) Token: 0x06001ACA RID: 6858 RVA: 0x0006EBCB File Offset: 0x0006CDCB
			public override string ussName
			{
				get
				{
					return "bottom";
				}
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x06001ACB RID: 6859 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001ACC RID: 6860 RVA: 0x0006EBD2 File Offset: 0x0006CDD2
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).bottom;
			}

			// Token: 0x06001ACD RID: 6861 RVA: 0x0006EBDB File Offset: 0x0006CDDB
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).bottom = value;
			}
		}

		// Token: 0x02000375 RID: 885
		private class ColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0006EBE6 File Offset: 0x0006CDE6
			public override string Name
			{
				get
				{
					return "color";
				}
			}

			// Token: 0x170005A5 RID: 1445
			// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x0006EBE6 File Offset: 0x0006CDE6
			public override string ussName
			{
				get
				{
					return "color";
				}
			}

			// Token: 0x170005A6 RID: 1446
			// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001AD2 RID: 6866 RVA: 0x0006EBED File Offset: 0x0006CDED
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).color;
			}

			// Token: 0x06001AD3 RID: 6867 RVA: 0x0006EBF6 File Offset: 0x0006CDF6
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).color = value;
			}
		}

		// Token: 0x02000376 RID: 886
		private class CursorProperty : InlineStyleAccessPropertyBag.InlineStyleCursorProperty
		{
			// Token: 0x170005A7 RID: 1447
			// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x0006EC01 File Offset: 0x0006CE01
			public override string Name
			{
				get
				{
					return "cursor";
				}
			}

			// Token: 0x170005A8 RID: 1448
			// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x0006EC01 File Offset: 0x0006CE01
			public override string ussName
			{
				get
				{
					return "cursor";
				}
			}

			// Token: 0x170005A9 RID: 1449
			// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001AD8 RID: 6872 RVA: 0x0006EC08 File Offset: 0x0006CE08
			public override StyleCursor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).cursor;
			}

			// Token: 0x06001AD9 RID: 6873 RVA: 0x0006EC11 File Offset: 0x0006CE11
			public override void SetValue(ref InlineStyleAccess container, StyleCursor value)
			{
				((IStyle)container).cursor = value;
			}
		}

		// Token: 0x02000377 RID: 887
		private class DisplayProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<DisplayStyle>
		{
			// Token: 0x170005AA RID: 1450
			// (get) Token: 0x06001ADB RID: 6875 RVA: 0x0006EC25 File Offset: 0x0006CE25
			public override string Name
			{
				get
				{
					return "display";
				}
			}

			// Token: 0x170005AB RID: 1451
			// (get) Token: 0x06001ADC RID: 6876 RVA: 0x0006EC25 File Offset: 0x0006CE25
			public override string ussName
			{
				get
				{
					return "display";
				}
			}

			// Token: 0x170005AC RID: 1452
			// (get) Token: 0x06001ADD RID: 6877 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001ADE RID: 6878 RVA: 0x0006EC2C File Offset: 0x0006CE2C
			public override StyleEnum<DisplayStyle> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).display;
			}

			// Token: 0x06001ADF RID: 6879 RVA: 0x0006EC35 File Offset: 0x0006CE35
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<DisplayStyle> value)
			{
				((IStyle)container).display = value;
			}
		}

		// Token: 0x02000378 RID: 888
		private class FlexBasisProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005AD RID: 1453
			// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x0006EC49 File Offset: 0x0006CE49
			public override string Name
			{
				get
				{
					return "flexBasis";
				}
			}

			// Token: 0x170005AE RID: 1454
			// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x0006EC50 File Offset: 0x0006CE50
			public override string ussName
			{
				get
				{
					return "flex-basis";
				}
			}

			// Token: 0x170005AF RID: 1455
			// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001AE4 RID: 6884 RVA: 0x0006EC57 File Offset: 0x0006CE57
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).flexBasis;
			}

			// Token: 0x06001AE5 RID: 6885 RVA: 0x0006EC60 File Offset: 0x0006CE60
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).flexBasis = value;
			}
		}

		// Token: 0x02000379 RID: 889
		private class FlexDirectionProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<FlexDirection>
		{
			// Token: 0x170005B0 RID: 1456
			// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x0006EC6B File Offset: 0x0006CE6B
			public override string Name
			{
				get
				{
					return "flexDirection";
				}
			}

			// Token: 0x170005B1 RID: 1457
			// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x0006EC72 File Offset: 0x0006CE72
			public override string ussName
			{
				get
				{
					return "flex-direction";
				}
			}

			// Token: 0x170005B2 RID: 1458
			// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001AEA RID: 6890 RVA: 0x0006EC79 File Offset: 0x0006CE79
			public override StyleEnum<FlexDirection> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).flexDirection;
			}

			// Token: 0x06001AEB RID: 6891 RVA: 0x0006EC82 File Offset: 0x0006CE82
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<FlexDirection> value)
			{
				((IStyle)container).flexDirection = value;
			}
		}

		// Token: 0x0200037A RID: 890
		private class FlexGrowProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x170005B3 RID: 1459
			// (get) Token: 0x06001AED RID: 6893 RVA: 0x0006EC96 File Offset: 0x0006CE96
			public override string Name
			{
				get
				{
					return "flexGrow";
				}
			}

			// Token: 0x170005B4 RID: 1460
			// (get) Token: 0x06001AEE RID: 6894 RVA: 0x0006EC9D File Offset: 0x0006CE9D
			public override string ussName
			{
				get
				{
					return "flex-grow";
				}
			}

			// Token: 0x170005B5 RID: 1461
			// (get) Token: 0x06001AEF RID: 6895 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001AF0 RID: 6896 RVA: 0x0006ECA4 File Offset: 0x0006CEA4
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).flexGrow;
			}

			// Token: 0x06001AF1 RID: 6897 RVA: 0x0006ECAD File Offset: 0x0006CEAD
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).flexGrow = value;
			}
		}

		// Token: 0x0200037B RID: 891
		private class FlexShrinkProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x170005B6 RID: 1462
			// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0006ECB8 File Offset: 0x0006CEB8
			public override string Name
			{
				get
				{
					return "flexShrink";
				}
			}

			// Token: 0x170005B7 RID: 1463
			// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x0006ECBF File Offset: 0x0006CEBF
			public override string ussName
			{
				get
				{
					return "flex-shrink";
				}
			}

			// Token: 0x170005B8 RID: 1464
			// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001AF6 RID: 6902 RVA: 0x0006ECC6 File Offset: 0x0006CEC6
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).flexShrink;
			}

			// Token: 0x06001AF7 RID: 6903 RVA: 0x0006ECCF File Offset: 0x0006CECF
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).flexShrink = value;
			}
		}

		// Token: 0x0200037C RID: 892
		private class FlexWrapProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Wrap>
		{
			// Token: 0x170005B9 RID: 1465
			// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x0006ECDA File Offset: 0x0006CEDA
			public override string Name
			{
				get
				{
					return "flexWrap";
				}
			}

			// Token: 0x170005BA RID: 1466
			// (get) Token: 0x06001AFA RID: 6906 RVA: 0x0006ECE1 File Offset: 0x0006CEE1
			public override string ussName
			{
				get
				{
					return "flex-wrap";
				}
			}

			// Token: 0x170005BB RID: 1467
			// (get) Token: 0x06001AFB RID: 6907 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001AFC RID: 6908 RVA: 0x0006ECE8 File Offset: 0x0006CEE8
			public override StyleEnum<Wrap> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).flexWrap;
			}

			// Token: 0x06001AFD RID: 6909 RVA: 0x0006ECF1 File Offset: 0x0006CEF1
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Wrap> value)
			{
				((IStyle)container).flexWrap = value;
			}
		}

		// Token: 0x0200037D RID: 893
		private class FontSizeProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005BC RID: 1468
			// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0006ED05 File Offset: 0x0006CF05
			public override string Name
			{
				get
				{
					return "fontSize";
				}
			}

			// Token: 0x170005BD RID: 1469
			// (get) Token: 0x06001B00 RID: 6912 RVA: 0x0006ED0C File Offset: 0x0006CF0C
			public override string ussName
			{
				get
				{
					return "font-size";
				}
			}

			// Token: 0x170005BE RID: 1470
			// (get) Token: 0x06001B01 RID: 6913 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B02 RID: 6914 RVA: 0x0006ED13 File Offset: 0x0006CF13
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).fontSize;
			}

			// Token: 0x06001B03 RID: 6915 RVA: 0x0006ED1C File Offset: 0x0006CF1C
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).fontSize = value;
			}
		}

		// Token: 0x0200037E RID: 894
		private class HeightProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005BF RID: 1471
			// (get) Token: 0x06001B05 RID: 6917 RVA: 0x0006ED27 File Offset: 0x0006CF27
			public override string Name
			{
				get
				{
					return "height";
				}
			}

			// Token: 0x170005C0 RID: 1472
			// (get) Token: 0x06001B06 RID: 6918 RVA: 0x0006ED27 File Offset: 0x0006CF27
			public override string ussName
			{
				get
				{
					return "height";
				}
			}

			// Token: 0x170005C1 RID: 1473
			// (get) Token: 0x06001B07 RID: 6919 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B08 RID: 6920 RVA: 0x0006ED2E File Offset: 0x0006CF2E
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).height;
			}

			// Token: 0x06001B09 RID: 6921 RVA: 0x0006ED37 File Offset: 0x0006CF37
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).height = value;
			}
		}

		// Token: 0x0200037F RID: 895
		private class JustifyContentProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Justify>
		{
			// Token: 0x170005C2 RID: 1474
			// (get) Token: 0x06001B0B RID: 6923 RVA: 0x0006ED42 File Offset: 0x0006CF42
			public override string Name
			{
				get
				{
					return "justifyContent";
				}
			}

			// Token: 0x170005C3 RID: 1475
			// (get) Token: 0x06001B0C RID: 6924 RVA: 0x0006ED49 File Offset: 0x0006CF49
			public override string ussName
			{
				get
				{
					return "justify-content";
				}
			}

			// Token: 0x170005C4 RID: 1476
			// (get) Token: 0x06001B0D RID: 6925 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B0E RID: 6926 RVA: 0x0006ED50 File Offset: 0x0006CF50
			public override StyleEnum<Justify> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).justifyContent;
			}

			// Token: 0x06001B0F RID: 6927 RVA: 0x0006ED59 File Offset: 0x0006CF59
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Justify> value)
			{
				((IStyle)container).justifyContent = value;
			}
		}

		// Token: 0x02000380 RID: 896
		private class LeftProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005C5 RID: 1477
			// (get) Token: 0x06001B11 RID: 6929 RVA: 0x0006ED6D File Offset: 0x0006CF6D
			public override string Name
			{
				get
				{
					return "left";
				}
			}

			// Token: 0x170005C6 RID: 1478
			// (get) Token: 0x06001B12 RID: 6930 RVA: 0x0006ED6D File Offset: 0x0006CF6D
			public override string ussName
			{
				get
				{
					return "left";
				}
			}

			// Token: 0x170005C7 RID: 1479
			// (get) Token: 0x06001B13 RID: 6931 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B14 RID: 6932 RVA: 0x0006ED74 File Offset: 0x0006CF74
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).left;
			}

			// Token: 0x06001B15 RID: 6933 RVA: 0x0006ED7D File Offset: 0x0006CF7D
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).left = value;
			}
		}

		// Token: 0x02000381 RID: 897
		private class LetterSpacingProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005C8 RID: 1480
			// (get) Token: 0x06001B17 RID: 6935 RVA: 0x0006ED88 File Offset: 0x0006CF88
			public override string Name
			{
				get
				{
					return "letterSpacing";
				}
			}

			// Token: 0x170005C9 RID: 1481
			// (get) Token: 0x06001B18 RID: 6936 RVA: 0x0006ED8F File Offset: 0x0006CF8F
			public override string ussName
			{
				get
				{
					return "letter-spacing";
				}
			}

			// Token: 0x170005CA RID: 1482
			// (get) Token: 0x06001B19 RID: 6937 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B1A RID: 6938 RVA: 0x0006ED96 File Offset: 0x0006CF96
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).letterSpacing;
			}

			// Token: 0x06001B1B RID: 6939 RVA: 0x0006ED9F File Offset: 0x0006CF9F
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).letterSpacing = value;
			}
		}

		// Token: 0x02000382 RID: 898
		private class MarginBottomProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005CB RID: 1483
			// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0006EDAA File Offset: 0x0006CFAA
			public override string Name
			{
				get
				{
					return "marginBottom";
				}
			}

			// Token: 0x170005CC RID: 1484
			// (get) Token: 0x06001B1E RID: 6942 RVA: 0x0006EDB1 File Offset: 0x0006CFB1
			public override string ussName
			{
				get
				{
					return "margin-bottom";
				}
			}

			// Token: 0x170005CD RID: 1485
			// (get) Token: 0x06001B1F RID: 6943 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B20 RID: 6944 RVA: 0x0006EDB8 File Offset: 0x0006CFB8
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).marginBottom;
			}

			// Token: 0x06001B21 RID: 6945 RVA: 0x0006EDC1 File Offset: 0x0006CFC1
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).marginBottom = value;
			}
		}

		// Token: 0x02000383 RID: 899
		private class MarginLeftProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005CE RID: 1486
			// (get) Token: 0x06001B23 RID: 6947 RVA: 0x0006EDCC File Offset: 0x0006CFCC
			public override string Name
			{
				get
				{
					return "marginLeft";
				}
			}

			// Token: 0x170005CF RID: 1487
			// (get) Token: 0x06001B24 RID: 6948 RVA: 0x0006EDD3 File Offset: 0x0006CFD3
			public override string ussName
			{
				get
				{
					return "margin-left";
				}
			}

			// Token: 0x170005D0 RID: 1488
			// (get) Token: 0x06001B25 RID: 6949 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B26 RID: 6950 RVA: 0x0006EDDA File Offset: 0x0006CFDA
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).marginLeft;
			}

			// Token: 0x06001B27 RID: 6951 RVA: 0x0006EDE3 File Offset: 0x0006CFE3
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).marginLeft = value;
			}
		}

		// Token: 0x02000384 RID: 900
		private class MarginRightProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005D1 RID: 1489
			// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0006EDEE File Offset: 0x0006CFEE
			public override string Name
			{
				get
				{
					return "marginRight";
				}
			}

			// Token: 0x170005D2 RID: 1490
			// (get) Token: 0x06001B2A RID: 6954 RVA: 0x0006EDF5 File Offset: 0x0006CFF5
			public override string ussName
			{
				get
				{
					return "margin-right";
				}
			}

			// Token: 0x170005D3 RID: 1491
			// (get) Token: 0x06001B2B RID: 6955 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B2C RID: 6956 RVA: 0x0006EDFC File Offset: 0x0006CFFC
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).marginRight;
			}

			// Token: 0x06001B2D RID: 6957 RVA: 0x0006EE05 File Offset: 0x0006D005
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).marginRight = value;
			}
		}

		// Token: 0x02000385 RID: 901
		private class MarginTopProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005D4 RID: 1492
			// (get) Token: 0x06001B2F RID: 6959 RVA: 0x0006EE10 File Offset: 0x0006D010
			public override string Name
			{
				get
				{
					return "marginTop";
				}
			}

			// Token: 0x170005D5 RID: 1493
			// (get) Token: 0x06001B30 RID: 6960 RVA: 0x0006EE17 File Offset: 0x0006D017
			public override string ussName
			{
				get
				{
					return "margin-top";
				}
			}

			// Token: 0x170005D6 RID: 1494
			// (get) Token: 0x06001B31 RID: 6961 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B32 RID: 6962 RVA: 0x0006EE1E File Offset: 0x0006D01E
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).marginTop;
			}

			// Token: 0x06001B33 RID: 6963 RVA: 0x0006EE27 File Offset: 0x0006D027
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).marginTop = value;
			}
		}

		// Token: 0x02000386 RID: 902
		private class MaxHeightProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005D7 RID: 1495
			// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0006EE32 File Offset: 0x0006D032
			public override string Name
			{
				get
				{
					return "maxHeight";
				}
			}

			// Token: 0x170005D8 RID: 1496
			// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0006EE39 File Offset: 0x0006D039
			public override string ussName
			{
				get
				{
					return "max-height";
				}
			}

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x06001B37 RID: 6967 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B38 RID: 6968 RVA: 0x0006EE40 File Offset: 0x0006D040
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).maxHeight;
			}

			// Token: 0x06001B39 RID: 6969 RVA: 0x0006EE49 File Offset: 0x0006D049
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).maxHeight = value;
			}
		}

		// Token: 0x02000387 RID: 903
		private class MaxWidthProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x06001B3B RID: 6971 RVA: 0x0006EE54 File Offset: 0x0006D054
			public override string Name
			{
				get
				{
					return "maxWidth";
				}
			}

			// Token: 0x170005DB RID: 1499
			// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0006EE5B File Offset: 0x0006D05B
			public override string ussName
			{
				get
				{
					return "max-width";
				}
			}

			// Token: 0x170005DC RID: 1500
			// (get) Token: 0x06001B3D RID: 6973 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B3E RID: 6974 RVA: 0x0006EE62 File Offset: 0x0006D062
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).maxWidth;
			}

			// Token: 0x06001B3F RID: 6975 RVA: 0x0006EE6B File Offset: 0x0006D06B
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).maxWidth = value;
			}
		}

		// Token: 0x02000388 RID: 904
		private class MinHeightProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x06001B41 RID: 6977 RVA: 0x0006EE76 File Offset: 0x0006D076
			public override string Name
			{
				get
				{
					return "minHeight";
				}
			}

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x06001B42 RID: 6978 RVA: 0x0006EE7D File Offset: 0x0006D07D
			public override string ussName
			{
				get
				{
					return "min-height";
				}
			}

			// Token: 0x170005DF RID: 1503
			// (get) Token: 0x06001B43 RID: 6979 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B44 RID: 6980 RVA: 0x0006EE84 File Offset: 0x0006D084
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).minHeight;
			}

			// Token: 0x06001B45 RID: 6981 RVA: 0x0006EE8D File Offset: 0x0006D08D
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).minHeight = value;
			}
		}

		// Token: 0x02000389 RID: 905
		private class MinWidthProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005E0 RID: 1504
			// (get) Token: 0x06001B47 RID: 6983 RVA: 0x0006EE98 File Offset: 0x0006D098
			public override string Name
			{
				get
				{
					return "minWidth";
				}
			}

			// Token: 0x170005E1 RID: 1505
			// (get) Token: 0x06001B48 RID: 6984 RVA: 0x0006EE9F File Offset: 0x0006D09F
			public override string ussName
			{
				get
				{
					return "min-width";
				}
			}

			// Token: 0x170005E2 RID: 1506
			// (get) Token: 0x06001B49 RID: 6985 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B4A RID: 6986 RVA: 0x0006EEA6 File Offset: 0x0006D0A6
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).minWidth;
			}

			// Token: 0x06001B4B RID: 6987 RVA: 0x0006EEAF File Offset: 0x0006D0AF
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).minWidth = value;
			}
		}

		// Token: 0x0200038A RID: 906
		private class OpacityProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0006EEBA File Offset: 0x0006D0BA
			public override string Name
			{
				get
				{
					return "opacity";
				}
			}

			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x06001B4E RID: 6990 RVA: 0x0006EEBA File Offset: 0x0006D0BA
			public override string ussName
			{
				get
				{
					return "opacity";
				}
			}

			// Token: 0x170005E5 RID: 1509
			// (get) Token: 0x06001B4F RID: 6991 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B50 RID: 6992 RVA: 0x0006EEC1 File Offset: 0x0006D0C1
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).opacity;
			}

			// Token: 0x06001B51 RID: 6993 RVA: 0x0006EECA File Offset: 0x0006D0CA
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).opacity = value;
			}
		}

		// Token: 0x0200038B RID: 907
		private class OverflowProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Overflow>
		{
			// Token: 0x170005E6 RID: 1510
			// (get) Token: 0x06001B53 RID: 6995 RVA: 0x0006EED5 File Offset: 0x0006D0D5
			public override string Name
			{
				get
				{
					return "overflow";
				}
			}

			// Token: 0x170005E7 RID: 1511
			// (get) Token: 0x06001B54 RID: 6996 RVA: 0x0006EED5 File Offset: 0x0006D0D5
			public override string ussName
			{
				get
				{
					return "overflow";
				}
			}

			// Token: 0x170005E8 RID: 1512
			// (get) Token: 0x06001B55 RID: 6997 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B56 RID: 6998 RVA: 0x0006EEDC File Offset: 0x0006D0DC
			public override StyleEnum<Overflow> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).overflow;
			}

			// Token: 0x06001B57 RID: 6999 RVA: 0x0006EEE5 File Offset: 0x0006D0E5
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Overflow> value)
			{
				((IStyle)container).overflow = value;
			}
		}

		// Token: 0x0200038C RID: 908
		private class PaddingBottomProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x06001B59 RID: 7001 RVA: 0x0006EEF9 File Offset: 0x0006D0F9
			public override string Name
			{
				get
				{
					return "paddingBottom";
				}
			}

			// Token: 0x170005EA RID: 1514
			// (get) Token: 0x06001B5A RID: 7002 RVA: 0x0006EF00 File Offset: 0x0006D100
			public override string ussName
			{
				get
				{
					return "padding-bottom";
				}
			}

			// Token: 0x170005EB RID: 1515
			// (get) Token: 0x06001B5B RID: 7003 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B5C RID: 7004 RVA: 0x0006EF07 File Offset: 0x0006D107
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).paddingBottom;
			}

			// Token: 0x06001B5D RID: 7005 RVA: 0x0006EF10 File Offset: 0x0006D110
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).paddingBottom = value;
			}
		}

		// Token: 0x0200038D RID: 909
		private class PaddingLeftProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005EC RID: 1516
			// (get) Token: 0x06001B5F RID: 7007 RVA: 0x0006EF1B File Offset: 0x0006D11B
			public override string Name
			{
				get
				{
					return "paddingLeft";
				}
			}

			// Token: 0x170005ED RID: 1517
			// (get) Token: 0x06001B60 RID: 7008 RVA: 0x0006EF22 File Offset: 0x0006D122
			public override string ussName
			{
				get
				{
					return "padding-left";
				}
			}

			// Token: 0x170005EE RID: 1518
			// (get) Token: 0x06001B61 RID: 7009 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B62 RID: 7010 RVA: 0x0006EF29 File Offset: 0x0006D129
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).paddingLeft;
			}

			// Token: 0x06001B63 RID: 7011 RVA: 0x0006EF32 File Offset: 0x0006D132
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).paddingLeft = value;
			}
		}

		// Token: 0x0200038E RID: 910
		private class PaddingRightProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x06001B65 RID: 7013 RVA: 0x0006EF3D File Offset: 0x0006D13D
			public override string Name
			{
				get
				{
					return "paddingRight";
				}
			}

			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0006EF44 File Offset: 0x0006D144
			public override string ussName
			{
				get
				{
					return "padding-right";
				}
			}

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x06001B67 RID: 7015 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B68 RID: 7016 RVA: 0x0006EF4B File Offset: 0x0006D14B
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).paddingRight;
			}

			// Token: 0x06001B69 RID: 7017 RVA: 0x0006EF54 File Offset: 0x0006D154
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).paddingRight = value;
			}
		}

		// Token: 0x0200038F RID: 911
		private class PaddingTopProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005F2 RID: 1522
			// (get) Token: 0x06001B6B RID: 7019 RVA: 0x0006EF5F File Offset: 0x0006D15F
			public override string Name
			{
				get
				{
					return "paddingTop";
				}
			}

			// Token: 0x170005F3 RID: 1523
			// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0006EF66 File Offset: 0x0006D166
			public override string ussName
			{
				get
				{
					return "padding-top";
				}
			}

			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x06001B6D RID: 7021 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B6E RID: 7022 RVA: 0x0006EF6D File Offset: 0x0006D16D
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).paddingTop;
			}

			// Token: 0x06001B6F RID: 7023 RVA: 0x0006EF76 File Offset: 0x0006D176
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).paddingTop = value;
			}
		}

		// Token: 0x02000390 RID: 912
		private class PositionProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Position>
		{
			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x06001B71 RID: 7025 RVA: 0x0006EF81 File Offset: 0x0006D181
			public override string Name
			{
				get
				{
					return "position";
				}
			}

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x06001B72 RID: 7026 RVA: 0x0006EF81 File Offset: 0x0006D181
			public override string ussName
			{
				get
				{
					return "position";
				}
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x06001B73 RID: 7027 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B74 RID: 7028 RVA: 0x0006EF88 File Offset: 0x0006D188
			public override StyleEnum<Position> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).position;
			}

			// Token: 0x06001B75 RID: 7029 RVA: 0x0006EF91 File Offset: 0x0006D191
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Position> value)
			{
				((IStyle)container).position = value;
			}
		}

		// Token: 0x02000391 RID: 913
		private class RightProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0006EFA5 File Offset: 0x0006D1A5
			public override string Name
			{
				get
				{
					return "right";
				}
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06001B78 RID: 7032 RVA: 0x0006EFA5 File Offset: 0x0006D1A5
			public override string ussName
			{
				get
				{
					return "right";
				}
			}

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x06001B79 RID: 7033 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B7A RID: 7034 RVA: 0x0006EFAC File Offset: 0x0006D1AC
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).right;
			}

			// Token: 0x06001B7B RID: 7035 RVA: 0x0006EFB5 File Offset: 0x0006D1B5
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).right = value;
			}
		}

		// Token: 0x02000392 RID: 914
		private class RotateProperty : InlineStyleAccessPropertyBag.InlineStyleRotateProperty
		{
			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0006EFC0 File Offset: 0x0006D1C0
			public override string Name
			{
				get
				{
					return "rotate";
				}
			}

			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x06001B7E RID: 7038 RVA: 0x0006EFC0 File Offset: 0x0006D1C0
			public override string ussName
			{
				get
				{
					return "rotate";
				}
			}

			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x06001B7F RID: 7039 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B80 RID: 7040 RVA: 0x0006EFC7 File Offset: 0x0006D1C7
			public override StyleRotate GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).rotate;
			}

			// Token: 0x06001B81 RID: 7041 RVA: 0x0006EFD0 File Offset: 0x0006D1D0
			public override void SetValue(ref InlineStyleAccess container, StyleRotate value)
			{
				((IStyle)container).rotate = value;
			}
		}

		// Token: 0x02000393 RID: 915
		private class ScaleProperty : InlineStyleAccessPropertyBag.InlineStyleScaleProperty
		{
			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x06001B83 RID: 7043 RVA: 0x0006EFE4 File Offset: 0x0006D1E4
			public override string Name
			{
				get
				{
					return "scale";
				}
			}

			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x06001B84 RID: 7044 RVA: 0x0006EFE4 File Offset: 0x0006D1E4
			public override string ussName
			{
				get
				{
					return "scale";
				}
			}

			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x06001B85 RID: 7045 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B86 RID: 7046 RVA: 0x0006EFEB File Offset: 0x0006D1EB
			public override StyleScale GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).scale;
			}

			// Token: 0x06001B87 RID: 7047 RVA: 0x0006EFF4 File Offset: 0x0006D1F4
			public override void SetValue(ref InlineStyleAccess container, StyleScale value)
			{
				((IStyle)container).scale = value;
			}
		}

		// Token: 0x02000394 RID: 916
		private class TextOverflowProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<TextOverflow>
		{
			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x06001B89 RID: 7049 RVA: 0x0006F008 File Offset: 0x0006D208
			public override string Name
			{
				get
				{
					return "textOverflow";
				}
			}

			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x06001B8A RID: 7050 RVA: 0x0006F00F File Offset: 0x0006D20F
			public override string ussName
			{
				get
				{
					return "text-overflow";
				}
			}

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B8C RID: 7052 RVA: 0x0006F016 File Offset: 0x0006D216
			public override StyleEnum<TextOverflow> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).textOverflow;
			}

			// Token: 0x06001B8D RID: 7053 RVA: 0x0006F01F File Offset: 0x0006D21F
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<TextOverflow> value)
			{
				((IStyle)container).textOverflow = value;
			}
		}

		// Token: 0x02000395 RID: 917
		private class TextShadowProperty : InlineStyleAccessPropertyBag.InlineStyleTextShadowProperty
		{
			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x06001B8F RID: 7055 RVA: 0x0006F033 File Offset: 0x0006D233
			public override string Name
			{
				get
				{
					return "textShadow";
				}
			}

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x06001B90 RID: 7056 RVA: 0x0006F03A File Offset: 0x0006D23A
			public override string ussName
			{
				get
				{
					return "text-shadow";
				}
			}

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x06001B91 RID: 7057 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B92 RID: 7058 RVA: 0x0006F041 File Offset: 0x0006D241
			public override StyleTextShadow GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).textShadow;
			}

			// Token: 0x06001B93 RID: 7059 RVA: 0x0006F04A File Offset: 0x0006D24A
			public override void SetValue(ref InlineStyleAccess container, StyleTextShadow value)
			{
				((IStyle)container).textShadow = value;
			}
		}

		// Token: 0x02000396 RID: 918
		private class TopProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x06001B95 RID: 7061 RVA: 0x0006F05E File Offset: 0x0006D25E
			public override string Name
			{
				get
				{
					return "top";
				}
			}

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x06001B96 RID: 7062 RVA: 0x0006F05E File Offset: 0x0006D25E
			public override string ussName
			{
				get
				{
					return "top";
				}
			}

			// Token: 0x17000609 RID: 1545
			// (get) Token: 0x06001B97 RID: 7063 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B98 RID: 7064 RVA: 0x0006F065 File Offset: 0x0006D265
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).top;
			}

			// Token: 0x06001B99 RID: 7065 RVA: 0x0006F06E File Offset: 0x0006D26E
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).top = value;
			}
		}

		// Token: 0x02000397 RID: 919
		private class TransformOriginProperty : InlineStyleAccessPropertyBag.InlineStyleTransformOriginProperty
		{
			// Token: 0x1700060A RID: 1546
			// (get) Token: 0x06001B9B RID: 7067 RVA: 0x0006F079 File Offset: 0x0006D279
			public override string Name
			{
				get
				{
					return "transformOrigin";
				}
			}

			// Token: 0x1700060B RID: 1547
			// (get) Token: 0x06001B9C RID: 7068 RVA: 0x0006F080 File Offset: 0x0006D280
			public override string ussName
			{
				get
				{
					return "transform-origin";
				}
			}

			// Token: 0x1700060C RID: 1548
			// (get) Token: 0x06001B9D RID: 7069 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001B9E RID: 7070 RVA: 0x0006F087 File Offset: 0x0006D287
			public override StyleTransformOrigin GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).transformOrigin;
			}

			// Token: 0x06001B9F RID: 7071 RVA: 0x0006F090 File Offset: 0x0006D290
			public override void SetValue(ref InlineStyleAccess container, StyleTransformOrigin value)
			{
				((IStyle)container).transformOrigin = value;
			}
		}

		// Token: 0x02000398 RID: 920
		private class TransitionDelayProperty : InlineStyleAccessPropertyBag.InlineStyleListProperty<TimeValue>
		{
			// Token: 0x1700060D RID: 1549
			// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x0006F0A4 File Offset: 0x0006D2A4
			public override string Name
			{
				get
				{
					return "transitionDelay";
				}
			}

			// Token: 0x1700060E RID: 1550
			// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x0006F0AB File Offset: 0x0006D2AB
			public override string ussName
			{
				get
				{
					return "transition-delay";
				}
			}

			// Token: 0x1700060F RID: 1551
			// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BA4 RID: 7076 RVA: 0x0006F0B2 File Offset: 0x0006D2B2
			public override StyleList<TimeValue> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).transitionDelay;
			}

			// Token: 0x06001BA5 RID: 7077 RVA: 0x0006F0BB File Offset: 0x0006D2BB
			public override void SetValue(ref InlineStyleAccess container, StyleList<TimeValue> value)
			{
				((IStyle)container).transitionDelay = value;
			}
		}

		// Token: 0x02000399 RID: 921
		private class TransitionDurationProperty : InlineStyleAccessPropertyBag.InlineStyleListProperty<TimeValue>
		{
			// Token: 0x17000610 RID: 1552
			// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x0006F0CF File Offset: 0x0006D2CF
			public override string Name
			{
				get
				{
					return "transitionDuration";
				}
			}

			// Token: 0x17000611 RID: 1553
			// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x0006F0D6 File Offset: 0x0006D2D6
			public override string ussName
			{
				get
				{
					return "transition-duration";
				}
			}

			// Token: 0x17000612 RID: 1554
			// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BAA RID: 7082 RVA: 0x0006F0DD File Offset: 0x0006D2DD
			public override StyleList<TimeValue> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).transitionDuration;
			}

			// Token: 0x06001BAB RID: 7083 RVA: 0x0006F0E6 File Offset: 0x0006D2E6
			public override void SetValue(ref InlineStyleAccess container, StyleList<TimeValue> value)
			{
				((IStyle)container).transitionDuration = value;
			}
		}

		// Token: 0x0200039A RID: 922
		private class TransitionPropertyProperty : InlineStyleAccessPropertyBag.InlineStyleListProperty<StylePropertyName>
		{
			// Token: 0x17000613 RID: 1555
			// (get) Token: 0x06001BAD RID: 7085 RVA: 0x0006F0F1 File Offset: 0x0006D2F1
			public override string Name
			{
				get
				{
					return "transitionProperty";
				}
			}

			// Token: 0x17000614 RID: 1556
			// (get) Token: 0x06001BAE RID: 7086 RVA: 0x0006F0F8 File Offset: 0x0006D2F8
			public override string ussName
			{
				get
				{
					return "transition-property";
				}
			}

			// Token: 0x17000615 RID: 1557
			// (get) Token: 0x06001BAF RID: 7087 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BB0 RID: 7088 RVA: 0x0006F0FF File Offset: 0x0006D2FF
			public override StyleList<StylePropertyName> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).transitionProperty;
			}

			// Token: 0x06001BB1 RID: 7089 RVA: 0x0006F108 File Offset: 0x0006D308
			public override void SetValue(ref InlineStyleAccess container, StyleList<StylePropertyName> value)
			{
				((IStyle)container).transitionProperty = value;
			}
		}

		// Token: 0x0200039B RID: 923
		private class TransitionTimingFunctionProperty : InlineStyleAccessPropertyBag.InlineStyleListProperty<EasingFunction>
		{
			// Token: 0x17000616 RID: 1558
			// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0006F11C File Offset: 0x0006D31C
			public override string Name
			{
				get
				{
					return "transitionTimingFunction";
				}
			}

			// Token: 0x17000617 RID: 1559
			// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x0006F123 File Offset: 0x0006D323
			public override string ussName
			{
				get
				{
					return "transition-timing-function";
				}
			}

			// Token: 0x17000618 RID: 1560
			// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BB6 RID: 7094 RVA: 0x0006F12A File Offset: 0x0006D32A
			public override StyleList<EasingFunction> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).transitionTimingFunction;
			}

			// Token: 0x06001BB7 RID: 7095 RVA: 0x0006F133 File Offset: 0x0006D333
			public override void SetValue(ref InlineStyleAccess container, StyleList<EasingFunction> value)
			{
				((IStyle)container).transitionTimingFunction = value;
			}
		}

		// Token: 0x0200039C RID: 924
		private class TranslateProperty : InlineStyleAccessPropertyBag.InlineStyleTranslateProperty
		{
			// Token: 0x17000619 RID: 1561
			// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x0006F147 File Offset: 0x0006D347
			public override string Name
			{
				get
				{
					return "translate";
				}
			}

			// Token: 0x1700061A RID: 1562
			// (get) Token: 0x06001BBA RID: 7098 RVA: 0x0006F147 File Offset: 0x0006D347
			public override string ussName
			{
				get
				{
					return "translate";
				}
			}

			// Token: 0x1700061B RID: 1563
			// (get) Token: 0x06001BBB RID: 7099 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BBC RID: 7100 RVA: 0x0006F14E File Offset: 0x0006D34E
			public override StyleTranslate GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).translate;
			}

			// Token: 0x06001BBD RID: 7101 RVA: 0x0006F157 File Offset: 0x0006D357
			public override void SetValue(ref InlineStyleAccess container, StyleTranslate value)
			{
				((IStyle)container).translate = value;
			}
		}

		// Token: 0x0200039D RID: 925
		private class UnityBackgroundImageTintColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x1700061C RID: 1564
			// (get) Token: 0x06001BBF RID: 7103 RVA: 0x0006F16B File Offset: 0x0006D36B
			public override string Name
			{
				get
				{
					return "unityBackgroundImageTintColor";
				}
			}

			// Token: 0x1700061D RID: 1565
			// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x0006F172 File Offset: 0x0006D372
			public override string ussName
			{
				get
				{
					return "-unity-background-image-tint-color";
				}
			}

			// Token: 0x1700061E RID: 1566
			// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BC2 RID: 7106 RVA: 0x0006F179 File Offset: 0x0006D379
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityBackgroundImageTintColor;
			}

			// Token: 0x06001BC3 RID: 7107 RVA: 0x0006F182 File Offset: 0x0006D382
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).unityBackgroundImageTintColor = value;
			}
		}

		// Token: 0x0200039E RID: 926
		private class UnityEditorTextRenderingModeProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<EditorTextRenderingMode>
		{
			// Token: 0x1700061F RID: 1567
			// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x0006F18D File Offset: 0x0006D38D
			public override string Name
			{
				get
				{
					return "unityEditorTextRenderingMode";
				}
			}

			// Token: 0x17000620 RID: 1568
			// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x0006F194 File Offset: 0x0006D394
			public override string ussName
			{
				get
				{
					return "-unity-editor-text-rendering-mode";
				}
			}

			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BC8 RID: 7112 RVA: 0x0006F19B File Offset: 0x0006D39B
			public override StyleEnum<EditorTextRenderingMode> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityEditorTextRenderingMode;
			}

			// Token: 0x06001BC9 RID: 7113 RVA: 0x0006F1A4 File Offset: 0x0006D3A4
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<EditorTextRenderingMode> value)
			{
				((IStyle)container).unityEditorTextRenderingMode = value;
			}
		}

		// Token: 0x0200039F RID: 927
		private class UnityFontProperty : InlineStyleAccessPropertyBag.InlineStyleFontProperty
		{
			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x06001BCB RID: 7115 RVA: 0x0006F1B8 File Offset: 0x0006D3B8
			public override string Name
			{
				get
				{
					return "unityFont";
				}
			}

			// Token: 0x17000623 RID: 1571
			// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0006F1BF File Offset: 0x0006D3BF
			public override string ussName
			{
				get
				{
					return "-unity-font";
				}
			}

			// Token: 0x17000624 RID: 1572
			// (get) Token: 0x06001BCD RID: 7117 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BCE RID: 7118 RVA: 0x0006F1C6 File Offset: 0x0006D3C6
			public override StyleFont GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityFont;
			}

			// Token: 0x06001BCF RID: 7119 RVA: 0x0006F1CF File Offset: 0x0006D3CF
			public override void SetValue(ref InlineStyleAccess container, StyleFont value)
			{
				((IStyle)container).unityFont = value;
			}
		}

		// Token: 0x020003A0 RID: 928
		private class UnityFontDefinitionProperty : InlineStyleAccessPropertyBag.InlineStyleFontDefinitionProperty
		{
			// Token: 0x17000625 RID: 1573
			// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x0006F1E3 File Offset: 0x0006D3E3
			public override string Name
			{
				get
				{
					return "unityFontDefinition";
				}
			}

			// Token: 0x17000626 RID: 1574
			// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x0006F1EA File Offset: 0x0006D3EA
			public override string ussName
			{
				get
				{
					return "-unity-font-definition";
				}
			}

			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BD4 RID: 7124 RVA: 0x0006F1F1 File Offset: 0x0006D3F1
			public override StyleFontDefinition GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityFontDefinition;
			}

			// Token: 0x06001BD5 RID: 7125 RVA: 0x0006F1FA File Offset: 0x0006D3FA
			public override void SetValue(ref InlineStyleAccess container, StyleFontDefinition value)
			{
				((IStyle)container).unityFontDefinition = value;
			}
		}

		// Token: 0x020003A1 RID: 929
		private class UnityFontStyleAndWeightProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<FontStyle>
		{
			// Token: 0x17000628 RID: 1576
			// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x0006F20E File Offset: 0x0006D40E
			public override string Name
			{
				get
				{
					return "unityFontStyleAndWeight";
				}
			}

			// Token: 0x17000629 RID: 1577
			// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x0006F215 File Offset: 0x0006D415
			public override string ussName
			{
				get
				{
					return "-unity-font-style";
				}
			}

			// Token: 0x1700062A RID: 1578
			// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BDA RID: 7130 RVA: 0x0006F21C File Offset: 0x0006D41C
			public override StyleEnum<FontStyle> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityFontStyleAndWeight;
			}

			// Token: 0x06001BDB RID: 7131 RVA: 0x0006F225 File Offset: 0x0006D425
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<FontStyle> value)
			{
				((IStyle)container).unityFontStyleAndWeight = value;
			}
		}

		// Token: 0x020003A2 RID: 930
		private class UnityOverflowClipBoxProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<OverflowClipBox>
		{
			// Token: 0x1700062B RID: 1579
			// (get) Token: 0x06001BDD RID: 7133 RVA: 0x0006F239 File Offset: 0x0006D439
			public override string Name
			{
				get
				{
					return "unityOverflowClipBox";
				}
			}

			// Token: 0x1700062C RID: 1580
			// (get) Token: 0x06001BDE RID: 7134 RVA: 0x0006F240 File Offset: 0x0006D440
			public override string ussName
			{
				get
				{
					return "-unity-overflow-clip-box";
				}
			}

			// Token: 0x1700062D RID: 1581
			// (get) Token: 0x06001BDF RID: 7135 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BE0 RID: 7136 RVA: 0x0006F247 File Offset: 0x0006D447
			public override StyleEnum<OverflowClipBox> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityOverflowClipBox;
			}

			// Token: 0x06001BE1 RID: 7137 RVA: 0x0006F250 File Offset: 0x0006D450
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<OverflowClipBox> value)
			{
				((IStyle)container).unityOverflowClipBox = value;
			}
		}

		// Token: 0x020003A3 RID: 931
		private class UnityParagraphSpacingProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x1700062E RID: 1582
			// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x0006F264 File Offset: 0x0006D464
			public override string Name
			{
				get
				{
					return "unityParagraphSpacing";
				}
			}

			// Token: 0x1700062F RID: 1583
			// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x0006F26B File Offset: 0x0006D46B
			public override string ussName
			{
				get
				{
					return "-unity-paragraph-spacing";
				}
			}

			// Token: 0x17000630 RID: 1584
			// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BE6 RID: 7142 RVA: 0x0006F272 File Offset: 0x0006D472
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityParagraphSpacing;
			}

			// Token: 0x06001BE7 RID: 7143 RVA: 0x0006F27B File Offset: 0x0006D47B
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).unityParagraphSpacing = value;
			}
		}

		// Token: 0x020003A4 RID: 932
		private class UnitySliceBottomProperty : InlineStyleAccessPropertyBag.InlineStyleIntProperty
		{
			// Token: 0x17000631 RID: 1585
			// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x0006F286 File Offset: 0x0006D486
			public override string Name
			{
				get
				{
					return "unitySliceBottom";
				}
			}

			// Token: 0x17000632 RID: 1586
			// (get) Token: 0x06001BEA RID: 7146 RVA: 0x0006F28D File Offset: 0x0006D48D
			public override string ussName
			{
				get
				{
					return "-unity-slice-bottom";
				}
			}

			// Token: 0x17000633 RID: 1587
			// (get) Token: 0x06001BEB RID: 7147 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BEC RID: 7148 RVA: 0x0006F294 File Offset: 0x0006D494
			public override StyleInt GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unitySliceBottom;
			}

			// Token: 0x06001BED RID: 7149 RVA: 0x0006F29D File Offset: 0x0006D49D
			public override void SetValue(ref InlineStyleAccess container, StyleInt value)
			{
				((IStyle)container).unitySliceBottom = value;
			}
		}

		// Token: 0x020003A5 RID: 933
		private class UnitySliceLeftProperty : InlineStyleAccessPropertyBag.InlineStyleIntProperty
		{
			// Token: 0x17000634 RID: 1588
			// (get) Token: 0x06001BEF RID: 7151 RVA: 0x0006F2B1 File Offset: 0x0006D4B1
			public override string Name
			{
				get
				{
					return "unitySliceLeft";
				}
			}

			// Token: 0x17000635 RID: 1589
			// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0006F2B8 File Offset: 0x0006D4B8
			public override string ussName
			{
				get
				{
					return "-unity-slice-left";
				}
			}

			// Token: 0x17000636 RID: 1590
			// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BF2 RID: 7154 RVA: 0x0006F2BF File Offset: 0x0006D4BF
			public override StyleInt GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unitySliceLeft;
			}

			// Token: 0x06001BF3 RID: 7155 RVA: 0x0006F2C8 File Offset: 0x0006D4C8
			public override void SetValue(ref InlineStyleAccess container, StyleInt value)
			{
				((IStyle)container).unitySliceLeft = value;
			}
		}

		// Token: 0x020003A6 RID: 934
		private class UnitySliceRightProperty : InlineStyleAccessPropertyBag.InlineStyleIntProperty
		{
			// Token: 0x17000637 RID: 1591
			// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x0006F2D3 File Offset: 0x0006D4D3
			public override string Name
			{
				get
				{
					return "unitySliceRight";
				}
			}

			// Token: 0x17000638 RID: 1592
			// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x0006F2DA File Offset: 0x0006D4DA
			public override string ussName
			{
				get
				{
					return "-unity-slice-right";
				}
			}

			// Token: 0x17000639 RID: 1593
			// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BF8 RID: 7160 RVA: 0x0006F2E1 File Offset: 0x0006D4E1
			public override StyleInt GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unitySliceRight;
			}

			// Token: 0x06001BF9 RID: 7161 RVA: 0x0006F2EA File Offset: 0x0006D4EA
			public override void SetValue(ref InlineStyleAccess container, StyleInt value)
			{
				((IStyle)container).unitySliceRight = value;
			}
		}

		// Token: 0x020003A7 RID: 935
		private class UnitySliceScaleProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x1700063A RID: 1594
			// (get) Token: 0x06001BFB RID: 7163 RVA: 0x0006F2F5 File Offset: 0x0006D4F5
			public override string Name
			{
				get
				{
					return "unitySliceScale";
				}
			}

			// Token: 0x1700063B RID: 1595
			// (get) Token: 0x06001BFC RID: 7164 RVA: 0x0006F2FC File Offset: 0x0006D4FC
			public override string ussName
			{
				get
				{
					return "-unity-slice-scale";
				}
			}

			// Token: 0x1700063C RID: 1596
			// (get) Token: 0x06001BFD RID: 7165 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001BFE RID: 7166 RVA: 0x0006F303 File Offset: 0x0006D503
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unitySliceScale;
			}

			// Token: 0x06001BFF RID: 7167 RVA: 0x0006F30C File Offset: 0x0006D50C
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).unitySliceScale = value;
			}
		}

		// Token: 0x020003A8 RID: 936
		private class UnitySliceTopProperty : InlineStyleAccessPropertyBag.InlineStyleIntProperty
		{
			// Token: 0x1700063D RID: 1597
			// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0006F317 File Offset: 0x0006D517
			public override string Name
			{
				get
				{
					return "unitySliceTop";
				}
			}

			// Token: 0x1700063E RID: 1598
			// (get) Token: 0x06001C02 RID: 7170 RVA: 0x0006F31E File Offset: 0x0006D51E
			public override string ussName
			{
				get
				{
					return "-unity-slice-top";
				}
			}

			// Token: 0x1700063F RID: 1599
			// (get) Token: 0x06001C03 RID: 7171 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001C04 RID: 7172 RVA: 0x0006F325 File Offset: 0x0006D525
			public override StyleInt GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unitySliceTop;
			}

			// Token: 0x06001C05 RID: 7173 RVA: 0x0006F32E File Offset: 0x0006D52E
			public override void SetValue(ref InlineStyleAccess container, StyleInt value)
			{
				((IStyle)container).unitySliceTop = value;
			}
		}

		// Token: 0x020003A9 RID: 937
		private class UnitySliceTypeProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<SliceType>
		{
			// Token: 0x17000640 RID: 1600
			// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0006F339 File Offset: 0x0006D539
			public override string Name
			{
				get
				{
					return "unitySliceType";
				}
			}

			// Token: 0x17000641 RID: 1601
			// (get) Token: 0x06001C08 RID: 7176 RVA: 0x0006F340 File Offset: 0x0006D540
			public override string ussName
			{
				get
				{
					return "-unity-slice-type";
				}
			}

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x06001C09 RID: 7177 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001C0A RID: 7178 RVA: 0x0006F347 File Offset: 0x0006D547
			public override StyleEnum<SliceType> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unitySliceType;
			}

			// Token: 0x06001C0B RID: 7179 RVA: 0x0006F350 File Offset: 0x0006D550
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<SliceType> value)
			{
				((IStyle)container).unitySliceType = value;
			}
		}

		// Token: 0x020003AA RID: 938
		private class UnityTextAlignProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<TextAnchor>
		{
			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x06001C0D RID: 7181 RVA: 0x0006F364 File Offset: 0x0006D564
			public override string Name
			{
				get
				{
					return "unityTextAlign";
				}
			}

			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x06001C0E RID: 7182 RVA: 0x0006F36B File Offset: 0x0006D56B
			public override string ussName
			{
				get
				{
					return "-unity-text-align";
				}
			}

			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x06001C0F RID: 7183 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001C10 RID: 7184 RVA: 0x0006F372 File Offset: 0x0006D572
			public override StyleEnum<TextAnchor> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityTextAlign;
			}

			// Token: 0x06001C11 RID: 7185 RVA: 0x0006F37B File Offset: 0x0006D57B
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<TextAnchor> value)
			{
				((IStyle)container).unityTextAlign = value;
			}
		}

		// Token: 0x020003AB RID: 939
		private class UnityTextGeneratorProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<TextGeneratorType>
		{
			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x06001C13 RID: 7187 RVA: 0x0006F38F File Offset: 0x0006D58F
			public override string Name
			{
				get
				{
					return "unityTextGenerator";
				}
			}

			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x06001C14 RID: 7188 RVA: 0x0006F396 File Offset: 0x0006D596
			public override string ussName
			{
				get
				{
					return "-unity-text-generator";
				}
			}

			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x06001C15 RID: 7189 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001C16 RID: 7190 RVA: 0x0006F39D File Offset: 0x0006D59D
			public override StyleEnum<TextGeneratorType> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityTextGenerator;
			}

			// Token: 0x06001C17 RID: 7191 RVA: 0x0006F3A6 File Offset: 0x0006D5A6
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<TextGeneratorType> value)
			{
				((IStyle)container).unityTextGenerator = value;
			}
		}

		// Token: 0x020003AC RID: 940
		private class UnityTextOutlineColorProperty : InlineStyleAccessPropertyBag.InlineStyleColorProperty
		{
			// Token: 0x17000649 RID: 1609
			// (get) Token: 0x06001C19 RID: 7193 RVA: 0x0006F3BA File Offset: 0x0006D5BA
			public override string Name
			{
				get
				{
					return "unityTextOutlineColor";
				}
			}

			// Token: 0x1700064A RID: 1610
			// (get) Token: 0x06001C1A RID: 7194 RVA: 0x0006F3C1 File Offset: 0x0006D5C1
			public override string ussName
			{
				get
				{
					return "-unity-text-outline-color";
				}
			}

			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x06001C1B RID: 7195 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001C1C RID: 7196 RVA: 0x0006F3C8 File Offset: 0x0006D5C8
			public override StyleColor GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityTextOutlineColor;
			}

			// Token: 0x06001C1D RID: 7197 RVA: 0x0006F3D1 File Offset: 0x0006D5D1
			public override void SetValue(ref InlineStyleAccess container, StyleColor value)
			{
				((IStyle)container).unityTextOutlineColor = value;
			}
		}

		// Token: 0x020003AD RID: 941
		private class UnityTextOutlineWidthProperty : InlineStyleAccessPropertyBag.InlineStyleFloatProperty
		{
			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x06001C1F RID: 7199 RVA: 0x0006F3DC File Offset: 0x0006D5DC
			public override string Name
			{
				get
				{
					return "unityTextOutlineWidth";
				}
			}

			// Token: 0x1700064D RID: 1613
			// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0006F3E3 File Offset: 0x0006D5E3
			public override string ussName
			{
				get
				{
					return "-unity-text-outline-width";
				}
			}

			// Token: 0x1700064E RID: 1614
			// (get) Token: 0x06001C21 RID: 7201 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001C22 RID: 7202 RVA: 0x0006F3EA File Offset: 0x0006D5EA
			public override StyleFloat GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityTextOutlineWidth;
			}

			// Token: 0x06001C23 RID: 7203 RVA: 0x0006F3F3 File Offset: 0x0006D5F3
			public override void SetValue(ref InlineStyleAccess container, StyleFloat value)
			{
				((IStyle)container).unityTextOutlineWidth = value;
			}
		}

		// Token: 0x020003AE RID: 942
		private class UnityTextOverflowPositionProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<TextOverflowPosition>
		{
			// Token: 0x1700064F RID: 1615
			// (get) Token: 0x06001C25 RID: 7205 RVA: 0x0006F3FE File Offset: 0x0006D5FE
			public override string Name
			{
				get
				{
					return "unityTextOverflowPosition";
				}
			}

			// Token: 0x17000650 RID: 1616
			// (get) Token: 0x06001C26 RID: 7206 RVA: 0x0006F405 File Offset: 0x0006D605
			public override string ussName
			{
				get
				{
					return "-unity-text-overflow-position";
				}
			}

			// Token: 0x17000651 RID: 1617
			// (get) Token: 0x06001C27 RID: 7207 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001C28 RID: 7208 RVA: 0x0006F40C File Offset: 0x0006D60C
			public override StyleEnum<TextOverflowPosition> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).unityTextOverflowPosition;
			}

			// Token: 0x06001C29 RID: 7209 RVA: 0x0006F415 File Offset: 0x0006D615
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<TextOverflowPosition> value)
			{
				((IStyle)container).unityTextOverflowPosition = value;
			}
		}

		// Token: 0x020003AF RID: 943
		private class VisibilityProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<Visibility>
		{
			// Token: 0x17000652 RID: 1618
			// (get) Token: 0x06001C2B RID: 7211 RVA: 0x0006F429 File Offset: 0x0006D629
			public override string Name
			{
				get
				{
					return "visibility";
				}
			}

			// Token: 0x17000653 RID: 1619
			// (get) Token: 0x06001C2C RID: 7212 RVA: 0x0006F429 File Offset: 0x0006D629
			public override string ussName
			{
				get
				{
					return "visibility";
				}
			}

			// Token: 0x17000654 RID: 1620
			// (get) Token: 0x06001C2D RID: 7213 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001C2E RID: 7214 RVA: 0x0006F430 File Offset: 0x0006D630
			public override StyleEnum<Visibility> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).visibility;
			}

			// Token: 0x06001C2F RID: 7215 RVA: 0x0006F439 File Offset: 0x0006D639
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<Visibility> value)
			{
				((IStyle)container).visibility = value;
			}
		}

		// Token: 0x020003B0 RID: 944
		private class WhiteSpaceProperty : InlineStyleAccessPropertyBag.InlineStyleEnumProperty<WhiteSpace>
		{
			// Token: 0x17000655 RID: 1621
			// (get) Token: 0x06001C31 RID: 7217 RVA: 0x0006F44D File Offset: 0x0006D64D
			public override string Name
			{
				get
				{
					return "whiteSpace";
				}
			}

			// Token: 0x17000656 RID: 1622
			// (get) Token: 0x06001C32 RID: 7218 RVA: 0x0006F454 File Offset: 0x0006D654
			public override string ussName
			{
				get
				{
					return "white-space";
				}
			}

			// Token: 0x17000657 RID: 1623
			// (get) Token: 0x06001C33 RID: 7219 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001C34 RID: 7220 RVA: 0x0006F45B File Offset: 0x0006D65B
			public override StyleEnum<WhiteSpace> GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).whiteSpace;
			}

			// Token: 0x06001C35 RID: 7221 RVA: 0x0006F464 File Offset: 0x0006D664
			public override void SetValue(ref InlineStyleAccess container, StyleEnum<WhiteSpace> value)
			{
				((IStyle)container).whiteSpace = value;
			}
		}

		// Token: 0x020003B1 RID: 945
		private class WidthProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x17000658 RID: 1624
			// (get) Token: 0x06001C37 RID: 7223 RVA: 0x0006F478 File Offset: 0x0006D678
			public override string Name
			{
				get
				{
					return "width";
				}
			}

			// Token: 0x17000659 RID: 1625
			// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0006F478 File Offset: 0x0006D678
			public override string ussName
			{
				get
				{
					return "width";
				}
			}

			// Token: 0x1700065A RID: 1626
			// (get) Token: 0x06001C39 RID: 7225 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001C3A RID: 7226 RVA: 0x0006F47F File Offset: 0x0006D67F
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).width;
			}

			// Token: 0x06001C3B RID: 7227 RVA: 0x0006F488 File Offset: 0x0006D688
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).width = value;
			}
		}

		// Token: 0x020003B2 RID: 946
		private class WordSpacingProperty : InlineStyleAccessPropertyBag.InlineStyleLengthProperty
		{
			// Token: 0x1700065B RID: 1627
			// (get) Token: 0x06001C3D RID: 7229 RVA: 0x0006F493 File Offset: 0x0006D693
			public override string Name
			{
				get
				{
					return "wordSpacing";
				}
			}

			// Token: 0x1700065C RID: 1628
			// (get) Token: 0x06001C3E RID: 7230 RVA: 0x0006F49A File Offset: 0x0006D69A
			public override string ussName
			{
				get
				{
					return "word-spacing";
				}
			}

			// Token: 0x1700065D RID: 1629
			// (get) Token: 0x06001C3F RID: 7231 RVA: 0x00011DCA File Offset: 0x0000FFCA
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001C40 RID: 7232 RVA: 0x0006F4A1 File Offset: 0x0006D6A1
			public override StyleLength GetValue(ref InlineStyleAccess container)
			{
				return ((IStyle)container).wordSpacing;
			}

			// Token: 0x06001C41 RID: 7233 RVA: 0x0006F4AA File Offset: 0x0006D6AA
			public override void SetValue(ref InlineStyleAccess container, StyleLength value)
			{
				((IStyle)container).wordSpacing = value;
			}
		}

		// Token: 0x020003B3 RID: 947
		private abstract class InlineStyleProperty<TStyleValue, TValue> : Property<InlineStyleAccess, TStyleValue> where TStyleValue : IStyleValue<TValue>, new()
		{
			// Token: 0x06001C43 RID: 7235 RVA: 0x0006F4B8 File Offset: 0x0006D6B8
			protected InlineStyleProperty()
			{
				ConverterGroups.RegisterGlobal<TStyleValue, TValue>(delegate(ref TStyleValue sv)
				{
					return sv.value;
				});
				ConverterGroups.RegisterGlobal<TValue, TStyleValue>(delegate(ref TValue v)
				{
					TStyleValue tstyleValue = new TStyleValue();
					tstyleValue.value = v;
					return tstyleValue;
				});
				ConverterGroups.RegisterGlobal<TStyleValue, StyleKeyword>(delegate(ref TStyleValue sv)
				{
					return sv.keyword;
				});
				ConverterGroups.RegisterGlobal<StyleKeyword, TStyleValue>(delegate(ref StyleKeyword kw)
				{
					TStyleValue tstyleValue2 = new TStyleValue();
					tstyleValue2.keyword = kw;
					return tstyleValue2;
				});
			}

			// Token: 0x1700065E RID: 1630
			// (get) Token: 0x06001C44 RID: 7236
			public abstract string ussName { get; }
		}

		// Token: 0x020003B5 RID: 949
		private abstract class InlineStyleEnumProperty<TValue> : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleEnum<TValue>, TValue> where TValue : struct, IConvertible
		{
		}

		// Token: 0x020003B6 RID: 950
		private abstract class InlineStyleColorProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleColor, Color>
		{
			// Token: 0x06001C4C RID: 7244 RVA: 0x0006F5E4 File Offset: 0x0006D7E4
			protected InlineStyleColorProperty()
			{
				ConverterGroups.RegisterGlobal<Color32, StyleColor>(delegate(ref Color32 v)
				{
					return new StyleColor(v);
				});
				ConverterGroups.RegisterGlobal<StyleColor, Color32>(delegate(ref StyleColor sv)
				{
					return sv.value;
				});
			}
		}

		// Token: 0x020003B8 RID: 952
		private abstract class InlineStyleBackgroundProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleBackground, Background>
		{
			// Token: 0x06001C51 RID: 7249 RVA: 0x0006F670 File Offset: 0x0006D870
			protected InlineStyleBackgroundProperty()
			{
				ConverterGroups.RegisterGlobal<Texture2D, StyleBackground>(delegate(ref Texture2D v)
				{
					return new StyleBackground(v);
				});
				ConverterGroups.RegisterGlobal<Sprite, StyleBackground>(delegate(ref Sprite v)
				{
					return new StyleBackground(v);
				});
				ConverterGroups.RegisterGlobal<VectorImage, StyleBackground>(delegate(ref VectorImage v)
				{
					return new StyleBackground(v);
				});
				ConverterGroups.RegisterGlobal<RenderTexture, StyleBackground>(delegate(ref RenderTexture v)
				{
					return new StyleBackground(Background.FromRenderTexture(v));
				});
				ConverterGroups.RegisterGlobal<StyleBackground, Texture2D>(delegate(ref StyleBackground sv)
				{
					return sv.value.texture;
				});
				ConverterGroups.RegisterGlobal<StyleBackground, Sprite>(delegate(ref StyleBackground sv)
				{
					return sv.value.sprite;
				});
				ConverterGroups.RegisterGlobal<StyleBackground, RenderTexture>(delegate(ref StyleBackground sv)
				{
					return sv.value.renderTexture;
				});
				ConverterGroups.RegisterGlobal<StyleBackground, VectorImage>(delegate(ref StyleBackground sv)
				{
					return sv.value.vectorImage;
				});
			}
		}

		// Token: 0x020003BA RID: 954
		private abstract class InlineStyleLengthProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleLength, Length>
		{
			// Token: 0x06001C5C RID: 7260 RVA: 0x0006F854 File Offset: 0x0006DA54
			protected InlineStyleLengthProperty()
			{
				ConverterGroups.RegisterGlobal<float, StyleLength>(delegate(ref float v)
				{
					return new StyleLength(v);
				});
				ConverterGroups.RegisterGlobal<int, StyleLength>(delegate(ref int v)
				{
					return new StyleLength((float)v);
				});
				ConverterGroups.RegisterGlobal<StyleLength, float>(delegate(ref StyleLength sv)
				{
					return sv.value.value;
				});
				ConverterGroups.RegisterGlobal<StyleLength, int>(delegate(ref StyleLength sv)
				{
					return (int)sv.value.value;
				});
			}
		}

		// Token: 0x020003BC RID: 956
		private abstract class InlineStyleFloatProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleFloat, float>
		{
			// Token: 0x06001C63 RID: 7267 RVA: 0x0006F954 File Offset: 0x0006DB54
			protected InlineStyleFloatProperty()
			{
				ConverterGroups.RegisterGlobal<int, StyleFloat>(delegate(ref int v)
				{
					return new StyleFloat((float)v);
				});
				ConverterGroups.RegisterGlobal<StyleFloat, int>(delegate(ref StyleFloat sv)
				{
					return (int)sv.value;
				});
			}
		}

		// Token: 0x020003BE RID: 958
		private abstract class InlineStyleListProperty<T> : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleList<T>, List<T>>
		{
		}

		// Token: 0x020003BF RID: 959
		private abstract class InlineStyleFontProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleFont, Font>
		{
		}

		// Token: 0x020003C0 RID: 960
		private abstract class InlineStyleFontDefinitionProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleFontDefinition, FontDefinition>
		{
			// Token: 0x06001C6A RID: 7274 RVA: 0x0006F9E4 File Offset: 0x0006DBE4
			protected InlineStyleFontDefinitionProperty()
			{
				ConverterGroups.RegisterGlobal<Font, StyleFontDefinition>(delegate(ref Font v)
				{
					return new StyleFontDefinition(v);
				});
				ConverterGroups.RegisterGlobal<FontAsset, StyleFontDefinition>(delegate(ref FontAsset v)
				{
					return new StyleFontDefinition(v);
				});
				ConverterGroups.RegisterGlobal<StyleFontDefinition, Font>(delegate(ref StyleFontDefinition sv)
				{
					return sv.value.font;
				});
				ConverterGroups.RegisterGlobal<StyleFontDefinition, FontAsset>(delegate(ref StyleFontDefinition sv)
				{
					return sv.value.fontAsset;
				});
			}
		}

		// Token: 0x020003C2 RID: 962
		private abstract class InlineStyleIntProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleInt, int>
		{
		}

		// Token: 0x020003C3 RID: 963
		private abstract class InlineStyleRotateProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleRotate, Rotate>
		{
		}

		// Token: 0x020003C4 RID: 964
		private abstract class InlineStyleScaleProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleScale, Scale>
		{
		}

		// Token: 0x020003C5 RID: 965
		private abstract class InlineStyleCursorProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleCursor, Cursor>
		{
		}

		// Token: 0x020003C6 RID: 966
		private abstract class InlineStyleTextShadowProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleTextShadow, TextShadow>
		{
		}

		// Token: 0x020003C7 RID: 967
		private abstract class InlineStyleTransformOriginProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleTransformOrigin, TransformOrigin>
		{
		}

		// Token: 0x020003C8 RID: 968
		private abstract class InlineStyleTranslateProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleTranslate, Translate>
		{
		}

		// Token: 0x020003C9 RID: 969
		private abstract class InlineStyleBackgroundPositionProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleBackgroundPosition, BackgroundPosition>
		{
		}

		// Token: 0x020003CA RID: 970
		private abstract class InlineStyleBackgroundRepeatProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleBackgroundRepeat, BackgroundRepeat>
		{
		}

		// Token: 0x020003CB RID: 971
		private abstract class InlineStyleBackgroundSizeProperty : InlineStyleAccessPropertyBag.InlineStyleProperty<StyleBackgroundSize, BackgroundSize>
		{
		}
	}
}
