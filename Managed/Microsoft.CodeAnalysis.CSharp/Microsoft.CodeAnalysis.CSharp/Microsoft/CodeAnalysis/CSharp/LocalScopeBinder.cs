using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000032 RID: 50
	internal class LocalScopeBinder : Binder
	{
		// Token: 0x06000665 RID: 1637 RVA: 0x0003A66A File Offset: 0x0003886A
		internal LocalScopeBinder(Binder next)
			: this(next, next.Flags)
		{
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0003A67C File Offset: 0x0003887C
		internal LocalScopeBinder(Binder next, BinderFlags flags)
			: base(next, flags)
		{
			uint localScopeDepth = next.LocalScopeDepth;
			if (localScopeDepth != 1U)
			{
				this._localScopeDepth = localScopeDepth + 1U;
				return;
			}
			for (Binder binder = next; binder != null; binder = binder.Next)
			{
				if (binder is InMethodBinder || binder is WithLambdaParametersBinder)
				{
					this._localScopeDepth = 1U;
					return;
				}
				if (binder is LocalScopeBinder)
				{
					this._localScopeDepth = 2U;
					return;
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0003A6DC File Offset: 0x000388DC
		internal sealed override ImmutableArray<LocalSymbol> Locals
		{
			get
			{
				if (this._locals.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<LocalSymbol>(ref this._locals, this.BuildLocals(), default(ImmutableArray<LocalSymbol>));
				}
				return this._locals;
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0003A717 File Offset: 0x00038917
		protected virtual ImmutableArray<LocalSymbol> BuildLocals()
		{
			return ImmutableArray<LocalSymbol>.Empty;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0003A720 File Offset: 0x00038920
		internal sealed override ImmutableArray<LocalFunctionSymbol> LocalFunctions
		{
			get
			{
				if (this._localFunctions.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<LocalFunctionSymbol>(ref this._localFunctions, this.BuildLocalFunctions(), default(ImmutableArray<LocalFunctionSymbol>));
				}
				return this._localFunctions;
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0003A75B File Offset: 0x0003895B
		protected virtual ImmutableArray<LocalFunctionSymbol> BuildLocalFunctions()
		{
			return ImmutableArray<LocalFunctionSymbol>.Empty;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0003A764 File Offset: 0x00038964
		internal sealed override ImmutableArray<LabelSymbol> Labels
		{
			get
			{
				if (this._labels.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<LabelSymbol>(ref this._labels, this.BuildLabels(), default(ImmutableArray<LabelSymbol>));
				}
				return this._labels;
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0003A79F File Offset: 0x0003899F
		protected virtual ImmutableArray<LabelSymbol> BuildLabels()
		{
			return ImmutableArray<LabelSymbol>.Empty;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0003A7A8 File Offset: 0x000389A8
		private SmallDictionary<string, LocalSymbol> LocalsMap
		{
			get
			{
				if (this._lazyLocalsMap == null && this.Locals.Length > 0)
				{
					this._lazyLocalsMap = LocalScopeBinder.BuildMap<LocalSymbol>(this.Locals);
				}
				return this._lazyLocalsMap;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0003A7E8 File Offset: 0x000389E8
		private SmallDictionary<string, LocalFunctionSymbol> LocalFunctionsMap
		{
			get
			{
				if (this._lazyLocalFunctionsMap == null && this.LocalFunctions.Length > 0)
				{
					this._lazyLocalFunctionsMap = LocalScopeBinder.BuildMap<LocalFunctionSymbol>(this.LocalFunctions);
				}
				return this._lazyLocalFunctionsMap;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0003A828 File Offset: 0x00038A28
		private SmallDictionary<string, LabelSymbol> LabelsMap
		{
			get
			{
				if (this._lazyLabelsMap == null && this.Labels.Length > 0)
				{
					this._lazyLabelsMap = LocalScopeBinder.BuildMap<LabelSymbol>(this.Labels);
				}
				return this._lazyLabelsMap;
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0003A868 File Offset: 0x00038A68
		private static SmallDictionary<string, TSymbol> BuildMap<TSymbol>(ImmutableArray<TSymbol> array) where TSymbol : Symbol
		{
			SmallDictionary<string, TSymbol> smallDictionary = new SmallDictionary<string, TSymbol>();
			for (int i = array.Length - 1; i >= 0; i--)
			{
				TSymbol tsymbol = array[i];
				smallDictionary[tsymbol.Name] = tsymbol;
			}
			return smallDictionary;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0003A8AC File Offset: 0x00038AAC
		protected ImmutableArray<LocalSymbol> BuildLocals(SyntaxList<StatementSyntax> statements, Binder enclosingBinder)
		{
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			foreach (StatementSyntax statementSyntax in statements)
			{
				this.BuildLocals(enclosingBinder, statementSyntax, instance);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0003A8EC File Offset: 0x00038AEC
		internal void BuildLocals(Binder enclosingBinder, StatementSyntax statement, ArrayBuilder<LocalSymbol> locals)
		{
			StatementSyntax statementSyntax = statement;
			while (statementSyntax.Kind() == SyntaxKind.LabeledStatement)
			{
				statementSyntax = ((LabeledStatementSyntax)statementSyntax).Statement;
			}
			SyntaxKind syntaxKind = statementSyntax.Kind();
			if (syntaxKind <= SyntaxKind.GotoCaseStatement)
			{
				if (syntaxKind == SyntaxKind.LocalDeclarationStatement)
				{
					Binder binder = enclosingBinder.GetBinder(statementSyntax) ?? enclosingBinder;
					LocalDeclarationStatementSyntax localDeclarationStatementSyntax = (LocalDeclarationStatementSyntax)statementSyntax;
					TypeSyntax type = localDeclarationStatementSyntax.Declaration.Type;
					Action<ArrayRankSpecifierSyntax, ValueTuple<LocalScopeBinder, ArrayBuilder<LocalSymbol>, Binder>> action = delegate([global::System.Runtime.CompilerServices.Nullable(1)] ArrayRankSpecifierSyntax rankSpecifier, [TupleElementNames(new string[] { "localScopeBinder", "locals", "localDeclarationBinder" })] ValueTuple<LocalScopeBinder, ArrayBuilder<LocalSymbol>, Binder> args)
					{
						foreach (ExpressionSyntax expressionSyntax in rankSpecifier.Sizes)
						{
							if (expressionSyntax.Kind() != SyntaxKind.OmittedArraySizeExpression)
							{
								ExpressionVariableFinder.FindExpressionVariables(args.Item1, args.Item2, expressionSyntax, args.Item3);
							}
						}
					};
					ValueTuple<LocalScopeBinder, ArrayBuilder<LocalSymbol>, Binder> valueTuple = new ValueTuple<LocalScopeBinder, ArrayBuilder<LocalSymbol>, Binder>(this, locals, binder);
					type.VisitRankSpecifiers<ValueTuple<LocalScopeBinder, ArrayBuilder<LocalSymbol>, Binder>>(action, in valueTuple);
					LocalDeclarationKind localDeclarationKind;
					if (localDeclarationStatementSyntax.IsConst)
					{
						localDeclarationKind = LocalDeclarationKind.Constant;
					}
					else if (localDeclarationStatementSyntax.UsingKeyword != default(SyntaxToken))
					{
						localDeclarationKind = LocalDeclarationKind.UsingVariable;
					}
					else
					{
						localDeclarationKind = LocalDeclarationKind.RegularVariable;
					}
					foreach (VariableDeclaratorSyntax variableDeclaratorSyntax in localDeclarationStatementSyntax.Declaration.Variables)
					{
						SourceLocalSymbol sourceLocalSymbol = this.MakeLocal(localDeclarationStatementSyntax.Declaration, variableDeclaratorSyntax, localDeclarationKind, binder);
						locals.Add(sourceLocalSymbol);
						ExpressionVariableFinder.FindExpressionVariables(this, locals, variableDeclaratorSyntax, binder);
					}
					return;
				}
				if (syntaxKind != SyntaxKind.ExpressionStatement && syntaxKind != SyntaxKind.GotoCaseStatement)
				{
					return;
				}
			}
			else if (syntaxKind - SyntaxKind.ReturnStatement > 1 && syntaxKind != SyntaxKind.ThrowStatement)
			{
				switch (syntaxKind)
				{
				case SyntaxKind.LockStatement:
				{
					Binder binder2 = enclosingBinder.GetBinder(statementSyntax);
					ExpressionVariableFinder.FindExpressionVariables(this, locals, statementSyntax, binder2);
					break;
				}
				case SyntaxKind.IfStatement:
					goto IL_0155;
				case SyntaxKind.ElseClause:
					break;
				case SyntaxKind.SwitchStatement:
				{
					SwitchStatementSyntax switchStatementSyntax = (SwitchStatementSyntax)statementSyntax;
					ExpressionVariableFinder.FindExpressionVariables(this, locals, statementSyntax, enclosingBinder.GetBinder(switchStatementSyntax.Expression) ?? enclosingBinder);
					return;
				}
				default:
					return;
				}
				return;
			}
			IL_0155:
			ExpressionVariableFinder.FindExpressionVariables(this, locals, statementSyntax, enclosingBinder.GetBinder(statementSyntax) ?? enclosingBinder);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0003AA98 File Offset: 0x00038C98
		protected ImmutableArray<LocalFunctionSymbol> BuildLocalFunctions(SyntaxList<StatementSyntax> statements)
		{
			ArrayBuilder<LocalFunctionSymbol> arrayBuilder = null;
			foreach (StatementSyntax statementSyntax in statements)
			{
				this.BuildLocalFunctions(statementSyntax, ref arrayBuilder);
			}
			if (arrayBuilder == null)
			{
				return ImmutableArray<LocalFunctionSymbol>.Empty;
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0003AADC File Offset: 0x00038CDC
		internal void BuildLocalFunctions(StatementSyntax statement, ref ArrayBuilder<LocalFunctionSymbol> locals)
		{
			StatementSyntax statementSyntax = statement;
			while (statementSyntax.Kind() == SyntaxKind.LabeledStatement)
			{
				statementSyntax = ((LabeledStatementSyntax)statementSyntax).Statement;
			}
			if (statementSyntax.Kind() == SyntaxKind.LocalFunctionStatement)
			{
				LocalFunctionStatementSyntax localFunctionStatementSyntax = (LocalFunctionStatementSyntax)statementSyntax;
				if (locals == null)
				{
					locals = ArrayBuilder<LocalFunctionSymbol>.GetInstance();
				}
				LocalFunctionSymbol localFunctionSymbol = this.MakeLocalFunction(localFunctionStatementSyntax);
				locals.Add(localFunctionSymbol);
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0003AB35 File Offset: 0x00038D35
		protected SourceLocalSymbol MakeLocal(VariableDeclarationSyntax declaration, VariableDeclaratorSyntax declarator, LocalDeclarationKind kind, Binder initializerBinderOpt = null)
		{
			return SourceLocalSymbol.MakeLocal(this.ContainingMemberOrLambda, this, true, declaration.Type, declarator.Identifier, kind, declarator.Initializer, initializerBinderOpt);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0003AB59 File Offset: 0x00038D59
		protected LocalFunctionSymbol MakeLocalFunction(LocalFunctionStatementSyntax declaration)
		{
			return new LocalFunctionSymbol(this, this.ContainingMemberOrLambda, declaration);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0003AB68 File Offset: 0x00038D68
		protected void BuildLabels(SyntaxList<StatementSyntax> statements, ref ArrayBuilder<LabelSymbol> labels)
		{
			MethodSymbol methodSymbol = (MethodSymbol)this.ContainingMemberOrLambda;
			foreach (StatementSyntax statementSyntax in statements)
			{
				LocalScopeBinder.BuildLabels(methodSymbol, statementSyntax, ref labels);
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0003ABA4 File Offset: 0x00038DA4
		internal static void BuildLabels(MethodSymbol containingMethod, StatementSyntax statement, ref ArrayBuilder<LabelSymbol> labels)
		{
			while (statement.Kind() == SyntaxKind.LabeledStatement)
			{
				LabeledStatementSyntax labeledStatementSyntax = (LabeledStatementSyntax)statement;
				if (labels == null)
				{
					labels = ArrayBuilder<LabelSymbol>.GetInstance();
				}
				SourceLabelSymbol sourceLabelSymbol = new SourceLabelSymbol(containingMethod, labeledStatementSyntax.Identifier, null);
				labels.Add(sourceLabelSymbol);
				statement = labeledStatementSyntax.Statement;
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0003ABF8 File Offset: 0x00038DF8
		protected override SourceLocalSymbol LookupLocal(SyntaxToken nameToken)
		{
			LocalSymbol localSymbol = null;
			if (this.LocalsMap != null && this.LocalsMap.TryGetValue(nameToken.ValueText, out localSymbol))
			{
				if (localSymbol.IdentifierToken == nameToken)
				{
					return (SourceLocalSymbol)localSymbol;
				}
				foreach (LocalSymbol localSymbol2 in this.Locals)
				{
					if (localSymbol2.IdentifierToken == nameToken)
					{
						return (SourceLocalSymbol)localSymbol2;
					}
				}
			}
			return base.LookupLocal(nameToken);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0003AC78 File Offset: 0x00038E78
		protected override LocalFunctionSymbol LookupLocalFunction(SyntaxToken nameToken)
		{
			LocalFunctionSymbol localFunctionSymbol = null;
			if (this.LocalFunctionsMap != null && this.LocalFunctionsMap.TryGetValue(nameToken.ValueText, out localFunctionSymbol))
			{
				if (localFunctionSymbol.NameToken == nameToken)
				{
					return localFunctionSymbol;
				}
				foreach (LocalFunctionSymbol localFunctionSymbol2 in this.LocalFunctions)
				{
					if (localFunctionSymbol2.NameToken == nameToken)
					{
						return localFunctionSymbol2;
					}
				}
			}
			return base.LookupLocalFunction(nameToken);
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0003ACEE File Offset: 0x00038EEE
		internal override uint LocalScopeDepth
		{
			get
			{
				return this._localScopeDepth;
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0003ACF8 File Offset: 0x00038EF8
		internal override void LookupSymbolsInSingleBinder(LookupResult result, string name, int arity, ConsList<TypeSymbol> basesBeingResolved, LookupOptions options, Binder originalBinder, bool diagnose, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if ((options & LookupOptions.LabelsOnly) != LookupOptions.Default)
			{
				SmallDictionary<string, LabelSymbol> labelsMap = this.LabelsMap;
				LabelSymbol labelSymbol;
				if (labelsMap != null && labelsMap.TryGetValue(name, out labelSymbol))
				{
					result.MergeEqual(LookupResult.Good(labelSymbol));
				}
				return;
			}
			SmallDictionary<string, LocalSymbol> localsMap = this.LocalsMap;
			LocalSymbol localSymbol;
			if (localsMap != null && (options & LookupOptions.NamespaceAliasesOnly) == LookupOptions.Default && localsMap.TryGetValue(name, out localSymbol))
			{
				result.MergeEqual(originalBinder.CheckViability(localSymbol, arity, options, null, diagnose, ref useSiteInfo, basesBeingResolved));
			}
			SmallDictionary<string, LocalFunctionSymbol> localFunctionsMap = this.LocalFunctionsMap;
			LocalFunctionSymbol localFunctionSymbol;
			if (localFunctionsMap != null && options.CanConsiderLocals() && localFunctionsMap.TryGetValue(name, out localFunctionSymbol))
			{
				result.MergeEqual(originalBinder.CheckViability(localFunctionSymbol, arity, options, null, diagnose, ref useSiteInfo, basesBeingResolved));
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0003AD9C File Offset: 0x00038F9C
		protected override void AddLookupSymbolsInfoInSingleBinder(LookupSymbolsInfo result, LookupOptions options, Binder originalBinder)
		{
			if ((options & LookupOptions.LabelsOnly) != LookupOptions.Default && this.LabelsMap != null)
			{
				foreach (KeyValuePair<string, LabelSymbol> keyValuePair in this.LabelsMap)
				{
					result.AddSymbol(keyValuePair.Value, keyValuePair.Key, 0);
				}
			}
			if (options.CanConsiderLocals())
			{
				if (this.LocalsMap != null)
				{
					foreach (KeyValuePair<string, LocalSymbol> keyValuePair2 in this.LocalsMap)
					{
						if (originalBinder.CanAddLookupSymbolInfo(keyValuePair2.Value, options, result, null, null))
						{
							result.AddSymbol(keyValuePair2.Value, keyValuePair2.Key, 0);
						}
					}
				}
				if (this.LocalFunctionsMap != null)
				{
					foreach (KeyValuePair<string, LocalFunctionSymbol> keyValuePair3 in this.LocalFunctionsMap)
					{
						if (originalBinder.CanAddLookupSymbolInfo(keyValuePair3.Value, options, result, null, null))
						{
							result.AddSymbol(keyValuePair3.Value, keyValuePair3.Key, 0);
						}
					}
				}
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0003AE9C File Offset: 0x0003909C
		private bool ReportConflictWithLocal(Symbol local, Symbol newSymbol, string name, Location newLocation, BindingDiagnosticBag diagnostics)
		{
			SymbolKind symbolKind = ((newSymbol == null) ? SymbolKind.Parameter : newSymbol.Kind);
			if (symbolKind == SymbolKind.ErrorType)
			{
				return true;
			}
			if ((false | (symbolKind == SymbolKind.Local && this.Locals.Contains((LocalSymbol)newSymbol)) | (symbolKind == SymbolKind.Method && this.LocalFunctions.Contains((LocalFunctionSymbol)newSymbol))) && newLocation.SourceSpan.Start >= local.Locations[0].SourceSpan.Start)
			{
				diagnostics.Add(ErrorCode.ERR_LocalDuplicate, newLocation, new object[] { name });
				return true;
			}
			if (symbolKind - SymbolKind.Local > 1)
			{
				switch (symbolKind)
				{
				case SymbolKind.Parameter:
				case SymbolKind.TypeParameter:
					goto IL_00BE;
				case SymbolKind.RangeVariable:
					diagnostics.Add(ErrorCode.ERR_QueryRangeVariableOverrides, newLocation, new object[] { name });
					return true;
				}
				diagnostics.Add(ErrorCode.ERR_InternalError, newLocation);
				return false;
			}
			IL_00BE:
			diagnostics.Add(ErrorCode.ERR_LocalIllegallyOverrides, newLocation, new object[] { name });
			return true;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0003AFB0 File Offset: 0x000391B0
		internal virtual bool EnsureSingleDefinition(Symbol symbol, string name, Location location, BindingDiagnosticBag diagnostics)
		{
			LocalSymbol localSymbol = null;
			LocalFunctionSymbol localFunctionSymbol = null;
			SmallDictionary<string, LocalSymbol> localsMap = this.LocalsMap;
			SmallDictionary<string, LocalFunctionSymbol> localFunctionsMap = this.LocalFunctionsMap;
			if ((localsMap != null && localsMap.TryGetValue(name, out localSymbol)) || (localFunctionsMap != null && localFunctionsMap.TryGetValue(name, out localFunctionSymbol)))
			{
				Symbol symbol2 = localSymbol ?? localFunctionSymbol;
				return !(symbol == symbol2) && this.ReportConflictWithLocal(symbol2, symbol, name, location, diagnostics);
			}
			return false;
		}

		// Token: 0x0400008E RID: 142
		private ImmutableArray<LocalSymbol> _locals;

		// Token: 0x0400008F RID: 143
		private ImmutableArray<LocalFunctionSymbol> _localFunctions;

		// Token: 0x04000090 RID: 144
		private ImmutableArray<LabelSymbol> _labels;

		// Token: 0x04000091 RID: 145
		private readonly uint _localScopeDepth;

		// Token: 0x04000092 RID: 146
		private SmallDictionary<string, LocalSymbol> _lazyLocalsMap;

		// Token: 0x04000093 RID: 147
		private SmallDictionary<string, LocalFunctionSymbol> _lazyLocalFunctionsMap;

		// Token: 0x04000094 RID: 148
		private SmallDictionary<string, LabelSymbol> _lazyLabelsMap;
	}
}
