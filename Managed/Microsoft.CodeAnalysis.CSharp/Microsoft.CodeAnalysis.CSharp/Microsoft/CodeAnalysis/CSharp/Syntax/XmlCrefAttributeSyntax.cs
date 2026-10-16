using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200033D RID: 829
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class XmlCrefAttributeSyntax : XmlAttributeSyntax
	{
		// Token: 0x06004662 RID: 18018 RVA: 0x0011838A File Offset: 0x0011658A
		internal XmlCrefAttributeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06004663 RID: 18019 RVA: 0x00118395 File Offset: 0x00116595
		public override XmlNameSyntax Name
		{
			get
			{
				return base.GetRedAtZero<XmlNameSyntax>(ref this.name);
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06004664 RID: 18020 RVA: 0x001183A3 File Offset: 0x001165A3
		public override SyntaxToken EqualsToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlCrefAttributeSyntax)base.Green).equalsToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06004665 RID: 18021 RVA: 0x001183C9 File Offset: 0x001165C9
		public override SyntaxToken StartQuoteToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlCrefAttributeSyntax)base.Green).startQuoteToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06004666 RID: 18022 RVA: 0x001183EF File Offset: 0x001165EF
		public CrefSyntax Cref
		{
			get
			{
				return base.GetRed<CrefSyntax>(ref this.cref, 3);
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06004667 RID: 18023 RVA: 0x001183FE File Offset: 0x001165FE
		public override SyntaxToken EndQuoteToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlCrefAttributeSyntax)base.Green).endQuoteToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x00118424 File Offset: 0x00116624
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
					syntaxNode = base.GetRed<CrefSyntax>(ref this.cref, 3);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<XmlNameSyntax>(ref this.name);
			}
			return syntaxNode;
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x0011845C File Offset: 0x0011665C
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
					syntaxNode = this.cref;
				}
			}
			else
			{
				syntaxNode = this.name;
			}
			return syntaxNode;
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x00118487 File Offset: 0x00116687
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlCrefAttribute(this);
		}

		// Token: 0x0600466B RID: 18027 RVA: 0x00118490 File Offset: 0x00116690
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlCrefAttribute(this);
		}

		// Token: 0x0600466C RID: 18028 RVA: 0x0011849C File Offset: 0x0011669C
		public XmlCrefAttributeSyntax Update(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, CrefSyntax cref, SyntaxToken endQuoteToken)
		{
			if (name == this.Name && !(equalsToken != this.EqualsToken) && !(startQuoteToken != this.StartQuoteToken) && cref == this.Cref && !(endQuoteToken != this.EndQuoteToken))
			{
				return this;
			}
			XmlCrefAttributeSyntax xmlCrefAttributeSyntax = SyntaxFactory.XmlCrefAttribute(name, equalsToken, startQuoteToken, cref, endQuoteToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return xmlCrefAttributeSyntax;
			}
			return xmlCrefAttributeSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x0011850D File Offset: 0x0011670D
		internal override XmlAttributeSyntax WithNameCore(XmlNameSyntax name)
		{
			return this.WithName(name);
		}

		// Token: 0x0600466E RID: 18030 RVA: 0x00118516 File Offset: 0x00116716
		public new XmlCrefAttributeSyntax WithName(XmlNameSyntax name)
		{
			return this.Update(name, this.EqualsToken, this.StartQuoteToken, this.Cref, this.EndQuoteToken);
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x00118537 File Offset: 0x00116737
		internal override XmlAttributeSyntax WithEqualsTokenCore(SyntaxToken equalsToken)
		{
			return this.WithEqualsToken(equalsToken);
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x00118540 File Offset: 0x00116740
		public new XmlCrefAttributeSyntax WithEqualsToken(SyntaxToken equalsToken)
		{
			return this.Update(this.Name, equalsToken, this.StartQuoteToken, this.Cref, this.EndQuoteToken);
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x00118561 File Offset: 0x00116761
		internal override XmlAttributeSyntax WithStartQuoteTokenCore(SyntaxToken startQuoteToken)
		{
			return this.WithStartQuoteToken(startQuoteToken);
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x0011856A File Offset: 0x0011676A
		public new XmlCrefAttributeSyntax WithStartQuoteToken(SyntaxToken startQuoteToken)
		{
			return this.Update(this.Name, this.EqualsToken, startQuoteToken, this.Cref, this.EndQuoteToken);
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x0011858B File Offset: 0x0011678B
		public XmlCrefAttributeSyntax WithCref(CrefSyntax cref)
		{
			return this.Update(this.Name, this.EqualsToken, this.StartQuoteToken, cref, this.EndQuoteToken);
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x001185AC File Offset: 0x001167AC
		internal override XmlAttributeSyntax WithEndQuoteTokenCore(SyntaxToken endQuoteToken)
		{
			return this.WithEndQuoteToken(endQuoteToken);
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x001185B5 File Offset: 0x001167B5
		public new XmlCrefAttributeSyntax WithEndQuoteToken(SyntaxToken endQuoteToken)
		{
			return this.Update(this.Name, this.EqualsToken, this.StartQuoteToken, this.Cref, endQuoteToken);
		}

		// Token: 0x04001525 RID: 5413
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private XmlNameSyntax name;

		// Token: 0x04001526 RID: 5414
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private CrefSyntax cref;
	}
}
