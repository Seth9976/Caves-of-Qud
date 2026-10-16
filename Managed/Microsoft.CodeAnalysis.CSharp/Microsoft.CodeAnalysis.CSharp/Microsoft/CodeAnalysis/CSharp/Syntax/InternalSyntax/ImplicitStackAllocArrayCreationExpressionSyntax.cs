using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003C0 RID: 960
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ImplicitStackAllocArrayCreationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060054B1 RID: 21681 RVA: 0x001509B4 File Offset: 0x0014EBB4
		internal ImplicitStackAllocArrayCreationExpressionSyntax(SyntaxKind kind, SyntaxToken stackAllocKeyword, SyntaxToken openBracketToken, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(stackAllocKeyword);
			this.stackAllocKeyword = stackAllocKeyword;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
			base.AdjustFlagsAndWidth(initializer);
			this.initializer = initializer;
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x00150A10 File Offset: 0x0014EC10
		internal ImplicitStackAllocArrayCreationExpressionSyntax(SyntaxKind kind, SyntaxToken stackAllocKeyword, SyntaxToken openBracketToken, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(stackAllocKeyword);
			this.stackAllocKeyword = stackAllocKeyword;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
			base.AdjustFlagsAndWidth(initializer);
			this.initializer = initializer;
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x00150A70 File Offset: 0x0014EC70
		internal ImplicitStackAllocArrayCreationExpressionSyntax(SyntaxKind kind, SyntaxToken stackAllocKeyword, SyntaxToken openBracketToken, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(stackAllocKeyword);
			this.stackAllocKeyword = stackAllocKeyword;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
			base.AdjustFlagsAndWidth(initializer);
			this.initializer = initializer;
		}

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x060054B4 RID: 21684 RVA: 0x00150AC7 File Offset: 0x0014ECC7
		public SyntaxToken StackAllocKeyword
		{
			get
			{
				return this.stackAllocKeyword;
			}
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x060054B5 RID: 21685 RVA: 0x00150ACF File Offset: 0x0014ECCF
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return this.openBracketToken;
			}
		}

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x060054B6 RID: 21686 RVA: 0x00150AD7 File Offset: 0x0014ECD7
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return this.closeBracketToken;
			}
		}

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x060054B7 RID: 21687 RVA: 0x00150ADF File Offset: 0x0014ECDF
		public InitializerExpressionSyntax Initializer
		{
			get
			{
				return this.initializer;
			}
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x00150AE8 File Offset: 0x0014ECE8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.stackAllocKeyword;
				break;
			case 1:
				greenNode = this.openBracketToken;
				break;
			case 2:
				greenNode = this.closeBracketToken;
				break;
			case 3:
				greenNode = this.initializer;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060054B9 RID: 21689 RVA: 0x00150B34 File Offset: 0x0014ED34
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ImplicitStackAllocArrayCreationExpressionSyntax(this, parent, position);
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x00150B3E File Offset: 0x0014ED3E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitImplicitStackAllocArrayCreationExpression(this);
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x00150B47 File Offset: 0x0014ED47
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitImplicitStackAllocArrayCreationExpression(this);
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x00150B50 File Offset: 0x0014ED50
		public ImplicitStackAllocArrayCreationExpressionSyntax Update(SyntaxToken stackAllocKeyword, SyntaxToken openBracketToken, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer)
		{
			if (stackAllocKeyword != this.StackAllocKeyword || openBracketToken != this.OpenBracketToken || closeBracketToken != this.CloseBracketToken || initializer != this.Initializer)
			{
				ImplicitStackAllocArrayCreationExpressionSyntax implicitStackAllocArrayCreationExpressionSyntax = SyntaxFactory.ImplicitStackAllocArrayCreationExpression(stackAllocKeyword, openBracketToken, closeBracketToken, initializer);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					implicitStackAllocArrayCreationExpressionSyntax = implicitStackAllocArrayCreationExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					implicitStackAllocArrayCreationExpressionSyntax = implicitStackAllocArrayCreationExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return implicitStackAllocArrayCreationExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x00150BBC File Offset: 0x0014EDBC
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ImplicitStackAllocArrayCreationExpressionSyntax(base.Kind, this.stackAllocKeyword, this.openBracketToken, this.closeBracketToken, this.initializer, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x00150BE8 File Offset: 0x0014EDE8
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ImplicitStackAllocArrayCreationExpressionSyntax(base.Kind, this.stackAllocKeyword, this.openBracketToken, this.closeBracketToken, this.initializer, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x00150C14 File Offset: 0x0014EE14
		internal ImplicitStackAllocArrayCreationExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.stackAllocKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.openBracketToken = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.closeBracketToken = syntaxToken3;
			InitializerExpressionSyntax initializerExpressionSyntax = (InitializerExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(initializerExpressionSyntax);
			this.initializer = initializerExpressionSyntax;
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x00150C97 File Offset: 0x0014EE97
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.stackAllocKeyword);
			writer.WriteValue(this.openBracketToken);
			writer.WriteValue(this.closeBracketToken);
			writer.WriteValue(this.initializer);
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x00150CD0 File Offset: 0x0014EED0
		static ImplicitStackAllocArrayCreationExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ImplicitStackAllocArrayCreationExpressionSyntax), (ObjectReader r) => new ImplicitStackAllocArrayCreationExpressionSyntax(r));
		}

		// Token: 0x04001691 RID: 5777
		internal readonly SyntaxToken stackAllocKeyword;

		// Token: 0x04001692 RID: 5778
		internal readonly SyntaxToken openBracketToken;

		// Token: 0x04001693 RID: 5779
		internal readonly SyntaxToken closeBracketToken;

		// Token: 0x04001694 RID: 5780
		internal readonly InitializerExpressionSyntax initializer;
	}
}
