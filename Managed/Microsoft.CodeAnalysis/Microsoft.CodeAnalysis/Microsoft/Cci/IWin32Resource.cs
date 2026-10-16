using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000B8 RID: 184
	[NullableContext(1)]
	internal interface IWin32Resource
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005E9 RID: 1513
		string TypeName { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005EA RID: 1514
		int TypeId { get; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005EB RID: 1515
		string Name { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005EC RID: 1516
		int Id { get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005ED RID: 1517
		uint LanguageId { get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005EE RID: 1518
		uint CodePage { get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005EF RID: 1519
		IEnumerable<byte> Data { get; }
	}
}
