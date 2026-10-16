using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000015 RID: 21
	internal sealed class CatchClauseBinder : LocalScopeBinder
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x000332EE File Offset: 0x000314EE
		public CatchClauseBinder(Binder enclosing, CatchClauseSyntax syntax)
			: base(enclosing, (enclosing.Flags | BinderFlags.InCatchBlock) & ~BinderFlags.InNestedFinallyBlock)
		{
			this._syntax = syntax;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00033310 File Offset: 0x00031510
		protected override ImmutableArray<LocalSymbol> BuildLocals()
		{
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			CatchDeclarationSyntax declaration = this._syntax.Declaration;
			if (declaration != null && declaration.Identifier.Kind() != SyntaxKind.None)
			{
				instance.Add(SourceLocalSymbol.MakeLocal(this.ContainingMemberOrLambda, this, false, declaration.Type, declaration.Identifier, LocalDeclarationKind.CatchVariable, null, null));
			}
			if (this._syntax.Filter != null)
			{
				ExpressionVariableFinder.FindExpressionVariables(this, instance, this._syntax.Filter.FilterExpression, null);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0003338C File Offset: 0x0003158C
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			if (this._syntax == scopeDesignator)
			{
				return this.Locals;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000333A3 File Offset: 0x000315A3
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x000333AA File Offset: 0x000315AA
		internal override SyntaxNode ScopeDesignator
		{
			get
			{
				return this._syntax;
			}
		}

		// Token: 0x04000045 RID: 69
		private readonly CatchClauseSyntax _syntax;
	}
}
