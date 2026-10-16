using System;

namespace Unity.Properties
{
	// Token: 0x02000039 RID: 57
	internal readonly struct IndexedCollectionPropertyBagEnumerable<TContainer>
	{
		// Token: 0x0600010F RID: 271 RVA: 0x0000534D File Offset: 0x0000354D
		public IndexedCollectionPropertyBagEnumerable(IIndexedCollectionPropertyBagEnumerator<TContainer> impl, TContainer container)
		{
			this.m_Impl = impl;
			this.m_Container = container;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000535E File Offset: 0x0000355E
		public IndexedCollectionPropertyBagEnumerator<TContainer> GetEnumerator()
		{
			return new IndexedCollectionPropertyBagEnumerator<TContainer>(this.m_Impl, this.m_Container);
		}

		// Token: 0x0400005B RID: 91
		private readonly IIndexedCollectionPropertyBagEnumerator<TContainer> m_Impl;

		// Token: 0x0400005C RID: 92
		private readonly TContainer m_Container;
	}
}
