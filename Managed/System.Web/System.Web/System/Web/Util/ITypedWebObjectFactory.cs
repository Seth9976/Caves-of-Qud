using System;

namespace System.Web.Util
{
	// Token: 0x0200011B RID: 283
	internal interface ITypedWebObjectFactory : IWebObjectFactory
	{
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000DF8 RID: 3576
		Type InstantiatedType { get; }
	}
}
