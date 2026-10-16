using System;

namespace System.Collections.Generic
{
	// Token: 0x02000ACE RID: 2766
	[Serializable]
	internal sealed class InternalStringComparer : EqualityComparer<string>
	{
		// Token: 0x060062C4 RID: 25284 RVA: 0x0014A570 File Offset: 0x00148770
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x060062C5 RID: 25285 RVA: 0x0014A57D File Offset: 0x0014877D
		public override bool Equals(string x, string y)
		{
			if (x == null)
			{
				return y == null;
			}
			return x == y || x.Equals(y);
		}

		// Token: 0x060062C6 RID: 25286 RVA: 0x0014A594 File Offset: 0x00148794
		internal override int IndexOf(string[] array, string value, int startIndex, int count)
		{
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (Array.UnsafeLoad<string>(array, i) == value)
				{
					return i;
				}
			}
			return -1;
		}
	}
}
