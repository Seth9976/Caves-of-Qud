using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000468 RID: 1128
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class XmlTextSyntax : XmlNodeSyntax
	{
		// Token: 0x06005ECB RID: 24267 RVA: 0x0016FB79 File Offset: 0x0016DD79
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal XmlTextSyntax(SyntaxKind kind, GreenNode textTokens, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
		}

		// Token: 0x06005ECC RID: 24268 RVA: 0x0016FB9D File Offset: 0x0016DD9D
		internal XmlTextSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode textTokens, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
		}

		// Token: 0x06005ECD RID: 24269 RVA: 0x0016FBC5 File Offset: 0x0016DDC5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal XmlTextSyntax(SyntaxKind kind, GreenNode textTokens)
			: base(kind)
		{
			base.SlotCount = 1;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x06005ECE RID: 24270 RVA: 0x0016FBE6 File Offset: 0x0016DDE6
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<SyntaxToken> TextTokens
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.textTokens);
			}
		}

		// Token: 0x06005ECF RID: 24271 RVA: 0x0016FBF3 File Offset: 0x0016DDF3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.textTokens;
		}

		// Token: 0x06005ED0 RID: 24272 RVA: 0x0016FC00 File Offset: 0x0016DE00
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new XmlTextSyntax(this, parent, position);
		}

		// Token: 0x06005ED1 RID: 24273 RVA: 0x0016FC0A File Offset: 0x0016DE0A
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlText(this);
		}

		// Token: 0x06005ED2 RID: 24274 RVA: 0x0016FC13 File Offset: 0x0016DE13
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlText(this);
		}

		// Token: 0x06005ED3 RID: 24275 RVA: 0x0016FC1C File Offset: 0x0016DE1C
		public XmlTextSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens)
		{
			if (textTokens != this.TextTokens)
			{
				XmlTextSyntax xmlTextSyntax = SyntaxFactory.XmlText(textTokens);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					xmlTextSyntax = xmlTextSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					xmlTextSyntax = xmlTextSyntax.WithAnnotationsGreen(annotations);
				}
				return xmlTextSyntax;
			}
			return this;
		}

		// Token: 0x06005ED4 RID: 24276 RVA: 0x0016FC6D File Offset: 0x0016DE6D
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new XmlTextSyntax(base.Kind, this.textTokens, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005ED5 RID: 24277 RVA: 0x0016FC87 File Offset: 0x0016DE87
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new XmlTextSyntax(base.Kind, this.textTokens, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005ED6 RID: 24278 RVA: 0x0016FCA4 File Offset: 0x0016DEA4
		internal XmlTextSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.textTokens = greenNode;
			}
		}

		// Token: 0x06005ED7 RID: 24279 RVA: 0x0016FCDC File Offset: 0x0016DEDC
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.textTokens);
		}

		// Token: 0x06005ED8 RID: 24280 RVA: 0x0016FCF1 File Offset: 0x0016DEF1
		static XmlTextSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(XmlTextSyntax), (ObjectReader r) => new XmlTextSyntax(r));
		}

		// Token: 0x040018E1 RID: 6369
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode textTokens;
	}
}
