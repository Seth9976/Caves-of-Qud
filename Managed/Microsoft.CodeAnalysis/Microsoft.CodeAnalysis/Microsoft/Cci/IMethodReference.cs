using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000A7 RID: 167
	[NullableContext(2)]
	internal interface IMethodReference : ISignature, ITypeMemberReference, IReference, INamedEntity
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600046A RID: 1130
		bool AcceptsExtraArguments { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600046B RID: 1131
		ushort GenericParameterCount { get; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600046C RID: 1132
		bool IsGeneric { get; }

		// Token: 0x0600046D RID: 1133
		IMethodDefinition GetResolvedMethod(EmitContext context);

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600046E RID: 1134
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IParameterTypeInformation> ExtraParameters
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600046F RID: 1135
		IGenericMethodInstanceReference AsGenericMethodInstanceReference { get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000470 RID: 1136
		ISpecializedMethodReference AsSpecializedMethodReference { get; }
	}
}
