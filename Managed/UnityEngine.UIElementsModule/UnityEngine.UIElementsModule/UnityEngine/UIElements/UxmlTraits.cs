using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000576 RID: 1398
	[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public abstract class UxmlTraits : BaseUxmlTraits
	{
		// Token: 0x06002A1D RID: 10781 RVA: 0x00002152 File Offset: 0x00000352
		public virtual void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
		{
		}
	}
}
