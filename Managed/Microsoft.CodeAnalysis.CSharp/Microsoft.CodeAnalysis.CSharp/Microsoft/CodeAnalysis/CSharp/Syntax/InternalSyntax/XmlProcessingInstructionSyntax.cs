using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200046A RID: 1130
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class XmlProcessingInstructionSyntax : XmlNodeSyntax
	{
		// Token: 0x06005EE9 RID: 24297 RVA: 0x0016FFE0 File Offset: 0x0016E1E0
		internal XmlProcessingInstructionSyntax(SyntaxKind kind, SyntaxToken startProcessingInstructionToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode textTokens, SyntaxToken endProcessingInstructionToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(startProcessingInstructionToken);
			this.startProcessingInstructionToken = startProcessingInstructionToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
			base.AdjustFlagsAndWidth(endProcessingInstructionToken);
			this.endProcessingInstructionToken = endProcessingInstructionToken;
		}

		// Token: 0x06005EEA RID: 24298 RVA: 0x00170040 File Offset: 0x0016E240
		internal XmlProcessingInstructionSyntax(SyntaxKind kind, SyntaxToken startProcessingInstructionToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode textTokens, SyntaxToken endProcessingInstructionToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(startProcessingInstructionToken);
			this.startProcessingInstructionToken = startProcessingInstructionToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
			base.AdjustFlagsAndWidth(endProcessingInstructionToken);
			this.endProcessingInstructionToken = endProcessingInstructionToken;
		}

		// Token: 0x06005EEB RID: 24299 RVA: 0x001700A4 File Offset: 0x0016E2A4
		internal XmlProcessingInstructionSyntax(SyntaxKind kind, SyntaxToken startProcessingInstructionToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode textTokens, SyntaxToken endProcessingInstructionToken)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(startProcessingInstructionToken);
			this.startProcessingInstructionToken = startProcessingInstructionToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
			base.AdjustFlagsAndWidth(endProcessingInstructionToken);
			this.endProcessingInstructionToken = endProcessingInstructionToken;
		}

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x06005EEC RID: 24300 RVA: 0x001700FF File Offset: 0x0016E2FF
		public SyntaxToken StartProcessingInstructionToken
		{
			get
			{
				return this.startProcessingInstructionToken;
			}
		}

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06005EED RID: 24301 RVA: 0x00170107 File Offset: 0x0016E307
		public XmlNameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06005EEE RID: 24302 RVA: 0x0017010F File Offset: 0x0016E30F
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<SyntaxToken> TextTokens
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.textTokens);
			}
		}

		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x06005EEF RID: 24303 RVA: 0x0017011C File Offset: 0x0016E31C
		public SyntaxToken EndProcessingInstructionToken
		{
			get
			{
				return this.endProcessingInstructionToken;
			}
		}

		// Token: 0x06005EF0 RID: 24304 RVA: 0x00170124 File Offset: 0x0016E324
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.startProcessingInstructionToken;
				break;
			case 1:
				greenNode = this.name;
				break;
			case 2:
				greenNode = this.textTokens;
				break;
			case 3:
				greenNode = this.endProcessingInstructionToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005EF1 RID: 24305 RVA: 0x00170170 File Offset: 0x0016E370
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new XmlProcessingInstructionSyntax(this, parent, position);
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x0017017A File Offset: 0x0016E37A
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlProcessingInstruction(this);
		}

		// Token: 0x06005EF3 RID: 24307 RVA: 0x00170183 File Offset: 0x0016E383
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlProcessingInstruction(this);
		}

		// Token: 0x06005EF4 RID: 24308 RVA: 0x0017018C File Offset: 0x0016E38C
		public XmlProcessingInstructionSyntax Update(SyntaxToken startProcessingInstructionToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens, SyntaxToken endProcessingInstructionToken)
		{
			if (startProcessingInstructionToken != this.StartProcessingInstructionToken || name != this.Name || textTokens != this.TextTokens || endProcessingInstructionToken != this.EndProcessingInstructionToken)
			{
				XmlProcessingInstructionSyntax xmlProcessingInstructionSyntax = SyntaxFactory.XmlProcessingInstruction(startProcessingInstructionToken, name, textTokens, endProcessingInstructionToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					xmlProcessingInstructionSyntax = xmlProcessingInstructionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					xmlProcessingInstructionSyntax = xmlProcessingInstructionSyntax.WithAnnotationsGreen(annotations);
				}
				return xmlProcessingInstructionSyntax;
			}
			return this;
		}

		// Token: 0x06005EF5 RID: 24309 RVA: 0x001701FD File Offset: 0x0016E3FD
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new XmlProcessingInstructionSyntax(base.Kind, this.startProcessingInstructionToken, this.name, this.textTokens, this.endProcessingInstructionToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005EF6 RID: 24310 RVA: 0x00170229 File Offset: 0x0016E429
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new XmlProcessingInstructionSyntax(base.Kind, this.startProcessingInstructionToken, this.name, this.textTokens, this.endProcessingInstructionToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005EF7 RID: 24311 RVA: 0x00170258 File Offset: 0x0016E458
		internal XmlProcessingInstructionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.startProcessingInstructionToken = syntaxToken;
			XmlNameSyntax xmlNameSyntax = (XmlNameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(xmlNameSyntax);
			this.name = xmlNameSyntax;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.textTokens = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.endProcessingInstructionToken = syntaxToken2;
		}

		// Token: 0x06005EF8 RID: 24312 RVA: 0x001702DE File Offset: 0x0016E4DE
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.startProcessingInstructionToken);
			writer.WriteValue(this.name);
			writer.WriteValue(this.textTokens);
			writer.WriteValue(this.endProcessingInstructionToken);
		}

		// Token: 0x06005EF9 RID: 24313 RVA: 0x00170317 File Offset: 0x0016E517
		static XmlProcessingInstructionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(XmlProcessingInstructionSyntax), (ObjectReader r) => new XmlProcessingInstructionSyntax(r));
		}

		// Token: 0x040018E5 RID: 6373
		internal readonly SyntaxToken startProcessingInstructionToken;

		// Token: 0x040018E6 RID: 6374
		internal readonly XmlNameSyntax name;

		// Token: 0x040018E7 RID: 6375
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode textTokens;

		// Token: 0x040018E8 RID: 6376
		internal readonly SyntaxToken endProcessingInstructionToken;
	}
}
