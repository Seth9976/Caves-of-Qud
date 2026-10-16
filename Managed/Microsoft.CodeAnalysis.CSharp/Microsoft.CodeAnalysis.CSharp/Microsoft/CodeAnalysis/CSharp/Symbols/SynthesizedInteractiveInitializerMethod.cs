using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000595 RID: 1429
	internal sealed class SynthesizedInteractiveInitializerMethod : SynthesizedInstanceMethodSymbol
	{
		// Token: 0x0600776F RID: 30575 RVA: 0x001B8223 File Offset: 0x001B6423
		internal SynthesizedInteractiveInitializerMethod(SourceMemberContainerTypeSymbol containingType, BindingDiagnosticBag diagnostics)
		{
			this._containingType = containingType;
			SynthesizedInteractiveInitializerMethod.CalculateReturnType(containingType, diagnostics, out this._resultType, out this._returnType);
		}

		// Token: 0x1700219E RID: 8606
		// (get) Token: 0x06007770 RID: 30576 RVA: 0x001B8245 File Offset: 0x001B6445
		public override string Name
		{
			get
			{
				return "<Initialize>";
			}
		}

		// Token: 0x1700219F RID: 8607
		// (get) Token: 0x06007771 RID: 30577 RVA: 0x001B824C File Offset: 0x001B644C
		internal override bool IsScriptInitializer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170021A0 RID: 8608
		// (get) Token: 0x06007772 RID: 30578 RVA: 0x001B8250 File Offset: 0x001B6450
		public override int Arity
		{
			get
			{
				return this.TypeParameters.Length;
			}
		}

		// Token: 0x170021A1 RID: 8609
		// (get) Token: 0x06007773 RID: 30579 RVA: 0x001B826B File Offset: 0x001B646B
		public override Symbol AssociatedSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170021A2 RID: 8610
		// (get) Token: 0x06007774 RID: 30580 RVA: 0x001B826E File Offset: 0x001B646E
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x170021A3 RID: 8611
		// (get) Token: 0x06007775 RID: 30581 RVA: 0x001B8276 File Offset: 0x001B6476
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Internal;
			}
		}

		// Token: 0x170021A4 RID: 8612
		// (get) Token: 0x06007776 RID: 30582 RVA: 0x001B8279 File Offset: 0x001B6479
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x170021A5 RID: 8613
		// (get) Token: 0x06007777 RID: 30583 RVA: 0x001B8280 File Offset: 0x001B6480
		public override bool HidesBaseMethodsByName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021A6 RID: 8614
		// (get) Token: 0x06007778 RID: 30584 RVA: 0x001B8283 File Offset: 0x001B6483
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021A7 RID: 8615
		// (get) Token: 0x06007779 RID: 30585 RVA: 0x001B8286 File Offset: 0x001B6486
		public override bool IsAsync
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170021A8 RID: 8616
		// (get) Token: 0x0600777A RID: 30586 RVA: 0x001B8289 File Offset: 0x001B6489
		public override bool IsExtensionMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021A9 RID: 8617
		// (get) Token: 0x0600777B RID: 30587 RVA: 0x001B828C File Offset: 0x001B648C
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021AA RID: 8618
		// (get) Token: 0x0600777C RID: 30588 RVA: 0x001B828F File Offset: 0x001B648F
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021AB RID: 8619
		// (get) Token: 0x0600777D RID: 30589 RVA: 0x001B8292 File Offset: 0x001B6492
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021AC RID: 8620
		// (get) Token: 0x0600777E RID: 30590 RVA: 0x001B8295 File Offset: 0x001B6495
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021AD RID: 8621
		// (get) Token: 0x0600777F RID: 30591 RVA: 0x001B8298 File Offset: 0x001B6498
		public override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021AE RID: 8622
		// (get) Token: 0x06007780 RID: 30592 RVA: 0x001B829B File Offset: 0x001B649B
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x170021AF RID: 8623
		// (get) Token: 0x06007781 RID: 30593 RVA: 0x001B829E File Offset: 0x001B649E
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021B0 RID: 8624
		// (get) Token: 0x06007782 RID: 30594 RVA: 0x001B82A1 File Offset: 0x001B64A1
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._containingType.Locations;
			}
		}

		// Token: 0x170021B1 RID: 8625
		// (get) Token: 0x06007783 RID: 30595 RVA: 0x001B82AE File Offset: 0x001B64AE
		public override MethodKind MethodKind
		{
			get
			{
				return MethodKind.Ordinary;
			}
		}

		// Token: 0x170021B2 RID: 8626
		// (get) Token: 0x06007784 RID: 30596 RVA: 0x001B82B2 File Offset: 0x001B64B2
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
		}

		// Token: 0x170021B3 RID: 8627
		// (get) Token: 0x06007785 RID: 30597 RVA: 0x001B82B9 File Offset: 0x001B64B9
		public override bool ReturnsVoid
		{
			get
			{
				return this._returnType.IsVoidType();
			}
		}

		// Token: 0x170021B4 RID: 8628
		// (get) Token: 0x06007786 RID: 30598 RVA: 0x001B82C8 File Offset: 0x001B64C8
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return TypeWithAnnotations.Create(this._returnType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
		}

		// Token: 0x170021B5 RID: 8629
		// (get) Token: 0x06007787 RID: 30599 RVA: 0x001B82EA File Offset: 0x001B64EA
		public override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x170021B6 RID: 8630
		// (get) Token: 0x06007788 RID: 30600 RVA: 0x001B82ED File Offset: 0x001B64ED
		public override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x170021B7 RID: 8631
		// (get) Token: 0x06007789 RID: 30601 RVA: 0x001B82F4 File Offset: 0x001B64F4
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x170021B8 RID: 8632
		// (get) Token: 0x0600778A RID: 30602 RVA: 0x001B82FB File Offset: 0x001B64FB
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x170021B9 RID: 8633
		// (get) Token: 0x0600778B RID: 30603 RVA: 0x001B8302 File Offset: 0x001B6502
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x170021BA RID: 8634
		// (get) Token: 0x0600778C RID: 30604 RVA: 0x001B8309 File Offset: 0x001B6509
		internal override CallingConvention CallingConvention
		{
			get
			{
				return CallingConvention.HasThis;
			}
		}

		// Token: 0x170021BB RID: 8635
		// (get) Token: 0x0600778D RID: 30605 RVA: 0x001B830D File Offset: 0x001B650D
		internal override bool GenerateDebugInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170021BC RID: 8636
		// (get) Token: 0x0600778E RID: 30606 RVA: 0x001B8310 File Offset: 0x001B6510
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021BD RID: 8637
		// (get) Token: 0x0600778F RID: 30607 RVA: 0x001B8313 File Offset: 0x001B6513
		internal override bool HasSpecialName
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170021BE RID: 8638
		// (get) Token: 0x06007790 RID: 30608 RVA: 0x001B8316 File Offset: 0x001B6516
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170021BF RID: 8639
		// (get) Token: 0x06007791 RID: 30609 RVA: 0x001B8319 File Offset: 0x001B6519
		internal override bool RequiresSecurityObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021C0 RID: 8640
		// (get) Token: 0x06007792 RID: 30610 RVA: 0x001B831C File Offset: 0x001B651C
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007793 RID: 30611 RVA: 0x001B831F File Offset: 0x001B651F
		public override DllImportData GetDllImportData()
		{
			return null;
		}

		// Token: 0x06007794 RID: 30612 RVA: 0x001B8322 File Offset: 0x001B6522
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x06007795 RID: 30613 RVA: 0x001B8329 File Offset: 0x001B6529
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06007796 RID: 30614 RVA: 0x001B8330 File Offset: 0x001B6530
		internal override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x06007797 RID: 30615 RVA: 0x001B8333 File Offset: 0x001B6533
		internal override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x06007798 RID: 30616 RVA: 0x001B8336 File Offset: 0x001B6536
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			return this._containingType.CalculateSyntaxOffsetInSynthesizedConstructor(localPosition, localTree, false);
		}

		// Token: 0x170021C1 RID: 8641
		// (get) Token: 0x06007799 RID: 30617 RVA: 0x001B8346 File Offset: 0x001B6546
		internal TypeSymbol ResultType
		{
			get
			{
				return this._resultType;
			}
		}

		// Token: 0x0600779A RID: 30618 RVA: 0x001B8350 File Offset: 0x001B6550
		internal override bool IsNullableAnalysisEnabled()
		{
			if (this._lazyIsNullableAnalysisEnabled == ThreeState.Unknown)
			{
				CSharpCompilation declaringCompilation = this.DeclaringCompilation;
				bool flag;
				if (declaringCompilation.Options.NullableContextOptions == NullableContextOptions.Disable)
				{
					flag = declaringCompilation.SyntaxTrees.Any<SyntaxTree>(delegate(SyntaxTree tree)
					{
						bool? flag3 = ((CSharpSyntaxTree)tree).IsNullableAnalysisEnabled(new TextSpan(0, tree.Length));
						bool flag4 = true;
						return (flag3.GetValueOrDefault() == flag4) & (flag3 != null);
					});
				}
				else
				{
					flag = true;
				}
				bool flag2 = flag;
				this._lazyIsNullableAnalysisEnabled = flag2.ToThreeState();
			}
			return this._lazyIsNullableAnalysisEnabled == ThreeState.True;
		}

		// Token: 0x0600779B RID: 30619 RVA: 0x001B83BC File Offset: 0x001B65BC
		private static void CalculateReturnType(SourceMemberContainerTypeSymbol containingType, BindingDiagnosticBag diagnostics, out TypeSymbol resultType, out TypeSymbol returnType)
		{
			CSharpCompilation declaringCompilation = containingType.DeclaringCompilation;
			CSharpScriptCompilationInfo scriptCompilationInfo = declaringCompilation.ScriptCompilationInfo;
			Type type = ((scriptCompilationInfo != null) ? scriptCompilationInfo.ReturnTypeOpt : null);
			NamedTypeSymbol wellKnownType = declaringCompilation.GetWellKnownType(WellKnownType.System_Threading_Tasks_Task_T);
			diagnostics.ReportUseSite(wellKnownType, NoLocation.Singleton);
			resultType = ((type == null) ? declaringCompilation.GetSpecialType(SpecialType.System_Object) : declaringCompilation.GetTypeByReflectionType(type, diagnostics));
			returnType = wellKnownType.Construct(new TypeSymbol[] { resultType });
		}

		// Token: 0x04001CDC RID: 7388
		internal const string InitializerName = "<Initialize>";

		// Token: 0x04001CDD RID: 7389
		private readonly SourceMemberContainerTypeSymbol _containingType;

		// Token: 0x04001CDE RID: 7390
		private readonly TypeSymbol _resultType;

		// Token: 0x04001CDF RID: 7391
		private readonly TypeSymbol _returnType;

		// Token: 0x04001CE0 RID: 7392
		private ThreeState _lazyIsNullableAnalysisEnabled;
	}
}
