using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000338 RID: 824
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class XmlEmptyElementSyntax : XmlNodeSyntax
	{
		// Token: 0x0600461D RID: 17949 RVA: 0x00117D73 File Offset: 0x00115F73
		internal XmlEmptyElementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x0600461E RID: 17950 RVA: 0x00117D7E File Offset: 0x00115F7E
		public SyntaxToken LessThanToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlEmptyElementSyntax)base.Green).lessThanToken, base.Position, 0);
			}
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x0600461F RID: 17951 RVA: 0x00117D9D File Offset: 0x00115F9D
		public XmlNameSyntax Name
		{
			get
			{
				return base.GetRed<XmlNameSyntax>(ref this.name, 1);
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06004620 RID: 17952 RVA: 0x00117DAC File Offset: 0x00115FAC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<XmlAttributeSyntax> Attributes
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<XmlAttributeSyntax>(base.GetRed(ref this.attributes, 2));
			}
		}

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06004621 RID: 17953 RVA: 0x00117DC0 File Offset: 0x00115FC0
		public SyntaxToken SlashGreaterThanToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlEmptyElementSyntax)base.Green).slashGreaterThanToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x00117DE8 File Offset: 0x00115FE8
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

		// Token: 0x06004623 RID: 17955 RVA: 0x00117E24 File Offset: 0x00116024
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

		// Token: 0x06004624 RID: 17956 RVA: 0x00117E50 File Offset: 0x00116050
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlEmptyElement(this);
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x00117E59 File Offset: 0x00116059
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlEmptyElement(this);
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x00117E64 File Offset: 0x00116064
		public XmlEmptyElementSyntax Update(SyntaxToken lessThanToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlAttributeSyntax> attributes, SyntaxToken slashGreaterThanToken)
		{
			if (!(lessThanToken != this.LessThanToken) && name == this.Name && !(attributes != this.Attributes) && !(slashGreaterThanToken != this.SlashGreaterThanToken))
			{
				return this;
			}
			XmlEmptyElementSyntax xmlEmptyElementSyntax = SyntaxFactory.XmlEmptyElement(lessThanToken, name, attributes, slashGreaterThanToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return xmlEmptyElementSyntax;
			}
			return xmlEmptyElementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x00117EC9 File Offset: 0x001160C9
		public XmlEmptyElementSyntax WithLessThanToken(SyntaxToken lessThanToken)
		{
			return this.Update(lessThanToken, this.Name, this.Attributes, this.SlashGreaterThanToken);
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x00117EE4 File Offset: 0x001160E4
		public XmlEmptyElementSyntax WithName(XmlNameSyntax name)
		{
			return this.Update(this.LessThanToken, name, this.Attributes, this.SlashGreaterThanToken);
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x00117EFF File Offset: 0x001160FF
		public XmlEmptyElementSyntax WithAttributes([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlAttributeSyntax> attributes)
		{
			return this.Update(this.LessThanToken, this.Name, attributes, this.SlashGreaterThanToken);
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x00117F1A File Offset: 0x0011611A
		public XmlEmptyElementSyntax WithSlashGreaterThanToken(SyntaxToken slashGreaterThanToken)
		{
			return this.Update(this.LessThanToken, this.Name, this.Attributes, slashGreaterThanToken);
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x00117F38 File Offset: 0x00116138
		public XmlEmptyElementSyntax AddAttributes(params XmlAttributeSyntax[] items)
		{
			return this.WithAttributes(this.Attributes.AddRange(items));
		}

		// Token: 0x04001521 RID: 5409
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private XmlNameSyntax name;

		// Token: 0x04001522 RID: 5410
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributes;
	}
}
