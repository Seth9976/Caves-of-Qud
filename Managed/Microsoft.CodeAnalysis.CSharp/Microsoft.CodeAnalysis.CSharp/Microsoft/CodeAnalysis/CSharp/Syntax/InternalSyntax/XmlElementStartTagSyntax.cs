using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200045F RID: 1119
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class XmlElementStartTagSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005E3E RID: 24126 RVA: 0x0016E240 File Offset: 0x0016C440
		internal XmlElementStartTagSyntax(SyntaxKind kind, SyntaxToken lessThanToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributes, SyntaxToken greaterThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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
			base.AdjustFlagsAndWidth(greaterThanToken);
			this.greaterThanToken = greaterThanToken;
		}

		// Token: 0x06005E3F RID: 24127 RVA: 0x0016E2A0 File Offset: 0x0016C4A0
		internal XmlElementStartTagSyntax(SyntaxKind kind, SyntaxToken lessThanToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributes, SyntaxToken greaterThanToken, SyntaxFactoryContext context)
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
			base.AdjustFlagsAndWidth(greaterThanToken);
			this.greaterThanToken = greaterThanToken;
		}

		// Token: 0x06005E40 RID: 24128 RVA: 0x0016E304 File Offset: 0x0016C504
		internal XmlElementStartTagSyntax(SyntaxKind kind, SyntaxToken lessThanToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributes, SyntaxToken greaterThanToken)
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
			base.AdjustFlagsAndWidth(greaterThanToken);
			this.greaterThanToken = greaterThanToken;
		}

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x06005E41 RID: 24129 RVA: 0x0016E35F File Offset: 0x0016C55F
		public SyntaxToken LessThanToken
		{
			get
			{
				return this.lessThanToken;
			}
		}

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x06005E42 RID: 24130 RVA: 0x0016E367 File Offset: 0x0016C567
		public XmlNameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x06005E43 RID: 24131 RVA: 0x0016E36F File Offset: 0x0016C56F
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<XmlAttributeSyntax> Attributes
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<XmlAttributeSyntax>(this.attributes);
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x06005E44 RID: 24132 RVA: 0x0016E37C File Offset: 0x0016C57C
		public SyntaxToken GreaterThanToken
		{
			get
			{
				return this.greaterThanToken;
			}
		}

		// Token: 0x06005E45 RID: 24133 RVA: 0x0016E384 File Offset: 0x0016C584
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
				greenNode = this.greaterThanToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005E46 RID: 24134 RVA: 0x0016E3D0 File Offset: 0x0016C5D0
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new XmlElementStartTagSyntax(this, parent, position);
		}

		// Token: 0x06005E47 RID: 24135 RVA: 0x0016E3DA File Offset: 0x0016C5DA
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlElementStartTag(this);
		}

		// Token: 0x06005E48 RID: 24136 RVA: 0x0016E3E3 File Offset: 0x0016C5E3
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlElementStartTag(this);
		}

		// Token: 0x06005E49 RID: 24137 RVA: 0x0016E3EC File Offset: 0x0016C5EC
		public XmlElementStartTagSyntax Update(SyntaxToken lessThanToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlAttributeSyntax> attributes, SyntaxToken greaterThanToken)
		{
			if (lessThanToken != this.LessThanToken || name != this.Name || attributes != this.Attributes || greaterThanToken != this.GreaterThanToken)
			{
				XmlElementStartTagSyntax xmlElementStartTagSyntax = SyntaxFactory.XmlElementStartTag(lessThanToken, name, attributes, greaterThanToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					xmlElementStartTagSyntax = xmlElementStartTagSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					xmlElementStartTagSyntax = xmlElementStartTagSyntax.WithAnnotationsGreen(annotations);
				}
				return xmlElementStartTagSyntax;
			}
			return this;
		}

		// Token: 0x06005E4A RID: 24138 RVA: 0x0016E45D File Offset: 0x0016C65D
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new XmlElementStartTagSyntax(base.Kind, this.lessThanToken, this.name, this.attributes, this.greaterThanToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005E4B RID: 24139 RVA: 0x0016E489 File Offset: 0x0016C689
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new XmlElementStartTagSyntax(base.Kind, this.lessThanToken, this.name, this.attributes, this.greaterThanToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005E4C RID: 24140 RVA: 0x0016E4B8 File Offset: 0x0016C6B8
		internal XmlElementStartTagSyntax(ObjectReader reader)
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
			this.greaterThanToken = syntaxToken2;
		}

		// Token: 0x06005E4D RID: 24141 RVA: 0x0016E53E File Offset: 0x0016C73E
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.lessThanToken);
			writer.WriteValue(this.name);
			writer.WriteValue(this.attributes);
			writer.WriteValue(this.greaterThanToken);
		}

		// Token: 0x06005E4E RID: 24142 RVA: 0x0016E577 File Offset: 0x0016C777
		static XmlElementStartTagSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(XmlElementStartTagSyntax), (ObjectReader r) => new XmlElementStartTagSyntax(r));
		}

		// Token: 0x040018C3 RID: 6339
		internal readonly SyntaxToken lessThanToken;

		// Token: 0x040018C4 RID: 6340
		internal readonly XmlNameSyntax name;

		// Token: 0x040018C5 RID: 6341
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributes;

		// Token: 0x040018C6 RID: 6342
		internal readonly SyntaxToken greaterThanToken;
	}
}
