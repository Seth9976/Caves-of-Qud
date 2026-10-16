using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002E5 RID: 741
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ConstantPatternSyntax : PatternSyntax
	{
		// Token: 0x06004195 RID: 16789 RVA: 0x0010FB96 File Offset: 0x0010DD96
		internal ConstantPatternSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x0010FBA1 File Offset: 0x0010DDA1
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
			}
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x0010FBAF File Offset: 0x0010DDAF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x0010FBC2 File Offset: 0x0010DDC2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.expression;
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x0010FBCF File Offset: 0x0010DDCF
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConstantPattern(this);
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x0010FBD8 File Offset: 0x0010DDD8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConstantPattern(this);
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x0010FBE4 File Offset: 0x0010DDE4
		public ConstantPatternSyntax Update(ExpressionSyntax expression)
		{
			if (expression == this.Expression)
			{
				return this;
			}
			ConstantPatternSyntax constantPatternSyntax = SyntaxFactory.ConstantPattern(expression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return constantPatternSyntax;
			}
			return constantPatternSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x0010FC1A File Offset: 0x0010DE1A
		public ConstantPatternSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(expression);
		}

		// Token: 0x040014B4 RID: 5300
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
