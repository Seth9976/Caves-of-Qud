using System;

namespace Unity.Properties
{
	// Token: 0x02000069 RID: 105
	public interface IDictionaryPropertyBagAccept<TContainer>
	{
		// Token: 0x060001CC RID: 460
		void Accept(IDictionaryPropertyBagVisitor visitor, ref TContainer container);
	}
}
