using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002D8 RID: 728
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class OmittedArraySizeExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x0600410F RID: 16655 RVA: 0x0010EE1E File Offset: 0x0010D01E
		internal OmittedArraySizeExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06004110 RID: 16656 RVA: 0x0010EE29 File Offset: 0x0010D029
		public SyntaxToken OmittedArraySizeExpressionToken
		{
			get
			{
				return new SyntaxToken(this, ((OmittedArraySizeExpressionSyntax)base.Green).omittedArraySizeExpressionToken, base.Position, 0);
			}
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x0010EE48 File Offset: 0x0010D048
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x0010EE4B File Offset: 0x0010D04B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x0010EE4E File Offset: 0x0010D04E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitOmittedArraySizeExpression(this);
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x0010EE57 File Offset: 0x0010D057
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitOmittedArraySizeExpression(this);
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x0010EE60 File Offset: 0x0010D060
		public OmittedArraySizeExpressionSyntax Update(SyntaxToken omittedArraySizeExpressionToken)
		{
			if (!(omittedArraySizeExpressionToken != this.OmittedArraySizeExpressionToken))
			{
				return this;
			}
			OmittedArraySizeExpressionSyntax omittedArraySizeExpressionSyntax = SyntaxFactory.OmittedArraySizeExpression(omittedArraySizeExpressionToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return omittedArraySizeExpressionSyntax;
			}
			return omittedArraySizeExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x0010EE9B File Offset: 0x0010D09B
		public OmittedArraySizeExpressionSyntax WithOmittedArraySizeExpressionToken(SyntaxToken omittedArraySizeExpressionToken)
		{
			return this.Update(omittedArraySizeExpressionToken);
		}
	}
}
