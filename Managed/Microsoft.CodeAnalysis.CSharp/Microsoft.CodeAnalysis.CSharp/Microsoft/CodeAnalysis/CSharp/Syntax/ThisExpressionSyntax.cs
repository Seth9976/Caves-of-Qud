using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002AD RID: 685
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ThisExpressionSyntax : InstanceExpressionSyntax
	{
		// Token: 0x06003EFA RID: 16122 RVA: 0x0010B2DC File Offset: 0x001094DC
		internal ThisExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x06003EFB RID: 16123 RVA: 0x0010B2E7 File Offset: 0x001094E7
		public SyntaxToken Token
		{
			get
			{
				return new SyntaxToken(this, ((ThisExpressionSyntax)base.Green).token, base.Position, 0);
			}
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x0010B306 File Offset: 0x00109506
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x0010B309 File Offset: 0x00109509
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x0010B30C File Offset: 0x0010950C
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitThisExpression(this);
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x0010B315 File Offset: 0x00109515
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitThisExpression(this);
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x0010B320 File Offset: 0x00109520
		public ThisExpressionSyntax Update(SyntaxToken token)
		{
			if (!(token != this.Token))
			{
				return this;
			}
			ThisExpressionSyntax thisExpressionSyntax = SyntaxFactory.ThisExpression(token);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return thisExpressionSyntax;
			}
			return thisExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x0010B35B File Offset: 0x0010955B
		public ThisExpressionSyntax WithToken(SyntaxToken token)
		{
			return this.Update(token);
		}
	}
}
