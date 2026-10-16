using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002AF RID: 687
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class LiteralExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003F0A RID: 16138 RVA: 0x0010B3EC File Offset: 0x001095EC
		internal LiteralExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06003F0B RID: 16139 RVA: 0x0010B3F7 File Offset: 0x001095F7
		public SyntaxToken Token
		{
			get
			{
				return new SyntaxToken(this, ((LiteralExpressionSyntax)base.Green).token, base.Position, 0);
			}
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x0010B416 File Offset: 0x00109616
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x0010B419 File Offset: 0x00109619
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x0010B41C File Offset: 0x0010961C
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLiteralExpression(this);
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x0010B425 File Offset: 0x00109625
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLiteralExpression(this);
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x0010B430 File Offset: 0x00109630
		public LiteralExpressionSyntax Update(SyntaxToken token)
		{
			if (!(token != this.Token))
			{
				return this;
			}
			LiteralExpressionSyntax literalExpressionSyntax = SyntaxFactory.LiteralExpression(base.Kind(), token);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return literalExpressionSyntax;
			}
			return literalExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x0010B471 File Offset: 0x00109671
		public LiteralExpressionSyntax WithToken(SyntaxToken token)
		{
			return this.Update(token);
		}
	}
}
