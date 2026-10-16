using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000034 RID: 52
	internal abstract class LockOrUsingBinder : LocalScopeBinder
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x0003B0EC File Offset: 0x000392EC
		internal LockOrUsingBinder(Binder enclosing)
			: base(enclosing)
		{
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000684 RID: 1668
		protected abstract ExpressionSyntax TargetExpressionSyntax { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0003B0F8 File Offset: 0x000392F8
		internal sealed override ImmutableHashSet<Symbol> LockedOrDisposedVariables
		{
			get
			{
				if (this._lazyLockedOrDisposedVariables == null)
				{
					ImmutableHashSet<Symbol> immutableHashSet = base.Next.LockedOrDisposedVariables;
					ExpressionSyntax targetExpressionSyntax = this.TargetExpressionSyntax;
					if (targetExpressionSyntax != null && targetExpressionSyntax.Kind() == SyntaxKind.IdentifierName)
					{
						BoundExpression boundExpression = this.BindTargetExpression(null, this.GetBinder(targetExpressionSyntax.Parent), null);
						BoundKind kind = boundExpression.Kind;
						if (kind != BoundKind.Local)
						{
							if (kind == BoundKind.Parameter)
							{
								immutableHashSet = immutableHashSet.Add(((BoundParameter)boundExpression).ParameterSymbol);
							}
						}
						else
						{
							immutableHashSet = immutableHashSet.Add(((BoundLocal)boundExpression).LocalSymbol);
						}
					}
					Interlocked.CompareExchange<ImmutableHashSet<Symbol>>(ref this._lazyLockedOrDisposedVariables, immutableHashSet, null);
				}
				return this._lazyLockedOrDisposedVariables;
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0003B194 File Offset: 0x00039394
		protected BoundExpression BindTargetExpression(BindingDiagnosticBag diagnostics, Binder originalBinder, TypeSymbol targetTypeOpt = null)
		{
			if (this._lazyExpressionAndDiagnostics == null)
			{
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
				BoundExpression boundExpression = originalBinder.BindValue(this.TargetExpressionSyntax, instance, Binder.BindValueKind.RValueOrMethodGroup);
				if (targetTypeOpt != null)
				{
					boundExpression = originalBinder.GenerateConversionForAssignment(targetTypeOpt, boundExpression, instance, false, false);
				}
				else
				{
					boundExpression = originalBinder.BindToNaturalType(boundExpression, instance, true);
				}
				Interlocked.CompareExchange<LockOrUsingBinder.ExpressionAndDiagnostics>(ref this._lazyExpressionAndDiagnostics, new LockOrUsingBinder.ExpressionAndDiagnostics(boundExpression, instance.ToReadOnlyAndFree()), null);
			}
			if (diagnostics != null)
			{
				diagnostics.AddRange(this._lazyExpressionAndDiagnostics.Diagnostics, true);
			}
			return this._lazyExpressionAndDiagnostics.Expression;
		}

		// Token: 0x04000096 RID: 150
		private ImmutableHashSet<Symbol> _lazyLockedOrDisposedVariables;

		// Token: 0x04000097 RID: 151
		private LockOrUsingBinder.ExpressionAndDiagnostics _lazyExpressionAndDiagnostics;

		// Token: 0x02000644 RID: 1604
		private class ExpressionAndDiagnostics
		{
			// Token: 0x0600826F RID: 33391 RVA: 0x001DA424 File Offset: 0x001D8624
			public ExpressionAndDiagnostics(BoundExpression expression, ImmutableBindingDiagnostic<AssemblySymbol> diagnostics)
			{
				this.Expression = expression;
				this.Diagnostics = diagnostics;
			}

			// Token: 0x04001F8F RID: 8079
			public readonly BoundExpression Expression;

			// Token: 0x04001F90 RID: 8080
			public readonly ImmutableBindingDiagnostic<AssemblySymbol> Diagnostics;
		}
	}
}
