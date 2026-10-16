using System;
using System.Collections.Immutable;
using System.Reflection;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000530 RID: 1328
	internal abstract class SourceDelegateMethodSymbol : SourceMemberMethodSymbol
	{
		// Token: 0x06006F7A RID: 28538 RVA: 0x00199D58 File Offset: 0x00197F58
		protected SourceDelegateMethodSymbol(SourceMemberContainerTypeSymbol delegateType, TypeWithAnnotations returnType, DelegateDeclarationSyntax syntax, MethodKind methodKind, DeclarationModifiers declarationModifiers)
			: base(delegateType, syntax.GetReference(), syntax.Identifier.GetLocation(), false)
		{
			this._returnType = returnType;
			base.MakeFlags(methodKind, declarationModifiers, this._returnType.IsVoidType(), false, false, false);
		}

		// Token: 0x06006F7B RID: 28539 RVA: 0x00199DA0 File Offset: 0x00197FA0
		protected void InitializeParameters(ImmutableArray<ParameterSymbol> parameters)
		{
			this._parameters = parameters;
		}

		// Token: 0x06006F7C RID: 28540 RVA: 0x00199DAC File Offset: 0x00197FAC
		internal static void AddDelegateMembers(SourceMemberContainerTypeSymbol delegateType, ArrayBuilder<Symbol> symbols, DelegateDeclarationSyntax syntax, BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = delegateType.DeclaringCompilation;
			Binder binder = delegateType.GetBinder(syntax.ParameterList);
			RefKind refKind;
			TypeSyntax typeSyntax = syntax.ReturnType.SkipRef(out refKind);
			TypeWithAnnotations typeWithAnnotations = binder.BindType(typeSyntax, diagnostics, null, false);
			TypeWithAnnotations typeWithAnnotations2 = TypeWithAnnotations.Create(binder.GetSpecialType(SpecialType.System_Void, diagnostics, syntax), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			TypeWithAnnotations typeWithAnnotations3 = TypeWithAnnotations.Create(binder.GetSpecialType(SpecialType.System_Object, diagnostics, syntax), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			TypeWithAnnotations typeWithAnnotations4 = TypeWithAnnotations.Create(binder.GetSpecialType(SpecialType.System_IntPtr, diagnostics, syntax), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			if (typeWithAnnotations.IsRestrictedType(true))
			{
				diagnostics.Add(ErrorCode.ERR_MethodReturnCantBeRefAny, typeSyntax.Location, new object[] { typeWithAnnotations.Type });
			}
			SourceDelegateMethodSymbol.InvokeMethod invokeMethod = new SourceDelegateMethodSymbol.InvokeMethod(delegateType, refKind, typeWithAnnotations, syntax, binder, diagnostics);
			invokeMethod.CheckDelegateVarianceSafety(diagnostics);
			symbols.Add(invokeMethod);
			symbols.Add(new SourceDelegateMethodSymbol.Constructor(delegateType, typeWithAnnotations2, typeWithAnnotations3, typeWithAnnotations4, syntax));
			if (binder.Compilation.GetSpecialType(SpecialType.System_IAsyncResult).TypeKind != TypeKind.Error && binder.Compilation.GetSpecialType(SpecialType.System_AsyncCallback).TypeKind != TypeKind.Error && !delegateType.IsCompilationOutputWinMdObj())
			{
				TypeWithAnnotations typeWithAnnotations5 = TypeWithAnnotations.Create(binder.GetSpecialType(SpecialType.System_IAsyncResult, diagnostics, syntax), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				TypeWithAnnotations typeWithAnnotations6 = TypeWithAnnotations.Create(binder.GetSpecialType(SpecialType.System_AsyncCallback, diagnostics, syntax), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				symbols.Add(new SourceDelegateMethodSymbol.BeginInvokeMethod(invokeMethod, typeWithAnnotations5, typeWithAnnotations3, typeWithAnnotations6, syntax));
				symbols.Add(new SourceDelegateMethodSymbol.EndInvokeMethod(invokeMethod, typeWithAnnotations5, syntax));
			}
			if (delegateType.DeclaredAccessibility <= Accessibility.Private)
			{
				return;
			}
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, delegateType.ContainingAssembly);
			if (!delegateType.IsNoMoreVisibleThan(invokeMethod.ReturnTypeWithAnnotations, ref compoundUseSiteInfo))
			{
				diagnostics.Add(ErrorCode.ERR_BadVisDelegateReturn, delegateType.Locations[0], new object[] { delegateType, invokeMethod.ReturnType });
			}
			foreach (ParameterSymbol parameterSymbol in invokeMethod.Parameters)
			{
				if (!parameterSymbol.TypeWithAnnotations.IsAtLeastAsVisibleAs(delegateType, ref compoundUseSiteInfo))
				{
					diagnostics.Add(ErrorCode.ERR_BadVisDelegateParam, delegateType.Locations[0], new object[] { delegateType, parameterSymbol.Type });
				}
			}
			diagnostics.Add(delegateType.Locations[0], compoundUseSiteInfo);
		}

		// Token: 0x06006F7D RID: 28541 RVA: 0x00199FF9 File Offset: 0x001981F9
		protected override void MethodChecks(BindingDiagnosticBag diagnostics)
		{
		}

		// Token: 0x17001DBE RID: 7614
		// (get) Token: 0x06006F7E RID: 28542 RVA: 0x00199FFB File Offset: 0x001981FB
		public sealed override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001DBF RID: 7615
		// (get) Token: 0x06006F7F RID: 28543 RVA: 0x00199FFE File Offset: 0x001981FE
		public sealed override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17001DC0 RID: 7616
		// (get) Token: 0x06006F80 RID: 28544 RVA: 0x0019A006 File Offset: 0x00198206
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x06006F81 RID: 28545 RVA: 0x0019A00D File Offset: 0x0019820D
		public override ImmutableArray<ImmutableArray<TypeWithAnnotations>> GetTypeParameterConstraintTypes()
		{
			return ImmutableArray<ImmutableArray<TypeWithAnnotations>>.Empty;
		}

		// Token: 0x06006F82 RID: 28546 RVA: 0x0019A014 File Offset: 0x00198214
		public override ImmutableArray<TypeParameterConstraintKind> GetTypeParameterConstraintKinds()
		{
			return ImmutableArray<TypeParameterConstraintKind>.Empty;
		}

		// Token: 0x17001DC1 RID: 7617
		// (get) Token: 0x06006F83 RID: 28547 RVA: 0x0019A01B File Offset: 0x0019821B
		public sealed override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return this._returnType;
			}
		}

		// Token: 0x17001DC2 RID: 7618
		// (get) Token: 0x06006F84 RID: 28548 RVA: 0x0019A023 File Offset: 0x00198223
		public sealed override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001DC3 RID: 7619
		// (get) Token: 0x06006F85 RID: 28549 RVA: 0x0019A026 File Offset: 0x00198226
		internal override bool IsExpressionBodied
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001DC4 RID: 7620
		// (get) Token: 0x06006F86 RID: 28550 RVA: 0x0019A029 File Offset: 0x00198229
		internal override bool GenerateDebugInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001DC5 RID: 7621
		// (get) Token: 0x06006F87 RID: 28551 RVA: 0x0019A02C File Offset: 0x0019822C
		protected sealed override IAttributeTargetSymbol AttributeOwner
		{
			get
			{
				return (SourceNamedTypeSymbol)this.ContainingSymbol;
			}
		}

		// Token: 0x17001DC6 RID: 7622
		// (get) Token: 0x06006F88 RID: 28552 RVA: 0x0019A039 File Offset: 0x00198239
		internal sealed override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06006F89 RID: 28553 RVA: 0x0019A03C File Offset: 0x0019823C
		internal sealed override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(((SourceNamedTypeSymbol)this.ContainingSymbol).GetAttributeDeclarations());
		}

		// Token: 0x06006F8A RID: 28554 RVA: 0x0019A053 File Offset: 0x00198253
		internal sealed override AttributeTargets GetAttributeTarget()
		{
			return 4096;
		}

		// Token: 0x06006F8B RID: 28555 RVA: 0x0019A05A File Offset: 0x0019825A
		private static string GetUniqueParameterName(ArrayBuilder<ParameterSymbol> currentParameters, string name)
		{
			while (!SourceDelegateMethodSymbol.IsUnique(currentParameters, name))
			{
				name = "__" + name;
			}
			return name;
		}

		// Token: 0x06006F8C RID: 28556 RVA: 0x0019A078 File Offset: 0x00198278
		private static bool IsUnique(ArrayBuilder<ParameterSymbol> currentParameters, string name)
		{
			ArrayBuilder<ParameterSymbol>.Enumerator enumerator = currentParameters.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (string.CompareOrdinal(enumerator.Current.Name, name) == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001B58 RID: 7000
		private ImmutableArray<ParameterSymbol> _parameters;

		// Token: 0x04001B59 RID: 7001
		private readonly TypeWithAnnotations _returnType;

		// Token: 0x02000915 RID: 2325
		private sealed class Constructor : SourceDelegateMethodSymbol
		{
			// Token: 0x06008FB1 RID: 36785 RVA: 0x001F963C File Offset: 0x001F783C
			internal Constructor(SourceMemberContainerTypeSymbol delegateType, TypeWithAnnotations voidType, TypeWithAnnotations objectType, TypeWithAnnotations intPtrType, DelegateDeclarationSyntax syntax)
				: base(delegateType, voidType, syntax, MethodKind.Constructor, DeclarationModifiers.Public)
			{
				base.InitializeParameters(ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(this, objectType, 0, RefKind.None, "object", default(ImmutableArray<CustomModifier>), null), SynthesizedParameterSymbol.Create(this, intPtrType, 1, RefKind.None, "method", default(ImmutableArray<CustomModifier>), null)));
			}

			// Token: 0x1700288E RID: 10382
			// (get) Token: 0x06008FB2 RID: 36786 RVA: 0x001F9692 File Offset: 0x001F7892
			public override string Name
			{
				get
				{
					return ".ctor";
				}
			}

			// Token: 0x1700288F RID: 10383
			// (get) Token: 0x06008FB3 RID: 36787 RVA: 0x001F9699 File Offset: 0x001F7899
			public override RefKind RefKind
			{
				get
				{
					return RefKind.None;
				}
			}

			// Token: 0x06008FB4 RID: 36788 RVA: 0x001F969C File Offset: 0x001F789C
			internal override OneOrMany<SyntaxList<AttributeListSyntax>> GetReturnTypeAttributeDeclarations()
			{
				return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(default(SyntaxList<AttributeListSyntax>));
			}

			// Token: 0x06008FB5 RID: 36789 RVA: 0x001F96B7 File Offset: 0x001F78B7
			internal override LexicalSortKey GetLexicalSortKey()
			{
				return new LexicalSortKey(this.syntaxReferenceOpt.GetLocation(), this.DeclaringCompilation);
			}
		}

		// Token: 0x02000916 RID: 2326
		private sealed class InvokeMethod : SourceDelegateMethodSymbol
		{
			// Token: 0x06008FB6 RID: 36790 RVA: 0x001F96D0 File Offset: 0x001F78D0
			internal InvokeMethod(SourceMemberContainerTypeSymbol delegateType, RefKind refKind, TypeWithAnnotations returnType, DelegateDeclarationSyntax syntax, Binder binder, BindingDiagnosticBag diagnostics)
				: base(delegateType, returnType, syntax, MethodKind.DelegateInvoke, DeclarationModifiers.Public | DeclarationModifiers.Virtual)
			{
				this._refKind = refKind;
				SyntaxToken syntaxToken;
				ImmutableArray<ParameterSymbol> immutableArray = ParameterHelpers.MakeParameters(binder, this, syntax.ParameterList, out syntaxToken, diagnostics, true, false, true);
				if (syntaxToken.Kind() == SyntaxKind.ArgListKeyword)
				{
					diagnostics.Add(ErrorCode.ERR_IllegalVarArgs, new SourceLocation(in syntaxToken));
				}
				if (this._refKind == RefKind.In)
				{
					NamedTypeSymbol wellKnownType = binder.GetWellKnownType(WellKnownType.System_Runtime_InteropServices_InAttribute, diagnostics, syntax.ReturnType);
					this._refCustomModifiers = ImmutableArray.Create<CustomModifier>(CSharpCustomModifier.CreateRequired(wellKnownType));
				}
				else
				{
					this._refCustomModifiers = ImmutableArray<CustomModifier>.Empty;
				}
				base.InitializeParameters(immutableArray);
			}

			// Token: 0x17002890 RID: 10384
			// (get) Token: 0x06008FB7 RID: 36791 RVA: 0x001F9770 File Offset: 0x001F7970
			public override string Name
			{
				get
				{
					return "Invoke";
				}
			}

			// Token: 0x17002891 RID: 10385
			// (get) Token: 0x06008FB8 RID: 36792 RVA: 0x001F9777 File Offset: 0x001F7977
			public override RefKind RefKind
			{
				get
				{
					return this._refKind;
				}
			}

			// Token: 0x06008FB9 RID: 36793 RVA: 0x001F977F File Offset: 0x001F797F
			internal override LexicalSortKey GetLexicalSortKey()
			{
				return new LexicalSortKey(this.syntaxReferenceOpt.GetLocation(), this.DeclaringCompilation);
			}

			// Token: 0x06008FBA RID: 36794 RVA: 0x001F9798 File Offset: 0x001F7998
			internal override void AfterAddingTypeMembersChecks(ConversionsBase conversions, BindingDiagnosticBag diagnostics)
			{
				Location location = ((DelegateDeclarationSyntax)base.SyntaxRef.GetSyntax(default(CancellationToken))).ReturnType.GetLocation();
				CSharpCompilation declaringCompilation = this.DeclaringCompilation;
				base.AfterAddingTypeMembersChecks(conversions, diagnostics);
				if (this._refKind == RefKind.In)
				{
					declaringCompilation.EnsureIsReadOnlyAttributeExists(diagnostics, location, true);
				}
				ParameterHelpers.EnsureIsReadOnlyAttributeExists(declaringCompilation, this.Parameters, diagnostics, true);
				if (base.ReturnType.ContainsNativeInteger())
				{
					declaringCompilation.EnsureNativeIntegerAttributeExists(diagnostics, location, true);
				}
				ParameterHelpers.EnsureNativeIntegerAttributeExists(declaringCompilation, this.Parameters, diagnostics, true);
				if (declaringCompilation.ShouldEmitNullableAttributes(this) && this.ReturnTypeWithAnnotations.NeedsNullableAttribute())
				{
					declaringCompilation.EnsureNullableAttributeExists(diagnostics, location, true);
				}
				ParameterHelpers.EnsureNullableAttributeExists(declaringCompilation, this, this.Parameters, diagnostics, true);
			}

			// Token: 0x17002892 RID: 10386
			// (get) Token: 0x06008FBB RID: 36795 RVA: 0x001F984D File Offset: 0x001F7A4D
			public override ImmutableArray<CustomModifier> RefCustomModifiers
			{
				get
				{
					return this._refCustomModifiers;
				}
			}

			// Token: 0x0400276B RID: 10091
			private readonly RefKind _refKind;

			// Token: 0x0400276C RID: 10092
			private readonly ImmutableArray<CustomModifier> _refCustomModifiers;
		}

		// Token: 0x02000917 RID: 2327
		private sealed class BeginInvokeMethod : SourceDelegateMethodSymbol
		{
			// Token: 0x06008FBC RID: 36796 RVA: 0x001F9858 File Offset: 0x001F7A58
			internal BeginInvokeMethod(SourceDelegateMethodSymbol.InvokeMethod invoke, TypeWithAnnotations iAsyncResultType, TypeWithAnnotations objectType, TypeWithAnnotations asyncCallbackType, DelegateDeclarationSyntax syntax)
				: base((SourceNamedTypeSymbol)invoke.ContainingType, iAsyncResultType, syntax, MethodKind.Ordinary, DeclarationModifiers.Public | DeclarationModifiers.Virtual)
			{
				ArrayBuilder<ParameterSymbol> instance = ArrayBuilder<ParameterSymbol>.GetInstance();
				foreach (ParameterSymbol parameterSymbol in invoke.Parameters)
				{
					SourceParameterSymbol sourceParameterSymbol = (SourceParameterSymbol)parameterSymbol;
					SourceClonedParameterSymbol sourceClonedParameterSymbol = new SourceClonedParameterSymbol(sourceParameterSymbol, this, sourceParameterSymbol.Ordinal, true);
					instance.Add(sourceClonedParameterSymbol);
				}
				int parameterCount = invoke.ParameterCount;
				instance.Add(SynthesizedParameterSymbol.Create(this, asyncCallbackType, parameterCount, RefKind.None, SourceDelegateMethodSymbol.GetUniqueParameterName(instance, "callback"), default(ImmutableArray<CustomModifier>), null));
				instance.Add(SynthesizedParameterSymbol.Create(this, objectType, parameterCount + 1, RefKind.None, SourceDelegateMethodSymbol.GetUniqueParameterName(instance, "object"), default(ImmutableArray<CustomModifier>), null));
				base.InitializeParameters(instance.ToImmutableAndFree());
			}

			// Token: 0x17002893 RID: 10387
			// (get) Token: 0x06008FBD RID: 36797 RVA: 0x001F9928 File Offset: 0x001F7B28
			public override string Name
			{
				get
				{
					return "BeginInvoke";
				}
			}

			// Token: 0x17002894 RID: 10388
			// (get) Token: 0x06008FBE RID: 36798 RVA: 0x001F992F File Offset: 0x001F7B2F
			public override RefKind RefKind
			{
				get
				{
					return RefKind.None;
				}
			}

			// Token: 0x06008FBF RID: 36799 RVA: 0x001F9934 File Offset: 0x001F7B34
			internal override OneOrMany<SyntaxList<AttributeListSyntax>> GetReturnTypeAttributeDeclarations()
			{
				return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(default(SyntaxList<AttributeListSyntax>));
			}
		}

		// Token: 0x02000918 RID: 2328
		private sealed class EndInvokeMethod : SourceDelegateMethodSymbol
		{
			// Token: 0x06008FC0 RID: 36800 RVA: 0x001F9950 File Offset: 0x001F7B50
			internal EndInvokeMethod(SourceDelegateMethodSymbol.InvokeMethod invoke, TypeWithAnnotations iAsyncResultType, DelegateDeclarationSyntax syntax)
				: base((SourceNamedTypeSymbol)invoke.ContainingType, invoke.ReturnTypeWithAnnotations, syntax, MethodKind.Ordinary, DeclarationModifiers.Public | DeclarationModifiers.Virtual)
			{
				this._invoke = invoke;
				ArrayBuilder<ParameterSymbol> instance = ArrayBuilder<ParameterSymbol>.GetInstance();
				int num = 0;
				foreach (ParameterSymbol parameterSymbol in invoke.Parameters)
				{
					SourceParameterSymbol sourceParameterSymbol = (SourceParameterSymbol)parameterSymbol;
					if (sourceParameterSymbol.RefKind != RefKind.None)
					{
						SourceClonedParameterSymbol sourceClonedParameterSymbol = new SourceClonedParameterSymbol(sourceParameterSymbol, this, num++, true);
						instance.Add(sourceClonedParameterSymbol);
					}
				}
				instance.Add(SynthesizedParameterSymbol.Create(this, iAsyncResultType, num++, RefKind.None, SourceDelegateMethodSymbol.GetUniqueParameterName(instance, "result"), default(ImmutableArray<CustomModifier>), null));
				base.InitializeParameters(instance.ToImmutableAndFree());
			}

			// Token: 0x17002895 RID: 10389
			// (get) Token: 0x06008FC1 RID: 36801 RVA: 0x001F9A09 File Offset: 0x001F7C09
			protected override SourceMemberMethodSymbol BoundAttributesSource
			{
				get
				{
					return this._invoke;
				}
			}

			// Token: 0x17002896 RID: 10390
			// (get) Token: 0x06008FC2 RID: 36802 RVA: 0x001F9A11 File Offset: 0x001F7C11
			public override string Name
			{
				get
				{
					return "EndInvoke";
				}
			}

			// Token: 0x17002897 RID: 10391
			// (get) Token: 0x06008FC3 RID: 36803 RVA: 0x001F9A18 File Offset: 0x001F7C18
			public override RefKind RefKind
			{
				get
				{
					return this._invoke.RefKind;
				}
			}

			// Token: 0x17002898 RID: 10392
			// (get) Token: 0x06008FC4 RID: 36804 RVA: 0x001F9A25 File Offset: 0x001F7C25
			public override ImmutableArray<CustomModifier> RefCustomModifiers
			{
				get
				{
					return this._invoke.RefCustomModifiers;
				}
			}

			// Token: 0x0400276D RID: 10093
			private readonly SourceDelegateMethodSymbol.InvokeMethod _invoke;
		}
	}
}
