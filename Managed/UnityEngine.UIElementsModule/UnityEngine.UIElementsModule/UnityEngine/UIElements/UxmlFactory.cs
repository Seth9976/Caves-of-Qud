using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200057D RID: 1405
	[Obsolete("UxmlFactory<TCreatedType, TTraits> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlFactory<TCreatedType, TTraits> : BaseUxmlFactory<TCreatedType, TTraits>, IUxmlFactory, IBaseUxmlFactory where TCreatedType : VisualElement, new() where TTraits : UxmlTraits, new()
	{
		// Token: 0x06002A3A RID: 10810 RVA: 0x00098304 File Offset: 0x00096504
		public virtual VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			TCreatedType tcreatedType = new TCreatedType();
			this.m_Traits.Init(tcreatedType, bag, cc);
			return tcreatedType;
		}
	}
}
