using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000336 RID: 822
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class XmlElementStartTagSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004602 RID: 17922 RVA: 0x00117A62 File Offset: 0x00115C62
		internal XmlElementStartTagSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06004603 RID: 17923 RVA: 0x00117A6D File Offset: 0x00115C6D
		public SyntaxToken LessThanToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlElementStartTagSyntax)base.Green).lessThanToken, base.Position, 0);
			}
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06004604 RID: 17924 RVA: 0x00117A8C File Offset: 0x00115C8C
		public XmlNameSyntax Name
		{
			get
			{
				return base.GetRed<XmlNameSyntax>(ref this.name, 1);
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06004605 RID: 17925 RVA: 0x00117A9B File Offset: 0x00115C9B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<XmlAttributeSyntax> Attributes
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<XmlAttributeSyntax>(base.GetRed(ref this.attributes, 2));
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06004606 RID: 17926 RVA: 0x00117AAF File Offset: 0x00115CAF
		public SyntaxToken GreaterThanToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlElementStartTagSyntax)base.Green).greaterThanToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x00117AD8 File Offset: 0x00115CD8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 1)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed(ref this.attributes, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRed<XmlNameSyntax>(ref this.name, 1);
			}
			return syntaxNode;
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x00117B14 File Offset: 0x00115D14
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 1)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.attributes;
				}
			}
			else
			{
				syntaxNode = this.name;
			}
			return syntaxNode;
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x00117B40 File Offset: 0x00115D40
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlElementStartTag(this);
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x00117B49 File Offset: 0x00115D49
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlElementStartTag(this);
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x00117B54 File Offset: 0x00115D54
		public XmlElementStartTagSyntax Update(SyntaxToken lessThanToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlAttributeSyntax> attributes, SyntaxToken greaterThanToken)
		{
			if (!(lessThanToken != this.LessThanToken) && name == this.Name && !(attributes != this.Attributes) && !(greaterThanToken != this.GreaterThanToken))
			{
				return this;
			}
			XmlElementStartTagSyntax xmlElementStartTagSyntax = SyntaxFactory.XmlElementStartTag(lessThanToken, name, attributes, greaterThanToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return xmlElementStartTagSyntax;
			}
			return xmlElementStartTagSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x00117BB9 File Offset: 0x00115DB9
		public XmlElementStartTagSyntax WithLessThanToken(SyntaxToken lessThanToken)
		{
			return this.Update(lessThanToken, this.Name, this.Attributes, this.GreaterThanToken);
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x00117BD4 File Offset: 0x00115DD4
		public XmlElementStartTagSyntax WithName(XmlNameSyntax name)
		{
			return this.Update(this.LessThanToken, name, this.Attributes, this.GreaterThanToken);
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x00117BEF File Offset: 0x00115DEF
		public XmlElementStartTagSyntax WithAttributes([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlAttributeSyntax> attributes)
		{
			return this.Update(this.LessThanToken, this.Name, attributes, this.GreaterThanToken);
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x00117C0A File Offset: 0x00115E0A
		public XmlElementStartTagSyntax WithGreaterThanToken(SyntaxToken greaterThanToken)
		{
			return this.Update(this.LessThanToken, this.Name, this.Attributes, greaterThanToken);
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x00117C28 File Offset: 0x00115E28
		public XmlElementStartTagSyntax AddAttributes(params XmlAttributeSyntax[] items)
		{
			return this.WithAttributes(this.Attributes.AddRange(items));
		}

		// Token: 0x0400151E RID: 5406
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private XmlNameSyntax name;

		// Token: 0x0400151F RID: 5407
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributes;
	}
}
