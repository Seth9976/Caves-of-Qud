using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000591 RID: 1425
	internal abstract class SynthesizedGlobalMethodSymbol : MethodSymbol
	{
		// Token: 0x060076CC RID: 30412 RVA: 0x001B7C6D File Offset: 0x001B5E6D
		internal SynthesizedGlobalMethodSymbol(ModuleSymbol containingModule, PrivateImplementationDetails privateImplType, TypeSymbol returnType, string name)
		{
			this._containingModule = containingModule;
			this._privateImplType = privateImplType;
			this._returnType = returnType;
			this._name = name;
		}

		// Token: 0x060076CD RID: 30413 RVA: 0x001B7C92 File Offset: 0x001B5E92
		protected void SetParameters(ImmutableArray<ParameterSymbol> parameters)
		{
			ImmutableInterlocked.InterlockedExchange<ParameterSymbol>(ref this._parameters, parameters);
		}

		// Token: 0x1700211D RID: 8477
		// (get) Token: 0x060076CE RID: 30414 RVA: 0x001B7CA1 File Offset: 0x001B5EA1
		public sealed override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700211E RID: 8478
		// (get) Token: 0x060076CF RID: 30415 RVA: 0x001B7CA4 File Offset: 0x001B5EA4
		internal sealed override bool GenerateDebugInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700211F RID: 8479
		// (get) Token: 0x060076D0 RID: 30416 RVA: 0x001B7CA7 File Offset: 0x001B5EA7
		internal sealed override ModuleSymbol ContainingModule
		{
			get
			{
				return this._containingModule;
			}
		}

		// Token: 0x17002120 RID: 8480
		// (get) Token: 0x060076D1 RID: 30417 RVA: 0x001B7CAF File Offset: 0x001B5EAF
		public sealed override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._containingModule.ContainingAssembly;
			}
		}

		// Token: 0x17002121 RID: 8481
		// (get) Token: 0x060076D2 RID: 30418 RVA: 0x001B7CBC File Offset: 0x001B5EBC
		public sealed override Symbol ContainingSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002122 RID: 8482
		// (get) Token: 0x060076D3 RID: 30419 RVA: 0x001B7CBF File Offset: 0x001B5EBF
		public sealed override NamedTypeSymbol ContainingType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002123 RID: 8483
		// (get) Token: 0x060076D4 RID: 30420 RVA: 0x001B7CC2 File Offset: 0x001B5EC2
		internal PrivateImplementationDetails ContainingPrivateImplementationDetailsType
		{
			get
			{
				return this._privateImplType;
			}
		}

		// Token: 0x17002124 RID: 8484
		// (get) Token: 0x060076D5 RID: 30421 RVA: 0x001B7CCA File Offset: 0x001B5ECA
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17002125 RID: 8485
		// (get) Token: 0x060076D6 RID: 30422 RVA: 0x001B7CD2 File Offset: 0x001B5ED2
		internal override bool HasSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002126 RID: 8486
		// (get) Token: 0x060076D7 RID: 30423 RVA: 0x001B7CD5 File Offset: 0x001B5ED5
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002127 RID: 8487
		// (get) Token: 0x060076D8 RID: 30424 RVA: 0x001B7CD8 File Offset: 0x001B5ED8
		internal override bool RequiresSecurityObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060076D9 RID: 30425 RVA: 0x001B7CDB File Offset: 0x001B5EDB
		public override DllImportData GetDllImportData()
		{
			return null;
		}

		// Token: 0x17002128 RID: 8488
		// (get) Token: 0x060076DA RID: 30426 RVA: 0x001B7CDE File Offset: 0x001B5EDE
		public sealed override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17002129 RID: 8489
		// (get) Token: 0x060076DB RID: 30427 RVA: 0x001B7CE1 File Offset: 0x001B5EE1
		public sealed override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x1700212A RID: 8490
		// (get) Token: 0x060076DC RID: 30428 RVA: 0x001B7CE8 File Offset: 0x001B5EE8
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				return this.ContainingModule.AreLocalsZeroed;
			}
		}

		// Token: 0x1700212B RID: 8491
		// (get) Token: 0x060076DD RID: 30429 RVA: 0x001B7CF5 File Offset: 0x001B5EF5
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700212C RID: 8492
		// (get) Token: 0x060076DE RID: 30430 RVA: 0x001B7CF8 File Offset: 0x001B5EF8
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060076DF RID: 30431 RVA: 0x001B7CFB File Offset: 0x001B5EFB
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700212D RID: 8493
		// (get) Token: 0x060076E0 RID: 30432 RVA: 0x001B7D02 File Offset: 0x001B5F02
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060076E1 RID: 30433 RVA: 0x001B7D05 File Offset: 0x001B5F05
		internal sealed override UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete)
		{
			return null;
		}

		// Token: 0x060076E2 RID: 30434 RVA: 0x001B7D08 File Offset: 0x001B5F08
		internal sealed override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x1700212E RID: 8494
		// (get) Token: 0x060076E3 RID: 30435 RVA: 0x001B7D0F File Offset: 0x001B5F0F
		public override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700212F RID: 8495
		// (get) Token: 0x060076E4 RID: 30436 RVA: 0x001B7D12 File Offset: 0x001B5F12
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x17002130 RID: 8496
		// (get) Token: 0x060076E5 RID: 30437 RVA: 0x001B7D19 File Offset: 0x001B5F19
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				if (this._parameters.IsEmpty)
				{
					return ImmutableArray<ParameterSymbol>.Empty;
				}
				return this._parameters;
			}
		}

		// Token: 0x17002131 RID: 8497
		// (get) Token: 0x060076E6 RID: 30438 RVA: 0x001B7D34 File Offset: 0x001B5F34
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Internal;
			}
		}

		// Token: 0x17002132 RID: 8498
		// (get) Token: 0x060076E7 RID: 30439 RVA: 0x001B7D37 File Offset: 0x001B5F37
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17002133 RID: 8499
		// (get) Token: 0x060076E8 RID: 30440 RVA: 0x001B7D3E File Offset: 0x001B5F3E
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17002134 RID: 8500
		// (get) Token: 0x060076E9 RID: 30441 RVA: 0x001B7D45 File Offset: 0x001B5F45
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x17002135 RID: 8501
		// (get) Token: 0x060076EA RID: 30442 RVA: 0x001B7D48 File Offset: 0x001B5F48
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return TypeWithAnnotations.Create(this._returnType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
		}

		// Token: 0x17002136 RID: 8502
		// (get) Token: 0x060076EB RID: 30443 RVA: 0x001B7D6A File Offset: 0x001B5F6A
		public sealed override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17002137 RID: 8503
		// (get) Token: 0x060076EC RID: 30444 RVA: 0x001B7D6D File Offset: 0x001B5F6D
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x17002138 RID: 8504
		// (get) Token: 0x060076ED RID: 30445 RVA: 0x001B7D74 File Offset: 0x001B5F74
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x17002139 RID: 8505
		// (get) Token: 0x060076EE RID: 30446 RVA: 0x001B7D7B File Offset: 0x001B5F7B
		public override Symbol AssociatedSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700213A RID: 8506
		// (get) Token: 0x060076EF RID: 30447 RVA: 0x001B7D7E File Offset: 0x001B5F7E
		public override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700213B RID: 8507
		// (get) Token: 0x060076F0 RID: 30448 RVA: 0x001B7D81 File Offset: 0x001B5F81
		public override bool ReturnsVoid
		{
			get
			{
				return base.ReturnType.IsVoidType();
			}
		}

		// Token: 0x1700213C RID: 8508
		// (get) Token: 0x060076F1 RID: 30449 RVA: 0x001B7D8E File Offset: 0x001B5F8E
		public override MethodKind MethodKind
		{
			get
			{
				return MethodKind.Ordinary;
			}
		}

		// Token: 0x1700213D RID: 8509
		// (get) Token: 0x060076F2 RID: 30450 RVA: 0x001B7D92 File Offset: 0x001B5F92
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700213E RID: 8510
		// (get) Token: 0x060076F3 RID: 30451 RVA: 0x001B7D95 File Offset: 0x001B5F95
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700213F RID: 8511
		// (get) Token: 0x060076F4 RID: 30452 RVA: 0x001B7D98 File Offset: 0x001B5F98
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002140 RID: 8512
		// (get) Token: 0x060076F5 RID: 30453 RVA: 0x001B7D9B File Offset: 0x001B5F9B
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002141 RID: 8513
		// (get) Token: 0x060076F6 RID: 30454 RVA: 0x001B7D9E File Offset: 0x001B5F9E
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002142 RID: 8514
		// (get) Token: 0x060076F7 RID: 30455 RVA: 0x001B7DA1 File Offset: 0x001B5FA1
		public override bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002143 RID: 8515
		// (get) Token: 0x060076F8 RID: 30456 RVA: 0x001B7DA4 File Offset: 0x001B5FA4
		public override bool IsAsync
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002144 RID: 8516
		// (get) Token: 0x060076F9 RID: 30457 RVA: 0x001B7DA7 File Offset: 0x001B5FA7
		public override bool HidesBaseMethodsByName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060076FA RID: 30458 RVA: 0x001B7DAA File Offset: 0x001B5FAA
		internal sealed override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x060076FB RID: 30459 RVA: 0x001B7DAD File Offset: 0x001B5FAD
		internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x17002145 RID: 8517
		// (get) Token: 0x060076FC RID: 30460 RVA: 0x001B7DB0 File Offset: 0x001B5FB0
		internal override bool IsMetadataFinal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002146 RID: 8518
		// (get) Token: 0x060076FD RID: 30461 RVA: 0x001B7DB3 File Offset: 0x001B5FB3
		public override bool IsExtensionMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002147 RID: 8519
		// (get) Token: 0x060076FE RID: 30462 RVA: 0x001B7DB6 File Offset: 0x001B5FB6
		internal override CallingConvention CallingConvention
		{
			get
			{
				return CallingConvention.Default;
			}
		}

		// Token: 0x17002148 RID: 8520
		// (get) Token: 0x060076FF RID: 30463 RVA: 0x001B7DB9 File Offset: 0x001B5FB9
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002149 RID: 8521
		// (get) Token: 0x06007700 RID: 30464 RVA: 0x001B7DBC File Offset: 0x001B5FBC
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x1700214A RID: 8522
		// (get) Token: 0x06007701 RID: 30465 RVA: 0x001B7DC3 File Offset: 0x001B5FC3
		internal sealed override bool IsDeclaredReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700214B RID: 8523
		// (get) Token: 0x06007702 RID: 30466 RVA: 0x001B7DC6 File Offset: 0x001B5FC6
		internal sealed override bool IsInitOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700214C RID: 8524
		// (get) Token: 0x06007703 RID: 30467 RVA: 0x001B7DC9 File Offset: 0x001B5FC9
		internal override bool SynthesizesLoweredBoundBody
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007704 RID: 30468
		internal abstract override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics);

		// Token: 0x06007705 RID: 30469 RVA: 0x001B7DCC File Offset: 0x001B5FCC
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06007706 RID: 30470 RVA: 0x001B7DD3 File Offset: 0x001B5FD3
		internal sealed override bool IsNullableAnalysisEnabled()
		{
			return false;
		}

		// Token: 0x04001CCD RID: 7373
		private readonly ModuleSymbol _containingModule;

		// Token: 0x04001CCE RID: 7374
		private readonly PrivateImplementationDetails _privateImplType;

		// Token: 0x04001CCF RID: 7375
		private readonly TypeSymbol _returnType;

		// Token: 0x04001CD0 RID: 7376
		private ImmutableArray<ParameterSymbol> _parameters;

		// Token: 0x04001CD1 RID: 7377
		private readonly string _name;
	}
}
