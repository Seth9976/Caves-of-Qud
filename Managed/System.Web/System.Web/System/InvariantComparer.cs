using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002F27 File Offset: 0x00001127
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F40 File Offset: 0x00001140
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

		// Token: 0x04000D48 RID: 3400
		private CompareInfo m_compareInfo;

		// Token: 0x04000D49 RID: 3401
		internal static readonly global::System.InvariantComparer Default = new global::System.InvariantComparer();
	}
}
