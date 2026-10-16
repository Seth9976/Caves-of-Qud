using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x0200009F RID: 159
	internal interface IPropertyDefinition : ISignature, ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition
	{
		// Token: 0x06000454 RID: 1108
		[NullableContext(1)]
		IEnumerable<IMethodReference> GetAccessors(EmitContext context);

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000455 RID: 1109
		[Nullable(2)]
		MetadataConstant DefaultValue
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000456 RID: 1110
		[Nullable(2)]
		IMethodReference Getter
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000457 RID: 1111
		bool HasDefaultValue { get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000458 RID: 1112
		bool IsRuntimeSpecial { get; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000459 RID: 1113
		bool IsSpecialName { get; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600045A RID: 1114
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IParameterDefinition> Parameters
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600045B RID: 1115
		[Nullable(2)]
		IMethodReference Setter
		{
			[NullableContext(2)]
			get;
		}
	}
}
