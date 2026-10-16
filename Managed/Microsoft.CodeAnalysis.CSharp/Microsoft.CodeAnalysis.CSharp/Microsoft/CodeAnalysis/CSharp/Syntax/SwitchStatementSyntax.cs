using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000280 RID: 640
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class SwitchStatementSyntax : StatementSyntax
	{
		// Token: 0x06003CBA RID: 15546 RVA: 0x00105DCC File Offset: 0x00103FCC
		public SwitchStatementSyntax Update(SyntaxToken switchKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SwitchSectionSyntax> sections, SyntaxToken closeBraceToken)
		{
			return this.Update(this.AttributeLists, switchKeyword, openParenToken, expression, closeParenToken, openBraceToken, sections, closeBraceToken);
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x00105DF0 File Offset: 0x00103FF0
		internal SwitchStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06003CBC RID: 15548 RVA: 0x00105DFB File Offset: 0x00103FFB
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06003CBD RID: 15549 RVA: 0x00105E0F File Offset: 0x0010400F
		public SyntaxToken SwitchKeyword
		{
			get
			{
				return new SyntaxToken(this, ((SwitchStatementSyntax)base.Green).switchKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06003CBE RID: 15550 RVA: 0x00105E38 File Offset: 0x00104038
		public SyntaxToken OpenParenToken
		{
			get
			{
				SyntaxToken openParenToken = ((SwitchStatementSyntax)base.Green).openParenToken;
				if (openParenToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, openParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x00105E78 File Offset: 0x00104078
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 3);
			}
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06003CC0 RID: 15552 RVA: 0x00105E88 File Offset: 0x00104088
		public SyntaxToken CloseParenToken
		{
			get
			{
				SyntaxToken closeParenToken = ((SwitchStatementSyntax)base.Green).closeParenToken;
				if (closeParenToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, closeParenToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x00105EC8 File Offset: 0x001040C8
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((SwitchStatementSyntax)base.Green).openBraceToken, this.GetChildPosition(5), base.GetChildIndex(5));
			}
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06003CC2 RID: 15554 RVA: 0x00105EEE File Offset: 0x001040EE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<SwitchSectionSyntax> Sections
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SwitchSectionSyntax>(base.GetRed(ref this.sections, 6));
			}
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x00105F02 File Offset: 0x00104102
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((SwitchStatementSyntax)base.Green).closeBraceToken, this.GetChildPosition(7), base.GetChildIndex(7));
			}
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x00105F28 File Offset: 0x00104128
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 3)
				{
					if (index != 6)
					{
						syntaxNode = null;
					}
					else
					{
						syntaxNode = base.GetRed(ref this.sections, 6);
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

		// Token: 0x06003CC5 RID: 15557 RVA: 0x00105F74 File Offset: 0x00104174
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 3)
				{
					if (index != 6)
					{
						syntaxNode = null;
					}
					else
					{
						syntaxNode = this.sections;
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

		// Token: 0x06003CC6 RID: 15558 RVA: 0x00105FAC File Offset: 0x001041AC
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSwitchStatement(this);
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x00105FB5 File Offset: 0x001041B5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSwitchStatement(this);
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x00105FC0 File Offset: 0x001041C0
		public SwitchStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken switchKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SwitchSectionSyntax> sections, SyntaxToken closeBraceToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(switchKeyword != this.SwitchKeyword) && !(openParenToken != this.OpenParenToken) && expression == this.Expression && !(closeParenToken != this.CloseParenToken) && !(openBraceToken != this.OpenBraceToken) && !(sections != this.Sections) && !(closeBraceToken != this.CloseBraceToken))
			{
				return this;
			}
			SwitchStatementSyntax switchStatementSyntax = SyntaxFactory.SwitchStatement(attributeLists, switchKeyword, openParenToken, expression, closeParenToken, openBraceToken, sections, closeBraceToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return switchStatementSyntax;
			}
			return switchStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x00106069 File Offset: 0x00104269
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x00106074 File Offset: 0x00104274
		public new SwitchStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.SwitchKeyword, this.OpenParenToken, this.Expression, this.CloseParenToken, this.OpenBraceToken, this.Sections, this.CloseBraceToken);
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x001060B4 File Offset: 0x001042B4
		public SwitchStatementSyntax WithSwitchKeyword(SyntaxToken switchKeyword)
		{
			return this.Update(this.AttributeLists, switchKeyword, this.OpenParenToken, this.Expression, this.CloseParenToken, this.OpenBraceToken, this.Sections, this.CloseBraceToken);
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x001060F4 File Offset: 0x001042F4
		public SwitchStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.AttributeLists, this.SwitchKeyword, openParenToken, this.Expression, this.CloseParenToken, this.OpenBraceToken, this.Sections, this.CloseBraceToken);
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x00106134 File Offset: 0x00104334
		public SwitchStatementSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.AttributeLists, this.SwitchKeyword, this.OpenParenToken, expression, this.CloseParenToken, this.OpenBraceToken, this.Sections, this.CloseBraceToken);
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x00106174 File Offset: 0x00104374
		public SwitchStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.AttributeLists, this.SwitchKeyword, this.OpenParenToken, this.Expression, closeParenToken, this.OpenBraceToken, this.Sections, this.CloseBraceToken);
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x001061B4 File Offset: 0x001043B4
		public SwitchStatementSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.Update(this.AttributeLists, this.SwitchKeyword, this.OpenParenToken, this.Expression, this.CloseParenToken, openBraceToken, this.Sections, this.CloseBraceToken);
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x001061F4 File Offset: 0x001043F4
		public SwitchStatementSyntax WithSections([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SwitchSectionSyntax> sections)
		{
			return this.Update(this.AttributeLists, this.SwitchKeyword, this.OpenParenToken, this.Expression, this.CloseParenToken, this.OpenBraceToken, sections, this.CloseBraceToken);
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x00106234 File Offset: 0x00104434
		public SwitchStatementSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.Update(this.AttributeLists, this.SwitchKeyword, this.OpenParenToken, this.Expression, this.CloseParenToken, this.OpenBraceToken, this.Sections, closeBraceToken);
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x00106272 File Offset: 0x00104472
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x0010627C File Offset: 0x0010447C
		public new SwitchStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x001062A0 File Offset: 0x001044A0
		public SwitchStatementSyntax AddSections(params SwitchSectionSyntax[] items)
		{
			return this.WithSections(this.Sections.AddRange(items));
		}

		// Token: 0x04001425 RID: 5157
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x04001426 RID: 5158
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;

		// Token: 0x04001427 RID: 5159
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode sections;
	}
}
