using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x0200014D RID: 333
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x00020AA9 File Offset: 0x0001ECA9
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00020AC4 File Offset: 0x0001ECC4
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return this.m_compareInfo.Compare(text, text2);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x040005A0 RID: 1440
		private CompareInfo m_compareInfo;

		// Token: 0x040005A1 RID: 1441
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}
