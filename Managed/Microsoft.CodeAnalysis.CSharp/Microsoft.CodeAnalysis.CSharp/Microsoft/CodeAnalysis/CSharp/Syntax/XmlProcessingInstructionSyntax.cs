using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000341 RID: 833
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class XmlProcessingInstructionSyntax : XmlNodeSyntax
	{
		// Token: 0x060046A0 RID: 18080 RVA: 0x00118A46 File Offset: 0x00116C46
		internal XmlProcessingInstructionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x060046A1 RID: 18081 RVA: 0x00118A51 File Offset: 0x00116C51
		public SyntaxToken StartProcessingInstructionToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlProcessingInstructionSyntax)base.Green).startProcessingInstructionToken, base.Position, 0);
			}
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x060046A2 RID: 18082 RVA: 0x00118A70 File Offset: 0x00116C70
		public XmlNameSyntax Name
		{
			get
			{
				return base.GetRed<XmlNameSyntax>(ref this.name, 1);
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x060046A3 RID: 18083 RVA: 0x00118A80 File Offset: 0x00116C80
		public SyntaxTokenList TextTokens
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(2);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x060046A4 RID: 18084 RVA: 0x00118ABC File Offset: 0x00116CBC
		public SyntaxToken EndProcessingInstructionToken
		{
			get
			{
				return new SyntaxToken(this, ((XmlProcessingInstructionSyntax)base.Green).endProcessingInstructionToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x00118AE2 File Offset: 0x00116CE2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<XmlNameSyntax>(ref this.name, 1);
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x00118AF7 File Offset: 0x00116CF7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.name;
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x00118B05 File Offset: 0x00116D05
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlProcessingInstruction(this);
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x00118B0E File Offset: 0x00116D0E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlProcessingInstruction(this);
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x00118B18 File Offset: 0x00116D18
		public XmlProcessingInstructionSyntax Update(SyntaxToken startProcessingInstructionToken, XmlNameSyntax name, SyntaxTokenList textTokens, SyntaxToken endProcessingInstructionToken)
		{
			if (!(startProcessingInstructionToken != this.StartProcessingInstructionToken) && name == this.Name && !(textTokens != this.TextTokens) && !(endProcessingInstructionToken != this.EndProcessingInstructionToken))
			{
				return this;
			}
			XmlProcessingInstructionSyntax xmlProcessingInstructionSyntax = SyntaxFactory.XmlProcessingInstruction(startProcessingInstructionToken, name, textTokens, endProcessingInstructionToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return xmlProcessingInstructionSyntax;
			}
			return xmlProcessingInstructionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x00118B7D File Offset: 0x00116D7D
		public XmlProcessingInstructionSyntax WithStartProcessingInstructionToken(SyntaxToken startProcessingInstructionToken)
		{
			return this.Update(startProcessingInstructionToken, this.Name, this.TextTokens, this.EndProcessingInstructionToken);
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x00118B98 File Offset: 0x00116D98
		public XmlProcessingInstructionSyntax WithName(XmlNameSyntax name)
		{
			return this.Update(this.StartProcessingInstructionToken, name, this.TextTokens, this.EndProcessingInstructionToken);
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x00118BB3 File Offset: 0x00116DB3
		public XmlProcessingInstructionSyntax WithTextTokens(SyntaxTokenList textTokens)
		{
			return this.Update(this.StartProcessingInstructionToken, this.Name, textTokens, this.EndProcessingInstructionToken);
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x00118BCE File Offset: 0x00116DCE
		public XmlProcessingInstructionSyntax WithEndProcessingInstructionToken(SyntaxToken endProcessingInstructionToken)
		{
			return this.Update(this.StartProcessingInstructionToken, this.Name, this.TextTokens, endProcessingInstructionToken);
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x00118BEC File Offset: 0x00116DEC
		public XmlProcessingInstructionSyntax AddTextTokens(params SyntaxToken[] items)
		{
			return this.WithTextTokens(this.TextTokens.AddRange(items));
		}

		// Token: 0x04001529 RID: 5417
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private XmlNameSyntax name;
	}
}
