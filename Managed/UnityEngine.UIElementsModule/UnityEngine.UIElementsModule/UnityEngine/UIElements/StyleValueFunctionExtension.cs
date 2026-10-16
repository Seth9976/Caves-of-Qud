using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004D5 RID: 1237
	internal static class StyleValueFunctionExtension
	{
		// Token: 0x060025E9 RID: 9705 RVA: 0x0008971C File Offset: 0x0008791C
		public static StyleValueFunction FromUssString(string ussValue)
		{
			ussValue = ussValue.ToLowerInvariant();
			string text = ussValue;
			string text2 = text;
			StyleValueFunction styleValueFunction;
			if (!(text2 == "var"))
			{
				if (!(text2 == "env"))
				{
					if (!(text2 == "linear-gradient"))
					{
						throw new ArgumentOutOfRangeException("ussValue", ussValue, "Unknown function name");
					}
					styleValueFunction = StyleValueFunction.LinearGradient;
				}
				else
				{
					styleValueFunction = StyleValueFunction.Env;
				}
			}
			else
			{
				styleValueFunction = StyleValueFunction.Var;
			}
			return styleValueFunction;
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x00089780 File Offset: 0x00087980
		public static string ToUssString(this StyleValueFunction svf)
		{
			string text;
			switch (svf)
			{
			case StyleValueFunction.Var:
				text = "var";
				break;
			case StyleValueFunction.Env:
				text = "env";
				break;
			case StyleValueFunction.LinearGradient:
				text = "linear-gradient";
				break;
			default:
				throw new ArgumentOutOfRangeException("svf", svf, "Unknown StyleValueFunction");
			}
			return text;
		}

		// Token: 0x040010F8 RID: 4344
		public const string k_Var = "var";

		// Token: 0x040010F9 RID: 4345
		public const string k_Env = "env";

		// Token: 0x040010FA RID: 4346
		public const string k_LinearGradient = "linear-gradient";
	}
}
