using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200024B RID: 587
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class BreakStatementSyntax : StatementSyntax
	{
		// Token: 0x0600382B RID: 14379 RVA: 0x000FA412 File Offset: 0x000F8612
		public BreakStatementSyntax Update(SyntaxToken breakKeyword, SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, breakKeyword, semicolonToken);
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000FA422 File Offset: 0x000F8622
		internal BreakStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x0600382D RID: 14381 RVA: 0x000FA42D File Offset: 0x000F862D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x0600382E RID: 14382 RVA: 0x000FA441 File Offset: 0x000F8641
		public SyntaxToken BreakKeyword
		{
			get
			{
				return new SyntaxToken(this, ((BreakStatementSyntax)base.Green).breakKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x0600382F RID: 14383 RVA: 0x000FA467 File Offset: 0x000F8667
		public SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((BreakStatementSyntax)base.Green).semicolonToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x000FA48D File Offset: 0x000F868D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero(ref this.attributeLists);
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x000FA4A0 File Offset: 0x000F86A0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.attributeLists;
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x000FA4AD File Offset: 0x000F86AD
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBreakStatement(this);
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000FA4B6 File Offset: 0x000F86B6
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBreakStatement(this);
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x000FA4C0 File Offset: 0x000F86C0
		public BreakStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken breakKeyword, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(breakKeyword != this.BreakKeyword) && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			BreakStatementSyntax breakStatementSyntax = SyntaxFactory.BreakStatement(attributeLists, breakKeyword, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return breakStatementSyntax;
			}
			return breakStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x000FA519 File Offset: 0x000F8719
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000FA522 File Offset: 0x000F8722
		public new BreakStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.BreakKeyword, this.SemicolonToken);
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000FA537 File Offset: 0x000F8737
		public BreakStatementSyntax WithBreakKeyword(SyntaxToken breakKeyword)
		{
			return this.Update(this.AttributeLists, breakKeyword, this.SemicolonToken);
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000FA54C File Offset: 0x000F874C
		public BreakStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.BreakKeyword, semicolonToken);
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000FA561 File Offset: 0x000F8761
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000FA56C File Offset: 0x000F876C
		public new BreakStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x0400139B RID: 5019
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;
	}
}
