using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.UIElements.Layout;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x0200034D RID: 845
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal struct ComputedStyle
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x0005E29F File Offset: 0x0005C49F
		public int customPropertiesCount
		{
			get
			{
				Dictionary<string, StylePropertyValue> dictionary = this.customProperties;
				return (dictionary != null) ? dictionary.Count : 0;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0005E2B3 File Offset: 0x0005C4B3
		public bool hasTransition
		{
			get
			{
				ComputedTransitionProperty[] array = this.computedTransitions;
				return array != null && array.Length != 0;
			}
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0005E2C8 File Offset: 0x0005C4C8
		public static ComputedStyle Create()
		{
			return InitialStyle.Acquire();
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0005E2E0 File Offset: 0x0005C4E0
		public void FinalizeApply(ref ComputedStyle parentStyle)
		{
			bool flag = this.fontSize.unit == LengthUnit.Percent;
			if (flag)
			{
				float value = parentStyle.fontSize.value;
				float num = value * this.fontSize.value / 100f;
				this.inheritedData.Write().fontSize = new Length(num);
			}
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x0005E344 File Offset: 0x0005C544
		private bool ApplyGlobalKeyword(StylePropertyReader reader, ref ComputedStyle parentStyle)
		{
			StyleValueHandle handle = reader.GetValue(0).handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)handle.valueIndex;
				StyleValueKeyword styleValueKeyword = valueIndex;
				if (styleValueKeyword == StyleValueKeyword.Initial)
				{
					this.ApplyInitialValue(reader);
					return true;
				}
				if (styleValueKeyword == StyleValueKeyword.Unset)
				{
					this.ApplyUnsetValue(reader, ref parentStyle);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x0005E3A8 File Offset: 0x0005C5A8
		private bool ApplyGlobalKeyword(StylePropertyId id, StyleKeyword keyword, ref ComputedStyle parentStyle)
		{
			bool flag = keyword == StyleKeyword.Initial;
			bool flag2;
			if (flag)
			{
				this.ApplyInitialValue(id);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0005E3D0 File Offset: 0x0005C5D0
		private void RemoveCustomStyleProperty(StylePropertyReader reader)
		{
			string name = reader.property.name;
			bool flag = this.customProperties == null || !this.customProperties.ContainsKey(name);
			if (!flag)
			{
				this.customProperties.Remove(name);
			}
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0005E418 File Offset: 0x0005C618
		private void ApplyCustomStyleProperty(StylePropertyReader reader)
		{
			this.dpiScaling = reader.dpiScaling;
			bool flag = this.customProperties == null;
			if (flag)
			{
				this.customProperties = new Dictionary<string, StylePropertyValue>();
			}
			StyleProperty property = reader.property;
			StylePropertyValue value = reader.GetValue(0);
			this.customProperties[property.name] = value;
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0005E46E File Offset: 0x0005C66E
		private void ApplyAllPropertyInitial()
		{
			this.CopyFrom(InitialStyle.Get());
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0005E47D File Offset: 0x0005C67D
		private void ResetComputedTransitions()
		{
			this.computedTransitions = null;
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x0005E488 File Offset: 0x0005C688
		public static bool StartAnimationInlineTextShadow(VisualElement element, ref ComputedStyle computedStyle, StyleTextShadow textShadow, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			TextShadow textShadow2 = ((textShadow.keyword == StyleKeyword.Initial) ? InitialStyle.textShadow : textShadow.value);
			return element.styleAnimation.Start(StylePropertyId.TextShadow, computedStyle.inheritedData.Read().textShadow, textShadow2, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0005E4DC File Offset: 0x0005C6DC
		public static bool StartAnimationInlineRotate(VisualElement element, ref ComputedStyle computedStyle, StyleRotate rotate, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			Rotate rotate2 = ((rotate.keyword == StyleKeyword.Initial) ? InitialStyle.rotate : rotate.value);
			bool flag = element.styleAnimation.Start(StylePropertyId.Rotate, computedStyle.transformData.Read().rotate, rotate2, durationMs, delayMs, easingCurve);
			bool flag2 = flag && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
			if (flag2)
			{
				element.usageHints |= UsageHints.DynamicTransform;
			}
			return flag;
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0005E558 File Offset: 0x0005C758
		public static bool StartAnimationInlineTranslate(VisualElement element, ref ComputedStyle computedStyle, StyleTranslate translate, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			Translate translate2 = ((translate.keyword == StyleKeyword.Initial) ? InitialStyle.translate : translate.value);
			bool flag = element.styleAnimation.Start(StylePropertyId.Translate, computedStyle.transformData.Read().translate, translate2, durationMs, delayMs, easingCurve);
			bool flag2 = flag && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
			if (flag2)
			{
				element.usageHints |= UsageHints.DynamicTransform;
			}
			return flag;
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x0005E5D4 File Offset: 0x0005C7D4
		public static bool StartAnimationInlineScale(VisualElement element, ref ComputedStyle computedStyle, StyleScale scale, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			Scale scale2 = ((scale.keyword == StyleKeyword.Initial) ? InitialStyle.scale : scale.value);
			bool flag = element.styleAnimation.Start(StylePropertyId.Scale, computedStyle.transformData.Read().scale, scale2, durationMs, delayMs, easingCurve);
			bool flag2 = flag && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
			if (flag2)
			{
				element.usageHints |= UsageHints.DynamicTransform;
			}
			return flag;
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0005E650 File Offset: 0x0005C850
		public static bool StartAnimationInlineTransformOrigin(VisualElement element, ref ComputedStyle computedStyle, StyleTransformOrigin transformOrigin, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			TransformOrigin transformOrigin2 = ((transformOrigin.keyword == StyleKeyword.Initial) ? InitialStyle.transformOrigin : transformOrigin.value);
			bool flag = element.styleAnimation.Start(StylePropertyId.TransformOrigin, computedStyle.transformData.Read().transformOrigin, transformOrigin2, durationMs, delayMs, easingCurve);
			bool flag2 = flag && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
			if (flag2)
			{
				element.usageHints |= UsageHints.DynamicTransform;
			}
			return flag;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0005E6CC File Offset: 0x0005C8CC
		public static bool StartAnimationInlineBackgroundSize(VisualElement element, ref ComputedStyle computedStyle, StyleBackgroundSize backgroundSize, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			BackgroundSize backgroundSize2 = ((backgroundSize.keyword == StyleKeyword.Initial) ? InitialStyle.backgroundSize : backgroundSize.value);
			return element.styleAnimation.Start(StylePropertyId.BackgroundSize, computedStyle.visualData.Read().backgroundSize, backgroundSize2, durationMs, delayMs, easingCurve);
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x0005E71D File Offset: 0x0005C91D
		public Align alignContent
		{
			get
			{
				return this.layoutData.Read().alignContent;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x0005E72F File Offset: 0x0005C92F
		public Align alignItems
		{
			get
			{
				return this.layoutData.Read().alignItems;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x0005E741 File Offset: 0x0005C941
		public Align alignSelf
		{
			get
			{
				return this.layoutData.Read().alignSelf;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x0005E753 File Offset: 0x0005C953
		public Color backgroundColor
		{
			get
			{
				return this.visualData.Read().backgroundColor;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x0005E765 File Offset: 0x0005C965
		public Background backgroundImage
		{
			get
			{
				return this.visualData.Read().backgroundImage;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x0005E777 File Offset: 0x0005C977
		public BackgroundPosition backgroundPositionX
		{
			get
			{
				return this.visualData.Read().backgroundPositionX;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0005E789 File Offset: 0x0005C989
		public BackgroundPosition backgroundPositionY
		{
			get
			{
				return this.visualData.Read().backgroundPositionY;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x0005E79B File Offset: 0x0005C99B
		public BackgroundRepeat backgroundRepeat
		{
			get
			{
				return this.visualData.Read().backgroundRepeat;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x0005E7AD File Offset: 0x0005C9AD
		public BackgroundSize backgroundSize
		{
			get
			{
				return this.visualData.Read().backgroundSize;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x0005E7BF File Offset: 0x0005C9BF
		public Color borderBottomColor
		{
			get
			{
				return this.visualData.Read().borderBottomColor;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0005E7D1 File Offset: 0x0005C9D1
		public Length borderBottomLeftRadius
		{
			get
			{
				return this.visualData.Read().borderBottomLeftRadius;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x0005E7E3 File Offset: 0x0005C9E3
		public Length borderBottomRightRadius
		{
			get
			{
				return this.visualData.Read().borderBottomRightRadius;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0005E7F5 File Offset: 0x0005C9F5
		public float borderBottomWidth
		{
			get
			{
				return this.layoutData.Read().borderBottomWidth;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x0005E807 File Offset: 0x0005CA07
		public Color borderLeftColor
		{
			get
			{
				return this.visualData.Read().borderLeftColor;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x0005E819 File Offset: 0x0005CA19
		public float borderLeftWidth
		{
			get
			{
				return this.layoutData.Read().borderLeftWidth;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x0005E82B File Offset: 0x0005CA2B
		public Color borderRightColor
		{
			get
			{
				return this.visualData.Read().borderRightColor;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x0005E83D File Offset: 0x0005CA3D
		public float borderRightWidth
		{
			get
			{
				return this.layoutData.Read().borderRightWidth;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x0005E84F File Offset: 0x0005CA4F
		public Color borderTopColor
		{
			get
			{
				return this.visualData.Read().borderTopColor;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x0005E861 File Offset: 0x0005CA61
		public Length borderTopLeftRadius
		{
			get
			{
				return this.visualData.Read().borderTopLeftRadius;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x0005E873 File Offset: 0x0005CA73
		public Length borderTopRightRadius
		{
			get
			{
				return this.visualData.Read().borderTopRightRadius;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x0005E885 File Offset: 0x0005CA85
		public float borderTopWidth
		{
			get
			{
				return this.layoutData.Read().borderTopWidth;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x0005E897 File Offset: 0x0005CA97
		public Length bottom
		{
			get
			{
				return this.layoutData.Read().bottom;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x0005E8A9 File Offset: 0x0005CAA9
		public Color color
		{
			get
			{
				return this.inheritedData.Read().color;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x0005E8BB File Offset: 0x0005CABB
		public Cursor cursor
		{
			get
			{
				return this.rareData.Read().cursor;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x0005E8CD File Offset: 0x0005CACD
		public DisplayStyle display
		{
			get
			{
				return this.layoutData.Read().display;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x0005E8DF File Offset: 0x0005CADF
		public Length flexBasis
		{
			get
			{
				return this.layoutData.Read().flexBasis;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x0005E8F1 File Offset: 0x0005CAF1
		public FlexDirection flexDirection
		{
			get
			{
				return this.layoutData.Read().flexDirection;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x0005E903 File Offset: 0x0005CB03
		public float flexGrow
		{
			get
			{
				return this.layoutData.Read().flexGrow;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0005E915 File Offset: 0x0005CB15
		public float flexShrink
		{
			get
			{
				return this.layoutData.Read().flexShrink;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x0005E927 File Offset: 0x0005CB27
		public Wrap flexWrap
		{
			get
			{
				return this.layoutData.Read().flexWrap;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x0005E939 File Offset: 0x0005CB39
		public Length fontSize
		{
			get
			{
				return this.inheritedData.Read().fontSize;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0005E94B File Offset: 0x0005CB4B
		public Length height
		{
			get
			{
				return this.layoutData.Read().height;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0005E95D File Offset: 0x0005CB5D
		public Justify justifyContent
		{
			get
			{
				return this.layoutData.Read().justifyContent;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0005E96F File Offset: 0x0005CB6F
		public Length left
		{
			get
			{
				return this.layoutData.Read().left;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x0005E981 File Offset: 0x0005CB81
		public Length letterSpacing
		{
			get
			{
				return this.inheritedData.Read().letterSpacing;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x0005E993 File Offset: 0x0005CB93
		public Length marginBottom
		{
			get
			{
				return this.layoutData.Read().marginBottom;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x0005E9A5 File Offset: 0x0005CBA5
		public Length marginLeft
		{
			get
			{
				return this.layoutData.Read().marginLeft;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0005E9B7 File Offset: 0x0005CBB7
		public Length marginRight
		{
			get
			{
				return this.layoutData.Read().marginRight;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0005E9C9 File Offset: 0x0005CBC9
		public Length marginTop
		{
			get
			{
				return this.layoutData.Read().marginTop;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0005E9DB File Offset: 0x0005CBDB
		public Length maxHeight
		{
			get
			{
				return this.layoutData.Read().maxHeight;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0005E9ED File Offset: 0x0005CBED
		public Length maxWidth
		{
			get
			{
				return this.layoutData.Read().maxWidth;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x0005E9FF File Offset: 0x0005CBFF
		public Length minHeight
		{
			get
			{
				return this.layoutData.Read().minHeight;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0005EA11 File Offset: 0x0005CC11
		public Length minWidth
		{
			get
			{
				return this.layoutData.Read().minWidth;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0005EA23 File Offset: 0x0005CC23
		public float opacity
		{
			get
			{
				return this.visualData.Read().opacity;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x0005EA35 File Offset: 0x0005CC35
		public OverflowInternal overflow
		{
			get
			{
				return this.visualData.Read().overflow;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0005EA47 File Offset: 0x0005CC47
		public Length paddingBottom
		{
			get
			{
				return this.layoutData.Read().paddingBottom;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x0005EA59 File Offset: 0x0005CC59
		public Length paddingLeft
		{
			get
			{
				return this.layoutData.Read().paddingLeft;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0005EA6B File Offset: 0x0005CC6B
		public Length paddingRight
		{
			get
			{
				return this.layoutData.Read().paddingRight;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x0005EA7D File Offset: 0x0005CC7D
		public Length paddingTop
		{
			get
			{
				return this.layoutData.Read().paddingTop;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0005EA8F File Offset: 0x0005CC8F
		public Position position
		{
			get
			{
				return this.layoutData.Read().position;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x0005EAA1 File Offset: 0x0005CCA1
		public Length right
		{
			get
			{
				return this.layoutData.Read().right;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x0005EAB3 File Offset: 0x0005CCB3
		public Rotate rotate
		{
			get
			{
				return this.transformData.Read().rotate;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x0005EAC5 File Offset: 0x0005CCC5
		public Scale scale
		{
			get
			{
				return this.transformData.Read().scale;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x0005EAD7 File Offset: 0x0005CCD7
		public TextOverflow textOverflow
		{
			get
			{
				return this.rareData.Read().textOverflow;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x0005EAE9 File Offset: 0x0005CCE9
		public TextShadow textShadow
		{
			get
			{
				return this.inheritedData.Read().textShadow;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x0005EAFB File Offset: 0x0005CCFB
		public Length top
		{
			get
			{
				return this.layoutData.Read().top;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x0005EB0D File Offset: 0x0005CD0D
		public TransformOrigin transformOrigin
		{
			get
			{
				return this.transformData.Read().transformOrigin;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0005EB1F File Offset: 0x0005CD1F
		public List<TimeValue> transitionDelay
		{
			get
			{
				return this.transitionData.Read().transitionDelay;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x0005EB31 File Offset: 0x0005CD31
		public List<TimeValue> transitionDuration
		{
			get
			{
				return this.transitionData.Read().transitionDuration;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x0005EB43 File Offset: 0x0005CD43
		public List<StylePropertyName> transitionProperty
		{
			get
			{
				return this.transitionData.Read().transitionProperty;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x0005EB55 File Offset: 0x0005CD55
		public List<EasingFunction> transitionTimingFunction
		{
			get
			{
				return this.transitionData.Read().transitionTimingFunction;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x0005EB67 File Offset: 0x0005CD67
		public Translate translate
		{
			get
			{
				return this.transformData.Read().translate;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x0005EB79 File Offset: 0x0005CD79
		public Color unityBackgroundImageTintColor
		{
			get
			{
				return this.rareData.Read().unityBackgroundImageTintColor;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x0005EB8B File Offset: 0x0005CD8B
		public EditorTextRenderingMode unityEditorTextRenderingMode
		{
			get
			{
				return this.inheritedData.Read().unityEditorTextRenderingMode;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x0005EB9D File Offset: 0x0005CD9D
		public Font unityFont
		{
			get
			{
				return this.inheritedData.Read().unityFont;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0005EBAF File Offset: 0x0005CDAF
		public FontDefinition unityFontDefinition
		{
			get
			{
				return this.inheritedData.Read().unityFontDefinition;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x0005EBC1 File Offset: 0x0005CDC1
		public FontStyle unityFontStyleAndWeight
		{
			get
			{
				return this.inheritedData.Read().unityFontStyleAndWeight;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x0005EBD3 File Offset: 0x0005CDD3
		public OverflowClipBox unityOverflowClipBox
		{
			get
			{
				return this.rareData.Read().unityOverflowClipBox;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x0005EBE5 File Offset: 0x0005CDE5
		public Length unityParagraphSpacing
		{
			get
			{
				return this.inheritedData.Read().unityParagraphSpacing;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x0005EBF7 File Offset: 0x0005CDF7
		public int unitySliceBottom
		{
			get
			{
				return this.rareData.Read().unitySliceBottom;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x0005EC09 File Offset: 0x0005CE09
		public int unitySliceLeft
		{
			get
			{
				return this.rareData.Read().unitySliceLeft;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0005EC1B File Offset: 0x0005CE1B
		public int unitySliceRight
		{
			get
			{
				return this.rareData.Read().unitySliceRight;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x0005EC2D File Offset: 0x0005CE2D
		public float unitySliceScale
		{
			get
			{
				return this.rareData.Read().unitySliceScale;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0005EC3F File Offset: 0x0005CE3F
		public int unitySliceTop
		{
			get
			{
				return this.rareData.Read().unitySliceTop;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x0005EC51 File Offset: 0x0005CE51
		public SliceType unitySliceType
		{
			get
			{
				return this.rareData.Read().unitySliceType;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0005EC63 File Offset: 0x0005CE63
		public TextAnchor unityTextAlign
		{
			get
			{
				return this.inheritedData.Read().unityTextAlign;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x0005EC75 File Offset: 0x0005CE75
		public TextGeneratorType unityTextGenerator
		{
			get
			{
				return this.inheritedData.Read().unityTextGenerator;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0005EC87 File Offset: 0x0005CE87
		public Color unityTextOutlineColor
		{
			get
			{
				return this.inheritedData.Read().unityTextOutlineColor;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x0005EC99 File Offset: 0x0005CE99
		public float unityTextOutlineWidth
		{
			get
			{
				return this.inheritedData.Read().unityTextOutlineWidth;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x0005ECAB File Offset: 0x0005CEAB
		public TextOverflowPosition unityTextOverflowPosition
		{
			get
			{
				return this.rareData.Read().unityTextOverflowPosition;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0005ECBD File Offset: 0x0005CEBD
		public Visibility visibility
		{
			get
			{
				return this.inheritedData.Read().visibility;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x0005ECCF File Offset: 0x0005CECF
		public WhiteSpace whiteSpace
		{
			get
			{
				return this.inheritedData.Read().whiteSpace;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x0005ECE1 File Offset: 0x0005CEE1
		public Length width
		{
			get
			{
				return this.layoutData.Read().width;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0005ECF3 File Offset: 0x0005CEF3
		public Length wordSpacing
		{
			get
			{
				return this.inheritedData.Read().wordSpacing;
			}
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0005ED08 File Offset: 0x0005CF08
		public static ComputedStyle Create(ref ComputedStyle parentStyle)
		{
			ref ComputedStyle ptr = ref InitialStyle.Get();
			ComputedStyle computedStyle = new ComputedStyle
			{
				dpiScaling = 1f
			};
			computedStyle.inheritedData = parentStyle.inheritedData.Acquire();
			computedStyle.layoutData = ptr.layoutData.Acquire();
			computedStyle.rareData = ptr.rareData.Acquire();
			computedStyle.transformData = ptr.transformData.Acquire();
			computedStyle.transitionData = ptr.transitionData.Acquire();
			computedStyle.visualData = ptr.visualData.Acquire();
			return computedStyle;
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0005EDA4 File Offset: 0x0005CFA4
		public static ComputedStyle CreateInitial()
		{
			ComputedStyle computedStyle = new ComputedStyle
			{
				dpiScaling = 1f
			};
			computedStyle.inheritedData = StyleDataRef<InheritedData>.Create();
			computedStyle.layoutData = StyleDataRef<LayoutData>.Create();
			computedStyle.rareData = StyleDataRef<RareData>.Create();
			computedStyle.transformData = StyleDataRef<TransformData>.Create();
			computedStyle.transitionData = StyleDataRef<TransitionData>.Create();
			computedStyle.visualData = StyleDataRef<VisualData>.Create();
			return computedStyle;
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0005EE18 File Offset: 0x0005D018
		public ComputedStyle Acquire()
		{
			this.inheritedData.Acquire();
			this.layoutData.Acquire();
			this.rareData.Acquire();
			this.transformData.Acquire();
			this.transitionData.Acquire();
			this.visualData.Acquire();
			return this;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0005EE78 File Offset: 0x0005D078
		public void Release()
		{
			this.inheritedData.Release();
			this.layoutData.Release();
			this.rareData.Release();
			this.transformData.Release();
			this.transitionData.Release();
			this.visualData.Release();
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0005EED0 File Offset: 0x0005D0D0
		public void CopyFrom(ref ComputedStyle other)
		{
			this.inheritedData.CopyFrom(other.inheritedData);
			this.layoutData.CopyFrom(other.layoutData);
			this.rareData.CopyFrom(other.rareData);
			this.transformData.CopyFrom(other.transformData);
			this.transitionData.CopyFrom(other.transitionData);
			this.visualData.CopyFrom(other.visualData);
			this.customProperties = other.customProperties;
			this.matchingRulesHash = other.matchingRulesHash;
			this.dpiScaling = other.dpiScaling;
			this.computedTransitions = other.computedTransitions;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0005EF7C File Offset: 0x0005D17C
		public void ApplyProperties(StylePropertyReader reader, ref ComputedStyle parentStyle)
		{
			StylePropertyId stylePropertyId = reader.propertyId;
			while (reader.property != null)
			{
				bool flag = this.ApplyGlobalKeyword(reader, ref parentStyle);
				if (!flag)
				{
					StylePropertyId stylePropertyId2 = stylePropertyId;
					StylePropertyId stylePropertyId3 = stylePropertyId2;
					if (stylePropertyId3 <= StylePropertyId.Width)
					{
						if (stylePropertyId3 <= StylePropertyId.Unknown)
						{
							if (stylePropertyId3 != StylePropertyId.Custom)
							{
								if (stylePropertyId3 != StylePropertyId.Unknown)
								{
									goto IL_0C2B;
								}
							}
							else
							{
								this.ApplyCustomStyleProperty(reader);
							}
						}
						else
						{
							switch (stylePropertyId3)
							{
							case StylePropertyId.Color:
								this.inheritedData.Write().color = reader.ReadColor(0);
								break;
							case StylePropertyId.FontSize:
								this.inheritedData.Write().fontSize = reader.ReadLength(0);
								break;
							case StylePropertyId.LetterSpacing:
								this.inheritedData.Write().letterSpacing = reader.ReadLength(0);
								break;
							case StylePropertyId.TextShadow:
								this.inheritedData.Write().textShadow = reader.ReadTextShadow(0);
								break;
							case StylePropertyId.UnityEditorTextRenderingMode:
								this.inheritedData.Write().unityEditorTextRenderingMode = (EditorTextRenderingMode)reader.ReadEnum(StyleEnumType.EditorTextRenderingMode, 0);
								break;
							case StylePropertyId.UnityFont:
								this.inheritedData.Write().unityFont = reader.ReadFont(0);
								break;
							case StylePropertyId.UnityFontDefinition:
								this.inheritedData.Write().unityFontDefinition = reader.ReadFontDefinition(0);
								break;
							case StylePropertyId.UnityFontStyleAndWeight:
								this.inheritedData.Write().unityFontStyleAndWeight = (FontStyle)reader.ReadEnum(StyleEnumType.FontStyle, 0);
								break;
							case StylePropertyId.UnityParagraphSpacing:
								this.inheritedData.Write().unityParagraphSpacing = reader.ReadLength(0);
								break;
							case StylePropertyId.UnityTextAlign:
								this.inheritedData.Write().unityTextAlign = (TextAnchor)reader.ReadEnum(StyleEnumType.TextAnchor, 0);
								break;
							case StylePropertyId.UnityTextGenerator:
								this.inheritedData.Write().unityTextGenerator = (TextGeneratorType)reader.ReadEnum(StyleEnumType.TextGeneratorType, 0);
								break;
							case StylePropertyId.UnityTextOutlineColor:
								this.inheritedData.Write().unityTextOutlineColor = reader.ReadColor(0);
								break;
							case StylePropertyId.UnityTextOutlineWidth:
								this.inheritedData.Write().unityTextOutlineWidth = reader.ReadFloat(0);
								break;
							case StylePropertyId.Visibility:
								this.inheritedData.Write().visibility = (Visibility)reader.ReadEnum(StyleEnumType.Visibility, 0);
								break;
							case StylePropertyId.WhiteSpace:
								this.inheritedData.Write().whiteSpace = (WhiteSpace)reader.ReadEnum(StyleEnumType.WhiteSpace, 0);
								break;
							case StylePropertyId.WordSpacing:
								this.inheritedData.Write().wordSpacing = reader.ReadLength(0);
								break;
							default:
								switch (stylePropertyId3)
								{
								case StylePropertyId.AlignContent:
									this.layoutData.Write().alignContent = (Align)reader.ReadEnum(StyleEnumType.Align, 0);
									break;
								case StylePropertyId.AlignItems:
									this.layoutData.Write().alignItems = (Align)reader.ReadEnum(StyleEnumType.Align, 0);
									break;
								case StylePropertyId.AlignSelf:
									this.layoutData.Write().alignSelf = (Align)reader.ReadEnum(StyleEnumType.Align, 0);
									break;
								case StylePropertyId.BorderBottomWidth:
									this.layoutData.Write().borderBottomWidth = reader.ReadFloat(0);
									break;
								case StylePropertyId.BorderLeftWidth:
									this.layoutData.Write().borderLeftWidth = reader.ReadFloat(0);
									break;
								case StylePropertyId.BorderRightWidth:
									this.layoutData.Write().borderRightWidth = reader.ReadFloat(0);
									break;
								case StylePropertyId.BorderTopWidth:
									this.layoutData.Write().borderTopWidth = reader.ReadFloat(0);
									break;
								case StylePropertyId.Bottom:
									this.layoutData.Write().bottom = reader.ReadLength(0);
									break;
								case StylePropertyId.Display:
									this.layoutData.Write().display = (DisplayStyle)reader.ReadEnum(StyleEnumType.DisplayStyle, 0);
									break;
								case StylePropertyId.FlexBasis:
									this.layoutData.Write().flexBasis = reader.ReadLength(0);
									break;
								case StylePropertyId.FlexDirection:
									this.layoutData.Write().flexDirection = (FlexDirection)reader.ReadEnum(StyleEnumType.FlexDirection, 0);
									break;
								case StylePropertyId.FlexGrow:
									this.layoutData.Write().flexGrow = reader.ReadFloat(0);
									break;
								case StylePropertyId.FlexShrink:
									this.layoutData.Write().flexShrink = reader.ReadFloat(0);
									break;
								case StylePropertyId.FlexWrap:
									this.layoutData.Write().flexWrap = (Wrap)reader.ReadEnum(StyleEnumType.Wrap, 0);
									break;
								case StylePropertyId.Height:
									this.layoutData.Write().height = reader.ReadLength(0);
									break;
								case StylePropertyId.JustifyContent:
									this.layoutData.Write().justifyContent = (Justify)reader.ReadEnum(StyleEnumType.Justify, 0);
									break;
								case StylePropertyId.Left:
									this.layoutData.Write().left = reader.ReadLength(0);
									break;
								case StylePropertyId.MarginBottom:
									this.layoutData.Write().marginBottom = reader.ReadLength(0);
									break;
								case StylePropertyId.MarginLeft:
									this.layoutData.Write().marginLeft = reader.ReadLength(0);
									break;
								case StylePropertyId.MarginRight:
									this.layoutData.Write().marginRight = reader.ReadLength(0);
									break;
								case StylePropertyId.MarginTop:
									this.layoutData.Write().marginTop = reader.ReadLength(0);
									break;
								case StylePropertyId.MaxHeight:
									this.layoutData.Write().maxHeight = reader.ReadLength(0);
									break;
								case StylePropertyId.MaxWidth:
									this.layoutData.Write().maxWidth = reader.ReadLength(0);
									break;
								case StylePropertyId.MinHeight:
									this.layoutData.Write().minHeight = reader.ReadLength(0);
									break;
								case StylePropertyId.MinWidth:
									this.layoutData.Write().minWidth = reader.ReadLength(0);
									break;
								case StylePropertyId.PaddingBottom:
									this.layoutData.Write().paddingBottom = reader.ReadLength(0);
									break;
								case StylePropertyId.PaddingLeft:
									this.layoutData.Write().paddingLeft = reader.ReadLength(0);
									break;
								case StylePropertyId.PaddingRight:
									this.layoutData.Write().paddingRight = reader.ReadLength(0);
									break;
								case StylePropertyId.PaddingTop:
									this.layoutData.Write().paddingTop = reader.ReadLength(0);
									break;
								case StylePropertyId.Position:
									this.layoutData.Write().position = (Position)reader.ReadEnum(StyleEnumType.Position, 0);
									break;
								case StylePropertyId.Right:
									this.layoutData.Write().right = reader.ReadLength(0);
									break;
								case StylePropertyId.Top:
									this.layoutData.Write().top = reader.ReadLength(0);
									break;
								case StylePropertyId.Width:
									this.layoutData.Write().width = reader.ReadLength(0);
									break;
								default:
									goto IL_0C2B;
								}
								break;
							}
						}
					}
					else if (stylePropertyId3 <= StylePropertyId.UnityTextOutline)
					{
						switch (stylePropertyId3)
						{
						case StylePropertyId.Cursor:
							this.rareData.Write().cursor = reader.ReadCursor(0);
							break;
						case StylePropertyId.TextOverflow:
							this.rareData.Write().textOverflow = (TextOverflow)reader.ReadEnum(StyleEnumType.TextOverflow, 0);
							break;
						case StylePropertyId.UnityBackgroundImageTintColor:
							this.rareData.Write().unityBackgroundImageTintColor = reader.ReadColor(0);
							break;
						case StylePropertyId.UnityOverflowClipBox:
							this.rareData.Write().unityOverflowClipBox = (OverflowClipBox)reader.ReadEnum(StyleEnumType.OverflowClipBox, 0);
							break;
						case StylePropertyId.UnitySliceBottom:
							this.rareData.Write().unitySliceBottom = reader.ReadInt(0);
							break;
						case StylePropertyId.UnitySliceLeft:
							this.rareData.Write().unitySliceLeft = reader.ReadInt(0);
							break;
						case StylePropertyId.UnitySliceRight:
							this.rareData.Write().unitySliceRight = reader.ReadInt(0);
							break;
						case StylePropertyId.UnitySliceScale:
							this.rareData.Write().unitySliceScale = reader.ReadFloat(0);
							break;
						case StylePropertyId.UnitySliceTop:
							this.rareData.Write().unitySliceTop = reader.ReadInt(0);
							break;
						case StylePropertyId.UnitySliceType:
							this.rareData.Write().unitySliceType = (SliceType)reader.ReadEnum(StyleEnumType.SliceType, 0);
							break;
						case StylePropertyId.UnityTextOverflowPosition:
							this.rareData.Write().unityTextOverflowPosition = (TextOverflowPosition)reader.ReadEnum(StyleEnumType.TextOverflowPosition, 0);
							break;
						default:
							switch (stylePropertyId3)
							{
							case StylePropertyId.All:
								break;
							case StylePropertyId.BackgroundPosition:
								ShorthandApplicator.ApplyBackgroundPosition(reader, ref this);
								break;
							case StylePropertyId.BorderColor:
								ShorthandApplicator.ApplyBorderColor(reader, ref this);
								break;
							case StylePropertyId.BorderRadius:
								ShorthandApplicator.ApplyBorderRadius(reader, ref this);
								break;
							case StylePropertyId.BorderWidth:
								ShorthandApplicator.ApplyBorderWidth(reader, ref this);
								break;
							case StylePropertyId.Flex:
								ShorthandApplicator.ApplyFlex(reader, ref this);
								break;
							case StylePropertyId.Margin:
								ShorthandApplicator.ApplyMargin(reader, ref this);
								break;
							case StylePropertyId.Padding:
								ShorthandApplicator.ApplyPadding(reader, ref this);
								break;
							case StylePropertyId.Transition:
								ShorthandApplicator.ApplyTransition(reader, ref this);
								break;
							case StylePropertyId.UnityBackgroundScaleMode:
								ShorthandApplicator.ApplyUnityBackgroundScaleMode(reader, ref this);
								break;
							case StylePropertyId.UnityTextOutline:
								ShorthandApplicator.ApplyUnityTextOutline(reader, ref this);
								break;
							default:
								goto IL_0C2B;
							}
							break;
						}
					}
					else
					{
						switch (stylePropertyId3)
						{
						case StylePropertyId.Rotate:
							this.transformData.Write().rotate = reader.ReadRotate(0);
							break;
						case StylePropertyId.Scale:
							this.transformData.Write().scale = reader.ReadScale(0);
							break;
						case StylePropertyId.TransformOrigin:
							this.transformData.Write().transformOrigin = reader.ReadTransformOrigin(0);
							break;
						case StylePropertyId.Translate:
							this.transformData.Write().translate = reader.ReadTranslate(0);
							break;
						default:
							switch (stylePropertyId3)
							{
							case StylePropertyId.TransitionDelay:
								reader.ReadListTimeValue(this.transitionData.Write().transitionDelay, 0);
								this.ResetComputedTransitions();
								break;
							case StylePropertyId.TransitionDuration:
								reader.ReadListTimeValue(this.transitionData.Write().transitionDuration, 0);
								this.ResetComputedTransitions();
								break;
							case StylePropertyId.TransitionProperty:
								reader.ReadListStylePropertyName(this.transitionData.Write().transitionProperty, 0);
								this.ResetComputedTransitions();
								break;
							case StylePropertyId.TransitionTimingFunction:
								reader.ReadListEasingFunction(this.transitionData.Write().transitionTimingFunction, 0);
								this.ResetComputedTransitions();
								break;
							default:
								switch (stylePropertyId3)
								{
								case StylePropertyId.BackgroundColor:
									this.visualData.Write().backgroundColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BackgroundImage:
									this.visualData.Write().backgroundImage = reader.ReadBackground(0);
									break;
								case StylePropertyId.BackgroundPositionX:
									this.visualData.Write().backgroundPositionX = reader.ReadBackgroundPositionX(0);
									break;
								case StylePropertyId.BackgroundPositionY:
									this.visualData.Write().backgroundPositionY = reader.ReadBackgroundPositionY(0);
									break;
								case StylePropertyId.BackgroundRepeat:
									this.visualData.Write().backgroundRepeat = reader.ReadBackgroundRepeat(0);
									break;
								case StylePropertyId.BackgroundSize:
									this.visualData.Write().backgroundSize = reader.ReadBackgroundSize(0);
									break;
								case StylePropertyId.BorderBottomColor:
									this.visualData.Write().borderBottomColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BorderBottomLeftRadius:
									this.visualData.Write().borderBottomLeftRadius = reader.ReadLength(0);
									break;
								case StylePropertyId.BorderBottomRightRadius:
									this.visualData.Write().borderBottomRightRadius = reader.ReadLength(0);
									break;
								case StylePropertyId.BorderLeftColor:
									this.visualData.Write().borderLeftColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BorderRightColor:
									this.visualData.Write().borderRightColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BorderTopColor:
									this.visualData.Write().borderTopColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BorderTopLeftRadius:
									this.visualData.Write().borderTopLeftRadius = reader.ReadLength(0);
									break;
								case StylePropertyId.BorderTopRightRadius:
									this.visualData.Write().borderTopRightRadius = reader.ReadLength(0);
									break;
								case StylePropertyId.Opacity:
									this.visualData.Write().opacity = reader.ReadFloat(0);
									break;
								case StylePropertyId.Overflow:
									this.visualData.Write().overflow = (OverflowInternal)reader.ReadEnum(StyleEnumType.OverflowInternal, 0);
									break;
								default:
									goto IL_0C2B;
								}
								break;
							}
							break;
						}
					}
					goto IL_0C44;
					IL_0C2B:
					Debug.LogAssertion(string.Format("Unknown property id {0}", stylePropertyId));
				}
				IL_0C44:
				stylePropertyId = reader.MoveNextProperty();
			}
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0005FBE8 File Offset: 0x0005DDE8
		public void ApplyStyleValue(StyleValue sv, ref ComputedStyle parentStyle)
		{
			bool flag = this.ApplyGlobalKeyword(sv.id, sv.keyword, ref parentStyle);
			if (!flag)
			{
				StylePropertyId id = sv.id;
				StylePropertyId stylePropertyId = id;
				if (stylePropertyId <= StylePropertyId.Width)
				{
					switch (stylePropertyId)
					{
					case StylePropertyId.Color:
						this.inheritedData.Write().color = sv.color;
						return;
					case StylePropertyId.FontSize:
						this.inheritedData.Write().fontSize = sv.length;
						return;
					case StylePropertyId.LetterSpacing:
						this.inheritedData.Write().letterSpacing = sv.length;
						return;
					case StylePropertyId.TextShadow:
						break;
					case StylePropertyId.UnityEditorTextRenderingMode:
						this.inheritedData.Write().unityEditorTextRenderingMode = (EditorTextRenderingMode)sv.number;
						return;
					case StylePropertyId.UnityFont:
						this.inheritedData.Write().unityFont = (sv.resource.IsAllocated ? (sv.resource.Target as Font) : null);
						return;
					case StylePropertyId.UnityFontDefinition:
						this.inheritedData.Write().unityFontDefinition = (sv.resource.IsAllocated ? FontDefinition.FromObject(sv.resource.Target) : default(FontDefinition));
						return;
					case StylePropertyId.UnityFontStyleAndWeight:
						this.inheritedData.Write().unityFontStyleAndWeight = (FontStyle)sv.number;
						return;
					case StylePropertyId.UnityParagraphSpacing:
						this.inheritedData.Write().unityParagraphSpacing = sv.length;
						return;
					case StylePropertyId.UnityTextAlign:
						this.inheritedData.Write().unityTextAlign = (TextAnchor)sv.number;
						return;
					case StylePropertyId.UnityTextGenerator:
						this.inheritedData.Write().unityTextGenerator = (TextGeneratorType)sv.number;
						return;
					case StylePropertyId.UnityTextOutlineColor:
						this.inheritedData.Write().unityTextOutlineColor = sv.color;
						return;
					case StylePropertyId.UnityTextOutlineWidth:
						this.inheritedData.Write().unityTextOutlineWidth = sv.number;
						return;
					case StylePropertyId.Visibility:
						this.inheritedData.Write().visibility = (Visibility)sv.number;
						return;
					case StylePropertyId.WhiteSpace:
						this.inheritedData.Write().whiteSpace = (WhiteSpace)sv.number;
						return;
					case StylePropertyId.WordSpacing:
						this.inheritedData.Write().wordSpacing = sv.length;
						return;
					default:
						switch (stylePropertyId)
						{
						case StylePropertyId.AlignContent:
						{
							this.layoutData.Write().alignContent = (Align)sv.number;
							bool flag2 = sv.keyword == StyleKeyword.Auto;
							if (flag2)
							{
								this.layoutData.Write().alignContent = Align.Auto;
							}
							return;
						}
						case StylePropertyId.AlignItems:
						{
							this.layoutData.Write().alignItems = (Align)sv.number;
							bool flag3 = sv.keyword == StyleKeyword.Auto;
							if (flag3)
							{
								this.layoutData.Write().alignItems = Align.Auto;
							}
							return;
						}
						case StylePropertyId.AlignSelf:
						{
							this.layoutData.Write().alignSelf = (Align)sv.number;
							bool flag4 = sv.keyword == StyleKeyword.Auto;
							if (flag4)
							{
								this.layoutData.Write().alignSelf = Align.Auto;
							}
							return;
						}
						case StylePropertyId.BorderBottomWidth:
							this.layoutData.Write().borderBottomWidth = sv.number;
							return;
						case StylePropertyId.BorderLeftWidth:
							this.layoutData.Write().borderLeftWidth = sv.number;
							return;
						case StylePropertyId.BorderRightWidth:
							this.layoutData.Write().borderRightWidth = sv.number;
							return;
						case StylePropertyId.BorderTopWidth:
							this.layoutData.Write().borderTopWidth = sv.number;
							return;
						case StylePropertyId.Bottom:
							this.layoutData.Write().bottom = sv.length;
							return;
						case StylePropertyId.Display:
						{
							this.layoutData.Write().display = (DisplayStyle)sv.number;
							bool flag5 = sv.keyword == StyleKeyword.None;
							if (flag5)
							{
								this.layoutData.Write().display = DisplayStyle.None;
							}
							return;
						}
						case StylePropertyId.FlexBasis:
							this.layoutData.Write().flexBasis = sv.length;
							return;
						case StylePropertyId.FlexDirection:
							this.layoutData.Write().flexDirection = (FlexDirection)sv.number;
							return;
						case StylePropertyId.FlexGrow:
							this.layoutData.Write().flexGrow = sv.number;
							return;
						case StylePropertyId.FlexShrink:
							this.layoutData.Write().flexShrink = sv.number;
							return;
						case StylePropertyId.FlexWrap:
							this.layoutData.Write().flexWrap = (Wrap)sv.number;
							return;
						case StylePropertyId.Height:
							this.layoutData.Write().height = sv.length;
							return;
						case StylePropertyId.JustifyContent:
							this.layoutData.Write().justifyContent = (Justify)sv.number;
							return;
						case StylePropertyId.Left:
							this.layoutData.Write().left = sv.length;
							return;
						case StylePropertyId.MarginBottom:
							this.layoutData.Write().marginBottom = sv.length;
							return;
						case StylePropertyId.MarginLeft:
							this.layoutData.Write().marginLeft = sv.length;
							return;
						case StylePropertyId.MarginRight:
							this.layoutData.Write().marginRight = sv.length;
							return;
						case StylePropertyId.MarginTop:
							this.layoutData.Write().marginTop = sv.length;
							return;
						case StylePropertyId.MaxHeight:
							this.layoutData.Write().maxHeight = sv.length;
							return;
						case StylePropertyId.MaxWidth:
							this.layoutData.Write().maxWidth = sv.length;
							return;
						case StylePropertyId.MinHeight:
							this.layoutData.Write().minHeight = sv.length;
							return;
						case StylePropertyId.MinWidth:
							this.layoutData.Write().minWidth = sv.length;
							return;
						case StylePropertyId.PaddingBottom:
							this.layoutData.Write().paddingBottom = sv.length;
							return;
						case StylePropertyId.PaddingLeft:
							this.layoutData.Write().paddingLeft = sv.length;
							return;
						case StylePropertyId.PaddingRight:
							this.layoutData.Write().paddingRight = sv.length;
							return;
						case StylePropertyId.PaddingTop:
							this.layoutData.Write().paddingTop = sv.length;
							return;
						case StylePropertyId.Position:
							this.layoutData.Write().position = (Position)sv.number;
							return;
						case StylePropertyId.Right:
							this.layoutData.Write().right = sv.length;
							return;
						case StylePropertyId.Top:
							this.layoutData.Write().top = sv.length;
							return;
						case StylePropertyId.Width:
							this.layoutData.Write().width = sv.length;
							return;
						}
						break;
					}
				}
				else
				{
					switch (stylePropertyId)
					{
					case StylePropertyId.TextOverflow:
						this.rareData.Write().textOverflow = (TextOverflow)sv.number;
						return;
					case StylePropertyId.UnityBackgroundImageTintColor:
						this.rareData.Write().unityBackgroundImageTintColor = sv.color;
						return;
					case StylePropertyId.UnityOverflowClipBox:
						this.rareData.Write().unityOverflowClipBox = (OverflowClipBox)sv.number;
						return;
					case StylePropertyId.UnitySliceBottom:
						this.rareData.Write().unitySliceBottom = (int)sv.number;
						return;
					case StylePropertyId.UnitySliceLeft:
						this.rareData.Write().unitySliceLeft = (int)sv.number;
						return;
					case StylePropertyId.UnitySliceRight:
						this.rareData.Write().unitySliceRight = (int)sv.number;
						return;
					case StylePropertyId.UnitySliceScale:
						this.rareData.Write().unitySliceScale = sv.number;
						return;
					case StylePropertyId.UnitySliceTop:
						this.rareData.Write().unitySliceTop = (int)sv.number;
						return;
					case StylePropertyId.UnitySliceType:
						this.rareData.Write().unitySliceType = (SliceType)sv.number;
						return;
					case StylePropertyId.UnityTextOverflowPosition:
						this.rareData.Write().unityTextOverflowPosition = (TextOverflowPosition)sv.number;
						return;
					default:
						switch (stylePropertyId)
						{
						case StylePropertyId.BackgroundColor:
							this.visualData.Write().backgroundColor = sv.color;
							return;
						case StylePropertyId.BackgroundImage:
							this.visualData.Write().backgroundImage = (sv.resource.IsAllocated ? Background.FromObject(sv.resource.Target) : default(Background));
							return;
						case StylePropertyId.BackgroundPositionX:
							this.visualData.Write().backgroundPositionX = sv.position;
							return;
						case StylePropertyId.BackgroundPositionY:
							this.visualData.Write().backgroundPositionY = sv.position;
							return;
						case StylePropertyId.BackgroundRepeat:
							this.visualData.Write().backgroundRepeat = sv.repeat;
							return;
						case StylePropertyId.BorderBottomColor:
							this.visualData.Write().borderBottomColor = sv.color;
							return;
						case StylePropertyId.BorderBottomLeftRadius:
							this.visualData.Write().borderBottomLeftRadius = sv.length;
							return;
						case StylePropertyId.BorderBottomRightRadius:
							this.visualData.Write().borderBottomRightRadius = sv.length;
							return;
						case StylePropertyId.BorderLeftColor:
							this.visualData.Write().borderLeftColor = sv.color;
							return;
						case StylePropertyId.BorderRightColor:
							this.visualData.Write().borderRightColor = sv.color;
							return;
						case StylePropertyId.BorderTopColor:
							this.visualData.Write().borderTopColor = sv.color;
							return;
						case StylePropertyId.BorderTopLeftRadius:
							this.visualData.Write().borderTopLeftRadius = sv.length;
							return;
						case StylePropertyId.BorderTopRightRadius:
							this.visualData.Write().borderTopRightRadius = sv.length;
							return;
						case StylePropertyId.Opacity:
							this.visualData.Write().opacity = sv.number;
							return;
						case StylePropertyId.Overflow:
							this.visualData.Write().overflow = (OverflowInternal)sv.number;
							return;
						}
						break;
					}
				}
				Debug.LogAssertion(string.Format("Unexpected property id {0}", sv.id));
			}
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0006064C File Offset: 0x0005E84C
		public void ApplyStyleValueManaged(StyleValueManaged sv, ref ComputedStyle parentStyle)
		{
			bool flag = this.ApplyGlobalKeyword(sv.id, sv.keyword, ref parentStyle);
			if (!flag)
			{
				switch (sv.id)
				{
				case StylePropertyId.TransitionDelay:
				{
					bool flag2 = sv.value == null;
					if (flag2)
					{
						this.transitionData.Write().transitionDelay.CopyFrom<TimeValue>(InitialStyle.transitionDelay);
					}
					else
					{
						this.transitionData.Write().transitionDelay = sv.value as List<TimeValue>;
					}
					this.ResetComputedTransitions();
					break;
				}
				case StylePropertyId.TransitionDuration:
				{
					bool flag3 = sv.value == null;
					if (flag3)
					{
						this.transitionData.Write().transitionDuration.CopyFrom<TimeValue>(InitialStyle.transitionDuration);
					}
					else
					{
						this.transitionData.Write().transitionDuration = sv.value as List<TimeValue>;
					}
					this.ResetComputedTransitions();
					break;
				}
				case StylePropertyId.TransitionProperty:
				{
					bool flag4 = sv.value == null;
					if (flag4)
					{
						this.transitionData.Write().transitionProperty.CopyFrom<StylePropertyName>(InitialStyle.transitionProperty);
					}
					else
					{
						this.transitionData.Write().transitionProperty = sv.value as List<StylePropertyName>;
					}
					this.ResetComputedTransitions();
					break;
				}
				case StylePropertyId.TransitionTimingFunction:
				{
					bool flag5 = sv.value == null;
					if (flag5)
					{
						this.transitionData.Write().transitionTimingFunction.CopyFrom<EasingFunction>(InitialStyle.transitionTimingFunction);
					}
					else
					{
						this.transitionData.Write().transitionTimingFunction = sv.value as List<EasingFunction>;
					}
					this.ResetComputedTransitions();
					break;
				}
				default:
					Debug.LogAssertion(string.Format("Unexpected property id {0}", sv.id));
					break;
				}
			}
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00060801 File Offset: 0x0005EA01
		public void ApplyStyleCursor(Cursor cursor)
		{
			this.rareData.Write().cursor = cursor;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x00060815 File Offset: 0x0005EA15
		public void ApplyStyleTextShadow(TextShadow st)
		{
			this.inheritedData.Write().textShadow = st;
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0006082C File Offset: 0x0005EA2C
		public void ApplyFromComputedStyle(StylePropertyId id, ref ComputedStyle other)
		{
			if (id <= StylePropertyId.UnityTextOverflowPosition)
			{
				switch (id)
				{
				case StylePropertyId.Color:
					this.inheritedData.Write().color = other.inheritedData.Read().color;
					return;
				case StylePropertyId.FontSize:
					this.inheritedData.Write().fontSize = other.inheritedData.Read().fontSize;
					return;
				case StylePropertyId.LetterSpacing:
					this.inheritedData.Write().letterSpacing = other.inheritedData.Read().letterSpacing;
					return;
				case StylePropertyId.TextShadow:
					this.inheritedData.Write().textShadow = other.inheritedData.Read().textShadow;
					return;
				case StylePropertyId.UnityEditorTextRenderingMode:
					this.inheritedData.Write().unityEditorTextRenderingMode = other.inheritedData.Read().unityEditorTextRenderingMode;
					return;
				case StylePropertyId.UnityFont:
					this.inheritedData.Write().unityFont = other.inheritedData.Read().unityFont;
					return;
				case StylePropertyId.UnityFontDefinition:
					this.inheritedData.Write().unityFontDefinition = other.inheritedData.Read().unityFontDefinition;
					return;
				case StylePropertyId.UnityFontStyleAndWeight:
					this.inheritedData.Write().unityFontStyleAndWeight = other.inheritedData.Read().unityFontStyleAndWeight;
					return;
				case StylePropertyId.UnityParagraphSpacing:
					this.inheritedData.Write().unityParagraphSpacing = other.inheritedData.Read().unityParagraphSpacing;
					return;
				case StylePropertyId.UnityTextAlign:
					this.inheritedData.Write().unityTextAlign = other.inheritedData.Read().unityTextAlign;
					return;
				case StylePropertyId.UnityTextGenerator:
					this.inheritedData.Write().unityTextGenerator = other.inheritedData.Read().unityTextGenerator;
					return;
				case StylePropertyId.UnityTextOutlineColor:
					this.inheritedData.Write().unityTextOutlineColor = other.inheritedData.Read().unityTextOutlineColor;
					return;
				case StylePropertyId.UnityTextOutlineWidth:
					this.inheritedData.Write().unityTextOutlineWidth = other.inheritedData.Read().unityTextOutlineWidth;
					return;
				case StylePropertyId.Visibility:
					this.inheritedData.Write().visibility = other.inheritedData.Read().visibility;
					return;
				case StylePropertyId.WhiteSpace:
					this.inheritedData.Write().whiteSpace = other.inheritedData.Read().whiteSpace;
					return;
				case StylePropertyId.WordSpacing:
					this.inheritedData.Write().wordSpacing = other.inheritedData.Read().wordSpacing;
					return;
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
						this.layoutData.Write().alignContent = other.layoutData.Read().alignContent;
						return;
					case StylePropertyId.AlignItems:
						this.layoutData.Write().alignItems = other.layoutData.Read().alignItems;
						return;
					case StylePropertyId.AlignSelf:
						this.layoutData.Write().alignSelf = other.layoutData.Read().alignSelf;
						return;
					case StylePropertyId.BorderBottomWidth:
						this.layoutData.Write().borderBottomWidth = other.layoutData.Read().borderBottomWidth;
						return;
					case StylePropertyId.BorderLeftWidth:
						this.layoutData.Write().borderLeftWidth = other.layoutData.Read().borderLeftWidth;
						return;
					case StylePropertyId.BorderRightWidth:
						this.layoutData.Write().borderRightWidth = other.layoutData.Read().borderRightWidth;
						return;
					case StylePropertyId.BorderTopWidth:
						this.layoutData.Write().borderTopWidth = other.layoutData.Read().borderTopWidth;
						return;
					case StylePropertyId.Bottom:
						this.layoutData.Write().bottom = other.layoutData.Read().bottom;
						return;
					case StylePropertyId.Display:
						this.layoutData.Write().display = other.layoutData.Read().display;
						return;
					case StylePropertyId.FlexBasis:
						this.layoutData.Write().flexBasis = other.layoutData.Read().flexBasis;
						return;
					case StylePropertyId.FlexDirection:
						this.layoutData.Write().flexDirection = other.layoutData.Read().flexDirection;
						return;
					case StylePropertyId.FlexGrow:
						this.layoutData.Write().flexGrow = other.layoutData.Read().flexGrow;
						return;
					case StylePropertyId.FlexShrink:
						this.layoutData.Write().flexShrink = other.layoutData.Read().flexShrink;
						return;
					case StylePropertyId.FlexWrap:
						this.layoutData.Write().flexWrap = other.layoutData.Read().flexWrap;
						return;
					case StylePropertyId.Height:
						this.layoutData.Write().height = other.layoutData.Read().height;
						return;
					case StylePropertyId.JustifyContent:
						this.layoutData.Write().justifyContent = other.layoutData.Read().justifyContent;
						return;
					case StylePropertyId.Left:
						this.layoutData.Write().left = other.layoutData.Read().left;
						return;
					case StylePropertyId.MarginBottom:
						this.layoutData.Write().marginBottom = other.layoutData.Read().marginBottom;
						return;
					case StylePropertyId.MarginLeft:
						this.layoutData.Write().marginLeft = other.layoutData.Read().marginLeft;
						return;
					case StylePropertyId.MarginRight:
						this.layoutData.Write().marginRight = other.layoutData.Read().marginRight;
						return;
					case StylePropertyId.MarginTop:
						this.layoutData.Write().marginTop = other.layoutData.Read().marginTop;
						return;
					case StylePropertyId.MaxHeight:
						this.layoutData.Write().maxHeight = other.layoutData.Read().maxHeight;
						return;
					case StylePropertyId.MaxWidth:
						this.layoutData.Write().maxWidth = other.layoutData.Read().maxWidth;
						return;
					case StylePropertyId.MinHeight:
						this.layoutData.Write().minHeight = other.layoutData.Read().minHeight;
						return;
					case StylePropertyId.MinWidth:
						this.layoutData.Write().minWidth = other.layoutData.Read().minWidth;
						return;
					case StylePropertyId.PaddingBottom:
						this.layoutData.Write().paddingBottom = other.layoutData.Read().paddingBottom;
						return;
					case StylePropertyId.PaddingLeft:
						this.layoutData.Write().paddingLeft = other.layoutData.Read().paddingLeft;
						return;
					case StylePropertyId.PaddingRight:
						this.layoutData.Write().paddingRight = other.layoutData.Read().paddingRight;
						return;
					case StylePropertyId.PaddingTop:
						this.layoutData.Write().paddingTop = other.layoutData.Read().paddingTop;
						return;
					case StylePropertyId.Position:
						this.layoutData.Write().position = other.layoutData.Read().position;
						return;
					case StylePropertyId.Right:
						this.layoutData.Write().right = other.layoutData.Read().right;
						return;
					case StylePropertyId.Top:
						this.layoutData.Write().top = other.layoutData.Read().top;
						return;
					case StylePropertyId.Width:
						this.layoutData.Write().width = other.layoutData.Read().width;
						return;
					default:
						switch (id)
						{
						case StylePropertyId.Cursor:
							this.rareData.Write().cursor = other.rareData.Read().cursor;
							return;
						case StylePropertyId.TextOverflow:
							this.rareData.Write().textOverflow = other.rareData.Read().textOverflow;
							return;
						case StylePropertyId.UnityBackgroundImageTintColor:
							this.rareData.Write().unityBackgroundImageTintColor = other.rareData.Read().unityBackgroundImageTintColor;
							return;
						case StylePropertyId.UnityOverflowClipBox:
							this.rareData.Write().unityOverflowClipBox = other.rareData.Read().unityOverflowClipBox;
							return;
						case StylePropertyId.UnitySliceBottom:
							this.rareData.Write().unitySliceBottom = other.rareData.Read().unitySliceBottom;
							return;
						case StylePropertyId.UnitySliceLeft:
							this.rareData.Write().unitySliceLeft = other.rareData.Read().unitySliceLeft;
							return;
						case StylePropertyId.UnitySliceRight:
							this.rareData.Write().unitySliceRight = other.rareData.Read().unitySliceRight;
							return;
						case StylePropertyId.UnitySliceScale:
							this.rareData.Write().unitySliceScale = other.rareData.Read().unitySliceScale;
							return;
						case StylePropertyId.UnitySliceTop:
							this.rareData.Write().unitySliceTop = other.rareData.Read().unitySliceTop;
							return;
						case StylePropertyId.UnitySliceType:
							this.rareData.Write().unitySliceType = other.rareData.Read().unitySliceType;
							return;
						case StylePropertyId.UnityTextOverflowPosition:
							this.rareData.Write().unityTextOverflowPosition = other.rareData.Read().unityTextOverflowPosition;
							return;
						}
						break;
					}
					break;
				}
			}
			else
			{
				switch (id)
				{
				case StylePropertyId.Rotate:
					this.transformData.Write().rotate = other.transformData.Read().rotate;
					return;
				case StylePropertyId.Scale:
					this.transformData.Write().scale = other.transformData.Read().scale;
					return;
				case StylePropertyId.TransformOrigin:
					this.transformData.Write().transformOrigin = other.transformData.Read().transformOrigin;
					return;
				case StylePropertyId.Translate:
					this.transformData.Write().translate = other.transformData.Read().translate;
					return;
				default:
					switch (id)
					{
					case StylePropertyId.TransitionDelay:
						this.transitionData.Write().transitionDelay.CopyFrom<TimeValue>(other.transitionData.Read().transitionDelay);
						this.ResetComputedTransitions();
						return;
					case StylePropertyId.TransitionDuration:
						this.transitionData.Write().transitionDuration.CopyFrom<TimeValue>(other.transitionData.Read().transitionDuration);
						this.ResetComputedTransitions();
						return;
					case StylePropertyId.TransitionProperty:
						this.transitionData.Write().transitionProperty.CopyFrom<StylePropertyName>(other.transitionData.Read().transitionProperty);
						this.ResetComputedTransitions();
						return;
					case StylePropertyId.TransitionTimingFunction:
						this.transitionData.Write().transitionTimingFunction.CopyFrom<EasingFunction>(other.transitionData.Read().transitionTimingFunction);
						this.ResetComputedTransitions();
						return;
					default:
						switch (id)
						{
						case StylePropertyId.BackgroundColor:
							this.visualData.Write().backgroundColor = other.visualData.Read().backgroundColor;
							return;
						case StylePropertyId.BackgroundImage:
							this.visualData.Write().backgroundImage = other.visualData.Read().backgroundImage;
							return;
						case StylePropertyId.BackgroundPositionX:
							this.visualData.Write().backgroundPositionX = other.visualData.Read().backgroundPositionX;
							return;
						case StylePropertyId.BackgroundPositionY:
							this.visualData.Write().backgroundPositionY = other.visualData.Read().backgroundPositionY;
							return;
						case StylePropertyId.BackgroundRepeat:
							this.visualData.Write().backgroundRepeat = other.visualData.Read().backgroundRepeat;
							return;
						case StylePropertyId.BackgroundSize:
							this.visualData.Write().backgroundSize = other.visualData.Read().backgroundSize;
							return;
						case StylePropertyId.BorderBottomColor:
							this.visualData.Write().borderBottomColor = other.visualData.Read().borderBottomColor;
							return;
						case StylePropertyId.BorderBottomLeftRadius:
							this.visualData.Write().borderBottomLeftRadius = other.visualData.Read().borderBottomLeftRadius;
							return;
						case StylePropertyId.BorderBottomRightRadius:
							this.visualData.Write().borderBottomRightRadius = other.visualData.Read().borderBottomRightRadius;
							return;
						case StylePropertyId.BorderLeftColor:
							this.visualData.Write().borderLeftColor = other.visualData.Read().borderLeftColor;
							return;
						case StylePropertyId.BorderRightColor:
							this.visualData.Write().borderRightColor = other.visualData.Read().borderRightColor;
							return;
						case StylePropertyId.BorderTopColor:
							this.visualData.Write().borderTopColor = other.visualData.Read().borderTopColor;
							return;
						case StylePropertyId.BorderTopLeftRadius:
							this.visualData.Write().borderTopLeftRadius = other.visualData.Read().borderTopLeftRadius;
							return;
						case StylePropertyId.BorderTopRightRadius:
							this.visualData.Write().borderTopRightRadius = other.visualData.Read().borderTopRightRadius;
							return;
						case StylePropertyId.Opacity:
							this.visualData.Write().opacity = other.visualData.Read().opacity;
							return;
						case StylePropertyId.Overflow:
							this.visualData.Write().overflow = other.visualData.Read().overflow;
							return;
						}
						break;
					}
					break;
				}
			}
			Debug.LogAssertion(string.Format("Unexpected property id {0}", id));
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00061658 File Offset: 0x0005F858
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Length newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 <= StylePropertyId.UnityParagraphSpacing)
			{
				if (stylePropertyId2 == StylePropertyId.FontSize)
				{
					this.inheritedData.Write().fontSize = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
				if (stylePropertyId2 == StylePropertyId.LetterSpacing)
				{
					this.inheritedData.Write().letterSpacing = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
				if (stylePropertyId2 == StylePropertyId.UnityParagraphSpacing)
				{
					this.inheritedData.Write().unityParagraphSpacing = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
			}
			else
			{
				if (stylePropertyId2 == StylePropertyId.WordSpacing)
				{
					this.inheritedData.Write().wordSpacing = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
				switch (stylePropertyId2)
				{
				case StylePropertyId.Bottom:
					this.layoutData.Write().bottom = newValue;
					ve.layoutNode.Bottom = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Display:
				case StylePropertyId.FlexDirection:
				case StylePropertyId.FlexGrow:
				case StylePropertyId.FlexShrink:
				case StylePropertyId.FlexWrap:
				case StylePropertyId.JustifyContent:
				case StylePropertyId.Position:
					break;
				case StylePropertyId.FlexBasis:
					this.layoutData.Write().flexBasis = newValue;
					ve.layoutNode.FlexBasis = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Height:
					this.layoutData.Write().height = newValue;
					ve.layoutNode.Height = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Left:
					this.layoutData.Write().left = newValue;
					ve.layoutNode.Left = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MarginBottom:
					this.layoutData.Write().marginBottom = newValue;
					ve.layoutNode.MarginBottom = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MarginLeft:
					this.layoutData.Write().marginLeft = newValue;
					ve.layoutNode.MarginLeft = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MarginRight:
					this.layoutData.Write().marginRight = newValue;
					ve.layoutNode.MarginRight = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MarginTop:
					this.layoutData.Write().marginTop = newValue;
					ve.layoutNode.MarginTop = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MaxHeight:
					this.layoutData.Write().maxHeight = newValue;
					ve.layoutNode.MaxHeight = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MaxWidth:
					this.layoutData.Write().maxWidth = newValue;
					ve.layoutNode.MaxWidth = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MinHeight:
					this.layoutData.Write().minHeight = newValue;
					ve.layoutNode.MinHeight = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MinWidth:
					this.layoutData.Write().minWidth = newValue;
					ve.layoutNode.MinWidth = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.PaddingBottom:
					this.layoutData.Write().paddingBottom = newValue;
					ve.layoutNode.PaddingBottom = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.PaddingLeft:
					this.layoutData.Write().paddingLeft = newValue;
					ve.layoutNode.PaddingLeft = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.PaddingRight:
					this.layoutData.Write().paddingRight = newValue;
					ve.layoutNode.PaddingRight = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.PaddingTop:
					this.layoutData.Write().paddingTop = newValue;
					ve.layoutNode.PaddingTop = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Right:
					this.layoutData.Write().right = newValue;
					ve.layoutNode.Right = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Top:
					this.layoutData.Write().top = newValue;
					ve.layoutNode.Top = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Width:
					this.layoutData.Write().width = newValue;
					ve.layoutNode.Width = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				default:
					switch (stylePropertyId2)
					{
					case StylePropertyId.BorderBottomLeftRadius:
						this.visualData.Write().borderBottomLeftRadius = newValue;
						ve.IncrementVersion(VersionChangeType.BorderRadius | VersionChangeType.Repaint);
						return;
					case StylePropertyId.BorderBottomRightRadius:
						this.visualData.Write().borderBottomRightRadius = newValue;
						ve.IncrementVersion(VersionChangeType.BorderRadius | VersionChangeType.Repaint);
						return;
					case StylePropertyId.BorderTopLeftRadius:
						this.visualData.Write().borderTopLeftRadius = newValue;
						ve.IncrementVersion(VersionChangeType.BorderRadius | VersionChangeType.Repaint);
						return;
					case StylePropertyId.BorderTopRightRadius:
						this.visualData.Write().borderTopRightRadius = newValue;
						ve.IncrementVersion(VersionChangeType.BorderRadius | VersionChangeType.Repaint);
						return;
					}
					break;
				}
			}
			throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Length' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00061C0C File Offset: 0x0005FE0C
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, float newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 <= StylePropertyId.FlexShrink)
			{
				if (stylePropertyId2 == StylePropertyId.UnityTextOutlineWidth)
				{
					this.inheritedData.Write().unityTextOutlineWidth = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
				switch (stylePropertyId2)
				{
				case StylePropertyId.BorderBottomWidth:
					this.layoutData.Write().borderBottomWidth = newValue;
					ve.layoutNode.BorderBottomWidth = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					return;
				case StylePropertyId.BorderLeftWidth:
					this.layoutData.Write().borderLeftWidth = newValue;
					ve.layoutNode.BorderLeftWidth = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					return;
				case StylePropertyId.BorderRightWidth:
					this.layoutData.Write().borderRightWidth = newValue;
					ve.layoutNode.BorderRightWidth = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					return;
				case StylePropertyId.BorderTopWidth:
					this.layoutData.Write().borderTopWidth = newValue;
					ve.layoutNode.BorderTopWidth = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					return;
				case StylePropertyId.FlexGrow:
					this.layoutData.Write().flexGrow = newValue;
					ve.layoutNode.FlexGrow = newValue;
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.FlexShrink:
					this.layoutData.Write().flexShrink = newValue;
					ve.layoutNode.FlexShrink = newValue;
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				}
			}
			else
			{
				if (stylePropertyId2 == StylePropertyId.UnitySliceScale)
				{
					this.rareData.Write().unitySliceScale = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					return;
				}
				if (stylePropertyId2 == StylePropertyId.Opacity)
				{
					this.visualData.Write().opacity = newValue;
					ve.IncrementVersion(VersionChangeType.Opacity);
					return;
				}
			}
			throw new ArgumentException("Invalid animation property id. Can't apply value of type 'float' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00061E20 File Offset: 0x00060020
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, int newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 <= StylePropertyId.AlignSelf)
			{
				if (stylePropertyId2 <= StylePropertyId.UnityTextAlign)
				{
					if (stylePropertyId2 == StylePropertyId.UnityFontStyleAndWeight)
					{
						bool flag = this.inheritedData.Read().unityFontStyleAndWeight != (FontStyle)newValue;
						if (flag)
						{
							this.inheritedData.Write().unityFontStyleAndWeight = (FontStyle)newValue;
							ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
						}
						return;
					}
					if (stylePropertyId2 == StylePropertyId.UnityTextAlign)
					{
						bool flag2 = this.inheritedData.Read().unityTextAlign != (TextAnchor)newValue;
						if (flag2)
						{
							this.inheritedData.Write().unityTextAlign = (TextAnchor)newValue;
							ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Repaint);
						}
						return;
					}
				}
				else
				{
					if (stylePropertyId2 == StylePropertyId.Visibility)
					{
						bool flag3 = this.inheritedData.Read().visibility != (Visibility)newValue;
						if (flag3)
						{
							this.inheritedData.Write().visibility = (Visibility)newValue;
							ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Repaint | VersionChangeType.Picking);
						}
						return;
					}
					if (stylePropertyId2 == StylePropertyId.WhiteSpace)
					{
						bool flag4 = this.inheritedData.Read().whiteSpace != (WhiteSpace)newValue;
						if (flag4)
						{
							this.inheritedData.Write().whiteSpace = (WhiteSpace)newValue;
							ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
						}
						return;
					}
					switch (stylePropertyId2)
					{
					case StylePropertyId.AlignContent:
					{
						bool flag5 = this.layoutData.Read().alignContent != (Align)newValue;
						if (flag5)
						{
							this.layoutData.Write().alignContent = (Align)newValue;
							ve.layoutNode.AlignContent = (LayoutAlign)newValue;
							ve.IncrementVersion(VersionChangeType.Layout);
						}
						return;
					}
					case StylePropertyId.AlignItems:
					{
						bool flag6 = this.layoutData.Read().alignItems != (Align)newValue;
						if (flag6)
						{
							this.layoutData.Write().alignItems = (Align)newValue;
							ve.layoutNode.AlignItems = (LayoutAlign)newValue;
							ve.IncrementVersion(VersionChangeType.Layout);
						}
						return;
					}
					case StylePropertyId.AlignSelf:
					{
						bool flag7 = this.layoutData.Read().alignSelf != (Align)newValue;
						if (flag7)
						{
							this.layoutData.Write().alignSelf = (Align)newValue;
							ve.layoutNode.AlignSelf = (LayoutAlign)newValue;
							ve.IncrementVersion(VersionChangeType.Layout);
						}
						return;
					}
					}
				}
			}
			else if (stylePropertyId2 <= StylePropertyId.JustifyContent)
			{
				if (stylePropertyId2 == StylePropertyId.FlexDirection)
				{
					bool flag8 = this.layoutData.Read().flexDirection != (FlexDirection)newValue;
					if (flag8)
					{
						this.layoutData.Write().flexDirection = (FlexDirection)newValue;
						ve.layoutNode.FlexDirection = (LayoutFlexDirection)newValue;
						ve.IncrementVersion(VersionChangeType.Layout);
					}
					return;
				}
				if (stylePropertyId2 == StylePropertyId.FlexWrap)
				{
					bool flag9 = this.layoutData.Read().flexWrap != (Wrap)newValue;
					if (flag9)
					{
						this.layoutData.Write().flexWrap = (Wrap)newValue;
						ve.layoutNode.Wrap = (LayoutWrap)newValue;
						ve.IncrementVersion(VersionChangeType.Layout);
					}
					return;
				}
				if (stylePropertyId2 == StylePropertyId.JustifyContent)
				{
					bool flag10 = this.layoutData.Read().justifyContent != (Justify)newValue;
					if (flag10)
					{
						this.layoutData.Write().justifyContent = (Justify)newValue;
						ve.layoutNode.JustifyContent = (LayoutJustify)newValue;
						ve.IncrementVersion(VersionChangeType.Layout);
					}
					return;
				}
			}
			else
			{
				if (stylePropertyId2 == StylePropertyId.Position)
				{
					bool flag11 = this.layoutData.Read().position != (Position)newValue;
					if (flag11)
					{
						this.layoutData.Write().position = (Position)newValue;
						ve.layoutNode.PositionType = (LayoutPositionType)newValue;
						ve.IncrementVersion(VersionChangeType.Layout);
					}
					return;
				}
				switch (stylePropertyId2)
				{
				case StylePropertyId.TextOverflow:
				{
					bool flag12 = this.rareData.Read().textOverflow != (TextOverflow)newValue;
					if (flag12)
					{
						this.rareData.Write().textOverflow = (TextOverflow)newValue;
						ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					}
					return;
				}
				case StylePropertyId.UnityBackgroundImageTintColor:
				case StylePropertyId.UnitySliceScale:
					break;
				case StylePropertyId.UnityOverflowClipBox:
				{
					bool flag13 = this.rareData.Read().unityOverflowClipBox != (OverflowClipBox)newValue;
					if (flag13)
					{
						this.rareData.Write().unityOverflowClipBox = (OverflowClipBox)newValue;
						ve.IncrementVersion(VersionChangeType.Repaint);
					}
					return;
				}
				case StylePropertyId.UnitySliceBottom:
					this.rareData.Write().unitySliceBottom = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnitySliceLeft:
					this.rareData.Write().unitySliceLeft = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnitySliceRight:
					this.rareData.Write().unitySliceRight = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnitySliceTop:
					this.rareData.Write().unitySliceTop = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnitySliceType:
				{
					bool flag14 = this.rareData.Read().unitySliceType != (SliceType)newValue;
					if (flag14)
					{
						this.rareData.Write().unitySliceType = (SliceType)newValue;
						ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					}
					return;
				}
				case StylePropertyId.UnityTextOverflowPosition:
				{
					bool flag15 = this.rareData.Read().unityTextOverflowPosition != (TextOverflowPosition)newValue;
					if (flag15)
					{
						this.rareData.Write().unityTextOverflowPosition = (TextOverflowPosition)newValue;
						ve.IncrementVersion(VersionChangeType.Repaint);
					}
					return;
				}
				default:
					if (stylePropertyId2 == StylePropertyId.Overflow)
					{
						bool flag16 = this.visualData.Read().overflow != (OverflowInternal)newValue;
						if (flag16)
						{
							this.visualData.Write().overflow = (OverflowInternal)newValue;
							ve.layoutNode.Overflow = (LayoutOverflow)newValue;
							ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Overflow);
						}
						return;
					}
					break;
				}
			}
			throw new ArgumentException("Invalid animation property id. Can't apply value of type 'int' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0006240C File Offset: 0x0006060C
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, BackgroundPosition newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.BackgroundPositionX)
			{
				if (stylePropertyId2 != StylePropertyId.BackgroundPositionY)
				{
					throw new ArgumentException("Invalid animation property id. Can't apply value of type 'BackgroundPosition' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
				}
				bool flag = this.visualData.Read().backgroundPositionY != newValue;
				if (flag)
				{
					this.visualData.Write().backgroundPositionY = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
				}
			}
			else
			{
				bool flag2 = this.visualData.Read().backgroundPositionX != newValue;
				if (flag2)
				{
					this.visualData.Write().backgroundPositionX = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000624D0 File Offset: 0x000606D0
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, BackgroundRepeat newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.BackgroundRepeat)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'BackgroundRepeat' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			bool flag = this.visualData.Read().backgroundRepeat != newValue;
			if (flag)
			{
				this.visualData.Write().backgroundRepeat = newValue;
				ve.IncrementVersion(VersionChangeType.Repaint);
			}
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00062550 File Offset: 0x00060750
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, BackgroundSize newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.BackgroundSize)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'BackgroundSize' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.visualData.Write().backgroundSize = newValue;
			ve.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000625B4 File Offset: 0x000607B4
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Color newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 <= StylePropertyId.UnityTextOutlineColor)
			{
				if (stylePropertyId2 == StylePropertyId.Color)
				{
					this.inheritedData.Write().color = newValue;
					ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Color);
					return;
				}
				if (stylePropertyId2 == StylePropertyId.UnityTextOutlineColor)
				{
					this.inheritedData.Write().unityTextOutlineColor = newValue;
					ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
			}
			else
			{
				if (stylePropertyId2 == StylePropertyId.UnityBackgroundImageTintColor)
				{
					this.rareData.Write().unityBackgroundImageTintColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				}
				if (stylePropertyId2 == StylePropertyId.BackgroundColor)
				{
					this.visualData.Write().backgroundColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				}
				switch (stylePropertyId2)
				{
				case StylePropertyId.BorderBottomColor:
					this.visualData.Write().borderBottomColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				case StylePropertyId.BorderLeftColor:
					this.visualData.Write().borderLeftColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				case StylePropertyId.BorderRightColor:
					this.visualData.Write().borderRightColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				case StylePropertyId.BorderTopColor:
					this.visualData.Write().borderTopColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				}
			}
			throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Color' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0006275C File Offset: 0x0006095C
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Background newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.BackgroundImage)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Background' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			bool flag = this.visualData.Read().backgroundImage != newValue;
			if (flag)
			{
				this.visualData.Write().backgroundImage = newValue;
				ve.IncrementVersion(VersionChangeType.Overflow | VersionChangeType.Repaint);
			}
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x000627DC File Offset: 0x000609DC
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Font newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.UnityFont)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Font' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			bool flag = this.inheritedData.Read().unityFont != newValue;
			if (flag)
			{
				this.inheritedData.Write().unityFont = newValue;
				ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
			}
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0006285C File Offset: 0x00060A5C
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, FontDefinition newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.UnityFontDefinition)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'FontDefinition' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			bool flag = this.inheritedData.Read().unityFontDefinition != newValue;
			if (flag)
			{
				this.inheritedData.Write().unityFontDefinition = newValue;
				ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
			}
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x000628DC File Offset: 0x00060ADC
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, TextShadow newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.TextShadow)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'TextShadow' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.inheritedData.Write().textShadow = newValue;
			ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Repaint);
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00062940 File Offset: 0x00060B40
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Translate newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.Translate)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Translate' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.transformData.Write().translate = newValue;
			ve.IncrementVersion(VersionChangeType.Transform);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x000629A4 File Offset: 0x00060BA4
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, TransformOrigin newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.TransformOrigin)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'TransformOrigin' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.transformData.Write().transformOrigin = newValue;
			ve.IncrementVersion(VersionChangeType.Transform);
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00062A08 File Offset: 0x00060C08
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Rotate newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.Rotate)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Rotate' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.transformData.Write().rotate = newValue;
			ve.IncrementVersion(VersionChangeType.Transform);
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00062A6C File Offset: 0x00060C6C
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Scale newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.Scale)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Scale' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.transformData.Write().scale = newValue;
			ve.IncrementVersion(VersionChangeType.Transform);
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x00062AD0 File Offset: 0x00060CD0
		public static bool StartAnimation(VisualElement element, StylePropertyId id, ref ComputedStyle oldStyle, ref ComputedStyle newStyle, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			if (id <= StylePropertyId.UnityTextOverflowPosition)
			{
				switch (id)
				{
				case StylePropertyId.Color:
				{
					bool flag = element.styleAnimation.Start(StylePropertyId.Color, oldStyle.inheritedData.Read().color, newStyle.inheritedData.Read().color, durationMs, delayMs, easingCurve);
					bool flag2 = flag && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
					if (flag2)
					{
						element.usageHints |= UsageHints.DynamicColor;
					}
					return flag;
				}
				case StylePropertyId.FontSize:
					return element.styleAnimation.Start(StylePropertyId.FontSize, oldStyle.inheritedData.Read().fontSize, newStyle.inheritedData.Read().fontSize, durationMs, delayMs, easingCurve);
				case StylePropertyId.LetterSpacing:
					return element.styleAnimation.Start(StylePropertyId.LetterSpacing, oldStyle.inheritedData.Read().letterSpacing, newStyle.inheritedData.Read().letterSpacing, durationMs, delayMs, easingCurve);
				case StylePropertyId.TextShadow:
					return element.styleAnimation.Start(StylePropertyId.TextShadow, oldStyle.inheritedData.Read().textShadow, newStyle.inheritedData.Read().textShadow, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityEditorTextRenderingMode:
				case StylePropertyId.UnityTextGenerator:
					break;
				case StylePropertyId.UnityFont:
					return element.styleAnimation.Start(StylePropertyId.UnityFont, oldStyle.inheritedData.Read().unityFont, newStyle.inheritedData.Read().unityFont, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityFontDefinition:
					return element.styleAnimation.Start(StylePropertyId.UnityFontDefinition, oldStyle.inheritedData.Read().unityFontDefinition, newStyle.inheritedData.Read().unityFontDefinition, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityFontStyleAndWeight:
					return element.styleAnimation.StartEnum(StylePropertyId.UnityFontStyleAndWeight, (int)oldStyle.inheritedData.Read().unityFontStyleAndWeight, (int)newStyle.inheritedData.Read().unityFontStyleAndWeight, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityParagraphSpacing:
					return element.styleAnimation.Start(StylePropertyId.UnityParagraphSpacing, oldStyle.inheritedData.Read().unityParagraphSpacing, newStyle.inheritedData.Read().unityParagraphSpacing, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityTextAlign:
					return element.styleAnimation.StartEnum(StylePropertyId.UnityTextAlign, (int)oldStyle.inheritedData.Read().unityTextAlign, (int)newStyle.inheritedData.Read().unityTextAlign, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityTextOutlineColor:
					return element.styleAnimation.Start(StylePropertyId.UnityTextOutlineColor, oldStyle.inheritedData.Read().unityTextOutlineColor, newStyle.inheritedData.Read().unityTextOutlineColor, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityTextOutlineWidth:
					return element.styleAnimation.Start(StylePropertyId.UnityTextOutlineWidth, oldStyle.inheritedData.Read().unityTextOutlineWidth, newStyle.inheritedData.Read().unityTextOutlineWidth, durationMs, delayMs, easingCurve);
				case StylePropertyId.Visibility:
					return element.styleAnimation.StartEnum(StylePropertyId.Visibility, (int)oldStyle.inheritedData.Read().visibility, (int)newStyle.inheritedData.Read().visibility, durationMs, delayMs, easingCurve);
				case StylePropertyId.WhiteSpace:
					return element.styleAnimation.StartEnum(StylePropertyId.WhiteSpace, (int)oldStyle.inheritedData.Read().whiteSpace, (int)newStyle.inheritedData.Read().whiteSpace, durationMs, delayMs, easingCurve);
				case StylePropertyId.WordSpacing:
					return element.styleAnimation.Start(StylePropertyId.WordSpacing, oldStyle.inheritedData.Read().wordSpacing, newStyle.inheritedData.Read().wordSpacing, durationMs, delayMs, easingCurve);
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
						return element.styleAnimation.StartEnum(StylePropertyId.AlignContent, (int)oldStyle.layoutData.Read().alignContent, (int)newStyle.layoutData.Read().alignContent, durationMs, delayMs, easingCurve);
					case StylePropertyId.AlignItems:
						return element.styleAnimation.StartEnum(StylePropertyId.AlignItems, (int)oldStyle.layoutData.Read().alignItems, (int)newStyle.layoutData.Read().alignItems, durationMs, delayMs, easingCurve);
					case StylePropertyId.AlignSelf:
						return element.styleAnimation.StartEnum(StylePropertyId.AlignSelf, (int)oldStyle.layoutData.Read().alignSelf, (int)newStyle.layoutData.Read().alignSelf, durationMs, delayMs, easingCurve);
					case StylePropertyId.BorderBottomWidth:
						return element.styleAnimation.Start(StylePropertyId.BorderBottomWidth, oldStyle.layoutData.Read().borderBottomWidth, newStyle.layoutData.Read().borderBottomWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.BorderLeftWidth:
						return element.styleAnimation.Start(StylePropertyId.BorderLeftWidth, oldStyle.layoutData.Read().borderLeftWidth, newStyle.layoutData.Read().borderLeftWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.BorderRightWidth:
						return element.styleAnimation.Start(StylePropertyId.BorderRightWidth, oldStyle.layoutData.Read().borderRightWidth, newStyle.layoutData.Read().borderRightWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.BorderTopWidth:
						return element.styleAnimation.Start(StylePropertyId.BorderTopWidth, oldStyle.layoutData.Read().borderTopWidth, newStyle.layoutData.Read().borderTopWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.Bottom:
						return element.styleAnimation.Start(StylePropertyId.Bottom, oldStyle.layoutData.Read().bottom, newStyle.layoutData.Read().bottom, durationMs, delayMs, easingCurve);
					case StylePropertyId.Display:
						break;
					case StylePropertyId.FlexBasis:
						return element.styleAnimation.Start(StylePropertyId.FlexBasis, oldStyle.layoutData.Read().flexBasis, newStyle.layoutData.Read().flexBasis, durationMs, delayMs, easingCurve);
					case StylePropertyId.FlexDirection:
						return element.styleAnimation.StartEnum(StylePropertyId.FlexDirection, (int)oldStyle.layoutData.Read().flexDirection, (int)newStyle.layoutData.Read().flexDirection, durationMs, delayMs, easingCurve);
					case StylePropertyId.FlexGrow:
						return element.styleAnimation.Start(StylePropertyId.FlexGrow, oldStyle.layoutData.Read().flexGrow, newStyle.layoutData.Read().flexGrow, durationMs, delayMs, easingCurve);
					case StylePropertyId.FlexShrink:
						return element.styleAnimation.Start(StylePropertyId.FlexShrink, oldStyle.layoutData.Read().flexShrink, newStyle.layoutData.Read().flexShrink, durationMs, delayMs, easingCurve);
					case StylePropertyId.FlexWrap:
						return element.styleAnimation.StartEnum(StylePropertyId.FlexWrap, (int)oldStyle.layoutData.Read().flexWrap, (int)newStyle.layoutData.Read().flexWrap, durationMs, delayMs, easingCurve);
					case StylePropertyId.Height:
						return element.styleAnimation.Start(StylePropertyId.Height, oldStyle.layoutData.Read().height, newStyle.layoutData.Read().height, durationMs, delayMs, easingCurve);
					case StylePropertyId.JustifyContent:
						return element.styleAnimation.StartEnum(StylePropertyId.JustifyContent, (int)oldStyle.layoutData.Read().justifyContent, (int)newStyle.layoutData.Read().justifyContent, durationMs, delayMs, easingCurve);
					case StylePropertyId.Left:
						return element.styleAnimation.Start(StylePropertyId.Left, oldStyle.layoutData.Read().left, newStyle.layoutData.Read().left, durationMs, delayMs, easingCurve);
					case StylePropertyId.MarginBottom:
						return element.styleAnimation.Start(StylePropertyId.MarginBottom, oldStyle.layoutData.Read().marginBottom, newStyle.layoutData.Read().marginBottom, durationMs, delayMs, easingCurve);
					case StylePropertyId.MarginLeft:
						return element.styleAnimation.Start(StylePropertyId.MarginLeft, oldStyle.layoutData.Read().marginLeft, newStyle.layoutData.Read().marginLeft, durationMs, delayMs, easingCurve);
					case StylePropertyId.MarginRight:
						return element.styleAnimation.Start(StylePropertyId.MarginRight, oldStyle.layoutData.Read().marginRight, newStyle.layoutData.Read().marginRight, durationMs, delayMs, easingCurve);
					case StylePropertyId.MarginTop:
						return element.styleAnimation.Start(StylePropertyId.MarginTop, oldStyle.layoutData.Read().marginTop, newStyle.layoutData.Read().marginTop, durationMs, delayMs, easingCurve);
					case StylePropertyId.MaxHeight:
						return element.styleAnimation.Start(StylePropertyId.MaxHeight, oldStyle.layoutData.Read().maxHeight, newStyle.layoutData.Read().maxHeight, durationMs, delayMs, easingCurve);
					case StylePropertyId.MaxWidth:
						return element.styleAnimation.Start(StylePropertyId.MaxWidth, oldStyle.layoutData.Read().maxWidth, newStyle.layoutData.Read().maxWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.MinHeight:
						return element.styleAnimation.Start(StylePropertyId.MinHeight, oldStyle.layoutData.Read().minHeight, newStyle.layoutData.Read().minHeight, durationMs, delayMs, easingCurve);
					case StylePropertyId.MinWidth:
						return element.styleAnimation.Start(StylePropertyId.MinWidth, oldStyle.layoutData.Read().minWidth, newStyle.layoutData.Read().minWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.PaddingBottom:
						return element.styleAnimation.Start(StylePropertyId.PaddingBottom, oldStyle.layoutData.Read().paddingBottom, newStyle.layoutData.Read().paddingBottom, durationMs, delayMs, easingCurve);
					case StylePropertyId.PaddingLeft:
						return element.styleAnimation.Start(StylePropertyId.PaddingLeft, oldStyle.layoutData.Read().paddingLeft, newStyle.layoutData.Read().paddingLeft, durationMs, delayMs, easingCurve);
					case StylePropertyId.PaddingRight:
						return element.styleAnimation.Start(StylePropertyId.PaddingRight, oldStyle.layoutData.Read().paddingRight, newStyle.layoutData.Read().paddingRight, durationMs, delayMs, easingCurve);
					case StylePropertyId.PaddingTop:
						return element.styleAnimation.Start(StylePropertyId.PaddingTop, oldStyle.layoutData.Read().paddingTop, newStyle.layoutData.Read().paddingTop, durationMs, delayMs, easingCurve);
					case StylePropertyId.Position:
						return element.styleAnimation.StartEnum(StylePropertyId.Position, (int)oldStyle.layoutData.Read().position, (int)newStyle.layoutData.Read().position, durationMs, delayMs, easingCurve);
					case StylePropertyId.Right:
						return element.styleAnimation.Start(StylePropertyId.Right, oldStyle.layoutData.Read().right, newStyle.layoutData.Read().right, durationMs, delayMs, easingCurve);
					case StylePropertyId.Top:
						return element.styleAnimation.Start(StylePropertyId.Top, oldStyle.layoutData.Read().top, newStyle.layoutData.Read().top, durationMs, delayMs, easingCurve);
					case StylePropertyId.Width:
						return element.styleAnimation.Start(StylePropertyId.Width, oldStyle.layoutData.Read().width, newStyle.layoutData.Read().width, durationMs, delayMs, easingCurve);
					default:
						switch (id)
						{
						case StylePropertyId.TextOverflow:
							return element.styleAnimation.StartEnum(StylePropertyId.TextOverflow, (int)oldStyle.rareData.Read().textOverflow, (int)newStyle.rareData.Read().textOverflow, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnityBackgroundImageTintColor:
						{
							bool flag3 = element.styleAnimation.Start(StylePropertyId.UnityBackgroundImageTintColor, oldStyle.rareData.Read().unityBackgroundImageTintColor, newStyle.rareData.Read().unityBackgroundImageTintColor, durationMs, delayMs, easingCurve);
							bool flag4 = flag3 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
							if (flag4)
							{
								element.usageHints |= UsageHints.DynamicColor;
							}
							return flag3;
						}
						case StylePropertyId.UnityOverflowClipBox:
							return element.styleAnimation.StartEnum(StylePropertyId.UnityOverflowClipBox, (int)oldStyle.rareData.Read().unityOverflowClipBox, (int)newStyle.rareData.Read().unityOverflowClipBox, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceBottom:
							return element.styleAnimation.Start(StylePropertyId.UnitySliceBottom, oldStyle.rareData.Read().unitySliceBottom, newStyle.rareData.Read().unitySliceBottom, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceLeft:
							return element.styleAnimation.Start(StylePropertyId.UnitySliceLeft, oldStyle.rareData.Read().unitySliceLeft, newStyle.rareData.Read().unitySliceLeft, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceRight:
							return element.styleAnimation.Start(StylePropertyId.UnitySliceRight, oldStyle.rareData.Read().unitySliceRight, newStyle.rareData.Read().unitySliceRight, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceScale:
							return element.styleAnimation.Start(StylePropertyId.UnitySliceScale, oldStyle.rareData.Read().unitySliceScale, newStyle.rareData.Read().unitySliceScale, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceTop:
							return element.styleAnimation.Start(StylePropertyId.UnitySliceTop, oldStyle.rareData.Read().unitySliceTop, newStyle.rareData.Read().unitySliceTop, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceType:
							return element.styleAnimation.StartEnum(StylePropertyId.UnitySliceType, (int)oldStyle.rareData.Read().unitySliceType, (int)newStyle.rareData.Read().unitySliceType, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnityTextOverflowPosition:
							return element.styleAnimation.StartEnum(StylePropertyId.UnityTextOverflowPosition, (int)oldStyle.rareData.Read().unityTextOverflowPosition, (int)newStyle.rareData.Read().unityTextOverflowPosition, durationMs, delayMs, easingCurve);
						}
						break;
					}
					break;
				}
			}
			else
			{
				switch (id)
				{
				case StylePropertyId.All:
					return ComputedStyle.StartAnimationAllProperty(element, ref oldStyle, ref newStyle, durationMs, delayMs, easingCurve);
				case StylePropertyId.BackgroundPosition:
				{
					bool flag5 = false;
					flag5 |= element.styleAnimation.Start(StylePropertyId.BackgroundPositionX, oldStyle.visualData.Read().backgroundPositionX, newStyle.visualData.Read().backgroundPositionX, durationMs, delayMs, easingCurve);
					return flag5 | element.styleAnimation.Start(StylePropertyId.BackgroundPositionY, oldStyle.visualData.Read().backgroundPositionY, newStyle.visualData.Read().backgroundPositionY, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.BorderColor:
				{
					bool flag6 = false;
					flag6 |= element.styleAnimation.Start(StylePropertyId.BorderTopColor, oldStyle.visualData.Read().borderTopColor, newStyle.visualData.Read().borderTopColor, durationMs, delayMs, easingCurve);
					flag6 |= element.styleAnimation.Start(StylePropertyId.BorderRightColor, oldStyle.visualData.Read().borderRightColor, newStyle.visualData.Read().borderRightColor, durationMs, delayMs, easingCurve);
					flag6 |= element.styleAnimation.Start(StylePropertyId.BorderBottomColor, oldStyle.visualData.Read().borderBottomColor, newStyle.visualData.Read().borderBottomColor, durationMs, delayMs, easingCurve);
					flag6 |= element.styleAnimation.Start(StylePropertyId.BorderLeftColor, oldStyle.visualData.Read().borderLeftColor, newStyle.visualData.Read().borderLeftColor, durationMs, delayMs, easingCurve);
					bool flag7 = flag6 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
					if (flag7)
					{
						element.usageHints |= UsageHints.DynamicColor;
					}
					return flag6;
				}
				case StylePropertyId.BorderRadius:
				{
					bool flag8 = false;
					flag8 |= element.styleAnimation.Start(StylePropertyId.BorderTopLeftRadius, oldStyle.visualData.Read().borderTopLeftRadius, newStyle.visualData.Read().borderTopLeftRadius, durationMs, delayMs, easingCurve);
					flag8 |= element.styleAnimation.Start(StylePropertyId.BorderTopRightRadius, oldStyle.visualData.Read().borderTopRightRadius, newStyle.visualData.Read().borderTopRightRadius, durationMs, delayMs, easingCurve);
					flag8 |= element.styleAnimation.Start(StylePropertyId.BorderBottomRightRadius, oldStyle.visualData.Read().borderBottomRightRadius, newStyle.visualData.Read().borderBottomRightRadius, durationMs, delayMs, easingCurve);
					return flag8 | element.styleAnimation.Start(StylePropertyId.BorderBottomLeftRadius, oldStyle.visualData.Read().borderBottomLeftRadius, newStyle.visualData.Read().borderBottomLeftRadius, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.BorderWidth:
				{
					bool flag9 = false;
					flag9 |= element.styleAnimation.Start(StylePropertyId.BorderTopWidth, oldStyle.layoutData.Read().borderTopWidth, newStyle.layoutData.Read().borderTopWidth, durationMs, delayMs, easingCurve);
					flag9 |= element.styleAnimation.Start(StylePropertyId.BorderRightWidth, oldStyle.layoutData.Read().borderRightWidth, newStyle.layoutData.Read().borderRightWidth, durationMs, delayMs, easingCurve);
					flag9 |= element.styleAnimation.Start(StylePropertyId.BorderBottomWidth, oldStyle.layoutData.Read().borderBottomWidth, newStyle.layoutData.Read().borderBottomWidth, durationMs, delayMs, easingCurve);
					return flag9 | element.styleAnimation.Start(StylePropertyId.BorderLeftWidth, oldStyle.layoutData.Read().borderLeftWidth, newStyle.layoutData.Read().borderLeftWidth, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Flex:
				{
					bool flag10 = false;
					flag10 |= element.styleAnimation.Start(StylePropertyId.FlexGrow, oldStyle.layoutData.Read().flexGrow, newStyle.layoutData.Read().flexGrow, durationMs, delayMs, easingCurve);
					flag10 |= element.styleAnimation.Start(StylePropertyId.FlexShrink, oldStyle.layoutData.Read().flexShrink, newStyle.layoutData.Read().flexShrink, durationMs, delayMs, easingCurve);
					return flag10 | element.styleAnimation.Start(StylePropertyId.FlexBasis, oldStyle.layoutData.Read().flexBasis, newStyle.layoutData.Read().flexBasis, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Margin:
				{
					bool flag11 = false;
					flag11 |= element.styleAnimation.Start(StylePropertyId.MarginTop, oldStyle.layoutData.Read().marginTop, newStyle.layoutData.Read().marginTop, durationMs, delayMs, easingCurve);
					flag11 |= element.styleAnimation.Start(StylePropertyId.MarginRight, oldStyle.layoutData.Read().marginRight, newStyle.layoutData.Read().marginRight, durationMs, delayMs, easingCurve);
					flag11 |= element.styleAnimation.Start(StylePropertyId.MarginBottom, oldStyle.layoutData.Read().marginBottom, newStyle.layoutData.Read().marginBottom, durationMs, delayMs, easingCurve);
					return flag11 | element.styleAnimation.Start(StylePropertyId.MarginLeft, oldStyle.layoutData.Read().marginLeft, newStyle.layoutData.Read().marginLeft, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Padding:
				{
					bool flag12 = false;
					flag12 |= element.styleAnimation.Start(StylePropertyId.PaddingTop, oldStyle.layoutData.Read().paddingTop, newStyle.layoutData.Read().paddingTop, durationMs, delayMs, easingCurve);
					flag12 |= element.styleAnimation.Start(StylePropertyId.PaddingRight, oldStyle.layoutData.Read().paddingRight, newStyle.layoutData.Read().paddingRight, durationMs, delayMs, easingCurve);
					flag12 |= element.styleAnimation.Start(StylePropertyId.PaddingBottom, oldStyle.layoutData.Read().paddingBottom, newStyle.layoutData.Read().paddingBottom, durationMs, delayMs, easingCurve);
					return flag12 | element.styleAnimation.Start(StylePropertyId.PaddingLeft, oldStyle.layoutData.Read().paddingLeft, newStyle.layoutData.Read().paddingLeft, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Transition:
					break;
				case StylePropertyId.UnityBackgroundScaleMode:
				{
					bool flag13 = false;
					flag13 |= element.styleAnimation.Start(StylePropertyId.BackgroundPositionX, oldStyle.visualData.Read().backgroundPositionX, newStyle.visualData.Read().backgroundPositionX, durationMs, delayMs, easingCurve);
					flag13 |= element.styleAnimation.Start(StylePropertyId.BackgroundPositionY, oldStyle.visualData.Read().backgroundPositionY, newStyle.visualData.Read().backgroundPositionY, durationMs, delayMs, easingCurve);
					flag13 |= element.styleAnimation.Start(StylePropertyId.BackgroundRepeat, oldStyle.visualData.Read().backgroundRepeat, newStyle.visualData.Read().backgroundRepeat, durationMs, delayMs, easingCurve);
					return flag13 | element.styleAnimation.Start(StylePropertyId.BackgroundSize, oldStyle.visualData.Read().backgroundSize, newStyle.visualData.Read().backgroundSize, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityTextOutline:
				{
					bool flag14 = false;
					flag14 |= element.styleAnimation.Start(StylePropertyId.UnityTextOutlineColor, oldStyle.inheritedData.Read().unityTextOutlineColor, newStyle.inheritedData.Read().unityTextOutlineColor, durationMs, delayMs, easingCurve);
					return flag14 | element.styleAnimation.Start(StylePropertyId.UnityTextOutlineWidth, oldStyle.inheritedData.Read().unityTextOutlineWidth, newStyle.inheritedData.Read().unityTextOutlineWidth, durationMs, delayMs, easingCurve);
				}
				default:
					switch (id)
					{
					case StylePropertyId.Rotate:
					{
						bool flag15 = element.styleAnimation.Start(StylePropertyId.Rotate, oldStyle.transformData.Read().rotate, newStyle.transformData.Read().rotate, durationMs, delayMs, easingCurve);
						bool flag16 = flag15 && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
						if (flag16)
						{
							element.usageHints |= UsageHints.DynamicTransform;
						}
						return flag15;
					}
					case StylePropertyId.Scale:
					{
						bool flag17 = element.styleAnimation.Start(StylePropertyId.Scale, oldStyle.transformData.Read().scale, newStyle.transformData.Read().scale, durationMs, delayMs, easingCurve);
						bool flag18 = flag17 && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
						if (flag18)
						{
							element.usageHints |= UsageHints.DynamicTransform;
						}
						return flag17;
					}
					case StylePropertyId.TransformOrigin:
					{
						bool flag19 = element.styleAnimation.Start(StylePropertyId.TransformOrigin, oldStyle.transformData.Read().transformOrigin, newStyle.transformData.Read().transformOrigin, durationMs, delayMs, easingCurve);
						bool flag20 = flag19 && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
						if (flag20)
						{
							element.usageHints |= UsageHints.DynamicTransform;
						}
						return flag19;
					}
					case StylePropertyId.Translate:
					{
						bool flag21 = element.styleAnimation.Start(StylePropertyId.Translate, oldStyle.transformData.Read().translate, newStyle.transformData.Read().translate, durationMs, delayMs, easingCurve);
						bool flag22 = flag21 && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
						if (flag22)
						{
							element.usageHints |= UsageHints.DynamicTransform;
						}
						return flag21;
					}
					default:
						switch (id)
						{
						case StylePropertyId.BackgroundColor:
						{
							bool flag23 = element.styleAnimation.Start(StylePropertyId.BackgroundColor, oldStyle.visualData.Read().backgroundColor, newStyle.visualData.Read().backgroundColor, durationMs, delayMs, easingCurve);
							bool flag24 = flag23 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
							if (flag24)
							{
								element.usageHints |= UsageHints.DynamicColor;
							}
							return flag23;
						}
						case StylePropertyId.BackgroundImage:
							return element.styleAnimation.Start(StylePropertyId.BackgroundImage, oldStyle.visualData.Read().backgroundImage, newStyle.visualData.Read().backgroundImage, durationMs, delayMs, easingCurve);
						case StylePropertyId.BackgroundPositionX:
							return element.styleAnimation.Start(StylePropertyId.BackgroundPositionX, oldStyle.visualData.Read().backgroundPositionX, newStyle.visualData.Read().backgroundPositionX, durationMs, delayMs, easingCurve);
						case StylePropertyId.BackgroundPositionY:
							return element.styleAnimation.Start(StylePropertyId.BackgroundPositionY, oldStyle.visualData.Read().backgroundPositionY, newStyle.visualData.Read().backgroundPositionY, durationMs, delayMs, easingCurve);
						case StylePropertyId.BackgroundRepeat:
							return element.styleAnimation.Start(StylePropertyId.BackgroundRepeat, oldStyle.visualData.Read().backgroundRepeat, newStyle.visualData.Read().backgroundRepeat, durationMs, delayMs, easingCurve);
						case StylePropertyId.BackgroundSize:
							return element.styleAnimation.Start(StylePropertyId.BackgroundSize, oldStyle.visualData.Read().backgroundSize, newStyle.visualData.Read().backgroundSize, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderBottomColor:
						{
							bool flag25 = element.styleAnimation.Start(StylePropertyId.BorderBottomColor, oldStyle.visualData.Read().borderBottomColor, newStyle.visualData.Read().borderBottomColor, durationMs, delayMs, easingCurve);
							bool flag26 = flag25 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
							if (flag26)
							{
								element.usageHints |= UsageHints.DynamicColor;
							}
							return flag25;
						}
						case StylePropertyId.BorderBottomLeftRadius:
							return element.styleAnimation.Start(StylePropertyId.BorderBottomLeftRadius, oldStyle.visualData.Read().borderBottomLeftRadius, newStyle.visualData.Read().borderBottomLeftRadius, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderBottomRightRadius:
							return element.styleAnimation.Start(StylePropertyId.BorderBottomRightRadius, oldStyle.visualData.Read().borderBottomRightRadius, newStyle.visualData.Read().borderBottomRightRadius, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderLeftColor:
						{
							bool flag27 = element.styleAnimation.Start(StylePropertyId.BorderLeftColor, oldStyle.visualData.Read().borderLeftColor, newStyle.visualData.Read().borderLeftColor, durationMs, delayMs, easingCurve);
							bool flag28 = flag27 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
							if (flag28)
							{
								element.usageHints |= UsageHints.DynamicColor;
							}
							return flag27;
						}
						case StylePropertyId.BorderRightColor:
						{
							bool flag29 = element.styleAnimation.Start(StylePropertyId.BorderRightColor, oldStyle.visualData.Read().borderRightColor, newStyle.visualData.Read().borderRightColor, durationMs, delayMs, easingCurve);
							bool flag30 = flag29 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
							if (flag30)
							{
								element.usageHints |= UsageHints.DynamicColor;
							}
							return flag29;
						}
						case StylePropertyId.BorderTopColor:
						{
							bool flag31 = element.styleAnimation.Start(StylePropertyId.BorderTopColor, oldStyle.visualData.Read().borderTopColor, newStyle.visualData.Read().borderTopColor, durationMs, delayMs, easingCurve);
							bool flag32 = flag31 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
							if (flag32)
							{
								element.usageHints |= UsageHints.DynamicColor;
							}
							return flag31;
						}
						case StylePropertyId.BorderTopLeftRadius:
							return element.styleAnimation.Start(StylePropertyId.BorderTopLeftRadius, oldStyle.visualData.Read().borderTopLeftRadius, newStyle.visualData.Read().borderTopLeftRadius, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderTopRightRadius:
							return element.styleAnimation.Start(StylePropertyId.BorderTopRightRadius, oldStyle.visualData.Read().borderTopRightRadius, newStyle.visualData.Read().borderTopRightRadius, durationMs, delayMs, easingCurve);
						case StylePropertyId.Opacity:
							return element.styleAnimation.Start(StylePropertyId.Opacity, oldStyle.visualData.Read().opacity, newStyle.visualData.Read().opacity, durationMs, delayMs, easingCurve);
						case StylePropertyId.Overflow:
							return element.styleAnimation.StartEnum(StylePropertyId.Overflow, (int)oldStyle.visualData.Read().overflow, (int)newStyle.visualData.Read().overflow, durationMs, delayMs, easingCurve);
						}
						break;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00064840 File Offset: 0x00062A40
		public static bool StartAnimationAllProperty(VisualElement element, ref ComputedStyle oldStyle, ref ComputedStyle newStyle, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			bool flag = false;
			UsageHints usageHints = UsageHints.None;
			bool hasRunningAnimations = element.hasRunningAnimations;
			bool flag2 = hasRunningAnimations || !oldStyle.inheritedData.Equals(newStyle.inheritedData);
			if (flag2)
			{
				readonly ref InheritedData ptr = ref oldStyle.inheritedData.Read();
				readonly ref InheritedData ptr2 = ref newStyle.inheritedData.Read();
				bool flag3 = hasRunningAnimations || ptr.color != ptr2.color;
				if (flag3)
				{
					bool flag4 = element.styleAnimation.Start(StylePropertyId.Color, ptr.color, ptr2.color, durationMs, delayMs, easingCurve);
					bool flag5 = flag4;
					if (flag5)
					{
						usageHints |= UsageHints.DynamicColor;
					}
					flag = flag || flag4;
				}
				bool flag6 = hasRunningAnimations || ptr.fontSize != ptr2.fontSize;
				if (flag6)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.FontSize, ptr.fontSize, ptr2.fontSize, durationMs, delayMs, easingCurve);
				}
				bool flag7 = hasRunningAnimations || ptr.letterSpacing != ptr2.letterSpacing;
				if (flag7)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.LetterSpacing, ptr.letterSpacing, ptr2.letterSpacing, durationMs, delayMs, easingCurve);
				}
				bool flag8 = hasRunningAnimations || ptr.textShadow != ptr2.textShadow;
				if (flag8)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.TextShadow, ptr.textShadow, ptr2.textShadow, durationMs, delayMs, easingCurve);
				}
				bool flag9 = hasRunningAnimations || ptr.unityFont != ptr2.unityFont;
				if (flag9)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnityFont, ptr.unityFont, ptr2.unityFont, durationMs, delayMs, easingCurve);
				}
				bool flag10 = hasRunningAnimations || ptr.unityFontDefinition != ptr2.unityFontDefinition;
				if (flag10)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnityFontDefinition, ptr.unityFontDefinition, ptr2.unityFontDefinition, durationMs, delayMs, easingCurve);
				}
				bool flag11 = hasRunningAnimations || ptr.unityFontStyleAndWeight != ptr2.unityFontStyleAndWeight;
				if (flag11)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.UnityFontStyleAndWeight, (int)ptr.unityFontStyleAndWeight, (int)ptr2.unityFontStyleAndWeight, durationMs, delayMs, easingCurve);
				}
				bool flag12 = hasRunningAnimations || ptr.unityParagraphSpacing != ptr2.unityParagraphSpacing;
				if (flag12)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnityParagraphSpacing, ptr.unityParagraphSpacing, ptr2.unityParagraphSpacing, durationMs, delayMs, easingCurve);
				}
				bool flag13 = hasRunningAnimations || ptr.unityTextAlign != ptr2.unityTextAlign;
				if (flag13)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.UnityTextAlign, (int)ptr.unityTextAlign, (int)ptr2.unityTextAlign, durationMs, delayMs, easingCurve);
				}
				bool flag14 = hasRunningAnimations || ptr.unityTextOutlineColor != ptr2.unityTextOutlineColor;
				if (flag14)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnityTextOutlineColor, ptr.unityTextOutlineColor, ptr2.unityTextOutlineColor, durationMs, delayMs, easingCurve);
				}
				bool flag15 = hasRunningAnimations || ptr.unityTextOutlineWidth != ptr2.unityTextOutlineWidth;
				if (flag15)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnityTextOutlineWidth, ptr.unityTextOutlineWidth, ptr2.unityTextOutlineWidth, durationMs, delayMs, easingCurve);
				}
				bool flag16 = hasRunningAnimations || ptr.visibility != ptr2.visibility;
				if (flag16)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.Visibility, (int)ptr.visibility, (int)ptr2.visibility, durationMs, delayMs, easingCurve);
				}
				bool flag17 = hasRunningAnimations || ptr.whiteSpace != ptr2.whiteSpace;
				if (flag17)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.WhiteSpace, (int)ptr.whiteSpace, (int)ptr2.whiteSpace, durationMs, delayMs, easingCurve);
				}
				bool flag18 = hasRunningAnimations || ptr.wordSpacing != ptr2.wordSpacing;
				if (flag18)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.WordSpacing, ptr.wordSpacing, ptr2.wordSpacing, durationMs, delayMs, easingCurve);
				}
			}
			bool flag19 = hasRunningAnimations || !oldStyle.layoutData.Equals(newStyle.layoutData);
			if (flag19)
			{
				readonly ref LayoutData ptr3 = ref oldStyle.layoutData.Read();
				readonly ref LayoutData ptr4 = ref newStyle.layoutData.Read();
				bool flag20 = hasRunningAnimations || ptr3.alignContent != ptr4.alignContent;
				if (flag20)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.AlignContent, (int)ptr3.alignContent, (int)ptr4.alignContent, durationMs, delayMs, easingCurve);
				}
				bool flag21 = hasRunningAnimations || ptr3.alignItems != ptr4.alignItems;
				if (flag21)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.AlignItems, (int)ptr3.alignItems, (int)ptr4.alignItems, durationMs, delayMs, easingCurve);
				}
				bool flag22 = hasRunningAnimations || ptr3.alignSelf != ptr4.alignSelf;
				if (flag22)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.AlignSelf, (int)ptr3.alignSelf, (int)ptr4.alignSelf, durationMs, delayMs, easingCurve);
				}
				bool flag23 = hasRunningAnimations || ptr3.borderBottomWidth != ptr4.borderBottomWidth;
				if (flag23)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderBottomWidth, ptr3.borderBottomWidth, ptr4.borderBottomWidth, durationMs, delayMs, easingCurve);
				}
				bool flag24 = hasRunningAnimations || ptr3.borderLeftWidth != ptr4.borderLeftWidth;
				if (flag24)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderLeftWidth, ptr3.borderLeftWidth, ptr4.borderLeftWidth, durationMs, delayMs, easingCurve);
				}
				bool flag25 = hasRunningAnimations || ptr3.borderRightWidth != ptr4.borderRightWidth;
				if (flag25)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderRightWidth, ptr3.borderRightWidth, ptr4.borderRightWidth, durationMs, delayMs, easingCurve);
				}
				bool flag26 = hasRunningAnimations || ptr3.borderTopWidth != ptr4.borderTopWidth;
				if (flag26)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderTopWidth, ptr3.borderTopWidth, ptr4.borderTopWidth, durationMs, delayMs, easingCurve);
				}
				bool flag27 = hasRunningAnimations || ptr3.bottom != ptr4.bottom;
				if (flag27)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Bottom, ptr3.bottom, ptr4.bottom, durationMs, delayMs, easingCurve);
				}
				bool flag28 = hasRunningAnimations || ptr3.flexBasis != ptr4.flexBasis;
				if (flag28)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.FlexBasis, ptr3.flexBasis, ptr4.flexBasis, durationMs, delayMs, easingCurve);
				}
				bool flag29 = hasRunningAnimations || ptr3.flexDirection != ptr4.flexDirection;
				if (flag29)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.FlexDirection, (int)ptr3.flexDirection, (int)ptr4.flexDirection, durationMs, delayMs, easingCurve);
				}
				bool flag30 = hasRunningAnimations || ptr3.flexGrow != ptr4.flexGrow;
				if (flag30)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.FlexGrow, ptr3.flexGrow, ptr4.flexGrow, durationMs, delayMs, easingCurve);
				}
				bool flag31 = hasRunningAnimations || ptr3.flexShrink != ptr4.flexShrink;
				if (flag31)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.FlexShrink, ptr3.flexShrink, ptr4.flexShrink, durationMs, delayMs, easingCurve);
				}
				bool flag32 = hasRunningAnimations || ptr3.flexWrap != ptr4.flexWrap;
				if (flag32)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.FlexWrap, (int)ptr3.flexWrap, (int)ptr4.flexWrap, durationMs, delayMs, easingCurve);
				}
				bool flag33 = hasRunningAnimations || ptr3.height != ptr4.height;
				if (flag33)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Height, ptr3.height, ptr4.height, durationMs, delayMs, easingCurve);
				}
				bool flag34 = hasRunningAnimations || ptr3.justifyContent != ptr4.justifyContent;
				if (flag34)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.JustifyContent, (int)ptr3.justifyContent, (int)ptr4.justifyContent, durationMs, delayMs, easingCurve);
				}
				bool flag35 = hasRunningAnimations || ptr3.left != ptr4.left;
				if (flag35)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Left, ptr3.left, ptr4.left, durationMs, delayMs, easingCurve);
				}
				bool flag36 = hasRunningAnimations || ptr3.marginBottom != ptr4.marginBottom;
				if (flag36)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MarginBottom, ptr3.marginBottom, ptr4.marginBottom, durationMs, delayMs, easingCurve);
				}
				bool flag37 = hasRunningAnimations || ptr3.marginLeft != ptr4.marginLeft;
				if (flag37)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MarginLeft, ptr3.marginLeft, ptr4.marginLeft, durationMs, delayMs, easingCurve);
				}
				bool flag38 = hasRunningAnimations || ptr3.marginRight != ptr4.marginRight;
				if (flag38)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MarginRight, ptr3.marginRight, ptr4.marginRight, durationMs, delayMs, easingCurve);
				}
				bool flag39 = hasRunningAnimations || ptr3.marginTop != ptr4.marginTop;
				if (flag39)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MarginTop, ptr3.marginTop, ptr4.marginTop, durationMs, delayMs, easingCurve);
				}
				bool flag40 = hasRunningAnimations || ptr3.maxHeight != ptr4.maxHeight;
				if (flag40)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MaxHeight, ptr3.maxHeight, ptr4.maxHeight, durationMs, delayMs, easingCurve);
				}
				bool flag41 = hasRunningAnimations || ptr3.maxWidth != ptr4.maxWidth;
				if (flag41)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MaxWidth, ptr3.maxWidth, ptr4.maxWidth, durationMs, delayMs, easingCurve);
				}
				bool flag42 = hasRunningAnimations || ptr3.minHeight != ptr4.minHeight;
				if (flag42)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MinHeight, ptr3.minHeight, ptr4.minHeight, durationMs, delayMs, easingCurve);
				}
				bool flag43 = hasRunningAnimations || ptr3.minWidth != ptr4.minWidth;
				if (flag43)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MinWidth, ptr3.minWidth, ptr4.minWidth, durationMs, delayMs, easingCurve);
				}
				bool flag44 = hasRunningAnimations || ptr3.paddingBottom != ptr4.paddingBottom;
				if (flag44)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.PaddingBottom, ptr3.paddingBottom, ptr4.paddingBottom, durationMs, delayMs, easingCurve);
				}
				bool flag45 = hasRunningAnimations || ptr3.paddingLeft != ptr4.paddingLeft;
				if (flag45)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.PaddingLeft, ptr3.paddingLeft, ptr4.paddingLeft, durationMs, delayMs, easingCurve);
				}
				bool flag46 = hasRunningAnimations || ptr3.paddingRight != ptr4.paddingRight;
				if (flag46)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.PaddingRight, ptr3.paddingRight, ptr4.paddingRight, durationMs, delayMs, easingCurve);
				}
				bool flag47 = hasRunningAnimations || ptr3.paddingTop != ptr4.paddingTop;
				if (flag47)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.PaddingTop, ptr3.paddingTop, ptr4.paddingTop, durationMs, delayMs, easingCurve);
				}
				bool flag48 = hasRunningAnimations || ptr3.position != ptr4.position;
				if (flag48)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.Position, (int)ptr3.position, (int)ptr4.position, durationMs, delayMs, easingCurve);
				}
				bool flag49 = hasRunningAnimations || ptr3.right != ptr4.right;
				if (flag49)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Right, ptr3.right, ptr4.right, durationMs, delayMs, easingCurve);
				}
				bool flag50 = hasRunningAnimations || ptr3.top != ptr4.top;
				if (flag50)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Top, ptr3.top, ptr4.top, durationMs, delayMs, easingCurve);
				}
				bool flag51 = hasRunningAnimations || ptr3.width != ptr4.width;
				if (flag51)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Width, ptr3.width, ptr4.width, durationMs, delayMs, easingCurve);
				}
			}
			bool flag52 = hasRunningAnimations || !oldStyle.rareData.Equals(newStyle.rareData);
			if (flag52)
			{
				readonly ref RareData ptr5 = ref oldStyle.rareData.Read();
				readonly ref RareData ptr6 = ref newStyle.rareData.Read();
				bool flag53 = hasRunningAnimations || ptr5.textOverflow != ptr6.textOverflow;
				if (flag53)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.TextOverflow, (int)ptr5.textOverflow, (int)ptr6.textOverflow, durationMs, delayMs, easingCurve);
				}
				bool flag54 = hasRunningAnimations || ptr5.unityBackgroundImageTintColor != ptr6.unityBackgroundImageTintColor;
				if (flag54)
				{
					bool flag55 = element.styleAnimation.Start(StylePropertyId.UnityBackgroundImageTintColor, ptr5.unityBackgroundImageTintColor, ptr6.unityBackgroundImageTintColor, durationMs, delayMs, easingCurve);
					bool flag56 = flag55;
					if (flag56)
					{
						usageHints |= UsageHints.DynamicColor;
					}
					flag = flag || flag55;
				}
				bool flag57 = hasRunningAnimations || ptr5.unityOverflowClipBox != ptr6.unityOverflowClipBox;
				if (flag57)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.UnityOverflowClipBox, (int)ptr5.unityOverflowClipBox, (int)ptr6.unityOverflowClipBox, durationMs, delayMs, easingCurve);
				}
				bool flag58 = hasRunningAnimations || ptr5.unitySliceBottom != ptr6.unitySliceBottom;
				if (flag58)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnitySliceBottom, ptr5.unitySliceBottom, ptr6.unitySliceBottom, durationMs, delayMs, easingCurve);
				}
				bool flag59 = hasRunningAnimations || ptr5.unitySliceLeft != ptr6.unitySliceLeft;
				if (flag59)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnitySliceLeft, ptr5.unitySliceLeft, ptr6.unitySliceLeft, durationMs, delayMs, easingCurve);
				}
				bool flag60 = hasRunningAnimations || ptr5.unitySliceRight != ptr6.unitySliceRight;
				if (flag60)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnitySliceRight, ptr5.unitySliceRight, ptr6.unitySliceRight, durationMs, delayMs, easingCurve);
				}
				bool flag61 = hasRunningAnimations || ptr5.unitySliceScale != ptr6.unitySliceScale;
				if (flag61)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnitySliceScale, ptr5.unitySliceScale, ptr6.unitySliceScale, durationMs, delayMs, easingCurve);
				}
				bool flag62 = hasRunningAnimations || ptr5.unitySliceTop != ptr6.unitySliceTop;
				if (flag62)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnitySliceTop, ptr5.unitySliceTop, ptr6.unitySliceTop, durationMs, delayMs, easingCurve);
				}
				bool flag63 = hasRunningAnimations || ptr5.unitySliceType != ptr6.unitySliceType;
				if (flag63)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.UnitySliceType, (int)ptr5.unitySliceType, (int)ptr6.unitySliceType, durationMs, delayMs, easingCurve);
				}
				bool flag64 = hasRunningAnimations || ptr5.unityTextOverflowPosition != ptr6.unityTextOverflowPosition;
				if (flag64)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.UnityTextOverflowPosition, (int)ptr5.unityTextOverflowPosition, (int)ptr6.unityTextOverflowPosition, durationMs, delayMs, easingCurve);
				}
			}
			bool flag65 = hasRunningAnimations || !oldStyle.transformData.Equals(newStyle.transformData);
			if (flag65)
			{
				readonly ref TransformData ptr7 = ref oldStyle.transformData.Read();
				readonly ref TransformData ptr8 = ref newStyle.transformData.Read();
				bool flag66 = hasRunningAnimations || ptr7.rotate != ptr8.rotate;
				if (flag66)
				{
					bool flag67 = element.styleAnimation.Start(StylePropertyId.Rotate, ptr7.rotate, ptr8.rotate, durationMs, delayMs, easingCurve);
					bool flag68 = flag67;
					if (flag68)
					{
						usageHints |= UsageHints.DynamicTransform;
					}
					flag = flag || flag67;
				}
				bool flag69 = hasRunningAnimations || ptr7.scale != ptr8.scale;
				if (flag69)
				{
					bool flag70 = element.styleAnimation.Start(StylePropertyId.Scale, ptr7.scale, ptr8.scale, durationMs, delayMs, easingCurve);
					bool flag71 = flag70;
					if (flag71)
					{
						usageHints |= UsageHints.DynamicTransform;
					}
					flag = flag || flag70;
				}
				bool flag72 = hasRunningAnimations || ptr7.transformOrigin != ptr8.transformOrigin;
				if (flag72)
				{
					bool flag73 = element.styleAnimation.Start(StylePropertyId.TransformOrigin, ptr7.transformOrigin, ptr8.transformOrigin, durationMs, delayMs, easingCurve);
					bool flag74 = flag73;
					if (flag74)
					{
						usageHints |= UsageHints.DynamicTransform;
					}
					flag = flag || flag73;
				}
				bool flag75 = hasRunningAnimations || ptr7.translate != ptr8.translate;
				if (flag75)
				{
					bool flag76 = element.styleAnimation.Start(StylePropertyId.Translate, ptr7.translate, ptr8.translate, durationMs, delayMs, easingCurve);
					bool flag77 = flag76;
					if (flag77)
					{
						usageHints |= UsageHints.DynamicTransform;
					}
					flag = flag || flag76;
				}
			}
			bool flag78 = hasRunningAnimations || !oldStyle.visualData.Equals(newStyle.visualData);
			if (flag78)
			{
				readonly ref VisualData ptr9 = ref oldStyle.visualData.Read();
				readonly ref VisualData ptr10 = ref newStyle.visualData.Read();
				bool flag79 = hasRunningAnimations || ptr9.backgroundColor != ptr10.backgroundColor;
				if (flag79)
				{
					bool flag80 = element.styleAnimation.Start(StylePropertyId.BackgroundColor, ptr9.backgroundColor, ptr10.backgroundColor, durationMs, delayMs, easingCurve);
					bool flag81 = flag80;
					if (flag81)
					{
						usageHints |= UsageHints.DynamicColor;
					}
					flag = flag || flag80;
				}
				bool flag82 = hasRunningAnimations || ptr9.backgroundImage != ptr10.backgroundImage;
				if (flag82)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BackgroundImage, ptr9.backgroundImage, ptr10.backgroundImage, durationMs, delayMs, easingCurve);
				}
				bool flag83 = hasRunningAnimations || ptr9.backgroundPositionX != ptr10.backgroundPositionX;
				if (flag83)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BackgroundPositionX, ptr9.backgroundPositionX, ptr10.backgroundPositionX, durationMs, delayMs, easingCurve);
				}
				bool flag84 = hasRunningAnimations || ptr9.backgroundPositionY != ptr10.backgroundPositionY;
				if (flag84)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BackgroundPositionY, ptr9.backgroundPositionY, ptr10.backgroundPositionY, durationMs, delayMs, easingCurve);
				}
				bool flag85 = hasRunningAnimations || ptr9.backgroundRepeat != ptr10.backgroundRepeat;
				if (flag85)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BackgroundRepeat, ptr9.backgroundRepeat, ptr10.backgroundRepeat, durationMs, delayMs, easingCurve);
				}
				bool flag86 = hasRunningAnimations || ptr9.backgroundSize != ptr10.backgroundSize;
				if (flag86)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BackgroundSize, ptr9.backgroundSize, ptr10.backgroundSize, durationMs, delayMs, easingCurve);
				}
				bool flag87 = hasRunningAnimations || ptr9.borderBottomColor != ptr10.borderBottomColor;
				if (flag87)
				{
					bool flag88 = element.styleAnimation.Start(StylePropertyId.BorderBottomColor, ptr9.borderBottomColor, ptr10.borderBottomColor, durationMs, delayMs, easingCurve);
					bool flag89 = flag88;
					if (flag89)
					{
						usageHints |= UsageHints.DynamicColor;
					}
					flag = flag || flag88;
				}
				bool flag90 = hasRunningAnimations || ptr9.borderBottomLeftRadius != ptr10.borderBottomLeftRadius;
				if (flag90)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderBottomLeftRadius, ptr9.borderBottomLeftRadius, ptr10.borderBottomLeftRadius, durationMs, delayMs, easingCurve);
				}
				bool flag91 = hasRunningAnimations || ptr9.borderBottomRightRadius != ptr10.borderBottomRightRadius;
				if (flag91)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderBottomRightRadius, ptr9.borderBottomRightRadius, ptr10.borderBottomRightRadius, durationMs, delayMs, easingCurve);
				}
				bool flag92 = hasRunningAnimations || ptr9.borderLeftColor != ptr10.borderLeftColor;
				if (flag92)
				{
					bool flag93 = element.styleAnimation.Start(StylePropertyId.BorderLeftColor, ptr9.borderLeftColor, ptr10.borderLeftColor, durationMs, delayMs, easingCurve);
					bool flag94 = flag93;
					if (flag94)
					{
						usageHints |= UsageHints.DynamicColor;
					}
					flag = flag || flag93;
				}
				bool flag95 = hasRunningAnimations || ptr9.borderRightColor != ptr10.borderRightColor;
				if (flag95)
				{
					bool flag96 = element.styleAnimation.Start(StylePropertyId.BorderRightColor, ptr9.borderRightColor, ptr10.borderRightColor, durationMs, delayMs, easingCurve);
					bool flag97 = flag96;
					if (flag97)
					{
						usageHints |= UsageHints.DynamicColor;
					}
					flag = flag || flag96;
				}
				bool flag98 = hasRunningAnimations || ptr9.borderTopColor != ptr10.borderTopColor;
				if (flag98)
				{
					bool flag99 = element.styleAnimation.Start(StylePropertyId.BorderTopColor, ptr9.borderTopColor, ptr10.borderTopColor, durationMs, delayMs, easingCurve);
					bool flag100 = flag99;
					if (flag100)
					{
						usageHints |= UsageHints.DynamicColor;
					}
					flag = flag || flag99;
				}
				bool flag101 = hasRunningAnimations || ptr9.borderTopLeftRadius != ptr10.borderTopLeftRadius;
				if (flag101)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderTopLeftRadius, ptr9.borderTopLeftRadius, ptr10.borderTopLeftRadius, durationMs, delayMs, easingCurve);
				}
				bool flag102 = hasRunningAnimations || ptr9.borderTopRightRadius != ptr10.borderTopRightRadius;
				if (flag102)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderTopRightRadius, ptr9.borderTopRightRadius, ptr10.borderTopRightRadius, durationMs, delayMs, easingCurve);
				}
				bool flag103 = hasRunningAnimations || ptr9.opacity != ptr10.opacity;
				if (flag103)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Opacity, ptr9.opacity, ptr10.opacity, durationMs, delayMs, easingCurve);
				}
				bool flag104 = hasRunningAnimations || ptr9.overflow != ptr10.overflow;
				if (flag104)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.Overflow, (int)ptr9.overflow, (int)ptr10.overflow, durationMs, delayMs, easingCurve);
				}
			}
			bool flag105 = usageHints > UsageHints.None;
			if (flag105)
			{
				element.usageHints |= usageHints;
			}
			return flag;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x00065F90 File Offset: 0x00064190
		public static bool StartAnimationInline(VisualElement element, StylePropertyId id, ref ComputedStyle computedStyle, StyleValue sv, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			if (id <= StylePropertyId.Width)
			{
				switch (id)
				{
				case StylePropertyId.Color:
				{
					Color color = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.color : sv.color);
					bool flag = element.styleAnimation.Start(StylePropertyId.Color, computedStyle.inheritedData.Read().color, color, durationMs, delayMs, easingCurve);
					bool flag2 = flag && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
					if (flag2)
					{
						element.usageHints |= UsageHints.DynamicColor;
					}
					return flag;
				}
				case StylePropertyId.FontSize:
				{
					Length length = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.fontSize : sv.length);
					return element.styleAnimation.Start(StylePropertyId.FontSize, computedStyle.inheritedData.Read().fontSize, length, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.LetterSpacing:
				{
					Length length2 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.letterSpacing : sv.length);
					return element.styleAnimation.Start(StylePropertyId.LetterSpacing, computedStyle.inheritedData.Read().letterSpacing, length2, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.TextShadow:
				case StylePropertyId.UnityEditorTextRenderingMode:
				case StylePropertyId.UnityTextGenerator:
					break;
				case StylePropertyId.UnityFont:
				{
					Font font = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityFont : (sv.resource.IsAllocated ? (sv.resource.Target as Font) : null));
					return element.styleAnimation.Start(StylePropertyId.UnityFont, computedStyle.inheritedData.Read().unityFont, font, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityFontDefinition:
				{
					FontDefinition fontDefinition = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityFontDefinition : (sv.resource.IsAllocated ? FontDefinition.FromObject(sv.resource.Target) : default(FontDefinition)));
					return element.styleAnimation.Start(StylePropertyId.UnityFontDefinition, computedStyle.inheritedData.Read().unityFontDefinition, fontDefinition, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityFontStyleAndWeight:
				{
					FontStyle fontStyle = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityFontStyleAndWeight : ((FontStyle)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.UnityFontStyleAndWeight, (int)computedStyle.inheritedData.Read().unityFontStyleAndWeight, (int)fontStyle, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityParagraphSpacing:
				{
					Length length3 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityParagraphSpacing : sv.length);
					return element.styleAnimation.Start(StylePropertyId.UnityParagraphSpacing, computedStyle.inheritedData.Read().unityParagraphSpacing, length3, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityTextAlign:
				{
					TextAnchor textAnchor = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityTextAlign : ((TextAnchor)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.UnityTextAlign, (int)computedStyle.inheritedData.Read().unityTextAlign, (int)textAnchor, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityTextOutlineColor:
				{
					Color color2 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityTextOutlineColor : sv.color);
					return element.styleAnimation.Start(StylePropertyId.UnityTextOutlineColor, computedStyle.inheritedData.Read().unityTextOutlineColor, color2, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityTextOutlineWidth:
				{
					float num = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityTextOutlineWidth : sv.number);
					return element.styleAnimation.Start(StylePropertyId.UnityTextOutlineWidth, computedStyle.inheritedData.Read().unityTextOutlineWidth, num, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Visibility:
				{
					Visibility visibility = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.visibility : ((Visibility)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.Visibility, (int)computedStyle.inheritedData.Read().visibility, (int)visibility, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.WhiteSpace:
				{
					WhiteSpace whiteSpace = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.whiteSpace : ((WhiteSpace)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.WhiteSpace, (int)computedStyle.inheritedData.Read().whiteSpace, (int)whiteSpace, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.WordSpacing:
				{
					Length length4 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.wordSpacing : sv.length);
					return element.styleAnimation.Start(StylePropertyId.WordSpacing, computedStyle.inheritedData.Read().wordSpacing, length4, durationMs, delayMs, easingCurve);
				}
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
					{
						Align align = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.alignContent : ((Align)sv.number));
						bool flag3 = sv.keyword == StyleKeyword.Auto;
						if (flag3)
						{
							align = Align.Auto;
						}
						return element.styleAnimation.StartEnum(StylePropertyId.AlignContent, (int)computedStyle.layoutData.Read().alignContent, (int)align, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.AlignItems:
					{
						Align align2 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.alignItems : ((Align)sv.number));
						bool flag4 = sv.keyword == StyleKeyword.Auto;
						if (flag4)
						{
							align2 = Align.Auto;
						}
						return element.styleAnimation.StartEnum(StylePropertyId.AlignItems, (int)computedStyle.layoutData.Read().alignItems, (int)align2, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.AlignSelf:
					{
						Align align3 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.alignSelf : ((Align)sv.number));
						bool flag5 = sv.keyword == StyleKeyword.Auto;
						if (flag5)
						{
							align3 = Align.Auto;
						}
						return element.styleAnimation.StartEnum(StylePropertyId.AlignSelf, (int)computedStyle.layoutData.Read().alignSelf, (int)align3, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderBottomWidth:
					{
						float num2 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderBottomWidth : sv.number);
						return element.styleAnimation.Start(StylePropertyId.BorderBottomWidth, computedStyle.layoutData.Read().borderBottomWidth, num2, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderLeftWidth:
					{
						float num3 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderLeftWidth : sv.number);
						return element.styleAnimation.Start(StylePropertyId.BorderLeftWidth, computedStyle.layoutData.Read().borderLeftWidth, num3, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderRightWidth:
					{
						float num4 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderRightWidth : sv.number);
						return element.styleAnimation.Start(StylePropertyId.BorderRightWidth, computedStyle.layoutData.Read().borderRightWidth, num4, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderTopWidth:
					{
						float num5 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderTopWidth : sv.number);
						return element.styleAnimation.Start(StylePropertyId.BorderTopWidth, computedStyle.layoutData.Read().borderTopWidth, num5, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Bottom:
					{
						Length length5 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.bottom : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Bottom, computedStyle.layoutData.Read().bottom, length5, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexBasis:
					{
						Length length6 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexBasis : sv.length);
						return element.styleAnimation.Start(StylePropertyId.FlexBasis, computedStyle.layoutData.Read().flexBasis, length6, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexDirection:
					{
						FlexDirection flexDirection = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexDirection : ((FlexDirection)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.FlexDirection, (int)computedStyle.layoutData.Read().flexDirection, (int)flexDirection, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexGrow:
					{
						float num6 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexGrow : sv.number);
						return element.styleAnimation.Start(StylePropertyId.FlexGrow, computedStyle.layoutData.Read().flexGrow, num6, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexShrink:
					{
						float num7 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexShrink : sv.number);
						return element.styleAnimation.Start(StylePropertyId.FlexShrink, computedStyle.layoutData.Read().flexShrink, num7, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexWrap:
					{
						Wrap wrap = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexWrap : ((Wrap)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.FlexWrap, (int)computedStyle.layoutData.Read().flexWrap, (int)wrap, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Height:
					{
						Length length7 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.height : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Height, computedStyle.layoutData.Read().height, length7, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.JustifyContent:
					{
						Justify justify = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.justifyContent : ((Justify)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.JustifyContent, (int)computedStyle.layoutData.Read().justifyContent, (int)justify, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Left:
					{
						Length length8 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.left : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Left, computedStyle.layoutData.Read().left, length8, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MarginBottom:
					{
						Length length9 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.marginBottom : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MarginBottom, computedStyle.layoutData.Read().marginBottom, length9, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MarginLeft:
					{
						Length length10 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.marginLeft : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MarginLeft, computedStyle.layoutData.Read().marginLeft, length10, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MarginRight:
					{
						Length length11 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.marginRight : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MarginRight, computedStyle.layoutData.Read().marginRight, length11, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MarginTop:
					{
						Length length12 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.marginTop : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MarginTop, computedStyle.layoutData.Read().marginTop, length12, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MaxHeight:
					{
						Length length13 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.maxHeight : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MaxHeight, computedStyle.layoutData.Read().maxHeight, length13, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MaxWidth:
					{
						Length length14 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.maxWidth : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MaxWidth, computedStyle.layoutData.Read().maxWidth, length14, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MinHeight:
					{
						Length length15 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.minHeight : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MinHeight, computedStyle.layoutData.Read().minHeight, length15, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MinWidth:
					{
						Length length16 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.minWidth : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MinWidth, computedStyle.layoutData.Read().minWidth, length16, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.PaddingBottom:
					{
						Length length17 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.paddingBottom : sv.length);
						return element.styleAnimation.Start(StylePropertyId.PaddingBottom, computedStyle.layoutData.Read().paddingBottom, length17, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.PaddingLeft:
					{
						Length length18 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.paddingLeft : sv.length);
						return element.styleAnimation.Start(StylePropertyId.PaddingLeft, computedStyle.layoutData.Read().paddingLeft, length18, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.PaddingRight:
					{
						Length length19 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.paddingRight : sv.length);
						return element.styleAnimation.Start(StylePropertyId.PaddingRight, computedStyle.layoutData.Read().paddingRight, length19, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.PaddingTop:
					{
						Length length20 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.paddingTop : sv.length);
						return element.styleAnimation.Start(StylePropertyId.PaddingTop, computedStyle.layoutData.Read().paddingTop, length20, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Position:
					{
						Position position = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.position : ((Position)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.Position, (int)computedStyle.layoutData.Read().position, (int)position, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Right:
					{
						Length length21 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.right : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Right, computedStyle.layoutData.Read().right, length21, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Top:
					{
						Length length22 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.top : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Top, computedStyle.layoutData.Read().top, length22, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Width:
					{
						Length length23 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.width : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Width, computedStyle.layoutData.Read().width, length23, durationMs, delayMs, easingCurve);
					}
					}
					break;
				}
			}
			else
			{
				switch (id)
				{
				case StylePropertyId.TextOverflow:
				{
					TextOverflow textOverflow = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.textOverflow : ((TextOverflow)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.TextOverflow, (int)computedStyle.rareData.Read().textOverflow, (int)textOverflow, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityBackgroundImageTintColor:
				{
					Color color3 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityBackgroundImageTintColor : sv.color);
					bool flag6 = element.styleAnimation.Start(StylePropertyId.UnityBackgroundImageTintColor, computedStyle.rareData.Read().unityBackgroundImageTintColor, color3, durationMs, delayMs, easingCurve);
					bool flag7 = flag6 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
					if (flag7)
					{
						element.usageHints |= UsageHints.DynamicColor;
					}
					return flag6;
				}
				case StylePropertyId.UnityOverflowClipBox:
				{
					OverflowClipBox overflowClipBox = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityOverflowClipBox : ((OverflowClipBox)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.UnityOverflowClipBox, (int)computedStyle.rareData.Read().unityOverflowClipBox, (int)overflowClipBox, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceBottom:
				{
					int num8 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceBottom : ((int)sv.number));
					return element.styleAnimation.Start(StylePropertyId.UnitySliceBottom, computedStyle.rareData.Read().unitySliceBottom, num8, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceLeft:
				{
					int num9 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceLeft : ((int)sv.number));
					return element.styleAnimation.Start(StylePropertyId.UnitySliceLeft, computedStyle.rareData.Read().unitySliceLeft, num9, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceRight:
				{
					int num10 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceRight : ((int)sv.number));
					return element.styleAnimation.Start(StylePropertyId.UnitySliceRight, computedStyle.rareData.Read().unitySliceRight, num10, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceScale:
				{
					float num11 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceScale : sv.number);
					return element.styleAnimation.Start(StylePropertyId.UnitySliceScale, computedStyle.rareData.Read().unitySliceScale, num11, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceTop:
				{
					int num12 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceTop : ((int)sv.number));
					return element.styleAnimation.Start(StylePropertyId.UnitySliceTop, computedStyle.rareData.Read().unitySliceTop, num12, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceType:
				{
					SliceType sliceType = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceType : ((SliceType)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.UnitySliceType, (int)computedStyle.rareData.Read().unitySliceType, (int)sliceType, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityTextOverflowPosition:
				{
					TextOverflowPosition textOverflowPosition = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityTextOverflowPosition : ((TextOverflowPosition)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.UnityTextOverflowPosition, (int)computedStyle.rareData.Read().unityTextOverflowPosition, (int)textOverflowPosition, durationMs, delayMs, easingCurve);
				}
				default:
					switch (id)
					{
					case StylePropertyId.BackgroundColor:
					{
						Color color4 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.backgroundColor : sv.color);
						bool flag8 = element.styleAnimation.Start(StylePropertyId.BackgroundColor, computedStyle.visualData.Read().backgroundColor, color4, durationMs, delayMs, easingCurve);
						bool flag9 = flag8 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
						if (flag9)
						{
							element.usageHints |= UsageHints.DynamicColor;
						}
						return flag8;
					}
					case StylePropertyId.BackgroundImage:
					{
						Background background = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.backgroundImage : (sv.resource.IsAllocated ? Background.FromObject(sv.resource.Target) : default(Background)));
						return element.styleAnimation.Start(StylePropertyId.BackgroundImage, computedStyle.visualData.Read().backgroundImage, background, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BackgroundPositionX:
					{
						BackgroundPosition backgroundPosition = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.backgroundPositionX : sv.position);
						return element.styleAnimation.Start(StylePropertyId.BackgroundPositionX, computedStyle.visualData.Read().backgroundPositionX, backgroundPosition, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BackgroundPositionY:
					{
						BackgroundPosition backgroundPosition2 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.backgroundPositionY : sv.position);
						return element.styleAnimation.Start(StylePropertyId.BackgroundPositionY, computedStyle.visualData.Read().backgroundPositionY, backgroundPosition2, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BackgroundRepeat:
					{
						BackgroundRepeat backgroundRepeat = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.backgroundRepeat : sv.repeat);
						return element.styleAnimation.Start(StylePropertyId.BackgroundRepeat, computedStyle.visualData.Read().backgroundRepeat, backgroundRepeat, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderBottomColor:
					{
						Color color5 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderBottomColor : sv.color);
						bool flag10 = element.styleAnimation.Start(StylePropertyId.BorderBottomColor, computedStyle.visualData.Read().borderBottomColor, color5, durationMs, delayMs, easingCurve);
						bool flag11 = flag10 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
						if (flag11)
						{
							element.usageHints |= UsageHints.DynamicColor;
						}
						return flag10;
					}
					case StylePropertyId.BorderBottomLeftRadius:
					{
						Length length24 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderBottomLeftRadius : sv.length);
						return element.styleAnimation.Start(StylePropertyId.BorderBottomLeftRadius, computedStyle.visualData.Read().borderBottomLeftRadius, length24, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderBottomRightRadius:
					{
						Length length25 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderBottomRightRadius : sv.length);
						return element.styleAnimation.Start(StylePropertyId.BorderBottomRightRadius, computedStyle.visualData.Read().borderBottomRightRadius, length25, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderLeftColor:
					{
						Color color6 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderLeftColor : sv.color);
						bool flag12 = element.styleAnimation.Start(StylePropertyId.BorderLeftColor, computedStyle.visualData.Read().borderLeftColor, color6, durationMs, delayMs, easingCurve);
						bool flag13 = flag12 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
						if (flag13)
						{
							element.usageHints |= UsageHints.DynamicColor;
						}
						return flag12;
					}
					case StylePropertyId.BorderRightColor:
					{
						Color color7 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderRightColor : sv.color);
						bool flag14 = element.styleAnimation.Start(StylePropertyId.BorderRightColor, computedStyle.visualData.Read().borderRightColor, color7, durationMs, delayMs, easingCurve);
						bool flag15 = flag14 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
						if (flag15)
						{
							element.usageHints |= UsageHints.DynamicColor;
						}
						return flag14;
					}
					case StylePropertyId.BorderTopColor:
					{
						Color color8 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderTopColor : sv.color);
						bool flag16 = element.styleAnimation.Start(StylePropertyId.BorderTopColor, computedStyle.visualData.Read().borderTopColor, color8, durationMs, delayMs, easingCurve);
						bool flag17 = flag16 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
						if (flag17)
						{
							element.usageHints |= UsageHints.DynamicColor;
						}
						return flag16;
					}
					case StylePropertyId.BorderTopLeftRadius:
					{
						Length length26 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderTopLeftRadius : sv.length);
						return element.styleAnimation.Start(StylePropertyId.BorderTopLeftRadius, computedStyle.visualData.Read().borderTopLeftRadius, length26, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderTopRightRadius:
					{
						Length length27 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderTopRightRadius : sv.length);
						return element.styleAnimation.Start(StylePropertyId.BorderTopRightRadius, computedStyle.visualData.Read().borderTopRightRadius, length27, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Opacity:
					{
						float num13 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.opacity : sv.number);
						return element.styleAnimation.Start(StylePropertyId.Opacity, computedStyle.visualData.Read().opacity, num13, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Overflow:
					{
						OverflowInternal overflowInternal = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.overflow : ((OverflowInternal)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.Overflow, (int)computedStyle.visualData.Read().overflow, (int)overflowInternal, durationMs, delayMs, easingCurve);
					}
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x000676AE File Offset: 0x000658AE
		public void ApplyStyleTransformOrigin(TransformOrigin st)
		{
			this.transformData.Write().transformOrigin = st;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x000676C2 File Offset: 0x000658C2
		public void ApplyStyleTranslate(Translate translateValue)
		{
			this.transformData.Write().translate = translateValue;
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x000676D6 File Offset: 0x000658D6
		public void ApplyStyleRotate(Rotate rotateValue)
		{
			this.transformData.Write().rotate = rotateValue;
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x000676EA File Offset: 0x000658EA
		public void ApplyStyleScale(Scale scaleValue)
		{
			this.transformData.Write().scale = scaleValue;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x000676FE File Offset: 0x000658FE
		public void ApplyStyleBackgroundSize(BackgroundSize backgroundSizeValue)
		{
			this.visualData.Write().backgroundSize = backgroundSizeValue;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00067714 File Offset: 0x00065914
		public void ApplyInitialValue(StylePropertyReader reader)
		{
			StylePropertyId propertyId = reader.propertyId;
			StylePropertyId stylePropertyId = propertyId;
			if (stylePropertyId != StylePropertyId.Custom)
			{
				if (stylePropertyId != StylePropertyId.All)
				{
					this.ApplyInitialValue(reader.propertyId);
				}
				else
				{
					this.ApplyAllPropertyInitial();
				}
			}
			else
			{
				this.RemoveCustomStyleProperty(reader);
			}
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00067760 File Offset: 0x00065960
		public void ApplyInitialValue(StylePropertyId id)
		{
			if (id <= StylePropertyId.UnityTextOverflowPosition)
			{
				switch (id)
				{
				case StylePropertyId.Color:
					this.inheritedData.Write().color = InitialStyle.color;
					return;
				case StylePropertyId.FontSize:
					this.inheritedData.Write().fontSize = InitialStyle.fontSize;
					return;
				case StylePropertyId.LetterSpacing:
					this.inheritedData.Write().letterSpacing = InitialStyle.letterSpacing;
					return;
				case StylePropertyId.TextShadow:
					this.inheritedData.Write().textShadow = InitialStyle.textShadow;
					return;
				case StylePropertyId.UnityEditorTextRenderingMode:
					this.inheritedData.Write().unityEditorTextRenderingMode = InitialStyle.unityEditorTextRenderingMode;
					return;
				case StylePropertyId.UnityFont:
					this.inheritedData.Write().unityFont = InitialStyle.unityFont;
					return;
				case StylePropertyId.UnityFontDefinition:
					this.inheritedData.Write().unityFontDefinition = InitialStyle.unityFontDefinition;
					return;
				case StylePropertyId.UnityFontStyleAndWeight:
					this.inheritedData.Write().unityFontStyleAndWeight = InitialStyle.unityFontStyleAndWeight;
					return;
				case StylePropertyId.UnityParagraphSpacing:
					this.inheritedData.Write().unityParagraphSpacing = InitialStyle.unityParagraphSpacing;
					return;
				case StylePropertyId.UnityTextAlign:
					this.inheritedData.Write().unityTextAlign = InitialStyle.unityTextAlign;
					return;
				case StylePropertyId.UnityTextGenerator:
					this.inheritedData.Write().unityTextGenerator = InitialStyle.unityTextGenerator;
					return;
				case StylePropertyId.UnityTextOutlineColor:
					this.inheritedData.Write().unityTextOutlineColor = InitialStyle.unityTextOutlineColor;
					return;
				case StylePropertyId.UnityTextOutlineWidth:
					this.inheritedData.Write().unityTextOutlineWidth = InitialStyle.unityTextOutlineWidth;
					return;
				case StylePropertyId.Visibility:
					this.inheritedData.Write().visibility = InitialStyle.visibility;
					return;
				case StylePropertyId.WhiteSpace:
					this.inheritedData.Write().whiteSpace = InitialStyle.whiteSpace;
					return;
				case StylePropertyId.WordSpacing:
					this.inheritedData.Write().wordSpacing = InitialStyle.wordSpacing;
					return;
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
						this.layoutData.Write().alignContent = InitialStyle.alignContent;
						return;
					case StylePropertyId.AlignItems:
						this.layoutData.Write().alignItems = InitialStyle.alignItems;
						return;
					case StylePropertyId.AlignSelf:
						this.layoutData.Write().alignSelf = InitialStyle.alignSelf;
						return;
					case StylePropertyId.BorderBottomWidth:
						this.layoutData.Write().borderBottomWidth = InitialStyle.borderBottomWidth;
						return;
					case StylePropertyId.BorderLeftWidth:
						this.layoutData.Write().borderLeftWidth = InitialStyle.borderLeftWidth;
						return;
					case StylePropertyId.BorderRightWidth:
						this.layoutData.Write().borderRightWidth = InitialStyle.borderRightWidth;
						return;
					case StylePropertyId.BorderTopWidth:
						this.layoutData.Write().borderTopWidth = InitialStyle.borderTopWidth;
						return;
					case StylePropertyId.Bottom:
						this.layoutData.Write().bottom = InitialStyle.bottom;
						return;
					case StylePropertyId.Display:
						this.layoutData.Write().display = InitialStyle.display;
						return;
					case StylePropertyId.FlexBasis:
						this.layoutData.Write().flexBasis = InitialStyle.flexBasis;
						return;
					case StylePropertyId.FlexDirection:
						this.layoutData.Write().flexDirection = InitialStyle.flexDirection;
						return;
					case StylePropertyId.FlexGrow:
						this.layoutData.Write().flexGrow = InitialStyle.flexGrow;
						return;
					case StylePropertyId.FlexShrink:
						this.layoutData.Write().flexShrink = InitialStyle.flexShrink;
						return;
					case StylePropertyId.FlexWrap:
						this.layoutData.Write().flexWrap = InitialStyle.flexWrap;
						return;
					case StylePropertyId.Height:
						this.layoutData.Write().height = InitialStyle.height;
						return;
					case StylePropertyId.JustifyContent:
						this.layoutData.Write().justifyContent = InitialStyle.justifyContent;
						return;
					case StylePropertyId.Left:
						this.layoutData.Write().left = InitialStyle.left;
						return;
					case StylePropertyId.MarginBottom:
						this.layoutData.Write().marginBottom = InitialStyle.marginBottom;
						return;
					case StylePropertyId.MarginLeft:
						this.layoutData.Write().marginLeft = InitialStyle.marginLeft;
						return;
					case StylePropertyId.MarginRight:
						this.layoutData.Write().marginRight = InitialStyle.marginRight;
						return;
					case StylePropertyId.MarginTop:
						this.layoutData.Write().marginTop = InitialStyle.marginTop;
						return;
					case StylePropertyId.MaxHeight:
						this.layoutData.Write().maxHeight = InitialStyle.maxHeight;
						return;
					case StylePropertyId.MaxWidth:
						this.layoutData.Write().maxWidth = InitialStyle.maxWidth;
						return;
					case StylePropertyId.MinHeight:
						this.layoutData.Write().minHeight = InitialStyle.minHeight;
						return;
					case StylePropertyId.MinWidth:
						this.layoutData.Write().minWidth = InitialStyle.minWidth;
						return;
					case StylePropertyId.PaddingBottom:
						this.layoutData.Write().paddingBottom = InitialStyle.paddingBottom;
						return;
					case StylePropertyId.PaddingLeft:
						this.layoutData.Write().paddingLeft = InitialStyle.paddingLeft;
						return;
					case StylePropertyId.PaddingRight:
						this.layoutData.Write().paddingRight = InitialStyle.paddingRight;
						return;
					case StylePropertyId.PaddingTop:
						this.layoutData.Write().paddingTop = InitialStyle.paddingTop;
						return;
					case StylePropertyId.Position:
						this.layoutData.Write().position = InitialStyle.position;
						return;
					case StylePropertyId.Right:
						this.layoutData.Write().right = InitialStyle.right;
						return;
					case StylePropertyId.Top:
						this.layoutData.Write().top = InitialStyle.top;
						return;
					case StylePropertyId.Width:
						this.layoutData.Write().width = InitialStyle.width;
						return;
					default:
						switch (id)
						{
						case StylePropertyId.Cursor:
							this.rareData.Write().cursor = InitialStyle.cursor;
							return;
						case StylePropertyId.TextOverflow:
							this.rareData.Write().textOverflow = InitialStyle.textOverflow;
							return;
						case StylePropertyId.UnityBackgroundImageTintColor:
							this.rareData.Write().unityBackgroundImageTintColor = InitialStyle.unityBackgroundImageTintColor;
							return;
						case StylePropertyId.UnityOverflowClipBox:
							this.rareData.Write().unityOverflowClipBox = InitialStyle.unityOverflowClipBox;
							return;
						case StylePropertyId.UnitySliceBottom:
							this.rareData.Write().unitySliceBottom = InitialStyle.unitySliceBottom;
							return;
						case StylePropertyId.UnitySliceLeft:
							this.rareData.Write().unitySliceLeft = InitialStyle.unitySliceLeft;
							return;
						case StylePropertyId.UnitySliceRight:
							this.rareData.Write().unitySliceRight = InitialStyle.unitySliceRight;
							return;
						case StylePropertyId.UnitySliceScale:
							this.rareData.Write().unitySliceScale = InitialStyle.unitySliceScale;
							return;
						case StylePropertyId.UnitySliceTop:
							this.rareData.Write().unitySliceTop = InitialStyle.unitySliceTop;
							return;
						case StylePropertyId.UnitySliceType:
							this.rareData.Write().unitySliceType = InitialStyle.unitySliceType;
							return;
						case StylePropertyId.UnityTextOverflowPosition:
							this.rareData.Write().unityTextOverflowPosition = InitialStyle.unityTextOverflowPosition;
							return;
						}
						break;
					}
					break;
				}
			}
			else if (id <= StylePropertyId.Translate)
			{
				switch (id)
				{
				case StylePropertyId.All:
					return;
				case StylePropertyId.BackgroundPosition:
					this.visualData.Write().backgroundPositionX = InitialStyle.backgroundPositionX;
					this.visualData.Write().backgroundPositionY = InitialStyle.backgroundPositionY;
					return;
				case StylePropertyId.BorderColor:
					this.visualData.Write().borderTopColor = InitialStyle.borderTopColor;
					this.visualData.Write().borderRightColor = InitialStyle.borderRightColor;
					this.visualData.Write().borderBottomColor = InitialStyle.borderBottomColor;
					this.visualData.Write().borderLeftColor = InitialStyle.borderLeftColor;
					return;
				case StylePropertyId.BorderRadius:
					this.visualData.Write().borderTopLeftRadius = InitialStyle.borderTopLeftRadius;
					this.visualData.Write().borderTopRightRadius = InitialStyle.borderTopRightRadius;
					this.visualData.Write().borderBottomRightRadius = InitialStyle.borderBottomRightRadius;
					this.visualData.Write().borderBottomLeftRadius = InitialStyle.borderBottomLeftRadius;
					return;
				case StylePropertyId.BorderWidth:
					this.layoutData.Write().borderTopWidth = InitialStyle.borderTopWidth;
					this.layoutData.Write().borderRightWidth = InitialStyle.borderRightWidth;
					this.layoutData.Write().borderBottomWidth = InitialStyle.borderBottomWidth;
					this.layoutData.Write().borderLeftWidth = InitialStyle.borderLeftWidth;
					return;
				case StylePropertyId.Flex:
					this.layoutData.Write().flexGrow = InitialStyle.flexGrow;
					this.layoutData.Write().flexShrink = InitialStyle.flexShrink;
					this.layoutData.Write().flexBasis = InitialStyle.flexBasis;
					return;
				case StylePropertyId.Margin:
					this.layoutData.Write().marginTop = InitialStyle.marginTop;
					this.layoutData.Write().marginRight = InitialStyle.marginRight;
					this.layoutData.Write().marginBottom = InitialStyle.marginBottom;
					this.layoutData.Write().marginLeft = InitialStyle.marginLeft;
					return;
				case StylePropertyId.Padding:
					this.layoutData.Write().paddingTop = InitialStyle.paddingTop;
					this.layoutData.Write().paddingRight = InitialStyle.paddingRight;
					this.layoutData.Write().paddingBottom = InitialStyle.paddingBottom;
					this.layoutData.Write().paddingLeft = InitialStyle.paddingLeft;
					return;
				case StylePropertyId.Transition:
					this.transitionData.Write().transitionDelay.CopyFrom<TimeValue>(InitialStyle.transitionDelay);
					this.transitionData.Write().transitionDuration.CopyFrom<TimeValue>(InitialStyle.transitionDuration);
					this.transitionData.Write().transitionProperty.CopyFrom<StylePropertyName>(InitialStyle.transitionProperty);
					this.transitionData.Write().transitionTimingFunction.CopyFrom<EasingFunction>(InitialStyle.transitionTimingFunction);
					this.ResetComputedTransitions();
					return;
				case StylePropertyId.UnityBackgroundScaleMode:
					this.visualData.Write().backgroundPositionX = InitialStyle.backgroundPositionX;
					this.visualData.Write().backgroundPositionY = InitialStyle.backgroundPositionY;
					this.visualData.Write().backgroundRepeat = InitialStyle.backgroundRepeat;
					this.visualData.Write().backgroundSize = InitialStyle.backgroundSize;
					return;
				case StylePropertyId.UnityTextOutline:
					this.inheritedData.Write().unityTextOutlineColor = InitialStyle.unityTextOutlineColor;
					this.inheritedData.Write().unityTextOutlineWidth = InitialStyle.unityTextOutlineWidth;
					return;
				default:
					switch (id)
					{
					case StylePropertyId.Rotate:
						this.transformData.Write().rotate = InitialStyle.rotate;
						return;
					case StylePropertyId.Scale:
						this.transformData.Write().scale = InitialStyle.scale;
						return;
					case StylePropertyId.TransformOrigin:
						this.transformData.Write().transformOrigin = InitialStyle.transformOrigin;
						return;
					case StylePropertyId.Translate:
						this.transformData.Write().translate = InitialStyle.translate;
						return;
					}
					break;
				}
			}
			else
			{
				switch (id)
				{
				case StylePropertyId.TransitionDelay:
					this.transitionData.Write().transitionDelay.CopyFrom<TimeValue>(InitialStyle.transitionDelay);
					this.ResetComputedTransitions();
					return;
				case StylePropertyId.TransitionDuration:
					this.transitionData.Write().transitionDuration.CopyFrom<TimeValue>(InitialStyle.transitionDuration);
					this.ResetComputedTransitions();
					return;
				case StylePropertyId.TransitionProperty:
					this.transitionData.Write().transitionProperty.CopyFrom<StylePropertyName>(InitialStyle.transitionProperty);
					this.ResetComputedTransitions();
					return;
				case StylePropertyId.TransitionTimingFunction:
					this.transitionData.Write().transitionTimingFunction.CopyFrom<EasingFunction>(InitialStyle.transitionTimingFunction);
					this.ResetComputedTransitions();
					return;
				default:
					switch (id)
					{
					case StylePropertyId.BackgroundColor:
						this.visualData.Write().backgroundColor = InitialStyle.backgroundColor;
						return;
					case StylePropertyId.BackgroundImage:
						this.visualData.Write().backgroundImage = InitialStyle.backgroundImage;
						return;
					case StylePropertyId.BackgroundPositionX:
						this.visualData.Write().backgroundPositionX = InitialStyle.backgroundPositionX;
						return;
					case StylePropertyId.BackgroundPositionY:
						this.visualData.Write().backgroundPositionY = InitialStyle.backgroundPositionY;
						return;
					case StylePropertyId.BackgroundRepeat:
						this.visualData.Write().backgroundRepeat = InitialStyle.backgroundRepeat;
						return;
					case StylePropertyId.BackgroundSize:
						this.visualData.Write().backgroundSize = InitialStyle.backgroundSize;
						return;
					case StylePropertyId.BorderBottomColor:
						this.visualData.Write().borderBottomColor = InitialStyle.borderBottomColor;
						return;
					case StylePropertyId.BorderBottomLeftRadius:
						this.visualData.Write().borderBottomLeftRadius = InitialStyle.borderBottomLeftRadius;
						return;
					case StylePropertyId.BorderBottomRightRadius:
						this.visualData.Write().borderBottomRightRadius = InitialStyle.borderBottomRightRadius;
						return;
					case StylePropertyId.BorderLeftColor:
						this.visualData.Write().borderLeftColor = InitialStyle.borderLeftColor;
						return;
					case StylePropertyId.BorderRightColor:
						this.visualData.Write().borderRightColor = InitialStyle.borderRightColor;
						return;
					case StylePropertyId.BorderTopColor:
						this.visualData.Write().borderTopColor = InitialStyle.borderTopColor;
						return;
					case StylePropertyId.BorderTopLeftRadius:
						this.visualData.Write().borderTopLeftRadius = InitialStyle.borderTopLeftRadius;
						return;
					case StylePropertyId.BorderTopRightRadius:
						this.visualData.Write().borderTopRightRadius = InitialStyle.borderTopRightRadius;
						return;
					case StylePropertyId.Opacity:
						this.visualData.Write().opacity = InitialStyle.opacity;
						return;
					case StylePropertyId.Overflow:
						this.visualData.Write().overflow = InitialStyle.overflow;
						return;
					}
					break;
				}
			}
			Debug.LogAssertion(string.Format("Unexpected property id {0}", id));
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x00068568 File Offset: 0x00066768
		public void ApplyUnsetValue(StylePropertyReader reader, ref ComputedStyle parentStyle)
		{
			StylePropertyId propertyId = reader.propertyId;
			StylePropertyId stylePropertyId = propertyId;
			if (stylePropertyId != StylePropertyId.Custom)
			{
				this.ApplyUnsetValue(reader.propertyId, ref parentStyle);
			}
			else
			{
				this.RemoveCustomStyleProperty(reader);
			}
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x000685A0 File Offset: 0x000667A0
		public void ApplyUnsetValue(StylePropertyId id, ref ComputedStyle parentStyle)
		{
			switch (id)
			{
			case StylePropertyId.Color:
				this.inheritedData.Write().color = parentStyle.color;
				break;
			case StylePropertyId.FontSize:
				this.inheritedData.Write().fontSize = parentStyle.fontSize;
				break;
			case StylePropertyId.LetterSpacing:
				this.inheritedData.Write().letterSpacing = parentStyle.letterSpacing;
				break;
			case StylePropertyId.TextShadow:
				this.inheritedData.Write().textShadow = parentStyle.textShadow;
				break;
			case StylePropertyId.UnityEditorTextRenderingMode:
				this.inheritedData.Write().unityEditorTextRenderingMode = parentStyle.unityEditorTextRenderingMode;
				break;
			case StylePropertyId.UnityFont:
				this.inheritedData.Write().unityFont = parentStyle.unityFont;
				break;
			case StylePropertyId.UnityFontDefinition:
				this.inheritedData.Write().unityFontDefinition = parentStyle.unityFontDefinition;
				break;
			case StylePropertyId.UnityFontStyleAndWeight:
				this.inheritedData.Write().unityFontStyleAndWeight = parentStyle.unityFontStyleAndWeight;
				break;
			case StylePropertyId.UnityParagraphSpacing:
				this.inheritedData.Write().unityParagraphSpacing = parentStyle.unityParagraphSpacing;
				break;
			case StylePropertyId.UnityTextAlign:
				this.inheritedData.Write().unityTextAlign = parentStyle.unityTextAlign;
				break;
			case StylePropertyId.UnityTextGenerator:
				this.inheritedData.Write().unityTextGenerator = parentStyle.unityTextGenerator;
				break;
			case StylePropertyId.UnityTextOutlineColor:
				this.inheritedData.Write().unityTextOutlineColor = parentStyle.unityTextOutlineColor;
				break;
			case StylePropertyId.UnityTextOutlineWidth:
				this.inheritedData.Write().unityTextOutlineWidth = parentStyle.unityTextOutlineWidth;
				break;
			case StylePropertyId.Visibility:
				this.inheritedData.Write().visibility = parentStyle.visibility;
				break;
			case StylePropertyId.WhiteSpace:
				this.inheritedData.Write().whiteSpace = parentStyle.whiteSpace;
				break;
			case StylePropertyId.WordSpacing:
				this.inheritedData.Write().wordSpacing = parentStyle.wordSpacing;
				break;
			default:
				this.ApplyInitialValue(id);
				break;
			}
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x000687B0 File Offset: 0x000669B0
		public static VersionChangeType CompareChanges(ref ComputedStyle x, ref ComputedStyle y)
		{
			VersionChangeType versionChangeType = VersionChangeType.Styles;
			bool flag = !x.layoutData.ReferenceEquals(y.layoutData);
			if (flag)
			{
				bool flag2 = x.display != y.display || x.flexGrow != y.flexGrow || x.flexShrink != y.flexShrink || x.flexWrap != y.flexWrap || x.flexDirection != y.flexDirection || x.justifyContent != y.justifyContent || x.bottom != y.bottom || x.left != y.left || x.right != y.right || x.top != y.top || x.height != y.height || x.width != y.width || x.paddingBottom != y.paddingBottom || x.paddingLeft != y.paddingLeft || x.paddingRight != y.paddingRight || x.paddingTop != y.paddingTop || x.marginBottom != y.marginBottom || x.marginLeft != y.marginLeft || x.marginRight != y.marginRight || x.marginTop != y.marginTop || x.position != y.position || x.alignContent != y.alignContent || x.alignItems != y.alignItems || x.alignSelf != y.alignSelf || x.flexBasis != y.flexBasis || x.maxHeight != y.maxHeight || x.maxWidth != y.maxWidth || x.minHeight != y.minHeight || x.minWidth != y.minWidth;
				if (flag2)
				{
					versionChangeType |= VersionChangeType.Layout;
				}
				bool flag3 = x.borderBottomWidth != y.borderBottomWidth || x.borderLeftWidth != y.borderLeftWidth || x.borderRightWidth != y.borderRightWidth || x.borderTopWidth != y.borderTopWidth;
				if (flag3)
				{
					versionChangeType |= VersionChangeType.Layout | VersionChangeType.BorderWidth | VersionChangeType.Repaint;
				}
			}
			bool flag4 = !x.inheritedData.ReferenceEquals(y.inheritedData);
			if (flag4)
			{
				bool flag5 = x.color != y.color;
				if (flag5)
				{
					versionChangeType |= VersionChangeType.Color;
				}
				bool flag6 = (versionChangeType & (VersionChangeType.Layout | VersionChangeType.Repaint)) == (VersionChangeType)0 && (x.unityFont != y.unityFont || x.unityTextGenerator != y.unityTextGenerator || x.fontSize != y.fontSize || x.unityFontDefinition != y.unityFontDefinition || x.whiteSpace != y.whiteSpace || x.unityFontStyleAndWeight != y.unityFontStyleAndWeight || x.unityTextOutlineWidth != y.unityTextOutlineWidth || x.letterSpacing != y.letterSpacing || x.wordSpacing != y.wordSpacing || x.unityEditorTextRenderingMode != y.unityEditorTextRenderingMode || x.unityParagraphSpacing != y.unityParagraphSpacing);
				if (flag6)
				{
					versionChangeType |= VersionChangeType.Layout | VersionChangeType.Repaint;
				}
				bool flag7 = (versionChangeType & VersionChangeType.Repaint) == (VersionChangeType)0 && (x.textShadow != y.textShadow || x.unityTextAlign != y.unityTextAlign || x.unityTextOutlineColor != y.unityTextOutlineColor);
				if (flag7)
				{
					versionChangeType |= VersionChangeType.Repaint;
				}
				bool flag8 = x.visibility != y.visibility;
				if (flag8)
				{
					versionChangeType |= VersionChangeType.Repaint | VersionChangeType.Picking;
				}
			}
			bool flag9 = !x.transformData.ReferenceEquals(y.transformData);
			if (flag9)
			{
				bool flag10 = x.scale != y.scale || x.rotate != y.rotate || x.translate != y.translate || x.transformOrigin != y.transformOrigin;
				if (flag10)
				{
					versionChangeType |= VersionChangeType.Transform;
				}
			}
			bool flag11 = !x.transitionData.ReferenceEquals(y.transitionData);
			if (flag11)
			{
				bool flag12 = !ComputedTransitionUtils.SameTransitionProperty(ref x, ref y);
				if (flag12)
				{
					versionChangeType |= VersionChangeType.TransitionProperty;
				}
			}
			bool flag13 = !x.visualData.ReferenceEquals(y.visualData);
			if (flag13)
			{
				bool flag14 = (versionChangeType & VersionChangeType.Color) == (VersionChangeType)0 && (x.backgroundColor != y.backgroundColor || x.borderBottomColor != y.borderBottomColor || x.borderLeftColor != y.borderLeftColor || x.borderRightColor != y.borderRightColor || x.borderTopColor != y.borderTopColor);
				if (flag14)
				{
					versionChangeType |= VersionChangeType.Color;
				}
				bool flag15 = x.backgroundImage != y.backgroundImage;
				if (flag15)
				{
					versionChangeType |= VersionChangeType.Overflow | VersionChangeType.Repaint;
				}
				bool flag16 = (versionChangeType & VersionChangeType.Repaint) == (VersionChangeType)0 && (x.backgroundPositionX != y.backgroundPositionX || x.backgroundPositionY != y.backgroundPositionY || x.backgroundRepeat != y.backgroundRepeat || x.backgroundSize != y.backgroundSize);
				if (flag16)
				{
					versionChangeType |= VersionChangeType.Repaint;
				}
				bool flag17 = x.borderBottomLeftRadius != y.borderBottomLeftRadius || x.borderBottomRightRadius != y.borderBottomRightRadius || x.borderTopLeftRadius != y.borderTopLeftRadius || x.borderTopRightRadius != y.borderTopRightRadius;
				if (flag17)
				{
					versionChangeType |= VersionChangeType.BorderRadius | VersionChangeType.Repaint;
				}
				bool flag18 = x.opacity != y.opacity;
				if (flag18)
				{
					versionChangeType |= VersionChangeType.Opacity;
				}
				bool flag19 = x.overflow != y.overflow;
				if (flag19)
				{
					versionChangeType |= VersionChangeType.Layout | VersionChangeType.Overflow;
				}
			}
			bool flag20 = !x.rareData.ReferenceEquals(y.rareData);
			if (flag20)
			{
				bool flag21 = (versionChangeType & (VersionChangeType.Layout | VersionChangeType.Repaint)) == (VersionChangeType)0 && (x.unitySliceType != y.unitySliceType || x.textOverflow != y.textOverflow || x.unitySliceScale != y.unitySliceScale);
				if (flag21)
				{
					versionChangeType |= VersionChangeType.Layout | VersionChangeType.Repaint;
				}
				bool flag22 = x.unityBackgroundImageTintColor != y.unityBackgroundImageTintColor;
				if (flag22)
				{
					versionChangeType |= VersionChangeType.Color;
				}
				bool flag23 = (versionChangeType & VersionChangeType.Repaint) == (VersionChangeType)0 && (x.unityOverflowClipBox != y.unityOverflowClipBox || x.unitySliceBottom != y.unitySliceBottom || x.unitySliceLeft != y.unitySliceLeft || x.unitySliceRight != y.unitySliceRight || x.unitySliceTop != y.unitySliceTop || x.unityTextOverflowPosition != y.unityTextOverflowPosition);
				if (flag23)
				{
					versionChangeType |= VersionChangeType.Repaint;
				}
			}
			return versionChangeType;
		}

		// Token: 0x04000D49 RID: 3401
		public StyleDataRef<InheritedData> inheritedData;

		// Token: 0x04000D4A RID: 3402
		public StyleDataRef<LayoutData> layoutData;

		// Token: 0x04000D4B RID: 3403
		public StyleDataRef<RareData> rareData;

		// Token: 0x04000D4C RID: 3404
		public StyleDataRef<TransformData> transformData;

		// Token: 0x04000D4D RID: 3405
		public StyleDataRef<TransitionData> transitionData;

		// Token: 0x04000D4E RID: 3406
		public StyleDataRef<VisualData> visualData;

		// Token: 0x04000D4F RID: 3407
		public Dictionary<string, StylePropertyValue> customProperties;

		// Token: 0x04000D50 RID: 3408
		public long matchingRulesHash;

		// Token: 0x04000D51 RID: 3409
		public float dpiScaling;

		// Token: 0x04000D52 RID: 3410
		public ComputedTransitionProperty[] computedTransitions;
	}
}
