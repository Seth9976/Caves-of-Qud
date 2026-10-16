using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000B2 RID: 178
	[NullableContext(1)]
	internal interface IMarshallingInformation
	{
		// Token: 0x060005D9 RID: 1497
		object GetCustomMarshaller(EmitContext context);

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060005DA RID: 1498
		string CustomMarshallerRuntimeArgument { get; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060005DB RID: 1499
		UnmanagedType ElementType { get; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060005DC RID: 1500
		int IidParameterIndex { get; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060005DD RID: 1501
		UnmanagedType UnmanagedType { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060005DE RID: 1502
		int NumberOfElements { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060005DF RID: 1503
		short ParamIndex { get; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060005E0 RID: 1504
		VarEnum SafeArrayElementSubtype { get; }

		// Token: 0x060005E1 RID: 1505
		ITypeReference GetSafeArrayElementUserDefinedSubtype(EmitContext context);
	}
}
