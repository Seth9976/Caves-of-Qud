using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004CA RID: 1226
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class LocalConstantDefinition : ILocalDefinition, INamedEntity
	{
		// Token: 0x06003151 RID: 12625 RVA: 0x0008D84E File Offset: 0x0008BA4E
		public LocalConstantDefinition(string name, Location location, MetadataConstant compileTimeValue, [Nullable(0)] ImmutableArray<bool> dynamicTransformFlags, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> tupleElementNames)
		{
			this.Name = name;
			this.Location = location;
			this.CompileTimeValue = compileTimeValue;
			this.DynamicTransformFlags = dynamicTransformFlags.NullToEmpty<bool>();
			this.TupleElementNames = tupleElementNames.NullToEmpty<string>();
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x0008D885 File Offset: 0x0008BA85
		public string Name { get; }

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x0008D88D File Offset: 0x0008BA8D
		public Location Location { get; }

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06003154 RID: 12628 RVA: 0x0008D895 File Offset: 0x0008BA95
		public MetadataConstant CompileTimeValue { get; }

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06003155 RID: 12629 RVA: 0x0008D89D File Offset: 0x0008BA9D
		public ITypeReference Type
		{
			get
			{
				return this.CompileTimeValue.Type;
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x0008D8AA File Offset: 0x0008BAAA
		public bool IsConstant
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06003157 RID: 12631 RVA: 0x0008D8AD File Offset: 0x0008BAAD
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ICustomModifier> CustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<ICustomModifier>.Empty;
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x0008D8B4 File Offset: 0x0008BAB4
		public bool IsModified
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06003159 RID: 12633 RVA: 0x0008D8B7 File Offset: 0x0008BAB7
		public bool IsPinned
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x0008D8BA File Offset: 0x0008BABA
		public bool IsReference
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x0008D8BD File Offset: 0x0008BABD
		public LocalSlotConstraints Constraints
		{
			get
			{
				return LocalSlotConstraints.None;
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x0008D8C0 File Offset: 0x0008BAC0
		public LocalVariableAttributes PdbAttributes
		{
			get
			{
				return LocalVariableAttributes.None;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x0008D8C3 File Offset: 0x0008BAC3
		[Nullable(0)]
		public ImmutableArray<bool> DynamicTransformFlags
		{
			[NullableContext(0)]
			get;
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x0008D8CB File Offset: 0x0008BACB
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> TupleElementNames
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x0008D8D3 File Offset: 0x0008BAD3
		public int SlotIndex
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x0008D8D6 File Offset: 0x0008BAD6
		[Nullable(2)]
		public byte[] Signature
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x0008D8D9 File Offset: 0x0008BAD9
		public LocalSlotDebugInfo SlotInfo
		{
			get
			{
				return new LocalSlotDebugInfo(SynthesizedLocalKind.UserDefined, LocalDebugId.None);
			}
		}
	}
}
