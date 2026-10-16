using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002D3 RID: 723
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class OrderByClauseSyntax : QueryClauseSyntax
	{
		// Token: 0x060040D5 RID: 16597 RVA: 0x0010E81F File Offset: 0x0010CA1F
		internal OrderByClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x0010E82A File Offset: 0x0010CA2A
		public SyntaxToken OrderByKeyword
		{
			get
			{
				return new SyntaxToken(this, ((OrderByClauseSyntax)base.Green).orderByKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x060040D7 RID: 16599 RVA: 0x0010E84C File Offset: 0x0010CA4C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<OrderingSyntax> Orderings
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.orderings, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<OrderingSyntax>);
				}
				return new SeparatedSyntaxList<OrderingSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x0010E881 File Offset: 0x0010CA81
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.orderings, 1);
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x0010E896 File Offset: 0x0010CA96
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.orderings;
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x0010E8A4 File Offset: 0x0010CAA4
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitOrderByClause(this);
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x0010E8AD File Offset: 0x0010CAAD
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitOrderByClause(this);
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x0010E8B8 File Offset: 0x0010CAB8
		public OrderByClauseSyntax Update(SyntaxToken orderByKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<OrderingSyntax> orderings)
		{
			if (!(orderByKeyword != this.OrderByKeyword) && !(orderings != this.Orderings))
			{
				return this;
			}
			OrderByClauseSyntax orderByClauseSyntax = SyntaxFactory.OrderByClause(orderByKeyword, orderings);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return orderByClauseSyntax;
			}
			return orderByClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060040DD RID: 16605 RVA: 0x0010E902 File Offset: 0x0010CB02
		public OrderByClauseSyntax WithOrderByKeyword(SyntaxToken orderByKeyword)
		{
			return this.Update(orderByKeyword, this.Orderings);
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x0010E911 File Offset: 0x0010CB11
		public OrderByClauseSyntax WithOrderings([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<OrderingSyntax> orderings)
		{
			return this.Update(this.OrderByKeyword, orderings);
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x0010E920 File Offset: 0x0010CB20
		public OrderByClauseSyntax AddOrderings(params OrderingSyntax[] items)
		{
			return this.WithOrderings(this.Orderings.AddRange(items));
		}

		// Token: 0x0400149E RID: 5278
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode orderings;
	}
}
