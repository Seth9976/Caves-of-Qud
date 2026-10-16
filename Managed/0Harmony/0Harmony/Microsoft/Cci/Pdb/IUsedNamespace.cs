using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003FA RID: 1018
	internal interface IUsedNamespace
	{
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060015AA RID: 5546
		IName Alias { get; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060015AB RID: 5547
		IName NamespaceName { get; }
	}
}
