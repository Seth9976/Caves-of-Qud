using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020002ED RID: 749
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEditor.UIBuilderModule" })]
	internal static class UINumericFieldsUtils
	{
		// Token: 0x060024CF RID: 9423 RVA: 0x00046F58 File Offset: 0x00045158
		public static bool TryConvertStringToDouble(string str, out double value)
		{
			ExpressionEvaluator.Expression expression;
			return UINumericFieldsUtils.TryConvertStringToDouble(str, out value, out expression);
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x00046F74 File Offset: 0x00045174
		public static bool TryConvertStringToDouble(string str, out double value, out ExpressionEvaluator.Expression expr)
		{
			expr = null;
			string text = str.ToLower();
			string text2 = text;
			string text3 = text2;
			if (!(text3 == "inf") && !(text3 == "infinity"))
			{
				if (!(text3 == "-inf") && !(text3 == "-infinity"))
				{
					if (!(text3 == "nan"))
					{
						return ExpressionEvaluator.Evaluate<double>(str, out value, out expr);
					}
					value = double.NaN;
				}
				else
				{
					value = double.NegativeInfinity;
				}
			}
			else
			{
				value = double.PositiveInfinity;
			}
			return true;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x0004700C File Offset: 0x0004520C
		public static bool TryConvertStringToDouble(string str, string initialValueAsString, out double value, out ExpressionEvaluator.Expression expression)
		{
			bool flag = UINumericFieldsUtils.TryConvertStringToDouble(str, out value, out expression);
			bool flag2 = !flag && expression != null && !string.IsNullOrEmpty(initialValueAsString);
			if (flag2)
			{
				double num;
				ExpressionEvaluator.Expression expression2;
				bool flag3 = UINumericFieldsUtils.TryConvertStringToDouble(initialValueAsString, out num, out expression2);
				if (flag3)
				{
					value = num;
					flag = expression.Evaluate<double>(ref value, 0, 1);
				}
			}
			return flag;
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x00047064 File Offset: 0x00045264
		public static bool TryConvertStringToFloat(string str, string initialValueAsString, out float value, out ExpressionEvaluator.Expression expression)
		{
			double num;
			bool flag = UINumericFieldsUtils.TryConvertStringToDouble(str, initialValueAsString, out num, out expression);
			value = Mathf.ClampToFloat(num);
			return flag;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x0004708C File Offset: 0x0004528C
		public static bool TryConvertStringToLong(string str, out long value)
		{
			ExpressionEvaluator.Expression expression;
			return ExpressionEvaluator.Evaluate<long>(str, out value, out expression);
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000470A8 File Offset: 0x000452A8
		public static bool TryConvertStringToLong(string str, out long value, out ExpressionEvaluator.Expression expr)
		{
			return ExpressionEvaluator.Evaluate<long>(str, out value, out expr);
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000470C4 File Offset: 0x000452C4
		public static bool TryConvertStringToLong(string str, string initialValueAsString, out long value, out ExpressionEvaluator.Expression expression)
		{
			bool flag = UINumericFieldsUtils.TryConvertStringToLong(str, out value, out expression);
			bool flag2 = !flag && expression != null && !string.IsNullOrEmpty(initialValueAsString);
			if (flag2)
			{
				long num;
				ExpressionEvaluator.Expression expression2;
				bool flag3 = UINumericFieldsUtils.TryConvertStringToLong(initialValueAsString, out num, out expression2);
				if (flag3)
				{
					value = num;
					flag = expression.Evaluate<long>(ref value, 0, 1);
				}
			}
			return flag;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x0004711C File Offset: 0x0004531C
		public static bool TryConvertStringToULong(string str, out ulong value, out ExpressionEvaluator.Expression expr)
		{
			return ExpressionEvaluator.Evaluate<ulong>(str, out value, out expr);
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x00047138 File Offset: 0x00045338
		public static bool TryConvertStringToULong(string str, string initialValueAsString, out ulong value, out ExpressionEvaluator.Expression expression)
		{
			bool flag = UINumericFieldsUtils.TryConvertStringToULong(str, out value, out expression);
			bool flag2 = !flag && expression != null && !string.IsNullOrEmpty(initialValueAsString);
			if (flag2)
			{
				ulong num;
				ExpressionEvaluator.Expression expression2;
				bool flag3 = UINumericFieldsUtils.TryConvertStringToULong(initialValueAsString, out num, out expression2);
				if (flag3)
				{
					value = num;
					flag = expression.Evaluate<ulong>(ref value, 0, 1);
				}
			}
			return flag;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x00047190 File Offset: 0x00045390
		public static bool TryConvertStringToInt(string str, string initialValueAsString, out int value, out ExpressionEvaluator.Expression expression)
		{
			long num;
			bool flag = UINumericFieldsUtils.TryConvertStringToLong(str, initialValueAsString, out num, out expression);
			value = Mathf.ClampToInt(num);
			return flag;
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000471B8 File Offset: 0x000453B8
		public static bool TryConvertStringToUInt(string str, string initialValueAsString, out uint value, out ExpressionEvaluator.Expression expression)
		{
			long num;
			bool flag = UINumericFieldsUtils.TryConvertStringToLong(str, initialValueAsString, out num, out expression);
			value = Mathf.ClampToUInt(num);
			return flag;
		}

		// Token: 0x04000A72 RID: 2674
		public static readonly string k_AllowedCharactersForFloat = "inftynaeINFTYNAE0123456789.,-*/+%^()cosqrludxvRL=pP#";

		// Token: 0x04000A73 RID: 2675
		public static readonly string k_AllowedCharactersForInt = "0123456789-*/+%^()cosintaqrtelfundxvRL,=pPI#";

		// Token: 0x04000A74 RID: 2676
		public static readonly string k_DoubleFieldFormatString = "R";

		// Token: 0x04000A75 RID: 2677
		public static readonly string k_FloatFieldFormatString = "g7";

		// Token: 0x04000A76 RID: 2678
		public static readonly string k_IntFieldFormatString = "#######0";
	}
}
