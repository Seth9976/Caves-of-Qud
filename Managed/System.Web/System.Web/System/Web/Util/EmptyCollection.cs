using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x02000114 RID: 276
	internal class EmptyCollection : ICollection, IEnumerable, IEnumerator
	{
		// Token: 0x06000DDD RID: 3549 RVA: 0x00002050 File Offset: 0x00000250
		private EmptyCollection()
		{
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00025614 File Offset: 0x00023814
		internal static EmptyCollection Instance
		{
			get
			{
				return EmptyCollection.s_theEmptyCollection;
			}
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00002058 File Offset: 0x00000258
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00008A4D File Offset: 0x00006C4D
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x00008B4A File Offset: 0x00006D4A
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00002058 File Offset: 0x00000258
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00003946 File Offset: 0x00001B46
		public void CopyTo(Array array, int index)
		{
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x00003BF6 File Offset: 0x00001DF6
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00008A4D File Offset: 0x00006C4D
		bool IEnumerator.MoveNext()
		{
			return false;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00003946 File Offset: 0x00001B46
		void IEnumerator.Reset()
		{
		}

		// Token: 0x040011B0 RID: 4528
		private static EmptyCollection s_theEmptyCollection = new EmptyCollection();
	}
}
