using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000339 RID: 825
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class XmlNameSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600462C RID: 17964 RVA: 0x00117F5A File Offset: 0x0011615A
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal XmlNameSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x0600462D RID: 17965 RVA: 0x00117F65 File Offset: 0x00116165
		public XmlPrefixSyntax Prefix
		{
			get
			{
				return base.GetRedAtZero<XmlPrefixSyntax>(ref this.prefix);
			}
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x0600462E RID: 17966 RVA: 0x00117F73 File Offset: 0x00116173
		public SyntaxToken LocalName
		{
			get
			{
				return new SyntaxToken(this, ((XmlNameSyntax)base.Green).localName, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x00117F99 File Offset: 0x00116199
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<XmlPrefixSyntax>(ref this.prefix);
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x00117FAC File Offset: 0x001161AC
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.prefix;
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x00117FB9 File Offset: 0x001161B9
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlName(this);
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x00117FC2 File Offset: 0x001161C2
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlName(this);
		}

		// Token: 0x06004633 RID: 17971 RVA: 0x00117FCC File Offset: 0x001161CC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public XmlNameSyntax Update([global::System.Runtime.CompilerServices.Nullable(2)] XmlPrefixSyntax prefix, SyntaxToken localName)
		{
			if (prefix == this.Prefix && !(localName != this.LocalName))
			{
				return this;
			}
			XmlNameSyntax xmlNameSyntax = SyntaxFactory.XmlName(prefix, localName);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return xmlNameSyntax;
			}
			return xmlNameSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x00118011 File Offset: 0x00116211
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public XmlNameSyntax WithPrefix([global::System.Runtime.CompilerServices.Nullable(2)] XmlPrefixSyntax prefix)
		{
			return this.Update(prefix, this.LocalName);
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x00118020 File Offset: 0x00116220
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public XmlNameSyntax WithLocalName(SyntaxToken localName)
		{
			return this.Update(this.Prefix, localName);
		}

		// Token: 0x04001523 RID: 5411
		private XmlPrefixSyntax prefix;
	}
}
