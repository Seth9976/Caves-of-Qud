using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006CE RID: 1742
	internal static class InitialStyle
	{
		// Token: 0x0600322A RID: 12842 RVA: 0x000BD638 File Offset: 0x000BB838
		public static ref ComputedStyle Get()
		{
			return ref InitialStyle.s_InitialStyle;
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000BD650 File Offset: 0x000BB850
		public static ComputedStyle Acquire()
		{
			return InitialStyle.s_InitialStyle.Acquire();
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000BD66C File Offset: 0x000BB86C
		static InitialStyle()
		{
			InitialStyle.s_InitialStyle.layoutData.Write().alignContent = Align.FlexStart;
			InitialStyle.s_InitialStyle.layoutData.Write().alignItems = Align.Stretch;
			InitialStyle.s_InitialStyle.layoutData.Write().alignSelf = Align.Auto;
			InitialStyle.s_InitialStyle.visualData.Write().backgroundColor = Color.clear;
			InitialStyle.s_InitialStyle.visualData.Write().backgroundImage = default(Background);
			InitialStyle.s_InitialStyle.visualData.Write().backgroundPositionX = BackgroundPosition.Initial();
			InitialStyle.s_InitialStyle.visualData.Write().backgroundPositionY = BackgroundPosition.Initial();
			InitialStyle.s_InitialStyle.visualData.Write().backgroundRepeat = BackgroundRepeat.Initial();
			InitialStyle.s_InitialStyle.visualData.Write().backgroundSize = BackgroundSize.Initial();
			InitialStyle.s_InitialStyle.visualData.Write().borderBottomColor = Color.clear;
			InitialStyle.s_InitialStyle.visualData.Write().borderBottomLeftRadius = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderBottomRightRadius = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().borderBottomWidth = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderLeftColor = Color.clear;
			InitialStyle.s_InitialStyle.layoutData.Write().borderLeftWidth = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderRightColor = Color.clear;
			InitialStyle.s_InitialStyle.layoutData.Write().borderRightWidth = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderTopColor = Color.clear;
			InitialStyle.s_InitialStyle.visualData.Write().borderTopLeftRadius = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderTopRightRadius = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().borderTopWidth = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().bottom = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.inheritedData.Write().color = Color.black;
			InitialStyle.s_InitialStyle.rareData.Write().cursor = default(Cursor);
			InitialStyle.s_InitialStyle.layoutData.Write().display = DisplayStyle.Flex;
			InitialStyle.s_InitialStyle.layoutData.Write().flexBasis = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().flexDirection = FlexDirection.Column;
			InitialStyle.s_InitialStyle.layoutData.Write().flexGrow = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().flexShrink = 1f;
			InitialStyle.s_InitialStyle.layoutData.Write().flexWrap = Wrap.NoWrap;
			InitialStyle.s_InitialStyle.inheritedData.Write().fontSize = 14f;
			InitialStyle.s_InitialStyle.layoutData.Write().height = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().justifyContent = Justify.FlexStart;
			InitialStyle.s_InitialStyle.layoutData.Write().left = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.inheritedData.Write().letterSpacing = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().marginBottom = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().marginLeft = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().marginRight = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().marginTop = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().maxHeight = StyleKeyword.None.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().maxWidth = StyleKeyword.None.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().minHeight = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().minWidth = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.visualData.Write().opacity = 1f;
			InitialStyle.s_InitialStyle.visualData.Write().overflow = OverflowInternal.Visible;
			InitialStyle.s_InitialStyle.layoutData.Write().paddingBottom = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().paddingLeft = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().paddingRight = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().paddingTop = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().position = Position.Relative;
			InitialStyle.s_InitialStyle.layoutData.Write().right = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.transformData.Write().rotate = StyleKeyword.None.ToRotate();
			InitialStyle.s_InitialStyle.transformData.Write().scale = StyleKeyword.None.ToScale();
			InitialStyle.s_InitialStyle.rareData.Write().textOverflow = TextOverflow.Clip;
			InitialStyle.s_InitialStyle.inheritedData.Write().textShadow = default(TextShadow);
			InitialStyle.s_InitialStyle.layoutData.Write().top = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.transformData.Write().transformOrigin = TransformOrigin.Initial();
			InitialStyle.s_InitialStyle.transitionData.Write().transitionDelay = new List<TimeValue> { 0f };
			InitialStyle.s_InitialStyle.transitionData.Write().transitionDuration = new List<TimeValue> { 0f };
			InitialStyle.s_InitialStyle.transitionData.Write().transitionProperty = new List<StylePropertyName> { "all" };
			InitialStyle.s_InitialStyle.transitionData.Write().transitionTimingFunction = new List<EasingFunction> { EasingMode.Ease };
			InitialStyle.s_InitialStyle.transformData.Write().translate = StyleKeyword.None.ToTranslate();
			InitialStyle.s_InitialStyle.rareData.Write().unityBackgroundImageTintColor = Color.white;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityEditorTextRenderingMode = EditorTextRenderingMode.SDF;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityFont = null;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityFontDefinition = default(FontDefinition);
			InitialStyle.s_InitialStyle.inheritedData.Write().unityFontStyleAndWeight = FontStyle.Normal;
			InitialStyle.s_InitialStyle.rareData.Write().unityOverflowClipBox = OverflowClipBox.PaddingBox;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityParagraphSpacing = 0f;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceBottom = 0;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceLeft = 0;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceRight = 0;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceScale = 1f;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceTop = 0;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceType = SliceType.Sliced;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityTextAlign = TextAnchor.UpperLeft;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityTextGenerator = TextGeneratorType.Standard;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityTextOutlineColor = Color.clear;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityTextOutlineWidth = 0f;
			InitialStyle.s_InitialStyle.rareData.Write().unityTextOverflowPosition = TextOverflowPosition.End;
			InitialStyle.s_InitialStyle.inheritedData.Write().visibility = Visibility.Visible;
			InitialStyle.s_InitialStyle.inheritedData.Write().whiteSpace = WhiteSpace.Normal;
			InitialStyle.s_InitialStyle.layoutData.Write().width = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.inheritedData.Write().wordSpacing = 0f;
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x0600322D RID: 12845 RVA: 0x000BDEFA File Offset: 0x000BC0FA
		public static Align alignContent
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().alignContent;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x0600322E RID: 12846 RVA: 0x000BDF10 File Offset: 0x000BC110
		public static Align alignItems
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().alignItems;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x000BDF26 File Offset: 0x000BC126
		public static Align alignSelf
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().alignSelf;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x000BDF3C File Offset: 0x000BC13C
		public static Color backgroundColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().backgroundColor;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x000BDF52 File Offset: 0x000BC152
		public static Background backgroundImage
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().backgroundImage;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x000BDF68 File Offset: 0x000BC168
		public static BackgroundPosition backgroundPositionX
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().backgroundPositionX;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000BDF7E File Offset: 0x000BC17E
		public static BackgroundPosition backgroundPositionY
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().backgroundPositionY;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x000BDF94 File Offset: 0x000BC194
		public static BackgroundRepeat backgroundRepeat
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().backgroundRepeat;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x000BDFAA File Offset: 0x000BC1AA
		public static BackgroundSize backgroundSize
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().backgroundSize;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x000BDFC0 File Offset: 0x000BC1C0
		public static Color borderBottomColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderBottomColor;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x000BDFD6 File Offset: 0x000BC1D6
		public static Length borderBottomLeftRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderBottomLeftRadius;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06003238 RID: 12856 RVA: 0x000BDFEC File Offset: 0x000BC1EC
		public static Length borderBottomRightRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderBottomRightRadius;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06003239 RID: 12857 RVA: 0x000BE002 File Offset: 0x000BC202
		public static float borderBottomWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().borderBottomWidth;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x000BE018 File Offset: 0x000BC218
		public static Color borderLeftColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderLeftColor;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x000BE02E File Offset: 0x000BC22E
		public static float borderLeftWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().borderLeftWidth;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x000BE044 File Offset: 0x000BC244
		public static Color borderRightColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderRightColor;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x000BE05A File Offset: 0x000BC25A
		public static float borderRightWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().borderRightWidth;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x0600323E RID: 12862 RVA: 0x000BE070 File Offset: 0x000BC270
		public static Color borderTopColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderTopColor;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x000BE086 File Offset: 0x000BC286
		public static Length borderTopLeftRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderTopLeftRadius;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x000BE09C File Offset: 0x000BC29C
		public static Length borderTopRightRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderTopRightRadius;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06003241 RID: 12865 RVA: 0x000BE0B2 File Offset: 0x000BC2B2
		public static float borderTopWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().borderTopWidth;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06003242 RID: 12866 RVA: 0x000BE0C8 File Offset: 0x000BC2C8
		public static Length bottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().bottom;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06003243 RID: 12867 RVA: 0x000BE0DE File Offset: 0x000BC2DE
		public static Color color
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().color;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06003244 RID: 12868 RVA: 0x000BE0F4 File Offset: 0x000BC2F4
		public static Cursor cursor
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().cursor;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06003245 RID: 12869 RVA: 0x000BE10A File Offset: 0x000BC30A
		public static DisplayStyle display
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().display;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06003246 RID: 12870 RVA: 0x000BE120 File Offset: 0x000BC320
		public static Length flexBasis
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexBasis;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x000BE136 File Offset: 0x000BC336
		public static FlexDirection flexDirection
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexDirection;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06003248 RID: 12872 RVA: 0x000BE14C File Offset: 0x000BC34C
		public static float flexGrow
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexGrow;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06003249 RID: 12873 RVA: 0x000BE162 File Offset: 0x000BC362
		public static float flexShrink
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexShrink;
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x0600324A RID: 12874 RVA: 0x000BE178 File Offset: 0x000BC378
		public static Wrap flexWrap
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexWrap;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x000BE18E File Offset: 0x000BC38E
		public static Length fontSize
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().fontSize;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x000BE1A4 File Offset: 0x000BC3A4
		public static Length height
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().height;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x0600324D RID: 12877 RVA: 0x000BE1BA File Offset: 0x000BC3BA
		public static Justify justifyContent
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().justifyContent;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x0600324E RID: 12878 RVA: 0x000BE1D0 File Offset: 0x000BC3D0
		public static Length left
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().left;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x0600324F RID: 12879 RVA: 0x000BE1E6 File Offset: 0x000BC3E6
		public static Length letterSpacing
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().letterSpacing;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06003250 RID: 12880 RVA: 0x000BE1FC File Offset: 0x000BC3FC
		public static Length marginBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().marginBottom;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06003251 RID: 12881 RVA: 0x000BE212 File Offset: 0x000BC412
		public static Length marginLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().marginLeft;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x000BE228 File Offset: 0x000BC428
		public static Length marginRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().marginRight;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x000BE23E File Offset: 0x000BC43E
		public static Length marginTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().marginTop;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06003254 RID: 12884 RVA: 0x000BE254 File Offset: 0x000BC454
		public static Length maxHeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().maxHeight;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x000BE26A File Offset: 0x000BC46A
		public static Length maxWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().maxWidth;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06003256 RID: 12886 RVA: 0x000BE280 File Offset: 0x000BC480
		public static Length minHeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().minHeight;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x000BE296 File Offset: 0x000BC496
		public static Length minWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().minWidth;
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06003258 RID: 12888 RVA: 0x000BE2AC File Offset: 0x000BC4AC
		public static float opacity
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().opacity;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000BE2C2 File Offset: 0x000BC4C2
		public static OverflowInternal overflow
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().overflow;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x000BE2D8 File Offset: 0x000BC4D8
		public static Length paddingBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().paddingBottom;
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000BE2EE File Offset: 0x000BC4EE
		public static Length paddingLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().paddingLeft;
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x0600325C RID: 12892 RVA: 0x000BE304 File Offset: 0x000BC504
		public static Length paddingRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().paddingRight;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000BE31A File Offset: 0x000BC51A
		public static Length paddingTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().paddingTop;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x000BE330 File Offset: 0x000BC530
		public static Position position
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().position;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x000BE346 File Offset: 0x000BC546
		public static Length right
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().right;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x000BE35C File Offset: 0x000BC55C
		public static Rotate rotate
		{
			get
			{
				return InitialStyle.s_InitialStyle.transformData.Read().rotate;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x000BE372 File Offset: 0x000BC572
		public static Scale scale
		{
			get
			{
				return InitialStyle.s_InitialStyle.transformData.Read().scale;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x000BE388 File Offset: 0x000BC588
		public static TextOverflow textOverflow
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().textOverflow;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x000BE39E File Offset: 0x000BC59E
		public static TextShadow textShadow
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().textShadow;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x000BE3B4 File Offset: 0x000BC5B4
		public static Length top
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().top;
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x000BE3CA File Offset: 0x000BC5CA
		public static TransformOrigin transformOrigin
		{
			get
			{
				return InitialStyle.s_InitialStyle.transformData.Read().transformOrigin;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x000BE3E0 File Offset: 0x000BC5E0
		public static List<TimeValue> transitionDelay
		{
			get
			{
				return InitialStyle.s_InitialStyle.transitionData.Read().transitionDelay;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x000BE3F6 File Offset: 0x000BC5F6
		public static List<TimeValue> transitionDuration
		{
			get
			{
				return InitialStyle.s_InitialStyle.transitionData.Read().transitionDuration;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x000BE40C File Offset: 0x000BC60C
		public static List<StylePropertyName> transitionProperty
		{
			get
			{
				return InitialStyle.s_InitialStyle.transitionData.Read().transitionProperty;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x000BE422 File Offset: 0x000BC622
		public static List<EasingFunction> transitionTimingFunction
		{
			get
			{
				return InitialStyle.s_InitialStyle.transitionData.Read().transitionTimingFunction;
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x0600326A RID: 12906 RVA: 0x000BE438 File Offset: 0x000BC638
		public static Translate translate
		{
			get
			{
				return InitialStyle.s_InitialStyle.transformData.Read().translate;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x0600326B RID: 12907 RVA: 0x000BE44E File Offset: 0x000BC64E
		public static Color unityBackgroundImageTintColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unityBackgroundImageTintColor;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x0600326C RID: 12908 RVA: 0x000BE464 File Offset: 0x000BC664
		public static EditorTextRenderingMode unityEditorTextRenderingMode
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityEditorTextRenderingMode;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x0600326D RID: 12909 RVA: 0x000BE47A File Offset: 0x000BC67A
		public static Font unityFont
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityFont;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x0600326E RID: 12910 RVA: 0x000BE490 File Offset: 0x000BC690
		public static FontDefinition unityFontDefinition
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityFontDefinition;
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x0600326F RID: 12911 RVA: 0x000BE4A6 File Offset: 0x000BC6A6
		public static FontStyle unityFontStyleAndWeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityFontStyleAndWeight;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06003270 RID: 12912 RVA: 0x000BE4BC File Offset: 0x000BC6BC
		public static OverflowClipBox unityOverflowClipBox
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unityOverflowClipBox;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06003271 RID: 12913 RVA: 0x000BE4D2 File Offset: 0x000BC6D2
		public static Length unityParagraphSpacing
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityParagraphSpacing;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06003272 RID: 12914 RVA: 0x000BE4E8 File Offset: 0x000BC6E8
		public static int unitySliceBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceBottom;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06003273 RID: 12915 RVA: 0x000BE4FE File Offset: 0x000BC6FE
		public static int unitySliceLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceLeft;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06003274 RID: 12916 RVA: 0x000BE514 File Offset: 0x000BC714
		public static int unitySliceRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceRight;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06003275 RID: 12917 RVA: 0x000BE52A File Offset: 0x000BC72A
		public static float unitySliceScale
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceScale;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06003276 RID: 12918 RVA: 0x000BE540 File Offset: 0x000BC740
		public static int unitySliceTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceTop;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x000BE556 File Offset: 0x000BC756
		public static SliceType unitySliceType
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceType;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06003278 RID: 12920 RVA: 0x000BE56C File Offset: 0x000BC76C
		public static TextAnchor unityTextAlign
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityTextAlign;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x000BE582 File Offset: 0x000BC782
		public static TextGeneratorType unityTextGenerator
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityTextGenerator;
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x000BE598 File Offset: 0x000BC798
		public static Color unityTextOutlineColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityTextOutlineColor;
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x000BE5AE File Offset: 0x000BC7AE
		public static float unityTextOutlineWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityTextOutlineWidth;
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x000BE5C4 File Offset: 0x000BC7C4
		public static TextOverflowPosition unityTextOverflowPosition
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unityTextOverflowPosition;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x000BE5DA File Offset: 0x000BC7DA
		public static Visibility visibility
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().visibility;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x000BE5F0 File Offset: 0x000BC7F0
		public static WhiteSpace whiteSpace
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().whiteSpace;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000BE606 File Offset: 0x000BC806
		public static Length width
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().width;
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06003280 RID: 12928 RVA: 0x000BE61C File Offset: 0x000BC81C
		public static Length wordSpacing
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().wordSpacing;
			}
		}

		// Token: 0x04001931 RID: 6449
		private static ComputedStyle s_InitialStyle = ComputedStyle.CreateInitial();
	}
}
