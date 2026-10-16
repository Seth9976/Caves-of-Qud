using System;

namespace Unity.Properties
{
	// Token: 0x02000063 RID: 99
	public abstract class ConcreteTypeVisitor : IPropertyBagVisitor
	{
		// Token: 0x060001C0 RID: 448
		protected abstract void VisitContainer<TContainer>(ref TContainer container);

		// Token: 0x060001C1 RID: 449 RVA: 0x000065CF File Offset: 0x000047CF
		void IPropertyBagVisitor.Visit<TContainer>(IPropertyBag<TContainer> properties, ref TContainer container)
		{
			this.VisitContainer<TContainer>(ref container);
		}
	}
}
