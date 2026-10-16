using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004CF RID: 1231
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class LocalSlotManager
	{
		// Token: 0x06003186 RID: 12678 RVA: 0x0008DC00 File Offset: 0x0008BE00
		[NullableContext(2)]
		public LocalSlotManager(VariableSlotAllocator slotAllocator)
		{
			this._slotAllocator = slotAllocator;
			if (slotAllocator != null)
			{
				this._lazyAllLocals = new ArrayBuilder<ILocalDefinition>();
				slotAllocator.AddPreviousLocals(this._lazyAllLocals);
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x0008DC2C File Offset: 0x0008BE2C
		private Dictionary<ILocalSymbolInternal, LocalDefinition> LocalMap
		{
			get
			{
				Dictionary<ILocalSymbolInternal, LocalDefinition> dictionary = this._localMap;
				if (dictionary == null)
				{
					dictionary = new Dictionary<ILocalSymbolInternal, LocalDefinition>(ReferenceEqualityComparer.Instance);
					this._localMap = dictionary;
				}
				return dictionary;
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x0008DC58 File Offset: 0x0008BE58
		private KeyedStack<LocalSlotManager.LocalSignature, LocalDefinition> FreeSlots
		{
			get
			{
				KeyedStack<LocalSlotManager.LocalSignature, LocalDefinition> keyedStack = this._freeSlots;
				if (keyedStack == null)
				{
					keyedStack = new KeyedStack<LocalSlotManager.LocalSignature, LocalDefinition>();
					this._freeSlots = keyedStack;
				}
				return keyedStack;
			}
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x0008DC80 File Offset: 0x0008BE80
		internal LocalDefinition DeclareLocal(ITypeReference type, ILocalSymbolInternal symbol, string name, SynthesizedLocalKind kind, LocalDebugId id, LocalVariableAttributes pdbAttributes, LocalSlotConstraints constraints, [Nullable(0)] ImmutableArray<bool> dynamicTransformFlags, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> tupleElementNames, bool isSlotReusable)
		{
			LocalDefinition localDefinition;
			if (!isSlotReusable || !this.FreeSlots.TryPop(new LocalSlotManager.LocalSignature(type, constraints), out localDefinition))
			{
				localDefinition = this.DeclareLocalImpl(type, symbol, name, kind, id, pdbAttributes, constraints, dynamicTransformFlags, tupleElementNames);
			}
			this.LocalMap.Add(symbol, localDefinition);
			return localDefinition;
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x0008DCCC File Offset: 0x0008BECC
		internal LocalDefinition GetLocal(ILocalSymbolInternal symbol)
		{
			return this.LocalMap[symbol];
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x0008DCDC File Offset: 0x0008BEDC
		internal void FreeLocal(ILocalSymbolInternal symbol)
		{
			LocalDefinition local = this.GetLocal(symbol);
			this.LocalMap.Remove(symbol);
			this.FreeSlot(local);
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x0008DD08 File Offset: 0x0008BF08
		internal LocalDefinition AllocateSlot(ITypeReference type, LocalSlotConstraints constraints, [Nullable(0)] ImmutableArray<bool> dynamicTransformFlags = default(ImmutableArray<bool>), [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> tupleElementNames = default(ImmutableArray<string>))
		{
			LocalDefinition localDefinition;
			if (!this.FreeSlots.TryPop(new LocalSlotManager.LocalSignature(type, constraints), out localDefinition))
			{
				localDefinition = this.DeclareLocalImpl(type, null, null, SynthesizedLocalKind.EmitterTemp, LocalDebugId.None, LocalVariableAttributes.DebuggerHidden, constraints, dynamicTransformFlags, tupleElementNames);
			}
			return localDefinition;
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x0008DD44 File Offset: 0x0008BF44
		private LocalDefinition DeclareLocalImpl(ITypeReference type, [Nullable(2)] ILocalSymbolInternal symbol, [Nullable(2)] string name, SynthesizedLocalKind kind, LocalDebugId id, LocalVariableAttributes pdbAttributes, LocalSlotConstraints constraints, [Nullable(0)] ImmutableArray<bool> dynamicTransformFlags, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> tupleElementNames)
		{
			if (this._lazyAllLocals == null)
			{
				this._lazyAllLocals = new ArrayBuilder<ILocalDefinition>(1);
			}
			LocalDefinition localDefinition;
			if (symbol != null && this._slotAllocator != null)
			{
				localDefinition = this._slotAllocator.GetPreviousLocal(type, symbol, name, kind, id, pdbAttributes, constraints, dynamicTransformFlags, tupleElementNames);
				if (localDefinition != null)
				{
					int slotIndex = localDefinition.SlotIndex;
					this._lazyAllLocals[slotIndex] = localDefinition;
					return localDefinition;
				}
			}
			localDefinition = new LocalDefinition(symbol, name, type, this._lazyAllLocals.Count, kind, id, pdbAttributes, constraints, dynamicTransformFlags, tupleElementNames);
			this._lazyAllLocals.Add(localDefinition);
			return localDefinition;
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x0008DDD1 File Offset: 0x0008BFD1
		internal void FreeSlot(LocalDefinition slot)
		{
			this.FreeSlots.Push(new LocalSlotManager.LocalSignature(slot.Type, slot.Constraints), slot);
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x0008DDF0 File Offset: 0x0008BFF0
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalDefinition> LocalsInOrder()
		{
			if (this._lazyAllLocals == null)
			{
				return ImmutableArray<ILocalDefinition>.Empty;
			}
			return this._lazyAllLocals.ToImmutable();
		}

		// Token: 0x0400131A RID: 4890
		[Nullable(new byte[] { 2, 1, 1 })]
		private Dictionary<ILocalSymbolInternal, LocalDefinition> _localMap;

		// Token: 0x0400131B RID: 4891
		[Nullable(new byte[] { 2, 1 })]
		private KeyedStack<LocalSlotManager.LocalSignature, LocalDefinition> _freeSlots;

		// Token: 0x0400131C RID: 4892
		[Nullable(new byte[] { 2, 1 })]
		private ArrayBuilder<ILocalDefinition> _lazyAllLocals;

		// Token: 0x0400131D RID: 4893
		[Nullable(2)]
		private readonly VariableSlotAllocator _slotAllocator;

		// Token: 0x0200077B RID: 1915
		[NullableContext(0)]
		private readonly struct LocalSignature : IEquatable<LocalSlotManager.LocalSignature>
		{
			// Token: 0x06003EC7 RID: 16071 RVA: 0x000B0C6C File Offset: 0x000AEE6C
			[NullableContext(1)]
			internal LocalSignature(ITypeReference valType, LocalSlotConstraints constraints)
			{
				this._constraints = constraints;
				this._type = valType;
			}

			// Token: 0x06003EC8 RID: 16072 RVA: 0x000B0C7C File Offset: 0x000AEE7C
			public bool Equals(LocalSlotManager.LocalSignature other)
			{
				return this._constraints == other._constraints && SymbolEquivalentEqualityComparer.Instance.Equals(this._type, other._type);
			}

			// Token: 0x06003EC9 RID: 16073 RVA: 0x000B0CA4 File Offset: 0x000AEEA4
			public override int GetHashCode()
			{
				return Hash.Combine(SymbolEquivalentEqualityComparer.Instance.GetHashCode(this._type), (int)this._constraints);
			}

			// Token: 0x06003ECA RID: 16074 RVA: 0x000B0CC4 File Offset: 0x000AEEC4
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				if (obj is LocalSlotManager.LocalSignature)
				{
					LocalSlotManager.LocalSignature localSignature = (LocalSlotManager.LocalSignature)obj;
					return this.Equals(localSignature);
				}
				return false;
			}

			// Token: 0x04001D72 RID: 7538
			[Nullable(1)]
			private readonly ITypeReference _type;

			// Token: 0x04001D73 RID: 7539
			private readonly LocalSlotConstraints _constraints;
		}
	}
}
