using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000593 RID: 1427
	internal class SynthesizedInstanceConstructor : SynthesizedInstanceMethodSymbol
	{
		// Token: 0x06007732 RID: 30514 RVA: 0x001B7F86 File Offset: 0x001B6186
		internal SynthesizedInstanceConstructor(NamedTypeSymbol containingType)
		{
			this._containingType = containingType;
		}

		// Token: 0x17002172 RID: 8562
		// (get) Token: 0x06007733 RID: 30515 RVA: 0x001B7F95 File Offset: 0x001B6195
		internal override bool GenerateDebugInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002173 RID: 8563
		// (get) Token: 0x06007734 RID: 30516 RVA: 0x001B7F98 File Offset: 0x001B6198
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
		}

		// Token: 0x17002174 RID: 8564
		// (get) Token: 0x06007735 RID: 30517 RVA: 0x001B7F9F File Offset: 0x001B619F
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				if (!this.ContainingType.IsAbstract)
				{
					return Accessibility.Public;
				}
				return Accessibility.Protected;
			}
		}

		// Token: 0x17002175 RID: 8565
		// (get) Token: 0x06007736 RID: 30518 RVA: 0x001B7FB1 File Offset: 0x001B61B1
		internal override bool IsMetadataFinal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002176 RID: 8566
		// (get) Token: 0x06007737 RID: 30519 RVA: 0x001B7FB4 File Offset: 0x001B61B4
		public sealed override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002177 RID: 8567
		// (get) Token: 0x06007738 RID: 30520 RVA: 0x001B7FBC File Offset: 0x001B61BC
		public sealed override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002178 RID: 8568
		// (get) Token: 0x06007739 RID: 30521 RVA: 0x001B7FC4 File Offset: 0x001B61C4
		public sealed override string Name
		{
			get
			{
				return ".ctor";
			}
		}

		// Token: 0x17002179 RID: 8569
		// (get) Token: 0x0600773A RID: 30522 RVA: 0x001B7FCB File Offset: 0x001B61CB
		internal sealed override bool HasSpecialName
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700217A RID: 8570
		// (get) Token: 0x0600773B RID: 30523 RVA: 0x001B7FCE File Offset: 0x001B61CE
		internal sealed override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				if (this._containingType.IsComImport)
				{
					return 4099;
				}
				if (this._containingType.TypeKind == TypeKind.Delegate)
				{
					return 3;
				}
				return 0;
			}
		}

		// Token: 0x1700217B RID: 8571
		// (get) Token: 0x0600773C RID: 30524 RVA: 0x001B7FF4 File Offset: 0x001B61F4
		internal sealed override bool RequiresSecurityObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600773D RID: 30525 RVA: 0x001B7FF7 File Offset: 0x001B61F7
		public sealed override DllImportData GetDllImportData()
		{
			return null;
		}

		// Token: 0x1700217C RID: 8572
		// (get) Token: 0x0600773E RID: 30526 RVA: 0x001B7FFA File Offset: 0x001B61FA
		internal sealed override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700217D RID: 8573
		// (get) Token: 0x0600773F RID: 30527 RVA: 0x001B7FFD File Offset: 0x001B61FD
		internal sealed override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007740 RID: 30528 RVA: 0x001B8000 File Offset: 0x001B6200
		internal sealed override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06007741 RID: 30529 RVA: 0x001B8007 File Offset: 0x001B6207
		internal sealed override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x1700217E RID: 8574
		// (get) Token: 0x06007742 RID: 30530 RVA: 0x001B800E File Offset: 0x001B620E
		public sealed override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700217F RID: 8575
		// (get) Token: 0x06007743 RID: 30531 RVA: 0x001B8011 File Offset: 0x001B6211
		public sealed override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x06007744 RID: 30532 RVA: 0x001B8018 File Offset: 0x001B6218
		internal override LexicalSortKey GetLexicalSortKey()
		{
			return LexicalSortKey.SynthesizedCtor;
		}

		// Token: 0x17002180 RID: 8576
		// (get) Token: 0x06007745 RID: 30533 RVA: 0x001B801F File Offset: 0x001B621F
		public sealed override ImmutableArray<Location> Locations
		{
			get
			{
				return this.ContainingType.Locations;
			}
		}

		// Token: 0x17002181 RID: 8577
		// (get) Token: 0x06007746 RID: 30534 RVA: 0x001B802C File Offset: 0x001B622C
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x17002182 RID: 8578
		// (get) Token: 0x06007747 RID: 30535 RVA: 0x001B8030 File Offset: 0x001B6230
		public sealed override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return TypeWithAnnotations.Create(this.ContainingAssembly.GetSpecialType(SpecialType.System_Void), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
		}

		// Token: 0x17002183 RID: 8579
		// (get) Token: 0x06007748 RID: 30536 RVA: 0x001B8058 File Offset: 0x001B6258
		public sealed override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17002184 RID: 8580
		// (get) Token: 0x06007749 RID: 30537 RVA: 0x001B805B File Offset: 0x001B625B
		public sealed override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x17002185 RID: 8581
		// (get) Token: 0x0600774A RID: 30538 RVA: 0x001B8062 File Offset: 0x001B6262
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x17002186 RID: 8582
		// (get) Token: 0x0600774B RID: 30539 RVA: 0x001B8069 File Offset: 0x001B6269
		public sealed override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x17002187 RID: 8583
		// (get) Token: 0x0600774C RID: 30540 RVA: 0x001B8070 File Offset: 0x001B6270
		public sealed override Symbol AssociatedSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002188 RID: 8584
		// (get) Token: 0x0600774D RID: 30541 RVA: 0x001B8073 File Offset: 0x001B6273
		public sealed override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002189 RID: 8585
		// (get) Token: 0x0600774E RID: 30542 RVA: 0x001B8076 File Offset: 0x001B6276
		public sealed override bool ReturnsVoid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700218A RID: 8586
		// (get) Token: 0x0600774F RID: 30543 RVA: 0x001B8079 File Offset: 0x001B6279
		public sealed override MethodKind MethodKind
		{
			get
			{
				return MethodKind.Constructor;
			}
		}

		// Token: 0x1700218B RID: 8587
		// (get) Token: 0x06007750 RID: 30544 RVA: 0x001B807C File Offset: 0x001B627C
		public sealed override bool IsExtern
		{
			get
			{
				NamedTypeSymbol containingType = this.ContainingType;
				return containingType != null && containingType.IsComImport;
			}
		}

		// Token: 0x1700218C RID: 8588
		// (get) Token: 0x06007751 RID: 30545 RVA: 0x001B809B File Offset: 0x001B629B
		public sealed override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700218D RID: 8589
		// (get) Token: 0x06007752 RID: 30546 RVA: 0x001B809E File Offset: 0x001B629E
		public sealed override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700218E RID: 8590
		// (get) Token: 0x06007753 RID: 30547 RVA: 0x001B80A1 File Offset: 0x001B62A1
		public sealed override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700218F RID: 8591
		// (get) Token: 0x06007754 RID: 30548 RVA: 0x001B80A4 File Offset: 0x001B62A4
		public sealed override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002190 RID: 8592
		// (get) Token: 0x06007755 RID: 30549 RVA: 0x001B80A7 File Offset: 0x001B62A7
		public sealed override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002191 RID: 8593
		// (get) Token: 0x06007756 RID: 30550 RVA: 0x001B80AA File Offset: 0x001B62AA
		public sealed override bool IsAsync
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002192 RID: 8594
		// (get) Token: 0x06007757 RID: 30551 RVA: 0x001B80AD File Offset: 0x001B62AD
		public sealed override bool HidesBaseMethodsByName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007758 RID: 30552 RVA: 0x001B80B0 File Offset: 0x001B62B0
		internal sealed override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x06007759 RID: 30553 RVA: 0x001B80B3 File Offset: 0x001B62B3
		internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x17002193 RID: 8595
		// (get) Token: 0x0600775A RID: 30554 RVA: 0x001B80B6 File Offset: 0x001B62B6
		public sealed override bool IsExtensionMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002194 RID: 8596
		// (get) Token: 0x0600775B RID: 30555 RVA: 0x001B80B9 File Offset: 0x001B62B9
		internal sealed override CallingConvention CallingConvention
		{
			get
			{
				return CallingConvention.HasThis;
			}
		}

		// Token: 0x17002195 RID: 8597
		// (get) Token: 0x0600775C RID: 30556 RVA: 0x001B80BD File Offset: 0x001B62BD
		internal sealed override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002196 RID: 8598
		// (get) Token: 0x0600775D RID: 30557 RVA: 0x001B80C0 File Offset: 0x001B62C0
		public sealed override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x0600775E RID: 30558 RVA: 0x001B80C7 File Offset: 0x001B62C7
		internal sealed override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			return ((SourceMemberContainerTypeSymbol)this.ContainingType).CalculateSyntaxOffsetInSynthesizedConstructor(localPosition, localTree, false);
		}

		// Token: 0x0600775F RID: 30559 RVA: 0x001B80DC File Offset: 0x001B62DC
		internal sealed override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			UseSiteInfo<AssemblySymbol> useSiteInfo = new UseSiteInfo<AssemblySymbol>(base.PrimaryDependency);
			base.MergeUseSiteInfo(ref useSiteInfo, this.ReturnTypeWithAnnotations.Type.GetUseSiteInfo());
			return useSiteInfo;
		}

		// Token: 0x06007760 RID: 30560 RVA: 0x001B8113 File Offset: 0x001B6313
		internal sealed override bool IsNullableAnalysisEnabled()
		{
			SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = this.ContainingType as SourceMemberContainerTypeSymbol;
			return sourceMemberContainerTypeSymbol != null && sourceMemberContainerTypeSymbol.IsNullableEnabledForConstructorsAndInitializers(false);
		}

		// Token: 0x06007761 RID: 30561 RVA: 0x001B812C File Offset: 0x001B632C
		protected void GenerateMethodBodyCore(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.GetNonNullSyntaxNode(), compilationState, diagnostics);
			syntheticBoundNodeFactory.CurrentFunction = this;
			if (this.ContainingType.BaseTypeNoUseSiteDiagnostics is MissingMetadataTypeSymbol)
			{
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block());
				return;
			}
			BoundCall boundCall = MethodCompiler.GenerateBaseParameterlessConstructorInitializer(this, diagnostics);
			if (boundCall == null)
			{
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block());
				return;
			}
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			instance.Add(syntheticBoundNodeFactory.ExpressionStatement(boundCall));
			this.GenerateMethodBodyStatements(syntheticBoundNodeFactory, instance, diagnostics);
			instance.Add(syntheticBoundNodeFactory.Return(null));
			BoundBlock boundBlock = syntheticBoundNodeFactory.Block(instance.ToImmutableAndFree());
			syntheticBoundNodeFactory.CloseMethod(boundBlock);
		}

		// Token: 0x06007762 RID: 30562 RVA: 0x001B81C3 File Offset: 0x001B63C3
		internal virtual void GenerateMethodBodyStatements(SyntheticBoundNodeFactory factory, ArrayBuilder<BoundStatement> statements, BindingDiagnosticBag diagnostics)
		{
		}

		// Token: 0x04001CDA RID: 7386
		private readonly NamedTypeSymbol _containingType;
	}
}
