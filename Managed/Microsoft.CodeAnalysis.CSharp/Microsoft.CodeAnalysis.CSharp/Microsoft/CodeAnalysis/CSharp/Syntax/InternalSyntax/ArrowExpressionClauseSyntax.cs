using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000443 RID: 1091
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ArrowExpressionClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005CBF RID: 23743 RVA: 0x00169ED9 File Offset: 0x001680D9
		internal ArrowExpressionClauseSyntax(SyntaxKind kind, SyntaxToken arrowToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(arrowToken);
			this.arrowToken = arrowToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x06005CC0 RID: 23744 RVA: 0x00169F09 File Offset: 0x00168109
		internal ArrowExpressionClauseSyntax(SyntaxKind kind, SyntaxToken arrowToken, ExpressionSyntax expression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(arrowToken);
			this.arrowToken = arrowToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x06005CC1 RID: 23745 RVA: 0x00169F3D File Offset: 0x0016813D
		internal ArrowExpressionClauseSyntax(SyntaxKind kind, SyntaxToken arrowToken, ExpressionSyntax expression)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(arrowToken);
			this.arrowToken = arrowToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x06005CC2 RID: 23746 RVA: 0x00169F69 File Offset: 0x00168169
		public SyntaxToken ArrowToken
		{
			get
			{
				return this.arrowToken;
			}
		}

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x06005CC3 RID: 23747 RVA: 0x00169F71 File Offset: 0x00168171
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x06005CC4 RID: 23748 RVA: 0x00169F7C File Offset: 0x0016817C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			if (index != 0)
			{
				if (index != 1)
				{
					greenNode = null;
				}
				else
				{
					greenNode = this.expression;
				}
			}
			else
			{
				greenNode = this.arrowToken;
			}
			return greenNode;
		}

		// Token: 0x06005CC5 RID: 23749 RVA: 0x00169FA7 File Offset: 0x001681A7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ArrowExpressionClauseSyntax(this, parent, position);
		}

		// Token: 0x06005CC6 RID: 23750 RVA: 0x00169FB1 File Offset: 0x001681B1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitArrowExpressionClause(this);
		}

		// Token: 0x06005CC7 RID: 23751 RVA: 0x00169FBA File Offset: 0x001681BA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitArrowExpressionClause(this);
		}

		// Token: 0x06005CC8 RID: 23752 RVA: 0x00169FC4 File Offset: 0x001681C4
		public ArrowExpressionClauseSyntax Update(SyntaxToken arrowToken, ExpressionSyntax expression)
		{
			if (arrowToken != this.ArrowToken || expression != this.Expression)
			{
				ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = SyntaxFactory.ArrowExpressionClause(arrowToken, expression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					arrowExpressionClauseSyntax = arrowExpressionClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					arrowExpressionClauseSyntax = arrowExpressionClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return arrowExpressionClauseSyntax;
			}
			return this;
		}

		// Token: 0x06005CC9 RID: 23753 RVA: 0x0016A01A File Offset: 0x0016821A
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ArrowExpressionClauseSyntax(base.Kind, this.arrowToken, this.expression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005CCA RID: 23754 RVA: 0x0016A03A File Offset: 0x0016823A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ArrowExpressionClauseSyntax(base.Kind, this.arrowToken, this.expression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005CCB RID: 23755 RVA: 0x0016A05C File Offset: 0x0016825C
		internal ArrowExpressionClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.arrowToken = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
		}

		// Token: 0x06005CCC RID: 23756 RVA: 0x0016A0AB File Offset: 0x001682AB
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.arrowToken);
			writer.WriteValue(this.expression);
		}

		// Token: 0x06005CCD RID: 23757 RVA: 0x0016A0CC File Offset: 0x001682CC
		static ArrowExpressionClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ArrowExpressionClauseSyntax), (ObjectReader r) => new ArrowExpressionClauseSyntax(r));
		}

		// Token: 0x04001879 RID: 6265
		internal readonly SyntaxToken arrowToken;

		// Token: 0x0400187A RID: 6266
		internal readonly ExpressionSyntax expression;
	}
}
