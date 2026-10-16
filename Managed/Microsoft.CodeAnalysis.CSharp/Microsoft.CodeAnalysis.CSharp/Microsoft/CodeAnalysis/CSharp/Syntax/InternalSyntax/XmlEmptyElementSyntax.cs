using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000461 RID: 1121
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class XmlEmptyElementSyntax : XmlNodeSyntax
	{
		// Token: 0x06005E5F RID: 24159 RVA: 0x0016E83C File Offset: 0x0016CA3C
		internal XmlEmptyElementSyntax(SyntaxKind kind, SyntaxToken lessThanToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributes, SyntaxToken slashGreaterThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(lessThanToken);
			this.lessThanToken = lessThanToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			if (attributes != null)
			{
				base.AdjustFlagsAndWidth(attributes);
				this.attributes = attributes;
			}
			base.AdjustFlagsAndWidth(slashGreaterThanToken);
			this.slashGreaterThanToken = slashGreaterThanToken;
		}

		// Token: 0x06005E60 RID: 24160 RVA: 0x0016E89C File Offset: 0x0016CA9C
		internal XmlEmptyElementSyntax(SyntaxKind kind, SyntaxToken lessThanToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributes, SyntaxToken slashGreaterThanToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(lessThanToken);
			this.lessThanToken = lessThanToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			if (attributes != null)
			{
				base.AdjustFlagsAndWidth(attributes);
				this.attributes = attributes;
			}
			base.AdjustFlagsAndWidth(slashGreaterThanToken);
			this.slashGreaterThanToken = slashGreaterThanToken;
		}

		// Token: 0x06005E61 RID: 24161 RVA: 0x0016E900 File Offset: 0x0016CB00
		internal XmlEmptyElementSyntax(SyntaxKind kind, SyntaxToken lessThanToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributes, SyntaxToken slashGreaterThanToken)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(lessThanToken);
			this.lessThanToken = lessThanToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			if (attributes != null)
			{
				base.AdjustFlagsAndWidth(attributes);
				this.attributes = attributes;
			}
			base.AdjustFlagsAndWidth(slashGreaterThanToken);
			this.slashGreaterThanToken = slashGreaterThanToken;
		}

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x06005E62 RID: 24162 RVA: 0x0016E95B File Offset: 0x0016CB5B
		public SyntaxToken LessThanToken
		{
			get
			{
				return this.lessThanToken;
			}
		}

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x06005E63 RID: 24163 RVA: 0x0016E963 File Offset: 0x0016CB63
		public XmlNameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x06005E64 RID: 24164 RVA: 0x0016E96B File Offset: 0x0016CB6B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<XmlAttributeSyntax> Attributes
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<XmlAttributeSyntax>(this.attributes);
			}
		}

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x06005E65 RID: 24165 RVA: 0x0016E978 File Offset: 0x0016CB78
		public SyntaxToken SlashGreaterThanToken
		{
			get
			{
				return this.slashGreaterThanToken;
			}
		}

		// Token: 0x06005E66 RID: 24166 RVA: 0x0016E980 File Offset: 0x0016CB80
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.lessThanToken;
				break;
			case 1:
				greenNode = this.name;
				break;
			case 2:
				greenNode = this.attributes;
				break;
			case 3:
				greenNode = this.slashGreaterThanToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005E67 RID: 24167 RVA: 0x0016E9CC File Offset: 0x0016CBCC
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new XmlEmptyElementSyntax(this, parent, position);
		}

		// Token: 0x06005E68 RID: 24168 RVA: 0x0016E9D6 File Offset: 0x0016CBD6
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlEmptyElement(this);
		}

		// Token: 0x06005E69 RID: 24169 RVA: 0x0016E9DF File Offset: 0x0016CBDF
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlEmptyElement(this);
		}

		// Token: 0x06005E6A RID: 24170 RVA: 0x0016E9E8 File Offset: 0x0016CBE8
		public XmlEmptyElementSyntax Update(SyntaxToken lessThanToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlAttributeSyntax> attributes, SyntaxToken slashGreaterThanToken)
		{
			if (lessThanToken != this.LessThanToken || name != this.Name || attributes != this.Attributes || slashGreaterThanToken != this.SlashGreaterThanToken)
			{
				XmlEmptyElementSyntax xmlEmptyElementSyntax = SyntaxFactory.XmlEmptyElement(lessThanToken, name, attributes, slashGreaterThanToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					xmlEmptyElementSyntax = xmlEmptyElementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					xmlEmptyElementSyntax = xmlEmptyElementSyntax.WithAnnotationsGreen(annotations);
				}
				return xmlEmptyElementSyntax;
			}
			return this;
		}

		// Token: 0x06005E6B RID: 24171 RVA: 0x0016EA59 File Offset: 0x0016CC59
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new XmlEmptyElementSyntax(base.Kind, this.lessThanToken, this.name, this.attributes, this.slashGreaterThanToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005E6C RID: 24172 RVA: 0x0016EA85 File Offset: 0x0016CC85
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new XmlEmptyElementSyntax(base.Kind, this.lessThanToken, this.name, this.attributes, this.slashGreaterThanToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005E6D RID: 24173 RVA: 0x0016EAB4 File Offset: 0x0016CCB4
		internal XmlEmptyElementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.lessThanToken = syntaxToken;
			XmlNameSyntax xmlNameSyntax = (XmlNameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(xmlNameSyntax);
			this.name = xmlNameSyntax;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributes = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.slashGreaterThanToken = syntaxToken2;
		}

		// Token: 0x06005E6E RID: 24174 RVA: 0x0016EB3A File Offset: 0x0016CD3A
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.lessThanToken);
			writer.WriteValue(this.name);
			writer.WriteValue(this.attributes);
			writer.WriteValue(this.slashGreaterThanToken);
		}

		// Token: 0x06005E6F RID: 24175 RVA: 0x0016EB73 File Offset: 0x0016CD73
		static XmlEmptyElementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(XmlEmptyElementSyntax), (ObjectReader r) => new XmlEmptyElementSyntax(r));
		}

		// Token: 0x040018CA RID: 6346
		internal readonly SyntaxToken lessThanToken;

		// Token: 0x040018CB RID: 6347
		internal readonly XmlNameSyntax name;

		// Token: 0x040018CC RID: 6348
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributes;

		// Token: 0x040018CD RID: 6349
		internal readonly SyntaxToken slashGreaterThanToken;
	}
}
