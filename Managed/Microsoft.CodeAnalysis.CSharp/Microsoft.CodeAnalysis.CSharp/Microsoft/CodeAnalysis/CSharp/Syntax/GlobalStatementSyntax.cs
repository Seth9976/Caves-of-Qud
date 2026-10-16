using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000263 RID: 611
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class GlobalStatementSyntax : MemberDeclarationSyntax
	{
		// Token: 0x06003A35 RID: 14901 RVA: 0x000FF7EA File Offset: 0x000FD9EA
		public GlobalStatementSyntax Update(StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, this.Modifiers, statement);
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x000FF7FF File Offset: 0x000FD9FF
		internal GlobalStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06003A37 RID: 14903 RVA: 0x000FF80A File Offset: 0x000FDA0A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06003A38 RID: 14904 RVA: 0x000FF820 File Offset: 0x000FDA20
		public override SyntaxTokenList Modifiers
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(1);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06003A39 RID: 14905 RVA: 0x000FF85C File Offset: 0x000FDA5C
		public StatementSyntax Statement
		{
			get
			{
				return base.GetRed<StatementSyntax>(ref this.statement, 2);
			}
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x000FF86C File Offset: 0x000FDA6C
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
					syntaxNode = base.GetRed<StatementSyntax>(ref this.statement, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000FF8A4 File Offset: 0x000FDAA4
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
					syntaxNode = this.statement;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x000FF8CF File Offset: 0x000FDACF
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitGlobalStatement(this);
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x000FF8D8 File Offset: 0x000FDAD8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitGlobalStatement(this);
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x000FF8E4 File Offset: 0x000FDAE4
		public GlobalStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, StatementSyntax statement)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && statement == this.Statement)
			{
				return this;
			}
			GlobalStatementSyntax globalStatementSyntax = SyntaxFactory.GlobalStatement(attributeLists, modifiers, statement);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return globalStatementSyntax;
			}
			return globalStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x000FF938 File Offset: 0x000FDB38
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x000FF941 File Offset: 0x000FDB41
		public new GlobalStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Statement);
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x000FF956 File Offset: 0x000FDB56
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x000FF95F File Offset: 0x000FDB5F
		public new GlobalStatementSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Statement);
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x000FF974 File Offset: 0x000FDB74
		public GlobalStatementSyntax WithStatement(StatementSyntax statement)
		{
			return this.Update(this.AttributeLists, this.Modifiers, statement);
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x000FF989 File Offset: 0x000FDB89
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x000FF994 File Offset: 0x000FDB94
		public new GlobalStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x000FF9B6 File Offset: 0x000FDBB6
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x000FF9C0 File Offset: 0x000FDBC0
		public new GlobalStatementSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x040013D7 RID: 5079
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013D8 RID: 5080
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private StatementSyntax statement;
	}
}
