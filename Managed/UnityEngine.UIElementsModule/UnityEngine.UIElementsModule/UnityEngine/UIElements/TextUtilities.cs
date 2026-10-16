using System;
using UnityEngine.TextCore;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020004F1 RID: 1265
	internal static class TextUtilities
	{
		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060026AB RID: 9899 RVA: 0x0008DED8 File Offset: 0x0008C0D8
		public static TextSettings textSettings
		{
			get
			{
				bool flag = TextUtilities.s_TextSettings == null;
				if (flag)
				{
					TextUtilities.s_TextSettings = TextUtilities.getEditorTextSettings();
				}
				return TextUtilities.s_TextSettings;
			}
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x0008DF10 File Offset: 0x0008C110
		internal static Vector2 MeasureVisualElementTextSize(TextElement te, in RenderedText textToMeasure, float width, VisualElement.MeasureMode widthMode, float height, VisualElement.MeasureMode heightMode)
		{
			float num = float.NaN;
			float num2 = float.NaN;
			bool flag = !TextUtilities.IsFontAssigned(te);
			Vector2 vector;
			if (flag)
			{
				vector = new Vector2(num, num2);
			}
			else
			{
				float num3 = 1f;
				bool flag2 = te.panel != null;
				if (flag2)
				{
					num3 = te.scaledPixelsPerPoint;
				}
				bool flag3 = num3 <= 0f;
				if (flag3)
				{
					vector = Vector2.zero;
				}
				else
				{
					bool flag4 = widthMode != VisualElement.MeasureMode.Exactly || heightMode != VisualElement.MeasureMode.Exactly;
					if (flag4)
					{
						Vector2 vector2 = te.uitkTextHandle.ComputeTextSize(in textToMeasure, width, height);
						num = vector2.x;
						num2 = vector2.y;
					}
					bool flag5 = widthMode == VisualElement.MeasureMode.Exactly;
					if (flag5)
					{
						num = width;
					}
					else
					{
						bool flag6 = widthMode == VisualElement.MeasureMode.AtMost;
						if (flag6)
						{
							num = Mathf.Min(num, width);
						}
					}
					bool flag7 = heightMode == VisualElement.MeasureMode.Exactly;
					if (flag7)
					{
						num2 = height;
					}
					else
					{
						bool flag8 = heightMode == VisualElement.MeasureMode.AtMost;
						if (flag8)
						{
							num2 = Mathf.Min(num2, height);
						}
					}
					float num4 = AlignmentUtils.CeilToPixelGrid(num, num3, 0f);
					float num5 = AlignmentUtils.CeilToPixelGrid(num2, num3, 0f);
					Vector2 vector3 = new Vector2(num4, num5);
					bool flag9 = TextUtilities.IsAdvancedTextEnabledForElement(te);
					if (flag9)
					{
						te.uitkTextHandle.ATGMeasuredSizes = new Vector2(num, num2);
						te.uitkTextHandle.ATGRoundedSizes = vector3;
					}
					else
					{
						te.uitkTextHandle.MeasuredSizes = new Vector2(num, num2);
						te.uitkTextHandle.RoundedSizes = vector3;
					}
					vector = vector3;
				}
			}
			return vector;
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x0008E08C File Offset: 0x0008C28C
		internal static FontAsset GetFontAsset(VisualElement ve)
		{
			bool flag = ve.computedStyle.unityFontDefinition.fontAsset != null;
			FontAsset fontAsset;
			if (flag)
			{
				fontAsset = ve.computedStyle.unityFontDefinition.fontAsset;
			}
			else
			{
				TextSettings textSettingsFrom = TextUtilities.GetTextSettingsFrom(ve);
				bool flag2 = !object.Equals(ve.computedStyle.unityFontDefinition.font, null);
				if (flag2)
				{
					fontAsset = textSettingsFrom.GetCachedFontAsset(ve.computedStyle.unityFontDefinition.font);
				}
				else
				{
					bool flag3 = !object.Equals(ve.computedStyle.unityFont, null);
					if (flag3)
					{
						fontAsset = textSettingsFrom.GetCachedFontAsset(ve.computedStyle.unityFont);
					}
					else
					{
						bool flag4 = !object.Equals(textSettingsFrom, null);
						if (flag4)
						{
							fontAsset = textSettingsFrom.defaultFontAsset;
						}
						else
						{
							fontAsset = null;
						}
					}
				}
			}
			return fontAsset;
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x0008E164 File Offset: 0x0008C364
		internal static bool IsFontAssigned(VisualElement ve)
		{
			return ve.computedStyle.unityFont != null || !ve.computedStyle.unityFontDefinition.IsEmpty();
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0008E1A4 File Offset: 0x0008C3A4
		internal static TextSettings GetTextSettingsFrom(VisualElement ve)
		{
			RuntimePanel runtimePanel = ve.panel as RuntimePanel;
			bool flag = runtimePanel != null;
			TextSettings textSettings;
			if (flag)
			{
				textSettings = runtimePanel.panelSettings.textSettings ?? PanelTextSettings.defaultPanelTextSettings;
			}
			else
			{
				textSettings = PanelTextSettings.defaultPanelTextSettings;
			}
			return textSettings;
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x0008E1E8 File Offset: 0x0008C3E8
		internal static bool IsAdvancedTextEnabledForElement(VisualElement ve)
		{
			bool flag = ve == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = ve.computedStyle.unityTextGenerator == TextGeneratorType.Advanced;
				bool flag4 = false;
				bool flag5 = ve.panel == null;
				if (flag5)
				{
					flag2 = false;
				}
				else
				{
					RuntimePanel runtimePanel = ve.panel as RuntimePanel;
					bool flag6 = runtimePanel != null;
					if (flag6)
					{
						PanelSettings panelSettings = runtimePanel.panelSettings;
						flag4 = ((panelSettings != null) ? panelSettings.m_ICUDataAsset : null) != null;
					}
					flag2 = flag3 && flag4;
				}
			}
			return flag2;
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x0008E260 File Offset: 0x0008C460
		internal unsafe static TextCoreSettings GetTextCoreSettingsForElement(VisualElement ve, bool ignoreColors)
		{
			FontAsset fontAsset = TextUtilities.GetFontAsset(ve);
			bool flag = fontAsset == null;
			TextCoreSettings textCoreSettings;
			if (flag)
			{
				textCoreSettings = default(TextCoreSettings);
			}
			else
			{
				IResolvedStyle resolvedStyle = ve.resolvedStyle;
				ComputedStyle computedStyle = *ve.computedStyle;
				TextShadow textShadow = computedStyle.textShadow;
				float num = TextHandle.ConvertPixelUnitsToTextCoreRelativeUnits(computedStyle.fontSize.value, fontAsset);
				float num2 = Mathf.Clamp(resolvedStyle.unityTextOutlineWidth * num, 0f, 1f);
				float num3 = Mathf.Clamp(textShadow.blurRadius * num, 0f, 1f);
				float num4 = ((textShadow.offset.x < 0f) ? Mathf.Max(textShadow.offset.x * num, -1f) : Mathf.Min(textShadow.offset.x * num, 1f));
				float num5 = ((textShadow.offset.y < 0f) ? Mathf.Max(textShadow.offset.y * num, -1f) : Mathf.Min(textShadow.offset.y * num, 1f));
				Vector2 vector = new Vector2(num4, num5);
				Color color;
				Color color3;
				if (ignoreColors)
				{
					color = Color.white;
					Color color2 = Color.white;
					color3 = Color.white;
				}
				else
				{
					bool flag2 = ((Texture2D)fontAsset.material.mainTexture).format != TextureFormat.Alpha8;
					color = resolvedStyle.color;
					color3 = resolvedStyle.unityTextOutlineColor;
					bool flag3 = num2 < 1E-30f;
					if (flag3)
					{
						color3.a = 0f;
					}
					Color color2 = textShadow.color;
					bool flag4 = flag2;
					if (flag4)
					{
						color = new Color(1f, 1f, 1f, color.a);
					}
					else
					{
						color2.r *= color.a;
						color2.g *= color.a;
						color2.b *= color.a;
						color3.r *= color3.a;
						color3.g *= color3.a;
						color3.b *= color3.a;
					}
				}
				textCoreSettings = new TextCoreSettings
				{
					faceColor = color,
					outlineColor = color3,
					outlineWidth = num2,
					underlayColor = textShadow.color,
					underlayOffset = vector,
					underlaySoftness = num3
				};
			}
			return textCoreSettings;
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x0008E4E8 File Offset: 0x0008C6E8
		public static TextWrappingMode toTextWrappingMode(this WhiteSpace whiteSpace, bool isSingleLineInputField)
		{
			TextWrappingMode textWrappingMode2;
			if (isSingleLineInputField)
			{
				if (!true)
				{
				}
				TextWrappingMode textWrappingMode;
				if (whiteSpace > WhiteSpace.NoWrap)
				{
					if (whiteSpace - WhiteSpace.Pre > 1)
					{
						textWrappingMode = TextWrappingMode.NoWrap;
					}
					else
					{
						textWrappingMode = TextWrappingMode.PreserveWhitespaceNoWrap;
					}
				}
				else
				{
					textWrappingMode = TextWrappingMode.NoWrap;
				}
				if (!true)
				{
				}
				textWrappingMode2 = textWrappingMode;
			}
			else
			{
				if (!true)
				{
				}
				TextWrappingMode textWrappingMode;
				switch (whiteSpace)
				{
				case WhiteSpace.Normal:
					textWrappingMode = TextWrappingMode.Normal;
					break;
				case WhiteSpace.NoWrap:
					textWrappingMode = TextWrappingMode.NoWrap;
					break;
				case WhiteSpace.Pre:
					textWrappingMode = TextWrappingMode.PreserveWhitespaceNoWrap;
					break;
				case WhiteSpace.PreWrap:
					textWrappingMode = TextWrappingMode.PreserveWhitespace;
					break;
				default:
					textWrappingMode = TextWrappingMode.Normal;
					break;
				}
				if (!true)
				{
				}
				textWrappingMode2 = textWrappingMode;
			}
			return textWrappingMode2;
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x0008E55C File Offset: 0x0008C75C
		public static WhiteSpace toTextCore(this WhiteSpace whiteSpace, bool isInputField)
		{
			WhiteSpace whiteSpace3;
			if (isInputField)
			{
				if (!true)
				{
				}
				WhiteSpace whiteSpace2;
				switch (whiteSpace)
				{
				case WhiteSpace.Normal:
				case WhiteSpace.PreWrap:
					whiteSpace2 = WhiteSpace.PreWrap;
					break;
				case WhiteSpace.NoWrap:
				case WhiteSpace.Pre:
					whiteSpace2 = WhiteSpace.Pre;
					break;
				default:
					whiteSpace2 = WhiteSpace.Pre;
					break;
				}
				if (!true)
				{
				}
				whiteSpace3 = whiteSpace2;
			}
			else
			{
				if (!true)
				{
				}
				WhiteSpace whiteSpace2;
				switch (whiteSpace)
				{
				case WhiteSpace.Normal:
					whiteSpace2 = WhiteSpace.Normal;
					break;
				case WhiteSpace.NoWrap:
					whiteSpace2 = WhiteSpace.NoWrap;
					break;
				case WhiteSpace.Pre:
					whiteSpace2 = WhiteSpace.Pre;
					break;
				case WhiteSpace.PreWrap:
					whiteSpace2 = WhiteSpace.PreWrap;
					break;
				default:
					whiteSpace2 = WhiteSpace.Normal;
					break;
				}
				if (!true)
				{
				}
				whiteSpace3 = whiteSpace2;
			}
			return whiteSpace3;
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x0008E5DC File Offset: 0x0008C7DC
		public static TextOverflow toTextCore(this TextOverflow textOverflow, OverflowInternal overflow)
		{
			if (!true)
			{
			}
			TextOverflow textOverflow2;
			if (textOverflow == TextOverflow.Ellipsis)
			{
				if (overflow == OverflowInternal.Hidden)
				{
					textOverflow2 = TextOverflow.Ellipsis;
					goto IL_001B;
				}
			}
			textOverflow2 = TextOverflow.Clip;
			IL_001B:
			if (!true)
			{
			}
			return textOverflow2;
		}

		// Token: 0x04001188 RID: 4488
		public static Func<TextSettings> getEditorTextSettings;

		// Token: 0x04001189 RID: 4489
		internal static Func<bool> IsAdvancedTextEnabled;

		// Token: 0x0400118A RID: 4490
		private static TextSettings s_TextSettings;
	}
}
