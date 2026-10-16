using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x0200009E RID: 158
	internal interface IParameterDefinition : IDefinition, IReference, INamedEntity, IParameterTypeInformation, IParameterListEntry
	{
		// Token: 0x0600044C RID: 1100
		[NullableContext(2)]
		MetadataConstant GetDefaultValue(EmitContext context);

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600044D RID: 1101
		bool HasDefaultValue { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600044E RID: 1102
		bool IsIn { get; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600044F RID: 1103
		bool IsMarshalledExplicitly { get; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000450 RID: 1104
		bool IsOptional { get; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000451 RID: 1105
		bool IsOut { get; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000452 RID: 1106
		[Nullable(2)]
		IMarshallingInformation MarshallingInformation
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000453 RID: 1107
		ImmutableArray<byte> MarshallingDescriptor { get; }
	}
}
