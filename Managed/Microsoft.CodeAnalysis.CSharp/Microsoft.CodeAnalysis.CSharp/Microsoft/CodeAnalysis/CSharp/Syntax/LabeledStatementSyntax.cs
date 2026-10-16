using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000268 RID: 616
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class LabeledStatementSyntax : StatementSyntax
	{
		// Token: 0x06003AA4 RID: 15012 RVA: 0x0010073D File Offset: 0x000FE93D
		public LabeledStatementSyntax Update(SyntaxToken identifier, SyntaxToken colonToken, StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, identifier, colonToken, statement);
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x0010074E File Offset: 0x000FE94E
		internal LabeledStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x00100759 File Offset: 0x000FE959
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06003AA7 RID: 15015 RVA: 0x0010076D File Offset: 0x000FE96D
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((LabeledStatementSyntax)base.Green).identifier, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x00100793 File Offset: 0x000FE993
		public SyntaxToken ColonToken
		{
			get
			{
				return new SyntaxToken(this, ((LabeledStatementSyntax)base.Green).colonToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06003AA9 RID: 15017 RVA: 0x001007B9 File Offset: 0x000FE9B9
		public StatementSyntax Statement
		{
			get
			{
				return base.GetRed<StatementSyntax>(ref this.statement, 3);
			}
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x001007C8 File Offset: 0x000FE9C8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 3)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<StatementSyntax>(ref this.statement, 3);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x00100800 File Offset: 0x000FEA00
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 3)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.statement;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x0010082B File Offset: 0x000FEA2B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLabeledStatement(this);
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x00100834 File Offset: 0x000FEA34
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLabeledStatement(this);
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x00100840 File Offset: 0x000FEA40
		public LabeledStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken identifier, SyntaxToken colonToken, StatementSyntax statement)
		{
			if (!(attributeLists != this.AttributeLists) && !(identifier != this.Identifier) && !(colonToken != this.ColonToken) && statement == this.Statement)
			{
				return this;
			}
			LabeledStatementSyntax labeledStatementSyntax = SyntaxFactory.LabeledStatement(attributeLists, identifier, colonToken, statement);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return labeledStatementSyntax;
			}
			return labeledStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x001008A5 File Offset: 0x000FEAA5
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x001008AE File Offset: 0x000FEAAE
		public new LabeledStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Identifier, this.ColonToken, this.Statement);
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x001008C9 File Offset: 0x000FEAC9
		public LabeledStatementSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, identifier, this.ColonToken, this.Statement);
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x001008E4 File Offset: 0x000FEAE4
		public LabeledStatementSyntax WithColonToken(SyntaxToken colonToken)
		{
			return this.Update(this.AttributeLists, this.Identifier, colonToken, this.Statement);
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x001008FF File Offset: 0x000FEAFF
		public LabeledStatementSyntax WithStatement(StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, this.Identifier, this.ColonToken, statement);
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x0010091A File Offset: 0x000FEB1A
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x00100924 File Offset: 0x000FEB24
		public new LabeledStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x040013E5 RID: 5093
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013E6 RID: 5094
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private StatementSyntax statement;
	}
}
