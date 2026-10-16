using System;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x020004DD RID: 1245
	[Serializable]
	internal sealed class TreeSet<T> : SortedSet<T>
	{
		// Token: 0x06002849 RID: 10313 RVA: 0x0008AB3A File Offset: 0x00088D3A
		public TreeSet()
		{
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x0008AB42 File Offset: 0x00088D42
		public TreeSet(IComparer<T> comparer)
			: base(comparer)
		{
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x0008AB4B File Offset: 0x00088D4B
		public TreeSet(SerializationInfo siInfo, StreamingContext context)
			: base(siInfo, context)
		{
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x0008AB55 File Offset: 0x00088D55
		internal override bool AddIfNotPresent(T item)
		{
			bool flag = base.AddIfNotPresent(item);
			if (!flag)
			{
				throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", item));
			}
			return flag;
		}
	}
}
