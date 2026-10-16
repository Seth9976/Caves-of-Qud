using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006F0 RID: 1776
	internal class StylePropertyValueMatcher : BaseStyleMatcher
	{
		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600335A RID: 13146 RVA: 0x000C8030 File Offset: 0x000C6230
		private StylePropertyValue current
		{
			get
			{
				return base.hasCurrent ? this.m_Values[base.currentIndex] : default(StylePropertyValue);
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x000C8061 File Offset: 0x000C6261
		public override int valueCount
		{
			get
			{
				return this.m_Values.Count;
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x00011DCA File Offset: 0x0000FFCA
		public override bool isCurrentVariable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x0600335D RID: 13149 RVA: 0x000C8070 File Offset: 0x000C6270
		public override bool isCurrentComma
		{
			get
			{
				return base.hasCurrent && this.m_Values[base.currentIndex].handle.valueType == StyleValueType.CommaSeparator;
			}
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x000C80AC File Offset: 0x000C62AC
		public MatchResult Match(Expression exp, List<StylePropertyValue> values)
		{
			MatchResult matchResult = new MatchResult
			{
				errorCode = MatchResultErrorCode.None
			};
			bool flag = values == null || values.Count == 0;
			MatchResult matchResult2;
			if (flag)
			{
				matchResult.errorCode = MatchResultErrorCode.EmptyValue;
				matchResult2 = matchResult;
			}
			else
			{
				base.Initialize();
				this.m_Values = values;
				StyleValueHandle handle = this.m_Values[0].handle;
				bool flag2 = handle.valueType == StyleValueType.Keyword && handle.valueIndex == 1;
				bool flag3;
				if (flag2)
				{
					base.MoveNext();
					flag3 = true;
				}
				else
				{
					flag3 = base.Match(exp);
				}
				bool flag4 = !flag3;
				if (flag4)
				{
					StyleSheet sheet = this.current.sheet;
					matchResult.errorCode = MatchResultErrorCode.Syntax;
					matchResult.errorValue = sheet.ReadAsString(this.current.handle);
				}
				else
				{
					bool hasCurrent = base.hasCurrent;
					if (hasCurrent)
					{
						StyleSheet sheet2 = this.current.sheet;
						matchResult.errorCode = MatchResultErrorCode.ExpectedEndOfValue;
						matchResult.errorValue = sheet2.ReadAsString(this.current.handle);
					}
				}
				matchResult2 = matchResult;
			}
			return matchResult2;
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x000C81C8 File Offset: 0x000C63C8
		protected override bool MatchKeyword(string keyword)
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Keyword;
			bool flag2;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)current.handle.valueIndex;
				flag2 = valueIndex.ToUssString() == keyword.ToLowerInvariant();
			}
			else
			{
				bool flag3 = current.handle.valueType == StyleValueType.Enum;
				if (flag3)
				{
					string text = current.sheet.ReadEnum(current.handle);
					flag2 = text == keyword.ToLowerInvariant();
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000C8250 File Offset: 0x000C6450
		protected override bool MatchNumber()
		{
			return this.current.handle.valueType == StyleValueType.Float;
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000C8278 File Offset: 0x000C6478
		protected override bool MatchInteger()
		{
			return this.current.handle.valueType == StyleValueType.Float;
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000C82A0 File Offset: 0x000C64A0
		protected override bool MatchLength()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			bool flag2;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				flag2 = dimension.unit == Dimension.Unit.Pixel;
			}
			else
			{
				bool flag3 = current.handle.valueType == StyleValueType.Float;
				if (flag3)
				{
					float num = current.sheet.ReadFloat(current.handle);
					flag2 = Mathf.Approximately(0f, num);
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x000C8324 File Offset: 0x000C6524
		protected override bool MatchPercentage()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			bool flag2;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				flag2 = dimension.unit == Dimension.Unit.Percent;
			}
			else
			{
				bool flag3 = current.handle.valueType == StyleValueType.Float;
				if (flag3)
				{
					float num = current.sheet.ReadFloat(current.handle);
					flag2 = Mathf.Approximately(0f, num);
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000C83A8 File Offset: 0x000C65A8
		protected override bool MatchColor()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Color;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = current.handle.valueType == StyleValueType.Enum;
				if (flag3)
				{
					Color clear = Color.clear;
					string text = current.sheet.ReadAsString(current.handle);
					bool flag4 = StyleSheetColor.TryGetColor(text.ToLowerInvariant(), out clear);
					if (flag4)
					{
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000C8420 File Offset: 0x000C6620
		protected override bool MatchResource()
		{
			return this.current.handle.valueType == StyleValueType.ResourcePath;
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000C8448 File Offset: 0x000C6648
		protected override bool MatchUrl()
		{
			StyleValueType valueType = this.current.handle.valueType;
			return valueType == StyleValueType.AssetReference || valueType == StyleValueType.ScalableImage;
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000C8480 File Offset: 0x000C6680
		protected override bool MatchTime()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			bool flag2;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				flag2 = dimension.unit == Dimension.Unit.Second || dimension.unit == Dimension.Unit.Millisecond;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x000C84D8 File Offset: 0x000C66D8
		protected override bool MatchCustomIdent()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Enum;
			bool flag2;
			if (flag)
			{
				string text = current.sheet.ReadAsString(current.handle);
				Match match = BaseStyleMatcher.s_CustomIdentRegex.Match(text);
				flag2 = match.Success && match.Length == text.Length;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x000C8544 File Offset: 0x000C6744
		protected override bool MatchAngle()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				Dimension.Unit unit = dimension.unit;
				Dimension.Unit unit2 = unit;
				if (unit2 - Dimension.Unit.Degree <= 3)
				{
					return true;
				}
			}
			bool flag2 = current.handle.valueType == StyleValueType.Float;
			bool flag3;
			if (flag2)
			{
				float num = current.sheet.ReadFloat(current.handle);
				flag3 = Mathf.Approximately(0f, num);
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x04001A32 RID: 6706
		private List<StylePropertyValue> m_Values;
	}
}
