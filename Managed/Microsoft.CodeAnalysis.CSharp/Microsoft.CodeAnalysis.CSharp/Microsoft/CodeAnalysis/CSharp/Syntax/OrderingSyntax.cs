using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002D4 RID: 724
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class OrderingSyntax : CSharpSyntaxNode
	{
		// Token: 0x060040E0 RID: 16608 RVA: 0x0010E942 File Offset: 0x0010CB42
		internal OrderingSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x060040E1 RID: 16609 RVA: 0x0010E94D File Offset: 0x0010CB4D
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x060040E2 RID: 16610 RVA: 0x0010E95C File Offset: 0x0010CB5C
		public SyntaxToken AscendingOrDescendingKeyword
		{
			get
			{
				SyntaxToken ascendingOrDescendingKeyword = ((OrderingSyntax)base.Green).ascendingOrDescendingKeyword;
				if (ascendingOrDescendingKeyword == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, ascendingOrDescendingKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x0010E99C File Offset: 0x0010CB9C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x0010E9AF File Offset: 0x0010CBAF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.expression;
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x0010E9BC File Offset: 0x0010CBBC
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitOrdering(this);
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x0010E9C5 File Offset: 0x0010CBC5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitOrdering(this);
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x0010E9D0 File Offset: 0x0010CBD0
		public OrderingSyntax Update(ExpressionSyntax expression, SyntaxToken ascendingOrDescendingKeyword)
		{
			if (expression == this.Expression && !(ascendingOrDescendingKeyword != this.AscendingOrDescendingKeyword))
			{
				return this;
			}
			OrderingSyntax orderingSyntax = SyntaxFactory.Ordering(base.Kind(), expression, ascendingOrDescendingKeyword);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return orderingSyntax;
			}
			return orderingSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x0010EA1B File Offset: 0x0010CC1B
		public OrderingSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(expression, this.AscendingOrDescendingKeyword);
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x0010EA2A File Offset: 0x0010CC2A
		public OrderingSyntax WithAscendingOrDescendingKeyword(SyntaxToken ascendingOrDescendingKeyword)
		{
			return this.Update(this.Expression, ascendingOrDescendingKeyword);
		}

		// Token: 0x0400149F RID: 5279
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
