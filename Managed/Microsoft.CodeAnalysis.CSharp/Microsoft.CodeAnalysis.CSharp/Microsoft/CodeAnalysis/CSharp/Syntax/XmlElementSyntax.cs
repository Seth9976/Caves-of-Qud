using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000335 RID: 821
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class XmlElementSyntax : XmlNodeSyntax
	{
		// Token: 0x060045F4 RID: 17908 RVA: 0x00117895 File Offset: 0x00115A95
		internal XmlElementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x001178A0 File Offset: 0x00115AA0
		public XmlElementStartTagSyntax StartTag
		{
			get
			{
				return base.GetRedAtZero<XmlElementStartTagSyntax>(ref this.startTag);
			}
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x060045F6 RID: 17910 RVA: 0x001178AE File Offset: 0x00115AAE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<XmlNodeSyntax> Content
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<XmlNodeSyntax>(base.GetRed(ref this.content, 1));
			}
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x001178C2 File Offset: 0x00115AC2
		public XmlElementEndTagSyntax EndTag
		{
			get
			{
				return base.GetRed<XmlElementEndTagSyntax>(ref this.endTag, 2);
			}
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x001178D4 File Offset: 0x00115AD4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 0:
				syntaxNode = base.GetRedAtZero<XmlElementStartTagSyntax>(ref this.startTag);
				break;
			case 1:
				syntaxNode = base.GetRed(ref this.content, 1);
				break;
			case 2:
				syntaxNode = base.GetRed<XmlElementEndTagSyntax>(ref this.endTag, 2);
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x00117928 File Offset: 0x00115B28
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 0:
				syntaxNode = this.startTag;
				break;
			case 1:
				syntaxNode = this.content;
				break;
			case 2:
				syntaxNode = this.endTag;
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x00117967 File Offset: 0x00115B67
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlElement(this);
		}

		// Token: 0x060045FB RID: 17915 RVA: 0x00117970 File Offset: 0x00115B70
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlElement(this);
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x0011797C File Offset: 0x00115B7C
		public XmlElementSyntax Update(XmlElementStartTagSyntax startTag, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlNodeSyntax> content, XmlElementEndTagSyntax endTag)
		{
			if (startTag == this.StartTag && !(content != this.Content) && endTag == this.EndTag)
			{
				return this;
			}
			XmlElementSyntax xmlElementSyntax = SyntaxFactory.XmlElement(startTag, content, endTag);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return xmlElementSyntax;
			}
			return xmlElementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x001179CB File Offset: 0x00115BCB
		public XmlElementSyntax WithStartTag(XmlElementStartTagSyntax startTag)
		{
			return this.Update(startTag, this.Content, this.EndTag);
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x001179E0 File Offset: 0x00115BE0
		public XmlElementSyntax WithContent([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlNodeSyntax> content)
		{
			return this.Update(this.StartTag, content, this.EndTag);
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x001179F5 File Offset: 0x00115BF5
		public XmlElementSyntax WithEndTag(XmlElementEndTagSyntax endTag)
		{
			return this.Update(this.StartTag, this.Content, endTag);
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x00117A0C File Offset: 0x00115C0C
		public XmlElementSyntax AddStartTagAttributes(params XmlAttributeSyntax[] items)
		{
			return this.WithStartTag(this.StartTag.WithAttributes(this.StartTag.Attributes.AddRange(items)));
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x00117A40 File Offset: 0x00115C40
		public XmlElementSyntax AddContent(params XmlNodeSyntax[] items)
		{
			return this.WithContent(this.Content.AddRange(items));
		}

		// Token: 0x0400151B RID: 5403
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private XmlElementStartTagSyntax startTag;

		// Token: 0x0400151C RID: 5404
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode content;

		// Token: 0x0400151D RID: 5405
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private XmlElementEndTagSyntax endTag;
	}
}
