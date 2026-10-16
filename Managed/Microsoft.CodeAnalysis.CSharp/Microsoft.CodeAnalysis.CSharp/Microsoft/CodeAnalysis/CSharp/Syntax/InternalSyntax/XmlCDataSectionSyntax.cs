using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000469 RID: 1129
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class XmlCDataSectionSyntax : XmlNodeSyntax
	{
		// Token: 0x06005ED9 RID: 24281 RVA: 0x0016FD14 File Offset: 0x0016DF14
		internal XmlCDataSectionSyntax(SyntaxKind kind, SyntaxToken startCDataToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode textTokens, SyntaxToken endCDataToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(startCDataToken);
			this.startCDataToken = startCDataToken;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
			base.AdjustFlagsAndWidth(endCDataToken);
			this.endCDataToken = endCDataToken;
		}

		// Token: 0x06005EDA RID: 24282 RVA: 0x0016FD64 File Offset: 0x0016DF64
		internal XmlCDataSectionSyntax(SyntaxKind kind, SyntaxToken startCDataToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode textTokens, SyntaxToken endCDataToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(startCDataToken);
			this.startCDataToken = startCDataToken;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
			base.AdjustFlagsAndWidth(endCDataToken);
			this.endCDataToken = endCDataToken;
		}

		// Token: 0x06005EDB RID: 24283 RVA: 0x0016FDB6 File Offset: 0x0016DFB6
		internal XmlCDataSectionSyntax(SyntaxKind kind, SyntaxToken startCDataToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode textTokens, SyntaxToken endCDataToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(startCDataToken);
			this.startCDataToken = startCDataToken;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
			base.AdjustFlagsAndWidth(endCDataToken);
			this.endCDataToken = endCDataToken;
		}

		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06005EDC RID: 24284 RVA: 0x0016FDF5 File Offset: 0x0016DFF5
		public SyntaxToken StartCDataToken
		{
			get
			{
				return this.startCDataToken;
			}
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06005EDD RID: 24285 RVA: 0x0016FDFD File Offset: 0x0016DFFD
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<SyntaxToken> TextTokens
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.textTokens);
			}
		}

		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x06005EDE RID: 24286 RVA: 0x0016FE0A File Offset: 0x0016E00A
		public SyntaxToken EndCDataToken
		{
			get
			{
				return this.endCDataToken;
			}
		}

		// Token: 0x06005EDF RID: 24287 RVA: 0x0016FE14 File Offset: 0x0016E014
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.startCDataToken;
				break;
			case 1:
				greenNode = this.textTokens;
				break;
			case 2:
				greenNode = this.endCDataToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005EE0 RID: 24288 RVA: 0x0016FE53 File Offset: 0x0016E053
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new XmlCDataSectionSyntax(this, parent, position);
		}

		// Token: 0x06005EE1 RID: 24289 RVA: 0x0016FE5D File Offset: 0x0016E05D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlCDataSection(this);
		}

		// Token: 0x06005EE2 RID: 24290 RVA: 0x0016FE66 File Offset: 0x0016E066
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlCDataSection(this);
		}

		// Token: 0x06005EE3 RID: 24291 RVA: 0x0016FE70 File Offset: 0x0016E070
		public XmlCDataSectionSyntax Update(SyntaxToken startCDataToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens, SyntaxToken endCDataToken)
		{
			if (startCDataToken != this.StartCDataToken || textTokens != this.TextTokens || endCDataToken != this.EndCDataToken)
			{
				XmlCDataSectionSyntax xmlCDataSectionSyntax = SyntaxFactory.XmlCDataSection(startCDataToken, textTokens, endCDataToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					xmlCDataSectionSyntax = xmlCDataSectionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					xmlCDataSectionSyntax = xmlCDataSectionSyntax.WithAnnotationsGreen(annotations);
				}
				return xmlCDataSectionSyntax;
			}
			return this;
		}

		// Token: 0x06005EE4 RID: 24292 RVA: 0x0016FED5 File Offset: 0x0016E0D5
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new XmlCDataSectionSyntax(base.Kind, this.startCDataToken, this.textTokens, this.endCDataToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005EE5 RID: 24293 RVA: 0x0016FEFB File Offset: 0x0016E0FB
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new XmlCDataSectionSyntax(base.Kind, this.startCDataToken, this.textTokens, this.endCDataToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005EE6 RID: 24294 RVA: 0x0016FF24 File Offset: 0x0016E124
		internal XmlCDataSectionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.startCDataToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.textTokens = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.endCDataToken = syntaxToken2;
		}

		// Token: 0x06005EE7 RID: 24295 RVA: 0x0016FF90 File Offset: 0x0016E190
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.startCDataToken);
			writer.WriteValue(this.textTokens);
			writer.WriteValue(this.endCDataToken);
		}

		// Token: 0x06005EE8 RID: 24296 RVA: 0x0016FFBD File Offset: 0x0016E1BD
		static XmlCDataSectionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(XmlCDataSectionSyntax), (ObjectReader r) => new XmlCDataSectionSyntax(r));
		}

		// Token: 0x040018E2 RID: 6370
		internal readonly SyntaxToken startCDataToken;

		// Token: 0x040018E3 RID: 6371
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode textTokens;

		// Token: 0x040018E4 RID: 6372
		internal readonly SyntaxToken endCDataToken;
	}
}
