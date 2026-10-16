using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200057F RID: 1407
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedRecordToString : SynthesizedRecordObjectMethod
	{
		// Token: 0x0600759D RID: 30109 RVA: 0x001B65C0 File Offset: 0x001B47C0
		public SynthesizedRecordToString(SourceMemberContainerTypeSymbol containingType, MethodSymbol printMethod, int memberOffset, BindingDiagnosticBag diagnostics)
			: base(containingType, "ToString", memberOffset, diagnostics)
		{
			this._printMethod = printMethod;
		}

		// Token: 0x17002063 RID: 8291
		// (get) Token: 0x0600759E RID: 30110 RVA: 0x001B65D8 File Offset: 0x001B47D8
		protected override SpecialMember OverriddenSpecialMember
		{
			get
			{
				return SpecialMember.System_Object__ToString;
			}
		}

		// Token: 0x0600759F RID: 30111 RVA: 0x001B65DC File Offset: 0x001B47DC
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters", "IsVararg", "DeclaredConstraintsForOverrideOrImplementation" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 0, 1 })]
		protected override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>> MakeParametersAndBindReturnType(BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Location returnTypeLocation = this.ReturnTypeLocation;
			NullableAnnotation nullableAnnotation = (this.ContainingType.IsRecordStruct ? NullableAnnotation.Oblivious : NullableAnnotation.NotAnnotated);
			return new ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>>(TypeWithAnnotations.Create(Binder.GetSpecialType(declaringCompilation, SpecialType.System_String, returnTypeLocation, diagnostics), nullableAnnotation, default(ImmutableArray<CustomModifier>)), ImmutableArray<ParameterSymbol>.Empty, false, ImmutableArray<TypeParameterConstraintClause>.Empty);
		}

		// Token: 0x060075A0 RID: 30112 RVA: 0x001B6630 File Offset: 0x001B4830
		protected override int GetParameterCountFromSyntax()
		{
			return 0;
		}

		// Token: 0x060075A1 RID: 30113 RVA: 0x001B6634 File Offset: 0x001B4834
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.SyntaxNode, compilationState, diagnostics);
			try
			{
				CSharpCompilation declaringCompilation = this.ContainingType.DeclaringCompilation;
				NamedTypeSymbol namedTypeSymbol = syntheticBoundNodeFactory.WellKnownType(WellKnownType.System_Text_StringBuilder);
				MethodSymbol methodSymbol = syntheticBoundNodeFactory.WellKnownMethod(WellKnownMember.System_Text_StringBuilder__ctor);
				LocalSymbol localSymbol = syntheticBoundNodeFactory.SynthesizedLocal(namedTypeSymbol, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
				BoundLocal boundLocal = syntheticBoundNodeFactory.Local(localSymbol);
				ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
				instance.Add(syntheticBoundNodeFactory.Assignment(boundLocal, syntheticBoundNodeFactory.New(methodSymbol, Array.Empty<BoundExpression>()), false));
				instance.Add(SynthesizedRecordToString.<GenerateMethodBody>g__makeAppendString|6_0(syntheticBoundNodeFactory, boundLocal, this.ContainingType.Name));
				instance.Add(SynthesizedRecordToString.<GenerateMethodBody>g__makeAppendString|6_0(syntheticBoundNodeFactory, boundLocal, " { "));
				instance.Add(syntheticBoundNodeFactory.If(syntheticBoundNodeFactory.Call(syntheticBoundNodeFactory.This(), this._printMethod, boundLocal), SynthesizedRecordToString.<GenerateMethodBody>g__makeAppendString|6_0(syntheticBoundNodeFactory, boundLocal, " "), null));
				instance.Add(SynthesizedRecordToString.<GenerateMethodBody>g__makeAppendString|6_0(syntheticBoundNodeFactory, boundLocal, "}"));
				instance.Add(syntheticBoundNodeFactory.Return(syntheticBoundNodeFactory.Call(boundLocal, syntheticBoundNodeFactory.SpecialMethod(SpecialMember.System_Object__ToString))));
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(ImmutableArray.Create<LocalSymbol>(localSymbol), instance.ToImmutableAndFree()));
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
			}
		}

		// Token: 0x060075A2 RID: 30114 RVA: 0x001B6790 File Offset: 0x001B4990
		[CompilerGenerated]
		internal static BoundStatement <GenerateMethodBody>g__makeAppendString|6_0(SyntheticBoundNodeFactory F, BoundLocal builder, string value)
		{
			return F.ExpressionStatement(F.Call(builder, F.WellKnownMethod(WellKnownMember.System_Text_StringBuilder__AppendString), F.StringLiteral(value)));
		}

		// Token: 0x04001CA5 RID: 7333
		private readonly MethodSymbol _printMethod;
	}
}
