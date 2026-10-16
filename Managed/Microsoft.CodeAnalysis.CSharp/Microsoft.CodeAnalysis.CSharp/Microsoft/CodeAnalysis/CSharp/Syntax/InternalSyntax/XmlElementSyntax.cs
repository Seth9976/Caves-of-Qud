using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200045E RID: 1118
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class XmlElementSyntax : XmlNodeSyntax
	{
		// Token: 0x06005E2E RID: 24110 RVA: 0x0016DF74 File Offset: 0x0016C174
		internal XmlElementSyntax(SyntaxKind kind, XmlElementStartTagSyntax startTag, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode content, XmlElementEndTagSyntax endTag, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(startTag);
			this.startTag = startTag;
			if (content != null)
			{
				base.AdjustFlagsAndWidth(content);
				this.content = content;
			}
			base.AdjustFlagsAndWidth(endTag);
			this.endTag = endTag;
		}

		// Token: 0x06005E2F RID: 24111 RVA: 0x0016DFC4 File Offset: 0x0016C1C4
		internal XmlElementSyntax(SyntaxKind kind, XmlElementStartTagSyntax startTag, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode content, XmlElementEndTagSyntax endTag, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(startTag);
			this.startTag = startTag;
			if (content != null)
			{
				base.AdjustFlagsAndWidth(content);
				this.content = content;
			}
			base.AdjustFlagsAndWidth(endTag);
			this.endTag = endTag;
		}

		// Token: 0x06005E30 RID: 24112 RVA: 0x0016E016 File Offset: 0x0016C216
		internal XmlElementSyntax(SyntaxKind kind, XmlElementStartTagSyntax startTag, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode content, XmlElementEndTagSyntax endTag)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(startTag);
			this.startTag = startTag;
			if (content != null)
			{
				base.AdjustFlagsAndWidth(content);
				this.content = content;
			}
			base.AdjustFlagsAndWidth(endTag);
			this.endTag = endTag;
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x06005E31 RID: 24113 RVA: 0x0016E055 File Offset: 0x0016C255
		public XmlElementStartTagSyntax StartTag
		{
			get
			{
				return this.startTag;
			}
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x06005E32 RID: 24114 RVA: 0x0016E05D File Offset: 0x0016C25D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<XmlNodeSyntax> Content
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<XmlNodeSyntax>(this.content);
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x06005E33 RID: 24115 RVA: 0x0016E06A File Offset: 0x0016C26A
		public XmlElementEndTagSyntax EndTag
		{
			get
			{
				return this.endTag;
			}
		}

		// Token: 0x06005E34 RID: 24116 RVA: 0x0016E074 File Offset: 0x0016C274
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.startTag;
				break;
			case 1:
				greenNode = this.content;
				break;
			case 2:
				greenNode = this.endTag;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005E35 RID: 24117 RVA: 0x0016E0B3 File Offset: 0x0016C2B3
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new XmlElementSyntax(this, parent, position);
		}

		// Token: 0x06005E36 RID: 24118 RVA: 0x0016E0BD File Offset: 0x0016C2BD
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlElement(this);
		}

		// Token: 0x06005E37 RID: 24119 RVA: 0x0016E0C6 File Offset: 0x0016C2C6
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlElement(this);
		}

		// Token: 0x06005E38 RID: 24120 RVA: 0x0016E0D0 File Offset: 0x0016C2D0
		public XmlElementSyntax Update(XmlElementStartTagSyntax startTag, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlNodeSyntax> content, XmlElementEndTagSyntax endTag)
		{
			if (startTag != this.StartTag || content != this.Content || endTag != this.EndTag)
			{
				XmlElementSyntax xmlElementSyntax = SyntaxFactory.XmlElement(startTag, content, endTag);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					xmlElementSyntax = xmlElementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					xmlElementSyntax = xmlElementSyntax.WithAnnotationsGreen(annotations);
				}
				return xmlElementSyntax;
			}
			return this;
		}

		// Token: 0x06005E39 RID: 24121 RVA: 0x0016E135 File Offset: 0x0016C335
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new XmlElementSyntax(base.Kind, this.startTag, this.content, this.endTag, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005E3A RID: 24122 RVA: 0x0016E15B File Offset: 0x0016C35B
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new XmlElementSyntax(base.Kind, this.startTag, this.content, this.endTag, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005E3B RID: 24123 RVA: 0x0016E184 File Offset: 0x0016C384
		internal XmlElementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			XmlElementStartTagSyntax xmlElementStartTagSyntax = (XmlElementStartTagSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(xmlElementStartTagSyntax);
			this.startTag = xmlElementStartTagSyntax;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.content = greenNode;
			}
			XmlElementEndTagSyntax xmlElementEndTagSyntax = (XmlElementEndTagSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(xmlElementEndTagSyntax);
			this.endTag = xmlElementEndTagSyntax;
		}

		// Token: 0x06005E3C RID: 24124 RVA: 0x0016E1F0 File Offset: 0x0016C3F0
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.startTag);
			writer.WriteValue(this.content);
			writer.WriteValue(this.endTag);
		}

		// Token: 0x06005E3D RID: 24125 RVA: 0x0016E21D File Offset: 0x0016C41D
		static XmlElementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(XmlElementSyntax), (ObjectReader r) => new XmlElementSyntax(r));
		}

		// Token: 0x040018C0 RID: 6336
		internal readonly XmlElementStartTagSyntax startTag;

		// Token: 0x040018C1 RID: 6337
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode content;

		// Token: 0x040018C2 RID: 6338
		internal readonly XmlElementEndTagSyntax endTag;
	}
}
