using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000474 RID: 1140
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ErrorDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06005F80 RID: 24448 RVA: 0x00171B18 File Offset: 0x0016FD18
		internal ErrorDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken errorKeyword, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(errorKeyword);
			this.errorKeyword = errorKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005F81 RID: 24449 RVA: 0x00171B6C File Offset: 0x0016FD6C
		internal ErrorDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken errorKeyword, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(errorKeyword);
			this.errorKeyword = errorKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005F82 RID: 24450 RVA: 0x00171BC4 File Offset: 0x0016FDC4
		internal ErrorDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken errorKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(errorKeyword);
			this.errorKeyword = errorKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06005F83 RID: 24451 RVA: 0x00171C13 File Offset: 0x0016FE13
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x06005F84 RID: 24452 RVA: 0x00171C1B File Offset: 0x0016FE1B
		public SyntaxToken ErrorKeyword
		{
			get
			{
				return this.errorKeyword;
			}
		}

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x06005F85 RID: 24453 RVA: 0x00171C23 File Offset: 0x0016FE23
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x06005F86 RID: 24454 RVA: 0x00171C2B File Offset: 0x0016FE2B
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x06005F87 RID: 24455 RVA: 0x00171C34 File Offset: 0x0016FE34
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			SyntaxToken syntaxToken;
			switch (index)
			{
			case 0:
				syntaxToken = this.hashToken;
				break;
			case 1:
				syntaxToken = this.errorKeyword;
				break;
			case 2:
				syntaxToken = this.endOfDirectiveToken;
				break;
			default:
				syntaxToken = null;
				break;
			}
			return syntaxToken;
		}

		// Token: 0x06005F88 RID: 24456 RVA: 0x00171C73 File Offset: 0x0016FE73
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ErrorDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005F89 RID: 24457 RVA: 0x00171C7D File Offset: 0x0016FE7D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitErrorDirectiveTrivia(this);
		}

		// Token: 0x06005F8A RID: 24458 RVA: 0x00171C86 File Offset: 0x0016FE86
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitErrorDirectiveTrivia(this);
		}

		// Token: 0x06005F8B RID: 24459 RVA: 0x00171C90 File Offset: 0x0016FE90
		public ErrorDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken errorKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken != this.HashToken || errorKeyword != this.ErrorKeyword || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				ErrorDirectiveTriviaSyntax errorDirectiveTriviaSyntax = SyntaxFactory.ErrorDirectiveTrivia(hashToken, errorKeyword, endOfDirectiveToken, isActive);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					errorDirectiveTriviaSyntax = errorDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					errorDirectiveTriviaSyntax = errorDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return errorDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06005F8C RID: 24460 RVA: 0x00171CF2 File Offset: 0x0016FEF2
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ErrorDirectiveTriviaSyntax(base.Kind, this.hashToken, this.errorKeyword, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005F8D RID: 24461 RVA: 0x00171D1E File Offset: 0x0016FF1E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ErrorDirectiveTriviaSyntax(base.Kind, this.hashToken, this.errorKeyword, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005F8E RID: 24462 RVA: 0x00171D4C File Offset: 0x0016FF4C
		internal ErrorDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.errorKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.endOfDirectiveToken = syntaxToken3;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06005F8F RID: 24463 RVA: 0x00171DC1 File Offset: 0x0016FFC1
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.errorKeyword);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x06005F90 RID: 24464 RVA: 0x00171DFA File Offset: 0x0016FFFA
		static ErrorDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ErrorDirectiveTriviaSyntax), (ObjectReader r) => new ErrorDirectiveTriviaSyntax(r));
		}

		// Token: 0x0400190B RID: 6411
		internal readonly SyntaxToken hashToken;

		// Token: 0x0400190C RID: 6412
		internal readonly SyntaxToken errorKeyword;

		// Token: 0x0400190D RID: 6413
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x0400190E RID: 6414
		internal readonly bool isActive;
	}
}
