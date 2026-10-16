using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000CB RID: 203
	internal interface IParameterTypeInformation : IParameterListEntry
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600070A RID: 1802
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ICustomModifier> CustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600070B RID: 1803
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ICustomModifier> RefCustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600070C RID: 1804
		bool IsByReference { get; }

		// Token: 0x0600070D RID: 1805
		[NullableContext(1)]
		ITypeReference GetType(EmitContext context);
	}
}
