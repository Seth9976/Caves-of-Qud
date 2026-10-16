using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000033 RID: 51
	internal sealed class LockBinder : LockOrUsingBinder
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x0003B00E File Offset: 0x0003920E
		public LockBinder(Binder enclosing, LockStatementSyntax syntax)
			: base(enclosing)
		{
			this._syntax = syntax;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0003B01E File Offset: 0x0003921E
		protected override ExpressionSyntax TargetExpressionSyntax
		{
			get
			{
				return this._syntax.Expression;
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0003B02C File Offset: 0x0003922C
		internal override BoundStatement BindLockStatementParts(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			ExpressionSyntax targetExpressionSyntax = this.TargetExpressionSyntax;
			BoundExpression boundExpression = base.BindTargetExpression(diagnostics, originalBinder, null);
			TypeSymbol type = boundExpression.Type;
			bool flag = false;
			if (type == null)
			{
				if (boundExpression.ConstantValue != ConstantValue.Null || base.Compilation.FeatureStrictEnabled)
				{
					Binder.Error(diagnostics, ErrorCode.ERR_LockNeedsReference, targetExpressionSyntax, new object[] { boundExpression.Display });
					flag = true;
				}
			}
			else if (!type.IsReferenceType && (type.IsValueType || base.Compilation.FeatureStrictEnabled))
			{
				Binder.Error(diagnostics, ErrorCode.ERR_LockNeedsReference, targetExpressionSyntax, new object[] { type });
				flag = true;
			}
			BoundStatement boundStatement = originalBinder.BindPossibleEmbeddedStatement(this._syntax.Statement, diagnostics);
			return new BoundLockStatement(this._syntax, boundExpression, boundStatement, flag);
		}

		// Token: 0x04000095 RID: 149
		private readonly LockStatementSyntax _syntax;
	}
}
