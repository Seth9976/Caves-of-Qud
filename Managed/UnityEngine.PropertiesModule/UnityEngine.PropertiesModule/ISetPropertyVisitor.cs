using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000078 RID: 120
	public interface ISetPropertyVisitor
	{
		// Token: 0x060001DB RID: 475
		void Visit<TContainer, TSet, TValue>(Property<TContainer, TSet> property, ref TContainer container, ref TSet set) where TSet : ISet<TValue>;
	}
}
