using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001FE RID: 510
	internal sealed class IteratorFinallyMethodSymbol : SynthesizedInstanceMethodSymbol, ISynthesizedMethodBodyImplementationSymbol, ISymbolInternal
	{
		// Token: 0x0600235C RID: 9052 RVA: 0x000B8CA1 File Offset: 0x000B6EA1
		public IteratorFinallyMethodSymbol(IteratorStateMachine stateMachineType, string name)
		{
			this._stateMachineType = stateMachineType;
			this._name = name;
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x000B8CB7 File Offset: 0x000B6EB7
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000B8CBF File Offset: 0x000B6EBF
		internal override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000B8CC2 File Offset: 0x000B6EC2
		internal override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x000B8CC5 File Offset: 0x000B6EC5
		internal override bool IsMetadataFinal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002361 RID: 9057 RVA: 0x000B8CC8 File Offset: 0x000B6EC8
		public override MethodKind MethodKind
		{
			get
			{
				return MethodKind.Ordinary;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x000B8CCC File Offset: 0x000B6ECC
		public override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x000B8CCF File Offset: 0x000B6ECF
		public override bool IsExtensionMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002364 RID: 9060 RVA: 0x000B8CD2 File Offset: 0x000B6ED2
		internal override bool HasSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002365 RID: 9061 RVA: 0x000B8CD5 File Offset: 0x000B6ED5
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06002366 RID: 9062 RVA: 0x000B8CD8 File Offset: 0x000B6ED8
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000B8CDB File Offset: 0x000B6EDB
		public override DllImportData GetDllImportData()
		{
			return null;
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000B8CDE File Offset: 0x000B6EDE
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x000B8CE5 File Offset: 0x000B6EE5
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x000B8CE8 File Offset: 0x000B6EE8
		internal override bool RequiresSecurityObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x000B8CEB File Offset: 0x000B6EEB
		public override bool HidesBaseMethodsByName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x000B8CEE File Offset: 0x000B6EEE
		public override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x000B8CF1 File Offset: 0x000B6EF1
		public override bool ReturnsVoid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600236E RID: 9070 RVA: 0x000B8CF4 File Offset: 0x000B6EF4
		public override bool IsAsync
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x000B8CF7 File Offset: 0x000B6EF7
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06002370 RID: 9072 RVA: 0x000B8CFC File Offset: 0x000B6EFC
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return TypeWithAnnotations.Create(this.ContainingAssembly.GetSpecialType(SpecialType.System_Void), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x000B8D24 File Offset: 0x000B6F24
		public override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002372 RID: 9074 RVA: 0x000B8D27 File Offset: 0x000B6F27
		public override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x000B8D2E File Offset: 0x000B6F2E
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x000B8D35 File Offset: 0x000B6F35
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x000B8D3C File Offset: 0x000B6F3C
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x000B8D43 File Offset: 0x000B6F43
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x000B8D4A File Offset: 0x000B6F4A
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06002378 RID: 9080 RVA: 0x000B8D51 File Offset: 0x000B6F51
		public override Symbol AssociatedSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000B8D54 File Offset: 0x000B6F54
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x000B8D5B File Offset: 0x000B6F5B
		internal override CallingConvention CallingConvention
		{
			get
			{
				return CallingConvention.HasThis;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x000B8D5F File Offset: 0x000B6F5F
		internal override bool GenerateDebugInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x000B8D62 File Offset: 0x000B6F62
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._stateMachineType;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x000B8D6A File Offset: 0x000B6F6A
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this.ContainingType.Locations;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x000B8D77 File Offset: 0x000B6F77
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Private;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x000B8D7A File Offset: 0x000B6F7A
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x000B8D7D File Offset: 0x000B6F7D
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x000B8D80 File Offset: 0x000B6F80
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x000B8D83 File Offset: 0x000B6F83
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x000B8D86 File Offset: 0x000B6F86
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x000B8D89 File Offset: 0x000B6F89
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x000B8D8C File Offset: 0x000B6F8C
		IMethodSymbolInternal ISynthesizedMethodBodyImplementationSymbol.Method
		{
			get
			{
				return this._stateMachineType.KickoffMethod;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x000B8D99 File Offset: 0x000B6F99
		bool ISynthesizedMethodBodyImplementationSymbol.HasMethodBodyDependency
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x000B8D9C File Offset: 0x000B6F9C
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			return this._stateMachineType.KickoffMethod.CalculateLocalSyntaxOffset(localPosition, localTree);
		}

		// Token: 0x040010B6 RID: 4278
		private readonly IteratorStateMachine _stateMachineType;

		// Token: 0x040010B7 RID: 4279
		private readonly string _name;
	}
}
