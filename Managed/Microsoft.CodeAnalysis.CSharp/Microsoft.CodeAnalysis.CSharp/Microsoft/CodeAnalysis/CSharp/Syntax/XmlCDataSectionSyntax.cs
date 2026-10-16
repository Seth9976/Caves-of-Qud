using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000340 RID: 832
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class XmlCDataSectionSyntax : XmlNodeSyntax
	{
		// Token: 0x06004693 RID: 18067 RVA: 0x001188E6 File Offset: 0x00116AE6
		internal XmlCDataSectionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06004694 RID: 18068 RVA: 0x001188F1 File Offset: 0x00116AF1
		public SyntaxToken StartCDataToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlCDataSectionSyntax)base.Green).startCDataToken, base.Position, 0);
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06004695 RID: 18069 RVA: 0x00118910 File Offset: 0x00116B10
		public SyntaxTokenList TextTokens
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(1);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06004696 RID: 18070 RVA: 0x0011894C File Offset: 0x00116B4C
		public SyntaxToken EndCDataToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlCDataSectionSyntax)base.Green).endCDataToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x00118972 File Offset: 0x00116B72
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x00118975 File Offset: 0x00116B75
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x00118978 File Offset: 0x00116B78
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlCDataSection(this);
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x00118981 File Offset: 0x00116B81
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlCDataSection(this);
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x0011898C File Offset: 0x00116B8C
		public XmlCDataSectionSyntax Update(SyntaxToken startCDataToken, SyntaxTokenList textTokens, SyntaxToken endCDataToken)
		{
			if (!(startCDataToken != this.StartCDataToken) && !(textTokens != this.TextTokens) && !(endCDataToken != this.EndCDataToken))
			{
				return this;
			}
			XmlCDataSectionSyntax xmlCDataSectionSyntax = SyntaxFactory.XmlCDataSection(startCDataToken, textTokens, endCDataToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return xmlCDataSectionSyntax;
			}
			return xmlCDataSectionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x001189E5 File Offset: 0x00116BE5
		public XmlCDataSectionSyntax WithStartCDataToken(SyntaxToken startCDataToken)
		{
			return this.Update(startCDataToken, this.TextTokens, this.EndCDataToken);
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x001189FA File Offset: 0x00116BFA
		public XmlCDataSectionSyntax WithTextTokens(SyntaxTokenList textTokens)
		{
			return this.Update(this.StartCDataToken, textTokens, this.EndCDataToken);
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x00118A0F File Offset: 0x00116C0F
		public XmlCDataSectionSyntax WithEndCDataToken(SyntaxToken endCDataToken)
		{
			return this.Update(this.StartCDataToken, this.TextTokens, endCDataToken);
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x00118A24 File Offset: 0x00116C24
		public XmlCDataSectionSyntax AddTextTokens(params SyntaxToken[] items)
		{
			return this.WithTextTokens(this.TextTokens.AddRange(items));
		}
	}
}
