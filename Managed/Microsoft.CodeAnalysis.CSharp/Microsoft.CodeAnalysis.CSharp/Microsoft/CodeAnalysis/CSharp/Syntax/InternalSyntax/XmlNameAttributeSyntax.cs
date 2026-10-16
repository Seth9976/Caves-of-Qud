using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000467 RID: 1127
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class XmlNameAttributeSyntax : XmlAttributeSyntax
	{
		// Token: 0x06005EB9 RID: 24249 RVA: 0x0016F7A8 File Offset: 0x0016D9A8
		internal XmlNameAttributeSyntax(SyntaxKind kind, XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, IdentifierNameSyntax identifier, SyntaxToken endQuoteToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
			base.AdjustFlagsAndWidth(startQuoteToken);
			this.startQuoteToken = startQuoteToken;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(endQuoteToken);
			this.endQuoteToken = endQuoteToken;
		}

		// Token: 0x06005EBA RID: 24250 RVA: 0x0016F814 File Offset: 0x0016DA14
		internal XmlNameAttributeSyntax(SyntaxKind kind, XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, IdentifierNameSyntax identifier, SyntaxToken endQuoteToken, SyntaxFactoryContext context)
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
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(endQuoteToken);
			this.endQuoteToken = endQuoteToken;
		}

		// Token: 0x06005EBB RID: 24251 RVA: 0x0016F884 File Offset: 0x0016DA84
		internal XmlNameAttributeSyntax(SyntaxKind kind, XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, IdentifierNameSyntax identifier, SyntaxToken endQuoteToken)
			: base(kind)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
			base.AdjustFlagsAndWidth(startQuoteToken);
			this.startQuoteToken = startQuoteToken;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(endQuoteToken);
			this.endQuoteToken = endQuoteToken;
		}

		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06005EBC RID: 24252 RVA: 0x0016F8EB File Offset: 0x0016DAEB
		public override XmlNameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x06005EBD RID: 24253 RVA: 0x0016F8F3 File Offset: 0x0016DAF3
		public override SyntaxToken EqualsToken
		{
			get
			{
				return this.equalsToken;
			}
		}

		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x06005EBE RID: 24254 RVA: 0x0016F8FB File Offset: 0x0016DAFB
		public override SyntaxToken StartQuoteToken
		{
			get
			{
				return this.startQuoteToken;
			}
		}

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x06005EBF RID: 24255 RVA: 0x0016F903 File Offset: 0x0016DB03
		public IdentifierNameSyntax Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x06005EC0 RID: 24256 RVA: 0x0016F90B File Offset: 0x0016DB0B
		public override SyntaxToken EndQuoteToken
		{
			get
			{
				return this.endQuoteToken;
			}
		}

		// Token: 0x06005EC1 RID: 24257 RVA: 0x0016F914 File Offset: 0x0016DB14
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
				greenNode = this.identifier;
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

		// Token: 0x06005EC2 RID: 24258 RVA: 0x0016F96D File Offset: 0x0016DB6D
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new XmlNameAttributeSyntax(this, parent, position);
		}

		// Token: 0x06005EC3 RID: 24259 RVA: 0x0016F977 File Offset: 0x0016DB77
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlNameAttribute(this);
		}

		// Token: 0x06005EC4 RID: 24260 RVA: 0x0016F980 File Offset: 0x0016DB80
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlNameAttribute(this);
		}

		// Token: 0x06005EC5 RID: 24261 RVA: 0x0016F98C File Offset: 0x0016DB8C
		public XmlNameAttributeSyntax Update(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, IdentifierNameSyntax identifier, SyntaxToken endQuoteToken)
		{
			if (name != this.Name || equalsToken != this.EqualsToken || startQuoteToken != this.StartQuoteToken || identifier != this.Identifier || endQuoteToken != this.EndQuoteToken)
			{
				XmlNameAttributeSyntax xmlNameAttributeSyntax = SyntaxFactory.XmlNameAttribute(name, equalsToken, startQuoteToken, identifier, endQuoteToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					xmlNameAttributeSyntax = xmlNameAttributeSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					xmlNameAttributeSyntax = xmlNameAttributeSyntax.WithAnnotationsGreen(annotations);
				}
				return xmlNameAttributeSyntax;
			}
			return this;
		}

		// Token: 0x06005EC6 RID: 24262 RVA: 0x0016FA04 File Offset: 0x0016DC04
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new XmlNameAttributeSyntax(base.Kind, this.name, this.equalsToken, this.startQuoteToken, this.identifier, this.endQuoteToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005EC7 RID: 24263 RVA: 0x0016FA36 File Offset: 0x0016DC36
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new XmlNameAttributeSyntax(base.Kind, this.name, this.equalsToken, this.startQuoteToken, this.identifier, this.endQuoteToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005EC8 RID: 24264 RVA: 0x0016FA68 File Offset: 0x0016DC68
		internal XmlNameAttributeSyntax(ObjectReader reader)
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
			IdentifierNameSyntax identifierNameSyntax = (IdentifierNameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(identifierNameSyntax);
			this.identifier = identifierNameSyntax;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.endQuoteToken = syntaxToken3;
		}

		// Token: 0x06005EC9 RID: 24265 RVA: 0x0016FB08 File Offset: 0x0016DD08
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.name);
			writer.WriteValue(this.equalsToken);
			writer.WriteValue(this.startQuoteToken);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.endQuoteToken);
		}

		// Token: 0x06005ECA RID: 24266 RVA: 0x0016FB58 File Offset: 0x0016DD58
		static XmlNameAttributeSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(XmlNameAttributeSyntax), (ObjectReader r) => new XmlNameAttributeSyntax(r));
		}

		// Token: 0x040018DC RID: 6364
		internal readonly XmlNameSyntax name;

		// Token: 0x040018DD RID: 6365
		internal readonly SyntaxToken equalsToken;

		// Token: 0x040018DE RID: 6366
		internal readonly SyntaxToken startQuoteToken;

		// Token: 0x040018DF RID: 6367
		internal readonly IdentifierNameSyntax identifier;

		// Token: 0x040018E0 RID: 6368
		internal readonly SyntaxToken endQuoteToken;
	}
}
