using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x0200008D RID: 141
	[NullableContext(1)]
	internal interface ICustomAttribute
	{
		// Token: 0x060003AB RID: 939
		[return: Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IMetadataExpression> GetArguments(EmitContext context);

		// Token: 0x060003AC RID: 940
		IMethodReference Constructor(EmitContext context, bool reportDiagnostics);

		// Token: 0x060003AD RID: 941
		[return: Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IMetadataNamedArgument> GetNamedArguments(EmitContext context);

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060003AE RID: 942
		int ArgumentCount { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060003AF RID: 943
		ushort NamedArgumentCount { get; }

		// Token: 0x060003B0 RID: 944
		ITypeReference GetType(EmitContext context);

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060003B1 RID: 945
		bool AllowMultiple { get; }
	}
}
