using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000570 RID: 1392
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedRecordClone : SynthesizedRecordOrdinaryMethod
	{
		// Token: 0x06007524 RID: 29988 RVA: 0x001B4738 File Offset: 0x001B2938
		public SynthesizedRecordClone(SourceMemberContainerTypeSymbol containingType, int memberOffset, BindingDiagnosticBag diagnostics)
			: base(containingType, "<Clone>$", !containingType.IsAbstract, memberOffset, diagnostics)
		{
		}

		// Token: 0x06007525 RID: 29989 RVA: 0x001B4754 File Offset: 0x001B2954
		protected override DeclarationModifiers MakeDeclarationModifiers(DeclarationModifiers allowedModifiers, BindingDiagnosticBag diagnostics)
		{
			DeclarationModifiers declarationModifiers = DeclarationModifiers.Public;
			if (this.VirtualCloneInBase() != null)
			{
				declarationModifiers |= DeclarationModifiers.Override;
			}
			else
			{
				declarationModifiers |= (this.ContainingType.IsSealed ? DeclarationModifiers.None : DeclarationModifiers.Virtual);
			}
			if (this.ContainingType.IsAbstract)
			{
				declarationModifiers &= ~DeclarationModifiers.Virtual;
				declarationModifiers |= DeclarationModifiers.Abstract;
			}
			return declarationModifiers;
		}

		// Token: 0x06007526 RID: 29990 RVA: 0x001B47A8 File Offset: 0x001B29A8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private MethodSymbol VirtualCloneInBase()
		{
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = this.ContainingType.BaseTypeNoUseSiteDiagnostics;
			if (!baseTypeNoUseSiteDiagnostics.IsObjectType())
			{
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				return SynthesizedRecordClone.FindValidCloneMethod(baseTypeNoUseSiteDiagnostics, ref discarded);
			}
			return null;
		}

		// Token: 0x06007527 RID: 29991 RVA: 0x001B47DC File Offset: 0x001B29DC
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters", "IsVararg", "DeclaredConstraintsForOverrideOrImplementation" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 0, 1 })]
		protected override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>> MakeParametersAndBindReturnType(BindingDiagnosticBag diagnostics)
		{
			MethodSymbol methodSymbol = this.VirtualCloneInBase();
			return new ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>>((methodSymbol != null) ? methodSymbol.ReturnTypeWithAnnotations : TypeWithAnnotations.Create(true, this.ContainingType, false), ImmutableArray<ParameterSymbol>.Empty, false, ImmutableArray<TypeParameterConstraintClause>.Empty);
		}

		// Token: 0x06007528 RID: 29992 RVA: 0x001B4818 File Offset: 0x001B2A18
		protected override int GetParameterCountFromSyntax()
		{
			return 0;
		}

		// Token: 0x06007529 RID: 29993 RVA: 0x001B481C File Offset: 0x001B2A1C
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.ContainingType.GetNonNullSyntaxNode(), compilationState, diagnostics);
			try
			{
				if (!base.ReturnType.IsErrorType())
				{
					foreach (MethodSymbol methodSymbol in this.ContainingType.InstanceConstructors)
					{
						if (methodSymbol.ParameterCount == 1 && methodSymbol.Parameters[0].RefKind == RefKind.None && methodSymbol.Parameters[0].Type.Equals(this.ContainingType, TypeCompareKind.AllIgnoreOptions))
						{
							syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Return(syntheticBoundNodeFactory.New(methodSymbol, new BoundExpression[] { syntheticBoundNodeFactory.This() })));
							return;
						}
					}
					throw ExceptionUtilities.Unreachable;
				}
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
			}
		}

		// Token: 0x0600752A RID: 29994 RVA: 0x001B4920 File Offset: 0x001B2B20
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static MethodSymbol FindValidCloneMethod(TypeSymbol containingType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!containingType.IsObjectType())
			{
				NamedTypeSymbol namedTypeSymbol = containingType as NamedTypeSymbol;
				if (namedTypeSymbol != null)
				{
					if (!namedTypeSymbol.HasPossibleWellKnownCloneMethod())
					{
						return null;
					}
					MethodSymbol methodSymbol = null;
					foreach (Symbol symbol in containingType.GetMembers("<Clone>$"))
					{
						MethodSymbol methodSymbol2 = symbol as MethodSymbol;
						if (methodSymbol2 != null && symbol.DeclaredAccessibility == Accessibility.Public && !symbol.IsStatic && methodSymbol2.ParameterCount == 0 && methodSymbol2.Arity == 0)
						{
							if (methodSymbol != null)
							{
								return null;
							}
							methodSymbol = methodSymbol2;
						}
					}
					if (methodSymbol == null || (!containingType.IsSealed && !methodSymbol.IsOverride && !methodSymbol.IsVirtual && !methodSymbol.IsAbstract) || !containingType.IsEqualToOrDerivedFrom(methodSymbol.ReturnType, TypeCompareKind.AllIgnoreOptions, ref useSiteInfo))
					{
						return null;
					}
					return methodSymbol;
				}
			}
			return null;
		}
	}
}
