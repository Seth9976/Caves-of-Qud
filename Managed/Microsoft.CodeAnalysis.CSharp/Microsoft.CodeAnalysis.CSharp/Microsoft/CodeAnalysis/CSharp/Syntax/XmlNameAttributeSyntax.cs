using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200033E RID: 830
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class XmlNameAttributeSyntax : XmlAttributeSyntax
	{
		// Token: 0x06004676 RID: 18038 RVA: 0x001185D6 File Offset: 0x001167D6
		internal XmlNameAttributeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06004677 RID: 18039 RVA: 0x001185E1 File Offset: 0x001167E1
		public override XmlNameSyntax Name
		{
			get
			{
				return base.GetRedAtZero<XmlNameSyntax>(ref this.name);
			}
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06004678 RID: 18040 RVA: 0x001185EF File Offset: 0x001167EF
		public override SyntaxToken EqualsToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlNameAttributeSyntax)base.Green).equalsToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06004679 RID: 18041 RVA: 0x00118615 File Offset: 0x00116815
		public override SyntaxToken StartQuoteToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlNameAttributeSyntax)base.Green).startQuoteToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x0600467A RID: 18042 RVA: 0x0011863B File Offset: 0x0011683B
		public IdentifierNameSyntax Identifier
		{
			get
			{
				return base.GetRed<IdentifierNameSyntax>(ref this.identifier, 3);
			}
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x0600467B RID: 18043 RVA: 0x0011864A File Offset: 0x0011684A
		public override SyntaxToken EndQuoteToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlNameAttributeSyntax)base.Green).endQuoteToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x00118670 File Offset: 0x00116870
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
					syntaxNode = base.GetRed<IdentifierNameSyntax>(ref this.identifier, 3);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<XmlNameSyntax>(ref this.name);
			}
			return syntaxNode;
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x001186A8 File Offset: 0x001168A8
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
					syntaxNode = this.identifier;
				}
			}
			else
			{
				syntaxNode = this.name;
			}
			return syntaxNode;
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x001186D3 File Offset: 0x001168D3
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlNameAttribute(this);
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x001186DC File Offset: 0x001168DC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlNameAttribute(this);
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x001186E8 File Offset: 0x001168E8
		public XmlNameAttributeSyntax Update(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, IdentifierNameSyntax identifier, SyntaxToken endQuoteToken)
		{
			if (name == this.Name && !(equalsToken != this.EqualsToken) && !(startQuoteToken != this.StartQuoteToken) && identifier == this.Identifier && !(endQuoteToken != this.EndQuoteToken))
			{
				return this;
			}
			XmlNameAttributeSyntax xmlNameAttributeSyntax = SyntaxFactory.XmlNameAttribute(name, equalsToken, startQuoteToken, identifier, endQuoteToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return xmlNameAttributeSyntax;
			}
			return xmlNameAttributeSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x00118759 File Offset: 0x00116959
		internal override XmlAttributeSyntax WithNameCore(XmlNameSyntax name)
		{
			return this.WithName(name);
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x00118762 File Offset: 0x00116962
		public new XmlNameAttributeSyntax WithName(XmlNameSyntax name)
		{
			return this.Update(name, this.EqualsToken, this.StartQuoteToken, this.Identifier, this.EndQuoteToken);
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x00118783 File Offset: 0x00116983
		internal override XmlAttributeSyntax WithEqualsTokenCore(SyntaxToken equalsToken)
		{
			return this.WithEqualsToken(equalsToken);
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x0011878C File Offset: 0x0011698C
		public new XmlNameAttributeSyntax WithEqualsToken(SyntaxToken equalsToken)
		{
			return this.Update(this.Name, equalsToken, this.StartQuoteToken, this.Identifier, this.EndQuoteToken);
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x001187AD File Offset: 0x001169AD
		internal override XmlAttributeSyntax WithStartQuoteTokenCore(SyntaxToken startQuoteToken)
		{
			return this.WithStartQuoteToken(startQuoteToken);
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x001187B6 File Offset: 0x001169B6
		public new XmlNameAttributeSyntax WithStartQuoteToken(SyntaxToken startQuoteToken)
		{
			return this.Update(this.Name, this.EqualsToken, startQuoteToken, this.Identifier, this.EndQuoteToken);
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x001187D7 File Offset: 0x001169D7
		public XmlNameAttributeSyntax WithIdentifier(IdentifierNameSyntax identifier)
		{
			return this.Update(this.Name, this.EqualsToken, this.StartQuoteToken, identifier, this.EndQuoteToken);
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x001187F8 File Offset: 0x001169F8
		internal override XmlAttributeSyntax WithEndQuoteTokenCore(SyntaxToken endQuoteToken)
		{
			return this.WithEndQuoteToken(endQuoteToken);
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x00118801 File Offset: 0x00116A01
		public new XmlNameAttributeSyntax WithEndQuoteToken(SyntaxToken endQuoteToken)
		{
			return this.Update(this.Name, this.EqualsToken, this.StartQuoteToken, this.Identifier, endQuoteToken);
		}

		// Token: 0x04001527 RID: 5415
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private XmlNameSyntax name;

		// Token: 0x04001528 RID: 5416
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private IdentifierNameSyntax identifier;
	}
}
