using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200007C RID: 124
	internal class WithLambdaParametersBinder : LocalScopeBinder
	{
		// Token: 0x06000AED RID: 2797 RVA: 0x000513D4 File Offset: 0x0004F5D4
		public WithLambdaParametersBinder(LambdaSymbol lambdaSymbol, Binder enclosing)
			: base(enclosing)
		{
			this.lambdaSymbol = lambdaSymbol;
			this.parameterMap = new MultiDictionary<string, ParameterSymbol>();
			ImmutableArray<ParameterSymbol> parameters = lambdaSymbol.Parameters;
			if (!parameters.IsDefaultOrEmpty)
			{
				this.<.ctor>g__recordDefinitions|3_0(parameters);
				foreach (ParameterSymbol parameterSymbol in lambdaSymbol.Parameters)
				{
					if (!parameterSymbol.IsDiscard)
					{
						this.parameterMap.Add(parameterSymbol.Name, parameterSymbol);
					}
				}
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0005144E File Offset: 0x0004F64E
		protected override TypeSymbol GetCurrentReturnType(out RefKind refKind)
		{
			refKind = this.lambdaSymbol.RefKind;
			return this.lambdaSymbol.ReturnType;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00051468 File Offset: 0x0004F668
		internal override Symbol ContainingMemberOrLambda
		{
			get
			{
				return this.lambdaSymbol;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00051470 File Offset: 0x0004F670
		internal override bool IsNestedFunctionBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00051473 File Offset: 0x0004F673
		internal override bool IsDirectlyInIterator
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00051478 File Offset: 0x0004F678
		internal override TypeWithAnnotations GetIteratorElementType()
		{
			return TypeWithAnnotations.Create(base.CreateErrorType(""), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000514A0 File Offset: 0x0004F6A0
		protected override void ValidateYield(YieldStatementSyntax node, BindingDiagnosticBag diagnostics)
		{
			if (node != null)
			{
				diagnostics.Add(ErrorCode.ERR_YieldInAnonMeth, node.YieldKeyword.GetLocation());
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000514CC File Offset: 0x0004F6CC
		internal override void LookupSymbolsInSingleBinder(LookupResult result, string name, int arity, ConsList<TypeSymbol> basesBeingResolved, LookupOptions options, Binder originalBinder, bool diagnose, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if ((options & LookupOptions.NamespaceAliasesOnly) != LookupOptions.Default)
			{
				return;
			}
			foreach (ParameterSymbol parameterSymbol in this.parameterMap[name])
			{
				result.MergeEqual(originalBinder.CheckViability(parameterSymbol, arity, options, null, diagnose, ref useSiteInfo, null));
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00051540 File Offset: 0x0004F740
		protected override void AddLookupSymbolsInfoInSingleBinder(LookupSymbolsInfo result, LookupOptions options, Binder originalBinder)
		{
			if (options.CanConsiderMembers())
			{
				foreach (ParameterSymbol parameterSymbol in this.lambdaSymbol.Parameters)
				{
					if (originalBinder.CanAddLookupSymbolInfo(parameterSymbol, options, result, null, null))
					{
						result.AddSymbol(parameterSymbol, parameterSymbol.Name, 0);
					}
				}
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00051598 File Offset: 0x0004F798
		private static bool ReportConflictWithParameter(ParameterSymbol parameter, Symbol newSymbol, string name, Location newLocation, BindingDiagnosticBag diagnostics)
		{
			if (parameter.Locations[0] == newLocation)
			{
				return false;
			}
			SymbolKind symbolKind = ((newSymbol == null) ? SymbolKind.Parameter : newSymbol.Kind);
			if (symbolKind <= SymbolKind.Local)
			{
				if (symbolKind == SymbolKind.ErrorType)
				{
					return true;
				}
				if (symbolKind != SymbolKind.Local)
				{
					goto IL_0093;
				}
			}
			else
			{
				if (symbolKind == SymbolKind.Method)
				{
					return false;
				}
				switch (symbolKind)
				{
				case SymbolKind.Parameter:
					break;
				case SymbolKind.PointerType:
				case SymbolKind.Property:
					goto IL_0093;
				case SymbolKind.RangeVariable:
					diagnostics.Add(ErrorCode.ERR_QueryRangeVariableOverrides, newLocation, new object[] { name });
					return true;
				case SymbolKind.TypeParameter:
					return false;
				default:
					goto IL_0093;
				}
			}
			diagnostics.Add(ErrorCode.ERR_LocalIllegallyOverrides, newLocation, new object[] { name });
			return true;
			IL_0093:
			diagnostics.Add(ErrorCode.ERR_InternalError, newLocation);
			return false;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00051648 File Offset: 0x0004F848
		internal override bool EnsureSingleDefinition(Symbol symbol, string name, Location location, BindingDiagnosticBag diagnostics)
		{
			SmallDictionary<string, ParameterSymbol> definitionMap = this._definitionMap;
			ParameterSymbol parameterSymbol;
			return definitionMap != null && definitionMap.TryGetValue(name, out parameterSymbol) && WithLambdaParametersBinder.ReportConflictWithParameter(parameterSymbol, symbol, name, location, diagnostics);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00051677 File Offset: 0x0004F877
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0005167E File Offset: 0x0004F87E
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00051685 File Offset: 0x0004F885
		internal override uint LocalScopeDepth
		{
			get
			{
				return 1U;
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00051688 File Offset: 0x0004F888
		[CompilerGenerated]
		private void <.ctor>g__recordDefinitions|3_0(ImmutableArray<ParameterSymbol> definitions)
		{
			SmallDictionary<string, ParameterSymbol> smallDictionary;
			if ((smallDictionary = this._definitionMap) == null)
			{
				smallDictionary = (this._definitionMap = new SmallDictionary<string, ParameterSymbol>());
			}
			SmallDictionary<string, ParameterSymbol> smallDictionary2 = smallDictionary;
			foreach (ParameterSymbol parameterSymbol in definitions)
			{
				if (!parameterSymbol.IsDiscard && !smallDictionary2.ContainsKey(parameterSymbol.Name))
				{
					smallDictionary2.Add(parameterSymbol.Name, parameterSymbol);
				}
			}
		}

		// Token: 0x0400047C RID: 1148
		protected readonly LambdaSymbol lambdaSymbol;

		// Token: 0x0400047D RID: 1149
		protected readonly MultiDictionary<string, ParameterSymbol> parameterMap;

		// Token: 0x0400047E RID: 1150
		private SmallDictionary<string, ParameterSymbol> _definitionMap;
	}
}
