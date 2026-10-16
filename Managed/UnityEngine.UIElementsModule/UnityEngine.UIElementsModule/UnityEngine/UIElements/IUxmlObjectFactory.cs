using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200057B RID: 1403
	[Obsolete("IUxmlObjectFactory<out T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	internal interface IUxmlObjectFactory<out T> : IBaseUxmlObjectFactory, IBaseUxmlFactory where T : new()
	{
		// Token: 0x06002A2D RID: 10797
		T CreateObject(IUxmlAttributes bag, CreationContext cc);
	}
}
