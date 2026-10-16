using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000471 RID: 1137
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class EndIfDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06005F4D RID: 24397 RVA: 0x0017120C File Offset: 0x0016F40C
		internal EndIfDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken endIfKeyword, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(endIfKeyword);
			this.endIfKeyword = endIfKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x00171260 File Offset: 0x0016F460
		internal EndIfDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken endIfKeyword, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(endIfKeyword);
			this.endIfKeyword = endIfKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x001712B8 File Offset: 0x0016F4B8
		internal EndIfDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken endIfKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(endIfKeyword);
			this.endIfKeyword = endIfKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x06005F50 RID: 24400 RVA: 0x00171307 File Offset: 0x0016F507
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x06005F51 RID: 24401 RVA: 0x0017130F File Offset: 0x0016F50F
		public SyntaxToken EndIfKeyword
		{
			get
			{
				return this.endIfKeyword;
			}
		}

		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06005F52 RID: 24402 RVA: 0x00171317 File Offset: 0x0016F517
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06005F53 RID: 24403 RVA: 0x0017131F File Offset: 0x0016F51F
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x06005F54 RID: 24404 RVA: 0x00171328 File Offset: 0x0016F528
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
				syntaxToken = this.endIfKeyword;
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

		// Token: 0x06005F55 RID: 24405 RVA: 0x00171367 File Offset: 0x0016F567
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new EndIfDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005F56 RID: 24406 RVA: 0x00171371 File Offset: 0x0016F571
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEndIfDirectiveTrivia(this);
		}

		// Token: 0x06005F57 RID: 24407 RVA: 0x0017137A File Offset: 0x0016F57A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEndIfDirectiveTrivia(this);
		}

		// Token: 0x06005F58 RID: 24408 RVA: 0x00171384 File Offset: 0x0016F584
		public EndIfDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken endIfKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken != this.HashToken || endIfKeyword != this.EndIfKeyword || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				EndIfDirectiveTriviaSyntax endIfDirectiveTriviaSyntax = SyntaxFactory.EndIfDirectiveTrivia(hashToken, endIfKeyword, endOfDirectiveToken, isActive);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					endIfDirectiveTriviaSyntax = endIfDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					endIfDirectiveTriviaSyntax = endIfDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return endIfDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06005F59 RID: 24409 RVA: 0x001713E6 File Offset: 0x0016F5E6
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new EndIfDirectiveTriviaSyntax(base.Kind, this.hashToken, this.endIfKeyword, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005F5A RID: 24410 RVA: 0x00171412 File Offset: 0x0016F612
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new EndIfDirectiveTriviaSyntax(base.Kind, this.hashToken, this.endIfKeyword, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005F5B RID: 24411 RVA: 0x00171440 File Offset: 0x0016F640
		internal EndIfDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.endIfKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.endOfDirectiveToken = syntaxToken3;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06005F5C RID: 24412 RVA: 0x001714B5 File Offset: 0x0016F6B5
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.endIfKeyword);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x06005F5D RID: 24413 RVA: 0x001714EE File Offset: 0x0016F6EE
		static EndIfDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(EndIfDirectiveTriviaSyntax), (ObjectReader r) => new EndIfDirectiveTriviaSyntax(r));
		}

		// Token: 0x040018FF RID: 6399
		internal readonly SyntaxToken hashToken;

		// Token: 0x04001900 RID: 6400
		internal readonly SyntaxToken endIfKeyword;

		// Token: 0x04001901 RID: 6401
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x04001902 RID: 6402
		internal readonly bool isActive;
	}
}
