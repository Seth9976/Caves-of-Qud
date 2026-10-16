using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000B6 RID: 182
	[NullableContext(2)]
	internal interface IPlatformInvokeInformation
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005E5 RID: 1509
		string ModuleName { get; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005E6 RID: 1510
		string EntryPointName { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005E7 RID: 1511
		MethodImportAttributes Flags { get; }
	}
}
