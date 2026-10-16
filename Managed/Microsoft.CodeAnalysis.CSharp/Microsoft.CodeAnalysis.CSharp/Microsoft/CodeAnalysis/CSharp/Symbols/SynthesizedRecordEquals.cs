using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000577 RID: 1399
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedRecordEquals : SynthesizedRecordOrdinaryMethod
	{
		// Token: 0x0600755B RID: 30043 RVA: 0x001B545C File Offset: 0x001B365C
		public SynthesizedRecordEquals(SourceMemberContainerTypeSymbol containingType, [global::System.Runtime.CompilerServices.Nullable(2)] PropertySymbol equalityContract, int memberOffset, BindingDiagnosticBag diagnostics)
			: base(containingType, "Equals", true, memberOffset, diagnostics)
		{
			this._equalityContract = equalityContract;
		}

		// Token: 0x0600755C RID: 30044 RVA: 0x001B5475 File Offset: 0x001B3675
		protected override DeclarationModifiers MakeDeclarationModifiers(DeclarationModifiers allowedModifiers, BindingDiagnosticBag diagnostics)
		{
			return DeclarationModifiers.Public | (this.ContainingType.IsSealed ? DeclarationModifiers.None : DeclarationModifiers.Virtual);
		}

		// Token: 0x0600755D RID: 30045 RVA: 0x001B5490 File Offset: 0x001B3690
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters", "IsVararg", "DeclaredConstraintsForOverrideOrImplementation" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 0, 1 })]
		protected override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>> MakeParametersAndBindReturnType(BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Location returnTypeLocation = this.ReturnTypeLocation;
			NullableAnnotation nullableAnnotation = (this.ContainingType.IsRecordStruct ? NullableAnnotation.Oblivious : NullableAnnotation.Annotated);
			return new ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>>(TypeWithAnnotations.Create(Binder.GetSpecialType(declaringCompilation, SpecialType.System_Boolean, returnTypeLocation, diagnostics), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray.Create<ParameterSymbol>(new SourceSimpleParameterSymbol(this, TypeWithAnnotations.Create(this.ContainingType, nullableAnnotation, default(ImmutableArray<CustomModifier>)), 0, RefKind.None, "other", this.Locations)), false, ImmutableArray<TypeParameterConstraintClause>.Empty);
		}

		// Token: 0x0600755E RID: 30046 RVA: 0x001B550B File Offset: 0x001B370B
		protected override int GetParameterCountFromSyntax()
		{
			return 1;
		}

		// Token: 0x0600755F RID: 30047 RVA: 0x001B5510 File Offset: 0x001B3710
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.ContainingType.GetNonNullSyntaxNode(), compilationState, diagnostics);
			try
			{
				BoundParameter boundParameter = syntheticBoundNodeFactory.Parameter(this.Parameters[0]);
				bool isRecordStruct = this.ContainingType.IsRecordStruct;
				BoundExpression boundExpression;
				if (isRecordStruct)
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
					if (this._equalityContract.IsStatic || !this._equalityContract.Type.Equals(this.DeclaringCompilation.GetWellKnownType(WellKnownType.System_Type), TypeCompareKind.AllIgnoreOptions))
					{
						syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
						return;
					}
					boundExpression = syntheticBoundNodeFactory.ObjectNotEqual(boundParameter, syntheticBoundNodeFactory.Null(syntheticBoundNodeFactory.SpecialType(SpecialType.System_Object)));
					BoundCall boundCall = syntheticBoundNodeFactory.Call(null, syntheticBoundNodeFactory.WellKnownMethod(WellKnownMember.System_Type__op_Equality), syntheticBoundNodeFactory.Property(syntheticBoundNodeFactory.This(), this._equalityContract), syntheticBoundNodeFactory.Property(boundParameter, this._equalityContract));
					boundExpression = syntheticBoundNodeFactory.LogicalAnd(boundExpression, boundCall);
				}
				else
				{
					MethodSymbol overriddenMethod = Enumerable.Single<SynthesizedRecordBaseEquals>(this.ContainingType.GetMembersUnordered().OfType<SynthesizedRecordBaseEquals>()).OverriddenMethod;
					if (overriddenMethod == null || !overriddenMethod.ContainingType.Equals(this.ContainingType.BaseTypeNoUseSiteDiagnostics, TypeCompareKind.AllIgnoreOptions) || overriddenMethod.ReturnType.SpecialType != SpecialType.System_Boolean)
					{
						syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
						return;
					}
					boundExpression = syntheticBoundNodeFactory.Call(syntheticBoundNodeFactory.Base(overriddenMethod.ContainingType), overriddenMethod, syntheticBoundNodeFactory.Convert(overriddenMethod.Parameters[0].Type, boundParameter));
				}
				ArrayBuilder<FieldSymbol> instance = ArrayBuilder<FieldSymbol>.GetInstance();
				bool flag = false;
				foreach (FieldSymbol fieldSymbol in this.ContainingType.GetFieldsToEmit())
				{
					if (!fieldSymbol.IsStatic)
					{
						instance.Add(fieldSymbol);
						TypeSymbol type = fieldSymbol.Type;
						if (type.IsUnsafe())
						{
							diagnostics.Add(ErrorCode.ERR_BadFieldTypeInRecord, fieldSymbol.Locations.FirstOrNone(), new object[] { type });
							flag = true;
						}
						else if (type.IsRestrictedType(false))
						{
							flag = true;
						}
					}
				}
				if (instance.Count > 0 && !flag)
				{
					boundExpression = MethodBodySynthesizer.GenerateFieldEquals(boundExpression, boundParameter, instance, syntheticBoundNodeFactory);
				}
				else if (boundExpression == null)
				{
					boundExpression = syntheticBoundNodeFactory.Literal(true);
				}
				instance.Free();
				if (!isRecordStruct)
				{
					boundExpression = syntheticBoundNodeFactory.LogicalOr(syntheticBoundNodeFactory.ObjectEqual(syntheticBoundNodeFactory.This(), boundParameter), boundExpression);
				}
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(new BoundStatement[] { syntheticBoundNodeFactory.Return(boundExpression) }));
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
			}
		}

		// Token: 0x04001CA0 RID: 7328
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly PropertySymbol _equalityContract;
	}
}
