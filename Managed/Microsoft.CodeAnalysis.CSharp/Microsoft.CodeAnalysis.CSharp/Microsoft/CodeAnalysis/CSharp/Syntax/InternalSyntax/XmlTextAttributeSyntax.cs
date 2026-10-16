using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000465 RID: 1125
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class XmlTextAttributeSyntax : XmlAttributeSyntax
	{
		// Token: 0x06005E95 RID: 24213 RVA: 0x0016EFE4 File Offset: 0x0016D1E4
		internal XmlTextAttributeSyntax(SyntaxKind kind, XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode textTokens, SyntaxToken endQuoteToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
			base.AdjustFlagsAndWidth(startQuoteToken);
			this.startQuoteToken = startQuoteToken;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
			base.AdjustFlagsAndWidth(endQuoteToken);
			this.endQuoteToken = endQuoteToken;
		}

		// Token: 0x06005E96 RID: 24214 RVA: 0x0016F054 File Offset: 0x0016D254
		internal XmlTextAttributeSyntax(SyntaxKind kind, XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode textTokens, SyntaxToken endQuoteToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
			base.AdjustFlagsAndWidth(startQuoteToken);
			this.startQuoteToken = startQuoteToken;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
			base.AdjustFlagsAndWidth(endQuoteToken);
			this.endQuoteToken = endQuoteToken;
		}

		// Token: 0x06005E97 RID: 24215 RVA: 0x0016F0C8 File Offset: 0x0016D2C8
		internal XmlTextAttributeSyntax(SyntaxKind kind, XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode textTokens, SyntaxToken endQuoteToken)
			: base(kind)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
			base.AdjustFlagsAndWidth(startQuoteToken);
			this.startQuoteToken = startQuoteToken;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
			base.AdjustFlagsAndWidth(endQuoteToken);
			this.endQuoteToken = endQuoteToken;
		}

		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x06005E98 RID: 24216 RVA: 0x0016F133 File Offset: 0x0016D333
		public override XmlNameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x06005E99 RID: 24217 RVA: 0x0016F13B File Offset: 0x0016D33B
		public override SyntaxToken EqualsToken
		{
			get
			{
				return this.equalsToken;
			}
		}

		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x06005E9A RID: 24218 RVA: 0x0016F143 File Offset: 0x0016D343
		public override SyntaxToken StartQuoteToken
		{
			get
			{
				return this.startQuoteToken;
			}
		}

		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06005E9B RID: 24219 RVA: 0x0016F14B File Offset: 0x0016D34B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<SyntaxToken> TextTokens
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.textTokens);
			}
		}

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06005E9C RID: 24220 RVA: 0x0016F158 File Offset: 0x0016D358
		public override SyntaxToken EndQuoteToken
		{
			get
			{
				return this.endQuoteToken;
			}
		}

		// Token: 0x06005E9D RID: 24221 RVA: 0x0016F160 File Offset: 0x0016D360
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.name;
				break;
			case 1:
				greenNode = this.equalsToken;
				break;
			case 2:
				greenNode = this.startQuoteToken;
				break;
			case 3:
				greenNode = this.textTokens;
				break;
			case 4:
				greenNode = this.endQuoteToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005E9E RID: 24222 RVA: 0x0016F1B9 File Offset: 0x0016D3B9
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new XmlTextAttributeSyntax(this, parent, position);
		}

		// Token: 0x06005E9F RID: 24223 RVA: 0x0016F1C3 File Offset: 0x0016D3C3
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlTextAttribute(this);
		}

		// Token: 0x06005EA0 RID: 24224 RVA: 0x0016F1CC File Offset: 0x0016D3CC
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlTextAttribute(this);
		}

		// Token: 0x06005EA1 RID: 24225 RVA: 0x0016F1D8 File Offset: 0x0016D3D8
		public XmlTextAttributeSyntax Update(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens, SyntaxToken endQuoteToken)
		{
			if (name != this.Name || equalsToken != this.EqualsToken || startQuoteToken != this.StartQuoteToken || textTokens != this.TextTokens || endQuoteToken != this.EndQuoteToken)
			{
				XmlTextAttributeSyntax xmlTextAttributeSyntax = SyntaxFactory.XmlTextAttribute(name, equalsToken, startQuoteToken, textTokens, endQuoteToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					xmlTextAttributeSyntax = xmlTextAttributeSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					xmlTextAttributeSyntax = xmlTextAttributeSyntax.WithAnnotationsGreen(annotations);
				}
				return xmlTextAttributeSyntax;
			}
			return this;
		}

		// Token: 0x06005EA2 RID: 24226 RVA: 0x0016F255 File Offset: 0x0016D455
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new XmlTextAttributeSyntax(base.Kind, this.name, this.equalsToken, this.startQuoteToken, this.textTokens, this.endQuoteToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005EA3 RID: 24227 RVA: 0x0016F287 File Offset: 0x0016D487
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new XmlTextAttributeSyntax(base.Kind, this.name, this.equalsToken, this.startQuoteToken, this.textTokens, this.endQuoteToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005EA4 RID: 24228 RVA: 0x0016F2BC File Offset: 0x0016D4BC
		internal XmlTextAttributeSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 5;
			XmlNameSyntax xmlNameSyntax = (XmlNameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(xmlNameSyntax);
			this.name = xmlNameSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.equalsToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.startQuoteToken = syntaxToken2;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.textTokens = greenNode;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.endQuoteToken = syntaxToken3;
		}

		// Token: 0x06005EA5 RID: 24229 RVA: 0x0016F360 File Offset: 0x0016D560
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.name);
			writer.WriteValue(this.equalsToken);
			writer.WriteValue(this.startQuoteToken);
			writer.WriteValue(this.textTokens);
			writer.WriteValue(this.endQuoteToken);
		}

		// Token: 0x06005EA6 RID: 24230 RVA: 0x0016F3B0 File Offset: 0x0016D5B0
		static XmlTextAttributeSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(XmlTextAttributeSyntax), (ObjectReader r) => new XmlTextAttributeSyntax(r));
		}

		// Token: 0x040018D2 RID: 6354
		internal readonly XmlNameSyntax name;

		// Token: 0x040018D3 RID: 6355
		internal readonly SyntaxToken equalsToken;

		// Token: 0x040018D4 RID: 6356
		internal readonly SyntaxToken startQuoteToken;

		// Token: 0x040018D5 RID: 6357
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode textTokens;

		// Token: 0x040018D6 RID: 6358
		internal readonly SyntaxToken endQuoteToken;
	}
}
