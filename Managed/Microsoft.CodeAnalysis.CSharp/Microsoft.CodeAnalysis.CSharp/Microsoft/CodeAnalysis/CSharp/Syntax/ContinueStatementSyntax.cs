using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000250 RID: 592
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ContinueStatementSyntax : StatementSyntax
	{
		// Token: 0x06003898 RID: 14488 RVA: 0x000FB2AD File Offset: 0x000F94AD
		public ContinueStatementSyntax Update(SyntaxToken continueKeyword, SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, continueKeyword, semicolonToken);
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000FB2BD File Offset: 0x000F94BD
		internal ContinueStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x0600389A RID: 14490 RVA: 0x000FB2C8 File Offset: 0x000F94C8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x0600389B RID: 14491 RVA: 0x000FB2DC File Offset: 0x000F94DC
		public SyntaxToken ContinueKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ContinueStatementSyntax)base.Green).continueKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x0600389C RID: 14492 RVA: 0x000FB302 File Offset: 0x000F9502
		public SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((ContinueStatementSyntax)base.Green).semicolonToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000FB328 File Offset: 0x000F9528
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero(ref this.attributeLists);
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000FB33B File Offset: 0x000F953B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.attributeLists;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000FB348 File Offset: 0x000F9548
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitContinueStatement(this);
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000FB351 File Offset: 0x000F9551
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitContinueStatement(this);
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000FB35C File Offset: 0x000F955C
		public ContinueStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken continueKeyword, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(continueKeyword != this.ContinueKeyword) && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			ContinueStatementSyntax continueStatementSyntax = SyntaxFactory.ContinueStatement(attributeLists, continueKeyword, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return continueStatementSyntax;
			}
			return continueStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000FB3B5 File Offset: 0x000F95B5
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000FB3BE File Offset: 0x000F95BE
		public new ContinueStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.ContinueKeyword, this.SemicolonToken);
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000FB3D3 File Offset: 0x000F95D3
		public ContinueStatementSyntax WithContinueKeyword(SyntaxToken continueKeyword)
		{
			return this.Update(this.AttributeLists, continueKeyword, this.SemicolonToken);
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x000FB3E8 File Offset: 0x000F95E8
		public ContinueStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.ContinueKeyword, semicolonToken);
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000FB3FD File Offset: 0x000F95FD
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000FB408 File Offset: 0x000F9608
		public new ContinueStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x040013A7 RID: 5031
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;
	}
}
