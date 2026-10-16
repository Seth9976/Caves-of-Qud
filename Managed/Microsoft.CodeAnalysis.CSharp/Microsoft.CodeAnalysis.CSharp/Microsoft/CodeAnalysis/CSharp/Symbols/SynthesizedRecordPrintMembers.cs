using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200057D RID: 1405
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedRecordPrintMembers : SynthesizedRecordOrdinaryMethod
	{
		// Token: 0x06007588 RID: 30088 RVA: 0x001B5E9A File Offset: 0x001B409A
		public SynthesizedRecordPrintMembers(SourceMemberContainerTypeSymbol containingType, int memberOffset, BindingDiagnosticBag diagnostics)
			: base(containingType, "PrintMembers", true, memberOffset, diagnostics)
		{
		}

		// Token: 0x06007589 RID: 30089 RVA: 0x001B5EAC File Offset: 0x001B40AC
		protected override DeclarationModifiers MakeDeclarationModifiers(DeclarationModifiers allowedModifiers, BindingDiagnosticBag diagnostics)
		{
			DeclarationModifiers declarationModifiers = ((this.ContainingType.IsRecordStruct || (this.ContainingType.BaseTypeNoUseSiteDiagnostics.IsObjectType() && this.ContainingType.IsSealed)) ? DeclarationModifiers.Private : DeclarationModifiers.Protected);
			if (this.ContainingType.IsRecord && !this.ContainingType.BaseTypeNoUseSiteDiagnostics.IsObjectType())
			{
				declarationModifiers |= DeclarationModifiers.Override;
			}
			else
			{
				declarationModifiers |= (this.ContainingType.IsSealed ? DeclarationModifiers.None : DeclarationModifiers.Virtual);
			}
			return declarationModifiers;
		}

		// Token: 0x0600758A RID: 30090 RVA: 0x001B5F34 File Offset: 0x001B4134
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters", "IsVararg", "DeclaredConstraintsForOverrideOrImplementation" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 0, 1 })]
		protected override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>> MakeParametersAndBindReturnType(BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Location returnTypeLocation = this.ReturnTypeLocation;
			NullableAnnotation nullableAnnotation = (this.ContainingType.IsRecordStruct ? NullableAnnotation.Oblivious : NullableAnnotation.NotAnnotated);
			return new ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>>(TypeWithAnnotations.Create(Binder.GetSpecialType(declaringCompilation, SpecialType.System_Boolean, returnTypeLocation, diagnostics), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray.Create<ParameterSymbol>(new SourceSimpleParameterSymbol(this, TypeWithAnnotations.Create(Binder.GetWellKnownType(declaringCompilation, WellKnownType.System_Text_StringBuilder, diagnostics, returnTypeLocation), nullableAnnotation, default(ImmutableArray<CustomModifier>)), 0, RefKind.None, "builder", this.Locations)), false, ImmutableArray<TypeParameterConstraintClause>.Empty);
		}

		// Token: 0x0600758B RID: 30091 RVA: 0x001B5FB8 File Offset: 0x001B41B8
		protected override int GetParameterCountFromSyntax()
		{
			return 1;
		}

		// Token: 0x0600758C RID: 30092 RVA: 0x001B5FBC File Offset: 0x001B41BC
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

		// Token: 0x0600758D RID: 30093 RVA: 0x001B6028 File Offset: 0x001B4228
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.ContainingType.GetNonNullSyntaxNode(), compilationState, diagnostics);
			try
			{
				ImmutableArray<Symbol> immutableArray = this.ContainingType.GetMembers().WhereAsArray<Symbol>((Symbol m) => SynthesizedRecordPrintMembers.<GenerateMethodBody>g__isPrintable|5_1(m));
				if (!base.ReturnType.IsErrorType())
				{
					if (!immutableArray.Any<Symbol>((Symbol m) => m.GetTypeOrReturnType().Type.IsErrorType()))
					{
						BoundParameter boundParameter = syntheticBoundNodeFactory.Parameter(this.Parameters[0]);
						ArrayBuilder<BoundStatement> arrayBuilder;
						if (this.ContainingType.BaseTypeNoUseSiteDiagnostics.IsObjectType() || this.ContainingType.IsRecordStruct)
						{
							if (immutableArray.IsEmpty)
							{
								syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Return(syntheticBoundNodeFactory.Literal(false)));
								return;
							}
							arrayBuilder = ArrayBuilder<BoundStatement>.GetInstance();
						}
						else
						{
							MethodSymbol overriddenMethod = base.OverriddenMethod;
							if (overriddenMethod == null || overriddenMethod.ReturnType.SpecialType != SpecialType.System_Boolean)
							{
								syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
								return;
							}
							BoundCall boundCall = syntheticBoundNodeFactory.Call(syntheticBoundNodeFactory.Base(this.ContainingType.BaseTypeNoUseSiteDiagnostics), overriddenMethod, boundParameter);
							if (immutableArray.IsEmpty)
							{
								syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Return(boundCall));
								return;
							}
							arrayBuilder = ArrayBuilder<BoundStatement>.GetInstance();
							arrayBuilder.Add(syntheticBoundNodeFactory.If(boundCall, SynthesizedRecordPrintMembers.<GenerateMethodBody>g__makeAppendString|5_0(syntheticBoundNodeFactory, boundParameter, ", "), null));
						}
						for (int i = 0; i < immutableArray.Length; i++)
						{
							Symbol symbol = immutableArray[i];
							arrayBuilder.Add(SynthesizedRecordPrintMembers.<GenerateMethodBody>g__makeAppendString|5_0(syntheticBoundNodeFactory, boundParameter, symbol.Name));
							arrayBuilder.Add(SynthesizedRecordPrintMembers.<GenerateMethodBody>g__makeAppendString|5_0(syntheticBoundNodeFactory, boundParameter, " = "));
							SymbolKind kind = symbol.Kind;
							BoundExpression boundExpression;
							if (kind != SymbolKind.Field)
							{
								if (kind != SymbolKind.Property)
								{
									throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
								}
								boundExpression = syntheticBoundNodeFactory.Property(syntheticBoundNodeFactory.This(), (PropertySymbol)symbol);
							}
							else
							{
								boundExpression = syntheticBoundNodeFactory.Field(syntheticBoundNodeFactory.This(), (FieldSymbol)symbol);
							}
							BoundExpression boundExpression2 = boundExpression;
							if (boundExpression2.Type.IsValueType)
							{
								arrayBuilder.Add(syntheticBoundNodeFactory.ExpressionStatement(syntheticBoundNodeFactory.Call(boundParameter, syntheticBoundNodeFactory.WellKnownMethod(WellKnownMember.System_Text_StringBuilder__AppendString), syntheticBoundNodeFactory.Call(boundExpression2, syntheticBoundNodeFactory.SpecialMethod(SpecialMember.System_Object__ToString)))));
							}
							else
							{
								arrayBuilder.Add(syntheticBoundNodeFactory.ExpressionStatement(syntheticBoundNodeFactory.Call(boundParameter, syntheticBoundNodeFactory.WellKnownMethod(WellKnownMember.System_Text_StringBuilder__AppendObject), syntheticBoundNodeFactory.Convert(syntheticBoundNodeFactory.SpecialType(SpecialType.System_Object), boundExpression2))));
							}
							if (i < immutableArray.Length - 1)
							{
								arrayBuilder.Add(SynthesizedRecordPrintMembers.<GenerateMethodBody>g__makeAppendString|5_0(syntheticBoundNodeFactory, boundParameter, ", "));
							}
						}
						arrayBuilder.Add(syntheticBoundNodeFactory.Return(syntheticBoundNodeFactory.Literal(true)));
						syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(arrayBuilder.ToImmutableAndFree()));
						return;
					}
				}
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
			}
		}

		// Token: 0x0600758E RID: 30094 RVA: 0x001B6338 File Offset: 0x001B4538
		internal static void VerifyOverridesPrintMembersFromBase(MethodSymbol overriding, BindingDiagnosticBag diagnostics)
		{
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = overriding.ContainingType.BaseTypeNoUseSiteDiagnostics;
			if (baseTypeNoUseSiteDiagnostics.IsObjectType())
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
				MethodSymbol overriddenMethod = overriding.OverriddenMethod;
				if (overriddenMethod != null && !overriddenMethod.ContainingType.Equals(baseTypeNoUseSiteDiagnostics, TypeCompareKind.AllIgnoreOptions))
				{
					flag = true;
				}
			}
			if (flag)
			{
				diagnostics.Add(ErrorCode.ERR_DoesNotOverrideBaseMethod, overriding.Locations[0], new object[] { overriding, baseTypeNoUseSiteDiagnostics });
			}
		}

		// Token: 0x0600758F RID: 30095 RVA: 0x001B63B0 File Offset: 0x001B45B0
		[CompilerGenerated]
		internal static BoundStatement <GenerateMethodBody>g__makeAppendString|5_0(SyntheticBoundNodeFactory F, BoundParameter builder, string value)
		{
			return F.ExpressionStatement(F.Call(builder, F.WellKnownMethod(WellKnownMember.System_Text_StringBuilder__AppendString), F.StringLiteral(value)));
		}

		// Token: 0x06007590 RID: 30096 RVA: 0x001B63D4 File Offset: 0x001B45D4
		[CompilerGenerated]
		internal static bool <GenerateMethodBody>g__isPrintable|5_1(Symbol m)
		{
			if (m.DeclaredAccessibility != Accessibility.Public || m.IsStatic)
			{
				return false;
			}
			if (m.Kind == SymbolKind.Field)
			{
				return true;
			}
			if (m.Kind == SymbolKind.Property)
			{
				PropertySymbol propertySymbol = (PropertySymbol)m;
				return !propertySymbol.IsIndexer && !propertySymbol.IsOverride && propertySymbol.GetMethod != null;
			}
			return false;
		}
	}
}
