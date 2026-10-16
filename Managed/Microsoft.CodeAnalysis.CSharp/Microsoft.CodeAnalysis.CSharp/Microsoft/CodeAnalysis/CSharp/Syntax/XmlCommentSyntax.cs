using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000342 RID: 834
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class XmlCommentSyntax : XmlNodeSyntax
	{
		// Token: 0x060046AF RID: 18095 RVA: 0x00118C0E File Offset: 0x00116E0E
		internal XmlCommentSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x060046B0 RID: 18096 RVA: 0x00118C19 File Offset: 0x00116E19
		public SyntaxToken LessThanExclamationMinusMinusToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlCommentSyntax)base.Green).lessThanExclamationMinusMinusToken, base.Position, 0);
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x060046B1 RID: 18097 RVA: 0x00118C38 File Offset: 0x00116E38
		public SyntaxTokenList TextTokens
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

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x060046B2 RID: 18098 RVA: 0x00118C74 File Offset: 0x00116E74
		public SyntaxToken MinusMinusGreaterThanToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlCommentSyntax)base.Green).minusMinusGreaterThanToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x00118C9A File Offset: 0x00116E9A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x00118C9D File Offset: 0x00116E9D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x00118CA0 File Offset: 0x00116EA0
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlComment(this);
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x00118CA9 File Offset: 0x00116EA9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlComment(this);
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x00118CB4 File Offset: 0x00116EB4
		public XmlCommentSyntax Update(SyntaxToken lessThanExclamationMinusMinusToken, SyntaxTokenList textTokens, SyntaxToken minusMinusGreaterThanToken)
		{
			if (!(lessThanExclamationMinusMinusToken != this.LessThanExclamationMinusMinusToken) && !(textTokens != this.TextTokens) && !(minusMinusGreaterThanToken != this.MinusMinusGreaterThanToken))
			{
				return this;
			}
			XmlCommentSyntax xmlCommentSyntax = SyntaxFactory.XmlComment(lessThanExclamationMinusMinusToken, textTokens, minusMinusGreaterThanToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return xmlCommentSyntax;
			}
			return xmlCommentSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x00118D0D File Offset: 0x00116F0D
		public XmlCommentSyntax WithLessThanExclamationMinusMinusToken(SyntaxToken lessThanExclamationMinusMinusToken)
		{
			return this.Update(lessThanExclamationMinusMinusToken, this.TextTokens, this.MinusMinusGreaterThanToken);
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x00118D22 File Offset: 0x00116F22
		public XmlCommentSyntax WithTextTokens(SyntaxTokenList textTokens)
		{
			return this.Update(this.LessThanExclamationMinusMinusToken, textTokens, this.MinusMinusGreaterThanToken);
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x00118D37 File Offset: 0x00116F37
		public XmlCommentSyntax WithMinusMinusGreaterThanToken(SyntaxToken minusMinusGreaterThanToken)
		{
			return this.Update(this.LessThanExclamationMinusMinusToken, this.TextTokens, minusMinusGreaterThanToken);
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x00118D4C File Offset: 0x00116F4C
		public XmlCommentSyntax AddTextTokens(params SyntaxToken[] items)
		{
			return this.WithTextTokens(this.TextTokens.AddRange(items));
		}
	}
}
