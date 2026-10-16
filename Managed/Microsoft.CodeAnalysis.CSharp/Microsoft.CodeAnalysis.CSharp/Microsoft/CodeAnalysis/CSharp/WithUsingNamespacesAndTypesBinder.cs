using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000081 RID: 129
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class WithUsingNamespacesAndTypesBinder : Binder
	{
		// Token: 0x06000B0C RID: 2828 RVA: 0x0005195C File Offset: 0x0004FB5C
		protected WithUsingNamespacesAndTypesBinder(Binder next, bool withImportChainEntry)
			: base(next, null)
		{
			this._withImportChainEntry = withImportChainEntry;
		}

		// Token: 0x06000B0D RID: 2829
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal abstract ImmutableArray<NamespaceOrTypeAndUsingDirective> GetUsings([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved);

		// Token: 0x06000B0E RID: 2830 RVA: 0x00051970 File Offset: 0x0004FB70
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override AssemblySymbol GetForwardedToAssemblyInUsingNamespaces(string name, ref NamespaceOrTypeSymbol qualifierOpt, BindingDiagnosticBag diagnostics, Location location)
		{
			foreach (NamespaceOrTypeAndUsingDirective namespaceOrTypeAndUsingDirective in this.GetUsings(null))
			{
				NamespaceOrTypeSymbol namespaceOrType = namespaceOrTypeAndUsingDirective.NamespaceOrType;
				string text = ((namespaceOrType != null) ? namespaceOrType.ToString() : null) + "." + name;
				AssemblySymbol forwardedToAssembly = base.GetForwardedToAssembly(text, diagnostics, location);
				if (forwardedToAssembly != null)
				{
					qualifierOpt = namespaceOrTypeAndUsingDirective.NamespaceOrType;
					return forwardedToAssembly;
				}
			}
			return base.GetForwardedToAssemblyInUsingNamespaces(name, ref qualifierOpt, diagnostics, location);
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x000519EA File Offset: 0x0004FBEA
		internal override bool SupportsExtensionMethods
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000519F0 File Offset: 0x0004FBF0
		internal override void GetCandidateExtensionMethods(ArrayBuilder<MethodSymbol> methods, string name, int arity, LookupOptions options, Binder originalBinder)
		{
			bool isSemanticModelBinder = originalBinder.IsSemanticModelBinder;
			bool flag = false;
			bool flag2 = false;
			foreach (NamespaceOrTypeAndUsingDirective namespaceOrTypeAndUsingDirective in this.GetUsings(null))
			{
				SymbolKind kind = namespaceOrTypeAndUsingDirective.NamespaceOrType.Kind;
				if (kind != SymbolKind.NamedType)
				{
					if (kind == SymbolKind.Namespace)
					{
						int count = methods.Count;
						((NamespaceSymbol)namespaceOrTypeAndUsingDirective.NamespaceOrType).GetExtensionMethods(methods, name, arity, options);
						if (methods.Count != count)
						{
							base.MarkImportDirective(namespaceOrTypeAndUsingDirective.UsingDirectiveReference, isSemanticModelBinder);
							flag = true;
						}
					}
				}
				else
				{
					int count2 = methods.Count;
					((NamedTypeSymbol)namespaceOrTypeAndUsingDirective.NamespaceOrType).GetExtensionMethods(methods, name, arity, options);
					if (methods.Count != count2)
					{
						base.MarkImportDirective(namespaceOrTypeAndUsingDirective.UsingDirectiveReference, isSemanticModelBinder);
						flag2 = true;
					}
				}
			}
			if (flag && flag2)
			{
				methods.RemoveDuplicates();
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00051ACC File Offset: 0x0004FCCC
		internal override void LookupSymbolsInSingleBinder(LookupResult result, string name, int arity, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved, LookupOptions options, Binder originalBinder, bool diagnose, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			bool isSemanticModelBinder = originalBinder.IsSemanticModelBinder;
			foreach (NamespaceOrTypeAndUsingDirective namespaceOrTypeAndUsingDirective in this.GetUsings(basesBeingResolved))
			{
				foreach (Symbol symbol in Binder.GetCandidateMembers(namespaceOrTypeAndUsingDirective.NamespaceOrType, name, options, originalBinder))
				{
					if (WithUsingNamespacesAndTypesBinder.IsValidLookupCandidateInUsings(symbol))
					{
						SingleLookupResult singleLookupResult = originalBinder.CheckViability(symbol, arity, options, null, diagnose, ref useSiteInfo, basesBeingResolved);
						if (singleLookupResult.Kind == LookupResultKind.Viable)
						{
							base.MarkImportDirective(namespaceOrTypeAndUsingDirective.UsingDirectiveReference, isSemanticModelBinder);
						}
						result.MergeEqual(singleLookupResult);
					}
				}
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00051B74 File Offset: 0x0004FD74
		private static bool IsValidLookupCandidateInUsings(Symbol symbol)
		{
			switch (symbol.Kind)
			{
			case SymbolKind.Method:
				if (!symbol.IsStatic || ((MethodSymbol)symbol).IsExtensionMethod)
				{
					return false;
				}
				return true;
			case SymbolKind.NamedType:
				return true;
			case SymbolKind.Namespace:
				return false;
			}
			if (!symbol.IsStatic)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00051BC8 File Offset: 0x0004FDC8
		protected override void AddLookupSymbolsInfoInSingleBinder(LookupSymbolsInfo result, LookupOptions options, Binder originalBinder)
		{
			if ((options & LookupOptions.LabelsOnly) == LookupOptions.Default)
			{
				options = (options & ~(LookupOptions.NamespaceAliasesOnly | LookupOptions.NamespacesOrTypesOnly)) | LookupOptions.MustNotBeNamespace;
				foreach (NamespaceOrTypeAndUsingDirective namespaceOrTypeAndUsingDirective in this.GetUsings(null))
				{
					foreach (Symbol symbol in namespaceOrTypeAndUsingDirective.NamespaceOrType.GetMembersUnordered())
					{
						if (WithUsingNamespacesAndTypesBinder.IsValidLookupCandidateInUsings(symbol) && originalBinder.CanAddLookupSymbolInfo(symbol, options, result, null, null))
						{
							result.AddSymbol(symbol, symbol.Name, symbol.GetArity());
						}
					}
				}
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00051C5D File Offset: 0x0004FE5D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected override SourceLocalSymbol LookupLocal(SyntaxToken nameToken)
		{
			return null;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00051C60 File Offset: 0x0004FE60
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected override LocalFunctionSymbol LookupLocalFunction(SyntaxToken nameToken)
		{
			return null;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00051C63 File Offset: 0x0004FE63
		internal override uint LocalScopeDepth
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00051C68 File Offset: 0x0004FE68
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override ImportChain ImportChain
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				if (this._lazyImportChain == null)
				{
					ImportChain importChain = base.Next.ImportChain;
					if (this._withImportChainEntry)
					{
						importChain = new ImportChain(this.GetImports(), importChain);
					}
					Interlocked.CompareExchange<ImportChain>(ref this._lazyImportChain, importChain, null);
				}
				return this._lazyImportChain;
			}
		}

		// Token: 0x06000B18 RID: 2840
		protected abstract Imports GetImports();

		// Token: 0x06000B19 RID: 2841 RVA: 0x00051CB2 File Offset: 0x0004FEB2
		internal static WithUsingNamespacesAndTypesBinder Create(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, Binder next, bool withPreviousSubmissionImports = false, bool withImportChainEntry = false)
		{
			if (withPreviousSubmissionImports)
			{
				return new WithUsingNamespacesAndTypesBinder.FromSyntaxWithPreviousSubmissionImports(declaringSymbol, declarationSyntax, next, withImportChainEntry);
			}
			return new WithUsingNamespacesAndTypesBinder.FromSyntax(declaringSymbol, declarationSyntax, next, withImportChainEntry);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00051CCC File Offset: 0x0004FECC
		internal static WithUsingNamespacesAndTypesBinder Create([global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<NamespaceOrTypeAndUsingDirective> namespacesOrTypes, Binder next, bool withImportChainEntry = false)
		{
			return new WithUsingNamespacesAndTypesBinder.FromNamespacesOrTypes(namespacesOrTypes, next, withImportChainEntry);
		}

		// Token: 0x04000484 RID: 1156
		private readonly bool _withImportChainEntry;

		// Token: 0x04000485 RID: 1157
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ImportChain _lazyImportChain;

		// Token: 0x0200067F RID: 1663
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class FromSyntax : WithUsingNamespacesAndTypesBinder
		{
			// Token: 0x0600830A RID: 33546 RVA: 0x001DB61D File Offset: 0x001D981D
			internal FromSyntax(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, Binder next, bool withImportChainEntry)
				: base(next, withImportChainEntry)
			{
				this._declaringSymbol = declaringSymbol;
				this._declarationSyntax = declarationSyntax;
			}

			// Token: 0x0600830B RID: 33547 RVA: 0x001DB636 File Offset: 0x001D9836
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			internal override ImmutableArray<NamespaceOrTypeAndUsingDirective> GetUsings([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				if (this._lazyUsings.IsDefault)
				{
					ImmutableInterlocked.InterlockedInitialize<NamespaceOrTypeAndUsingDirective>(ref this._lazyUsings, this._declaringSymbol.GetUsingNamespacesOrTypes(this._declarationSyntax, basesBeingResolved));
				}
				return this._lazyUsings;
			}

			// Token: 0x0600830C RID: 33548 RVA: 0x001DB669 File Offset: 0x001D9869
			protected override Imports GetImports()
			{
				return this._declaringSymbol.GetImports(this._declarationSyntax, null);
			}

			// Token: 0x04002084 RID: 8324
			private readonly SourceNamespaceSymbol _declaringSymbol;

			// Token: 0x04002085 RID: 8325
			private readonly CSharpSyntaxNode _declarationSyntax;

			// Token: 0x04002086 RID: 8326
			[global::System.Runtime.CompilerServices.Nullable(0)]
			private ImmutableArray<NamespaceOrTypeAndUsingDirective> _lazyUsings;
		}

		// Token: 0x02000680 RID: 1664
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class FromSyntaxWithPreviousSubmissionImports : WithUsingNamespacesAndTypesBinder
		{
			// Token: 0x0600830D RID: 33549 RVA: 0x001DB67D File Offset: 0x001D987D
			internal FromSyntaxWithPreviousSubmissionImports(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, Binder next, bool withImportChainEntry)
				: base(next, withImportChainEntry)
			{
				this._declaringSymbol = declaringSymbol;
				this._declarationSyntax = declarationSyntax;
			}

			// Token: 0x0600830E RID: 33550 RVA: 0x001DB696 File Offset: 0x001D9896
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			internal override ImmutableArray<NamespaceOrTypeAndUsingDirective> GetUsings([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				return this.GetImports(basesBeingResolved).Usings;
			}

			// Token: 0x0600830F RID: 33551 RVA: 0x001DB6A4 File Offset: 0x001D98A4
			private Imports GetImports([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				if (this._lazyFullImports == null)
				{
					Interlocked.CompareExchange<Imports>(ref this._lazyFullImports, this._declaringSymbol.DeclaringCompilation.GetPreviousSubmissionImports().Concat(this._declaringSymbol.GetImports(this._declarationSyntax, basesBeingResolved)), null);
				}
				return this._lazyFullImports;
			}

			// Token: 0x06008310 RID: 33552 RVA: 0x001DB6F3 File Offset: 0x001D98F3
			protected override Imports GetImports()
			{
				return this.GetImports(null);
			}

			// Token: 0x04002087 RID: 8327
			private readonly SourceNamespaceSymbol _declaringSymbol;

			// Token: 0x04002088 RID: 8328
			private readonly CSharpSyntaxNode _declarationSyntax;

			// Token: 0x04002089 RID: 8329
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private Imports _lazyFullImports;
		}

		// Token: 0x02000681 RID: 1665
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private sealed class FromNamespacesOrTypes : WithUsingNamespacesAndTypesBinder
		{
			// Token: 0x06008311 RID: 33553 RVA: 0x001DB6FC File Offset: 0x001D98FC
			internal FromNamespacesOrTypes(ImmutableArray<NamespaceOrTypeAndUsingDirective> namespacesOrTypes, [global::System.Runtime.CompilerServices.Nullable(1)] Binder next, bool withImportChainEntry)
				: base(next, withImportChainEntry)
			{
				this._usings = namespacesOrTypes;
			}

			// Token: 0x06008312 RID: 33554 RVA: 0x001DB70D File Offset: 0x001D990D
			internal override ImmutableArray<NamespaceOrTypeAndUsingDirective> GetUsings([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				return this._usings;
			}

			// Token: 0x06008313 RID: 33555 RVA: 0x001DB715 File Offset: 0x001D9915
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			protected override Imports GetImports()
			{
				return Imports.Create(ImmutableDictionary<string, AliasAndUsingDirective>.Empty, this._usings, ImmutableArray<AliasAndExternAliasDirective>.Empty);
			}

			// Token: 0x0400208A RID: 8330
			private readonly ImmutableArray<NamespaceOrTypeAndUsingDirective> _usings;
		}
	}
}
