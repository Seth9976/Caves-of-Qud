using System;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006EF RID: 1775
	internal class StyleMatcher : BaseStyleMatcher
	{
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x000C7AD2 File Offset: 0x000C5CD2
		private string current
		{
			get
			{
				return base.hasCurrent ? this.m_PropertyParts[base.currentIndex] : null;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x000C7AEC File Offset: 0x000C5CEC
		public override int valueCount
		{
			get
			{
				return this.m_PropertyParts.Length;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06003349 RID: 13129 RVA: 0x000C7AF6 File Offset: 0x000C5CF6
		public override bool isCurrentVariable
		{
			get
			{
				return base.hasCurrent && this.current.StartsWith("var(");
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x000C7B13 File Offset: 0x000C5D13
		public override bool isCurrentComma
		{
			get
			{
				return base.hasCurrent && this.current == ",";
			}
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000C7B30 File Offset: 0x000C5D30
		private void Initialize(string propertyValue)
		{
			base.Initialize();
			this.m_PropertyParts = this.m_Parser.Parse(propertyValue);
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000C7B4C File Offset: 0x000C5D4C
		public MatchResult Match(Expression exp, string propertyValue)
		{
			MatchResult matchResult = new MatchResult
			{
				errorCode = MatchResultErrorCode.None
			};
			bool flag = string.IsNullOrEmpty(propertyValue);
			MatchResult matchResult2;
			if (flag)
			{
				matchResult.errorCode = MatchResultErrorCode.EmptyValue;
				matchResult2 = matchResult;
			}
			else
			{
				this.Initialize(propertyValue);
				string current = this.current;
				bool flag2 = current == "initial" || current.StartsWith("env(");
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
					matchResult.errorCode = MatchResultErrorCode.Syntax;
					matchResult.errorValue = this.current;
				}
				else
				{
					bool hasCurrent = base.hasCurrent;
					if (hasCurrent)
					{
						matchResult.errorCode = MatchResultErrorCode.ExpectedEndOfValue;
						matchResult.errorValue = this.current;
					}
				}
				matchResult2 = matchResult;
			}
			return matchResult2;
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000C7C20 File Offset: 0x000C5E20
		protected override bool MatchKeyword(string keyword)
		{
			return string.Compare(this.current, keyword, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000C7C44 File Offset: 0x000C5E44
		protected override bool MatchNumber()
		{
			string current = this.current;
			Match match = StyleMatcher.s_NumberRegex.Match(current);
			return match.Success;
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000C7C70 File Offset: 0x000C5E70
		protected override bool MatchInteger()
		{
			string current = this.current;
			Match match = StyleMatcher.s_IntegerRegex.Match(current);
			return match.Success;
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x000C7C9C File Offset: 0x000C5E9C
		protected override bool MatchLength()
		{
			string current = this.current;
			Match match = StyleMatcher.s_LengthRegex.Match(current);
			bool success = match.Success;
			bool flag;
			if (success)
			{
				flag = true;
			}
			else
			{
				match = StyleMatcher.s_ZeroRegex.Match(current);
				flag = match.Success;
			}
			return flag;
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000C7CE4 File Offset: 0x000C5EE4
		protected override bool MatchPercentage()
		{
			string current = this.current;
			Match match = StyleMatcher.s_PercentRegex.Match(current);
			bool success = match.Success;
			bool flag;
			if (success)
			{
				flag = true;
			}
			else
			{
				match = StyleMatcher.s_ZeroRegex.Match(current);
				flag = match.Success;
			}
			return flag;
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000C7D2C File Offset: 0x000C5F2C
		protected override bool MatchColor()
		{
			string current = this.current;
			Match match = StyleMatcher.s_HexColorRegex.Match(current);
			bool success = match.Success;
			bool flag;
			if (success)
			{
				flag = true;
			}
			else
			{
				match = StyleMatcher.s_RgbRegex.Match(current);
				bool success2 = match.Success;
				if (success2)
				{
					flag = true;
				}
				else
				{
					match = StyleMatcher.s_RgbaRegex.Match(current);
					bool success3 = match.Success;
					if (success3)
					{
						flag = true;
					}
					else
					{
						Color clear = Color.clear;
						bool flag2 = StyleSheetColor.TryGetColor(current, out clear);
						flag = flag2;
					}
				}
			}
			return flag;
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000C7DB8 File Offset: 0x000C5FB8
		protected override bool MatchResource()
		{
			string current = this.current;
			Match match = StyleMatcher.s_ResourceRegex.Match(current);
			bool flag = !match.Success;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				string text = match.Groups[1].Value.Trim();
				match = StyleMatcher.s_VarFunctionRegex.Match(text);
				flag2 = !match.Success;
			}
			return flag2;
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000C7E20 File Offset: 0x000C6020
		protected override bool MatchUrl()
		{
			string current = this.current;
			Match match = StyleMatcher.s_UrlRegex.Match(current);
			bool flag = !match.Success;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				string text = match.Groups[1].Value.Trim();
				match = StyleMatcher.s_VarFunctionRegex.Match(text);
				flag2 = !match.Success;
			}
			return flag2;
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000C7E88 File Offset: 0x000C6088
		protected override bool MatchTime()
		{
			string current = this.current;
			Match match = StyleMatcher.s_TimeRegex.Match(current);
			return match.Success;
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x000C7EB4 File Offset: 0x000C60B4
		protected override bool MatchAngle()
		{
			string current = this.current;
			Match match = StyleMatcher.s_AngleRegex.Match(current);
			bool success = match.Success;
			bool flag;
			if (success)
			{
				flag = true;
			}
			else
			{
				match = StyleMatcher.s_ZeroRegex.Match(current);
				flag = match.Success;
			}
			return flag;
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x000C7EFC File Offset: 0x000C60FC
		protected override bool MatchCustomIdent()
		{
			string current = this.current;
			Match match = BaseStyleMatcher.s_CustomIdentRegex.Match(current);
			return match.Success && match.Length == current.Length;
		}

		// Token: 0x04001A23 RID: 6691
		private StylePropertyValueParser m_Parser = new StylePropertyValueParser();

		// Token: 0x04001A24 RID: 6692
		private string[] m_PropertyParts;

		// Token: 0x04001A25 RID: 6693
		private static readonly Regex s_NumberRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?$", RegexOptions.Compiled);

		// Token: 0x04001A26 RID: 6694
		private static readonly Regex s_IntegerRegex = new Regex("^[+-]?\\d+$", RegexOptions.Compiled);

		// Token: 0x04001A27 RID: 6695
		private static readonly Regex s_ZeroRegex = new Regex("^0(?:\\.0+)?$", RegexOptions.Compiled);

		// Token: 0x04001A28 RID: 6696
		private static readonly Regex s_LengthRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?(?:px)$", RegexOptions.Compiled);

		// Token: 0x04001A29 RID: 6697
		private static readonly Regex s_PercentRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?(?:%)$", RegexOptions.Compiled);

		// Token: 0x04001A2A RID: 6698
		private static readonly Regex s_HexColorRegex = new Regex("^#[a-fA-F0-9]{3}(?:[a-fA-F0-9]{3})?$", RegexOptions.Compiled);

		// Token: 0x04001A2B RID: 6699
		private static readonly Regex s_RgbRegex = new Regex("^rgb\\(\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*(\\d+)\\s*\\)$", RegexOptions.Compiled);

		// Token: 0x04001A2C RID: 6700
		private static readonly Regex s_RgbaRegex = new Regex("rgba\\(\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*([\\d.]+)\\s*\\)$", RegexOptions.Compiled);

		// Token: 0x04001A2D RID: 6701
		private static readonly Regex s_VarFunctionRegex = new Regex("^var\\(.+\\)$", RegexOptions.Compiled);

		// Token: 0x04001A2E RID: 6702
		private static readonly Regex s_ResourceRegex = new Regex("^resource\\((.+)\\)$", RegexOptions.Compiled);

		// Token: 0x04001A2F RID: 6703
		private static readonly Regex s_UrlRegex = new Regex("^url\\((.+)\\)$", RegexOptions.Compiled);

		// Token: 0x04001A30 RID: 6704
		private static readonly Regex s_TimeRegex = new Regex("^[+-]?\\.?\\d+(?:\\.\\d+)?(?:s|ms)$", RegexOptions.Compiled);

		// Token: 0x04001A31 RID: 6705
		private static readonly Regex s_AngleRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?(?:deg|grad|rad|turn)$", RegexOptions.Compiled);
	}
}
