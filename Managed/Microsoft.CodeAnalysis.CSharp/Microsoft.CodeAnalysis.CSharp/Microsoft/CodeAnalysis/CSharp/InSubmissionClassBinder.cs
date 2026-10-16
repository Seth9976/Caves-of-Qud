using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200002F RID: 47
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class InSubmissionClassBinder : InContainerBinder
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x00039572 File Offset: 0x00037772
		internal InSubmissionClassBinder(NamedTypeSymbol submissionClass, Binder next, CompilationUnitSyntax declarationSyntax, bool inUsings)
			: base(submissionClass, next)
		{
			this._declarationSyntax = declarationSyntax;
			this._inUsings = inUsings;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0003958C File Offset: 0x0003778C
		internal override void GetCandidateExtensionMethods(ArrayBuilder<MethodSymbol> methods, string name, int arity, LookupOptions options, Binder originalBinder)
		{
			for (CSharpCompilation csharpCompilation = base.Compilation; csharpCompilation != null; csharpCompilation = csharpCompilation.PreviousSubmission)
			{
				NamedTypeSymbol scriptClass = csharpCompilation.ScriptClass;
				if (scriptClass != null)
				{
					scriptClass.GetExtensionMethods(methods, name, arity, options);
				}
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000395C4 File Offset: 0x000377C4
		internal override void LookupSymbolsInSingleBinder(LookupResult result, string name, int arity, ConsList<TypeSymbol> basesBeingResolved, LookupOptions options, Binder originalBinder, bool diagnose, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			base.LookupMembersInSubmissions(result, (NamedTypeSymbol)base.Container, this._declarationSyntax, this._inUsings, name, arity, basesBeingResolved, options, originalBinder, diagnose, ref useSiteInfo);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000395FB File Offset: 0x000377FB
		protected override void AddLookupSymbolsInfoInSingleBinder(LookupSymbolsInfo result, LookupOptions options, Binder originalBinder)
		{
			base.AddMemberLookupSymbolsInfoInSubmissions(result, (NamedTypeSymbol)base.Container, this._inUsings, options, originalBinder);
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00039617 File Offset: 0x00037817
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal override ImmutableArray<AliasAndExternAliasDirective> ExternAliases
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return ((SourceNamespaceSymbol)base.Compilation.SourceModule.GlobalNamespace).GetExternAliases(this._declarationSyntax);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00039639 File Offset: 0x00037839
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal override ImmutableArray<AliasAndUsingDirective> UsingAliases
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return ((SourceNamespaceSymbol)base.Compilation.SourceModule.GlobalNamespace).GetUsingAliases(this._declarationSyntax, null);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x0003965C File Offset: 0x0003785C
		internal override QuickAttributeChecker QuickAttributeChecker
		{
			get
			{
				if (this._lazyQuickAttributeChecker == null)
				{
					QuickAttributeChecker quickAttributeChecker = base.Next.QuickAttributeChecker;
					quickAttributeChecker = quickAttributeChecker.AddAliasesIfAny(this._declarationSyntax.Usings, false);
					this._lazyQuickAttributeChecker = quickAttributeChecker;
				}
				return this._lazyQuickAttributeChecker;
			}
		}

		// Token: 0x04000086 RID: 134
		private readonly CompilationUnitSyntax _declarationSyntax;

		// Token: 0x04000087 RID: 135
		private readonly bool _inUsings;

		// Token: 0x04000088 RID: 136
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private QuickAttributeChecker _lazyQuickAttributeChecker;
	}
}
