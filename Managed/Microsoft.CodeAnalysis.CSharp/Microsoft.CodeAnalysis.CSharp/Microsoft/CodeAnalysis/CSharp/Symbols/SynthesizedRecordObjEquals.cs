using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200057B RID: 1403
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedRecordObjEquals : SynthesizedRecordObjectMethod
	{
		// Token: 0x0600756D RID: 30061 RVA: 0x001B5C1F File Offset: 0x001B3E1F
		public SynthesizedRecordObjEquals(SourceMemberContainerTypeSymbol containingType, MethodSymbol typedRecordEquals, int memberOffset, BindingDiagnosticBag diagnostics)
			: base(containingType, "Equals", memberOffset, diagnostics)
		{
			this._typedRecordEquals = typedRecordEquals;
		}

		// Token: 0x17002053 RID: 8275
		// (get) Token: 0x0600756E RID: 30062 RVA: 0x001B5C37 File Offset: 0x001B3E37
		protected override SpecialMember OverriddenSpecialMember
		{
			get
			{
				return SpecialMember.System_Object__Equals;
			}
		}

		// Token: 0x0600756F RID: 30063 RVA: 0x001B5C3C File Offset: 0x001B3E3C
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters", "IsVararg", "DeclaredConstraintsForOverrideOrImplementation" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 0, 1 })]
		protected override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>> MakeParametersAndBindReturnType(BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Location returnTypeLocation = this.ReturnTypeLocation;
			NullableAnnotation nullableAnnotation = (this.ContainingType.IsRecordStruct ? NullableAnnotation.Oblivious : NullableAnnotation.Annotated);
			return new ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>>(TypeWithAnnotations.Create(Binder.GetSpecialType(declaringCompilation, SpecialType.System_Boolean, returnTypeLocation, diagnostics), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray.Create<ParameterSymbol>(new SourceSimpleParameterSymbol(this, TypeWithAnnotations.Create(Binder.GetSpecialType(declaringCompilation, SpecialType.System_Object, returnTypeLocation, diagnostics), nullableAnnotation, default(ImmutableArray<CustomModifier>)), 0, RefKind.None, "obj", this.Locations)), false, ImmutableArray<TypeParameterConstraintClause>.Empty);
		}

		// Token: 0x06007570 RID: 30064 RVA: 0x001B5CBC File Offset: 0x001B3EBC
		protected override int GetParameterCountFromSyntax()
		{
			return 1;
		}

		// Token: 0x06007571 RID: 30065 RVA: 0x001B5CC0 File Offset: 0x001B3EC0
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.SyntaxNode, compilationState, diagnostics);
			try
			{
				if (this._typedRecordEquals.ReturnType.SpecialType != SpecialType.System_Boolean)
				{
					syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
				}
				else
				{
					BoundParameter boundParameter = syntheticBoundNodeFactory.Parameter(this.Parameters[0]);
					BoundExpression boundExpression;
					if (this.ContainingType.IsRecordStruct)
					{
						boundExpression = syntheticBoundNodeFactory.LogicalAnd(syntheticBoundNodeFactory.Is(boundParameter, this.ContainingType), syntheticBoundNodeFactory.Call(syntheticBoundNodeFactory.This(), this._typedRecordEquals, syntheticBoundNodeFactory.Convert(this.ContainingType, boundParameter)));
					}
					else
					{
						boundExpression = syntheticBoundNodeFactory.Call(syntheticBoundNodeFactory.This(), this._typedRecordEquals, syntheticBoundNodeFactory.As(boundParameter, this.ContainingType));
					}
					syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(ImmutableArray.Create<BoundStatement>(syntheticBoundNodeFactory.Return(boundExpression))));
				}
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
			}
		}

		// Token: 0x04001CA2 RID: 7330
		private readonly MethodSymbol _typedRecordEquals;
	}
}
