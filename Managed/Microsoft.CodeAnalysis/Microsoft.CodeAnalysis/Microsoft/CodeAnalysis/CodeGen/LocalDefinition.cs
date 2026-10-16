using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004CC RID: 1228
	[NullableContext(2)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal sealed class LocalDefinition : ILocalDefinition, INamedEntity
	{
		// Token: 0x0600316A RID: 12650 RVA: 0x0008D99C File Offset: 0x0008BB9C
		public LocalDefinition(ILocalSymbolInternal symbolOpt, string nameOpt, [Nullable(1)] ITypeReference type, int slot, SynthesizedLocalKind synthesizedKind, LocalDebugId id, LocalVariableAttributes pdbAttributes, LocalSlotConstraints constraints, [Nullable(0)] ImmutableArray<bool> dynamicTransformFlags, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> tupleElementNames)
		{
			this._symbolOpt = symbolOpt;
			this._nameOpt = nameOpt;
			this._type = type;
			this._slot = slot;
			this._slotInfo = new LocalSlotDebugInfo(synthesizedKind, id);
			this._pdbAttributes = pdbAttributes;
			this._dynamicTransformFlags = dynamicTransformFlags.NullToEmpty<bool>();
			this._tupleElementNames = tupleElementNames.NullToEmpty<string>();
			this._constraints = constraints;
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x0008DA05 File Offset: 0x0008BC05
		[NullableContext(1)]
		internal string GetDebuggerDisplay()
		{
			return string.Format("{0}: {1} ({2})", this._slot, this._nameOpt ?? "<unnamed>", this._type);
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x0008DA31 File Offset: 0x0008BC31
		public ILocalSymbolInternal SymbolOpt
		{
			get
			{
				return this._symbolOpt;
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x0008DA3C File Offset: 0x0008BC3C
		[Nullable(1)]
		public Location Location
		{
			[NullableContext(1)]
			get
			{
				if (this._symbolOpt != null)
				{
					ImmutableArray<Location> locations = this._symbolOpt.Locations;
					if (!locations.IsDefaultOrEmpty)
					{
						return locations[0];
					}
				}
				return Location.None;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x0600316E RID: 12654 RVA: 0x0008DA74 File Offset: 0x0008BC74
		public int SlotIndex
		{
			get
			{
				return this._slot;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600316F RID: 12655 RVA: 0x0008DA7C File Offset: 0x0008BC7C
		[Nullable(1)]
		public MetadataConstant CompileTimeValue
		{
			[NullableContext(1)]
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x0008DA83 File Offset: 0x0008BC83
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ICustomModifier> CustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<ICustomModifier>.Empty;
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06003171 RID: 12657 RVA: 0x0008DA8A File Offset: 0x0008BC8A
		public bool IsConstant
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06003172 RID: 12658 RVA: 0x0008DA91 File Offset: 0x0008BC91
		public bool IsModified
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06003173 RID: 12659 RVA: 0x0008DA94 File Offset: 0x0008BC94
		public LocalSlotConstraints Constraints
		{
			get
			{
				return this._constraints;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06003174 RID: 12660 RVA: 0x0008DA9C File Offset: 0x0008BC9C
		public bool IsPinned
		{
			get
			{
				return (this._constraints & LocalSlotConstraints.Pinned) > LocalSlotConstraints.None;
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06003175 RID: 12661 RVA: 0x0008DAA9 File Offset: 0x0008BCA9
		public bool IsReference
		{
			get
			{
				return (this._constraints & LocalSlotConstraints.ByRef) > LocalSlotConstraints.None;
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06003176 RID: 12662 RVA: 0x0008DAB6 File Offset: 0x0008BCB6
		public LocalVariableAttributes PdbAttributes
		{
			get
			{
				return this._pdbAttributes;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06003177 RID: 12663 RVA: 0x0008DABE File Offset: 0x0008BCBE
		[Nullable(0)]
		public ImmutableArray<bool> DynamicTransformFlags
		{
			[NullableContext(0)]
			get
			{
				return this._dynamicTransformFlags;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06003178 RID: 12664 RVA: 0x0008DAC6 File Offset: 0x0008BCC6
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> TupleElementNames
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._tupleElementNames;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x0008DACE File Offset: 0x0008BCCE
		[Nullable(1)]
		public ITypeReference Type
		{
			[NullableContext(1)]
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x0600317A RID: 12666 RVA: 0x0008DAD6 File Offset: 0x0008BCD6
		public string Name
		{
			get
			{
				return this._nameOpt;
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x0600317B RID: 12667 RVA: 0x0008DADE File Offset: 0x0008BCDE
		public byte[] Signature
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x0600317C RID: 12668 RVA: 0x0008DAE1 File Offset: 0x0008BCE1
		public LocalSlotDebugInfo SlotInfo
		{
			get
			{
				return this._slotInfo;
			}
		}

		// Token: 0x0400130D RID: 4877
		private readonly ILocalSymbolInternal _symbolOpt;

		// Token: 0x0400130E RID: 4878
		private readonly string _nameOpt;

		// Token: 0x0400130F RID: 4879
		[Nullable(1)]
		private readonly ITypeReference _type;

		// Token: 0x04001310 RID: 4880
		private readonly LocalSlotConstraints _constraints;

		// Token: 0x04001311 RID: 4881
		private readonly int _slot;

		// Token: 0x04001312 RID: 4882
		private readonly LocalSlotDebugInfo _slotInfo;

		// Token: 0x04001313 RID: 4883
		private readonly LocalVariableAttributes _pdbAttributes;

		// Token: 0x04001314 RID: 4884
		[Nullable(0)]
		private readonly ImmutableArray<bool> _dynamicTransformFlags;

		// Token: 0x04001315 RID: 4885
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<string> _tupleElementNames;
	}
}
