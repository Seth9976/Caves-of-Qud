using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200033A RID: 826
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class XmlPrefixSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004636 RID: 17974 RVA: 0x0011802F File Offset: 0x0011622F
		internal XmlPrefixSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06004637 RID: 17975 RVA: 0x0011803A File Offset: 0x0011623A
		public SyntaxToken Prefix
		{
			get
			{
				return new SyntaxToken(this, ((XmlPrefixSyntax)base.Green).prefix, base.Position, 0);
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06004638 RID: 17976 RVA: 0x00118059 File Offset: 0x00116259
		public SyntaxToken ColonToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlPrefixSyntax)base.Green).colonToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x06004639 RID: 17977 RVA: 0x0011807F File Offset: 0x0011627F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x0600463A RID: 17978 RVA: 0x00118082 File Offset: 0x00116282
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x0600463B RID: 17979 RVA: 0x00118085 File Offset: 0x00116285
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlPrefix(this);
		}

		// Token: 0x0600463C RID: 17980 RVA: 0x0011808E File Offset: 0x0011628E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlPrefix(this);
		}

		// Token: 0x0600463D RID: 17981 RVA: 0x00118098 File Offset: 0x00116298
		public XmlPrefixSyntax Update(SyntaxToken prefix, SyntaxToken colonToken)
		{
			if (!(prefix != this.Prefix) && !(colonToken != this.ColonToken))
			{
				return this;
			}
			XmlPrefixSyntax xmlPrefixSyntax = SyntaxFactory.XmlPrefix(prefix, colonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return xmlPrefixSyntax;
			}
			return xmlPrefixSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600463E RID: 17982 RVA: 0x001180E2 File Offset: 0x001162E2
		public XmlPrefixSyntax WithPrefix(SyntaxToken prefix)
		{
			return this.Update(prefix, this.ColonToken);
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x001180F1 File Offset: 0x001162F1
		public XmlPrefixSyntax WithColonToken(SyntaxToken colonToken)
		{
			return this.Update(this.Prefix, colonToken);
		}
	}
}
