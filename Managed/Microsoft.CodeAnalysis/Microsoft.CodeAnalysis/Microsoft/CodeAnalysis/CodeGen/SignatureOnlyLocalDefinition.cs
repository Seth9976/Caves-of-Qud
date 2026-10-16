using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004E2 RID: 1250
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SignatureOnlyLocalDefinition : ILocalDefinition, INamedEntity
	{
		// Token: 0x06003256 RID: 12886 RVA: 0x0008F12C File Offset: 0x0008D32C
		internal SignatureOnlyLocalDefinition(byte[] signature, int slot)
		{
			this._signature = signature;
			this._slot = slot;
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x0008F142 File Offset: 0x0008D342
		public MetadataConstant CompileTimeValue
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06003258 RID: 12888 RVA: 0x0008F149 File Offset: 0x0008D349
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ICustomModifier> CustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x0008F150 File Offset: 0x0008D350
		[Nullable(0)]
		public ImmutableArray<bool> DynamicTransformFlags
		{
			[NullableContext(0)]
			get
			{
				return ImmutableArray<bool>.Empty;
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x0008F157 File Offset: 0x0008D357
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> TupleElementNames
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<string>.Empty;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x0008F15E File Offset: 0x0008D35E
		public LocalVariableAttributes PdbAttributes
		{
			get
			{
				return LocalVariableAttributes.DebuggerHidden;
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x0600325C RID: 12892 RVA: 0x0008F161 File Offset: 0x0008D361
		public bool IsPinned
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x0008F168 File Offset: 0x0008D368
		public bool IsReference
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x0008F16F File Offset: 0x0008D36F
		public LocalSlotConstraints Constraints
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x0008F176 File Offset: 0x0008D376
		public Location Location
		{
			get
			{
				return Location.None;
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x0008F17D File Offset: 0x0008D37D
		[Nullable(2)]
		public string Name
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x0008F180 File Offset: 0x0008D380
		public int SlotIndex
		{
			get
			{
				return this._slot;
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x0008F188 File Offset: 0x0008D388
		public ITypeReference Type
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x0008F18F File Offset: 0x0008D38F
		public byte[] Signature
		{
			get
			{
				return this._signature;
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x0008F197 File Offset: 0x0008D397
		public LocalSlotDebugInfo SlotInfo
		{
			get
			{
				return new LocalSlotDebugInfo(SynthesizedLocalKind.EmitterTemp, LocalDebugId.None);
			}
		}

		// Token: 0x0400136C RID: 4972
		private readonly byte[] _signature;

		// Token: 0x0400136D RID: 4973
		private readonly int _slot;
	}
}
