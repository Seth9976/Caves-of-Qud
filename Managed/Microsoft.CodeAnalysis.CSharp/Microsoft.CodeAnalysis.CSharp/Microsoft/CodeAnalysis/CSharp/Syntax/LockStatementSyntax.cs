using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200026B RID: 619
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class LockStatementSyntax : StatementSyntax
	{
		// Token: 0x06003AEB RID: 15083 RVA: 0x001011B5 File Offset: 0x000FF3B5
		public LockStatementSyntax Update(SyntaxToken lockKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, lockKeyword, openParenToken, expression, closeParenToken, statement);
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x001011CA File Offset: 0x000FF3CA
		internal LockStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06003AED RID: 15085 RVA: 0x001011D5 File Offset: 0x000FF3D5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06003AEE RID: 15086 RVA: 0x001011E9 File Offset: 0x000FF3E9
		public SyntaxToken LockKeyword
		{
			get
			{
				return new SyntaxToken(this, ((LockStatementSyntax)base.Green).lockKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06003AEF RID: 15087 RVA: 0x0010120F File Offset: 0x000FF40F
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((LockStatementSyntax)base.Green).openParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x00101235 File Offset: 0x000FF435
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 3);
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x00101244 File Offset: 0x000FF444
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((LockStatementSyntax)base.Green).closeParenToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x0010126A File Offset: 0x000FF46A
		public StatementSyntax Statement
		{
			get
			{
				return base.GetRed<StatementSyntax>(ref this.statement, 5);
			}
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x0010127C File Offset: 0x000FF47C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 3)
				{
					if (index != 5)
					{
						syntaxNode = null;
					}
					else
					{
						syntaxNode = base.GetRed<StatementSyntax>(ref this.statement, 5);
					}
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

		// Token: 0x06003AF4 RID: 15092 RVA: 0x001012C8 File Offset: 0x000FF4C8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 3)
				{
					if (index != 5)
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
					syntaxNode = this.expression;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x00101300 File Offset: 0x000FF500
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLockStatement(this);
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x00101309 File Offset: 0x000FF509
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLockStatement(this);
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x00101314 File Offset: 0x000FF514
		public LockStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken lockKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			if (!(attributeLists != this.AttributeLists) && !(lockKeyword != this.LockKeyword) && !(openParenToken != this.OpenParenToken) && expression == this.Expression && !(closeParenToken != this.CloseParenToken) && statement == this.Statement)
			{
				return this;
			}
			LockStatementSyntax lockStatementSyntax = SyntaxFactory.LockStatement(attributeLists, lockKeyword, openParenToken, expression, closeParenToken, statement);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return lockStatementSyntax;
			}
			return lockStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x00101396 File Offset: 0x000FF596
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x0010139F File Offset: 0x000FF59F
		public new LockStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.LockKeyword, this.OpenParenToken, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x001013C6 File Offset: 0x000FF5C6
		public LockStatementSyntax WithLockKeyword(SyntaxToken lockKeyword)
		{
			return this.Update(this.AttributeLists, lockKeyword, this.OpenParenToken, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x001013ED File Offset: 0x000FF5ED
		public LockStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.AttributeLists, this.LockKeyword, openParenToken, this.Expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x00101414 File Offset: 0x000FF614
		public LockStatementSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.AttributeLists, this.LockKeyword, this.OpenParenToken, expression, this.CloseParenToken, this.Statement);
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x0010143B File Offset: 0x000FF63B
		public LockStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.AttributeLists, this.LockKeyword, this.OpenParenToken, this.Expression, closeParenToken, this.Statement);
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x00101462 File Offset: 0x000FF662
		public LockStatementSyntax WithStatement(StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, this.LockKeyword, this.OpenParenToken, this.Expression, this.CloseParenToken, statement);
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x00101489 File Offset: 0x000FF689
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x00101494 File Offset: 0x000FF694
		public new LockStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x040013EE RID: 5102
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013EF RID: 5103
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;

		// Token: 0x040013F0 RID: 5104
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private StatementSyntax statement;
	}
}
