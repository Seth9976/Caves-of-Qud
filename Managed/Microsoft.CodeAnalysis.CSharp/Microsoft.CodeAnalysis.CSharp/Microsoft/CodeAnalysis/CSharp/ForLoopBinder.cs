using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000028 RID: 40
	internal sealed class ForLoopBinder : LoopBinder
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x00038468 File Offset: 0x00036668
		public ForLoopBinder(Binder enclosing, ForStatementSyntax syntax)
			: base(enclosing)
		{
			this._syntax = syntax;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00038478 File Offset: 0x00036678
		protected override ImmutableArray<LocalSymbol> BuildLocals()
		{
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			if (this._syntax.Declaration != null)
			{
				TypeSyntax type = this._syntax.Declaration.Type;
				Action<ArrayRankSpecifierSyntax, ValueTuple<ForLoopBinder, ArrayBuilder<LocalSymbol>>> action = delegate([global::System.Runtime.CompilerServices.Nullable(1)] ArrayRankSpecifierSyntax rankSpecifier, [TupleElementNames(new string[] { "binder", "locals" })] ValueTuple<ForLoopBinder, ArrayBuilder<LocalSymbol>> args)
				{
					foreach (ExpressionSyntax expressionSyntax in rankSpecifier.Sizes)
					{
						if (expressionSyntax.Kind() != SyntaxKind.OmittedArraySizeExpression)
						{
							ExpressionVariableFinder.FindExpressionVariables(args.Item1, args.Item2, expressionSyntax, null);
						}
					}
				};
				ValueTuple<ForLoopBinder, ArrayBuilder<LocalSymbol>> valueTuple = new ValueTuple<ForLoopBinder, ArrayBuilder<LocalSymbol>>(this, instance);
				type.VisitRankSpecifiers<ValueTuple<ForLoopBinder, ArrayBuilder<LocalSymbol>>>(action, in valueTuple);
				foreach (VariableDeclaratorSyntax variableDeclaratorSyntax in this._syntax.Declaration.Variables)
				{
					SourceLocalSymbol sourceLocalSymbol = base.MakeLocal(this._syntax.Declaration, variableDeclaratorSyntax, LocalDeclarationKind.RegularVariable, null);
					instance.Add(sourceLocalSymbol);
					ExpressionVariableFinder.FindExpressionVariables(this, instance, variableDeclaratorSyntax, null);
				}
			}
			else
			{
				ExpressionVariableFinder.FindExpressionVariables(this, instance, this._syntax.Initializers);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00038549 File Offset: 0x00036749
		internal override BoundForStatement BindForParts(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			return this.BindForParts(this._syntax, originalBinder, diagnostics);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0003855C File Offset: 0x0003675C
		private BoundForStatement BindForParts(ForStatementSyntax node, Binder originalBinder, BindingDiagnosticBag diagnostics)
		{
			BoundStatement boundStatement;
			if (this._syntax.Declaration != null)
			{
				ImmutableArray<BoundLocalDeclaration> immutableArray;
				boundStatement = originalBinder.BindForOrUsingOrFixedDeclarations(node.Declaration, LocalDeclarationKind.RegularVariable, diagnostics, out immutableArray);
			}
			else
			{
				boundStatement = originalBinder.BindStatementExpressionList(node.Initializers, diagnostics);
			}
			BoundExpression boundExpression = null;
			ImmutableArray<LocalSymbol> immutableArray2 = ImmutableArray<LocalSymbol>.Empty;
			ExpressionSyntax condition = node.Condition;
			if (condition != null)
			{
				originalBinder = originalBinder.GetBinder(condition);
				boundExpression = originalBinder.BindBooleanExpression(condition, diagnostics);
				immutableArray2 = originalBinder.GetDeclaredLocalsForScope(condition);
			}
			BoundStatement boundStatement2 = null;
			SeparatedSyntaxList<ExpressionSyntax> incrementors = node.Incrementors;
			if (incrementors.Count > 0)
			{
				ExpressionSyntax expressionSyntax = incrementors.First();
				Binder binder = originalBinder.GetBinder(expressionSyntax);
				boundStatement2 = binder.BindStatementExpressionList(incrementors, diagnostics);
				ImmutableArray<LocalSymbol> declaredLocalsForScope = binder.GetDeclaredLocalsForScope(expressionSyntax);
				if (!declaredLocalsForScope.IsEmpty)
				{
					if (boundStatement2.Kind == BoundKind.StatementList)
					{
						boundStatement2 = new BoundBlock(expressionSyntax, declaredLocalsForScope, ((BoundStatementList)boundStatement2).Statements, false)
						{
							WasCompilerGenerated = true
						};
					}
					else
					{
						boundStatement2 = new BoundBlock(boundStatement2.Syntax, declaredLocalsForScope, ImmutableArray.Create<BoundStatement>(boundStatement2), false)
						{
							WasCompilerGenerated = true
						};
					}
				}
			}
			BoundStatement boundStatement3 = originalBinder.BindPossibleEmbeddedStatement(node.Statement, diagnostics);
			return new BoundForStatement(node, this.Locals, boundStatement, immutableArray2, boundExpression, boundStatement2, boundStatement3, this.BreakLabel, this.ContinueLabel, false);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00038680 File Offset: 0x00036880
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			if (this._syntax == scopeDesignator)
			{
				return this.Locals;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00038697 File Offset: 0x00036897
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0003869E File Offset: 0x0003689E
		internal override SyntaxNode ScopeDesignator
		{
			get
			{
				return this._syntax;
			}
		}

		// Token: 0x04000078 RID: 120
		private readonly ForStatementSyntax _syntax;
	}
}
