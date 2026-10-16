using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000151 RID: 337
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })]
	internal abstract class LocalDataFlowPass<[global::System.Runtime.CompilerServices.Nullable(0)] TLocalState, [global::System.Runtime.CompilerServices.Nullable(0)] TLocalFunctionState> : AbstractFlowPass<TLocalState, TLocalFunctionState> where TLocalState : LocalDataFlowPass<TLocalState, TLocalFunctionState>.ILocalDataFlowState where TLocalFunctionState : AbstractFlowPass<TLocalState, TLocalFunctionState>.AbstractLocalFunctionState
	{
		// Token: 0x06001B7E RID: 7038 RVA: 0x000907B4 File Offset: 0x0008E9B4
		protected LocalDataFlowPass(CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(2)] Symbol member, BoundNode node, EmptyStructTypeCache emptyStructs, bool trackUnassignments)
			: base(compilation, member, node, null, null, false, trackUnassignments)
		{
			this._emptyStructTypeCache = emptyStructs;
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x000907CC File Offset: 0x0008E9CC
		protected LocalDataFlowPass(CSharpCompilation compilation, Symbol member, BoundNode node, EmptyStructTypeCache emptyStructs, BoundNode firstInRegion, BoundNode lastInRegion, bool trackRegions, bool trackUnassignments)
			: base(compilation, member, node, firstInRegion, lastInRegion, trackRegions, trackUnassignments)
		{
			this._emptyStructTypeCache = emptyStructs;
		}

		// Token: 0x06001B80 RID: 7040
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		protected abstract bool TryGetVariable(LocalDataFlowPass<TLocalState, TLocalFunctionState>.VariableIdentifier identifier, out int slot);

		// Token: 0x06001B81 RID: 7041
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		protected abstract int AddVariable(LocalDataFlowPass<TLocalState, TLocalFunctionState>.VariableIdentifier identifier);

		// Token: 0x06001B82 RID: 7042 RVA: 0x000907E8 File Offset: 0x0008E9E8
		protected int VariableSlot(Symbol symbol, int containingSlot = 0)
		{
			containingSlot = this.DescendThroughTupleRestFields(ref symbol, containingSlot, false);
			int num;
			if (!this.TryGetVariable(new LocalDataFlowPass<TLocalState, TLocalFunctionState>.VariableIdentifier(symbol, containingSlot), out num))
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x00090815 File Offset: 0x0008EA15
		protected virtual bool IsEmptyStructType(TypeSymbol type)
		{
			return this._emptyStructTypeCache.IsEmptyStructType(type);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00090824 File Offset: 0x0008EA24
		protected virtual int GetOrCreateSlot(Symbol symbol, int containingSlot = 0, bool forceSlotEvenIfEmpty = false, bool createIfMissing = true)
		{
			if (symbol.Kind == SymbolKind.RangeVariable)
			{
				return -1;
			}
			containingSlot = this.DescendThroughTupleRestFields(ref symbol, containingSlot, true);
			if (containingSlot < 0)
			{
				return -1;
			}
			LocalDataFlowPass<TLocalState, TLocalFunctionState>.VariableIdentifier variableIdentifier = new LocalDataFlowPass<TLocalState, TLocalFunctionState>.VariableIdentifier(symbol, containingSlot);
			int num;
			if (!this.TryGetVariable(variableIdentifier, out num))
			{
				if (!createIfMissing)
				{
					return -1;
				}
				TypeSymbol type = symbol.GetTypeOrReturnType().Type;
				if (!forceSlotEvenIfEmpty && this.IsEmptyStructType(type))
				{
					return -1;
				}
				num = this.AddVariable(variableIdentifier);
			}
			if (this.IsConditionalState)
			{
				this.Normalize(ref this.StateWhenTrue);
				this.Normalize(ref this.StateWhenFalse);
			}
			else
			{
				this.Normalize(ref this.State);
			}
			return num;
		}

		// Token: 0x06001B85 RID: 7045
		protected abstract void Normalize(ref TLocalState state);

		// Token: 0x06001B86 RID: 7046 RVA: 0x000908C0 File Offset: 0x0008EAC0
		private int DescendThroughTupleRestFields(ref Symbol symbol, int containingSlot, bool forceContainingSlotsToExist)
		{
			TupleElementFieldSymbol tupleElementFieldSymbol = symbol as TupleElementFieldSymbol;
			if (tupleElementFieldSymbol != null)
			{
				TypeSymbol typeSymbol = symbol.ContainingType;
				symbol = tupleElementFieldSymbol.TupleUnderlyingField;
				while (!TypeSymbol.Equals(typeSymbol, symbol.ContainingType, TypeCompareKind.ConsiderEverything))
				{
					FieldSymbol fieldSymbol = typeSymbol.GetMembers("Rest").FirstOrDefault<Symbol>() as FieldSymbol;
					if (fieldSymbol == null)
					{
						return -1;
					}
					if (forceContainingSlotsToExist)
					{
						containingSlot = this.GetOrCreateSlot(fieldSymbol, containingSlot, false, true);
					}
					else if (!this.TryGetVariable(new LocalDataFlowPass<TLocalState, TLocalFunctionState>.VariableIdentifier(fieldSymbol, containingSlot), out containingSlot))
					{
						return -1;
					}
					typeSymbol = fieldSymbol.Type;
				}
			}
			return containingSlot;
		}

		// Token: 0x06001B87 RID: 7047
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected abstract bool TryGetReceiverAndMember([global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression expr, out BoundExpression receiver, [NotNullWhen(true)] out Symbol member);

		// Token: 0x06001B88 RID: 7048 RVA: 0x00090944 File Offset: 0x0008EB44
		protected virtual int MakeSlot(BoundExpression node)
		{
			BoundKind kind = node.Kind;
			if (kind <= BoundKind.Parameter)
			{
				if (kind == BoundKind.AssignmentOperator)
				{
					return this.MakeSlot(((BoundAssignmentOperator)node).Left);
				}
				switch (kind)
				{
				case BoundKind.ThisReference:
				case BoundKind.BaseReference:
					if (base.MethodThisParameter == null)
					{
						return -1;
					}
					return this.GetOrCreateSlot(base.MethodThisParameter, 0, false, true);
				case BoundKind.Local:
					return this.GetOrCreateSlot(((BoundLocal)node).LocalSymbol, 0, false, true);
				case BoundKind.RangeVariable:
					return this.MakeSlot(((BoundRangeVariable)node).Value);
				case BoundKind.Parameter:
					return this.GetOrCreateSlot(((BoundParameter)node).ParameterSymbol, 0, false, true);
				}
			}
			else if (kind == BoundKind.FieldAccess || kind - BoundKind.PropertyAccess <= 1)
			{
				BoundExpression boundExpression;
				Symbol symbol;
				if (this.TryGetReceiverAndMember(node, out boundExpression, out symbol))
				{
					return this.MakeMemberSlot(boundExpression, symbol);
				}
			}
			return -1;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00090A28 File Offset: 0x0008EC28
		protected int MakeMemberSlot([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, Symbol member)
		{
			int num;
			if (member.RequiresInstanceReceiver())
			{
				if (receiverOpt == null)
				{
					return -1;
				}
				num = this.MakeSlot(receiverOpt);
				if (num < 0)
				{
					return -1;
				}
			}
			else
			{
				num = 0;
			}
			return this.GetOrCreateSlot(member, num, false, true);
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00090A5C File Offset: 0x0008EC5C
		protected static bool HasInitializer(Symbol field)
		{
			SourceMemberFieldSymbol sourceMemberFieldSymbol = field as SourceMemberFieldSymbol;
			bool flag;
			if (sourceMemberFieldSymbol == null)
			{
				SynthesizedBackingFieldSymbol synthesizedBackingFieldSymbol = field as SynthesizedBackingFieldSymbol;
				if (synthesizedBackingFieldSymbol == null)
				{
					SourceFieldLikeEventSymbol sourceFieldLikeEventSymbol = field as SourceFieldLikeEventSymbol;
					if (sourceFieldLikeEventSymbol == null)
					{
						flag = false;
					}
					else
					{
						SourceEventFieldSymbol associatedEventField = sourceFieldLikeEventSymbol.AssociatedEventField;
						flag = associatedEventField != null && associatedEventField.HasInitializer;
					}
				}
				else
				{
					flag = synthesizedBackingFieldSymbol.HasInitializer;
				}
			}
			else
			{
				flag = sourceMemberFieldSymbol.HasInitializer;
			}
			return flag;
		}

		// Token: 0x04000E3A RID: 3642
		protected readonly EmptyStructTypeCache _emptyStructTypeCache;

		// Token: 0x020006EB RID: 1771
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal readonly struct VariableIdentifier : IEquatable<LocalDataFlowPass<TLocalState, TLocalFunctionState>.VariableIdentifier>
		{
			// Token: 0x060084D1 RID: 34001 RVA: 0x001E0FAB File Offset: 0x001DF1AB
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			public VariableIdentifier(Symbol symbol, int containingSlot = 0)
			{
				this.Symbol = symbol;
				this.ContainingSlot = containingSlot;
			}

			// Token: 0x170026A8 RID: 9896
			// (get) Token: 0x060084D2 RID: 34002 RVA: 0x001E0FBB File Offset: 0x001DF1BB
			public bool Exists
			{
				get
				{
					return this.Symbol != null;
				}
			}

			// Token: 0x060084D3 RID: 34003 RVA: 0x001E0FC8 File Offset: 0x001DF1C8
			public override int GetHashCode()
			{
				int containingSlot = this.ContainingSlot;
				int? memberIndexOpt = this.Symbol.MemberIndexOpt;
				if (memberIndexOpt == null)
				{
					return Hash.Combine<Symbol>(this.Symbol.OriginalDefinition, containingSlot);
				}
				return Hash.Combine(memberIndexOpt.GetValueOrDefault(), containingSlot);
			}

			// Token: 0x060084D4 RID: 34004 RVA: 0x001E1010 File Offset: 0x001DF210
			public bool Equals(LocalDataFlowPass<TLocalState, TLocalFunctionState>.VariableIdentifier other)
			{
				if (this.ContainingSlot != other.ContainingSlot)
				{
					return false;
				}
				int? memberIndexOpt = this.Symbol.MemberIndexOpt;
				int? memberIndexOpt2 = other.Symbol.MemberIndexOpt;
				int? num = memberIndexOpt;
				int? num2 = memberIndexOpt2;
				return ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))) && (memberIndexOpt != null || this.Symbol.Equals(other.Symbol, TypeCompareKind.AllIgnoreOptions));
			}

			// Token: 0x060084D5 RID: 34005 RVA: 0x001E108A File Offset: 0x001DF28A
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public override bool Equals(object obj)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x060084D6 RID: 34006 RVA: 0x001E1091 File Offset: 0x001DF291
			[Obsolete]
			public static bool operator ==(LocalDataFlowPass<TLocalState, TLocalFunctionState>.VariableIdentifier left, LocalDataFlowPass<TLocalState, TLocalFunctionState>.VariableIdentifier right)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x060084D7 RID: 34007 RVA: 0x001E1098 File Offset: 0x001DF298
			[Obsolete]
			public static bool operator !=(LocalDataFlowPass<TLocalState, TLocalFunctionState>.VariableIdentifier left, LocalDataFlowPass<TLocalState, TLocalFunctionState>.VariableIdentifier right)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x060084D8 RID: 34008 RVA: 0x001E109F File Offset: 0x001DF29F
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			public override string ToString()
			{
				return string.Format("ContainingSlot={0}, Symbol={1}", this.ContainingSlot, this.Symbol.GetDebuggerDisplay());
			}

			// Token: 0x040021EC RID: 8684
			[global::System.Runtime.CompilerServices.Nullable(1)]
			public readonly Symbol Symbol;

			// Token: 0x040021ED RID: 8685
			public readonly int ContainingSlot;
		}

		// Token: 0x020006EC RID: 1772
		internal interface ILocalDataFlowState : AbstractFlowPass<TLocalState, TLocalFunctionState>.ILocalState
		{
			// Token: 0x170026A9 RID: 9897
			// (get) Token: 0x060084D9 RID: 34009
			bool NormalizeToBottom { get; }
		}
	}
}
