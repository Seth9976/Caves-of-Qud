using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000202 RID: 514
	internal sealed class IteratorStateMachine : StateMachineTypeSymbol
	{
		// Token: 0x060023A8 RID: 9128 RVA: 0x000BA014 File Offset: 0x000B8214
		public IteratorStateMachine(VariableSlotAllocator slotAllocatorOpt, TypeCompilationState compilationState, MethodSymbol iteratorMethod, int iteratorMethodOrdinal, bool isEnumerable, TypeWithAnnotations elementType)
			: base(slotAllocatorOpt, compilationState, iteratorMethod, iteratorMethodOrdinal)
		{
			this.ElementType = base.TypeMap.SubstituteType(elementType);
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			if (isEnumerable)
			{
				instance.Add(this.ContainingAssembly.GetSpecialType(SpecialType.System_Collections_Generic_IEnumerable_T).Construct(new TypeSymbol[] { this.ElementType.Type }));
				instance.Add(this.ContainingAssembly.GetSpecialType(SpecialType.System_Collections_IEnumerable));
			}
			instance.Add(this.ContainingAssembly.GetSpecialType(SpecialType.System_Collections_Generic_IEnumerator_T).Construct(new TypeSymbol[] { this.ElementType.Type }));
			instance.Add(this.ContainingAssembly.GetSpecialType(SpecialType.System_IDisposable));
			instance.Add(this.ContainingAssembly.GetSpecialType(SpecialType.System_Collections_IEnumerator));
			this._interfaces = instance.ToImmutableAndFree();
			this._constructor = new IteratorConstructor(this);
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060023A9 RID: 9129 RVA: 0x000BA0F2 File Offset: 0x000B82F2
		public override TypeKind TypeKind
		{
			get
			{
				return TypeKind.Class;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x000BA0F5 File Offset: 0x000B82F5
		internal override MethodSymbol Constructor
		{
			get
			{
				return this._constructor;
			}
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000BA0FD File Offset: 0x000B82FD
		internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved)
		{
			return this._interfaces;
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x000BA105 File Offset: 0x000B8305
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				return this.ContainingAssembly.GetSpecialType(SpecialType.System_Object);
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x000BA113 File Offset: 0x000B8313
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x000BA116 File Offset: 0x000B8316
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000BA119 File Offset: 0x000B8319
		internal override bool HasPossibleWellKnownCloneMethod()
		{
			return false;
		}

		// Token: 0x040010C3 RID: 4291
		private readonly MethodSymbol _constructor;

		// Token: 0x040010C4 RID: 4292
		private readonly ImmutableArray<NamedTypeSymbol> _interfaces;

		// Token: 0x040010C5 RID: 4293
		internal readonly TypeWithAnnotations ElementType;
	}
}
