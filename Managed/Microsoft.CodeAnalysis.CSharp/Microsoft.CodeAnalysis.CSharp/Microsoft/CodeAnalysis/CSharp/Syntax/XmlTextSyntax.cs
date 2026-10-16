using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200033F RID: 831
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class XmlTextSyntax : XmlNodeSyntax
	{
		// Token: 0x0600468A RID: 18058 RVA: 0x00118822 File Offset: 0x00116A22
		internal XmlTextSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x0600468B RID: 18059 RVA: 0x00118830 File Offset: 0x00116A30
		public SyntaxTokenList TextTokens
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(0);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, base.Position, 0);
			}
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x00118865 File Offset: 0x00116A65
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x00118868 File Offset: 0x00116A68
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x0011886B File Offset: 0x00116A6B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlText(this);
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x00118874 File Offset: 0x00116A74
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlText(this);
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x00118880 File Offset: 0x00116A80
		public XmlTextSyntax Update(SyntaxTokenList textTokens)
		{
			if (!(textTokens != this.TextTokens))
			{
				return this;
			}
			XmlTextSyntax xmlTextSyntax = SyntaxFactory.XmlText(textTokens);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return xmlTextSyntax;
			}
			return xmlTextSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x001188BB File Offset: 0x00116ABB
		public XmlTextSyntax WithTextTokens(SyntaxTokenList textTokens)
		{
			return this.Update(textTokens);
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x001188C4 File Offset: 0x00116AC4
		public XmlTextSyntax AddTextTokens(params SyntaxToken[] items)
		{
			return this.WithTextTokens(this.TextTokens.AddRange(items));
		}
	}
}
