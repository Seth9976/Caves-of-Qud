using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200059D RID: 1437
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedStaticConstructor : MethodSymbol
	{
		// Token: 0x06007831 RID: 30769 RVA: 0x001B8F84 File Offset: 0x001B7184
		internal SynthesizedStaticConstructor(NamedTypeSymbol containingType)
		{
			this._containingType = containingType;
		}

		// Token: 0x17002231 RID: 8753
		// (get) Token: 0x06007832 RID: 30770 RVA: 0x001B8F93 File Offset: 0x001B7193
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002232 RID: 8754
		// (get) Token: 0x06007833 RID: 30771 RVA: 0x001B8F9B File Offset: 0x001B719B
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002233 RID: 8755
		// (get) Token: 0x06007834 RID: 30772 RVA: 0x001B8FA3 File Offset: 0x001B71A3
		public override string Name
		{
			get
			{
				return ".cctor";
			}
		}

		// Token: 0x17002234 RID: 8756
		// (get) Token: 0x06007835 RID: 30773 RVA: 0x001B8FAA File Offset: 0x001B71AA
		internal override bool HasSpecialName
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002235 RID: 8757
		// (get) Token: 0x06007836 RID: 30774 RVA: 0x001B8FAD File Offset: 0x001B71AD
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002236 RID: 8758
		// (get) Token: 0x06007837 RID: 30775 RVA: 0x001B8FB0 File Offset: 0x001B71B0
		public override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002237 RID: 8759
		// (get) Token: 0x06007838 RID: 30776 RVA: 0x001B8FB3 File Offset: 0x001B71B3
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x17002238 RID: 8760
		// (get) Token: 0x06007839 RID: 30777 RVA: 0x001B8FBA File Offset: 0x001B71BA
		internal override int ParameterCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002239 RID: 8761
		// (get) Token: 0x0600783A RID: 30778 RVA: 0x001B8FBD File Offset: 0x001B71BD
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
		}

		// Token: 0x0600783B RID: 30779 RVA: 0x001B8FC4 File Offset: 0x001B71C4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override bool TryGetThisParameter(out ParameterSymbol thisParameter)
		{
			thisParameter = null;
			return true;
		}

		// Token: 0x1700223A RID: 8762
		// (get) Token: 0x0600783C RID: 30780 RVA: 0x001B8FCA File Offset: 0x001B71CA
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Private;
			}
		}

		// Token: 0x0600783D RID: 30781 RVA: 0x001B8FCD File Offset: 0x001B71CD
		internal override LexicalSortKey GetLexicalSortKey()
		{
			return LexicalSortKey.SynthesizedCCtor;
		}

		// Token: 0x1700223B RID: 8763
		// (get) Token: 0x0600783E RID: 30782 RVA: 0x001B8FD4 File Offset: 0x001B71D4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.ContainingType.Locations;
			}
		}

		// Token: 0x1700223C RID: 8764
		// (get) Token: 0x0600783F RID: 30783 RVA: 0x001B8FE1 File Offset: 0x001B71E1
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x1700223D RID: 8765
		// (get) Token: 0x06007840 RID: 30784 RVA: 0x001B8FE8 File Offset: 0x001B71E8
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x1700223E RID: 8766
		// (get) Token: 0x06007841 RID: 30785 RVA: 0x001B8FEC File Offset: 0x001B71EC
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return TypeWithAnnotations.Create(this.ContainingAssembly.GetSpecialType(SpecialType.System_Void), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
		}

		// Token: 0x1700223F RID: 8767
		// (get) Token: 0x06007842 RID: 30786 RVA: 0x001B9014 File Offset: 0x001B7214
		public override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17002240 RID: 8768
		// (get) Token: 0x06007843 RID: 30787 RVA: 0x001B9017 File Offset: 0x001B7217
		public override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x17002241 RID: 8769
		// (get) Token: 0x06007844 RID: 30788 RVA: 0x001B901E File Offset: 0x001B721E
		public override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17002242 RID: 8770
		// (get) Token: 0x06007845 RID: 30789 RVA: 0x001B9021 File Offset: 0x001B7221
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x17002243 RID: 8771
		// (get) Token: 0x06007846 RID: 30790 RVA: 0x001B9028 File Offset: 0x001B7228
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x17002244 RID: 8772
		// (get) Token: 0x06007847 RID: 30791 RVA: 0x001B902F File Offset: 0x001B722F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol AssociatedSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17002245 RID: 8773
		// (get) Token: 0x06007848 RID: 30792 RVA: 0x001B9032 File Offset: 0x001B7232
		public override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002246 RID: 8774
		// (get) Token: 0x06007849 RID: 30793 RVA: 0x001B9035 File Offset: 0x001B7235
		public override bool ReturnsVoid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002247 RID: 8775
		// (get) Token: 0x0600784A RID: 30794 RVA: 0x001B9038 File Offset: 0x001B7238
		public override MethodKind MethodKind
		{
			get
			{
				return MethodKind.StaticConstructor;
			}
		}

		// Token: 0x17002248 RID: 8776
		// (get) Token: 0x0600784B RID: 30795 RVA: 0x001B903C File Offset: 0x001B723C
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002249 RID: 8777
		// (get) Token: 0x0600784C RID: 30796 RVA: 0x001B903F File Offset: 0x001B723F
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700224A RID: 8778
		// (get) Token: 0x0600784D RID: 30797 RVA: 0x001B9042 File Offset: 0x001B7242
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700224B RID: 8779
		// (get) Token: 0x0600784E RID: 30798 RVA: 0x001B9045 File Offset: 0x001B7245
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700224C RID: 8780
		// (get) Token: 0x0600784F RID: 30799 RVA: 0x001B9048 File Offset: 0x001B7248
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700224D RID: 8781
		// (get) Token: 0x06007850 RID: 30800 RVA: 0x001B904B File Offset: 0x001B724B
		public override bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700224E RID: 8782
		// (get) Token: 0x06007851 RID: 30801 RVA: 0x001B904E File Offset: 0x001B724E
		public override bool IsAsync
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700224F RID: 8783
		// (get) Token: 0x06007852 RID: 30802 RVA: 0x001B9051 File Offset: 0x001B7251
		public override bool HidesBaseMethodsByName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002250 RID: 8784
		// (get) Token: 0x06007853 RID: 30803 RVA: 0x001B9054 File Offset: 0x001B7254
		public override bool IsExtensionMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002251 RID: 8785
		// (get) Token: 0x06007854 RID: 30804 RVA: 0x001B9057 File Offset: 0x001B7257
		internal override CallingConvention CallingConvention
		{
			get
			{
				return CallingConvention.Default;
			}
		}

		// Token: 0x17002252 RID: 8786
		// (get) Token: 0x06007855 RID: 30805 RVA: 0x001B905A File Offset: 0x001B725A
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002253 RID: 8787
		// (get) Token: 0x06007856 RID: 30806 RVA: 0x001B905D File Offset: 0x001B725D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x17002254 RID: 8788
		// (get) Token: 0x06007857 RID: 30807 RVA: 0x001B9064 File Offset: 0x001B7264
		internal override bool IsDeclaredReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002255 RID: 8789
		// (get) Token: 0x06007858 RID: 30808 RVA: 0x001B9067 File Offset: 0x001B7267
		internal override bool IsInitOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002256 RID: 8790
		// (get) Token: 0x06007859 RID: 30809 RVA: 0x001B906A File Offset: 0x001B726A
		public sealed override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002257 RID: 8791
		// (get) Token: 0x0600785A RID: 30810 RVA: 0x001B906D File Offset: 0x001B726D
		internal sealed override bool GenerateDebugInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600785B RID: 30811 RVA: 0x001B9070 File Offset: 0x001B7270
		internal sealed override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x0600785C RID: 30812 RVA: 0x001B9073 File Offset: 0x001B7273
		internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x17002258 RID: 8792
		// (get) Token: 0x0600785D RID: 30813 RVA: 0x001B9076 File Offset: 0x001B7276
		internal override bool IsMetadataFinal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002259 RID: 8793
		// (get) Token: 0x0600785E RID: 30814 RVA: 0x001B9079 File Offset: 0x001B7279
		internal override bool RequiresSecurityObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600785F RID: 30815 RVA: 0x001B907C File Offset: 0x001B727C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override DllImportData GetDllImportData()
		{
			return null;
		}

		// Token: 0x1700225A RID: 8794
		// (get) Token: 0x06007860 RID: 30816 RVA: 0x001B907F File Offset: 0x001B727F
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				return this.ContainingType.AreLocalsZeroed;
			}
		}

		// Token: 0x1700225B RID: 8795
		// (get) Token: 0x06007861 RID: 30817 RVA: 0x001B908C File Offset: 0x001B728C
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x1700225C RID: 8796
		// (get) Token: 0x06007862 RID: 30818 RVA: 0x001B908F File Offset: 0x001B728F
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007863 RID: 30819 RVA: 0x001B9092 File Offset: 0x001B7292
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700225D RID: 8797
		// (get) Token: 0x06007864 RID: 30820 RVA: 0x001B9099 File Offset: 0x001B7299
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06007865 RID: 30821 RVA: 0x001B909C File Offset: 0x001B729C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal sealed override UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete)
		{
			return null;
		}

		// Token: 0x06007866 RID: 30822 RVA: 0x001B909F File Offset: 0x001B729F
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x06007867 RID: 30823 RVA: 0x001B90A6 File Offset: 0x001B72A6
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			return ((SourceMemberContainerTypeSymbol)this.ContainingType).CalculateSyntaxOffsetInSynthesizedConstructor(localPosition, localTree, true);
		}

		// Token: 0x06007868 RID: 30824 RVA: 0x001B90BB File Offset: 0x001B72BB
		internal sealed override bool IsNullableAnalysisEnabled()
		{
			SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = this.ContainingType as SourceMemberContainerTypeSymbol;
			return sourceMemberContainerTypeSymbol != null && sourceMemberContainerTypeSymbol.IsNullableEnabledForConstructorsAndInitializers(true);
		}

		// Token: 0x06007869 RID: 30825 RVA: 0x001B90D4 File Offset: 0x001B72D4
		internal bool ShouldEmit([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundInitializer> boundInitializersOpt = default(ImmutableArray<BoundInitializer>))
		{
			if (this._lazyShouldEmit.HasValue())
			{
				return this._lazyShouldEmit.Value();
			}
			bool flag = this.CalculateShouldEmit(boundInitializersOpt);
			this._lazyShouldEmit = flag.ToThreeState();
			return flag;
		}

		// Token: 0x0600786A RID: 30826 RVA: 0x001B9110 File Offset: 0x001B7310
		private bool CalculateShouldEmit([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundInitializer> boundInitializersOpt = default(ImmutableArray<BoundInitializer>))
		{
			if (boundInitializersOpt.IsDefault)
			{
				SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = this.ContainingType as SourceMemberContainerTypeSymbol;
				if (sourceMemberContainerTypeSymbol == null)
				{
					return true;
				}
				ImportChain importChain;
				boundInitializersOpt = Binder.BindFieldInitializers(this.DeclaringCompilation, sourceMemberContainerTypeSymbol.IsScriptClass ? sourceMemberContainerTypeSymbol.GetScriptInitializer() : null, sourceMemberContainerTypeSymbol.StaticInitializers, BindingDiagnosticBag.Discarded, out importChain);
			}
			foreach (BoundInitializer boundInitializer in boundInitializersOpt)
			{
				BoundFieldEqualsValue boundFieldEqualsValue = boundInitializer as BoundFieldEqualsValue;
				if (boundFieldEqualsValue != null)
				{
					BoundExpression value = boundFieldEqualsValue.Value;
					if (value != null)
					{
						if (!value.IsDefaultValue())
						{
							return true;
						}
						continue;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x04001CFA RID: 7418
		private readonly NamedTypeSymbol _containingType;

		// Token: 0x04001CFB RID: 7419
		private ThreeState _lazyShouldEmit;
	}
}
