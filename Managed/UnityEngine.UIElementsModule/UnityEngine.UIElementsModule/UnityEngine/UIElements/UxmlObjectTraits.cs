using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000577 RID: 1399
	[Obsolete("UxmlObjectTraits<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	internal abstract class UxmlObjectTraits<T> : BaseUxmlTraits
	{
		// Token: 0x06002A1F RID: 10783 RVA: 0x00002152 File Offset: 0x00000352
		public virtual void Init(ref T obj, IUxmlAttributes bag, CreationContext cc)
		{
		}
	}
}
