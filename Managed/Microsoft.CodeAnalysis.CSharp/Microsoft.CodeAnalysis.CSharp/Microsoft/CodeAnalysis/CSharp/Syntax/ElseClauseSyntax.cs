using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002F9 RID: 761
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ElseClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600425C RID: 16988 RVA: 0x00110C18 File Offset: 0x0010EE18
		internal ElseClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x0600425D RID: 16989 RVA: 0x00110C23 File Offset: 0x0010EE23
		public SyntaxToken ElseKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ElseClauseSyntax)base.Green).elseKeyword, base.Position, 0);
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x0600425E RID: 16990 RVA: 0x00110C42 File Offset: 0x0010EE42
		public StatementSyntax Statement
		{
			get
			{
				return base.GetRed<StatementSyntax>(ref this.statement, 1);
			}
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x00110C51 File Offset: 0x0010EE51
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<StatementSyntax>(ref this.statement, 1);
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x00110C66 File Offset: 0x0010EE66
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.statement;
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x00110C74 File Offset: 0x0010EE74
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitElseClause(this);
		}

		// Token: 0x06004262 RID: 16994 RVA: 0x00110C7D File Offset: 0x0010EE7D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitElseClause(this);
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x00110C88 File Offset: 0x0010EE88
		public ElseClauseSyntax Update(SyntaxToken elseKeyword, StatementSyntax statement)
		{
			if (!(elseKeyword != this.ElseKeyword) && statement == this.Statement)
			{
				return this;
			}
			ElseClauseSyntax elseClauseSyntax = SyntaxFactory.ElseClause(elseKeyword, statement);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return elseClauseSyntax;
			}
			return elseClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x00110CCD File Offset: 0x0010EECD
		public ElseClauseSyntax WithElseKeyword(SyntaxToken elseKeyword)
		{
			return this.Update(elseKeyword, this.Statement);
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x00110CDC File Offset: 0x0010EEDC
		public ElseClauseSyntax WithStatement(StatementSyntax statement)
		{
			return this.Update(this.ElseKeyword, statement);
		}

		// Token: 0x040014C5 RID: 5317
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private StatementSyntax statement;
	}
}
