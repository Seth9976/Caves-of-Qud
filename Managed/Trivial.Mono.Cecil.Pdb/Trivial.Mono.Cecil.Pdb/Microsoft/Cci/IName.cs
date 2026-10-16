using System;

namespace Microsoft.Cci
{
	// Token: 0x02000012 RID: 18
	public interface IName
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600014C RID: 332
		int UniqueKey { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600014D RID: 333
		int UniqueKeyIgnoringCase { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600014E RID: 334
		string Value { get; }
	}
}
