using System;

namespace Unity.Properties
{
	// Token: 0x0200003B RID: 59
	internal interface IIndexedCollectionPropertyBagEnumerator<TContainer>
	{
		// Token: 0x06000117 RID: 279
		int GetCount(ref TContainer container);

		// Token: 0x06000118 RID: 280
		IProperty<TContainer> GetSharedProperty();

		// Token: 0x06000119 RID: 281
		IndexedCollectionSharedPropertyState GetSharedPropertyState();

		// Token: 0x0600011A RID: 282
		void SetSharedPropertyState(IndexedCollectionSharedPropertyState state);
	}
}
