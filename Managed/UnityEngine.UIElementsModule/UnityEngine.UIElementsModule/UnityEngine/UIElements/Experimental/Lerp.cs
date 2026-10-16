using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x0200071C RID: 1820
	internal static class Lerp
	{
		// Token: 0x060034C6 RID: 13510 RVA: 0x000CCC1C File Offset: 0x000CAE1C
		public static float Interpolate(float start, float end, float ratio)
		{
			return Mathf.LerpUnclamped(start, end, ratio);
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000CCC38 File Offset: 0x000CAE38
		public static int Interpolate(int start, int end, float ratio)
		{
			return Mathf.RoundToInt(Mathf.LerpUnclamped((float)start, (float)end, ratio));
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000CCC5C File Offset: 0x000CAE5C
		public static Rect Interpolate(Rect r1, Rect r2, float ratio)
		{
			return new Rect(Mathf.LerpUnclamped(r1.x, r2.x, ratio), Mathf.LerpUnclamped(r1.y, r2.y, ratio), Mathf.LerpUnclamped(r1.width, r2.width, ratio), Mathf.LerpUnclamped(r1.height, r2.height, ratio));
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000CCCC4 File Offset: 0x000CAEC4
		public static Color Interpolate(Color start, Color end, float ratio)
		{
			return Color.LerpUnclamped(start, end, ratio);
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000CCCE0 File Offset: 0x000CAEE0
		public static Vector2 Interpolate(Vector2 start, Vector2 end, float ratio)
		{
			return Vector2.LerpUnclamped(start, end, ratio);
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000CCCFC File Offset: 0x000CAEFC
		public static Vector3 Interpolate(Vector3 start, Vector3 end, float ratio)
		{
			return Vector3.LerpUnclamped(start, end, ratio);
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000CCD18 File Offset: 0x000CAF18
		public static Quaternion Interpolate(Quaternion start, Quaternion end, float ratio)
		{
			return Quaternion.SlerpUnclamped(start, end, ratio);
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000CCD34 File Offset: 0x000CAF34
		internal static StyleValues Interpolate(StyleValues start, StyleValues end, float ratio)
		{
			StyleValues styleValues = default(StyleValues);
			bool flag = end.m_StyleValues != null;
			if (flag)
			{
				foreach (StyleValue styleValue in end.m_StyleValues.m_Values)
				{
					StyleValue styleValue2 = default(StyleValue);
					bool flag2 = !start.m_StyleValues.TryGetStyleValue(styleValue.id, ref styleValue2);
					if (flag2)
					{
						throw new ArgumentException("Start StyleValues must contain the same values as end values. Missing property:" + styleValue.id.ToString());
					}
					StylePropertyId id = styleValue.id;
					StylePropertyId stylePropertyId = id;
					if (stylePropertyId <= StylePropertyId.UnityTextAlign)
					{
						if (stylePropertyId <= StylePropertyId.Color)
						{
							if (stylePropertyId - StylePropertyId.Custom <= 1)
							{
								goto IL_02A9;
							}
							if (stylePropertyId != StylePropertyId.Color)
							{
								goto IL_02A9;
							}
							goto IL_0284;
						}
						else
						{
							if (stylePropertyId == StylePropertyId.FontSize)
							{
								goto IL_025F;
							}
							switch (stylePropertyId)
							{
							case StylePropertyId.UnityFont:
							case StylePropertyId.UnityFontDefinition:
							case StylePropertyId.UnityFontStyleAndWeight:
							case StylePropertyId.UnityParagraphSpacing:
							case StylePropertyId.UnityTextAlign:
								goto IL_02A9;
							default:
								goto IL_02A9;
							}
						}
					}
					else if (stylePropertyId <= StylePropertyId.Width)
					{
						if (stylePropertyId - StylePropertyId.Visibility <= 1)
						{
							goto IL_02A9;
						}
						switch (stylePropertyId)
						{
						case StylePropertyId.AlignContent:
						case StylePropertyId.AlignItems:
						case StylePropertyId.AlignSelf:
						case StylePropertyId.Display:
						case StylePropertyId.FlexDirection:
						case StylePropertyId.FlexWrap:
						case StylePropertyId.JustifyContent:
						case StylePropertyId.Position:
							goto IL_02A9;
						case StylePropertyId.BorderBottomWidth:
						case StylePropertyId.BorderLeftWidth:
						case StylePropertyId.BorderRightWidth:
						case StylePropertyId.BorderTopWidth:
						case StylePropertyId.Bottom:
						case StylePropertyId.FlexBasis:
						case StylePropertyId.FlexGrow:
						case StylePropertyId.FlexShrink:
						case StylePropertyId.Height:
						case StylePropertyId.Left:
						case StylePropertyId.MarginBottom:
						case StylePropertyId.MarginLeft:
						case StylePropertyId.MarginRight:
						case StylePropertyId.MarginTop:
						case StylePropertyId.MaxHeight:
						case StylePropertyId.MaxWidth:
						case StylePropertyId.MinHeight:
						case StylePropertyId.MinWidth:
						case StylePropertyId.PaddingBottom:
						case StylePropertyId.PaddingLeft:
						case StylePropertyId.PaddingRight:
						case StylePropertyId.PaddingTop:
						case StylePropertyId.Right:
						case StylePropertyId.Top:
						case StylePropertyId.Width:
							goto IL_025F;
						default:
							goto IL_02A9;
						}
					}
					else
					{
						switch (stylePropertyId)
						{
						case StylePropertyId.Cursor:
						case StylePropertyId.TextOverflow:
						case StylePropertyId.UnityOverflowClipBox:
						case StylePropertyId.UnitySliceBottom:
						case StylePropertyId.UnitySliceLeft:
						case StylePropertyId.UnitySliceRight:
						case StylePropertyId.UnitySliceScale:
						case StylePropertyId.UnitySliceTop:
						case StylePropertyId.UnitySliceType:
						case StylePropertyId.UnityTextOverflowPosition:
							goto IL_02A9;
						case StylePropertyId.UnityBackgroundImageTintColor:
							goto IL_0284;
						default:
							switch (stylePropertyId)
							{
							case StylePropertyId.BackgroundPosition:
							case StylePropertyId.BorderRadius:
							case StylePropertyId.BorderWidth:
							case StylePropertyId.Flex:
							case StylePropertyId.Margin:
							case StylePropertyId.Padding:
							case StylePropertyId.Transition:
							case StylePropertyId.UnityBackgroundScaleMode:
								goto IL_02A9;
							case StylePropertyId.BorderColor:
								goto IL_0284;
							default:
								switch (stylePropertyId)
								{
								case StylePropertyId.BackgroundColor:
									goto IL_0284;
								case StylePropertyId.BackgroundImage:
								case StylePropertyId.BackgroundPositionX:
								case StylePropertyId.BackgroundPositionY:
								case StylePropertyId.BackgroundRepeat:
								case StylePropertyId.BackgroundSize:
								case StylePropertyId.BorderBottomColor:
								case StylePropertyId.BorderLeftColor:
								case StylePropertyId.BorderRightColor:
								case StylePropertyId.BorderTopColor:
								case StylePropertyId.Overflow:
									goto IL_02A9;
								case StylePropertyId.BorderBottomLeftRadius:
								case StylePropertyId.BorderBottomRightRadius:
								case StylePropertyId.BorderTopLeftRadius:
								case StylePropertyId.BorderTopRightRadius:
								case StylePropertyId.Opacity:
									goto IL_025F;
								default:
									goto IL_02A9;
								}
								break;
							}
							break;
						}
					}
					continue;
					IL_025F:
					styleValues.SetValue(styleValue.id, Lerp.Interpolate(styleValue2.number, styleValue.number, ratio));
					continue;
					IL_0284:
					styleValues.SetValue(styleValue.id, Lerp.Interpolate(styleValue2.color, styleValue.color, ratio));
					continue;
					IL_02A9:
					throw new ArgumentException("Style Value can't be animated");
				}
			}
			return styleValues;
		}
	}
}
