using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000462 RID: 1122
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class XmlNameSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005E70 RID: 24176 RVA: 0x0016EB94 File Offset: 0x0016CD94
		internal XmlNameSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] XmlPrefixSyntax prefix, SyntaxToken localName, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			if (prefix != null)
			{
				base.AdjustFlagsAndWidth(prefix);
				this.prefix = prefix;
			}
			base.AdjustFlagsAndWidth(localName);
			this.localName = localName;
		}

		// Token: 0x06005E71 RID: 24177 RVA: 0x0016EBC7 File Offset: 0x0016CDC7
		internal XmlNameSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] XmlPrefixSyntax prefix, SyntaxToken localName, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			if (prefix != null)
			{
				base.AdjustFlagsAndWidth(prefix);
				this.prefix = prefix;
			}
			base.AdjustFlagsAndWidth(localName);
			this.localName = localName;
		}

		// Token: 0x06005E72 RID: 24178 RVA: 0x0016EBFE File Offset: 0x0016CDFE
		internal XmlNameSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] XmlPrefixSyntax prefix, SyntaxToken localName)
			: base(kind)
		{
			base.SlotCount = 2;
			if (prefix != null)
			{
				base.AdjustFlagsAndWidth(prefix);
				this.prefix = prefix;
			}
			base.AdjustFlagsAndWidth(localName);
			this.localName = localName;
		}

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x06005E73 RID: 24179 RVA: 0x0016EC2D File Offset: 0x0016CE2D
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public XmlPrefixSyntax Prefix
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x06005E74 RID: 24180 RVA: 0x0016EC35 File Offset: 0x0016CE35
		public SyntaxToken LocalName
		{
			get
			{
				return this.localName;
			}
		}

		// Token: 0x06005E75 RID: 24181 RVA: 0x0016EC40 File Offset: 0x0016CE40
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			if (index != 0)
			{
				if (index != 1)
				{
					greenNode = null;
				}
				else
				{
					greenNode = this.localName;
				}
			}
			else
			{
				greenNode = this.prefix;
			}
			return greenNode;
		}

		// Token: 0x06005E76 RID: 24182 RVA: 0x0016EC6B File Offset: 0x0016CE6B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new XmlNameSyntax(this, parent, position);
		}

		// Token: 0x06005E77 RID: 24183 RVA: 0x0016EC75 File Offset: 0x0016CE75
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlName(this);
		}

		// Token: 0x06005E78 RID: 24184 RVA: 0x0016EC7E File Offset: 0x0016CE7E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlName(this);
		}

		// Token: 0x06005E79 RID: 24185 RVA: 0x0016EC88 File Offset: 0x0016CE88
		public XmlNameSyntax Update(XmlPrefixSyntax prefix, SyntaxToken localName)
		{
			if (prefix != this.Prefix || localName != this.LocalName)
			{
				XmlNameSyntax xmlNameSyntax = SyntaxFactory.XmlName(prefix, localName);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					xmlNameSyntax = xmlNameSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					xmlNameSyntax = xmlNameSyntax.WithAnnotationsGreen(annotations);
				}
				return xmlNameSyntax;
			}
			return this;
		}

		// Token: 0x06005E7A RID: 24186 RVA: 0x0016ECDE File Offset: 0x0016CEDE
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new XmlNameSyntax(base.Kind, this.prefix, this.localName, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005E7B RID: 24187 RVA: 0x0016ECFE File Offset: 0x0016CEFE
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new XmlNameSyntax(base.Kind, this.prefix, this.localName, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005E7C RID: 24188 RVA: 0x0016ED20 File Offset: 0x0016CF20
		internal XmlNameSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			XmlPrefixSyntax xmlPrefixSyntax = (XmlPrefixSyntax)reader.ReadValue();
			if (xmlPrefixSyntax != null)
			{
				base.AdjustFlagsAndWidth(xmlPrefixSyntax);
				this.prefix = xmlPrefixSyntax;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.localName = syntaxToken;
		}

		// Token: 0x06005E7D RID: 24189 RVA: 0x0016ED72 File Offset: 0x0016CF72
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.prefix);
			writer.WriteValue(this.localName);
		}

		// Token: 0x06005E7E RID: 24190 RVA: 0x0016ED93 File Offset: 0x0016CF93
		static XmlNameSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(XmlNameSyntax), (ObjectReader r) => new XmlNameSyntax(r));
		}

		// Token: 0x040018CE RID: 6350
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly XmlPrefixSyntax prefix;

		// Token: 0x040018CF RID: 6351
		internal readonly SyntaxToken localName;
	}
}
