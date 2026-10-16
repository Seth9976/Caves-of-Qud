using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeGen;

namespace Microsoft.Cci
{
	// Token: 0x0200009B RID: 155
	[NullableContext(1)]
	internal interface ILocalDefinition : INamedEntity
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000412 RID: 1042
		MetadataConstant CompileTimeValue { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000413 RID: 1043
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ICustomModifier> CustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000414 RID: 1044
		bool IsPinned { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000415 RID: 1045
		bool IsReference { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000416 RID: 1046
		LocalSlotConstraints Constraints { get; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000417 RID: 1047
		LocalVariableAttributes PdbAttributes { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000418 RID: 1048
		[Nullable(0)]
		ImmutableArray<bool> DynamicTransformFlags
		{
			[NullableContext(0)]
			get;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000419 RID: 1049
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<string> TupleElementNames
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600041A RID: 1050
		ITypeReference Type { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600041B RID: 1051
		Location Location { get; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600041C RID: 1052
		int SlotIndex { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600041D RID: 1053
		[Nullable(2)]
		byte[] Signature
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600041E RID: 1054
		LocalSlotDebugInfo SlotInfo { get; }
	}
}
