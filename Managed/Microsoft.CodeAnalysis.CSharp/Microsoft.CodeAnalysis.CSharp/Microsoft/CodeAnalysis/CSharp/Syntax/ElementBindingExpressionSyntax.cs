using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002A6 RID: 678
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ElementBindingExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003EB3 RID: 16051 RVA: 0x0010AAFB File Offset: 0x00108CFB
		internal ElementBindingExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x0010AB06 File Offset: 0x00108D06
		public BracketedArgumentListSyntax ArgumentList
		{
			get
			{
				return base.GetRedAtZero<BracketedArgumentListSyntax>(ref this.argumentList);
			}
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x0010AB14 File Offset: 0x00108D14
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<BracketedArgumentListSyntax>(ref this.argumentList);
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x0010AB27 File Offset: 0x00108D27
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.argumentList;
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x0010AB34 File Offset: 0x00108D34
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitElementBindingExpression(this);
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x0010AB3D File Offset: 0x00108D3D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitElementBindingExpression(this);
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x0010AB48 File Offset: 0x00108D48
		public ElementBindingExpressionSyntax Update(BracketedArgumentListSyntax argumentList)
		{
			if (argumentList == this.ArgumentList)
			{
				return this;
			}
			ElementBindingExpressionSyntax elementBindingExpressionSyntax = SyntaxFactory.ElementBindingExpression(argumentList);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return elementBindingExpressionSyntax;
			}
			return elementBindingExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x0010AB7E File Offset: 0x00108D7E
		public ElementBindingExpressionSyntax WithArgumentList(BracketedArgumentListSyntax argumentList)
		{
			return this.Update(argumentList);
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x0010AB88 File Offset: 0x00108D88
		public ElementBindingExpressionSyntax AddArgumentListArguments(params ArgumentSyntax[] items)
		{
			return this.WithArgumentList(this.ArgumentList.WithArguments(this.ArgumentList.Arguments.AddRange(items)));
		}

		// Token: 0x04001462 RID: 5218
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BracketedArgumentListSyntax argumentList;
	}
}
