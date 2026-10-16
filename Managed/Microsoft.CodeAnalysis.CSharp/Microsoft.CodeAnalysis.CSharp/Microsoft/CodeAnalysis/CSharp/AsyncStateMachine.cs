using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001EF RID: 495
	internal sealed class AsyncStateMachine : StateMachineTypeSymbol
	{
		// Token: 0x060021B5 RID: 8629 RVA: 0x000B056C File Offset: 0x000AE76C
		public AsyncStateMachine(VariableSlotAllocator variableAllocatorOpt, TypeCompilationState compilationState, MethodSymbol asyncMethod, int asyncMethodOrdinal, TypeKind typeKind)
			: base(variableAllocatorOpt, compilationState, asyncMethod, asyncMethodOrdinal)
		{
			this._typeKind = typeKind;
			CSharpCompilation declaringCompilation = asyncMethod.DeclaringCompilation;
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			bool isIterator = asyncMethod.IsIterator;
			if (isIterator)
			{
				TypeSymbol type = base.TypeMap.SubstituteType(asyncMethod.IteratorElementTypeWithAnnotations).Type;
				this.IteratorElementType = type;
				if (asyncMethod.IsAsyncReturningIAsyncEnumerable(declaringCompilation))
				{
					instance.Add(declaringCompilation.GetWellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerable_T).Construct(new TypeSymbol[] { type }));
				}
				instance.Add(declaringCompilation.GetWellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerator_T).Construct(new TypeSymbol[] { type }));
				instance.Add(declaringCompilation.GetWellKnownType(WellKnownType.System_Threading_Tasks_Sources_IValueTaskSource_T).Construct(new TypeSymbol[] { declaringCompilation.GetSpecialType(SpecialType.System_Boolean) }));
				instance.Add(declaringCompilation.GetWellKnownType(WellKnownType.System_Threading_Tasks_Sources_IValueTaskSource));
				instance.Add(declaringCompilation.GetWellKnownType(WellKnownType.System_IAsyncDisposable));
			}
			instance.Add(declaringCompilation.GetWellKnownType(WellKnownType.System_Runtime_CompilerServices_IAsyncStateMachine));
			this._interfaces = instance.ToImmutableAndFree();
			this._constructor = (isIterator ? new IteratorConstructor(this) : new AsyncConstructor(this));
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x000B068D File Offset: 0x000AE88D
		public override TypeKind TypeKind
		{
			get
			{
				return this._typeKind;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x000B0695 File Offset: 0x000AE895
		internal override MethodSymbol Constructor
		{
			get
			{
				return this._constructor;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x000B069D File Offset: 0x000AE89D
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x000B06A0 File Offset: 0x000AE8A0
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x000B06A3 File Offset: 0x000AE8A3
		internal override bool HasPossibleWellKnownCloneMethod()
		{
			return false;
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x000B06A6 File Offset: 0x000AE8A6
		internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved)
		{
			return this._interfaces;
		}

		// Token: 0x04001062 RID: 4194
		private readonly TypeKind _typeKind;

		// Token: 0x04001063 RID: 4195
		private readonly MethodSymbol _constructor;

		// Token: 0x04001064 RID: 4196
		private readonly ImmutableArray<NamedTypeSymbol> _interfaces;

		// Token: 0x04001065 RID: 4197
		internal readonly TypeSymbol IteratorElementType;
	}
}
