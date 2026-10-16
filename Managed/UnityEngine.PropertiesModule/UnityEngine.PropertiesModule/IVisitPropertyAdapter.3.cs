using System;

namespace Unity.Properties
{
	// Token: 0x02000060 RID: 96
	public interface IVisitPropertyAdapter : IPropertyVisitorAdapter
	{
		// Token: 0x060001BD RID: 445
		void Visit<TContainer, TValue>(in VisitContext<TContainer, TValue> context, ref TContainer container, ref TValue value);
	}
}
