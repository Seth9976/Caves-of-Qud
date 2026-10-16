using System;

namespace Unity.Properties
{
	// Token: 0x02000061 RID: 97
	public interface IVisitContravariantPropertyAdapter<TContainer, in TValue> : IPropertyVisitorAdapter
	{
		// Token: 0x060001BE RID: 446
		void Visit(in VisitContext<TContainer> context, ref TContainer container, TValue value);
	}
}
