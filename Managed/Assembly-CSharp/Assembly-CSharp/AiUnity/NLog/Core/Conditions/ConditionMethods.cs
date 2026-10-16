using System;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002A8 RID: 680
	[ConditionMethods]
	public static class ConditionMethods
	{
		// Token: 0x060017F6 RID: 6134 RVA: 0x0007D59C File Offset: 0x0007B79C
		[ConditionMethod("equals")]
		public static bool Equals2(object firstValue, object secondValue)
		{
			return firstValue.Equals(secondValue);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0007D5A8 File Offset: 0x0007B7A8
		[ConditionMethod("strequals")]
		public static bool Equals2(string firstValue, string secondValue, bool ignoreCase = false)
		{
			return firstValue.Equals(secondValue, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x0007D5C8 File Offset: 0x0007B7C8
		[ConditionMethod("contains")]
		public static bool Contains(string haystack, string needle, bool ignoreCase = true)
		{
			return haystack.IndexOf(needle, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0;
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x0007D5EC File Offset: 0x0007B7EC
		[ConditionMethod("starts-with")]
		public static bool StartsWith(string haystack, string needle, bool ignoreCase = true)
		{
			return haystack.StartsWith(needle, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0007D60C File Offset: 0x0007B80C
		[ConditionMethod("ends-with")]
		public static bool EndsWith(string haystack, string needle, bool ignoreCase = true)
		{
			return haystack.EndsWith(needle, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0007D629 File Offset: 0x0007B829
		[ConditionMethod("length")]
		public static int Length(string text)
		{
			return text.Length;
		}
	}
}
