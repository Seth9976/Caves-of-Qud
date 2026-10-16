using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200002E RID: 46
	internal sealed class InMethodBinder : LocalScopeBinder
	{
		// Token: 0x0600060B RID: 1547 RVA: 0x00039019 File Offset: 0x00037219
		public InMethodBinder(MethodSymbol owner, Binder enclosing)
			: base(enclosing, enclosing.Flags & ~(BinderFlags.InLockBody | BinderFlags.InCatchBlock | BinderFlags.InFinallyBlock | BinderFlags.InTryBlockOfTryCatch | BinderFlags.InCatchFilter | BinderFlags.InNestedFinallyBlock))
		{
			this._methodSymbol = owner;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00039038 File Offset: 0x00037238
		private static void RecordDefinition<T>(SmallDictionary<string, Symbol> declarationMap, ImmutableArray<T> definitions) where T : Symbol
		{
			foreach (T t in definitions)
			{
				Symbol symbol = t;
				if (!declarationMap.ContainsKey(symbol.Name))
				{
					declarationMap.Add(symbol.Name, symbol);
				}
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00039080 File Offset: 0x00037280
		protected override SourceLocalSymbol LookupLocal(SyntaxToken nameToken)
		{
			return null;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00039083 File Offset: 0x00037283
		protected override LocalFunctionSymbol LookupLocalFunction(SyntaxToken nameToken)
		{
			return null;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00039086 File Offset: 0x00037286
		internal override uint LocalScopeDepth
		{
			get
			{
				return 1U;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00039089 File Offset: 0x00037289
		protected override bool InExecutableBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0003908C File Offset: 0x0003728C
		internal override Symbol ContainingMemberOrLambda
		{
			get
			{
				return this._methodSymbol;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00039094 File Offset: 0x00037294
		internal override bool IsInMethodBody
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00039097 File Offset: 0x00037297
		internal override bool IsNestedFunctionBinder
		{
			get
			{
				return this._methodSymbol.MethodKind == MethodKind.LocalFunction;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x000390A8 File Offset: 0x000372A8
		internal override bool IsDirectlyInIterator
		{
			get
			{
				return this._methodSymbol.IsIterator;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x000390B5 File Offset: 0x000372B5
		internal override bool IsIndirectlyInIterator
		{
			get
			{
				return this.IsDirectlyInIterator;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x000390BD File Offset: 0x000372BD
		internal override GeneratedLabelSymbol BreakLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x000390C0 File Offset: 0x000372C0
		internal override GeneratedLabelSymbol ContinueLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000390C3 File Offset: 0x000372C3
		protected override void ValidateYield(YieldStatementSyntax node, BindingDiagnosticBag diagnostics)
		{
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000390C8 File Offset: 0x000372C8
		internal override TypeWithAnnotations GetIteratorElementType()
		{
			RefKind refKind = this._methodSymbol.RefKind;
			TypeSymbol returnType = this._methodSymbol.ReturnType;
			if (this.IsDirectlyInIterator)
			{
				if (this._iteratorElementType == null)
				{
					TypeWithAnnotations typeWithAnnotations = InMethodBinder.GetIteratorElementTypeFromReturnType(base.Compilation, refKind, returnType, null, null);
					if (typeWithAnnotations.IsDefault)
					{
						typeWithAnnotations = TypeWithAnnotations.Create(base.CreateErrorType(""), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
					}
					Interlocked.CompareExchange<TypeWithAnnotations.Boxed>(ref this._iteratorElementType, new TypeWithAnnotations.Boxed(typeWithAnnotations), null);
				}
				return this._iteratorElementType.Value;
			}
			TypeWithAnnotations iteratorElementTypeFromReturnType = InMethodBinder.GetIteratorElementTypeFromReturnType(base.Compilation, refKind, returnType, null, null);
			if (iteratorElementTypeFromReturnType.IsDefault)
			{
				return TypeWithAnnotations.Create(base.CreateErrorType(""), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
			return iteratorElementTypeFromReturnType;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00039188 File Offset: 0x00037388
		internal static TypeWithAnnotations GetIteratorElementTypeFromReturnType(CSharpCompilation compilation, RefKind refKind, TypeSymbol returnType, Location errorLocation, BindingDiagnosticBag diagnostics)
		{
			if (refKind == RefKind.None && returnType.Kind == SymbolKind.NamedType)
			{
				TypeSymbol originalDefinition = returnType.OriginalDefinition;
				switch (originalDefinition.SpecialType)
				{
				case SpecialType.System_Collections_IEnumerable:
				case SpecialType.System_Collections_IEnumerator:
				{
					NamedTypeSymbol specialType = compilation.GetSpecialType(SpecialType.System_Object);
					if (diagnostics != null)
					{
						Binder.ReportUseSite(specialType, diagnostics, errorLocation);
					}
					return TypeWithAnnotations.Create(specialType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}
				case SpecialType.System_Collections_Generic_IEnumerable_T:
				case SpecialType.System_Collections_Generic_IEnumerator_T:
					return ((NamedTypeSymbol)returnType).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0];
				}
				if (TypeSymbol.Equals(originalDefinition, compilation.GetWellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerable_T), TypeCompareKind.ConsiderEverything) || TypeSymbol.Equals(originalDefinition, compilation.GetWellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerator_T), TypeCompareKind.ConsiderEverything))
				{
					return ((NamedTypeSymbol)returnType).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0];
				}
			}
			return default(TypeWithAnnotations);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00039260 File Offset: 0x00037460
		internal static bool IsAsyncStreamInterface(CSharpCompilation compilation, RefKind refKind, TypeSymbol returnType)
		{
			if (refKind == RefKind.None && returnType.Kind == SymbolKind.NamedType)
			{
				TypeSymbol originalDefinition = returnType.OriginalDefinition;
				if (TypeSymbol.Equals(originalDefinition, compilation.GetWellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerable_T), TypeCompareKind.ConsiderEverything) || TypeSymbol.Equals(originalDefinition, compilation.GetWellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerator_T), TypeCompareKind.ConsiderEverything))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000392AC File Offset: 0x000374AC
		internal override void LookupSymbolsInSingleBinder(LookupResult result, string name, int arity, ConsList<TypeSymbol> basesBeingResolved, LookupOptions options, Binder originalBinder, bool diagnose, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (this._methodSymbol.ParameterCount == 0 || (options & LookupOptions.NamespaceAliasesOnly) != LookupOptions.Default)
			{
				return;
			}
			MultiDictionary<string, ParameterSymbol> multiDictionary = this._lazyParameterMap;
			if (multiDictionary == null)
			{
				ImmutableArray<ParameterSymbol> parameters = this._methodSymbol.Parameters;
				multiDictionary = new MultiDictionary<string, ParameterSymbol>(parameters.Length, EqualityComparer<string>.Default, null);
				foreach (ParameterSymbol parameterSymbol in parameters)
				{
					multiDictionary.Add(parameterSymbol.Name, parameterSymbol);
				}
				this._lazyParameterMap = multiDictionary;
			}
			foreach (ParameterSymbol parameterSymbol2 in multiDictionary[name])
			{
				result.MergeEqual(originalBinder.CheckViability(parameterSymbol2, arity, options, null, diagnose, ref useSiteInfo, null));
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00039384 File Offset: 0x00037584
		protected override void AddLookupSymbolsInfoInSingleBinder(LookupSymbolsInfo result, LookupOptions options, Binder originalBinder)
		{
			if (options.CanConsiderMembers())
			{
				foreach (ParameterSymbol parameterSymbol in this._methodSymbol.Parameters)
				{
					if (originalBinder.CanAddLookupSymbolInfo(parameterSymbol, options, result, null, null))
					{
						result.AddSymbol(parameterSymbol, parameterSymbol.Name, 0);
					}
				}
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000393DC File Offset: 0x000375DC
		private static bool ReportConflictWithParameter(Symbol parameter, Symbol newSymbol, string name, Location newLocation, BindingDiagnosticBag diagnostics)
		{
			SymbolKind kind = parameter.Kind;
			SymbolKind symbolKind = ((newSymbol == null) ? SymbolKind.Parameter : newSymbol.Kind);
			if (symbolKind == SymbolKind.ErrorType)
			{
				return true;
			}
			if (kind == SymbolKind.Parameter)
			{
				if (symbolKind != SymbolKind.Local)
				{
					if (symbolKind != SymbolKind.Method)
					{
						switch (symbolKind)
						{
						case SymbolKind.Parameter:
							break;
						case SymbolKind.PointerType:
						case SymbolKind.Property:
							goto IL_008F;
						case SymbolKind.RangeVariable:
							diagnostics.Add(ErrorCode.ERR_QueryRangeVariableOverrides, newLocation, new object[] { name });
							return true;
						case SymbolKind.TypeParameter:
							return false;
						default:
							goto IL_008F;
						}
					}
					else if (((MethodSymbol)newSymbol).MethodKind != MethodKind.LocalFunction)
					{
						goto IL_008F;
					}
				}
				diagnostics.Add(ErrorCode.ERR_LocalIllegallyOverrides, newLocation, new object[] { name });
				return true;
			}
			IL_008F:
			if (kind == SymbolKind.TypeParameter)
			{
				if (symbolKind != SymbolKind.Local)
				{
					if (symbolKind != SymbolKind.Method)
					{
						switch (symbolKind)
						{
						case SymbolKind.Parameter:
							break;
						case SymbolKind.PointerType:
						case SymbolKind.Property:
							goto IL_0103;
						case SymbolKind.RangeVariable:
							diagnostics.Add(ErrorCode.ERR_QueryRangeVariableSameAsTypeParam, newLocation, new object[] { name });
							return true;
						case SymbolKind.TypeParameter:
							return false;
						default:
							goto IL_0103;
						}
					}
					else if (((MethodSymbol)newSymbol).MethodKind != MethodKind.LocalFunction)
					{
						goto IL_0103;
					}
				}
				diagnostics.Add(ErrorCode.ERR_LocalSameNameAsTypeParam, newLocation, new object[] { name });
				return true;
			}
			IL_0103:
			diagnostics.Add(ErrorCode.ERR_InternalError, newLocation);
			return true;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000394FC File Offset: 0x000376FC
		internal override bool EnsureSingleDefinition(Symbol symbol, string name, Location location, BindingDiagnosticBag diagnostics)
		{
			ImmutableArray<ParameterSymbol> parameters = this._methodSymbol.Parameters;
			ImmutableArray<TypeParameterSymbol> typeParameters = this._methodSymbol.TypeParameters;
			if (parameters.IsEmpty && typeParameters.IsEmpty)
			{
				return false;
			}
			SmallDictionary<string, Symbol> smallDictionary = this._lazyDefinitionMap;
			if (smallDictionary == null)
			{
				smallDictionary = new SmallDictionary<string, Symbol>();
				InMethodBinder.RecordDefinition<ParameterSymbol>(smallDictionary, parameters);
				InMethodBinder.RecordDefinition<TypeParameterSymbol>(smallDictionary, typeParameters);
				this._lazyDefinitionMap = smallDictionary;
			}
			Symbol symbol2;
			return smallDictionary.TryGetValue(name, out symbol2) && InMethodBinder.ReportConflictWithParameter(symbol2, symbol, name, location, diagnostics);
		}

		// Token: 0x04000082 RID: 130
		private MultiDictionary<string, ParameterSymbol> _lazyParameterMap;

		// Token: 0x04000083 RID: 131
		private readonly MethodSymbol _methodSymbol;

		// Token: 0x04000084 RID: 132
		private SmallDictionary<string, Symbol> _lazyDefinitionMap;

		// Token: 0x04000085 RID: 133
		private TypeWithAnnotations.Boxed _iteratorElementType;
	}
}
