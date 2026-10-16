using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000328 RID: 808
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class DocumentationCommentTriviaSyntax : StructuredTriviaSyntax
	{
		// Token: 0x0600457C RID: 17788 RVA: 0x00116C9E File Offset: 0x00114E9E
		internal DocumentationCommentTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x0600457D RID: 17789 RVA: 0x00116CA9 File Offset: 0x00114EA9
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<XmlNodeSyntax> Content
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<XmlNodeSyntax>(base.GetRed(ref this.content, 0));
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x0600457E RID: 17790 RVA: 0x00116CBD File Offset: 0x00114EBD
		public SyntaxToken EndOfComment
		{
			get
			{
				return new SyntaxToken(this, ((DocumentationCommentTriviaSyntax)base.Green).endOfComment, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x00116CE3 File Offset: 0x00114EE3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero(ref this.content);
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x00116CF6 File Offset: 0x00114EF6
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.content;
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x00116D03 File Offset: 0x00114F03
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDocumentationCommentTrivia(this);
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x00116D0C File Offset: 0x00114F0C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDocumentationCommentTrivia(this);
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x00116D18 File Offset: 0x00114F18
		public DocumentationCommentTriviaSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlNodeSyntax> content, SyntaxToken endOfComment)
		{
			if (!(content != this.Content) && !(endOfComment != this.EndOfComment))
			{
				return this;
			}
			DocumentationCommentTriviaSyntax documentationCommentTriviaSyntax = SyntaxFactory.DocumentationCommentTrivia(base.Kind(), content, endOfComment);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return documentationCommentTriviaSyntax;
			}
			return documentationCommentTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x00116D68 File Offset: 0x00114F68
		public DocumentationCommentTriviaSyntax WithContent([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlNodeSyntax> content)
		{
			return this.Update(content, this.EndOfComment);
		}

		// Token: 0x06004585 RID: 17797 RVA: 0x00116D77 File Offset: 0x00114F77
		public DocumentationCommentTriviaSyntax WithEndOfComment(SyntaxToken endOfComment)
		{
			return this.Update(this.Content, endOfComment);
		}

		// Token: 0x06004586 RID: 17798 RVA: 0x00116D88 File Offset: 0x00114F88
		public DocumentationCommentTriviaSyntax AddContent(params XmlNodeSyntax[] items)
		{
			return this.WithContent(this.Content.AddRange(items));
		}

		// Token: 0x0400150F RID: 5391
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode content;
	}
}
