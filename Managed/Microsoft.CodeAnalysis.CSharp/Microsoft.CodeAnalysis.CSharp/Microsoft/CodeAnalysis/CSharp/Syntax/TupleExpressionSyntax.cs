using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200029F RID: 671
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class TupleExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003E66 RID: 15974 RVA: 0x0010A37F File Offset: 0x0010857F
		internal TupleExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x06003E67 RID: 15975 RVA: 0x0010A38A File Offset: 0x0010858A
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((TupleExpressionSyntax)base.Green).openParenToken, base.Position, 0);
			}
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x06003E68 RID: 15976 RVA: 0x0010A3AC File Offset: 0x001085AC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<ArgumentSyntax> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.arguments, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<ArgumentSyntax>);
				}
				return new SeparatedSyntaxList<ArgumentSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x06003E69 RID: 15977 RVA: 0x0010A3E1 File Offset: 0x001085E1
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((TupleExpressionSyntax)base.Green).closeParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x0010A407 File Offset: 0x00108607
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.arguments, 1);
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x0010A41C File Offset: 0x0010861C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.arguments;
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x0010A42A File Offset: 0x0010862A
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTupleExpression(this);
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x0010A433 File Offset: 0x00108633
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTupleExpression(this);
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x0010A43C File Offset: 0x0010863C
		public TupleExpressionSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			if (!(openParenToken != this.OpenParenToken) && !(arguments != this.Arguments) && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			TupleExpressionSyntax tupleExpressionSyntax = SyntaxFactory.TupleExpression(openParenToken, arguments, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return tupleExpressionSyntax;
			}
			return tupleExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x0010A495 File Offset: 0x00108695
		public TupleExpressionSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(openParenToken, this.Arguments, this.CloseParenToken);
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x0010A4AA File Offset: 0x001086AA
		public TupleExpressionSyntax WithArguments([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments)
		{
			return this.Update(this.OpenParenToken, arguments, this.CloseParenToken);
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x0010A4BF File Offset: 0x001086BF
		public TupleExpressionSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.OpenParenToken, this.Arguments, closeParenToken);
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x0010A4D4 File Offset: 0x001086D4
		public TupleExpressionSyntax AddArguments(params ArgumentSyntax[] items)
		{
			return this.WithArguments(this.Arguments.AddRange(items));
		}

		// Token: 0x04001459 RID: 5209
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode arguments;
	}
}
