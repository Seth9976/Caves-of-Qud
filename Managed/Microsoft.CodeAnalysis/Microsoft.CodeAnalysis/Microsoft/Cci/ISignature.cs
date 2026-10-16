using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000A0 RID: 160
	[NullableContext(1)]
	internal interface ISignature
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600045C RID: 1116
		CallingConvention CallingConvention { get; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600045D RID: 1117
		ushort ParameterCount { get; }

		// Token: 0x0600045E RID: 1118
		[return: Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IParameterTypeInformation> GetParameters(EmitContext context);

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600045F RID: 1119
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ICustomModifier> ReturnValueCustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000460 RID: 1120
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ICustomModifier> RefCustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000461 RID: 1121
		bool ReturnValueIsByRef { get; }

		// Token: 0x06000462 RID: 1122
		ITypeReference GetType(EmitContext context);
	}
}
