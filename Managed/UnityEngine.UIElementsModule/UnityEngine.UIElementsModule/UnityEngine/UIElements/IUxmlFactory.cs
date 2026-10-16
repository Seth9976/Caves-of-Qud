using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000579 RID: 1401
	[Obsolete("IUxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public interface IUxmlFactory : IBaseUxmlFactory
	{
		// Token: 0x06002A2C RID: 10796
		VisualElement Create(IUxmlAttributes bag, CreationContext cc);
	}
}
