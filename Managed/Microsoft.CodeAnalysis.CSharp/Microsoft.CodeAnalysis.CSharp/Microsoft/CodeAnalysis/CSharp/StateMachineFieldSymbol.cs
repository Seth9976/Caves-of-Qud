using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200020E RID: 526
	internal sealed class StateMachineFieldSymbol : SynthesizedFieldSymbolBase, ISynthesizedMethodBodyImplementationSymbol, ISymbolInternal
	{
		// Token: 0x060025ED RID: 9709 RVA: 0x000CF8EE File Offset: 0x000CDAEE
		public StateMachineFieldSymbol(NamedTypeSymbol stateMachineType, TypeWithAnnotations type, string name, bool isPublic, bool isThis)
			: this(stateMachineType, type, name, new LocalSlotDebugInfo(SynthesizedLocalKind.LoweringTemp, LocalDebugId.None), -1, isPublic)
		{
			this._isThis = isThis;
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000CF910 File Offset: 0x000CDB10
		public StateMachineFieldSymbol(NamedTypeSymbol stateMachineType, TypeSymbol type, string name, SynthesizedLocalKind synthesizedKind, int slotIndex, bool isPublic)
			: this(stateMachineType, type, name, new LocalSlotDebugInfo(synthesizedKind, LocalDebugId.None), slotIndex, isPublic)
		{
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x000CF92C File Offset: 0x000CDB2C
		public StateMachineFieldSymbol(NamedTypeSymbol stateMachineType, TypeSymbol type, string name, LocalSlotDebugInfo slotDebugInfo, int slotIndex, bool isPublic)
			: this(stateMachineType, TypeWithAnnotations.Create(type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), name, slotDebugInfo, slotIndex, isPublic)
		{
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x000CF957 File Offset: 0x000CDB57
		public StateMachineFieldSymbol(NamedTypeSymbol stateMachineType, TypeWithAnnotations type, string name, LocalSlotDebugInfo slotDebugInfo, int slotIndex, bool isPublic)
			: base(stateMachineType, name, isPublic, false, false)
		{
			this._type = type;
			this.SlotIndex = slotIndex;
			this.SlotDebugInfo = slotDebugInfo;
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x000CF97C File Offset: 0x000CDB7C
		internal override bool SuppressDynamicAttribute
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000CF97F File Offset: 0x000CDB7F
		internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			return this._type;
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060025F3 RID: 9715 RVA: 0x000CF987 File Offset: 0x000CDB87
		bool ISynthesizedMethodBodyImplementationSymbol.HasMethodBodyDependency
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x000CF98A File Offset: 0x000CDB8A
		IMethodSymbolInternal ISynthesizedMethodBodyImplementationSymbol.Method
		{
			get
			{
				return ((ISynthesizedMethodBodyImplementationSymbol)this.ContainingSymbol).Method;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060025F5 RID: 9717 RVA: 0x000CF99C File Offset: 0x000CDB9C
		internal override bool IsCapturedFrame
		{
			get
			{
				return this._isThis;
			}
		}

		// Token: 0x040010FF RID: 4351
		private readonly TypeWithAnnotations _type;

		// Token: 0x04001100 RID: 4352
		private readonly bool _isThis;

		// Token: 0x04001101 RID: 4353
		internal readonly int SlotIndex;

		// Token: 0x04001102 RID: 4354
		internal readonly LocalSlotDebugInfo SlotDebugInfo;
	}
}
