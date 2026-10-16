using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000574 RID: 1396
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedRecordEqualityContractProperty : SourcePropertySymbolBase
	{
		// Token: 0x06007542 RID: 30018 RVA: 0x001B4F24 File Offset: 0x001B3124
		public SynthesizedRecordEqualityContractProperty(SourceMemberContainerTypeSymbol containingType, BindingDiagnosticBag diagnostics)
		{
			CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)containingType.SyntaxReferences[0].GetSyntax(default(CancellationToken));
			bool isSealed = containingType.IsSealed;
			bool flag = containingType.BaseTypeNoUseSiteDiagnostics.IsObjectType();
			DeclarationModifiers declarationModifiers;
			if (isSealed)
			{
				if (flag)
				{
					declarationModifiers = DeclarationModifiers.Private;
					goto IL_005B;
				}
			}
			else if (flag)
			{
				declarationModifiers = DeclarationModifiers.Protected | DeclarationModifiers.Virtual;
				goto IL_005B;
			}
			declarationModifiers = DeclarationModifiers.Protected | DeclarationModifiers.Override;
			IL_005B:
			base..ctor(containingType, csharpSyntaxNode, true, false, false, null, null, declarationModifiers, false, false, false, false, RefKind.None, "EqualityContract", default(SyntaxList<AttributeListSyntax>), containingType.Locations[0], diagnostics);
		}

		// Token: 0x17002046 RID: 8262
		// (get) Token: 0x06007543 RID: 30019 RVA: 0x001B4FBF File Offset: 0x001B31BF
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002047 RID: 8263
		// (get) Token: 0x06007544 RID: 30020 RVA: 0x001B4FC2 File Offset: 0x001B31C2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17002048 RID: 8264
		// (get) Token: 0x06007545 RID: 30021 RVA: 0x001B4FCC File Offset: 0x001B31CC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeDeclarationSyntaxList
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return default(SyntaxList<AttributeListSyntax>);
			}
		}

		// Token: 0x17002049 RID: 8265
		// (get) Token: 0x06007546 RID: 30022 RVA: 0x001B4FE2 File Offset: 0x001B31E2
		public override IAttributeTargetSymbol AttributesOwner
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700204A RID: 8266
		// (get) Token: 0x06007547 RID: 30023 RVA: 0x001B4FE8 File Offset: 0x001B31E8
		protected override Location TypeLocation
		{
			get
			{
				return this.ContainingType.Locations[0];
			}
		}

		// Token: 0x06007548 RID: 30024 RVA: 0x001B500C File Offset: 0x001B320C
		protected override SourcePropertyAccessorSymbol CreateGetAccessorSymbol(bool isAutoPropertyAccessor, BindingDiagnosticBag diagnostics)
		{
			return SourcePropertyAccessorSymbol.CreateAccessorSymbol(this.ContainingType, this, this._modifiers, this.ContainingType.Locations[0], (CSharpSyntaxNode)((SourceMemberContainerTypeSymbol)this.ContainingType).SyntaxReferences[0].GetSyntax(default(CancellationToken)), diagnostics);
		}

		// Token: 0x06007549 RID: 30025 RVA: 0x001B506C File Offset: 0x001B326C
		protected override SourcePropertyAccessorSymbol CreateSetAccessorSymbol(bool isAutoPropertyAccessor, BindingDiagnosticBag diagnostics)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600754A RID: 30026 RVA: 0x001B5074 File Offset: 0x001B3274
		[return: TupleElementNames(new string[] { "Type", "Parameters" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		protected override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>> MakeParametersAndBindType(BindingDiagnosticBag diagnostics)
		{
			return new ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>>(TypeWithAnnotations.Create(Binder.GetWellKnownType(this.DeclaringCompilation, WellKnownType.System_Type, diagnostics, base.Location), NullableAnnotation.NotAnnotated, default(ImmutableArray<CustomModifier>)), ImmutableArray<ParameterSymbol>.Empty);
		}

		// Token: 0x1700204B RID: 8267
		// (get) Token: 0x0600754B RID: 30027 RVA: 0x001B50AE File Offset: 0x001B32AE
		protected override bool HasPointerTypeSyntactically
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600754C RID: 30028 RVA: 0x001B50B1 File Offset: 0x001B32B1
		protected override void ValidatePropertyType(BindingDiagnosticBag diagnostics)
		{
			base.ValidatePropertyType(diagnostics);
			SynthesizedRecordEqualityContractProperty.VerifyOverridesEqualityContractFromBase(this, diagnostics);
		}

		// Token: 0x0600754D RID: 30029 RVA: 0x001B50C4 File Offset: 0x001B32C4
		internal static void VerifyOverridesEqualityContractFromBase(PropertySymbol overriding, BindingDiagnosticBag diagnostics)
		{
			if (overriding.ContainingType.BaseTypeNoUseSiteDiagnostics.IsObjectType())
			{
				return;
			}
			bool flag = false;
			if (!overriding.IsOverride)
			{
				flag = true;
			}
			else
			{
				PropertySymbol overriddenProperty = overriding.OverriddenProperty;
				if (overriddenProperty != null && !overriddenProperty.ContainingType.Equals(overriding.ContainingType.BaseTypeNoUseSiteDiagnostics, TypeCompareKind.AllIgnoreOptions))
				{
					flag = true;
				}
			}
			if (flag)
			{
				diagnostics.Add(ErrorCode.ERR_DoesNotOverrideBaseEqualityContract, overriding.Locations[0], new object[]
				{
					overriding,
					overriding.ContainingType.BaseTypeNoUseSiteDiagnostics
				});
			}
		}

		// Token: 0x04001C9E RID: 7326
		internal const string PropertyName = "EqualityContract";

		// Token: 0x02000946 RID: 2374
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal sealed class GetAccessorSymbol : SourcePropertyAccessorSymbol
		{
			// Token: 0x06009084 RID: 36996 RVA: 0x001FBFE4 File Offset: 0x001FA1E4
			internal GetAccessorSymbol(NamedTypeSymbol containingType, SourcePropertySymbolBase property, DeclarationModifiers propertyModifiers, Location location, CSharpSyntaxNode syntax, BindingDiagnosticBag diagnostics)
				: base(containingType, property, propertyModifiers, location, syntax, false, false, false, default(SyntaxTokenList), MethodKind.PropertyGet, false, true, false, diagnostics)
			{
			}

			// Token: 0x170028AE RID: 10414
			// (get) Token: 0x06009085 RID: 36997 RVA: 0x001FC011 File Offset: 0x001FA211
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
			{
				[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
				get
				{
					return ImmutableArray<SyntaxReference>.Empty;
				}
			}

			// Token: 0x170028AF RID: 10415
			// (get) Token: 0x06009086 RID: 36998 RVA: 0x001FC018 File Offset: 0x001FA218
			internal override bool SynthesizesLoweredBoundBody
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06009087 RID: 36999 RVA: 0x001FC01C File Offset: 0x001FA21C
			internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
			{
				SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.GetNonNullSyntaxNode(), compilationState, diagnostics);
				try
				{
					syntheticBoundNodeFactory.CurrentFunction = this;
					syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(new BoundStatement[] { syntheticBoundNodeFactory.Return(syntheticBoundNodeFactory.Typeof(this.ContainingType)) }));
				}
				catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
				{
					diagnostics.Add(missingPredefinedMember.Diagnostic);
					syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
				}
			}
		}
	}
}
