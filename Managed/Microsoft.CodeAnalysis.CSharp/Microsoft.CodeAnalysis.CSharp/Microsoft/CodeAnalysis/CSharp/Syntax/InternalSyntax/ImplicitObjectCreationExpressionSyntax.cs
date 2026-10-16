using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003B8 RID: 952
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ImplicitObjectCreationExpressionSyntax : BaseObjectCreationExpressionSyntax
	{
		// Token: 0x0600542E RID: 21550 RVA: 0x0014F200 File Offset: 0x0014D400
		internal ImplicitObjectCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x0014F250 File Offset: 0x0014D450
		internal ImplicitObjectCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x0014F2A3 File Offset: 0x0014D4A3
		internal ImplicitObjectCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
		}

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06005431 RID: 21553 RVA: 0x0014F2E3 File Offset: 0x0014D4E3
		public override SyntaxToken NewKeyword
		{
			get
			{
				return this.newKeyword;
			}
		}

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06005432 RID: 21554 RVA: 0x0014F2EB File Offset: 0x0014D4EB
		public override ArgumentListSyntax ArgumentList
		{
			get
			{
				return this.argumentList;
			}
		}

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06005433 RID: 21555 RVA: 0x0014F2F3 File Offset: 0x0014D4F3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override InitializerExpressionSyntax Initializer
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.initializer;
			}
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x0014F2FC File Offset: 0x0014D4FC
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
				greenNode = this.argumentList;
				break;
			case 2:
				greenNode = this.initializer;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x0014F33B File Offset: 0x0014D53B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ImplicitObjectCreationExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x0014F345 File Offset: 0x0014D545
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitImplicitObjectCreationExpression(this);
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x0014F34E File Offset: 0x0014D54E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitImplicitObjectCreationExpression(this);
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x0014F358 File Offset: 0x0014D558
		public ImplicitObjectCreationExpressionSyntax Update(SyntaxToken newKeyword, ArgumentListSyntax argumentList, InitializerExpressionSyntax initializer)
		{
			if (newKeyword != this.NewKeyword || argumentList != this.ArgumentList || initializer != this.Initializer)
			{
				ImplicitObjectCreationExpressionSyntax implicitObjectCreationExpressionSyntax = SyntaxFactory.ImplicitObjectCreationExpression(newKeyword, argumentList, initializer);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					implicitObjectCreationExpressionSyntax = implicitObjectCreationExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					implicitObjectCreationExpressionSyntax = implicitObjectCreationExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return implicitObjectCreationExpressionSyntax;
			}
			return this;
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x0014F3B8 File Offset: 0x0014D5B8
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ImplicitObjectCreationExpressionSyntax(base.Kind, this.newKeyword, this.argumentList, this.initializer, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x0014F3DE File Offset: 0x0014D5DE
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ImplicitObjectCreationExpressionSyntax(base.Kind, this.newKeyword, this.argumentList, this.initializer, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x0014F404 File Offset: 0x0014D604
		internal ImplicitObjectCreationExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.newKeyword = syntaxToken;
			ArgumentListSyntax argumentListSyntax = (ArgumentListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(argumentListSyntax);
			this.argumentList = argumentListSyntax;
			InitializerExpressionSyntax initializerExpressionSyntax = (InitializerExpressionSyntax)reader.ReadValue();
			if (initializerExpressionSyntax != null)
			{
				base.AdjustFlagsAndWidth(initializerExpressionSyntax);
				this.initializer = initializerExpressionSyntax;
			}
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x0014F470 File Offset: 0x0014D670
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.newKeyword);
			writer.WriteValue(this.argumentList);
			writer.WriteValue(this.initializer);
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x0014F49D File Offset: 0x0014D69D
		static ImplicitObjectCreationExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ImplicitObjectCreationExpressionSyntax), (ObjectReader r) => new ImplicitObjectCreationExpressionSyntax(r));
		}

		// Token: 0x04001676 RID: 5750
		internal readonly SyntaxToken newKeyword;

		// Token: 0x04001677 RID: 5751
		internal readonly ArgumentListSyntax argumentList;

		// Token: 0x04001678 RID: 5752
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly InitializerExpressionSyntax initializer;
	}
}
