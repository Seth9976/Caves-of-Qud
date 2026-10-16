using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000071 RID: 113
	public interface ICollectionPropertyBagVisitor
	{
		// Token: 0x060001D4 RID: 468
		void Visit<TCollection, TElement>(ICollectionPropertyBag<TCollection, TElement> properties, ref TCollection container) where TCollection : ICollection<TElement>;
	}
}
