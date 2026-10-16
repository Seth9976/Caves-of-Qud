using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x0200009A RID: 154
	internal interface IFieldReference : ITypeMemberReference, IReference, INamedEntity
	{
		// Token: 0x0600040E RID: 1038
		[NullableContext(1)]
		ITypeReference GetType(EmitContext context);

		// Token: 0x0600040F RID: 1039
		[NullableContext(2)]
		IFieldDefinition GetResolvedField(EmitContext context);

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000410 RID: 1040
		[Nullable(2)]
		ISpecializedFieldReference AsSpecializedFieldReference
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000411 RID: 1041
		bool IsContextualNamedEntity { get; }
	}
}
