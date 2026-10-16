using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006DB RID: 1755
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class StylePropertyReader
	{
		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x060032B4 RID: 12980 RVA: 0x000C31EB File Offset: 0x000C13EB
		// (set) Token: 0x060032B5 RID: 12981 RVA: 0x000C31F3 File Offset: 0x000C13F3
		public StyleProperty property { get; private set; }

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x060032B6 RID: 12982 RVA: 0x000C31FC File Offset: 0x000C13FC
		// (set) Token: 0x060032B7 RID: 12983 RVA: 0x000C3204 File Offset: 0x000C1404
		public StylePropertyId propertyId { get; private set; }

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x060032B8 RID: 12984 RVA: 0x000C320D File Offset: 0x000C140D
		// (set) Token: 0x060032B9 RID: 12985 RVA: 0x000C3215 File Offset: 0x000C1415
		public int valueCount { get; private set; }

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x060032BA RID: 12986 RVA: 0x000C321E File Offset: 0x000C141E
		// (set) Token: 0x060032BB RID: 12987 RVA: 0x000C3226 File Offset: 0x000C1426
		public float dpiScaling { get; private set; }

		// Token: 0x060032BC RID: 12988 RVA: 0x000C3230 File Offset: 0x000C1430
		public void SetContext(StyleSheet sheet, StyleComplexSelector selector, StyleVariableContext varContext, float dpiScaling = 1f)
		{
			this.m_Sheet = sheet;
			this.m_Properties = selector.rule.properties;
			this.m_PropertyIds = StyleSheetCache.GetPropertyIds(sheet, selector.ruleIndex);
			this.m_Resolver.variableContext = varContext;
			this.dpiScaling = dpiScaling;
			this.LoadProperties();
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000C3285 File Offset: 0x000C1485
		public void SetInlineContext(StyleSheet sheet, StyleProperty[] properties, StylePropertyId[] propertyIds, float dpiScaling = 1f)
		{
			this.m_Sheet = sheet;
			this.m_Properties = properties;
			this.m_PropertyIds = propertyIds;
			this.dpiScaling = dpiScaling;
			this.LoadProperties();
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000C32B0 File Offset: 0x000C14B0
		public StylePropertyId MoveNextProperty()
		{
			this.m_CurrentPropertyIndex++;
			this.m_CurrentValueIndex += this.valueCount;
			this.SetCurrentProperty();
			return this.propertyId;
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x000C32F0 File Offset: 0x000C14F0
		public StylePropertyValue GetValue(int index)
		{
			return this.m_Values[this.m_CurrentValueIndex + index];
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x000C3318 File Offset: 0x000C1518
		public StyleValueType GetValueType(int index)
		{
			return this.m_Values[this.m_CurrentValueIndex + index].handle.valueType;
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x000C334C File Offset: 0x000C154C
		public bool IsValueType(int index, StyleValueType type)
		{
			return this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == type;
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x000C3384 File Offset: 0x000C1584
		public bool IsKeyword(int index, StyleValueKeyword keyword)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.handle.valueType == StyleValueType.Keyword && stylePropertyValue.handle.valueIndex == (int)keyword;
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x000C33CC File Offset: 0x000C15CC
		public string ReadAsString(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x000C3404 File Offset: 0x000C1604
		public Length ReadLength(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Keyword;
			Length length;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
				StyleValueKeyword styleValueKeyword = valueIndex;
				StyleValueKeyword styleValueKeyword2 = styleValueKeyword;
				if (styleValueKeyword2 != StyleValueKeyword.Auto)
				{
					if (styleValueKeyword2 != StyleValueKeyword.None)
					{
						length = default(Length);
					}
					else
					{
						length = Length.None();
					}
				}
				else
				{
					length = Length.Auto();
				}
			}
			else
			{
				length = stylePropertyValue.sheet.ReadDimension(stylePropertyValue.handle).ToLength();
			}
			return length;
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000C349C File Offset: 0x000C169C
		public TimeValue ReadTimeValue(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.sheet.ReadDimension(stylePropertyValue.handle).ToTime();
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000C34DC File Offset: 0x000C16DC
		public Translate ReadTranslate(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue stylePropertyValue2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			StylePropertyValue stylePropertyValue3 = ((this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue));
			return StylePropertyReader.ReadTranslate(this.valueCount, stylePropertyValue, stylePropertyValue2, stylePropertyValue3);
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000C3568 File Offset: 0x000C1768
		public TransformOrigin ReadTransformOrigin(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue stylePropertyValue2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			StylePropertyValue stylePropertyValue3 = ((this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue));
			return StylePropertyReader.ReadTransformOrigin(this.valueCount, stylePropertyValue, stylePropertyValue2, stylePropertyValue3);
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000C35F4 File Offset: 0x000C17F4
		public Rotate ReadRotate(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue stylePropertyValue2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			StylePropertyValue stylePropertyValue3 = ((this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue));
			StylePropertyValue stylePropertyValue4 = ((this.valueCount > 3) ? this.m_Values[this.m_CurrentValueIndex + index + 3] : default(StylePropertyValue));
			return StylePropertyReader.ReadRotate(this.valueCount, stylePropertyValue, stylePropertyValue2, stylePropertyValue3, stylePropertyValue4);
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000C36AC File Offset: 0x000C18AC
		public Scale ReadScale(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue stylePropertyValue2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			StylePropertyValue stylePropertyValue3 = ((this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue));
			return StylePropertyReader.ReadScale(this.valueCount, stylePropertyValue, stylePropertyValue2, stylePropertyValue3);
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000C3738 File Offset: 0x000C1938
		public float ReadFloat(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.sheet.ReadFloat(stylePropertyValue.handle);
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000C3770 File Offset: 0x000C1970
		public int ReadInt(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return (int)stylePropertyValue.sheet.ReadFloat(stylePropertyValue.handle);
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000C37A8 File Offset: 0x000C19A8
		public Color ReadColor(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			Color color = Color.clear;
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Enum;
			if (flag)
			{
				string text = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
				StyleSheetColor.TryGetColor(text.ToLowerInvariant(), out color);
			}
			else
			{
				color = stylePropertyValue.sheet.ReadColor(stylePropertyValue.handle);
			}
			return color;
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000C3824 File Offset: 0x000C1A24
		public int ReadEnum(StyleEnumType enumType, int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueHandle handle = stylePropertyValue.handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			string text;
			if (flag)
			{
				StyleValueKeyword styleValueKeyword = stylePropertyValue.sheet.ReadKeyword(handle);
				text = styleValueKeyword.ToUssString();
			}
			else
			{
				text = stylePropertyValue.sheet.ReadEnum(handle);
			}
			int num;
			StylePropertyUtil.TryGetEnumIntValue(enumType, text, out num);
			return num;
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000C389C File Offset: 0x000C1A9C
		public FontDefinition ReadFontDefinition(int index)
		{
			FontAsset fontAsset = null;
			Font font = null;
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueType valueType = stylePropertyValue.handle.valueType;
			StyleValueType styleValueType = valueType;
			if (styleValueType != StyleValueType.Keyword)
			{
				if (styleValueType != StyleValueType.ResourcePath)
				{
					if (styleValueType != StyleValueType.AssetReference)
					{
						Debug.LogWarning("Invalid value for font " + stylePropertyValue.handle.valueType.ToString());
					}
					else
					{
						font = stylePropertyValue.sheet.ReadAssetReference(stylePropertyValue.handle) as Font;
						bool flag = font == null;
						if (flag)
						{
							fontAsset = stylePropertyValue.sheet.ReadAssetReference(stylePropertyValue.handle) as FontAsset;
						}
					}
				}
				else
				{
					string text = stylePropertyValue.sheet.ReadResourcePath(stylePropertyValue.handle);
					bool flag2 = !string.IsNullOrEmpty(text);
					if (flag2)
					{
						font = Panel.LoadResource(text, typeof(Font), this.dpiScaling) as Font;
						bool flag3 = font == null;
						if (flag3)
						{
							fontAsset = Panel.LoadResource(text, typeof(FontAsset), this.dpiScaling) as FontAsset;
						}
					}
					bool flag4 = fontAsset == null && font == null;
					if (flag4)
					{
						Debug.LogWarning(string.Format("Font not found for path: {0}", text));
					}
				}
			}
			else
			{
				bool flag5 = stylePropertyValue.handle.valueIndex != 6;
				if (flag5)
				{
					string text2 = "Invalid keyword for font ";
					StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
					Debug.LogWarning(text2 + valueIndex.ToString());
				}
			}
			bool flag6 = font != null;
			FontDefinition fontDefinition;
			if (flag6)
			{
				fontDefinition = FontDefinition.FromFont(font);
			}
			else
			{
				bool flag7 = fontAsset != null;
				if (flag7)
				{
					fontDefinition = FontDefinition.FromSDFFont(fontAsset);
				}
				else
				{
					fontDefinition = default(FontDefinition);
				}
			}
			return fontDefinition;
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000C3A7C File Offset: 0x000C1C7C
		public Font ReadFont(int index)
		{
			Font font = null;
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueType valueType = stylePropertyValue.handle.valueType;
			StyleValueType styleValueType = valueType;
			if (styleValueType != StyleValueType.Keyword)
			{
				if (styleValueType != StyleValueType.ResourcePath)
				{
					if (styleValueType != StyleValueType.AssetReference)
					{
						Debug.LogWarning("Invalid value for font " + stylePropertyValue.handle.valueType.ToString());
					}
					else
					{
						font = stylePropertyValue.sheet.ReadAssetReference(stylePropertyValue.handle) as Font;
					}
				}
				else
				{
					string text = stylePropertyValue.sheet.ReadResourcePath(stylePropertyValue.handle);
					bool flag = !string.IsNullOrEmpty(text);
					if (flag)
					{
						font = Panel.LoadResource(text, typeof(Font), this.dpiScaling) as Font;
					}
					bool flag2 = font == null;
					if (flag2)
					{
						Debug.LogWarning(string.Format("Font not found for path: {0}", text));
					}
				}
			}
			else
			{
				bool flag3 = stylePropertyValue.handle.valueIndex != 6;
				if (flag3)
				{
					string text2 = "Invalid keyword for font ";
					StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
					Debug.LogWarning(text2 + valueIndex.ToString());
				}
			}
			return font;
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000C3BC0 File Offset: 0x000C1DC0
		public Background ReadBackground(int index)
		{
			ImageSource imageSource = default(ImageSource);
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Keyword;
			if (flag)
			{
				bool flag2 = stylePropertyValue.handle.valueIndex != 6;
				if (flag2)
				{
					string text = "Invalid keyword for image source ";
					StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
					Debug.LogWarning(text + valueIndex.ToString());
				}
			}
			else
			{
				bool flag3 = !StylePropertyReader.TryGetImageSourceFromValue(stylePropertyValue, this.dpiScaling, out imageSource);
				if (flag3)
				{
				}
			}
			bool flag4 = imageSource.texture != null;
			Background background;
			if (flag4)
			{
				background = Background.FromTexture2D(imageSource.texture);
			}
			else
			{
				bool flag5 = imageSource.sprite != null;
				if (flag5)
				{
					background = Background.FromSprite(imageSource.sprite);
				}
				else
				{
					bool flag6 = imageSource.vectorImage != null;
					if (flag6)
					{
						background = Background.FromVectorImage(imageSource.vectorImage);
					}
					else
					{
						bool flag7 = imageSource.renderTexture != null;
						if (flag7)
						{
							background = Background.FromRenderTexture(imageSource.renderTexture);
						}
						else
						{
							background = default(Background);
						}
					}
				}
			}
			return background;
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000C3CF0 File Offset: 0x000C1EF0
		public Cursor ReadCursor(int index)
		{
			float num = 0f;
			float num2 = 0f;
			int num3 = 0;
			Texture2D texture2D = null;
			StyleValueType valueType = this.GetValueType(index);
			bool flag = valueType == StyleValueType.ResourcePath || valueType == StyleValueType.AssetReference || valueType == StyleValueType.ScalableImage || valueType == StyleValueType.MissingAssetReference;
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = this.valueCount < 1;
				if (flag3)
				{
					Debug.LogWarning(string.Format("USS 'cursor' has invalid value at {0}.", index));
				}
				else
				{
					ImageSource imageSource = default(ImageSource);
					StylePropertyValue value = this.GetValue(index);
					bool flag4 = StylePropertyReader.TryGetImageSourceFromValue(value, this.dpiScaling, out imageSource);
					if (flag4)
					{
						texture2D = imageSource.texture;
						bool flag5 = this.valueCount >= 3;
						if (flag5)
						{
							StylePropertyValue value2 = this.GetValue(index + 1);
							StylePropertyValue value3 = this.GetValue(index + 2);
							bool flag6 = value2.handle.valueType != StyleValueType.Float || value3.handle.valueType != StyleValueType.Float;
							if (flag6)
							{
								Debug.LogWarning("USS 'cursor' property requires two integers for the hot spot value.");
							}
							else
							{
								num = value2.sheet.ReadFloat(value2.handle);
								num2 = value3.sheet.ReadFloat(value3.handle);
							}
						}
					}
				}
			}
			else
			{
				bool flag7 = StylePropertyReader.getCursorIdFunc != null;
				if (flag7)
				{
					StylePropertyValue value4 = this.GetValue(index);
					num3 = StylePropertyReader.getCursorIdFunc(value4.sheet, value4.handle);
				}
			}
			return new Cursor
			{
				texture = texture2D,
				hotspot = new Vector2(num, num2),
				defaultCursorId = num3
			};
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000C3E94 File Offset: 0x000C2094
		public TextShadow ReadTextShadow(int index)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			Color color = Color.clear;
			bool flag = this.valueCount >= 2;
			if (flag)
			{
				int num4 = index;
				StyleValueType styleValueType = this.GetValueType(num4);
				bool flag2 = false;
				bool flag3 = styleValueType == StyleValueType.Color || styleValueType == StyleValueType.Enum;
				if (flag3)
				{
					color = this.ReadColor(num4++);
					flag2 = true;
				}
				bool flag4 = num4 + 1 < this.valueCount;
				if (flag4)
				{
					styleValueType = this.GetValueType(num4);
					StyleValueType valueType = this.GetValueType(num4 + 1);
					bool flag5 = (styleValueType == StyleValueType.Dimension || styleValueType == StyleValueType.Float) && (valueType == StyleValueType.Dimension || valueType == StyleValueType.Float);
					if (flag5)
					{
						StylePropertyValue value = this.GetValue(num4++);
						StylePropertyValue value2 = this.GetValue(num4++);
						num = value.sheet.ReadDimension(value.handle).value;
						num2 = value2.sheet.ReadDimension(value2.handle).value;
					}
				}
				bool flag6 = num4 < this.valueCount;
				if (flag6)
				{
					styleValueType = this.GetValueType(num4);
					bool flag7 = styleValueType == StyleValueType.Dimension || styleValueType == StyleValueType.Float;
					if (flag7)
					{
						StylePropertyValue value3 = this.GetValue(num4++);
						num3 = value3.sheet.ReadDimension(value3.handle).value;
					}
					else
					{
						bool flag8 = styleValueType == StyleValueType.Color || styleValueType == StyleValueType.Enum;
						if (flag8)
						{
							bool flag9 = !flag2;
							if (flag9)
							{
								color = this.ReadColor(num4);
							}
						}
					}
				}
				bool flag10 = num4 < this.valueCount;
				if (flag10)
				{
					styleValueType = this.GetValueType(num4);
					bool flag11 = styleValueType == StyleValueType.Color || styleValueType == StyleValueType.Enum;
					if (flag11)
					{
						bool flag12 = !flag2;
						if (flag12)
						{
							color = this.ReadColor(num4);
						}
					}
				}
			}
			return new TextShadow
			{
				offset = new Vector2(num, num2),
				blurRadius = num3,
				color = color
			};
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000C40A4 File Offset: 0x000C22A4
		public BackgroundPosition ReadBackgroundPositionX(int index)
		{
			return this.ReadBackgroundPosition(index, BackgroundPositionKeyword.Left);
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000C40C0 File Offset: 0x000C22C0
		public BackgroundPosition ReadBackgroundPositionY(int index)
		{
			return this.ReadBackgroundPosition(index, BackgroundPositionKeyword.Top);
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000C40DC File Offset: 0x000C22DC
		private BackgroundPosition ReadBackgroundPosition(int index, BackgroundPositionKeyword keyword)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue stylePropertyValue2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			return StylePropertyReader.ReadBackgroundPosition(this.valueCount, stylePropertyValue, stylePropertyValue2, keyword);
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000C413C File Offset: 0x000C233C
		public BackgroundRepeat ReadBackgroundRepeat(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue stylePropertyValue2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			return StylePropertyReader.ReadBackgroundRepeat(this.valueCount, stylePropertyValue, stylePropertyValue2);
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000C419C File Offset: 0x000C239C
		public BackgroundSize ReadBackgroundSize(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue stylePropertyValue2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			return StylePropertyReader.ReadBackgroundSize(this.valueCount, stylePropertyValue, stylePropertyValue2);
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000C41FC File Offset: 0x000C23FC
		public void ReadListEasingFunction(List<EasingFunction> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
				StyleValueHandle handle = stylePropertyValue.handle;
				bool flag = handle.valueType == StyleValueType.Enum;
				if (flag)
				{
					string text = stylePropertyValue.sheet.ReadEnum(handle);
					int num;
					StylePropertyUtil.TryGetEnumIntValue(StyleEnumType.EasingMode, text, out num);
					list.Add(new EasingFunction((EasingMode)num));
					index++;
				}
				bool flag2 = index < this.valueCount;
				if (flag2)
				{
					bool flag3 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag3)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000C42BC File Offset: 0x000C24BC
		public void ReadListTimeValue(List<TimeValue> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
				TimeValue timeValue = stylePropertyValue.sheet.ReadDimension(stylePropertyValue.handle).ToTime();
				list.Add(timeValue);
				index++;
				bool flag = index < this.valueCount;
				if (flag)
				{
					bool flag2 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag2)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000C4360 File Offset: 0x000C2560
		public void ReadListStylePropertyName(List<StylePropertyName> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
				string text = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
				list.Add(new StylePropertyName(text));
				index++;
				bool flag = index < this.valueCount;
				if (flag)
				{
					bool flag2 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag2)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000C4400 File Offset: 0x000C2600
		public void ReadListString(List<string> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
				string text = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
				list.Add(text);
				index++;
				bool flag = index < this.valueCount;
				if (flag)
				{
					bool flag2 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag2)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000C449C File Offset: 0x000C269C
		private void LoadProperties()
		{
			this.m_CurrentPropertyIndex = 0;
			this.m_CurrentValueIndex = 0;
			this.m_Values.Clear();
			this.m_ValueCount.Clear();
			foreach (StyleProperty styleProperty in this.m_Properties)
			{
				int num = 0;
				bool flag = true;
				bool requireVariableResolve = styleProperty.requireVariableResolve;
				if (requireVariableResolve)
				{
					this.m_Resolver.Init(styleProperty, this.m_Sheet, styleProperty.values);
					int num2 = 0;
					while (num2 < styleProperty.values.Length && flag)
					{
						StyleValueHandle styleValueHandle = styleProperty.values[num2];
						bool flag2 = styleValueHandle.IsVarFunction();
						if (flag2)
						{
							flag = this.m_Resolver.ResolveVarFunction(ref num2);
						}
						else
						{
							this.m_Resolver.AddValue(styleValueHandle);
						}
						num2++;
					}
					bool flag3 = flag && this.m_Resolver.ValidateResolvedValues();
					if (flag3)
					{
						this.m_Values.AddRange(this.m_Resolver.resolvedValues);
						num += this.m_Resolver.resolvedValues.Count;
					}
					else
					{
						StyleValueHandle styleValueHandle2 = new StyleValueHandle
						{
							valueType = StyleValueType.Keyword,
							valueIndex = 3
						};
						this.m_Values.Add(new StylePropertyValue
						{
							sheet = this.m_Sheet,
							handle = styleValueHandle2
						});
						num++;
					}
				}
				else
				{
					num = styleProperty.values.Length;
					for (int j = 0; j < num; j++)
					{
						this.m_Values.Add(new StylePropertyValue
						{
							sheet = this.m_Sheet,
							handle = styleProperty.values[j]
						});
					}
				}
				this.m_ValueCount.Add(num);
			}
			this.SetCurrentProperty();
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000C4688 File Offset: 0x000C2888
		private void SetCurrentProperty()
		{
			bool flag = this.m_CurrentPropertyIndex < this.m_PropertyIds.Length;
			if (flag)
			{
				this.property = this.m_Properties[this.m_CurrentPropertyIndex];
				this.propertyId = this.m_PropertyIds[this.m_CurrentPropertyIndex];
				this.valueCount = this.m_ValueCount[this.m_CurrentPropertyIndex];
			}
			else
			{
				this.property = null;
				this.propertyId = StylePropertyId.Unknown;
				this.valueCount = 0;
			}
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000C4708 File Offset: 0x000C2908
		public static TransformOrigin ReadTransformOrigin(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue zVvalue)
		{
			Length length = Length.Percent(50f);
			Length length2 = Length.Percent(50f);
			float num = 0f;
			switch (valCount)
			{
			case 1:
			{
				bool flag;
				bool flag2;
				Length length3 = StylePropertyReader.ReadTransformOriginEnum(val1, out flag, out flag2);
				bool flag3 = flag2;
				if (flag3)
				{
					length = length3;
				}
				else
				{
					length2 = length3;
				}
				goto IL_00F3;
			}
			case 2:
				break;
			case 3:
			{
				bool flag4 = zVvalue.handle.valueType == StyleValueType.Dimension || zVvalue.handle.valueType == StyleValueType.Float;
				if (flag4)
				{
					Dimension dimension = zVvalue.sheet.ReadDimension(zVvalue.handle);
					num = dimension.value;
				}
				break;
			}
			default:
				goto IL_00F3;
			}
			bool flag5;
			bool flag6;
			Length length4 = StylePropertyReader.ReadTransformOriginEnum(val1, out flag5, out flag6);
			bool flag7;
			bool flag8;
			Length length5 = StylePropertyReader.ReadTransformOriginEnum(val2, out flag7, out flag8);
			bool flag9 = !flag6 || !flag7;
			if (flag9)
			{
				bool flag10 = flag8 && flag5;
				if (flag10)
				{
					length = length5;
					length2 = length4;
				}
			}
			else
			{
				length = length4;
				length2 = length5;
			}
			IL_00F3:
			return new TransformOrigin(length, length2, num);
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000C4818 File Offset: 0x000C2A18
		private static Length ReadTransformOriginEnum(StylePropertyValue value, out bool isVertical, out bool isHorizontal)
		{
			bool flag = value.handle.valueType == StyleValueType.Enum;
			if (flag)
			{
				switch (StylePropertyReader.ReadEnum(StyleEnumType.TransformOriginOffset, value))
				{
				case 1:
					isVertical = false;
					isHorizontal = true;
					return Length.Percent(0f);
				case 2:
					isVertical = false;
					isHorizontal = true;
					return Length.Percent(100f);
				case 3:
					isVertical = true;
					isHorizontal = false;
					return Length.Percent(0f);
				case 4:
					isVertical = true;
					isHorizontal = false;
					return Length.Percent(100f);
				case 5:
					isVertical = true;
					isHorizontal = true;
					return Length.Percent(50f);
				}
			}
			else
			{
				bool flag2 = value.handle.valueType == StyleValueType.Dimension || value.handle.valueType == StyleValueType.Float;
				if (flag2)
				{
					isVertical = true;
					isHorizontal = true;
					return value.sheet.ReadDimension(value.handle).ToLength();
				}
			}
			isVertical = false;
			isHorizontal = false;
			return Length.Percent(50f);
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000C493C File Offset: 0x000C2B3C
		public static Translate ReadTranslate(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue val3)
		{
			bool flag = val1.handle.valueType == StyleValueType.Keyword && val1.handle.valueIndex == 6;
			Translate translate;
			if (flag)
			{
				translate = Translate.None();
			}
			else
			{
				Length length = 0f;
				Length length2 = 0f;
				float num = 0f;
				switch (valCount)
				{
				case 1:
				{
					bool flag2 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
					if (flag2)
					{
						length = val1.sheet.ReadDimension(val1.handle).ToLength();
						length2 = val1.sheet.ReadDimension(val1.handle).ToLength();
					}
					goto IL_01A6;
				}
				case 2:
					break;
				case 3:
				{
					bool flag3 = val3.handle.valueType == StyleValueType.Dimension || val3.handle.valueType == StyleValueType.Float;
					if (flag3)
					{
						Dimension dimension = val3.sheet.ReadDimension(val3.handle);
						num = dimension.value;
					}
					break;
				}
				default:
					goto IL_01A6;
				}
				bool flag4 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
				if (flag4)
				{
					length = val1.sheet.ReadDimension(val1.handle).ToLength();
				}
				bool flag5 = val2.handle.valueType == StyleValueType.Dimension || val2.handle.valueType == StyleValueType.Float;
				if (flag5)
				{
					length2 = val2.sheet.ReadDimension(val2.handle).ToLength();
				}
				IL_01A6:
				translate = new Translate(length, length2, num);
			}
			return translate;
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000C4B00 File Offset: 0x000C2D00
		public static Scale ReadScale(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue val3)
		{
			bool flag = val1.handle.valueType == StyleValueType.Keyword && val1.handle.valueIndex == 6;
			Scale scale;
			if (flag)
			{
				scale = Scale.None();
			}
			else
			{
				Vector3 one = Vector3.one;
				switch (valCount)
				{
				case 1:
				{
					bool flag2 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
					if (flag2)
					{
						one.x = val1.sheet.ReadFloat(val1.handle);
						one.y = one.x;
					}
					goto IL_0173;
				}
				case 2:
					break;
				case 3:
				{
					bool flag3 = val3.handle.valueType == StyleValueType.Dimension || val3.handle.valueType == StyleValueType.Float;
					if (flag3)
					{
						one.z = val3.sheet.ReadFloat(val3.handle);
					}
					break;
				}
				default:
					goto IL_0173;
				}
				bool flag4 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
				if (flag4)
				{
					one.x = val1.sheet.ReadFloat(val1.handle);
				}
				bool flag5 = val2.handle.valueType == StyleValueType.Dimension || val2.handle.valueType == StyleValueType.Float;
				if (flag5)
				{
					one.y = val2.sheet.ReadFloat(val2.handle);
				}
				IL_0173:
				scale = new Scale(one);
			}
			return scale;
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000C4C8C File Offset: 0x000C2E8C
		public static Rotate ReadRotate(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue val3, StylePropertyValue val4)
		{
			bool flag = val1.handle.valueType == StyleValueType.Keyword && val1.handle.valueIndex == 6;
			Rotate rotate;
			if (flag)
			{
				rotate = Rotate.None();
			}
			else
			{
				Rotate rotate2 = Rotate.Initial();
				switch (valCount)
				{
				case 1:
				{
					bool flag2 = val1.handle.valueType == StyleValueType.Dimension;
					if (flag2)
					{
						rotate2.angle = StylePropertyReader.ReadAngle(val1);
					}
					break;
				}
				case 2:
					rotate2.angle = StylePropertyReader.ReadAngle(val2);
					switch (StylePropertyReader.ReadEnum(StyleEnumType.Axis, val1))
					{
					case 0:
						rotate2.axis = new Vector3(1f, 0f, 0f);
						break;
					case 1:
						rotate2.axis = new Vector3(0f, 1f, 0f);
						break;
					case 2:
						rotate2.axis = new Vector3(0f, 0f, 1f);
						break;
					}
					break;
				case 4:
					rotate2.angle = StylePropertyReader.ReadAngle(val4);
					rotate2.axis = new Vector3(val1.sheet.ReadFloat(val1.handle), val1.sheet.ReadFloat(val2.handle), val1.sheet.ReadFloat(val3.handle));
					break;
				}
				rotate = rotate2;
			}
			return rotate;
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000C4E04 File Offset: 0x000C3004
		private static bool TryReadEnum(StyleEnumType enumType, StylePropertyValue value, out int intValue)
		{
			StyleValueHandle handle = value.handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			string text;
			if (flag)
			{
				StyleValueKeyword styleValueKeyword = value.sheet.ReadKeyword(handle);
				text = styleValueKeyword.ToUssString();
			}
			else
			{
				text = value.sheet.ReadEnum(handle);
			}
			return StylePropertyUtil.TryGetEnumIntValue(enumType, text, out intValue);
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000C4E60 File Offset: 0x000C3060
		private static int ReadEnum(StyleEnumType enumType, StylePropertyValue value)
		{
			StyleValueHandle handle = value.handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			string text;
			if (flag)
			{
				StyleValueKeyword styleValueKeyword = value.sheet.ReadKeyword(handle);
				text = styleValueKeyword.ToUssString();
			}
			else
			{
				text = value.sheet.ReadEnum(handle);
			}
			int num;
			StylePropertyUtil.TryGetEnumIntValue(enumType, text, out num);
			return num;
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000C4EC0 File Offset: 0x000C30C0
		public static Angle ReadAngle(StylePropertyValue value)
		{
			bool flag = value.handle.valueType == StyleValueType.Keyword;
			Angle angle;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)value.handle.valueIndex;
				StyleValueKeyword styleValueKeyword = valueIndex;
				StyleValueKeyword styleValueKeyword2 = styleValueKeyword;
				if (styleValueKeyword2 != StyleValueKeyword.None)
				{
					angle = default(Angle);
				}
				else
				{
					angle = Angle.None();
				}
			}
			else
			{
				angle = value.sheet.ReadDimension(value.handle).ToAngle();
			}
			return angle;
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000C4F30 File Offset: 0x000C3130
		public static BackgroundPosition ReadBackgroundPosition(int valCount, StylePropertyValue val1, StylePropertyValue val2, BackgroundPositionKeyword keyword)
		{
			bool flag = valCount == 1;
			if (flag)
			{
				bool flag2 = val1.handle.valueType == StyleValueType.Enum;
				if (flag2)
				{
					return new BackgroundPosition((BackgroundPositionKeyword)StylePropertyReader.ReadEnum(StyleEnumType.BackgroundPositionKeyword, val1));
				}
				bool flag3 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
				if (flag3)
				{
					return new BackgroundPosition(keyword, val1.sheet.ReadDimension(val1.handle).ToLength());
				}
			}
			else
			{
				bool flag4 = valCount == 2;
				if (flag4)
				{
					bool flag5 = val1.handle.valueType == StyleValueType.Enum && (val2.handle.valueType == StyleValueType.Dimension || val2.handle.valueType == StyleValueType.Float);
					if (flag5)
					{
						return new BackgroundPosition((BackgroundPositionKeyword)StylePropertyReader.ReadEnum(StyleEnumType.BackgroundPositionKeyword, val1), val1.sheet.ReadDimension(val2.handle).ToLength());
					}
				}
			}
			return default(BackgroundPosition);
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000C5038 File Offset: 0x000C3238
		public static BackgroundRepeat ReadBackgroundRepeat(int valCount, StylePropertyValue val1, StylePropertyValue val2)
		{
			BackgroundRepeat backgroundRepeat = default(BackgroundRepeat);
			bool flag = valCount == 1;
			if (flag)
			{
				int num;
				bool flag2 = StylePropertyReader.TryReadEnum(StyleEnumType.RepeatXY, val1, out num);
				if (flag2)
				{
					bool flag3 = num == 0;
					if (flag3)
					{
						backgroundRepeat.x = Repeat.Repeat;
						backgroundRepeat.y = Repeat.NoRepeat;
					}
					else
					{
						bool flag4 = num == 1;
						if (flag4)
						{
							backgroundRepeat.x = Repeat.NoRepeat;
							backgroundRepeat.y = Repeat.Repeat;
						}
					}
				}
				else
				{
					backgroundRepeat.x = (Repeat)StylePropertyReader.ReadEnum(StyleEnumType.Repeat, val1);
					backgroundRepeat.y = backgroundRepeat.x;
				}
			}
			else
			{
				backgroundRepeat.x = (Repeat)StylePropertyReader.ReadEnum(StyleEnumType.Repeat, val1);
				backgroundRepeat.y = (Repeat)StylePropertyReader.ReadEnum(StyleEnumType.Repeat, val2);
			}
			return backgroundRepeat;
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000C50EC File Offset: 0x000C32EC
		public static BackgroundSize ReadBackgroundSize(int valCount, StylePropertyValue val1, StylePropertyValue val2)
		{
			BackgroundSize backgroundSize = default(BackgroundSize);
			bool flag = valCount == 1;
			if (flag)
			{
				bool flag2 = val1.handle.valueType == StyleValueType.Keyword;
				if (flag2)
				{
					bool flag3 = val1.handle.valueIndex == 2;
					if (flag3)
					{
						backgroundSize.x = Length.Auto();
						backgroundSize.y = Length.Auto();
					}
					else
					{
						bool flag4 = val1.handle.valueIndex == 7;
						if (flag4)
						{
							backgroundSize.sizeType = BackgroundSizeType.Cover;
						}
						else
						{
							bool flag5 = val1.handle.valueIndex == 8;
							if (flag5)
							{
								backgroundSize.sizeType = BackgroundSizeType.Contain;
							}
						}
					}
				}
				else
				{
					bool flag6 = val1.handle.valueType == StyleValueType.Enum;
					if (flag6)
					{
						backgroundSize.sizeType = (BackgroundSizeType)StylePropertyReader.ReadEnum(StyleEnumType.BackgroundSizeType, val1);
					}
					else
					{
						bool flag7 = val1.handle.valueType == StyleValueType.Dimension;
						if (flag7)
						{
							backgroundSize.x = val1.sheet.ReadDimension(val1.handle).ToLength();
							backgroundSize.y = Length.Auto();
						}
					}
				}
			}
			else
			{
				bool flag8 = valCount == 2;
				if (flag8)
				{
					bool flag9 = val1.handle.valueType == StyleValueType.Keyword;
					if (flag9)
					{
						bool flag10 = val1.handle.valueIndex == 2;
						if (flag10)
						{
							backgroundSize.x = Length.Auto();
						}
					}
					else
					{
						bool flag11 = val1.handle.valueType == StyleValueType.Dimension;
						if (flag11)
						{
							backgroundSize.x = val1.sheet.ReadDimension(val1.handle).ToLength();
						}
					}
					bool flag12 = val2.handle.valueType == StyleValueType.Keyword;
					if (flag12)
					{
						bool flag13 = val2.handle.valueIndex == 2;
						if (flag13)
						{
							backgroundSize.y = Length.Auto();
						}
					}
					else
					{
						bool flag14 = val2.handle.valueType == StyleValueType.Dimension;
						if (flag14)
						{
							backgroundSize.y = val2.sheet.ReadDimension(val2.handle).ToLength();
						}
					}
				}
			}
			return backgroundSize;
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000C530C File Offset: 0x000C350C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static bool TryGetImageSourceFromValue(StylePropertyValue propertyValue, float dpiScaling, out ImageSource source)
		{
			source = default(ImageSource);
			StyleValueType valueType = propertyValue.handle.valueType;
			StyleValueType styleValueType = valueType;
			if (styleValueType <= StyleValueType.AssetReference)
			{
				if (styleValueType != StyleValueType.ResourcePath)
				{
					if (styleValueType == StyleValueType.AssetReference)
					{
						Object @object = propertyValue.sheet.ReadAssetReference(propertyValue.handle);
						source.texture = @object as Texture2D;
						source.sprite = @object as Sprite;
						source.vectorImage = @object as VectorImage;
						source.renderTexture = @object as RenderTexture;
						bool flag = source.IsNull();
						if (flag)
						{
							Debug.LogWarning("Invalid image specified");
							return false;
						}
						goto IL_0254;
					}
				}
				else
				{
					string text = propertyValue.sheet.ReadResourcePath(propertyValue.handle);
					bool flag2 = !string.IsNullOrEmpty(text);
					if (flag2)
					{
						source.sprite = Panel.LoadResource(text, typeof(Sprite), dpiScaling) as Sprite;
						bool flag3 = source.IsNull();
						if (flag3)
						{
							source.texture = Panel.LoadResource(text, typeof(Texture2D), dpiScaling) as Texture2D;
						}
						bool flag4 = source.IsNull();
						if (flag4)
						{
							source.vectorImage = Panel.LoadResource(text, typeof(VectorImage), dpiScaling) as VectorImage;
						}
						bool flag5 = source.IsNull();
						if (flag5)
						{
							source.renderTexture = Panel.LoadResource(text, typeof(RenderTexture), dpiScaling) as RenderTexture;
						}
					}
					bool flag6 = source.IsNull();
					if (flag6)
					{
						Debug.LogWarning(string.Format("Image not found for path: {0}", text));
						return false;
					}
					goto IL_0254;
				}
			}
			else if (styleValueType != StyleValueType.ScalableImage)
			{
				if (styleValueType == StyleValueType.MissingAssetReference)
				{
					return false;
				}
			}
			else
			{
				ScalableImage scalableImage = propertyValue.sheet.ReadScalableImage(propertyValue.handle);
				bool flag7 = scalableImage.normalImage == null && scalableImage.highResolutionImage == null;
				if (flag7)
				{
					Debug.LogWarning("Invalid scalable image specified");
					return false;
				}
				source.texture = scalableImage.normalImage;
				bool flag8 = !Mathf.Approximately(dpiScaling % 1f, 0f);
				if (flag8)
				{
					source.texture.filterMode = FilterMode.Bilinear;
				}
				goto IL_0254;
			}
			Debug.LogWarning("Invalid value for image texture " + propertyValue.handle.valueType.ToString());
			return false;
			IL_0254:
			return true;
		}

		// Token: 0x040019DC RID: 6620
		internal static StylePropertyReader.GetCursorIdFunction getCursorIdFunc;

		// Token: 0x040019DD RID: 6621
		private List<StylePropertyValue> m_Values = new List<StylePropertyValue>();

		// Token: 0x040019DE RID: 6622
		private List<int> m_ValueCount = new List<int>();

		// Token: 0x040019DF RID: 6623
		private StyleVariableResolver m_Resolver = new StyleVariableResolver();

		// Token: 0x040019E0 RID: 6624
		private StyleSheet m_Sheet;

		// Token: 0x040019E1 RID: 6625
		private StyleProperty[] m_Properties;

		// Token: 0x040019E2 RID: 6626
		private StylePropertyId[] m_PropertyIds;

		// Token: 0x040019E3 RID: 6627
		private int m_CurrentValueIndex;

		// Token: 0x040019E4 RID: 6628
		private int m_CurrentPropertyIndex;

		// Token: 0x020006DC RID: 1756
		// (Invoke) Token: 0x060032EC RID: 13036
		internal delegate int GetCursorIdFunction(StyleSheet sheet, StyleValueHandle handle);
	}
}
