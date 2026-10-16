using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002A8 RID: 680
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ImplicitElementAccessSyntax : ExpressionSyntax
	{
		// Token: 0x06003EC8 RID: 16072 RVA: 0x0010AD0E File Offset: 0x00108F0E
		internal ImplicitElementAccessSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x0010AD19 File Offset: 0x00108F19
		public BracketedArgumentListSyntax ArgumentList
		{
			get
			{
				return base.GetRedAtZero<BracketedArgumentListSyntax>(ref this.argumentList);
			}
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x0010AD27 File Offset: 0x00108F27
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<BracketedArgumentListSyntax>(ref this.argumentList);
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x0010AD3A File Offset: 0x00108F3A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.argumentList;
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x0010AD47 File Offset: 0x00108F47
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitImplicitElementAccess(this);
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x0010AD50 File Offset: 0x00108F50
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitImplicitElementAccess(this);
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x0010AD5C File Offset: 0x00108F5C
		public ImplicitElementAccessSyntax Update(BracketedArgumentListSyntax argumentList)
		{
			if (argumentList == this.ArgumentList)
			{
				return this;
			}
			ImplicitElementAccessSyntax implicitElementAccessSyntax = SyntaxFactory.ImplicitElementAccess(argumentList);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return implicitElementAccessSyntax;
			}
			return implicitElementAccessSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x0010AD92 File Offset: 0x00108F92
		public ImplicitElementAccessSyntax WithArgumentList(BracketedArgumentListSyntax argumentList)
		{
			return this.Update(argumentList);
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x0010AD9C File Offset: 0x00108F9C
		public ImplicitElementAccessSyntax AddArgumentListArguments(params ArgumentSyntax[] items)
		{
			return this.WithArgumentList(this.ArgumentList.WithArguments(this.ArgumentList.Arguments.AddRange(items)));
		}

		// Token: 0x04001465 RID: 5221
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BracketedArgumentListSyntax argumentList;
	}
}
