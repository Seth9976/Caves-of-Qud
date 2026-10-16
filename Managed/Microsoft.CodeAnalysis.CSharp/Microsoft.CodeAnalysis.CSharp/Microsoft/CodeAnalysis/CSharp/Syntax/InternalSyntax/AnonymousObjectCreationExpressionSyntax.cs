using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003BC RID: 956
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AnonymousObjectCreationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x0600546E RID: 21614 RVA: 0x0014FCDC File Offset: 0x0014DEDC
		internal AnonymousObjectCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode initializers, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (initializers != null)
			{
				base.AdjustFlagsAndWidth(initializers);
				this.initializers = initializers;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x0600546F RID: 21615 RVA: 0x0014FD3C File Offset: 0x0014DF3C
		internal AnonymousObjectCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode initializers, SyntaxToken closeBraceToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (initializers != null)
			{
				base.AdjustFlagsAndWidth(initializers);
				this.initializers = initializers;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x0014FDA0 File Offset: 0x0014DFA0
		internal AnonymousObjectCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode initializers, SyntaxToken closeBraceToken)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (initializers != null)
			{
				base.AdjustFlagsAndWidth(initializers);
				this.initializers = initializers;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06005471 RID: 21617 RVA: 0x0014FDFB File Offset: 0x0014DFFB
		public SyntaxToken NewKeyword
		{
			get
			{
				return this.newKeyword;
			}
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06005472 RID: 21618 RVA: 0x0014FE03 File Offset: 0x0014E003
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return this.openBraceToken;
			}
		}

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06005473 RID: 21619 RVA: 0x0014FE0B File Offset: 0x0014E00B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> Initializers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax>(new SyntaxList<CSharpSyntaxNode>(this.initializers));
			}
		}

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06005474 RID: 21620 RVA: 0x0014FE22 File Offset: 0x0014E022
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return this.closeBraceToken;
			}
		}

		// Token: 0x06005475 RID: 21621 RVA: 0x0014FE2C File Offset: 0x0014E02C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.newKeyword;
				break;
			case 1:
				greenNode = this.openBraceToken;
				break;
			case 2:
				greenNode = this.initializers;
				break;
			case 3:
				greenNode = this.closeBraceToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005476 RID: 21622 RVA: 0x0014FE78 File Offset: 0x0014E078
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new AnonymousObjectCreationExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005477 RID: 21623 RVA: 0x0014FE82 File Offset: 0x0014E082
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAnonymousObjectCreationExpression(this);
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x0014FE8B File Offset: 0x0014E08B
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAnonymousObjectCreationExpression(this);
		}

		// Token: 0x06005479 RID: 21625 RVA: 0x0014FE94 File Offset: 0x0014E094
		public AnonymousObjectCreationExpressionSyntax Update(SyntaxToken newKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> initializers, SyntaxToken closeBraceToken)
		{
			if (newKeyword == this.NewKeyword && openBraceToken == this.OpenBraceToken)
			{
				SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> separatedSyntaxList = this.Initializers;
				if (!((in initializers) != (in separatedSyntaxList)) && closeBraceToken == this.CloseBraceToken)
				{
					return this;
				}
			}
			AnonymousObjectCreationExpressionSyntax anonymousObjectCreationExpressionSyntax = SyntaxFactory.AnonymousObjectCreationExpression(newKeyword, openBraceToken, initializers, closeBraceToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				anonymousObjectCreationExpressionSyntax = anonymousObjectCreationExpressionSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				anonymousObjectCreationExpressionSyntax = anonymousObjectCreationExpressionSyntax.WithAnnotationsGreen(annotations);
			}
			return anonymousObjectCreationExpressionSyntax;
		}

		// Token: 0x0600547A RID: 21626 RVA: 0x0014FF09 File Offset: 0x0014E109
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new AnonymousObjectCreationExpressionSyntax(base.Kind, this.newKeyword, this.openBraceToken, this.initializers, this.closeBraceToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600547B RID: 21627 RVA: 0x0014FF35 File Offset: 0x0014E135
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new AnonymousObjectCreationExpressionSyntax(base.Kind, this.newKeyword, this.openBraceToken, this.initializers, this.closeBraceToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600547C RID: 21628 RVA: 0x0014FF64 File Offset: 0x0014E164
		internal AnonymousObjectCreationExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.newKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.openBraceToken = syntaxToken2;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.initializers = greenNode;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.closeBraceToken = syntaxToken3;
		}

		// Token: 0x0600547D RID: 21629 RVA: 0x0014FFEA File Offset: 0x0014E1EA
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.newKeyword);
			writer.WriteValue(this.openBraceToken);
			writer.WriteValue(this.initializers);
			writer.WriteValue(this.closeBraceToken);
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x00150023 File Offset: 0x0014E223
		static AnonymousObjectCreationExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(AnonymousObjectCreationExpressionSyntax), (ObjectReader r) => new AnonymousObjectCreationExpressionSyntax(r));
		}

		// Token: 0x04001682 RID: 5762
		internal readonly SyntaxToken newKeyword;

		// Token: 0x04001683 RID: 5763
		internal readonly SyntaxToken openBraceToken;

		// Token: 0x04001684 RID: 5764
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode initializers;

		// Token: 0x04001685 RID: 5765
		internal readonly SyntaxToken closeBraceToken;
	}
}
