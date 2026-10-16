using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002AE RID: 686
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class BaseExpressionSyntax : InstanceExpressionSyntax
	{
		// Token: 0x06003F02 RID: 16130 RVA: 0x0010B364 File Offset: 0x00109564
		internal BaseExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x06003F03 RID: 16131 RVA: 0x0010B36F File Offset: 0x0010956F
		public SyntaxToken Token
		{
			get
			{
				return new SyntaxToken(this, ((BaseExpressionSyntax)base.Green).token, base.Position, 0);
			}
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x0010B38E File Offset: 0x0010958E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x0010B391 File Offset: 0x00109591
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x0010B394 File Offset: 0x00109594
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBaseExpression(this);
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x0010B39D File Offset: 0x0010959D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBaseExpression(this);
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x0010B3A8 File Offset: 0x001095A8
		public BaseExpressionSyntax Update(SyntaxToken token)
		{
			if (!(token != this.Token))
			{
				return this;
			}
			BaseExpressionSyntax baseExpressionSyntax = SyntaxFactory.BaseExpression(token);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return baseExpressionSyntax;
			}
			return baseExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x0010B3E3 File Offset: 0x001095E3
		public BaseExpressionSyntax WithToken(SyntaxToken token)
		{
			return this.Update(token);
		}
	}
}
