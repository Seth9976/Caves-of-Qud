using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000307 RID: 775
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class AttributeListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004313 RID: 17171 RVA: 0x00111EF5 File Offset: 0x001100F5
		internal AttributeListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06004314 RID: 17172 RVA: 0x00111F00 File Offset: 0x00110100
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((AttributeListSyntax)base.Green).openBracketToken, base.Position, 0);
			}
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06004315 RID: 17173 RVA: 0x00111F1F File Offset: 0x0011011F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public AttributeTargetSpecifierSyntax Target
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<AttributeTargetSpecifierSyntax>(ref this.target, 1);
			}
		}

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x00111F30 File Offset: 0x00110130
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<AttributeSyntax> Attributes
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.attributes, 2);
				if (red == null)
				{
					return default(SeparatedSyntaxList<AttributeSyntax>);
				}
				return new SeparatedSyntaxList<AttributeSyntax>(red, base.GetChildIndex(2));
			}
		}

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x06004317 RID: 17175 RVA: 0x00111F65 File Offset: 0x00110165
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((AttributeListSyntax)base.Green).closeBracketToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x00111F8C File Offset: 0x0011018C
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
				syntaxNode = base.GetRed<AttributeTargetSpecifierSyntax>(ref this.target, 1);
			}
			return syntaxNode;
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x00111FC8 File Offset: 0x001101C8
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
				syntaxNode = this.target;
			}
			return syntaxNode;
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x00111FF4 File Offset: 0x001101F4
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAttributeList(this);
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x00111FFD File Offset: 0x001101FD
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAttributeList(this);
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x00112008 File Offset: 0x00110208
		public AttributeListSyntax Update(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeTargetSpecifierSyntax target, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AttributeSyntax> attributes, SyntaxToken closeBracketToken)
		{
			if (!(openBracketToken != this.OpenBracketToken) && target == this.Target && !(attributes != this.Attributes) && !(closeBracketToken != this.CloseBracketToken))
			{
				return this;
			}
			AttributeListSyntax attributeListSyntax = SyntaxFactory.AttributeList(openBracketToken, target, attributes, closeBracketToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return attributeListSyntax;
			}
			return attributeListSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x0011206D File Offset: 0x0011026D
		public AttributeListSyntax WithOpenBracketToken(SyntaxToken openBracketToken)
		{
			return this.Update(openBracketToken, this.Target, this.Attributes, this.CloseBracketToken);
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x00112088 File Offset: 0x00110288
		public AttributeListSyntax WithTarget([global::System.Runtime.CompilerServices.Nullable(2)] AttributeTargetSpecifierSyntax target)
		{
			return this.Update(this.OpenBracketToken, target, this.Attributes, this.CloseBracketToken);
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x001120A3 File Offset: 0x001102A3
		public AttributeListSyntax WithAttributes([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AttributeSyntax> attributes)
		{
			return this.Update(this.OpenBracketToken, this.Target, attributes, this.CloseBracketToken);
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x001120BE File Offset: 0x001102BE
		public AttributeListSyntax WithCloseBracketToken(SyntaxToken closeBracketToken)
		{
			return this.Update(this.OpenBracketToken, this.Target, this.Attributes, closeBracketToken);
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x001120DC File Offset: 0x001102DC
		public AttributeListSyntax AddAttributes(params AttributeSyntax[] items)
		{
			return this.WithAttributes(this.Attributes.AddRange(items));
		}

		// Token: 0x040014D6 RID: 5334
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private AttributeTargetSpecifierSyntax target;

		// Token: 0x040014D7 RID: 5335
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributes;
	}
}
