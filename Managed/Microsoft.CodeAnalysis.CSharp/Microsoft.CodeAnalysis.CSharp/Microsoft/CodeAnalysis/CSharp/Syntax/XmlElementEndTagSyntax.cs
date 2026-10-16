using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000337 RID: 823
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class XmlElementEndTagSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004611 RID: 17937 RVA: 0x00117C4A File Offset: 0x00115E4A
		internal XmlElementEndTagSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06004612 RID: 17938 RVA: 0x00117C55 File Offset: 0x00115E55
		public SyntaxToken LessThanSlashToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlElementEndTagSyntax)base.Green).lessThanSlashToken, base.Position, 0);
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06004613 RID: 17939 RVA: 0x00117C74 File Offset: 0x00115E74
		public XmlNameSyntax Name
		{
			get
			{
				return base.GetRed<XmlNameSyntax>(ref this.name, 1);
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06004614 RID: 17940 RVA: 0x00117C83 File Offset: 0x00115E83
		public SyntaxToken GreaterThanToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlElementEndTagSyntax)base.Green).greaterThanToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x00117CA9 File Offset: 0x00115EA9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<XmlNameSyntax>(ref this.name, 1);
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x00117CBE File Offset: 0x00115EBE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.name;
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x00117CCC File Offset: 0x00115ECC
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlElementEndTag(this);
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x00117CD5 File Offset: 0x00115ED5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlElementEndTag(this);
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x00117CE0 File Offset: 0x00115EE0
		public XmlElementEndTagSyntax Update(SyntaxToken lessThanSlashToken, XmlNameSyntax name, SyntaxToken greaterThanToken)
		{
			if (!(lessThanSlashToken != this.LessThanSlashToken) && name == this.Name && !(greaterThanToken != this.GreaterThanToken))
			{
				return this;
			}
			XmlElementEndTagSyntax xmlElementEndTagSyntax = SyntaxFactory.XmlElementEndTag(lessThanSlashToken, name, greaterThanToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return xmlElementEndTagSyntax;
			}
			return xmlElementEndTagSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x00117D34 File Offset: 0x00115F34
		public XmlElementEndTagSyntax WithLessThanSlashToken(SyntaxToken lessThanSlashToken)
		{
			return this.Update(lessThanSlashToken, this.Name, this.GreaterThanToken);
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x00117D49 File Offset: 0x00115F49
		public XmlElementEndTagSyntax WithName(XmlNameSyntax name)
		{
			return this.Update(this.LessThanSlashToken, name, this.GreaterThanToken);
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x00117D5E File Offset: 0x00115F5E
		public XmlElementEndTagSyntax WithGreaterThanToken(SyntaxToken greaterThanToken)
		{
			return this.Update(this.LessThanSlashToken, this.Name, greaterThanToken);
		}

		// Token: 0x04001520 RID: 5408
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private XmlNameSyntax name;
	}
}
