using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000466 RID: 1126
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class XmlCrefAttributeSyntax : XmlAttributeSyntax
	{
		// Token: 0x06005EA7 RID: 24231 RVA: 0x0016F3D4 File Offset: 0x0016D5D4
		internal XmlCrefAttributeSyntax(SyntaxKind kind, XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, CrefSyntax cref, SyntaxToken endQuoteToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
			base.AdjustFlagsAndWidth(startQuoteToken);
			this.startQuoteToken = startQuoteToken;
			base.AdjustFlagsAndWidth(cref);
			this.cref = cref;
			base.AdjustFlagsAndWidth(endQuoteToken);
			this.endQuoteToken = endQuoteToken;
		}

		// Token: 0x06005EA8 RID: 24232 RVA: 0x0016F440 File Offset: 0x0016D640
		internal XmlCrefAttributeSyntax(SyntaxKind kind, XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, CrefSyntax cref, SyntaxToken endQuoteToken, SyntaxFactoryContext context)
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
			base.AdjustFlagsAndWidth(cref);
			this.cref = cref;
			base.AdjustFlagsAndWidth(endQuoteToken);
			this.endQuoteToken = endQuoteToken;
		}

		// Token: 0x06005EA9 RID: 24233 RVA: 0x0016F4B0 File Offset: 0x0016D6B0
		internal XmlCrefAttributeSyntax(SyntaxKind kind, XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, CrefSyntax cref, SyntaxToken endQuoteToken)
			: base(kind)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
			base.AdjustFlagsAndWidth(startQuoteToken);
			this.startQuoteToken = startQuoteToken;
			base.AdjustFlagsAndWidth(cref);
			this.cref = cref;
			base.AdjustFlagsAndWidth(endQuoteToken);
			this.endQuoteToken = endQuoteToken;
		}

		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x06005EAA RID: 24234 RVA: 0x0016F517 File Offset: 0x0016D717
		public override XmlNameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x06005EAB RID: 24235 RVA: 0x0016F51F File Offset: 0x0016D71F
		public override SyntaxToken EqualsToken
		{
			get
			{
				return this.equalsToken;
			}
		}

		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x06005EAC RID: 24236 RVA: 0x0016F527 File Offset: 0x0016D727
		public override SyntaxToken StartQuoteToken
		{
			get
			{
				return this.startQuoteToken;
			}
		}

		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x06005EAD RID: 24237 RVA: 0x0016F52F File Offset: 0x0016D72F
		public CrefSyntax Cref
		{
			get
			{
				return this.cref;
			}
		}

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x06005EAE RID: 24238 RVA: 0x0016F537 File Offset: 0x0016D737
		public override SyntaxToken EndQuoteToken
		{
			get
			{
				return this.endQuoteToken;
			}
		}

		// Token: 0x06005EAF RID: 24239 RVA: 0x0016F540 File Offset: 0x0016D740
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
				greenNode = this.cref;
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

		// Token: 0x06005EB0 RID: 24240 RVA: 0x0016F599 File Offset: 0x0016D799
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new XmlCrefAttributeSyntax(this, parent, position);
		}

		// Token: 0x06005EB1 RID: 24241 RVA: 0x0016F5A3 File Offset: 0x0016D7A3
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlCrefAttribute(this);
		}

		// Token: 0x06005EB2 RID: 24242 RVA: 0x0016F5AC File Offset: 0x0016D7AC
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlCrefAttribute(this);
		}

		// Token: 0x06005EB3 RID: 24243 RVA: 0x0016F5B8 File Offset: 0x0016D7B8
		public XmlCrefAttributeSyntax Update(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, CrefSyntax cref, SyntaxToken endQuoteToken)
		{
			if (name != this.Name || equalsToken != this.EqualsToken || startQuoteToken != this.StartQuoteToken || cref != this.Cref || endQuoteToken != this.EndQuoteToken)
			{
				XmlCrefAttributeSyntax xmlCrefAttributeSyntax = SyntaxFactory.XmlCrefAttribute(name, equalsToken, startQuoteToken, cref, endQuoteToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					xmlCrefAttributeSyntax = xmlCrefAttributeSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					xmlCrefAttributeSyntax = xmlCrefAttributeSyntax.WithAnnotationsGreen(annotations);
				}
				return xmlCrefAttributeSyntax;
			}
			return this;
		}

		// Token: 0x06005EB4 RID: 24244 RVA: 0x0016F630 File Offset: 0x0016D830
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new XmlCrefAttributeSyntax(base.Kind, this.name, this.equalsToken, this.startQuoteToken, this.cref, this.endQuoteToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005EB5 RID: 24245 RVA: 0x0016F662 File Offset: 0x0016D862
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new XmlCrefAttributeSyntax(base.Kind, this.name, this.equalsToken, this.startQuoteToken, this.cref, this.endQuoteToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005EB6 RID: 24246 RVA: 0x0016F694 File Offset: 0x0016D894
		internal XmlCrefAttributeSyntax(ObjectReader reader)
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
			CrefSyntax crefSyntax = (CrefSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(crefSyntax);
			this.cref = crefSyntax;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.endQuoteToken = syntaxToken3;
		}

		// Token: 0x06005EB7 RID: 24247 RVA: 0x0016F734 File Offset: 0x0016D934
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.name);
			writer.WriteValue(this.equalsToken);
			writer.WriteValue(this.startQuoteToken);
			writer.WriteValue(this.cref);
			writer.WriteValue(this.endQuoteToken);
		}

		// Token: 0x06005EB8 RID: 24248 RVA: 0x0016F784 File Offset: 0x0016D984
		static XmlCrefAttributeSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(XmlCrefAttributeSyntax), (ObjectReader r) => new XmlCrefAttributeSyntax(r));
		}

		// Token: 0x040018D7 RID: 6359
		internal readonly XmlNameSyntax name;

		// Token: 0x040018D8 RID: 6360
		internal readonly SyntaxToken equalsToken;

		// Token: 0x040018D9 RID: 6361
		internal readonly SyntaxToken startQuoteToken;

		// Token: 0x040018DA RID: 6362
		internal readonly CrefSyntax cref;

		// Token: 0x040018DB RID: 6363
		internal readonly SyntaxToken endQuoteToken;
	}
}
