using System;

namespace Unity.Properties
{
	// Token: 0x0200005B RID: 91
	public interface IExcludeContravariantPropertyAdapter<TContainer, in TValue> : IPropertyVisitorAdapter
	{
		// Token: 0x060001B9 RID: 441
		bool IsExcluded(in ExcludeContext<TContainer> context, ref TContainer container, TValue value);
	}
}
