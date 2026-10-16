using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200046B RID: 1131
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class XmlCommentSyntax : XmlNodeSyntax
	{
		// Token: 0x06005EFA RID: 24314 RVA: 0x00170338 File Offset: 0x0016E538
		internal XmlCommentSyntax(SyntaxKind kind, SyntaxToken lessThanExclamationMinusMinusToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode textTokens, SyntaxToken minusMinusGreaterThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(lessThanExclamationMinusMinusToken);
			this.lessThanExclamationMinusMinusToken = lessThanExclamationMinusMinusToken;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
			base.AdjustFlagsAndWidth(minusMinusGreaterThanToken);
			this.minusMinusGreaterThanToken = minusMinusGreaterThanToken;
		}

		// Token: 0x06005EFB RID: 24315 RVA: 0x00170388 File Offset: 0x0016E588
		internal XmlCommentSyntax(SyntaxKind kind, SyntaxToken lessThanExclamationMinusMinusToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode textTokens, SyntaxToken minusMinusGreaterThanToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(lessThanExclamationMinusMinusToken);
			this.lessThanExclamationMinusMinusToken = lessThanExclamationMinusMinusToken;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
			base.AdjustFlagsAndWidth(minusMinusGreaterThanToken);
			this.minusMinusGreaterThanToken = minusMinusGreaterThanToken;
		}

		// Token: 0x06005EFC RID: 24316 RVA: 0x001703DA File Offset: 0x0016E5DA
		internal XmlCommentSyntax(SyntaxKind kind, SyntaxToken lessThanExclamationMinusMinusToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode textTokens, SyntaxToken minusMinusGreaterThanToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(lessThanExclamationMinusMinusToken);
			this.lessThanExclamationMinusMinusToken = lessThanExclamationMinusMinusToken;
			if (textTokens != null)
			{
				base.AdjustFlagsAndWidth(textTokens);
				this.textTokens = textTokens;
			}
			base.AdjustFlagsAndWidth(minusMinusGreaterThanToken);
			this.minusMinusGreaterThanToken = minusMinusGreaterThanToken;
		}

		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06005EFD RID: 24317 RVA: 0x00170419 File Offset: 0x0016E619
		public SyntaxToken LessThanExclamationMinusMinusToken
		{
			get
			{
				return this.lessThanExclamationMinusMinusToken;
			}
		}

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06005EFE RID: 24318 RVA: 0x00170421 File Offset: 0x0016E621
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<SyntaxToken> TextTokens
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.textTokens);
			}
		}

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06005EFF RID: 24319 RVA: 0x0017042E File Offset: 0x0016E62E
		public SyntaxToken MinusMinusGreaterThanToken
		{
			get
			{
				return this.minusMinusGreaterThanToken;
			}
		}

		// Token: 0x06005F00 RID: 24320 RVA: 0x00170438 File Offset: 0x0016E638
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.lessThanExclamationMinusMinusToken;
				break;
			case 1:
				greenNode = this.textTokens;
				break;
			case 2:
				greenNode = this.minusMinusGreaterThanToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005F01 RID: 24321 RVA: 0x00170477 File Offset: 0x0016E677
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new XmlCommentSyntax(this, parent, position);
		}

		// Token: 0x06005F02 RID: 24322 RVA: 0x00170481 File Offset: 0x0016E681
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlComment(this);
		}

		// Token: 0x06005F03 RID: 24323 RVA: 0x0017048A File Offset: 0x0016E68A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlComment(this);
		}

		// Token: 0x06005F04 RID: 24324 RVA: 0x00170494 File Offset: 0x0016E694
		public XmlCommentSyntax Update(SyntaxToken lessThanExclamationMinusMinusToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens, SyntaxToken minusMinusGreaterThanToken)
		{
			if (lessThanExclamationMinusMinusToken != this.LessThanExclamationMinusMinusToken || textTokens != this.TextTokens || minusMinusGreaterThanToken != this.MinusMinusGreaterThanToken)
			{
				XmlCommentSyntax xmlCommentSyntax = SyntaxFactory.XmlComment(lessThanExclamationMinusMinusToken, textTokens, minusMinusGreaterThanToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					xmlCommentSyntax = xmlCommentSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					xmlCommentSyntax = xmlCommentSyntax.WithAnnotationsGreen(annotations);
				}
				return xmlCommentSyntax;
			}
			return this;
		}

		// Token: 0x06005F05 RID: 24325 RVA: 0x001704F9 File Offset: 0x0016E6F9
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new XmlCommentSyntax(base.Kind, this.lessThanExclamationMinusMinusToken, this.textTokens, this.minusMinusGreaterThanToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005F06 RID: 24326 RVA: 0x0017051F File Offset: 0x0016E71F
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new XmlCommentSyntax(base.Kind, this.lessThanExclamationMinusMinusToken, this.textTokens, this.minusMinusGreaterThanToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005F07 RID: 24327 RVA: 0x00170548 File Offset: 0x0016E748
		internal XmlCommentSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.lessThanExclamationMinusMinusToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.textTokens = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.minusMinusGreaterThanToken = syntaxToken2;
		}

		// Token: 0x06005F08 RID: 24328 RVA: 0x001705B4 File Offset: 0x0016E7B4
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.lessThanExclamationMinusMinusToken);
			writer.WriteValue(this.textTokens);
			writer.WriteValue(this.minusMinusGreaterThanToken);
		}

		// Token: 0x06005F09 RID: 24329 RVA: 0x001705E1 File Offset: 0x0016E7E1
		static XmlCommentSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(XmlCommentSyntax), (ObjectReader r) => new XmlCommentSyntax(r));
		}

		// Token: 0x040018E9 RID: 6377
		internal readonly SyntaxToken lessThanExclamationMinusMinusToken;

		// Token: 0x040018EA RID: 6378
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode textTokens;

		// Token: 0x040018EB RID: 6379
		internal readonly SyntaxToken minusMinusGreaterThanToken;
	}
}
