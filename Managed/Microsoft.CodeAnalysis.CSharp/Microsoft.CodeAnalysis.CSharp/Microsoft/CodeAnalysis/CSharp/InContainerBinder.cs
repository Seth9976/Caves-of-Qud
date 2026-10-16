using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200002D RID: 45
	internal class InContainerBinder : Binder
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x00038E5F File Offset: 0x0003705F
		internal InContainerBinder(NamespaceOrTypeSymbol container, Binder next)
			: base(next, null)
		{
			this._container = container;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00038E70 File Offset: 0x00037070
		internal NamespaceOrTypeSymbol Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00038E78 File Offset: 0x00037078
		internal override Symbol ContainingMemberOrLambda
		{
			get
			{
				MergedNamespaceSymbol mergedNamespaceSymbol = this._container as MergedNamespaceSymbol;
				if (mergedNamespaceSymbol == null)
				{
					return this._container;
				}
				return mergedNamespaceSymbol.GetConstituentForCompilation(base.Compilation);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00038EA7 File Offset: 0x000370A7
		private bool IsScriptClass
		{
			get
			{
				return this._container.Kind == SymbolKind.NamedType && ((NamedTypeSymbol)this._container).IsScriptClass;
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00038ECC File Offset: 0x000370CC
		internal override bool IsAccessibleHelper(Symbol symbol, TypeSymbol accessThroughType, out bool failedThroughTypeCheck, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConsList<TypeSymbol> basesBeingResolved)
		{
			NamedTypeSymbol namedTypeSymbol = this._container as NamedTypeSymbol;
			if (namedTypeSymbol != null)
			{
				return base.IsSymbolAccessibleConditional(symbol, namedTypeSymbol, accessThroughType, out failedThroughTypeCheck, ref useSiteInfo, null);
			}
			return base.Next.IsAccessibleHelper(symbol, accessThroughType, out failedThroughTypeCheck, ref useSiteInfo, basesBeingResolved);
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00038F08 File Offset: 0x00037108
		internal override bool SupportsExtensionMethods
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00038F0B File Offset: 0x0003710B
		internal override void GetCandidateExtensionMethods(ArrayBuilder<MethodSymbol> methods, string name, int arity, LookupOptions options, Binder originalBinder)
		{
			if (this._container.Kind == SymbolKind.Namespace)
			{
				((NamespaceSymbol)this._container).GetExtensionMethods(methods, name, arity, options);
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00038F34 File Offset: 0x00037134
		internal override TypeWithAnnotations GetIteratorElementType()
		{
			if (this.IsScriptClass)
			{
				return TypeWithAnnotations.Create(base.Compilation.GetSpecialType(SpecialType.System_Object), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
			return base.Next.GetIteratorElementType();
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00038F70 File Offset: 0x00037170
		internal override void LookupSymbolsInSingleBinder(LookupResult result, string name, int arity, ConsList<TypeSymbol> basesBeingResolved, LookupOptions options, Binder originalBinder, bool diagnose, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if ((options & LookupOptions.NamespaceAliasesOnly) == LookupOptions.Default)
			{
				base.LookupMembersInternal(result, this._container, name, arity, basesBeingResolved, options, originalBinder, diagnose, ref useSiteInfo);
				if (result.IsMultiViable)
				{
					if (arity == 0)
					{
						WithExternAndUsingAliasesBinder withExternAndUsingAliasesBinder = base.Next as WithExternAndUsingAliasesBinder;
						if (withExternAndUsingAliasesBinder != null && withExternAndUsingAliasesBinder.IsUsingAlias(name, originalBinder.IsSemanticModelBinder, basesBeingResolved))
						{
							CSDiagnosticInfo csdiagnosticInfo = new CSDiagnosticInfo(ErrorCode.ERR_ConflictAliasAndMember, new object[] { name, this._container });
							ExtendedErrorTypeSymbol extendedErrorTypeSymbol = new ExtendedErrorTypeSymbol(null, name, arity, csdiagnosticInfo, true, false);
							result.SetFrom(LookupResult.Good(extendedErrorTypeSymbol));
						}
					}
					return;
				}
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00038FFF File Offset: 0x000371FF
		protected override void AddLookupSymbolsInfoInSingleBinder(LookupSymbolsInfo result, LookupOptions options, Binder originalBinder)
		{
			base.AddMemberLookupSymbolsInfo(result, this._container, options, originalBinder);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00039010 File Offset: 0x00037210
		protected override SourceLocalSymbol LookupLocal(SyntaxToken nameToken)
		{
			return null;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00039013 File Offset: 0x00037213
		protected override LocalFunctionSymbol LookupLocalFunction(SyntaxToken nameToken)
		{
			return null;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00039016 File Offset: 0x00037216
		internal override uint LocalScopeDepth
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x04000081 RID: 129
		private readonly NamespaceOrTypeSymbol _container;
	}
}
