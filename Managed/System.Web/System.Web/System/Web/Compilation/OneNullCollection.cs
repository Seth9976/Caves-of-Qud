using System;
using System.Collections;

namespace System.Web.Compilation
{
	// Token: 0x0200063E RID: 1598
	internal class OneNullCollection : ICollection, IEnumerable
	{
		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x060044BC RID: 17596 RVA: 0x00008B4A File Offset: 0x00006D4A
		public int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x060044BD RID: 17597 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x060044BE RID: 17598 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x000BA330 File Offset: 0x000B8530
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (array.Rank > 1)
			{
				throw new ArgumentException();
			}
			int length = array.Length;
			if (index >= length || index > length - 1)
			{
				throw new ArgumentException();
			}
			array.SetValue(null, index);
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x000BA37E File Offset: 0x000B857E
		public IEnumerator GetEnumerator()
		{
			yield return null;
			yield break;
		}
	}
}
