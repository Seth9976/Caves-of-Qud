using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002DA RID: 730
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class IsPatternExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06004124 RID: 16676 RVA: 0x0010EFFA File Offset: 0x0010D1FA
		internal IsPatternExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06004125 RID: 16677 RVA: 0x0010F005 File Offset: 0x0010D205
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
			}
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06004126 RID: 16678 RVA: 0x0010F013 File Offset: 0x0010D213
		public SyntaxToken IsKeyword
		{
			get
			{
				return new SyntaxToken(this, ((IsPatternExpressionSyntax)base.Green).isKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x06004127 RID: 16679 RVA: 0x0010F039 File Offset: 0x0010D239
		public PatternSyntax Pattern
		{
			get
			{
				return base.GetRed<PatternSyntax>(ref this.pattern, 2);
			}
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x0010F048 File Offset: 0x0010D248
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<PatternSyntax>(ref this.pattern, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
			}
			return syntaxNode;
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x0010F080 File Offset: 0x0010D280
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.pattern;
				}
			}
			else
			{
				syntaxNode = this.expression;
			}
			return syntaxNode;
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x0010F0AB File Offset: 0x0010D2AB
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitIsPatternExpression(this);
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x0010F0B4 File Offset: 0x0010D2B4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitIsPatternExpression(this);
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x0010F0C0 File Offset: 0x0010D2C0
		public IsPatternExpressionSyntax Update(ExpressionSyntax expression, SyntaxToken isKeyword, PatternSyntax pattern)
		{
			if (expression == this.Expression && !(isKeyword != this.IsKeyword) && pattern == this.Pattern)
			{
				return this;
			}
			IsPatternExpressionSyntax isPatternExpressionSyntax = SyntaxFactory.IsPatternExpression(expression, isKeyword, pattern);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return isPatternExpressionSyntax;
			}
			return isPatternExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x0010F10F File Offset: 0x0010D30F
		public IsPatternExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(expression, this.IsKeyword, this.Pattern);
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x0010F124 File Offset: 0x0010D324
		public IsPatternExpressionSyntax WithIsKeyword(SyntaxToken isKeyword)
		{
			return this.Update(this.Expression, isKeyword, this.Pattern);
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x0010F139 File Offset: 0x0010D339
		public IsPatternExpressionSyntax WithPattern(PatternSyntax pattern)
		{
			return this.Update(this.Expression, this.IsKeyword, pattern);
		}

		// Token: 0x040014A5 RID: 5285
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;

		// Token: 0x040014A6 RID: 5286
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private PatternSyntax pattern;
	}
}
