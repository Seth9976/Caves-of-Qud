using System;

namespace Unity.Properties
{
	// Token: 0x02000067 RID: 103
	public interface IListPropertyBagAccept<TContainer>
	{
		// Token: 0x060001CA RID: 458
		void Accept(IListPropertyBagVisitor visitor, ref TContainer container);
	}
}
