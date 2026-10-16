using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004A7 RID: 1191
	internal sealed class SynthesizedSealedPropertyAccessor : SynthesizedInstanceMethodSymbol
	{
		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x06006270 RID: 25200 RVA: 0x0017A648 File Offset: 0x00178848
		internal override bool SynthesizesLoweredBoundBody
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x06006271 RID: 25201 RVA: 0x0017A64B File Offset: 0x0017884B
		internal override bool GenerateDebugInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006272 RID: 25202 RVA: 0x0017A650 File Offset: 0x00178850
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.GetNonNullSyntaxNode(), compilationState, diagnostics);
			syntheticBoundNodeFactory.CurrentFunction = this.OriginalDefinition;
			try
			{
				syntheticBoundNodeFactory.CloseMethod(MethodBodySynthesizer.ConstructSingleInvocationMethodBody(syntheticBoundNodeFactory, this.OverriddenAccessor, true));
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
			}
		}

		// Token: 0x06006273 RID: 25203 RVA: 0x0017A6B8 File Offset: 0x001788B8
		public SynthesizedSealedPropertyAccessor(PropertySymbol property, MethodSymbol overriddenAccessor)
		{
			this._property = property;
			this._overriddenAccessor = overriddenAccessor;
			this._parameters = SynthesizedParameterSymbol.DeriveParameters(overriddenAccessor, this);
		}

		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x06006274 RID: 25204 RVA: 0x0017A6DB File Offset: 0x001788DB
		internal MethodSymbol OverriddenAccessor
		{
			get
			{
				return this._overriddenAccessor;
			}
		}

		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x06006275 RID: 25205 RVA: 0x0017A6E3 File Offset: 0x001788E3
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._property.ContainingType;
			}
		}

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x06006276 RID: 25206 RVA: 0x0017A6F0 File Offset: 0x001788F0
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x06006277 RID: 25207 RVA: 0x0017A6F8 File Offset: 0x001788F8
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				Accessibility declaredAccessibility = this._overriddenAccessor.DeclaredAccessibility;
				if (declaredAccessibility != Accessibility.ProtectedAndInternal)
				{
					if (declaredAccessibility == Accessibility.ProtectedOrInternal && !this.ContainingAssembly.HasInternalAccessTo(this._overriddenAccessor.ContainingAssembly))
					{
						return Accessibility.Protected;
					}
				}
				else if (!this.ContainingAssembly.HasInternalAccessTo(this._overriddenAccessor.ContainingAssembly))
				{
					return Accessibility.Private;
				}
				return declaredAccessibility;
			}
		}

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x06006278 RID: 25208 RVA: 0x0017A74E File Offset: 0x0017894E
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06006279 RID: 25209 RVA: 0x0017A751 File Offset: 0x00178951
		public override bool IsAsync
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x0600627A RID: 25210 RVA: 0x0017A754 File Offset: 0x00178954
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x0600627B RID: 25211 RVA: 0x0017A757 File Offset: 0x00178957
		internal override CallingConvention CallingConvention
		{
			get
			{
				return this._overriddenAccessor.CallingConvention;
			}
		}

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x0600627C RID: 25212 RVA: 0x0017A764 File Offset: 0x00178964
		public override MethodKind MethodKind
		{
			get
			{
				return this._overriddenAccessor.MethodKind;
			}
		}

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x0600627D RID: 25213 RVA: 0x0017A771 File Offset: 0x00178971
		public override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x0600627E RID: 25214 RVA: 0x0017A774 File Offset: 0x00178974
		public override bool IsExtensionMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x0600627F RID: 25215 RVA: 0x0017A777 File Offset: 0x00178977
		public override bool HidesBaseMethodsByName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x06006280 RID: 25216 RVA: 0x0017A77A File Offset: 0x0017897A
		public override bool IsVararg
		{
			get
			{
				return this._overriddenAccessor.IsVararg;
			}
		}

		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x06006281 RID: 25217 RVA: 0x0017A787 File Offset: 0x00178987
		public override bool ReturnsVoid
		{
			get
			{
				return this._overriddenAccessor.ReturnsVoid;
			}
		}

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x06006282 RID: 25218 RVA: 0x0017A794 File Offset: 0x00178994
		public override RefKind RefKind
		{
			get
			{
				return this._overriddenAccessor.RefKind;
			}
		}

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x06006283 RID: 25219 RVA: 0x0017A7A1 File Offset: 0x001789A1
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return this._overriddenAccessor.ReturnTypeWithAnnotations;
			}
		}

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x06006284 RID: 25220 RVA: 0x0017A7AE File Offset: 0x001789AE
		public override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x06006285 RID: 25221 RVA: 0x0017A7B1 File Offset: 0x001789B1
		public override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x06006286 RID: 25222 RVA: 0x0017A7B8 File Offset: 0x001789B8
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x06006287 RID: 25223 RVA: 0x0017A7BF File Offset: 0x001789BF
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x06006288 RID: 25224 RVA: 0x0017A7C6 File Offset: 0x001789C6
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x06006289 RID: 25225 RVA: 0x0017A7CE File Offset: 0x001789CE
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x0600628A RID: 25226 RVA: 0x0017A7D1 File Offset: 0x001789D1
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x0600628B RID: 25227 RVA: 0x0017A7D8 File Offset: 0x001789D8
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this._overriddenAccessor.RefCustomModifiers;
			}
		}

		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x0600628C RID: 25228 RVA: 0x0017A7E5 File Offset: 0x001789E5
		public override Symbol AssociatedSymbol
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x0600628D RID: 25229 RVA: 0x0017A7ED File Offset: 0x001789ED
		public override bool IsOverride
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x0600628E RID: 25230 RVA: 0x0017A7F0 File Offset: 0x001789F0
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x0600628F RID: 25231 RVA: 0x0017A7F3 File Offset: 0x001789F3
		public override bool IsSealed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x06006290 RID: 25232 RVA: 0x0017A7F6 File Offset: 0x001789F6
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x06006291 RID: 25233 RVA: 0x0017A7F9 File Offset: 0x001789F9
		public override string Name
		{
			get
			{
				return this._overriddenAccessor.Name;
			}
		}

		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x06006292 RID: 25234 RVA: 0x0017A806 File Offset: 0x00178A06
		internal override bool HasSpecialName
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x06006293 RID: 25235 RVA: 0x0017A809 File Offset: 0x00178A09
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06006294 RID: 25236 RVA: 0x0017A80C File Offset: 0x00178A0C
		internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return true;
		}

		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x06006295 RID: 25237 RVA: 0x0017A80F File Offset: 0x00178A0F
		internal override bool IsMetadataFinal
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006296 RID: 25238 RVA: 0x0017A812 File Offset: 0x00178A12
		internal sealed override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x06006297 RID: 25239 RVA: 0x0017A815 File Offset: 0x00178A15
		internal override bool RequiresSecurityObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006298 RID: 25240 RVA: 0x0017A818 File Offset: 0x00178A18
		public override DllImportData GetDllImportData()
		{
			return null;
		}

		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x06006299 RID: 25241 RVA: 0x0017A81B File Offset: 0x00178A1B
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x0600629A RID: 25242 RVA: 0x0017A81E File Offset: 0x00178A1E
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600629B RID: 25243 RVA: 0x0017A821 File Offset: 0x00178A21
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600629C RID: 25244 RVA: 0x0017A828 File Offset: 0x00178A28
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x04001980 RID: 6528
		private readonly PropertySymbol _property;

		// Token: 0x04001981 RID: 6529
		private readonly MethodSymbol _overriddenAccessor;

		// Token: 0x04001982 RID: 6530
		private readonly ImmutableArray<ParameterSymbol> _parameters;
	}
}
