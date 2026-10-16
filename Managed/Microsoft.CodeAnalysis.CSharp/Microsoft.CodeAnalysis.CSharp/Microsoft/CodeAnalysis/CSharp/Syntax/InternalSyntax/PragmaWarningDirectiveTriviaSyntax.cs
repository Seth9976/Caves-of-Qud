using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200047A RID: 1146
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PragmaWarningDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06005FEA RID: 24554 RVA: 0x00172F90 File Offset: 0x00171190
		internal PragmaWarningDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken warningKeyword, SyntaxToken disableOrRestoreKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode errorCodes, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 6;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(pragmaKeyword);
			this.pragmaKeyword = pragmaKeyword;
			base.AdjustFlagsAndWidth(warningKeyword);
			this.warningKeyword = warningKeyword;
			base.AdjustFlagsAndWidth(disableOrRestoreKeyword);
			this.disableOrRestoreKeyword = disableOrRestoreKeyword;
			if (errorCodes != null)
			{
				base.AdjustFlagsAndWidth(errorCodes);
				this.errorCodes = errorCodes;
			}
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x00173018 File Offset: 0x00171218
		internal PragmaWarningDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken warningKeyword, SyntaxToken disableOrRestoreKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode errorCodes, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 6;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(pragmaKeyword);
			this.pragmaKeyword = pragmaKeyword;
			base.AdjustFlagsAndWidth(warningKeyword);
			this.warningKeyword = warningKeyword;
			base.AdjustFlagsAndWidth(disableOrRestoreKeyword);
			this.disableOrRestoreKeyword = disableOrRestoreKeyword;
			if (errorCodes != null)
			{
				base.AdjustFlagsAndWidth(errorCodes);
				this.errorCodes = errorCodes;
			}
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x001730A4 File Offset: 0x001712A4
		internal PragmaWarningDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken warningKeyword, SyntaxToken disableOrRestoreKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode errorCodes, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 6;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(pragmaKeyword);
			this.pragmaKeyword = pragmaKeyword;
			base.AdjustFlagsAndWidth(warningKeyword);
			this.warningKeyword = warningKeyword;
			base.AdjustFlagsAndWidth(disableOrRestoreKeyword);
			this.disableOrRestoreKeyword = disableOrRestoreKeyword;
			if (errorCodes != null)
			{
				base.AdjustFlagsAndWidth(errorCodes);
				this.errorCodes = errorCodes;
			}
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x06005FED RID: 24557 RVA: 0x00173127 File Offset: 0x00171327
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06005FEE RID: 24558 RVA: 0x0017312F File Offset: 0x0017132F
		public SyntaxToken PragmaKeyword
		{
			get
			{
				return this.pragmaKeyword;
			}
		}

		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x06005FEF RID: 24559 RVA: 0x00173137 File Offset: 0x00171337
		public SyntaxToken WarningKeyword
		{
			get
			{
				return this.warningKeyword;
			}
		}

		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x06005FF0 RID: 24560 RVA: 0x0017313F File Offset: 0x0017133F
		public SyntaxToken DisableOrRestoreKeyword
		{
			get
			{
				return this.disableOrRestoreKeyword;
			}
		}

		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x06005FF1 RID: 24561 RVA: 0x00173147 File Offset: 0x00171347
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<ExpressionSyntax> ErrorCodes
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<ExpressionSyntax>(new SyntaxList<CSharpSyntaxNode>(this.errorCodes));
			}
		}

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06005FF2 RID: 24562 RVA: 0x0017315E File Offset: 0x0017135E
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06005FF3 RID: 24563 RVA: 0x00173166 File Offset: 0x00171366
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x06005FF4 RID: 24564 RVA: 0x00173170 File Offset: 0x00171370
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.hashToken;
				break;
			case 1:
				greenNode = this.pragmaKeyword;
				break;
			case 2:
				greenNode = this.warningKeyword;
				break;
			case 3:
				greenNode = this.disableOrRestoreKeyword;
				break;
			case 4:
				greenNode = this.errorCodes;
				break;
			case 5:
				greenNode = this.endOfDirectiveToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005FF5 RID: 24565 RVA: 0x001731D6 File Offset: 0x001713D6
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new PragmaWarningDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005FF6 RID: 24566 RVA: 0x001731E0 File Offset: 0x001713E0
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPragmaWarningDirectiveTrivia(this);
		}

		// Token: 0x06005FF7 RID: 24567 RVA: 0x001731E9 File Offset: 0x001713E9
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPragmaWarningDirectiveTrivia(this);
		}

		// Token: 0x06005FF8 RID: 24568 RVA: 0x001731F4 File Offset: 0x001713F4
		public PragmaWarningDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken warningKeyword, SyntaxToken disableOrRestoreKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> errorCodes, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken == this.HashToken && pragmaKeyword == this.PragmaKeyword && warningKeyword == this.WarningKeyword && disableOrRestoreKeyword == this.DisableOrRestoreKeyword)
			{
				SeparatedSyntaxList<ExpressionSyntax> separatedSyntaxList = this.ErrorCodes;
				if (!((in errorCodes) != (in separatedSyntaxList)) && endOfDirectiveToken == this.EndOfDirectiveToken)
				{
					return this;
				}
			}
			PragmaWarningDirectiveTriviaSyntax pragmaWarningDirectiveTriviaSyntax = SyntaxFactory.PragmaWarningDirectiveTrivia(hashToken, pragmaKeyword, warningKeyword, disableOrRestoreKeyword, errorCodes, endOfDirectiveToken, isActive);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				pragmaWarningDirectiveTriviaSyntax = pragmaWarningDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				pragmaWarningDirectiveTriviaSyntax = pragmaWarningDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
			}
			return pragmaWarningDirectiveTriviaSyntax;
		}

		// Token: 0x06005FF9 RID: 24569 RVA: 0x00173284 File Offset: 0x00171484
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new PragmaWarningDirectiveTriviaSyntax(base.Kind, this.hashToken, this.pragmaKeyword, this.warningKeyword, this.disableOrRestoreKeyword, this.errorCodes, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005FFA RID: 24570 RVA: 0x001732D0 File Offset: 0x001714D0
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new PragmaWarningDirectiveTriviaSyntax(base.Kind, this.hashToken, this.pragmaKeyword, this.warningKeyword, this.disableOrRestoreKeyword, this.errorCodes, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005FFB RID: 24571 RVA: 0x0017331C File Offset: 0x0017151C
		internal PragmaWarningDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 6;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.pragmaKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.warningKeyword = syntaxToken3;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.disableOrRestoreKeyword = syntaxToken4;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.errorCodes = greenNode;
			}
			SyntaxToken syntaxToken5 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken5);
			this.endOfDirectiveToken = syntaxToken5;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06005FFC RID: 24572 RVA: 0x001733EC File Offset: 0x001715EC
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.pragmaKeyword);
			writer.WriteValue(this.warningKeyword);
			writer.WriteValue(this.disableOrRestoreKeyword);
			writer.WriteValue(this.errorCodes);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x06005FFD RID: 24573 RVA: 0x00173454 File Offset: 0x00171654
		static PragmaWarningDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(PragmaWarningDirectiveTriviaSyntax), (ObjectReader r) => new PragmaWarningDirectiveTriviaSyntax(r));
		}

		// Token: 0x04001927 RID: 6439
		internal readonly SyntaxToken hashToken;

		// Token: 0x04001928 RID: 6440
		internal readonly SyntaxToken pragmaKeyword;

		// Token: 0x04001929 RID: 6441
		internal readonly SyntaxToken warningKeyword;

		// Token: 0x0400192A RID: 6442
		internal readonly SyntaxToken disableOrRestoreKeyword;

		// Token: 0x0400192B RID: 6443
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode errorCodes;

		// Token: 0x0400192C RID: 6444
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x0400192D RID: 6445
		internal readonly bool isActive;
	}
}
