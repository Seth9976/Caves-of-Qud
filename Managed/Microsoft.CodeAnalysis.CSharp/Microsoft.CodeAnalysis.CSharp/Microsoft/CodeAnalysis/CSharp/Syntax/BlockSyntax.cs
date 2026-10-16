using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200024A RID: 586
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class BlockSyntax : StatementSyntax
	{
		// Token: 0x06003818 RID: 14360 RVA: 0x000FA1DB File Offset: 0x000F83DB
		public BlockSyntax Update(SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<StatementSyntax> statements, SyntaxToken closeBraceToken)
		{
			return this.Update(this.AttributeLists, openBraceToken, statements, closeBraceToken);
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x000FA1EC File Offset: 0x000F83EC
		internal BlockSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x0600381A RID: 14362 RVA: 0x000FA1F7 File Offset: 0x000F83F7
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x0600381B RID: 14363 RVA: 0x000FA20B File Offset: 0x000F840B
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((BlockSyntax)base.Green).openBraceToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x0600381C RID: 14364 RVA: 0x000FA231 File Offset: 0x000F8431
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<StatementSyntax> Statements
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<StatementSyntax>(base.GetRed(ref this.statements, 2));
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x0600381D RID: 14365 RVA: 0x000FA245 File Offset: 0x000F8445
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((BlockSyntax)base.Green).closeBraceToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000FA26C File Offset: 0x000F846C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed(ref this.statements, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000FA2A4 File Offset: 0x000F84A4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.statements;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000FA2CF File Offset: 0x000F84CF
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBlock(this);
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000FA2D8 File Offset: 0x000F84D8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBlock(this);
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000FA2E4 File Offset: 0x000F84E4
		public BlockSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<StatementSyntax> statements, SyntaxToken closeBraceToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(openBraceToken != this.OpenBraceToken) && !(statements != this.Statements) && !(closeBraceToken != this.CloseBraceToken))
			{
				return this;
			}
			BlockSyntax blockSyntax = SyntaxFactory.Block(attributeLists, openBraceToken, statements, closeBraceToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return blockSyntax;
			}
			return blockSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000FA34E File Offset: 0x000F854E
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000FA357 File Offset: 0x000F8557
		public new BlockSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.OpenBraceToken, this.Statements, this.CloseBraceToken);
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000FA372 File Offset: 0x000F8572
		public BlockSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.Update(this.AttributeLists, openBraceToken, this.Statements, this.CloseBraceToken);
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000FA38D File Offset: 0x000F858D
		public BlockSyntax WithStatements([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<StatementSyntax> statements)
		{
			return this.Update(this.AttributeLists, this.OpenBraceToken, statements, this.CloseBraceToken);
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000FA3A8 File Offset: 0x000F85A8
		public BlockSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.Update(this.AttributeLists, this.OpenBraceToken, this.Statements, closeBraceToken);
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000FA3C3 File Offset: 0x000F85C3
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x000FA3CC File Offset: 0x000F85CC
		public new BlockSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x000FA3F0 File Offset: 0x000F85F0
		public BlockSyntax AddStatements(params StatementSyntax[] items)
		{
			return this.WithStatements(this.Statements.AddRange(items));
		}

		// Token: 0x04001399 RID: 5017
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x0400139A RID: 5018
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode statements;
	}
}
