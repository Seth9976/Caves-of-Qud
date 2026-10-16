using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000025 RID: 37
	internal sealed class FixedStatementBinder : LocalScopeBinder
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x00036347 File Offset: 0x00034547
		public FixedStatementBinder(Binder enclosing, FixedStatementSyntax syntax)
			: base(enclosing)
		{
			this._syntax = syntax;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00036358 File Offset: 0x00034558
		protected override ImmutableArray<LocalSymbol> BuildLocals()
		{
			if (this._syntax.Declaration != null)
			{
				ArrayBuilder<LocalSymbol> arrayBuilder = new ArrayBuilder<LocalSymbol>(this._syntax.Declaration.Variables.Count);
				TypeSyntax type = this._syntax.Declaration.Type;
				Action<ArrayRankSpecifierSyntax, ValueTuple<FixedStatementBinder, ArrayBuilder<LocalSymbol>>> action = delegate([global::System.Runtime.CompilerServices.Nullable(1)] ArrayRankSpecifierSyntax rankSpecifier, [TupleElementNames(new string[] { "binder", "locals" })] ValueTuple<FixedStatementBinder, ArrayBuilder<LocalSymbol>> args)
				{
					foreach (ExpressionSyntax expressionSyntax in rankSpecifier.Sizes)
					{
						if (expressionSyntax.Kind() != SyntaxKind.OmittedArraySizeExpression)
						{
							ExpressionVariableFinder.FindExpressionVariables(args.Item1, args.Item2, expressionSyntax, null);
						}
					}
				};
				ValueTuple<FixedStatementBinder, ArrayBuilder<LocalSymbol>> valueTuple = new ValueTuple<FixedStatementBinder, ArrayBuilder<LocalSymbol>>(this, arrayBuilder);
				type.VisitRankSpecifiers<ValueTuple<FixedStatementBinder, ArrayBuilder<LocalSymbol>>>(action, in valueTuple);
				foreach (VariableDeclaratorSyntax variableDeclaratorSyntax in this._syntax.Declaration.Variables)
				{
					arrayBuilder.Add(base.MakeLocal(this._syntax.Declaration, variableDeclaratorSyntax, LocalDeclarationKind.FixedVariable, null));
					ExpressionVariableFinder.FindExpressionVariables(this, arrayBuilder, variableDeclaratorSyntax, null);
				}
				return arrayBuilder.ToImmutable();
			}
			return ImmutableArray<LocalSymbol>.Empty;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0003642F File Offset: 0x0003462F
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			if (this._syntax == scopeDesignator)
			{
				return this.Locals;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00036446 File Offset: 0x00034646
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0003644D File Offset: 0x0003464D
		internal override SyntaxNode ScopeDesignator
		{
			get
			{
				return this._syntax;
			}
		}

		// Token: 0x04000064 RID: 100
		private readonly FixedStatementSyntax _syntax;
	}
}
