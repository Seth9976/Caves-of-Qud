using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x0200003A RID: 58
	internal struct IndexedCollectionPropertyBagEnumerator<TContainer> : IEnumerator<IProperty<TContainer>>, IEnumerator, IDisposable
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00005371 File Offset: 0x00003571
		internal IndexedCollectionPropertyBagEnumerator(IIndexedCollectionPropertyBagEnumerator<TContainer> impl, TContainer container)
		{
			this.m_Impl = impl;
			this.m_Container = container;
			this.m_Previous = impl.GetSharedPropertyState();
			this.m_Position = -1;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005395 File Offset: 0x00003595
		public IProperty<TContainer> Current
		{
			get
			{
				return this.m_Impl.GetSharedProperty();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000053A2 File Offset: 0x000035A2
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000053AC File Offset: 0x000035AC
		public bool MoveNext()
		{
			this.m_Position++;
			bool flag = this.m_Position < this.m_Impl.GetCount(ref this.m_Container);
			bool flag2;
			if (flag)
			{
				this.m_Impl.SetSharedPropertyState(new IndexedCollectionSharedPropertyState
				{
					Index = this.m_Position,
					IsReadOnly = false
				});
				flag2 = true;
			}
			else
			{
				this.m_Impl.SetSharedPropertyState(this.m_Previous);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000542B File Offset: 0x0000362B
		public void Reset()
		{
			this.m_Position = -1;
			this.m_Impl.SetSharedPropertyState(this.m_Previous);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005447 File Offset: 0x00003647
		public void Dispose()
		{
		}

		// Token: 0x0400005D RID: 93
		private readonly IIndexedCollectionPropertyBagEnumerator<TContainer> m_Impl;

		// Token: 0x0400005E RID: 94
		private readonly IndexedCollectionSharedPropertyState m_Previous;

		// Token: 0x0400005F RID: 95
		private TContainer m_Container;

		// Token: 0x04000060 RID: 96
		private int m_Position;
	}
}
