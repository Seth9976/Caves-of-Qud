using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200007B RID: 123
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class WithExternAndUsingAliasesBinder : WithExternAliasesBinder
	{
		// Token: 0x06000AE2 RID: 2786 RVA: 0x0005132A File Offset: 0x0004F52A
		protected WithExternAndUsingAliasesBinder(WithUsingNamespacesAndTypesBinder next)
			: base(next)
		{
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000AE3 RID: 2787
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal abstract override ImmutableArray<AliasAndUsingDirective> UsingAliases
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x06000AE4 RID: 2788
		protected abstract ImmutableDictionary<string, AliasAndUsingDirective> GetUsingAliasesMap([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved);

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00051334 File Offset: 0x0004F534
		internal override void LookupSymbolsInSingleBinder(LookupResult result, string name, int arity, ConsList<TypeSymbol> basesBeingResolved, LookupOptions options, Binder originalBinder, bool diagnose, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			base.LookupSymbolInAliases(this.GetUsingAliasesMap(basesBeingResolved), this.ExternAliases, originalBinder, result, name, arity, basesBeingResolved, options, diagnose, ref useSiteInfo);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00051362 File Offset: 0x0004F562
		protected override void AddLookupSymbolsInfoInSingleBinder(LookupSymbolsInfo result, LookupOptions options, Binder originalBinder)
		{
			if ((options & LookupOptions.LabelsOnly) == LookupOptions.Default)
			{
				base.AddLookupSymbolsInfoInAliases(this.GetUsingAliasesMap(null), this.ExternAliases, result, options, originalBinder);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00051383 File Offset: 0x0004F583
		internal override ImportChain ImportChain
		{
			get
			{
				if (this._lazyImportChain == null)
				{
					Interlocked.CompareExchange<ImportChain>(ref this._lazyImportChain, this.BuildImportChain(), null);
				}
				return this._lazyImportChain;
			}
		}

		// Token: 0x06000AE8 RID: 2792
		protected abstract ImportChain BuildImportChain();

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000513A6 File Offset: 0x0004F5A6
		internal bool IsUsingAlias(string name, bool callerIsSemanticModel, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
		{
			return base.IsUsingAlias(this.GetUsingAliasesMap(basesBeingResolved), name, callerIsSemanticModel);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000513B7 File Offset: 0x0004F5B7
		[Obsolete("Use other overloads", true)]
		internal new static WithExternAndUsingAliasesBinder Create(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, Binder next)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000513BE File Offset: 0x0004F5BE
		internal static WithExternAndUsingAliasesBinder Create(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, WithUsingNamespacesAndTypesBinder next)
		{
			return new WithExternAndUsingAliasesBinder.FromSyntax(declaringSymbol, declarationSyntax, next);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000513C8 File Offset: 0x0004F5C8
		internal static WithExternAndUsingAliasesBinder Create([global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<AliasAndExternAliasDirective> externAliases, ImmutableDictionary<string, AliasAndUsingDirective> usingAliases, WithUsingNamespacesAndTypesBinder next)
		{
			return new WithExternAndUsingAliasesBinder.FromSymbols(externAliases, usingAliases, next);
		}

		// Token: 0x0400047B RID: 1147
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ImportChain _lazyImportChain;

		// Token: 0x0200067D RID: 1661
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private sealed class FromSyntax : WithExternAndUsingAliasesBinder
		{
			// Token: 0x060082FF RID: 33535 RVA: 0x001DB380 File Offset: 0x001D9580
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			internal FromSyntax(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, WithUsingNamespacesAndTypesBinder next)
				: base(next)
			{
				this._declaringSymbol = declaringSymbol;
				this._declarationSyntax = declarationSyntax;
			}

			// Token: 0x17002689 RID: 9865
			// (get) Token: 0x06008300 RID: 33536 RVA: 0x001DB397 File Offset: 0x001D9597
			internal sealed override ImmutableArray<AliasAndExternAliasDirective> ExternAliases
			{
				get
				{
					if (this._lazyExternAliases.IsDefault)
					{
						ImmutableInterlocked.InterlockedInitialize<AliasAndExternAliasDirective>(ref this._lazyExternAliases, this._declaringSymbol.GetExternAliases(this._declarationSyntax));
					}
					return this._lazyExternAliases;
				}
			}

			// Token: 0x1700268A RID: 9866
			// (get) Token: 0x06008301 RID: 33537 RVA: 0x001DB3C9 File Offset: 0x001D95C9
			internal override ImmutableArray<AliasAndUsingDirective> UsingAliases
			{
				get
				{
					if (this._lazyUsingAliases.IsDefault)
					{
						ImmutableInterlocked.InterlockedInitialize<AliasAndUsingDirective>(ref this._lazyUsingAliases, this._declaringSymbol.GetUsingAliases(this._declarationSyntax, null));
					}
					return this._lazyUsingAliases;
				}
			}

			// Token: 0x06008302 RID: 33538 RVA: 0x001DB3FC File Offset: 0x001D95FC
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			protected override ImmutableDictionary<string, AliasAndUsingDirective> GetUsingAliasesMap([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				if (this._lazyUsingAliasesMap == null)
				{
					Interlocked.CompareExchange<ImmutableDictionary<string, AliasAndUsingDirective>>(ref this._lazyUsingAliasesMap, this._declaringSymbol.GetUsingAliasesMap(this._declarationSyntax, basesBeingResolved), null);
				}
				return this._lazyUsingAliasesMap;
			}

			// Token: 0x1700268B RID: 9867
			// (get) Token: 0x06008303 RID: 33539 RVA: 0x001DB42C File Offset: 0x001D962C
			[global::System.Runtime.CompilerServices.Nullable(1)]
			internal override QuickAttributeChecker QuickAttributeChecker
			{
				[global::System.Runtime.CompilerServices.NullableContext(1)]
				get
				{
					if (this._lazyQuickAttributeChecker == null)
					{
						QuickAttributeChecker quickAttributeChecker = base.Next.QuickAttributeChecker;
						CSharpSyntaxNode declarationSyntax = this._declarationSyntax;
						CompilationUnitSyntax compilationUnitSyntax = declarationSyntax as CompilationUnitSyntax;
						SyntaxList<UsingDirectiveSyntax> syntaxList;
						if (compilationUnitSyntax == null)
						{
							NamespaceDeclarationSyntax namespaceDeclarationSyntax = declarationSyntax as NamespaceDeclarationSyntax;
							if (namespaceDeclarationSyntax == null)
							{
								throw ExceptionUtilities.UnexpectedValue(this._declarationSyntax);
							}
							syntaxList = namespaceDeclarationSyntax.Usings;
						}
						else
						{
							foreach (SingleNamespaceDeclaration singleNamespaceDeclaration in ((SourceNamespaceSymbol)base.Compilation.SourceModule.GlobalNamespace).MergedDeclaration.Declarations)
							{
								if (singleNamespaceDeclaration.HasGlobalUsings && compilationUnitSyntax.SyntaxTree != singleNamespaceDeclaration.SyntaxReference.SyntaxTree)
								{
									quickAttributeChecker = quickAttributeChecker.AddAliasesIfAny(((CompilationUnitSyntax)singleNamespaceDeclaration.SyntaxReference.GetSyntax(default(CancellationToken))).Usings, true);
								}
							}
							syntaxList = compilationUnitSyntax.Usings;
						}
						quickAttributeChecker = quickAttributeChecker.AddAliasesIfAny(syntaxList, false);
						this._lazyQuickAttributeChecker = quickAttributeChecker;
					}
					return this._lazyQuickAttributeChecker;
				}
			}

			// Token: 0x06008304 RID: 33540 RVA: 0x001DB530 File Offset: 0x001D9730
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			protected override ImportChain BuildImportChain()
			{
				ImportChain importChain = base.Next.ImportChain;
				NamespaceDeclarationSyntax namespaceDeclarationSyntax = this._declarationSyntax as NamespaceDeclarationSyntax;
				if (namespaceDeclarationSyntax != null)
				{
					NameSyntax nameSyntax = namespaceDeclarationSyntax.Name;
					for (;;)
					{
						QualifiedNameSyntax qualifiedNameSyntax = nameSyntax as QualifiedNameSyntax;
						if (qualifiedNameSyntax == null)
						{
							break;
						}
						importChain = new ImportChain(Imports.Empty, importChain);
						nameSyntax = qualifiedNameSyntax.Left;
					}
				}
				return new ImportChain(this._declaringSymbol.GetImports(this._declarationSyntax, null), importChain);
			}

			// Token: 0x0400207C RID: 8316
			[global::System.Runtime.CompilerServices.Nullable(1)]
			private readonly SourceNamespaceSymbol _declaringSymbol;

			// Token: 0x0400207D RID: 8317
			[global::System.Runtime.CompilerServices.Nullable(1)]
			private readonly CSharpSyntaxNode _declarationSyntax;

			// Token: 0x0400207E RID: 8318
			private ImmutableArray<AliasAndExternAliasDirective> _lazyExternAliases;

			// Token: 0x0400207F RID: 8319
			private ImmutableArray<AliasAndUsingDirective> _lazyUsingAliases;

			// Token: 0x04002080 RID: 8320
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
			private ImmutableDictionary<string, AliasAndUsingDirective> _lazyUsingAliasesMap;

			// Token: 0x04002081 RID: 8321
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private QuickAttributeChecker _lazyQuickAttributeChecker;
		}

		// Token: 0x0200067E RID: 1662
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private sealed class FromSymbols : WithExternAndUsingAliasesBinder
		{
			// Token: 0x06008305 RID: 33541 RVA: 0x001DB596 File Offset: 0x001D9796
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			internal FromSymbols([global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<AliasAndExternAliasDirective> externAliases, ImmutableDictionary<string, AliasAndUsingDirective> usingAliases, WithUsingNamespacesAndTypesBinder next)
				: base(next)
			{
				this._externAliases = externAliases;
				this._usingAliases = usingAliases;
			}

			// Token: 0x1700268C RID: 9868
			// (get) Token: 0x06008306 RID: 33542 RVA: 0x001DB5AD File Offset: 0x001D97AD
			internal override ImmutableArray<AliasAndExternAliasDirective> ExternAliases
			{
				get
				{
					return this._externAliases;
				}
			}

			// Token: 0x1700268D RID: 9869
			// (get) Token: 0x06008307 RID: 33543 RVA: 0x001DB5B5 File Offset: 0x001D97B5
			internal override ImmutableArray<AliasAndUsingDirective> UsingAliases
			{
				get
				{
					return this._usingAliases.SelectAsArray<KeyValuePair<string, AliasAndUsingDirective>, AliasAndUsingDirective>((KeyValuePair<string, AliasAndUsingDirective> pair) => pair.Value);
				}
			}

			// Token: 0x06008308 RID: 33544 RVA: 0x001DB5E1 File Offset: 0x001D97E1
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			protected override ImmutableDictionary<string, AliasAndUsingDirective> GetUsingAliasesMap([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				return this._usingAliases;
			}

			// Token: 0x06008309 RID: 33545 RVA: 0x001DB5E9 File Offset: 0x001D97E9
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			protected override ImportChain BuildImportChain()
			{
				return new ImportChain(Imports.Create(this._usingAliases, ((WithUsingNamespacesAndTypesBinder)base.Next).GetUsings(null), this._externAliases), base.Next.ImportChain);
			}

			// Token: 0x04002082 RID: 8322
			private readonly ImmutableArray<AliasAndExternAliasDirective> _externAliases;

			// Token: 0x04002083 RID: 8323
			[global::System.Runtime.CompilerServices.Nullable(1)]
			private readonly ImmutableDictionary<string, AliasAndUsingDirective> _usingAliases;
		}
	}
}
