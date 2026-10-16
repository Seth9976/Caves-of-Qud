using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200028E RID: 654
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class YieldStatementSyntax : StatementSyntax
	{
		// Token: 0x06003DB5 RID: 15797 RVA: 0x0010918A File Offset: 0x0010738A
		public YieldStatementSyntax Update(SyntaxToken yieldKeyword, SyntaxToken returnOrBreakKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, yieldKeyword, returnOrBreakKeyword, expression, semicolonToken);
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x0010919D File Offset: 0x0010739D
		internal YieldStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06003DB7 RID: 15799 RVA: 0x001091A8 File Offset: 0x001073A8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06003DB8 RID: 15800 RVA: 0x001091BC File Offset: 0x001073BC
		public SyntaxToken YieldKeyword
		{
			get
			{
				return new SyntaxToken(this, ((YieldStatementSyntax)base.Green).yieldKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06003DB9 RID: 15801 RVA: 0x001091E2 File Offset: 0x001073E2
		public SyntaxToken ReturnOrBreakKeyword
		{
			get
			{
				return new SyntaxToken(this, ((YieldStatementSyntax)base.Green).returnOrBreakKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06003DBA RID: 15802 RVA: 0x00109208 File Offset: 0x00107408
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExpressionSyntax Expression
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 3);
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06003DBB RID: 15803 RVA: 0x00109217 File Offset: 0x00107417
		public SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((YieldStatementSyntax)base.Green).semicolonToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x00109240 File Offset: 0x00107440
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
					syntaxNode = base.GetRed<ExpressionSyntax>(ref this.expression, 3);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x00109278 File Offset: 0x00107478
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
					syntaxNode = this.expression;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x001092A3 File Offset: 0x001074A3
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitYieldStatement(this);
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x001092AC File Offset: 0x001074AC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitYieldStatement(this);
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x001092B8 File Offset: 0x001074B8
		public YieldStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken yieldKeyword, SyntaxToken returnOrBreakKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(yieldKeyword != this.YieldKeyword) && !(returnOrBreakKeyword != this.ReturnOrBreakKeyword) && expression == this.Expression && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			YieldStatementSyntax yieldStatementSyntax = SyntaxFactory.YieldStatement(base.Kind(), attributeLists, yieldKeyword, returnOrBreakKeyword, expression, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return yieldStatementSyntax;
			}
			return yieldStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x00109334 File Offset: 0x00107534
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x0010933D File Offset: 0x0010753D
		public new YieldStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.YieldKeyword, this.ReturnOrBreakKeyword, this.Expression, this.SemicolonToken);
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x0010935E File Offset: 0x0010755E
		public YieldStatementSyntax WithYieldKeyword(SyntaxToken yieldKeyword)
		{
			return this.Update(this.AttributeLists, yieldKeyword, this.ReturnOrBreakKeyword, this.Expression, this.SemicolonToken);
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x0010937F File Offset: 0x0010757F
		public YieldStatementSyntax WithReturnOrBreakKeyword(SyntaxToken returnOrBreakKeyword)
		{
			return this.Update(this.AttributeLists, this.YieldKeyword, returnOrBreakKeyword, this.Expression, this.SemicolonToken);
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x001093A0 File Offset: 0x001075A0
		public YieldStatementSyntax WithExpression([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression)
		{
			return this.Update(this.AttributeLists, this.YieldKeyword, this.ReturnOrBreakKeyword, expression, this.SemicolonToken);
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x001093C1 File Offset: 0x001075C1
		public YieldStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.YieldKeyword, this.ReturnOrBreakKeyword, this.Expression, semicolonToken);
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x001093E2 File Offset: 0x001075E2
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x001093EC File Offset: 0x001075EC
		public new YieldStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x0400144A RID: 5194
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x0400144B RID: 5195
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
