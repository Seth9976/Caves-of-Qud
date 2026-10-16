using System;

namespace Unity.Properties
{
	// Token: 0x0200006A RID: 106
	public interface IPropertyAccept<TContainer>
	{
		// Token: 0x060001CD RID: 461
		void Accept(IPropertyVisitor visitor, ref TContainer container);
	}
}
