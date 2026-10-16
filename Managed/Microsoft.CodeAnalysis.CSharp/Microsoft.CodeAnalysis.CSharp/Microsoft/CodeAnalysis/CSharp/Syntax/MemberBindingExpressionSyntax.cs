using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002A5 RID: 677
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class MemberBindingExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003EA9 RID: 16041 RVA: 0x0010AA2A File Offset: 0x00108C2A
		internal MemberBindingExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x06003EAA RID: 16042 RVA: 0x0010AA35 File Offset: 0x00108C35
		public SyntaxToken OperatorToken
		{
			get
			{
				return new SyntaxToken(this, ((MemberBindingExpressionSyntax)base.Green).operatorToken, base.Position, 0);
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x06003EAB RID: 16043 RVA: 0x0010AA54 File Offset: 0x00108C54
		public SimpleNameSyntax Name
		{
			get
			{
				return base.GetRed<SimpleNameSyntax>(ref this.name, 1);
			}
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x0010AA63 File Offset: 0x00108C63
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<SimpleNameSyntax>(ref this.name, 1);
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x0010AA78 File Offset: 0x00108C78
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.name;
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x0010AA86 File Offset: 0x00108C86
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitMemberBindingExpression(this);
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x0010AA8F File Offset: 0x00108C8F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitMemberBindingExpression(this);
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x0010AA98 File Offset: 0x00108C98
		public MemberBindingExpressionSyntax Update(SyntaxToken operatorToken, SimpleNameSyntax name)
		{
			if (!(operatorToken != this.OperatorToken) && name == this.Name)
			{
				return this;
			}
			MemberBindingExpressionSyntax memberBindingExpressionSyntax = SyntaxFactory.MemberBindingExpression(operatorToken, name);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return memberBindingExpressionSyntax;
			}
			return memberBindingExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x0010AADD File Offset: 0x00108CDD
		public MemberBindingExpressionSyntax WithOperatorToken(SyntaxToken operatorToken)
		{
			return this.Update(operatorToken, this.Name);
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x0010AAEC File Offset: 0x00108CEC
		public MemberBindingExpressionSyntax WithName(SimpleNameSyntax name)
		{
			return this.Update(this.OperatorToken, name);
		}

		// Token: 0x04001461 RID: 5217
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SimpleNameSyntax name;
	}
}
