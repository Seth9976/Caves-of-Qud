using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200057E RID: 1406
	[Obsolete("UxmlObjectFactory<TCreatedType, TTraits> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	internal class UxmlObjectFactory<TCreatedType, TTraits> : BaseUxmlFactory<TCreatedType, TTraits>, IUxmlObjectFactory<TCreatedType>, IBaseUxmlObjectFactory, IBaseUxmlFactory where TCreatedType : new() where TTraits : UxmlObjectTraits<TCreatedType>, new()
	{
		// Token: 0x06002A3C RID: 10812 RVA: 0x00098344 File Offset: 0x00096544
		public virtual TCreatedType CreateObject(IUxmlAttributes bag, CreationContext cc)
		{
			TCreatedType tcreatedType = new TCreatedType();
			this.m_Traits.Init(ref tcreatedType, bag, cc);
			return tcreatedType;
		}
	}
}
