using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x02000099 RID: 153
	internal interface IFieldDefinition : ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition, IFieldReference
	{
		// Token: 0x06000402 RID: 1026
		[NullableContext(2)]
		MetadataConstant GetCompileTimeValue(EmitContext context);

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000403 RID: 1027
		ImmutableArray<byte> MappedData { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000404 RID: 1028
		bool IsCompileTimeConstant { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000405 RID: 1029
		bool IsMarshalledExplicitly { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000406 RID: 1030
		bool IsNotSerialized { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000407 RID: 1031
		bool IsReadOnly { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000408 RID: 1032
		bool IsRuntimeSpecial { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000409 RID: 1033
		bool IsSpecialName { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600040A RID: 1034
		bool IsStatic { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600040B RID: 1035
		[Nullable(2)]
		IMarshallingInformation MarshallingInformation
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600040C RID: 1036
		ImmutableArray<byte> MarshallingDescriptor { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600040D RID: 1037
		int Offset { get; }
	}
}
