using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000578 RID: 1400
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedRecordGetHashCode : SynthesizedRecordObjectMethod
	{
		// Token: 0x06007560 RID: 30048 RVA: 0x001B5804 File Offset: 0x001B3A04
		public SynthesizedRecordGetHashCode(SourceMemberContainerTypeSymbol containingType, [global::System.Runtime.CompilerServices.Nullable(2)] PropertySymbol equalityContract, int memberOffset, BindingDiagnosticBag diagnostics)
			: base(containingType, "GetHashCode", memberOffset, diagnostics)
		{
			this._equalityContract = equalityContract;
		}

		// Token: 0x17002051 RID: 8273
		// (get) Token: 0x06007561 RID: 30049 RVA: 0x001B581C File Offset: 0x001B3A1C
		protected override SpecialMember OverriddenSpecialMember
		{
			get
			{
				return SpecialMember.System_Object__GetHashCode;
			}
		}

		// Token: 0x06007562 RID: 30050 RVA: 0x001B5820 File Offset: 0x001B3A20
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters", "IsVararg", "DeclaredConstraintsForOverrideOrImplementation" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 0, 1 })]
		protected override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>> MakeParametersAndBindReturnType(BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Location returnTypeLocation = this.ReturnTypeLocation;
			return new ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>>(TypeWithAnnotations.Create(Binder.GetSpecialType(declaringCompilation, SpecialType.System_Int32, returnTypeLocation, diagnostics), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray<ParameterSymbol>.Empty, false, ImmutableArray<TypeParameterConstraintClause>.Empty);
		}

		// Token: 0x06007563 RID: 30051 RVA: 0x001B5862 File Offset: 0x001B3A62
		protected override int GetParameterCountFromSyntax()
		{
			return 0;
		}

		// Token: 0x06007564 RID: 30052 RVA: 0x001B5868 File Offset: 0x001B3A68
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.SyntaxNode, compilationState, diagnostics);
			try
			{
				MethodSymbol methodSymbol = null;
				MethodSymbol methodSymbol2 = null;
				BoundExpression boundExpression;
				if (this.ContainingType.IsRecordStruct)
				{
					boundExpression = null;
				}
				else if (this.ContainingType.BaseTypeNoUseSiteDiagnostics.IsObjectType())
				{
					if (this._equalityContract.GetMethod == null)
					{
						syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
						return;
					}
					if (this._equalityContract.IsStatic)
					{
						syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
						return;
					}
					SynthesizedRecordGetHashCode.<GenerateMethodBody>g__ensureEqualityComparerHelpers|6_0(syntheticBoundNodeFactory, ref methodSymbol, ref methodSymbol2);
					boundExpression = MethodBodySynthesizer.GenerateGetHashCode(methodSymbol, methodSymbol2, syntheticBoundNodeFactory.Property(syntheticBoundNodeFactory.This(), this._equalityContract), syntheticBoundNodeFactory);
				}
				else
				{
					MethodSymbol overriddenMethod = base.OverriddenMethod;
					if (overriddenMethod == null || overriddenMethod.ReturnType.SpecialType != SpecialType.System_Int32)
					{
						syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
						return;
					}
					boundExpression = syntheticBoundNodeFactory.Call(syntheticBoundNodeFactory.Base(overriddenMethod.ContainingType), overriddenMethod);
				}
				BoundLiteral boundLiteral = null;
				foreach (FieldSymbol fieldSymbol in this.ContainingType.GetFieldsToEmit())
				{
					if (!fieldSymbol.IsStatic)
					{
						SynthesizedRecordGetHashCode.<GenerateMethodBody>g__ensureEqualityComparerHelpers|6_0(syntheticBoundNodeFactory, ref methodSymbol, ref methodSymbol2);
						if (boundExpression == null)
						{
							boundExpression = MethodBodySynthesizer.GenerateGetHashCode(methodSymbol, methodSymbol2, syntheticBoundNodeFactory.Field(syntheticBoundNodeFactory.This(), fieldSymbol), syntheticBoundNodeFactory);
						}
						else
						{
							boundExpression = MethodBodySynthesizer.GenerateHashCombine(boundExpression, methodSymbol, methodSymbol2, ref boundLiteral, syntheticBoundNodeFactory.Field(syntheticBoundNodeFactory.This(), fieldSymbol), syntheticBoundNodeFactory);
						}
					}
				}
				if (boundExpression == null)
				{
					boundExpression = syntheticBoundNodeFactory.Literal(0);
				}
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(new BoundStatement[] { syntheticBoundNodeFactory.Return(boundExpression) }));
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
			}
		}

		// Token: 0x06007565 RID: 30053 RVA: 0x001B5A50 File Offset: 0x001B3C50
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		internal static void <GenerateMethodBody>g__ensureEqualityComparerHelpers|6_0([global::System.Runtime.CompilerServices.Nullable(1)] SyntheticBoundNodeFactory F, [NotNull] ref MethodSymbol equalityComparer_GetHashCode, [NotNull] ref MethodSymbol equalityComparer_get_Default)
		{
			if (equalityComparer_GetHashCode == null)
			{
				equalityComparer_GetHashCode = F.WellKnownMethod(WellKnownMember.System_Collections_Generic_EqualityComparer_T__GetHashCode);
			}
			if (equalityComparer_get_Default == null)
			{
				equalityComparer_get_Default = F.WellKnownMethod(WellKnownMember.System_Collections_Generic_EqualityComparer_T__get_Default);
			}
		}

		// Token: 0x04001CA1 RID: 7329
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly PropertySymbol _equalityContract;
	}
}
