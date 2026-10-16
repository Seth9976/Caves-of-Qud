using System;

namespace Microsoft.Cci
{
	// Token: 0x02000014 RID: 20
	public interface IUsedNamespace
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000150 RID: 336
		IName Alias { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000151 RID: 337
		IName NamespaceName { get; }
	}
}
