using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200025A RID: 602
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class EmptyStatementSyntax : StatementSyntax
	{
		// Token: 0x0600394D RID: 14669 RVA: 0x000FD426 File Offset: 0x000FB626
		public EmptyStatementSyntax Update(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, semicolonToken);
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x000FD435 File Offset: 0x000FB635
		internal EmptyStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x0600394F RID: 14671 RVA: 0x000FD440 File Offset: 0x000FB640
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06003950 RID: 14672 RVA: 0x000FD454 File Offset: 0x000FB654
		public SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((EmptyStatementSyntax)base.Green).semicolonToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000FD47A File Offset: 0x000FB67A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero(ref this.attributeLists);
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x000FD48D File Offset: 0x000FB68D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.attributeLists;
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x000FD49A File Offset: 0x000FB69A
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEmptyStatement(this);
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000FD4A3 File Offset: 0x000FB6A3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEmptyStatement(this);
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x000FD4AC File Offset: 0x000FB6AC
		public EmptyStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			EmptyStatementSyntax emptyStatementSyntax = SyntaxFactory.EmptyStatement(attributeLists, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return emptyStatementSyntax;
			}
			return emptyStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x000FD4F6 File Offset: 0x000FB6F6
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x000FD4FF File Offset: 0x000FB6FF
		public new EmptyStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.SemicolonToken);
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x000FD50E File Offset: 0x000FB70E
		public EmptyStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, semicolonToken);
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000FD51D File Offset: 0x000FB71D
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000FD528 File Offset: 0x000FB728
		public new EmptyStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x040013BC RID: 5052
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;
	}
}
