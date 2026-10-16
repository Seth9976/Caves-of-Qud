using System;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200007B RID: 123
	public class StringUtil
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x0000AB44 File Offset: 0x00008D44
		public static int Compare(string strA, string strB, bool ignoreCase)
		{
			StringComparison stringComparison = (ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture);
			return string.Compare(strA, strB, stringComparison);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000AB61 File Offset: 0x00008D61
		public static bool StringsEqual(string strA, string strB, bool ignoreCase)
		{
			return StringUtil.Compare(strA, strB, ignoreCase) == 0;
		}
	}
}
