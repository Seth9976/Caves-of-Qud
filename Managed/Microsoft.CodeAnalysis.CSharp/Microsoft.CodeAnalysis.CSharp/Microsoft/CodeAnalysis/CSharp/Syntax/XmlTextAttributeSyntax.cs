using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200033C RID: 828
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class XmlTextAttributeSyntax : XmlAttributeSyntax
	{
		// Token: 0x0600464D RID: 17997 RVA: 0x0011812F File Offset: 0x0011632F
		internal XmlTextAttributeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x0600464E RID: 17998 RVA: 0x0011813A File Offset: 0x0011633A
		public override XmlNameSyntax Name
		{
			get
			{
				return base.GetRedAtZero<XmlNameSyntax>(ref this.name);
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x0600464F RID: 17999 RVA: 0x00118148 File Offset: 0x00116348
		public override SyntaxToken EqualsToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlTextAttributeSyntax)base.Green).equalsToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06004650 RID: 18000 RVA: 0x0011816E File Offset: 0x0011636E
		public override SyntaxToken StartQuoteToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlTextAttributeSyntax)base.Green).startQuoteToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06004651 RID: 18001 RVA: 0x00118194 File Offset: 0x00116394
		public SyntaxTokenList TextTokens
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(3);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06004652 RID: 18002 RVA: 0x001181D0 File Offset: 0x001163D0
		public override SyntaxToken EndQuoteToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlTextAttributeSyntax)base.Green).endQuoteToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x001181F6 File Offset: 0x001163F6
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<XmlNameSyntax>(ref this.name);
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x00118209 File Offset: 0x00116409
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.name;
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x00118216 File Offset: 0x00116416
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlTextAttribute(this);
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x0011821F File Offset: 0x0011641F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlTextAttribute(this);
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x00118228 File Offset: 0x00116428
		public XmlTextAttributeSyntax Update(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, SyntaxTokenList textTokens, SyntaxToken endQuoteToken)
		{
			if (name == this.Name && !(equalsToken != this.EqualsToken) && !(startQuoteToken != this.StartQuoteToken) && !(textTokens != this.TextTokens) && !(endQuoteToken != this.EndQuoteToken))
			{
				return this;
			}
			XmlTextAttributeSyntax xmlTextAttributeSyntax = SyntaxFactory.XmlTextAttribute(name, equalsToken, startQuoteToken, textTokens, endQuoteToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return xmlTextAttributeSyntax;
			}
			return xmlTextAttributeSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x0011829E File Offset: 0x0011649E
		internal override XmlAttributeSyntax WithNameCore(XmlNameSyntax name)
		{
			return this.WithName(name);
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x001182A7 File Offset: 0x001164A7
		public new XmlTextAttributeSyntax WithName(XmlNameSyntax name)
		{
			return this.Update(name, this.EqualsToken, this.StartQuoteToken, this.TextTokens, this.EndQuoteToken);
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x001182C8 File Offset: 0x001164C8
		internal override XmlAttributeSyntax WithEqualsTokenCore(SyntaxToken equalsToken)
		{
			return this.WithEqualsToken(equalsToken);
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x001182D1 File Offset: 0x001164D1
		public new XmlTextAttributeSyntax WithEqualsToken(SyntaxToken equalsToken)
		{
			return this.Update(this.Name, equalsToken, this.StartQuoteToken, this.TextTokens, this.EndQuoteToken);
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x001182F2 File Offset: 0x001164F2
		internal override XmlAttributeSyntax WithStartQuoteTokenCore(SyntaxToken startQuoteToken)
		{
			return this.WithStartQuoteToken(startQuoteToken);
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x001182FB File Offset: 0x001164FB
		public new XmlTextAttributeSyntax WithStartQuoteToken(SyntaxToken startQuoteToken)
		{
			return this.Update(this.Name, this.EqualsToken, startQuoteToken, this.TextTokens, this.EndQuoteToken);
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x0011831C File Offset: 0x0011651C
		public XmlTextAttributeSyntax WithTextTokens(SyntaxTokenList textTokens)
		{
			return this.Update(this.Name, this.EqualsToken, this.StartQuoteToken, textTokens, this.EndQuoteToken);
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x0011833D File Offset: 0x0011653D
		internal override XmlAttributeSyntax WithEndQuoteTokenCore(SyntaxToken endQuoteToken)
		{
			return this.WithEndQuoteToken(endQuoteToken);
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x00118346 File Offset: 0x00116546
		public new XmlTextAttributeSyntax WithEndQuoteToken(SyntaxToken endQuoteToken)
		{
			return this.Update(this.Name, this.EqualsToken, this.StartQuoteToken, this.TextTokens, endQuoteToken);
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x00118368 File Offset: 0x00116568
		public XmlTextAttributeSyntax AddTextTokens(params SyntaxToken[] items)
		{
			return this.WithTextTokens(this.TextTokens.AddRange(items));
		}

		// Token: 0x04001524 RID: 5412
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private XmlNameSyntax name;
	}
}
