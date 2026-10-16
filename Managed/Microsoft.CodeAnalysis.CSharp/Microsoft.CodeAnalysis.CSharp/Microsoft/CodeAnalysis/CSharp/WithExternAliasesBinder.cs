using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200007A RID: 122
	internal abstract class WithExternAliasesBinder : Binder
	{
		// Token: 0x06000AD9 RID: 2777 RVA: 0x000512B8 File Offset: 0x0004F4B8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal WithExternAliasesBinder(Binder next)
			: base(next, null)
		{
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000ADA RID: 2778
		internal abstract override ImmutableArray<AliasAndExternAliasDirective> ExternAliases { get; }

		// Token: 0x06000ADB RID: 2779 RVA: 0x000512C4 File Offset: 0x0004F4C4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void LookupSymbolsInSingleBinder(LookupResult result, string name, int arity, ConsList<TypeSymbol> basesBeingResolved, LookupOptions options, Binder originalBinder, bool diagnose, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			base.LookupSymbolInAliases(ImmutableDictionary<string, AliasAndUsingDirective>.Empty, this.ExternAliases, originalBinder, result, name, arity, basesBeingResolved, options, diagnose, ref useSiteInfo);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000512EF File Offset: 0x0004F4EF
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected override void AddLookupSymbolsInfoInSingleBinder(LookupSymbolsInfo result, LookupOptions options, Binder originalBinder)
		{
			if ((options & LookupOptions.LabelsOnly) == LookupOptions.Default)
			{
				base.AddLookupSymbolsInfoInAliases(ImmutableDictionary<string, AliasAndUsingDirective>.Empty, this.ExternAliases, result, options, originalBinder);
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0005130E File Offset: 0x0004F50E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected sealed override SourceLocalSymbol LookupLocal(SyntaxToken nameToken)
		{
			return null;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00051311 File Offset: 0x0004F511
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected sealed override LocalFunctionSymbol LookupLocalFunction(SyntaxToken nameToken)
		{
			return null;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00051314 File Offset: 0x0004F514
		internal sealed override uint LocalScopeDepth
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00051317 File Offset: 0x0004F517
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static WithExternAliasesBinder Create(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, Binder next)
		{
			return new WithExternAliasesBinder.FromSyntax(declaringSymbol, declarationSyntax, next);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00051321 File Offset: 0x0004F521
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static WithExternAliasesBinder Create([global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<AliasAndExternAliasDirective> externAliases, Binder next)
		{
			return new WithExternAliasesBinder.FromSymbols(externAliases, next);
		}

		// Token: 0x0200067B RID: 1659
		private sealed class FromSyntax : WithExternAliasesBinder
		{
			// Token: 0x060082FB RID: 33531 RVA: 0x001DB31F File Offset: 0x001D951F
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			internal FromSyntax(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, Binder next)
				: base(next)
			{
				this._declaringSymbol = declaringSymbol;
				this._declarationSyntax = declarationSyntax;
			}

			// Token: 0x17002687 RID: 9863
			// (get) Token: 0x060082FC RID: 33532 RVA: 0x001DB336 File Offset: 0x001D9536
			internal override ImmutableArray<AliasAndExternAliasDirective> ExternAliases
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

			// Token: 0x04002078 RID: 8312
			[global::System.Runtime.CompilerServices.Nullable(1)]
			private readonly SourceNamespaceSymbol _declaringSymbol;

			// Token: 0x04002079 RID: 8313
			[global::System.Runtime.CompilerServices.Nullable(1)]
			private readonly CSharpSyntaxNode _declarationSyntax;

			// Token: 0x0400207A RID: 8314
			private ImmutableArray<AliasAndExternAliasDirective> _lazyExternAliases;
		}

		// Token: 0x0200067C RID: 1660
		private sealed class FromSymbols : WithExternAliasesBinder
		{
			// Token: 0x060082FD RID: 33533 RVA: 0x001DB368 File Offset: 0x001D9568
			internal FromSymbols(ImmutableArray<AliasAndExternAliasDirective> externAliases, [global::System.Runtime.CompilerServices.Nullable(1)] Binder next)
				: base(next)
			{
				this._externAliases = externAliases;
			}

			// Token: 0x17002688 RID: 9864
			// (get) Token: 0x060082FE RID: 33534 RVA: 0x001DB378 File Offset: 0x001D9578
			internal override ImmutableArray<AliasAndExternAliasDirective> ExternAliases
			{
				get
				{
					return this._externAliases;
				}
			}

			// Token: 0x0400207B RID: 8315
			private readonly ImmutableArray<AliasAndExternAliasDirective> _externAliases;
		}
	}
}
