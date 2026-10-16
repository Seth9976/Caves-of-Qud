using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200056F RID: 1391
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedRecordBaseEquals : SynthesizedRecordOrdinaryMethod
	{
		// Token: 0x0600751E RID: 29982 RVA: 0x001B4574 File Offset: 0x001B2774
		public SynthesizedRecordBaseEquals(SourceMemberContainerTypeSymbol containingType, int memberOffset, BindingDiagnosticBag diagnostics)
			: base(containingType, "Equals", true, memberOffset, diagnostics)
		{
		}

		// Token: 0x0600751F RID: 29983 RVA: 0x001B4585 File Offset: 0x001B2785
		protected override DeclarationModifiers MakeDeclarationModifiers(DeclarationModifiers allowedModifiers, BindingDiagnosticBag diagnostics)
		{
			return DeclarationModifiers.Sealed | DeclarationModifiers.Public | DeclarationModifiers.Override;
		}

		// Token: 0x06007520 RID: 29984 RVA: 0x001B458C File Offset: 0x001B278C
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters", "IsVararg", "DeclaredConstraintsForOverrideOrImplementation" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 0, 1 })]
		protected override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>> MakeParametersAndBindReturnType(BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Location returnTypeLocation = this.ReturnTypeLocation;
			return new ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>>(TypeWithAnnotations.Create(Binder.GetSpecialType(declaringCompilation, SpecialType.System_Boolean, returnTypeLocation, diagnostics), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray.Create<ParameterSymbol>(new SourceSimpleParameterSymbol(this, TypeWithAnnotations.Create(this.ContainingType.BaseTypeNoUseSiteDiagnostics, NullableAnnotation.Annotated, default(ImmutableArray<CustomModifier>)), 0, RefKind.None, "other", this.Locations)), false, ImmutableArray<TypeParameterConstraintClause>.Empty);
		}

		// Token: 0x06007521 RID: 29985 RVA: 0x001B45FA File Offset: 0x001B27FA
		protected override int GetParameterCountFromSyntax()
		{
			return 1;
		}

		// Token: 0x06007522 RID: 29986 RVA: 0x001B4600 File Offset: 0x001B2800
		protected override void MethodChecks(BindingDiagnosticBag diagnostics)
		{
			base.MethodChecks(diagnostics);
			MethodSymbol overriddenMethod = base.OverriddenMethod;
			if (overriddenMethod != null && !overriddenMethod.ContainingType.Equals(this.ContainingType.BaseTypeNoUseSiteDiagnostics, TypeCompareKind.AllIgnoreOptions))
			{
				diagnostics.Add(ErrorCode.ERR_DoesNotOverrideBaseMethod, this.Locations[0], new object[]
				{
					this,
					this.ContainingType.BaseTypeNoUseSiteDiagnostics
				});
			}
		}

		// Token: 0x06007523 RID: 29987 RVA: 0x001B466C File Offset: 0x001B286C
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.SyntaxNode, compilationState, diagnostics);
			try
			{
				ParameterSymbol parameterSymbol = this.Parameters[0];
				if (parameterSymbol.Type.IsErrorType())
				{
					syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
				}
				else
				{
					BoundCall boundCall = syntheticBoundNodeFactory.Call(syntheticBoundNodeFactory.This(), Enumerable.Single<SynthesizedRecordObjEquals>(this.ContainingType.GetMembersUnordered().OfType<SynthesizedRecordObjEquals>()), syntheticBoundNodeFactory.Convert(syntheticBoundNodeFactory.SpecialType(SpecialType.System_Object), syntheticBoundNodeFactory.Parameter(parameterSymbol)));
					syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(new BoundStatement[] { syntheticBoundNodeFactory.Return(boundCall) }));
				}
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
			}
		}
	}
}
