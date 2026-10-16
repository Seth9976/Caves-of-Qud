using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000476 RID: 1142
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BadDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06005FA2 RID: 24482 RVA: 0x00172120 File Offset: 0x00170320
		internal BadDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken identifier, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005FA3 RID: 24483 RVA: 0x00172174 File Offset: 0x00170374
		internal BadDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken identifier, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005FA4 RID: 24484 RVA: 0x001721CC File Offset: 0x001703CC
		internal BadDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken identifier, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x06005FA5 RID: 24485 RVA: 0x0017221B File Offset: 0x0017041B
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x06005FA6 RID: 24486 RVA: 0x00172223 File Offset: 0x00170423
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x06005FA7 RID: 24487 RVA: 0x0017222B File Offset: 0x0017042B
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x06005FA8 RID: 24488 RVA: 0x00172233 File Offset: 0x00170433
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x06005FA9 RID: 24489 RVA: 0x0017223C File Offset: 0x0017043C
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
				syntaxToken = this.identifier;
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

		// Token: 0x06005FAA RID: 24490 RVA: 0x0017227B File Offset: 0x0017047B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new BadDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005FAB RID: 24491 RVA: 0x00172285 File Offset: 0x00170485
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBadDirectiveTrivia(this);
		}

		// Token: 0x06005FAC RID: 24492 RVA: 0x0017228E File Offset: 0x0017048E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBadDirectiveTrivia(this);
		}

		// Token: 0x06005FAD RID: 24493 RVA: 0x00172298 File Offset: 0x00170498
		public BadDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken identifier, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken != this.HashToken || identifier != this.Identifier || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				BadDirectiveTriviaSyntax badDirectiveTriviaSyntax = SyntaxFactory.BadDirectiveTrivia(hashToken, identifier, endOfDirectiveToken, isActive);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					badDirectiveTriviaSyntax = badDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					badDirectiveTriviaSyntax = badDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return badDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06005FAE RID: 24494 RVA: 0x001722FA File Offset: 0x001704FA
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new BadDirectiveTriviaSyntax(base.Kind, this.hashToken, this.identifier, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005FAF RID: 24495 RVA: 0x00172326 File Offset: 0x00170526
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new BadDirectiveTriviaSyntax(base.Kind, this.hashToken, this.identifier, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005FB0 RID: 24496 RVA: 0x00172354 File Offset: 0x00170554
		internal BadDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.identifier = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.endOfDirectiveToken = syntaxToken3;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06005FB1 RID: 24497 RVA: 0x001723C9 File Offset: 0x001705C9
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x06005FB2 RID: 24498 RVA: 0x00172402 File Offset: 0x00170602
		static BadDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(BadDirectiveTriviaSyntax), (ObjectReader r) => new BadDirectiveTriviaSyntax(r));
		}

		// Token: 0x04001913 RID: 6419
		internal readonly SyntaxToken hashToken;

		// Token: 0x04001914 RID: 6420
		internal readonly SyntaxToken identifier;

		// Token: 0x04001915 RID: 6421
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x04001916 RID: 6422
		internal readonly bool isActive;
	}
}
