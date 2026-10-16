using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200059C RID: 1436
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedSimpleProgramEntryPointSymbol : SourceMemberMethodSymbol
	{
		// Token: 0x06007814 RID: 30740 RVA: 0x001B8BA4 File Offset: 0x001B6DA4
		internal SynthesizedSimpleProgramEntryPointSymbol(SimpleProgramNamedTypeSymbol containingType, SingleTypeDeclaration declaration, BindingDiagnosticBag diagnostics)
			: base(containingType, declaration.SyntaxReference, ImmutableArray.Create<Location>(declaration.SyntaxReference.GetLocation()), declaration.IsIterator)
		{
			this._declaration = declaration;
			bool hasAwaitExpressions = declaration.HasAwaitExpressions;
			bool hasReturnWithExpression = declaration.HasReturnWithExpression;
			CSharpCompilation declaringCompilation = containingType.DeclaringCompilation;
			if (hasAwaitExpressions)
			{
				if (hasReturnWithExpression)
				{
					this._returnType = Binder.GetWellKnownType(declaringCompilation, WellKnownType.System_Threading_Tasks_Task_T, diagnostics, NoLocation.Singleton).Construct(new TypeSymbol[] { Binder.GetSpecialType(declaringCompilation, SpecialType.System_Int32, NoLocation.Singleton, diagnostics) });
				}
				else
				{
					this._returnType = Binder.GetWellKnownType(declaringCompilation, WellKnownType.System_Threading_Tasks_Task, diagnostics, NoLocation.Singleton);
				}
			}
			else if (hasReturnWithExpression)
			{
				this._returnType = Binder.GetSpecialType(declaringCompilation, SpecialType.System_Int32, NoLocation.Singleton, diagnostics);
			}
			else
			{
				this._returnType = Binder.GetSpecialType(declaringCompilation, SpecialType.System_Void, NoLocation.Singleton, diagnostics);
			}
			bool flag = SynthesizedSimpleProgramEntryPointSymbol.IsNullableAnalysisEnabled(declaringCompilation, this.CompilationUnit);
			base.MakeFlags(MethodKind.Ordinary, DeclarationModifiers.Static | DeclarationModifiers.Private | (hasAwaitExpressions ? DeclarationModifiers.Async : DeclarationModifiers.None), !hasAwaitExpressions && !hasReturnWithExpression, false, flag, false);
			this._parameters = ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(this, TypeWithAnnotations.Create(ArrayTypeSymbol.CreateCSharpArray(declaringCompilation.Assembly, TypeWithAnnotations.Create(Binder.GetSpecialType(declaringCompilation, SpecialType.System_String, NoLocation.Singleton, diagnostics), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), 1), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), 0, RefKind.None, "args", default(ImmutableArray<CustomModifier>), null));
		}

		// Token: 0x1700221E RID: 8734
		// (get) Token: 0x06007815 RID: 30741 RVA: 0x001B8CFE File Offset: 0x001B6EFE
		public override string Name
		{
			get
			{
				return "<Main>$";
			}
		}

		// Token: 0x1700221F RID: 8735
		// (get) Token: 0x06007816 RID: 30742 RVA: 0x001B8D05 File Offset: 0x001B6F05
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002220 RID: 8736
		// (get) Token: 0x06007817 RID: 30743 RVA: 0x001B8D08 File Offset: 0x001B6F08
		public override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002221 RID: 8737
		// (get) Token: 0x06007818 RID: 30744 RVA: 0x001B8D0B File Offset: 0x001B6F0B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x17002222 RID: 8738
		// (get) Token: 0x06007819 RID: 30745 RVA: 0x001B8D12 File Offset: 0x001B6F12
		internal override int ParameterCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17002223 RID: 8739
		// (get) Token: 0x0600781A RID: 30746 RVA: 0x001B8D15 File Offset: 0x001B6F15
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17002224 RID: 8740
		// (get) Token: 0x0600781B RID: 30747 RVA: 0x001B8D1D File Offset: 0x001B6F1D
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Private;
			}
		}

		// Token: 0x17002225 RID: 8741
		// (get) Token: 0x0600781C RID: 30748 RVA: 0x001B8D20 File Offset: 0x001B6F20
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x17002226 RID: 8742
		// (get) Token: 0x0600781D RID: 30749 RVA: 0x001B8D24 File Offset: 0x001B6F24
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return TypeWithAnnotations.Create(this._returnType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
		}

		// Token: 0x17002227 RID: 8743
		// (get) Token: 0x0600781E RID: 30750 RVA: 0x001B8D46 File Offset: 0x001B6F46
		public override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17002228 RID: 8744
		// (get) Token: 0x0600781F RID: 30751 RVA: 0x001B8D49 File Offset: 0x001B6F49
		public override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x17002229 RID: 8745
		// (get) Token: 0x06007820 RID: 30752 RVA: 0x001B8D50 File Offset: 0x001B6F50
		public override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x1700222A RID: 8746
		// (get) Token: 0x06007821 RID: 30753 RVA: 0x001B8D53 File Offset: 0x001B6F53
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x1700222B RID: 8747
		// (get) Token: 0x06007822 RID: 30754 RVA: 0x001B8D5A File Offset: 0x001B6F5A
		public sealed override bool IsImplicitlyDeclared
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700222C RID: 8748
		// (get) Token: 0x06007823 RID: 30755 RVA: 0x001B8D5D File Offset: 0x001B6F5D
		internal sealed override bool GenerateDebugInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007824 RID: 30756 RVA: 0x001B8D60 File Offset: 0x001B6F60
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			return localPosition;
		}

		// Token: 0x06007825 RID: 30757 RVA: 0x001B8D63 File Offset: 0x001B6F63
		protected override void MethodChecks(BindingDiagnosticBag diagnostics)
		{
		}

		// Token: 0x1700222D RID: 8749
		// (get) Token: 0x06007826 RID: 30758 RVA: 0x001B8D65 File Offset: 0x001B6F65
		internal override bool IsExpressionBodied
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007827 RID: 30759 RVA: 0x001B8D68 File Offset: 0x001B6F68
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override ImmutableArray<ImmutableArray<TypeWithAnnotations>> GetTypeParameterConstraintTypes()
		{
			return ImmutableArray<ImmutableArray<TypeWithAnnotations>>.Empty;
		}

		// Token: 0x06007828 RID: 30760 RVA: 0x001B8D6F File Offset: 0x001B6F6F
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override ImmutableArray<TypeParameterConstraintKind> GetTypeParameterConstraintKinds()
		{
			return ImmutableArray<TypeParameterConstraintKind>.Empty;
		}

		// Token: 0x1700222E RID: 8750
		// (get) Token: 0x06007829 RID: 30761 RVA: 0x001B8D76 File Offset: 0x001B6F76
		protected override object MethodChecksLockObject
		{
			get
			{
				return this._declaration;
			}
		}

		// Token: 0x1700222F RID: 8751
		// (get) Token: 0x0600782A RID: 30762 RVA: 0x001B8D7E File Offset: 0x001B6F7E
		internal CompilationUnitSyntax CompilationUnit
		{
			get
			{
				return (CompilationUnitSyntax)this.SyntaxNode;
			}
		}

		// Token: 0x0600782B RID: 30763 RVA: 0x001B8D8B File Offset: 0x001B6F8B
		internal override ExecutableCodeBinder TryGetBodyBinder([global::System.Runtime.CompilerServices.Nullable(2)] BinderFactory binderFactoryOpt = null, bool ignoreAccessibility = false)
		{
			return this.GetBodyBinder(ignoreAccessibility);
		}

		// Token: 0x0600782C RID: 30764 RVA: 0x001B8D94 File Offset: 0x001B6F94
		private ExecutableCodeBinder CreateBodyBinder(bool ignoreAccessibility)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Binder binder = new BuckStopsHereBinder(declaringCompilation);
			NamespaceSymbol globalNamespace = declaringCompilation.GlobalNamespace;
			SourceNamespaceSymbol sourceNamespaceSymbol = (SourceNamespaceSymbol)declaringCompilation.SourceModule.GlobalNamespace;
			CSharpSyntaxNode syntaxNode = this.SyntaxNode;
			binder = WithExternAndUsingAliasesBinder.Create(sourceNamespaceSymbol, syntaxNode, WithUsingNamespacesAndTypesBinder.Create(sourceNamespaceSymbol, syntaxNode, binder, false, false));
			binder = new InContainerBinder(globalNamespace, binder);
			binder = new InContainerBinder(this.ContainingType, binder);
			binder = new InMethodBinder(this, binder);
			binder = binder.WithAdditionalFlags(ignoreAccessibility ? BinderFlags.IgnoreAccessibility : BinderFlags.None);
			return new ExecutableCodeBinder(syntaxNode, this, binder, null);
		}

		// Token: 0x0600782D RID: 30765 RVA: 0x001B8E18 File Offset: 0x001B7018
		internal ExecutableCodeBinder GetBodyBinder(bool ignoreAccessibility)
		{
			ref WeakReference<ExecutableCodeBinder> ptr = (ref ignoreAccessibility ? ref this._weakIgnoreAccessibilityBodyBinder : ref this._weakBodyBinder);
			ExecutableCodeBinder executableCodeBinder;
			for (;;)
			{
				WeakReference<ExecutableCodeBinder> weakReference = ptr;
				if (weakReference != null && weakReference.TryGetTarget(ref executableCodeBinder))
				{
					break;
				}
				ExecutableCodeBinder executableCodeBinder2 = this.CreateBodyBinder(ignoreAccessibility);
				if (Interlocked.CompareExchange<WeakReference<ExecutableCodeBinder>>(ref ptr, new WeakReference<ExecutableCodeBinder>(executableCodeBinder2), weakReference) == weakReference)
				{
					return executableCodeBinder2;
				}
			}
			return executableCodeBinder;
		}

		// Token: 0x0600782E RID: 30766 RVA: 0x001B8E64 File Offset: 0x001B7064
		internal override bool IsDefinedInSourceTree(SyntaxTree tree, TextSpan? definedWithinSpan, CancellationToken cancellationToken)
		{
			if (this._declaration.SyntaxReference.SyntaxTree == tree)
			{
				if (definedWithinSpan == null)
				{
					return true;
				}
				TextSpan valueOrDefault = definedWithinSpan.GetValueOrDefault();
				foreach (SyntaxNode syntaxNode in Enumerable.OfType<GlobalStatementSyntax>(((CompilationUnitSyntax)tree.GetRoot(cancellationToken)).Members))
				{
					cancellationToken.ThrowIfCancellationRequested();
					if (syntaxNode.Span.IntersectsWith(valueOrDefault))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x17002230 RID: 8752
		// (get) Token: 0x0600782F RID: 30767 RVA: 0x001B8F04 File Offset: 0x001B7104
		public SyntaxNode ReturnTypeSyntax
		{
			get
			{
				return Enumerable.First<MemberDeclarationSyntax>(this.CompilationUnit.Members, (MemberDeclarationSyntax m) => m.Kind() == SyntaxKind.GlobalStatement);
			}
		}

		// Token: 0x06007830 RID: 30768 RVA: 0x001B8F3C File Offset: 0x001B713C
		private static bool IsNullableAnalysisEnabled(CSharpCompilation compilation, CompilationUnitSyntax syntax)
		{
			foreach (MemberDeclarationSyntax memberDeclarationSyntax in syntax.Members)
			{
				if (memberDeclarationSyntax.Kind() == SyntaxKind.GlobalStatement && compilation.IsNullableAnalysisEnabledIn(memberDeclarationSyntax))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001CF5 RID: 7413
		private readonly SingleTypeDeclaration _declaration;

		// Token: 0x04001CF6 RID: 7414
		private readonly TypeSymbol _returnType;

		// Token: 0x04001CF7 RID: 7415
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<ParameterSymbol> _parameters;

		// Token: 0x04001CF8 RID: 7416
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private WeakReference<ExecutableCodeBinder> _weakBodyBinder;

		// Token: 0x04001CF9 RID: 7417
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private WeakReference<ExecutableCodeBinder> _weakIgnoreAccessibilityBodyBinder;
	}
}
