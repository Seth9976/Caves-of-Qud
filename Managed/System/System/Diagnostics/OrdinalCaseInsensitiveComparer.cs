using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x02000245 RID: 581
	internal class OrdinalCaseInsensitiveComparer : IComparer
	{
		// Token: 0x060011CC RID: 4556 RVA: 0x0004DCAC File Offset: 0x0004BEAC
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return string.Compare(text, text2, StringComparison.OrdinalIgnoreCase);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x04000A8F RID: 2703
		internal static readonly OrdinalCaseInsensitiveComparer Default = new OrdinalCaseInsensitiveComparer();
	}
}
