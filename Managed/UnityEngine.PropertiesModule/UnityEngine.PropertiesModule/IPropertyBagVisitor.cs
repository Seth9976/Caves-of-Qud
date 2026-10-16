using System;

namespace Unity.Properties
{
	// Token: 0x02000070 RID: 112
	public interface IPropertyBagVisitor
	{
		// Token: 0x060001D3 RID: 467
		void Visit<TContainer>(IPropertyBag<TContainer> properties, ref TContainer container);
	}
}
