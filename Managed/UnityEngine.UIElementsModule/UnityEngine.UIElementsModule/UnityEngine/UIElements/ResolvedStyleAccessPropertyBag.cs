using System;
using System.Collections.Generic;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020003EA RID: 1002
	internal class ResolvedStyleAccessPropertyBag : PropertyBag<ResolvedStyleAccess>, INamedProperties<ResolvedStyleAccess>
	{
		// Token: 0x06002031 RID: 8241 RVA: 0x0007AB54 File Offset: 0x00078D54
		public ResolvedStyleAccessPropertyBag()
		{
			this.m_PropertiesList = new List<IProperty<ResolvedStyleAccess>>(80);
			this.m_PropertiesHash = new Dictionary<string, IProperty<ResolvedStyleAccess>>(240);
			this.AddProperty<Align>(new ResolvedStyleAccessPropertyBag.AlignContentProperty());
			this.AddProperty<Align>(new ResolvedStyleAccessPropertyBag.AlignItemsProperty());
			this.AddProperty<Align>(new ResolvedStyleAccessPropertyBag.AlignSelfProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.BackgroundColorProperty());
			this.AddProperty<Background>(new ResolvedStyleAccessPropertyBag.BackgroundImageProperty());
			this.AddProperty<BackgroundPosition>(new ResolvedStyleAccessPropertyBag.BackgroundPositionXProperty());
			this.AddProperty<BackgroundPosition>(new ResolvedStyleAccessPropertyBag.BackgroundPositionYProperty());
			this.AddProperty<BackgroundRepeat>(new ResolvedStyleAccessPropertyBag.BackgroundRepeatProperty());
			this.AddProperty<BackgroundSize>(new ResolvedStyleAccessPropertyBag.BackgroundSizeProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.BorderBottomColorProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderBottomLeftRadiusProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderBottomRightRadiusProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderBottomWidthProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.BorderLeftColorProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderLeftWidthProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.BorderRightColorProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderRightWidthProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.BorderTopColorProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderTopLeftRadiusProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderTopRightRadiusProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BorderTopWidthProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.BottomProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.ColorProperty());
			this.AddProperty<DisplayStyle>(new ResolvedStyleAccessPropertyBag.DisplayProperty());
			this.AddProperty<StyleFloat>(new ResolvedStyleAccessPropertyBag.FlexBasisProperty());
			this.AddProperty<FlexDirection>(new ResolvedStyleAccessPropertyBag.FlexDirectionProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.FlexGrowProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.FlexShrinkProperty());
			this.AddProperty<Wrap>(new ResolvedStyleAccessPropertyBag.FlexWrapProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.FontSizeProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.HeightProperty());
			this.AddProperty<Justify>(new ResolvedStyleAccessPropertyBag.JustifyContentProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.LeftProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.LetterSpacingProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.MarginBottomProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.MarginLeftProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.MarginRightProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.MarginTopProperty());
			this.AddProperty<StyleFloat>(new ResolvedStyleAccessPropertyBag.MaxHeightProperty());
			this.AddProperty<StyleFloat>(new ResolvedStyleAccessPropertyBag.MaxWidthProperty());
			this.AddProperty<StyleFloat>(new ResolvedStyleAccessPropertyBag.MinHeightProperty());
			this.AddProperty<StyleFloat>(new ResolvedStyleAccessPropertyBag.MinWidthProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.OpacityProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.PaddingBottomProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.PaddingLeftProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.PaddingRightProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.PaddingTopProperty());
			this.AddProperty<Position>(new ResolvedStyleAccessPropertyBag.PositionProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.RightProperty());
			this.AddProperty<Rotate>(new ResolvedStyleAccessPropertyBag.RotateProperty());
			this.AddProperty<Scale>(new ResolvedStyleAccessPropertyBag.ScaleProperty());
			this.AddProperty<TextOverflow>(new ResolvedStyleAccessPropertyBag.TextOverflowProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.TopProperty());
			this.AddProperty<Vector3>(new ResolvedStyleAccessPropertyBag.TransformOriginProperty());
			this.AddProperty<IEnumerable<TimeValue>>(new ResolvedStyleAccessPropertyBag.TransitionDelayProperty());
			this.AddProperty<IEnumerable<TimeValue>>(new ResolvedStyleAccessPropertyBag.TransitionDurationProperty());
			this.AddProperty<IEnumerable<StylePropertyName>>(new ResolvedStyleAccessPropertyBag.TransitionPropertyProperty());
			this.AddProperty<IEnumerable<EasingFunction>>(new ResolvedStyleAccessPropertyBag.TransitionTimingFunctionProperty());
			this.AddProperty<Vector3>(new ResolvedStyleAccessPropertyBag.TranslateProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.UnityBackgroundImageTintColorProperty());
			this.AddProperty<EditorTextRenderingMode>(new ResolvedStyleAccessPropertyBag.UnityEditorTextRenderingModeProperty());
			this.AddProperty<Font>(new ResolvedStyleAccessPropertyBag.UnityFontProperty());
			this.AddProperty<FontDefinition>(new ResolvedStyleAccessPropertyBag.UnityFontDefinitionProperty());
			this.AddProperty<FontStyle>(new ResolvedStyleAccessPropertyBag.UnityFontStyleAndWeightProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.UnityParagraphSpacingProperty());
			this.AddProperty<int>(new ResolvedStyleAccessPropertyBag.UnitySliceBottomProperty());
			this.AddProperty<int>(new ResolvedStyleAccessPropertyBag.UnitySliceLeftProperty());
			this.AddProperty<int>(new ResolvedStyleAccessPropertyBag.UnitySliceRightProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.UnitySliceScaleProperty());
			this.AddProperty<int>(new ResolvedStyleAccessPropertyBag.UnitySliceTopProperty());
			this.AddProperty<SliceType>(new ResolvedStyleAccessPropertyBag.UnitySliceTypeProperty());
			this.AddProperty<TextAnchor>(new ResolvedStyleAccessPropertyBag.UnityTextAlignProperty());
			this.AddProperty<TextGeneratorType>(new ResolvedStyleAccessPropertyBag.UnityTextGeneratorProperty());
			this.AddProperty<Color>(new ResolvedStyleAccessPropertyBag.UnityTextOutlineColorProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.UnityTextOutlineWidthProperty());
			this.AddProperty<TextOverflowPosition>(new ResolvedStyleAccessPropertyBag.UnityTextOverflowPositionProperty());
			this.AddProperty<Visibility>(new ResolvedStyleAccessPropertyBag.VisibilityProperty());
			this.AddProperty<WhiteSpace>(new ResolvedStyleAccessPropertyBag.WhiteSpaceProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.WidthProperty());
			this.AddProperty<float>(new ResolvedStyleAccessPropertyBag.WordSpacingProperty());
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x0007AF48 File Offset: 0x00079148
		private void AddProperty<TValue>(ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<TValue> property)
		{
			this.m_PropertiesList.Add(property);
			this.m_PropertiesHash.Add(property.Name, property);
			bool flag = string.CompareOrdinal(property.Name, property.ussName) != 0;
			if (flag)
			{
				this.m_PropertiesHash.Add(property.ussName, property);
			}
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x0007AFA1 File Offset: 0x000791A1
		public override PropertyCollection<ResolvedStyleAccess> GetProperties()
		{
			return new PropertyCollection<ResolvedStyleAccess>(this.m_PropertiesList);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0007AFA1 File Offset: 0x000791A1
		public override PropertyCollection<ResolvedStyleAccess> GetProperties(ref ResolvedStyleAccess container)
		{
			return new PropertyCollection<ResolvedStyleAccess>(this.m_PropertiesList);
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x0007AFAE File Offset: 0x000791AE
		public bool TryGetProperty(ref ResolvedStyleAccess container, string name, out IProperty<ResolvedStyleAccess> property)
		{
			return this.m_PropertiesHash.TryGetValue(name, out property);
		}

		// Token: 0x04000F39 RID: 3897
		private readonly List<IProperty<ResolvedStyleAccess>> m_PropertiesList;

		// Token: 0x04000F3A RID: 3898
		private readonly Dictionary<string, IProperty<ResolvedStyleAccess>> m_PropertiesHash;

		// Token: 0x020003EB RID: 1003
		private class AlignContentProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<Align>
		{
			// Token: 0x17000828 RID: 2088
			// (get) Token: 0x06002036 RID: 8246 RVA: 0x0006E8B9 File Offset: 0x0006CAB9
			public override string Name
			{
				get
				{
					return "alignContent";
				}
			}

			// Token: 0x17000829 RID: 2089
			// (get) Token: 0x06002037 RID: 8247 RVA: 0x0006E8C0 File Offset: 0x0006CAC0
			public override string ussName
			{
				get
				{
					return "align-content";
				}
			}

			// Token: 0x1700082A RID: 2090
			// (get) Token: 0x06002038 RID: 8248 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002039 RID: 8249 RVA: 0x0007AFBD File Offset: 0x000791BD
			public override Align GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).alignContent;
			}

			// Token: 0x0600203A RID: 8250 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Align value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003EC RID: 1004
		private class AlignItemsProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<Align>
		{
			// Token: 0x1700082B RID: 2091
			// (get) Token: 0x0600203C RID: 8252 RVA: 0x0006E8E4 File Offset: 0x0006CAE4
			public override string Name
			{
				get
				{
					return "alignItems";
				}
			}

			// Token: 0x1700082C RID: 2092
			// (get) Token: 0x0600203D RID: 8253 RVA: 0x0006E8EB File Offset: 0x0006CAEB
			public override string ussName
			{
				get
				{
					return "align-items";
				}
			}

			// Token: 0x1700082D RID: 2093
			// (get) Token: 0x0600203E RID: 8254 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600203F RID: 8255 RVA: 0x0007AFD6 File Offset: 0x000791D6
			public override Align GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).alignItems;
			}

			// Token: 0x06002040 RID: 8256 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Align value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003ED RID: 1005
		private class AlignSelfProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<Align>
		{
			// Token: 0x1700082E RID: 2094
			// (get) Token: 0x06002042 RID: 8258 RVA: 0x0006E906 File Offset: 0x0006CB06
			public override string Name
			{
				get
				{
					return "alignSelf";
				}
			}

			// Token: 0x1700082F RID: 2095
			// (get) Token: 0x06002043 RID: 8259 RVA: 0x0006E90D File Offset: 0x0006CB0D
			public override string ussName
			{
				get
				{
					return "align-self";
				}
			}

			// Token: 0x17000830 RID: 2096
			// (get) Token: 0x06002044 RID: 8260 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002045 RID: 8261 RVA: 0x0007AFDF File Offset: 0x000791DF
			public override Align GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).alignSelf;
			}

			// Token: 0x06002046 RID: 8262 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Align value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003EE RID: 1006
		private class BackgroundColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x17000831 RID: 2097
			// (get) Token: 0x06002048 RID: 8264 RVA: 0x0006E928 File Offset: 0x0006CB28
			public override string Name
			{
				get
				{
					return "backgroundColor";
				}
			}

			// Token: 0x17000832 RID: 2098
			// (get) Token: 0x06002049 RID: 8265 RVA: 0x0006E92F File Offset: 0x0006CB2F
			public override string ussName
			{
				get
				{
					return "background-color";
				}
			}

			// Token: 0x17000833 RID: 2099
			// (get) Token: 0x0600204A RID: 8266 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600204B RID: 8267 RVA: 0x0007AFE8 File Offset: 0x000791E8
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).backgroundColor;
			}

			// Token: 0x0600204C RID: 8268 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003EF RID: 1007
		private class BackgroundImageProperty : ResolvedStyleAccessPropertyBag.ResolvedBackgroundProperty
		{
			// Token: 0x17000834 RID: 2100
			// (get) Token: 0x0600204E RID: 8270 RVA: 0x0006E953 File Offset: 0x0006CB53
			public override string Name
			{
				get
				{
					return "backgroundImage";
				}
			}

			// Token: 0x17000835 RID: 2101
			// (get) Token: 0x0600204F RID: 8271 RVA: 0x0006E95A File Offset: 0x0006CB5A
			public override string ussName
			{
				get
				{
					return "background-image";
				}
			}

			// Token: 0x17000836 RID: 2102
			// (get) Token: 0x06002050 RID: 8272 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002051 RID: 8273 RVA: 0x0007AFFA File Offset: 0x000791FA
			public override Background GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).backgroundImage;
			}

			// Token: 0x06002052 RID: 8274 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Background value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003F0 RID: 1008
		private class BackgroundPositionXProperty : ResolvedStyleAccessPropertyBag.ResolvedBackgroundPositionProperty
		{
			// Token: 0x17000837 RID: 2103
			// (get) Token: 0x06002054 RID: 8276 RVA: 0x0006E97E File Offset: 0x0006CB7E
			public override string Name
			{
				get
				{
					return "backgroundPositionX";
				}
			}

			// Token: 0x17000838 RID: 2104
			// (get) Token: 0x06002055 RID: 8277 RVA: 0x0006E985 File Offset: 0x0006CB85
			public override string ussName
			{
				get
				{
					return "background-position-x";
				}
			}

			// Token: 0x17000839 RID: 2105
			// (get) Token: 0x06002056 RID: 8278 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002057 RID: 8279 RVA: 0x0007B00C File Offset: 0x0007920C
			public override BackgroundPosition GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).backgroundPositionX;
			}

			// Token: 0x06002058 RID: 8280 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, BackgroundPosition value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003F1 RID: 1009
		private class BackgroundPositionYProperty : ResolvedStyleAccessPropertyBag.ResolvedBackgroundPositionProperty
		{
			// Token: 0x1700083A RID: 2106
			// (get) Token: 0x0600205A RID: 8282 RVA: 0x0006E9A9 File Offset: 0x0006CBA9
			public override string Name
			{
				get
				{
					return "backgroundPositionY";
				}
			}

			// Token: 0x1700083B RID: 2107
			// (get) Token: 0x0600205B RID: 8283 RVA: 0x0006E9B0 File Offset: 0x0006CBB0
			public override string ussName
			{
				get
				{
					return "background-position-y";
				}
			}

			// Token: 0x1700083C RID: 2108
			// (get) Token: 0x0600205C RID: 8284 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600205D RID: 8285 RVA: 0x0007B01E File Offset: 0x0007921E
			public override BackgroundPosition GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).backgroundPositionY;
			}

			// Token: 0x0600205E RID: 8286 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, BackgroundPosition value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003F2 RID: 1010
		private class BackgroundRepeatProperty : ResolvedStyleAccessPropertyBag.ResolvedBackgroundRepeatProperty
		{
			// Token: 0x1700083D RID: 2109
			// (get) Token: 0x06002060 RID: 8288 RVA: 0x0006E9CB File Offset: 0x0006CBCB
			public override string Name
			{
				get
				{
					return "backgroundRepeat";
				}
			}

			// Token: 0x1700083E RID: 2110
			// (get) Token: 0x06002061 RID: 8289 RVA: 0x0006E9D2 File Offset: 0x0006CBD2
			public override string ussName
			{
				get
				{
					return "background-repeat";
				}
			}

			// Token: 0x1700083F RID: 2111
			// (get) Token: 0x06002062 RID: 8290 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002063 RID: 8291 RVA: 0x0007B027 File Offset: 0x00079227
			public override BackgroundRepeat GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).backgroundRepeat;
			}

			// Token: 0x06002064 RID: 8292 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, BackgroundRepeat value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003F3 RID: 1011
		private class BackgroundSizeProperty : ResolvedStyleAccessPropertyBag.ResolvedBackgroundSizeProperty
		{
			// Token: 0x17000840 RID: 2112
			// (get) Token: 0x06002066 RID: 8294 RVA: 0x0006E9F6 File Offset: 0x0006CBF6
			public override string Name
			{
				get
				{
					return "backgroundSize";
				}
			}

			// Token: 0x17000841 RID: 2113
			// (get) Token: 0x06002067 RID: 8295 RVA: 0x0006E9FD File Offset: 0x0006CBFD
			public override string ussName
			{
				get
				{
					return "background-size";
				}
			}

			// Token: 0x17000842 RID: 2114
			// (get) Token: 0x06002068 RID: 8296 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002069 RID: 8297 RVA: 0x0007B039 File Offset: 0x00079239
			public override BackgroundSize GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).backgroundSize;
			}

			// Token: 0x0600206A RID: 8298 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, BackgroundSize value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003F4 RID: 1012
		private class BorderBottomColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x0600206C RID: 8300 RVA: 0x0006EA21 File Offset: 0x0006CC21
			public override string Name
			{
				get
				{
					return "borderBottomColor";
				}
			}

			// Token: 0x17000844 RID: 2116
			// (get) Token: 0x0600206D RID: 8301 RVA: 0x0006EA28 File Offset: 0x0006CC28
			public override string ussName
			{
				get
				{
					return "border-bottom-color";
				}
			}

			// Token: 0x17000845 RID: 2117
			// (get) Token: 0x0600206E RID: 8302 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600206F RID: 8303 RVA: 0x0007B04B File Offset: 0x0007924B
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderBottomColor;
			}

			// Token: 0x06002070 RID: 8304 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003F5 RID: 1013
		private class BorderBottomLeftRadiusProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000846 RID: 2118
			// (get) Token: 0x06002072 RID: 8306 RVA: 0x0006EA43 File Offset: 0x0006CC43
			public override string Name
			{
				get
				{
					return "borderBottomLeftRadius";
				}
			}

			// Token: 0x17000847 RID: 2119
			// (get) Token: 0x06002073 RID: 8307 RVA: 0x0006EA4A File Offset: 0x0006CC4A
			public override string ussName
			{
				get
				{
					return "border-bottom-left-radius";
				}
			}

			// Token: 0x17000848 RID: 2120
			// (get) Token: 0x06002074 RID: 8308 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002075 RID: 8309 RVA: 0x0007B054 File Offset: 0x00079254
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderBottomLeftRadius;
			}

			// Token: 0x06002076 RID: 8310 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003F6 RID: 1014
		private class BorderBottomRightRadiusProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000849 RID: 2121
			// (get) Token: 0x06002078 RID: 8312 RVA: 0x0006EA6E File Offset: 0x0006CC6E
			public override string Name
			{
				get
				{
					return "borderBottomRightRadius";
				}
			}

			// Token: 0x1700084A RID: 2122
			// (get) Token: 0x06002079 RID: 8313 RVA: 0x0006EA75 File Offset: 0x0006CC75
			public override string ussName
			{
				get
				{
					return "border-bottom-right-radius";
				}
			}

			// Token: 0x1700084B RID: 2123
			// (get) Token: 0x0600207A RID: 8314 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600207B RID: 8315 RVA: 0x0007B066 File Offset: 0x00079266
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderBottomRightRadius;
			}

			// Token: 0x0600207C RID: 8316 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003F7 RID: 1015
		private class BorderBottomWidthProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x1700084C RID: 2124
			// (get) Token: 0x0600207E RID: 8318 RVA: 0x0006EA90 File Offset: 0x0006CC90
			public override string Name
			{
				get
				{
					return "borderBottomWidth";
				}
			}

			// Token: 0x1700084D RID: 2125
			// (get) Token: 0x0600207F RID: 8319 RVA: 0x0006EA97 File Offset: 0x0006CC97
			public override string ussName
			{
				get
				{
					return "border-bottom-width";
				}
			}

			// Token: 0x1700084E RID: 2126
			// (get) Token: 0x06002080 RID: 8320 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002081 RID: 8321 RVA: 0x0007B06F File Offset: 0x0007926F
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderBottomWidth;
			}

			// Token: 0x06002082 RID: 8322 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003F8 RID: 1016
		private class BorderLeftColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x1700084F RID: 2127
			// (get) Token: 0x06002084 RID: 8324 RVA: 0x0006EABB File Offset: 0x0006CCBB
			public override string Name
			{
				get
				{
					return "borderLeftColor";
				}
			}

			// Token: 0x17000850 RID: 2128
			// (get) Token: 0x06002085 RID: 8325 RVA: 0x0006EAC2 File Offset: 0x0006CCC2
			public override string ussName
			{
				get
				{
					return "border-left-color";
				}
			}

			// Token: 0x17000851 RID: 2129
			// (get) Token: 0x06002086 RID: 8326 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002087 RID: 8327 RVA: 0x0007B078 File Offset: 0x00079278
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderLeftColor;
			}

			// Token: 0x06002088 RID: 8328 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003F9 RID: 1017
		private class BorderLeftWidthProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000852 RID: 2130
			// (get) Token: 0x0600208A RID: 8330 RVA: 0x0006EADD File Offset: 0x0006CCDD
			public override string Name
			{
				get
				{
					return "borderLeftWidth";
				}
			}

			// Token: 0x17000853 RID: 2131
			// (get) Token: 0x0600208B RID: 8331 RVA: 0x0006EAE4 File Offset: 0x0006CCE4
			public override string ussName
			{
				get
				{
					return "border-left-width";
				}
			}

			// Token: 0x17000854 RID: 2132
			// (get) Token: 0x0600208C RID: 8332 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600208D RID: 8333 RVA: 0x0007B081 File Offset: 0x00079281
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderLeftWidth;
			}

			// Token: 0x0600208E RID: 8334 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003FA RID: 1018
		private class BorderRightColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x17000855 RID: 2133
			// (get) Token: 0x06002090 RID: 8336 RVA: 0x0006EAFF File Offset: 0x0006CCFF
			public override string Name
			{
				get
				{
					return "borderRightColor";
				}
			}

			// Token: 0x17000856 RID: 2134
			// (get) Token: 0x06002091 RID: 8337 RVA: 0x0006EB06 File Offset: 0x0006CD06
			public override string ussName
			{
				get
				{
					return "border-right-color";
				}
			}

			// Token: 0x17000857 RID: 2135
			// (get) Token: 0x06002092 RID: 8338 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002093 RID: 8339 RVA: 0x0007B08A File Offset: 0x0007928A
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderRightColor;
			}

			// Token: 0x06002094 RID: 8340 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003FB RID: 1019
		private class BorderRightWidthProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000858 RID: 2136
			// (get) Token: 0x06002096 RID: 8342 RVA: 0x0006EB21 File Offset: 0x0006CD21
			public override string Name
			{
				get
				{
					return "borderRightWidth";
				}
			}

			// Token: 0x17000859 RID: 2137
			// (get) Token: 0x06002097 RID: 8343 RVA: 0x0006EB28 File Offset: 0x0006CD28
			public override string ussName
			{
				get
				{
					return "border-right-width";
				}
			}

			// Token: 0x1700085A RID: 2138
			// (get) Token: 0x06002098 RID: 8344 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002099 RID: 8345 RVA: 0x0007B093 File Offset: 0x00079293
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderRightWidth;
			}

			// Token: 0x0600209A RID: 8346 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003FC RID: 1020
		private class BorderTopColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x1700085B RID: 2139
			// (get) Token: 0x0600209C RID: 8348 RVA: 0x0006EB43 File Offset: 0x0006CD43
			public override string Name
			{
				get
				{
					return "borderTopColor";
				}
			}

			// Token: 0x1700085C RID: 2140
			// (get) Token: 0x0600209D RID: 8349 RVA: 0x0006EB4A File Offset: 0x0006CD4A
			public override string ussName
			{
				get
				{
					return "border-top-color";
				}
			}

			// Token: 0x1700085D RID: 2141
			// (get) Token: 0x0600209E RID: 8350 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600209F RID: 8351 RVA: 0x0007B09C File Offset: 0x0007929C
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderTopColor;
			}

			// Token: 0x060020A0 RID: 8352 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003FD RID: 1021
		private class BorderTopLeftRadiusProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x1700085E RID: 2142
			// (get) Token: 0x060020A2 RID: 8354 RVA: 0x0006EB65 File Offset: 0x0006CD65
			public override string Name
			{
				get
				{
					return "borderTopLeftRadius";
				}
			}

			// Token: 0x1700085F RID: 2143
			// (get) Token: 0x060020A3 RID: 8355 RVA: 0x0006EB6C File Offset: 0x0006CD6C
			public override string ussName
			{
				get
				{
					return "border-top-left-radius";
				}
			}

			// Token: 0x17000860 RID: 2144
			// (get) Token: 0x060020A4 RID: 8356 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020A5 RID: 8357 RVA: 0x0007B0A5 File Offset: 0x000792A5
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderTopLeftRadius;
			}

			// Token: 0x060020A6 RID: 8358 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003FE RID: 1022
		private class BorderTopRightRadiusProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000861 RID: 2145
			// (get) Token: 0x060020A8 RID: 8360 RVA: 0x0006EB87 File Offset: 0x0006CD87
			public override string Name
			{
				get
				{
					return "borderTopRightRadius";
				}
			}

			// Token: 0x17000862 RID: 2146
			// (get) Token: 0x060020A9 RID: 8361 RVA: 0x0006EB8E File Offset: 0x0006CD8E
			public override string ussName
			{
				get
				{
					return "border-top-right-radius";
				}
			}

			// Token: 0x17000863 RID: 2147
			// (get) Token: 0x060020AA RID: 8362 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020AB RID: 8363 RVA: 0x0007B0AE File Offset: 0x000792AE
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderTopRightRadius;
			}

			// Token: 0x060020AC RID: 8364 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x020003FF RID: 1023
		private class BorderTopWidthProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000864 RID: 2148
			// (get) Token: 0x060020AE RID: 8366 RVA: 0x0006EBA9 File Offset: 0x0006CDA9
			public override string Name
			{
				get
				{
					return "borderTopWidth";
				}
			}

			// Token: 0x17000865 RID: 2149
			// (get) Token: 0x060020AF RID: 8367 RVA: 0x0006EBB0 File Offset: 0x0006CDB0
			public override string ussName
			{
				get
				{
					return "border-top-width";
				}
			}

			// Token: 0x17000866 RID: 2150
			// (get) Token: 0x060020B0 RID: 8368 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020B1 RID: 8369 RVA: 0x0007B0B7 File Offset: 0x000792B7
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).borderTopWidth;
			}

			// Token: 0x060020B2 RID: 8370 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000400 RID: 1024
		private class BottomProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000867 RID: 2151
			// (get) Token: 0x060020B4 RID: 8372 RVA: 0x0006EBCB File Offset: 0x0006CDCB
			public override string Name
			{
				get
				{
					return "bottom";
				}
			}

			// Token: 0x17000868 RID: 2152
			// (get) Token: 0x060020B5 RID: 8373 RVA: 0x0006EBCB File Offset: 0x0006CDCB
			public override string ussName
			{
				get
				{
					return "bottom";
				}
			}

			// Token: 0x17000869 RID: 2153
			// (get) Token: 0x060020B6 RID: 8374 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020B7 RID: 8375 RVA: 0x0007B0C0 File Offset: 0x000792C0
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).bottom;
			}

			// Token: 0x060020B8 RID: 8376 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000401 RID: 1025
		private class ColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x1700086A RID: 2154
			// (get) Token: 0x060020BA RID: 8378 RVA: 0x0006EBE6 File Offset: 0x0006CDE6
			public override string Name
			{
				get
				{
					return "color";
				}
			}

			// Token: 0x1700086B RID: 2155
			// (get) Token: 0x060020BB RID: 8379 RVA: 0x0006EBE6 File Offset: 0x0006CDE6
			public override string ussName
			{
				get
				{
					return "color";
				}
			}

			// Token: 0x1700086C RID: 2156
			// (get) Token: 0x060020BC RID: 8380 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020BD RID: 8381 RVA: 0x0007B0C9 File Offset: 0x000792C9
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).color;
			}

			// Token: 0x060020BE RID: 8382 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000402 RID: 1026
		private class DisplayProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<DisplayStyle>
		{
			// Token: 0x1700086D RID: 2157
			// (get) Token: 0x060020C0 RID: 8384 RVA: 0x0006EC25 File Offset: 0x0006CE25
			public override string Name
			{
				get
				{
					return "display";
				}
			}

			// Token: 0x1700086E RID: 2158
			// (get) Token: 0x060020C1 RID: 8385 RVA: 0x0006EC25 File Offset: 0x0006CE25
			public override string ussName
			{
				get
				{
					return "display";
				}
			}

			// Token: 0x1700086F RID: 2159
			// (get) Token: 0x060020C2 RID: 8386 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020C3 RID: 8387 RVA: 0x0007B0D2 File Offset: 0x000792D2
			public override DisplayStyle GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).display;
			}

			// Token: 0x060020C4 RID: 8388 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, DisplayStyle value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000403 RID: 1027
		private class FlexBasisProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleFloatProperty
		{
			// Token: 0x17000870 RID: 2160
			// (get) Token: 0x060020C6 RID: 8390 RVA: 0x0006EC49 File Offset: 0x0006CE49
			public override string Name
			{
				get
				{
					return "flexBasis";
				}
			}

			// Token: 0x17000871 RID: 2161
			// (get) Token: 0x060020C7 RID: 8391 RVA: 0x0006EC50 File Offset: 0x0006CE50
			public override string ussName
			{
				get
				{
					return "flex-basis";
				}
			}

			// Token: 0x17000872 RID: 2162
			// (get) Token: 0x060020C8 RID: 8392 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020C9 RID: 8393 RVA: 0x0007B0E4 File Offset: 0x000792E4
			public override StyleFloat GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).flexBasis;
			}

			// Token: 0x060020CA RID: 8394 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, StyleFloat value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000404 RID: 1028
		private class FlexDirectionProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<FlexDirection>
		{
			// Token: 0x17000873 RID: 2163
			// (get) Token: 0x060020CC RID: 8396 RVA: 0x0006EC6B File Offset: 0x0006CE6B
			public override string Name
			{
				get
				{
					return "flexDirection";
				}
			}

			// Token: 0x17000874 RID: 2164
			// (get) Token: 0x060020CD RID: 8397 RVA: 0x0006EC72 File Offset: 0x0006CE72
			public override string ussName
			{
				get
				{
					return "flex-direction";
				}
			}

			// Token: 0x17000875 RID: 2165
			// (get) Token: 0x060020CE RID: 8398 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020CF RID: 8399 RVA: 0x0007B0F6 File Offset: 0x000792F6
			public override FlexDirection GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).flexDirection;
			}

			// Token: 0x060020D0 RID: 8400 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, FlexDirection value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000405 RID: 1029
		private class FlexGrowProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000876 RID: 2166
			// (get) Token: 0x060020D2 RID: 8402 RVA: 0x0006EC96 File Offset: 0x0006CE96
			public override string Name
			{
				get
				{
					return "flexGrow";
				}
			}

			// Token: 0x17000877 RID: 2167
			// (get) Token: 0x060020D3 RID: 8403 RVA: 0x0006EC9D File Offset: 0x0006CE9D
			public override string ussName
			{
				get
				{
					return "flex-grow";
				}
			}

			// Token: 0x17000878 RID: 2168
			// (get) Token: 0x060020D4 RID: 8404 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020D5 RID: 8405 RVA: 0x0007B108 File Offset: 0x00079308
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).flexGrow;
			}

			// Token: 0x060020D6 RID: 8406 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000406 RID: 1030
		private class FlexShrinkProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000879 RID: 2169
			// (get) Token: 0x060020D8 RID: 8408 RVA: 0x0006ECB8 File Offset: 0x0006CEB8
			public override string Name
			{
				get
				{
					return "flexShrink";
				}
			}

			// Token: 0x1700087A RID: 2170
			// (get) Token: 0x060020D9 RID: 8409 RVA: 0x0006ECBF File Offset: 0x0006CEBF
			public override string ussName
			{
				get
				{
					return "flex-shrink";
				}
			}

			// Token: 0x1700087B RID: 2171
			// (get) Token: 0x060020DA RID: 8410 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020DB RID: 8411 RVA: 0x0007B111 File Offset: 0x00079311
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).flexShrink;
			}

			// Token: 0x060020DC RID: 8412 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000407 RID: 1031
		private class FlexWrapProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<Wrap>
		{
			// Token: 0x1700087C RID: 2172
			// (get) Token: 0x060020DE RID: 8414 RVA: 0x0006ECDA File Offset: 0x0006CEDA
			public override string Name
			{
				get
				{
					return "flexWrap";
				}
			}

			// Token: 0x1700087D RID: 2173
			// (get) Token: 0x060020DF RID: 8415 RVA: 0x0006ECE1 File Offset: 0x0006CEE1
			public override string ussName
			{
				get
				{
					return "flex-wrap";
				}
			}

			// Token: 0x1700087E RID: 2174
			// (get) Token: 0x060020E0 RID: 8416 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020E1 RID: 8417 RVA: 0x0007B11A File Offset: 0x0007931A
			public override Wrap GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).flexWrap;
			}

			// Token: 0x060020E2 RID: 8418 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Wrap value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000408 RID: 1032
		private class FontSizeProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x1700087F RID: 2175
			// (get) Token: 0x060020E4 RID: 8420 RVA: 0x0006ED05 File Offset: 0x0006CF05
			public override string Name
			{
				get
				{
					return "fontSize";
				}
			}

			// Token: 0x17000880 RID: 2176
			// (get) Token: 0x060020E5 RID: 8421 RVA: 0x0006ED0C File Offset: 0x0006CF0C
			public override string ussName
			{
				get
				{
					return "font-size";
				}
			}

			// Token: 0x17000881 RID: 2177
			// (get) Token: 0x060020E6 RID: 8422 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020E7 RID: 8423 RVA: 0x0007B12C File Offset: 0x0007932C
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).fontSize;
			}

			// Token: 0x060020E8 RID: 8424 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000409 RID: 1033
		private class HeightProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000882 RID: 2178
			// (get) Token: 0x060020EA RID: 8426 RVA: 0x0006ED27 File Offset: 0x0006CF27
			public override string Name
			{
				get
				{
					return "height";
				}
			}

			// Token: 0x17000883 RID: 2179
			// (get) Token: 0x060020EB RID: 8427 RVA: 0x0006ED27 File Offset: 0x0006CF27
			public override string ussName
			{
				get
				{
					return "height";
				}
			}

			// Token: 0x17000884 RID: 2180
			// (get) Token: 0x060020EC RID: 8428 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020ED RID: 8429 RVA: 0x0007B135 File Offset: 0x00079335
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).height;
			}

			// Token: 0x060020EE RID: 8430 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200040A RID: 1034
		private class JustifyContentProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<Justify>
		{
			// Token: 0x17000885 RID: 2181
			// (get) Token: 0x060020F0 RID: 8432 RVA: 0x0006ED42 File Offset: 0x0006CF42
			public override string Name
			{
				get
				{
					return "justifyContent";
				}
			}

			// Token: 0x17000886 RID: 2182
			// (get) Token: 0x060020F1 RID: 8433 RVA: 0x0006ED49 File Offset: 0x0006CF49
			public override string ussName
			{
				get
				{
					return "justify-content";
				}
			}

			// Token: 0x17000887 RID: 2183
			// (get) Token: 0x060020F2 RID: 8434 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020F3 RID: 8435 RVA: 0x0007B13E File Offset: 0x0007933E
			public override Justify GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).justifyContent;
			}

			// Token: 0x060020F4 RID: 8436 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Justify value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200040B RID: 1035
		private class LeftProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000888 RID: 2184
			// (get) Token: 0x060020F6 RID: 8438 RVA: 0x0006ED6D File Offset: 0x0006CF6D
			public override string Name
			{
				get
				{
					return "left";
				}
			}

			// Token: 0x17000889 RID: 2185
			// (get) Token: 0x060020F7 RID: 8439 RVA: 0x0006ED6D File Offset: 0x0006CF6D
			public override string ussName
			{
				get
				{
					return "left";
				}
			}

			// Token: 0x1700088A RID: 2186
			// (get) Token: 0x060020F8 RID: 8440 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020F9 RID: 8441 RVA: 0x0007B150 File Offset: 0x00079350
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).left;
			}

			// Token: 0x060020FA RID: 8442 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200040C RID: 1036
		private class LetterSpacingProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x1700088B RID: 2187
			// (get) Token: 0x060020FC RID: 8444 RVA: 0x0006ED88 File Offset: 0x0006CF88
			public override string Name
			{
				get
				{
					return "letterSpacing";
				}
			}

			// Token: 0x1700088C RID: 2188
			// (get) Token: 0x060020FD RID: 8445 RVA: 0x0006ED8F File Offset: 0x0006CF8F
			public override string ussName
			{
				get
				{
					return "letter-spacing";
				}
			}

			// Token: 0x1700088D RID: 2189
			// (get) Token: 0x060020FE RID: 8446 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060020FF RID: 8447 RVA: 0x0007B159 File Offset: 0x00079359
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).letterSpacing;
			}

			// Token: 0x06002100 RID: 8448 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200040D RID: 1037
		private class MarginBottomProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x1700088E RID: 2190
			// (get) Token: 0x06002102 RID: 8450 RVA: 0x0006EDAA File Offset: 0x0006CFAA
			public override string Name
			{
				get
				{
					return "marginBottom";
				}
			}

			// Token: 0x1700088F RID: 2191
			// (get) Token: 0x06002103 RID: 8451 RVA: 0x0006EDB1 File Offset: 0x0006CFB1
			public override string ussName
			{
				get
				{
					return "margin-bottom";
				}
			}

			// Token: 0x17000890 RID: 2192
			// (get) Token: 0x06002104 RID: 8452 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002105 RID: 8453 RVA: 0x0007B162 File Offset: 0x00079362
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).marginBottom;
			}

			// Token: 0x06002106 RID: 8454 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200040E RID: 1038
		private class MarginLeftProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000891 RID: 2193
			// (get) Token: 0x06002108 RID: 8456 RVA: 0x0006EDCC File Offset: 0x0006CFCC
			public override string Name
			{
				get
				{
					return "marginLeft";
				}
			}

			// Token: 0x17000892 RID: 2194
			// (get) Token: 0x06002109 RID: 8457 RVA: 0x0006EDD3 File Offset: 0x0006CFD3
			public override string ussName
			{
				get
				{
					return "margin-left";
				}
			}

			// Token: 0x17000893 RID: 2195
			// (get) Token: 0x0600210A RID: 8458 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600210B RID: 8459 RVA: 0x0007B16B File Offset: 0x0007936B
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).marginLeft;
			}

			// Token: 0x0600210C RID: 8460 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200040F RID: 1039
		private class MarginRightProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000894 RID: 2196
			// (get) Token: 0x0600210E RID: 8462 RVA: 0x0006EDEE File Offset: 0x0006CFEE
			public override string Name
			{
				get
				{
					return "marginRight";
				}
			}

			// Token: 0x17000895 RID: 2197
			// (get) Token: 0x0600210F RID: 8463 RVA: 0x0006EDF5 File Offset: 0x0006CFF5
			public override string ussName
			{
				get
				{
					return "margin-right";
				}
			}

			// Token: 0x17000896 RID: 2198
			// (get) Token: 0x06002110 RID: 8464 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002111 RID: 8465 RVA: 0x0007B174 File Offset: 0x00079374
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).marginRight;
			}

			// Token: 0x06002112 RID: 8466 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000410 RID: 1040
		private class MarginTopProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000897 RID: 2199
			// (get) Token: 0x06002114 RID: 8468 RVA: 0x0006EE10 File Offset: 0x0006D010
			public override string Name
			{
				get
				{
					return "marginTop";
				}
			}

			// Token: 0x17000898 RID: 2200
			// (get) Token: 0x06002115 RID: 8469 RVA: 0x0006EE17 File Offset: 0x0006D017
			public override string ussName
			{
				get
				{
					return "margin-top";
				}
			}

			// Token: 0x17000899 RID: 2201
			// (get) Token: 0x06002116 RID: 8470 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002117 RID: 8471 RVA: 0x0007B17D File Offset: 0x0007937D
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).marginTop;
			}

			// Token: 0x06002118 RID: 8472 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000411 RID: 1041
		private class MaxHeightProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleFloatProperty
		{
			// Token: 0x1700089A RID: 2202
			// (get) Token: 0x0600211A RID: 8474 RVA: 0x0006EE32 File Offset: 0x0006D032
			public override string Name
			{
				get
				{
					return "maxHeight";
				}
			}

			// Token: 0x1700089B RID: 2203
			// (get) Token: 0x0600211B RID: 8475 RVA: 0x0006EE39 File Offset: 0x0006D039
			public override string ussName
			{
				get
				{
					return "max-height";
				}
			}

			// Token: 0x1700089C RID: 2204
			// (get) Token: 0x0600211C RID: 8476 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600211D RID: 8477 RVA: 0x0007B186 File Offset: 0x00079386
			public override StyleFloat GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).maxHeight;
			}

			// Token: 0x0600211E RID: 8478 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, StyleFloat value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000412 RID: 1042
		private class MaxWidthProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleFloatProperty
		{
			// Token: 0x1700089D RID: 2205
			// (get) Token: 0x06002120 RID: 8480 RVA: 0x0006EE54 File Offset: 0x0006D054
			public override string Name
			{
				get
				{
					return "maxWidth";
				}
			}

			// Token: 0x1700089E RID: 2206
			// (get) Token: 0x06002121 RID: 8481 RVA: 0x0006EE5B File Offset: 0x0006D05B
			public override string ussName
			{
				get
				{
					return "max-width";
				}
			}

			// Token: 0x1700089F RID: 2207
			// (get) Token: 0x06002122 RID: 8482 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002123 RID: 8483 RVA: 0x0007B18F File Offset: 0x0007938F
			public override StyleFloat GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).maxWidth;
			}

			// Token: 0x06002124 RID: 8484 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, StyleFloat value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000413 RID: 1043
		private class MinHeightProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleFloatProperty
		{
			// Token: 0x170008A0 RID: 2208
			// (get) Token: 0x06002126 RID: 8486 RVA: 0x0006EE76 File Offset: 0x0006D076
			public override string Name
			{
				get
				{
					return "minHeight";
				}
			}

			// Token: 0x170008A1 RID: 2209
			// (get) Token: 0x06002127 RID: 8487 RVA: 0x0006EE7D File Offset: 0x0006D07D
			public override string ussName
			{
				get
				{
					return "min-height";
				}
			}

			// Token: 0x170008A2 RID: 2210
			// (get) Token: 0x06002128 RID: 8488 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002129 RID: 8489 RVA: 0x0007B198 File Offset: 0x00079398
			public override StyleFloat GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).minHeight;
			}

			// Token: 0x0600212A RID: 8490 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, StyleFloat value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000414 RID: 1044
		private class MinWidthProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleFloatProperty
		{
			// Token: 0x170008A3 RID: 2211
			// (get) Token: 0x0600212C RID: 8492 RVA: 0x0006EE98 File Offset: 0x0006D098
			public override string Name
			{
				get
				{
					return "minWidth";
				}
			}

			// Token: 0x170008A4 RID: 2212
			// (get) Token: 0x0600212D RID: 8493 RVA: 0x0006EE9F File Offset: 0x0006D09F
			public override string ussName
			{
				get
				{
					return "min-width";
				}
			}

			// Token: 0x170008A5 RID: 2213
			// (get) Token: 0x0600212E RID: 8494 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600212F RID: 8495 RVA: 0x0007B1A1 File Offset: 0x000793A1
			public override StyleFloat GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).minWidth;
			}

			// Token: 0x06002130 RID: 8496 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, StyleFloat value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000415 RID: 1045
		private class OpacityProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170008A6 RID: 2214
			// (get) Token: 0x06002132 RID: 8498 RVA: 0x0006EEBA File Offset: 0x0006D0BA
			public override string Name
			{
				get
				{
					return "opacity";
				}
			}

			// Token: 0x170008A7 RID: 2215
			// (get) Token: 0x06002133 RID: 8499 RVA: 0x0006EEBA File Offset: 0x0006D0BA
			public override string ussName
			{
				get
				{
					return "opacity";
				}
			}

			// Token: 0x170008A8 RID: 2216
			// (get) Token: 0x06002134 RID: 8500 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002135 RID: 8501 RVA: 0x0007B1AA File Offset: 0x000793AA
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).opacity;
			}

			// Token: 0x06002136 RID: 8502 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000416 RID: 1046
		private class PaddingBottomProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170008A9 RID: 2217
			// (get) Token: 0x06002138 RID: 8504 RVA: 0x0006EEF9 File Offset: 0x0006D0F9
			public override string Name
			{
				get
				{
					return "paddingBottom";
				}
			}

			// Token: 0x170008AA RID: 2218
			// (get) Token: 0x06002139 RID: 8505 RVA: 0x0006EF00 File Offset: 0x0006D100
			public override string ussName
			{
				get
				{
					return "padding-bottom";
				}
			}

			// Token: 0x170008AB RID: 2219
			// (get) Token: 0x0600213A RID: 8506 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600213B RID: 8507 RVA: 0x0007B1B3 File Offset: 0x000793B3
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).paddingBottom;
			}

			// Token: 0x0600213C RID: 8508 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000417 RID: 1047
		private class PaddingLeftProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170008AC RID: 2220
			// (get) Token: 0x0600213E RID: 8510 RVA: 0x0006EF1B File Offset: 0x0006D11B
			public override string Name
			{
				get
				{
					return "paddingLeft";
				}
			}

			// Token: 0x170008AD RID: 2221
			// (get) Token: 0x0600213F RID: 8511 RVA: 0x0006EF22 File Offset: 0x0006D122
			public override string ussName
			{
				get
				{
					return "padding-left";
				}
			}

			// Token: 0x170008AE RID: 2222
			// (get) Token: 0x06002140 RID: 8512 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002141 RID: 8513 RVA: 0x0007B1BC File Offset: 0x000793BC
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).paddingLeft;
			}

			// Token: 0x06002142 RID: 8514 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000418 RID: 1048
		private class PaddingRightProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170008AF RID: 2223
			// (get) Token: 0x06002144 RID: 8516 RVA: 0x0006EF3D File Offset: 0x0006D13D
			public override string Name
			{
				get
				{
					return "paddingRight";
				}
			}

			// Token: 0x170008B0 RID: 2224
			// (get) Token: 0x06002145 RID: 8517 RVA: 0x0006EF44 File Offset: 0x0006D144
			public override string ussName
			{
				get
				{
					return "padding-right";
				}
			}

			// Token: 0x170008B1 RID: 2225
			// (get) Token: 0x06002146 RID: 8518 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002147 RID: 8519 RVA: 0x0007B1C5 File Offset: 0x000793C5
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).paddingRight;
			}

			// Token: 0x06002148 RID: 8520 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000419 RID: 1049
		private class PaddingTopProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170008B2 RID: 2226
			// (get) Token: 0x0600214A RID: 8522 RVA: 0x0006EF5F File Offset: 0x0006D15F
			public override string Name
			{
				get
				{
					return "paddingTop";
				}
			}

			// Token: 0x170008B3 RID: 2227
			// (get) Token: 0x0600214B RID: 8523 RVA: 0x0006EF66 File Offset: 0x0006D166
			public override string ussName
			{
				get
				{
					return "padding-top";
				}
			}

			// Token: 0x170008B4 RID: 2228
			// (get) Token: 0x0600214C RID: 8524 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600214D RID: 8525 RVA: 0x0007B1CE File Offset: 0x000793CE
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).paddingTop;
			}

			// Token: 0x0600214E RID: 8526 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200041A RID: 1050
		private class PositionProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<Position>
		{
			// Token: 0x170008B5 RID: 2229
			// (get) Token: 0x06002150 RID: 8528 RVA: 0x0006EF81 File Offset: 0x0006D181
			public override string Name
			{
				get
				{
					return "position";
				}
			}

			// Token: 0x170008B6 RID: 2230
			// (get) Token: 0x06002151 RID: 8529 RVA: 0x0006EF81 File Offset: 0x0006D181
			public override string ussName
			{
				get
				{
					return "position";
				}
			}

			// Token: 0x170008B7 RID: 2231
			// (get) Token: 0x06002152 RID: 8530 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002153 RID: 8531 RVA: 0x0007B1D7 File Offset: 0x000793D7
			public override Position GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).position;
			}

			// Token: 0x06002154 RID: 8532 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Position value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200041B RID: 1051
		private class RightProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170008B8 RID: 2232
			// (get) Token: 0x06002156 RID: 8534 RVA: 0x0006EFA5 File Offset: 0x0006D1A5
			public override string Name
			{
				get
				{
					return "right";
				}
			}

			// Token: 0x170008B9 RID: 2233
			// (get) Token: 0x06002157 RID: 8535 RVA: 0x0006EFA5 File Offset: 0x0006D1A5
			public override string ussName
			{
				get
				{
					return "right";
				}
			}

			// Token: 0x170008BA RID: 2234
			// (get) Token: 0x06002158 RID: 8536 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002159 RID: 8537 RVA: 0x0007B1E9 File Offset: 0x000793E9
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).right;
			}

			// Token: 0x0600215A RID: 8538 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200041C RID: 1052
		private class RotateProperty : ResolvedStyleAccessPropertyBag.ResolvedRotateProperty
		{
			// Token: 0x170008BB RID: 2235
			// (get) Token: 0x0600215C RID: 8540 RVA: 0x0006EFC0 File Offset: 0x0006D1C0
			public override string Name
			{
				get
				{
					return "rotate";
				}
			}

			// Token: 0x170008BC RID: 2236
			// (get) Token: 0x0600215D RID: 8541 RVA: 0x0006EFC0 File Offset: 0x0006D1C0
			public override string ussName
			{
				get
				{
					return "rotate";
				}
			}

			// Token: 0x170008BD RID: 2237
			// (get) Token: 0x0600215E RID: 8542 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600215F RID: 8543 RVA: 0x0007B1F2 File Offset: 0x000793F2
			public override Rotate GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).rotate;
			}

			// Token: 0x06002160 RID: 8544 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Rotate value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200041D RID: 1053
		private class ScaleProperty : ResolvedStyleAccessPropertyBag.ResolvedScaleProperty
		{
			// Token: 0x170008BE RID: 2238
			// (get) Token: 0x06002162 RID: 8546 RVA: 0x0006EFE4 File Offset: 0x0006D1E4
			public override string Name
			{
				get
				{
					return "scale";
				}
			}

			// Token: 0x170008BF RID: 2239
			// (get) Token: 0x06002163 RID: 8547 RVA: 0x0006EFE4 File Offset: 0x0006D1E4
			public override string ussName
			{
				get
				{
					return "scale";
				}
			}

			// Token: 0x170008C0 RID: 2240
			// (get) Token: 0x06002164 RID: 8548 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002165 RID: 8549 RVA: 0x0007B204 File Offset: 0x00079404
			public override Scale GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).scale;
			}

			// Token: 0x06002166 RID: 8550 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Scale value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200041E RID: 1054
		private class TextOverflowProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<TextOverflow>
		{
			// Token: 0x170008C1 RID: 2241
			// (get) Token: 0x06002168 RID: 8552 RVA: 0x0006F008 File Offset: 0x0006D208
			public override string Name
			{
				get
				{
					return "textOverflow";
				}
			}

			// Token: 0x170008C2 RID: 2242
			// (get) Token: 0x06002169 RID: 8553 RVA: 0x0006F00F File Offset: 0x0006D20F
			public override string ussName
			{
				get
				{
					return "text-overflow";
				}
			}

			// Token: 0x170008C3 RID: 2243
			// (get) Token: 0x0600216A RID: 8554 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600216B RID: 8555 RVA: 0x0007B216 File Offset: 0x00079416
			public override TextOverflow GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).textOverflow;
			}

			// Token: 0x0600216C RID: 8556 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, TextOverflow value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200041F RID: 1055
		private class TopProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170008C4 RID: 2244
			// (get) Token: 0x0600216E RID: 8558 RVA: 0x0006F05E File Offset: 0x0006D25E
			public override string Name
			{
				get
				{
					return "top";
				}
			}

			// Token: 0x170008C5 RID: 2245
			// (get) Token: 0x0600216F RID: 8559 RVA: 0x0006F05E File Offset: 0x0006D25E
			public override string ussName
			{
				get
				{
					return "top";
				}
			}

			// Token: 0x170008C6 RID: 2246
			// (get) Token: 0x06002170 RID: 8560 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002171 RID: 8561 RVA: 0x0007B228 File Offset: 0x00079428
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).top;
			}

			// Token: 0x06002172 RID: 8562 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000420 RID: 1056
		private class TransformOriginProperty : ResolvedStyleAccessPropertyBag.ResolvedVector3Property
		{
			// Token: 0x170008C7 RID: 2247
			// (get) Token: 0x06002174 RID: 8564 RVA: 0x0006F079 File Offset: 0x0006D279
			public override string Name
			{
				get
				{
					return "transformOrigin";
				}
			}

			// Token: 0x170008C8 RID: 2248
			// (get) Token: 0x06002175 RID: 8565 RVA: 0x0006F080 File Offset: 0x0006D280
			public override string ussName
			{
				get
				{
					return "transform-origin";
				}
			}

			// Token: 0x170008C9 RID: 2249
			// (get) Token: 0x06002176 RID: 8566 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002177 RID: 8567 RVA: 0x0007B231 File Offset: 0x00079431
			public override Vector3 GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).transformOrigin;
			}

			// Token: 0x06002178 RID: 8568 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Vector3 value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000421 RID: 1057
		private class TransitionDelayProperty : ResolvedStyleAccessPropertyBag.ResolvedListProperty<TimeValue>
		{
			// Token: 0x170008CA RID: 2250
			// (get) Token: 0x0600217A RID: 8570 RVA: 0x0006F0A4 File Offset: 0x0006D2A4
			public override string Name
			{
				get
				{
					return "transitionDelay";
				}
			}

			// Token: 0x170008CB RID: 2251
			// (get) Token: 0x0600217B RID: 8571 RVA: 0x0006F0AB File Offset: 0x0006D2AB
			public override string ussName
			{
				get
				{
					return "transition-delay";
				}
			}

			// Token: 0x170008CC RID: 2252
			// (get) Token: 0x0600217C RID: 8572 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600217D RID: 8573 RVA: 0x0007B243 File Offset: 0x00079443
			public override IEnumerable<TimeValue> GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).transitionDelay;
			}

			// Token: 0x0600217E RID: 8574 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, IEnumerable<TimeValue> value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000422 RID: 1058
		private class TransitionDurationProperty : ResolvedStyleAccessPropertyBag.ResolvedListProperty<TimeValue>
		{
			// Token: 0x170008CD RID: 2253
			// (get) Token: 0x06002180 RID: 8576 RVA: 0x0006F0CF File Offset: 0x0006D2CF
			public override string Name
			{
				get
				{
					return "transitionDuration";
				}
			}

			// Token: 0x170008CE RID: 2254
			// (get) Token: 0x06002181 RID: 8577 RVA: 0x0006F0D6 File Offset: 0x0006D2D6
			public override string ussName
			{
				get
				{
					return "transition-duration";
				}
			}

			// Token: 0x170008CF RID: 2255
			// (get) Token: 0x06002182 RID: 8578 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002183 RID: 8579 RVA: 0x0007B255 File Offset: 0x00079455
			public override IEnumerable<TimeValue> GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).transitionDuration;
			}

			// Token: 0x06002184 RID: 8580 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, IEnumerable<TimeValue> value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000423 RID: 1059
		private class TransitionPropertyProperty : ResolvedStyleAccessPropertyBag.ResolvedListProperty<StylePropertyName>
		{
			// Token: 0x170008D0 RID: 2256
			// (get) Token: 0x06002186 RID: 8582 RVA: 0x0006F0F1 File Offset: 0x0006D2F1
			public override string Name
			{
				get
				{
					return "transitionProperty";
				}
			}

			// Token: 0x170008D1 RID: 2257
			// (get) Token: 0x06002187 RID: 8583 RVA: 0x0006F0F8 File Offset: 0x0006D2F8
			public override string ussName
			{
				get
				{
					return "transition-property";
				}
			}

			// Token: 0x170008D2 RID: 2258
			// (get) Token: 0x06002188 RID: 8584 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002189 RID: 8585 RVA: 0x0007B25E File Offset: 0x0007945E
			public override IEnumerable<StylePropertyName> GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).transitionProperty;
			}

			// Token: 0x0600218A RID: 8586 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, IEnumerable<StylePropertyName> value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000424 RID: 1060
		private class TransitionTimingFunctionProperty : ResolvedStyleAccessPropertyBag.ResolvedListProperty<EasingFunction>
		{
			// Token: 0x170008D3 RID: 2259
			// (get) Token: 0x0600218C RID: 8588 RVA: 0x0006F11C File Offset: 0x0006D31C
			public override string Name
			{
				get
				{
					return "transitionTimingFunction";
				}
			}

			// Token: 0x170008D4 RID: 2260
			// (get) Token: 0x0600218D RID: 8589 RVA: 0x0006F123 File Offset: 0x0006D323
			public override string ussName
			{
				get
				{
					return "transition-timing-function";
				}
			}

			// Token: 0x170008D5 RID: 2261
			// (get) Token: 0x0600218E RID: 8590 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600218F RID: 8591 RVA: 0x0007B270 File Offset: 0x00079470
			public override IEnumerable<EasingFunction> GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).transitionTimingFunction;
			}

			// Token: 0x06002190 RID: 8592 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, IEnumerable<EasingFunction> value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000425 RID: 1061
		private class TranslateProperty : ResolvedStyleAccessPropertyBag.ResolvedVector3Property
		{
			// Token: 0x170008D6 RID: 2262
			// (get) Token: 0x06002192 RID: 8594 RVA: 0x0006F147 File Offset: 0x0006D347
			public override string Name
			{
				get
				{
					return "translate";
				}
			}

			// Token: 0x170008D7 RID: 2263
			// (get) Token: 0x06002193 RID: 8595 RVA: 0x0006F147 File Offset: 0x0006D347
			public override string ussName
			{
				get
				{
					return "translate";
				}
			}

			// Token: 0x170008D8 RID: 2264
			// (get) Token: 0x06002194 RID: 8596 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002195 RID: 8597 RVA: 0x0007B282 File Offset: 0x00079482
			public override Vector3 GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).translate;
			}

			// Token: 0x06002196 RID: 8598 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Vector3 value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000426 RID: 1062
		private class UnityBackgroundImageTintColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x170008D9 RID: 2265
			// (get) Token: 0x06002198 RID: 8600 RVA: 0x0006F16B File Offset: 0x0006D36B
			public override string Name
			{
				get
				{
					return "unityBackgroundImageTintColor";
				}
			}

			// Token: 0x170008DA RID: 2266
			// (get) Token: 0x06002199 RID: 8601 RVA: 0x0006F172 File Offset: 0x0006D372
			public override string ussName
			{
				get
				{
					return "-unity-background-image-tint-color";
				}
			}

			// Token: 0x170008DB RID: 2267
			// (get) Token: 0x0600219A RID: 8602 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600219B RID: 8603 RVA: 0x0007B28B File Offset: 0x0007948B
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityBackgroundImageTintColor;
			}

			// Token: 0x0600219C RID: 8604 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000427 RID: 1063
		private class UnityEditorTextRenderingModeProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<EditorTextRenderingMode>
		{
			// Token: 0x170008DC RID: 2268
			// (get) Token: 0x0600219E RID: 8606 RVA: 0x0006F18D File Offset: 0x0006D38D
			public override string Name
			{
				get
				{
					return "unityEditorTextRenderingMode";
				}
			}

			// Token: 0x170008DD RID: 2269
			// (get) Token: 0x0600219F RID: 8607 RVA: 0x0006F194 File Offset: 0x0006D394
			public override string ussName
			{
				get
				{
					return "-unity-editor-text-rendering-mode";
				}
			}

			// Token: 0x170008DE RID: 2270
			// (get) Token: 0x060021A0 RID: 8608 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021A1 RID: 8609 RVA: 0x0007B294 File Offset: 0x00079494
			public override EditorTextRenderingMode GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityEditorTextRenderingMode;
			}

			// Token: 0x060021A2 RID: 8610 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, EditorTextRenderingMode value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000428 RID: 1064
		private class UnityFontProperty : ResolvedStyleAccessPropertyBag.ResolvedFontProperty
		{
			// Token: 0x170008DF RID: 2271
			// (get) Token: 0x060021A4 RID: 8612 RVA: 0x0006F1B8 File Offset: 0x0006D3B8
			public override string Name
			{
				get
				{
					return "unityFont";
				}
			}

			// Token: 0x170008E0 RID: 2272
			// (get) Token: 0x060021A5 RID: 8613 RVA: 0x0006F1BF File Offset: 0x0006D3BF
			public override string ussName
			{
				get
				{
					return "-unity-font";
				}
			}

			// Token: 0x170008E1 RID: 2273
			// (get) Token: 0x060021A6 RID: 8614 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021A7 RID: 8615 RVA: 0x0007B2A6 File Offset: 0x000794A6
			public override Font GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityFont;
			}

			// Token: 0x060021A8 RID: 8616 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Font value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000429 RID: 1065
		private class UnityFontDefinitionProperty : ResolvedStyleAccessPropertyBag.ResolvedFontDefinitionProperty
		{
			// Token: 0x170008E2 RID: 2274
			// (get) Token: 0x060021AA RID: 8618 RVA: 0x0006F1E3 File Offset: 0x0006D3E3
			public override string Name
			{
				get
				{
					return "unityFontDefinition";
				}
			}

			// Token: 0x170008E3 RID: 2275
			// (get) Token: 0x060021AB RID: 8619 RVA: 0x0006F1EA File Offset: 0x0006D3EA
			public override string ussName
			{
				get
				{
					return "-unity-font-definition";
				}
			}

			// Token: 0x170008E4 RID: 2276
			// (get) Token: 0x060021AC RID: 8620 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021AD RID: 8621 RVA: 0x0007B2B8 File Offset: 0x000794B8
			public override FontDefinition GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityFontDefinition;
			}

			// Token: 0x060021AE RID: 8622 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, FontDefinition value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200042A RID: 1066
		private class UnityFontStyleAndWeightProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<FontStyle>
		{
			// Token: 0x170008E5 RID: 2277
			// (get) Token: 0x060021B0 RID: 8624 RVA: 0x0006F20E File Offset: 0x0006D40E
			public override string Name
			{
				get
				{
					return "unityFontStyleAndWeight";
				}
			}

			// Token: 0x170008E6 RID: 2278
			// (get) Token: 0x060021B1 RID: 8625 RVA: 0x0006F215 File Offset: 0x0006D415
			public override string ussName
			{
				get
				{
					return "-unity-font-style";
				}
			}

			// Token: 0x170008E7 RID: 2279
			// (get) Token: 0x060021B2 RID: 8626 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021B3 RID: 8627 RVA: 0x0007B2CA File Offset: 0x000794CA
			public override FontStyle GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityFontStyleAndWeight;
			}

			// Token: 0x060021B4 RID: 8628 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, FontStyle value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200042B RID: 1067
		private class UnityParagraphSpacingProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170008E8 RID: 2280
			// (get) Token: 0x060021B6 RID: 8630 RVA: 0x0006F264 File Offset: 0x0006D464
			public override string Name
			{
				get
				{
					return "unityParagraphSpacing";
				}
			}

			// Token: 0x170008E9 RID: 2281
			// (get) Token: 0x060021B7 RID: 8631 RVA: 0x0006F26B File Offset: 0x0006D46B
			public override string ussName
			{
				get
				{
					return "-unity-paragraph-spacing";
				}
			}

			// Token: 0x170008EA RID: 2282
			// (get) Token: 0x060021B8 RID: 8632 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021B9 RID: 8633 RVA: 0x0007B2DC File Offset: 0x000794DC
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityParagraphSpacing;
			}

			// Token: 0x060021BA RID: 8634 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200042C RID: 1068
		private class UnitySliceBottomProperty : ResolvedStyleAccessPropertyBag.ResolvedIntProperty
		{
			// Token: 0x170008EB RID: 2283
			// (get) Token: 0x060021BC RID: 8636 RVA: 0x0006F286 File Offset: 0x0006D486
			public override string Name
			{
				get
				{
					return "unitySliceBottom";
				}
			}

			// Token: 0x170008EC RID: 2284
			// (get) Token: 0x060021BD RID: 8637 RVA: 0x0006F28D File Offset: 0x0006D48D
			public override string ussName
			{
				get
				{
					return "-unity-slice-bottom";
				}
			}

			// Token: 0x170008ED RID: 2285
			// (get) Token: 0x060021BE RID: 8638 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021BF RID: 8639 RVA: 0x0007B2E5 File Offset: 0x000794E5
			public override int GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unitySliceBottom;
			}

			// Token: 0x060021C0 RID: 8640 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, int value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200042D RID: 1069
		private class UnitySliceLeftProperty : ResolvedStyleAccessPropertyBag.ResolvedIntProperty
		{
			// Token: 0x170008EE RID: 2286
			// (get) Token: 0x060021C2 RID: 8642 RVA: 0x0006F2B1 File Offset: 0x0006D4B1
			public override string Name
			{
				get
				{
					return "unitySliceLeft";
				}
			}

			// Token: 0x170008EF RID: 2287
			// (get) Token: 0x060021C3 RID: 8643 RVA: 0x0006F2B8 File Offset: 0x0006D4B8
			public override string ussName
			{
				get
				{
					return "-unity-slice-left";
				}
			}

			// Token: 0x170008F0 RID: 2288
			// (get) Token: 0x060021C4 RID: 8644 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021C5 RID: 8645 RVA: 0x0007B2F7 File Offset: 0x000794F7
			public override int GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unitySliceLeft;
			}

			// Token: 0x060021C6 RID: 8646 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, int value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200042E RID: 1070
		private class UnitySliceRightProperty : ResolvedStyleAccessPropertyBag.ResolvedIntProperty
		{
			// Token: 0x170008F1 RID: 2289
			// (get) Token: 0x060021C8 RID: 8648 RVA: 0x0006F2D3 File Offset: 0x0006D4D3
			public override string Name
			{
				get
				{
					return "unitySliceRight";
				}
			}

			// Token: 0x170008F2 RID: 2290
			// (get) Token: 0x060021C9 RID: 8649 RVA: 0x0006F2DA File Offset: 0x0006D4DA
			public override string ussName
			{
				get
				{
					return "-unity-slice-right";
				}
			}

			// Token: 0x170008F3 RID: 2291
			// (get) Token: 0x060021CA RID: 8650 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021CB RID: 8651 RVA: 0x0007B300 File Offset: 0x00079500
			public override int GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unitySliceRight;
			}

			// Token: 0x060021CC RID: 8652 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, int value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200042F RID: 1071
		private class UnitySliceScaleProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x170008F4 RID: 2292
			// (get) Token: 0x060021CE RID: 8654 RVA: 0x0006F2F5 File Offset: 0x0006D4F5
			public override string Name
			{
				get
				{
					return "unitySliceScale";
				}
			}

			// Token: 0x170008F5 RID: 2293
			// (get) Token: 0x060021CF RID: 8655 RVA: 0x0006F2FC File Offset: 0x0006D4FC
			public override string ussName
			{
				get
				{
					return "-unity-slice-scale";
				}
			}

			// Token: 0x170008F6 RID: 2294
			// (get) Token: 0x060021D0 RID: 8656 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021D1 RID: 8657 RVA: 0x0007B309 File Offset: 0x00079509
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unitySliceScale;
			}

			// Token: 0x060021D2 RID: 8658 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000430 RID: 1072
		private class UnitySliceTopProperty : ResolvedStyleAccessPropertyBag.ResolvedIntProperty
		{
			// Token: 0x170008F7 RID: 2295
			// (get) Token: 0x060021D4 RID: 8660 RVA: 0x0006F317 File Offset: 0x0006D517
			public override string Name
			{
				get
				{
					return "unitySliceTop";
				}
			}

			// Token: 0x170008F8 RID: 2296
			// (get) Token: 0x060021D5 RID: 8661 RVA: 0x0006F31E File Offset: 0x0006D51E
			public override string ussName
			{
				get
				{
					return "-unity-slice-top";
				}
			}

			// Token: 0x170008F9 RID: 2297
			// (get) Token: 0x060021D6 RID: 8662 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021D7 RID: 8663 RVA: 0x0007B312 File Offset: 0x00079512
			public override int GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unitySliceTop;
			}

			// Token: 0x060021D8 RID: 8664 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, int value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000431 RID: 1073
		private class UnitySliceTypeProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<SliceType>
		{
			// Token: 0x170008FA RID: 2298
			// (get) Token: 0x060021DA RID: 8666 RVA: 0x0006F339 File Offset: 0x0006D539
			public override string Name
			{
				get
				{
					return "unitySliceType";
				}
			}

			// Token: 0x170008FB RID: 2299
			// (get) Token: 0x060021DB RID: 8667 RVA: 0x0006F340 File Offset: 0x0006D540
			public override string ussName
			{
				get
				{
					return "-unity-slice-type";
				}
			}

			// Token: 0x170008FC RID: 2300
			// (get) Token: 0x060021DC RID: 8668 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021DD RID: 8669 RVA: 0x0007B31B File Offset: 0x0007951B
			public override SliceType GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unitySliceType;
			}

			// Token: 0x060021DE RID: 8670 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, SliceType value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000432 RID: 1074
		private class UnityTextAlignProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<TextAnchor>
		{
			// Token: 0x170008FD RID: 2301
			// (get) Token: 0x060021E0 RID: 8672 RVA: 0x0006F364 File Offset: 0x0006D564
			public override string Name
			{
				get
				{
					return "unityTextAlign";
				}
			}

			// Token: 0x170008FE RID: 2302
			// (get) Token: 0x060021E1 RID: 8673 RVA: 0x0006F36B File Offset: 0x0006D56B
			public override string ussName
			{
				get
				{
					return "-unity-text-align";
				}
			}

			// Token: 0x170008FF RID: 2303
			// (get) Token: 0x060021E2 RID: 8674 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021E3 RID: 8675 RVA: 0x0007B32D File Offset: 0x0007952D
			public override TextAnchor GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityTextAlign;
			}

			// Token: 0x060021E4 RID: 8676 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, TextAnchor value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000433 RID: 1075
		private class UnityTextGeneratorProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<TextGeneratorType>
		{
			// Token: 0x17000900 RID: 2304
			// (get) Token: 0x060021E6 RID: 8678 RVA: 0x0006F38F File Offset: 0x0006D58F
			public override string Name
			{
				get
				{
					return "unityTextGenerator";
				}
			}

			// Token: 0x17000901 RID: 2305
			// (get) Token: 0x060021E7 RID: 8679 RVA: 0x0006F396 File Offset: 0x0006D596
			public override string ussName
			{
				get
				{
					return "-unity-text-generator";
				}
			}

			// Token: 0x17000902 RID: 2306
			// (get) Token: 0x060021E8 RID: 8680 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021E9 RID: 8681 RVA: 0x0007B33F File Offset: 0x0007953F
			public override TextGeneratorType GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityTextGenerator;
			}

			// Token: 0x060021EA RID: 8682 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, TextGeneratorType value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000434 RID: 1076
		private class UnityTextOutlineColorProperty : ResolvedStyleAccessPropertyBag.ResolvedColorProperty
		{
			// Token: 0x17000903 RID: 2307
			// (get) Token: 0x060021EC RID: 8684 RVA: 0x0006F3BA File Offset: 0x0006D5BA
			public override string Name
			{
				get
				{
					return "unityTextOutlineColor";
				}
			}

			// Token: 0x17000904 RID: 2308
			// (get) Token: 0x060021ED RID: 8685 RVA: 0x0006F3C1 File Offset: 0x0006D5C1
			public override string ussName
			{
				get
				{
					return "-unity-text-outline-color";
				}
			}

			// Token: 0x17000905 RID: 2309
			// (get) Token: 0x060021EE RID: 8686 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021EF RID: 8687 RVA: 0x0007B351 File Offset: 0x00079551
			public override Color GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityTextOutlineColor;
			}

			// Token: 0x060021F0 RID: 8688 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Color value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000435 RID: 1077
		private class UnityTextOutlineWidthProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000906 RID: 2310
			// (get) Token: 0x060021F2 RID: 8690 RVA: 0x0006F3DC File Offset: 0x0006D5DC
			public override string Name
			{
				get
				{
					return "unityTextOutlineWidth";
				}
			}

			// Token: 0x17000907 RID: 2311
			// (get) Token: 0x060021F3 RID: 8691 RVA: 0x0006F3E3 File Offset: 0x0006D5E3
			public override string ussName
			{
				get
				{
					return "-unity-text-outline-width";
				}
			}

			// Token: 0x17000908 RID: 2312
			// (get) Token: 0x060021F4 RID: 8692 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021F5 RID: 8693 RVA: 0x0007B35A File Offset: 0x0007955A
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityTextOutlineWidth;
			}

			// Token: 0x060021F6 RID: 8694 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000436 RID: 1078
		private class UnityTextOverflowPositionProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<TextOverflowPosition>
		{
			// Token: 0x17000909 RID: 2313
			// (get) Token: 0x060021F8 RID: 8696 RVA: 0x0006F3FE File Offset: 0x0006D5FE
			public override string Name
			{
				get
				{
					return "unityTextOverflowPosition";
				}
			}

			// Token: 0x1700090A RID: 2314
			// (get) Token: 0x060021F9 RID: 8697 RVA: 0x0006F405 File Offset: 0x0006D605
			public override string ussName
			{
				get
				{
					return "-unity-text-overflow-position";
				}
			}

			// Token: 0x1700090B RID: 2315
			// (get) Token: 0x060021FA RID: 8698 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060021FB RID: 8699 RVA: 0x0007B363 File Offset: 0x00079563
			public override TextOverflowPosition GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).unityTextOverflowPosition;
			}

			// Token: 0x060021FC RID: 8700 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, TextOverflowPosition value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000437 RID: 1079
		private class VisibilityProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<Visibility>
		{
			// Token: 0x1700090C RID: 2316
			// (get) Token: 0x060021FE RID: 8702 RVA: 0x0006F429 File Offset: 0x0006D629
			public override string Name
			{
				get
				{
					return "visibility";
				}
			}

			// Token: 0x1700090D RID: 2317
			// (get) Token: 0x060021FF RID: 8703 RVA: 0x0006F429 File Offset: 0x0006D629
			public override string ussName
			{
				get
				{
					return "visibility";
				}
			}

			// Token: 0x1700090E RID: 2318
			// (get) Token: 0x06002200 RID: 8704 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002201 RID: 8705 RVA: 0x0007B375 File Offset: 0x00079575
			public override Visibility GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).visibility;
			}

			// Token: 0x06002202 RID: 8706 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, Visibility value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000438 RID: 1080
		private class WhiteSpaceProperty : ResolvedStyleAccessPropertyBag.ResolvedEnumProperty<WhiteSpace>
		{
			// Token: 0x1700090F RID: 2319
			// (get) Token: 0x06002204 RID: 8708 RVA: 0x0006F44D File Offset: 0x0006D64D
			public override string Name
			{
				get
				{
					return "whiteSpace";
				}
			}

			// Token: 0x17000910 RID: 2320
			// (get) Token: 0x06002205 RID: 8709 RVA: 0x0006F454 File Offset: 0x0006D654
			public override string ussName
			{
				get
				{
					return "white-space";
				}
			}

			// Token: 0x17000911 RID: 2321
			// (get) Token: 0x06002206 RID: 8710 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002207 RID: 8711 RVA: 0x0007B387 File Offset: 0x00079587
			public override WhiteSpace GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).whiteSpace;
			}

			// Token: 0x06002208 RID: 8712 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, WhiteSpace value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000439 RID: 1081
		private class WidthProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000912 RID: 2322
			// (get) Token: 0x0600220A RID: 8714 RVA: 0x0006F478 File Offset: 0x0006D678
			public override string Name
			{
				get
				{
					return "width";
				}
			}

			// Token: 0x17000913 RID: 2323
			// (get) Token: 0x0600220B RID: 8715 RVA: 0x0006F478 File Offset: 0x0006D678
			public override string ussName
			{
				get
				{
					return "width";
				}
			}

			// Token: 0x17000914 RID: 2324
			// (get) Token: 0x0600220C RID: 8716 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600220D RID: 8717 RVA: 0x0007B399 File Offset: 0x00079599
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).width;
			}

			// Token: 0x0600220E RID: 8718 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200043A RID: 1082
		private class WordSpacingProperty : ResolvedStyleAccessPropertyBag.ResolvedFloatProperty
		{
			// Token: 0x17000915 RID: 2325
			// (get) Token: 0x06002210 RID: 8720 RVA: 0x0006F493 File Offset: 0x0006D693
			public override string Name
			{
				get
				{
					return "wordSpacing";
				}
			}

			// Token: 0x17000916 RID: 2326
			// (get) Token: 0x06002211 RID: 8721 RVA: 0x0006F49A File Offset: 0x0006D69A
			public override string ussName
			{
				get
				{
					return "word-spacing";
				}
			}

			// Token: 0x17000917 RID: 2327
			// (get) Token: 0x06002212 RID: 8722 RVA: 0x0000E724 File Offset: 0x0000C924
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002213 RID: 8723 RVA: 0x0007B3A2 File Offset: 0x000795A2
			public override float GetValue(ref ResolvedStyleAccess container)
			{
				return ((IResolvedStyle)container).wordSpacing;
			}

			// Token: 0x06002214 RID: 8724 RVA: 0x0007AFC6 File Offset: 0x000791C6
			public override void SetValue(ref ResolvedStyleAccess container, float value)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0200043B RID: 1083
		private abstract class ResolvedStyleProperty<TValue> : Property<ResolvedStyleAccess, TValue>
		{
			// Token: 0x17000918 RID: 2328
			// (get) Token: 0x06002216 RID: 8726
			public abstract string ussName { get; }
		}

		// Token: 0x0200043C RID: 1084
		private abstract class ResolvedEnumProperty<TValue> : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<TValue> where TValue : struct, IConvertible
		{
		}

		// Token: 0x0200043D RID: 1085
		private abstract class ResolvedColorProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<Color>
		{
		}

		// Token: 0x0200043E RID: 1086
		private abstract class ResolvedBackgroundProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<Background>
		{
		}

		// Token: 0x0200043F RID: 1087
		private abstract class ResolvedFloatProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<float>
		{
		}

		// Token: 0x02000440 RID: 1088
		private abstract class ResolvedStyleFloatProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<StyleFloat>
		{
		}

		// Token: 0x02000441 RID: 1089
		private abstract class ResolvedListProperty<T> : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<IEnumerable<T>>
		{
		}

		// Token: 0x02000442 RID: 1090
		private abstract class ResolvedFontProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<Font>
		{
		}

		// Token: 0x02000443 RID: 1091
		private abstract class ResolvedFontDefinitionProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<FontDefinition>
		{
		}

		// Token: 0x02000444 RID: 1092
		private abstract class ResolvedIntProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<int>
		{
		}

		// Token: 0x02000445 RID: 1093
		private abstract class ResolvedRotateProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<Rotate>
		{
		}

		// Token: 0x02000446 RID: 1094
		private abstract class ResolvedScaleProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<Scale>
		{
		}

		// Token: 0x02000447 RID: 1095
		private abstract class ResolvedVector3Property : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<Vector3>
		{
		}

		// Token: 0x02000448 RID: 1096
		private abstract class ResolvedBackgroundPositionProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<BackgroundPosition>
		{
		}

		// Token: 0x02000449 RID: 1097
		private abstract class ResolvedBackgroundRepeatProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<BackgroundRepeat>
		{
		}

		// Token: 0x0200044A RID: 1098
		private abstract class ResolvedBackgroundSizeProperty : ResolvedStyleAccessPropertyBag.ResolvedStyleProperty<BackgroundSize>
		{
		}
	}
}
