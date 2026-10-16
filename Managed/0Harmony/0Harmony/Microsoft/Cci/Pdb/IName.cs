using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003FB RID: 1019
	internal interface IName
	{
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060015AC RID: 5548
		int UniqueKey { get; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060015AD RID: 5549
		int UniqueKeyIgnoringCase { get; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060015AE RID: 5550
		string Value { get; }
	}
}
