using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200039A RID: 922
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AssignmentExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x0600527D RID: 21117 RVA: 0x0014A7CB File Offset: 0x001489CB
		internal AssignmentExpressionSyntax(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(left);
			this.left = left;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(right);
			this.right = right;
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x0014A80C File Offset: 0x00148A0C
		internal AssignmentExpressionSyntax(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(left);
			this.left = left;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(right);
			this.right = right;
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x0014A85B File Offset: 0x00148A5B
		internal AssignmentExpressionSyntax(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(left);
			this.left = left;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(right);
			this.right = right;
		}

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06005280 RID: 21120 RVA: 0x0014A897 File Offset: 0x00148A97
		public ExpressionSyntax Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x06005281 RID: 21121 RVA: 0x0014A89F File Offset: 0x00148A9F
		public SyntaxToken OperatorToken
		{
			get
			{
				return this.operatorToken;
			}
		}

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06005282 RID: 21122 RVA: 0x0014A8A7 File Offset: 0x00148AA7
		public ExpressionSyntax Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x0014A8B0 File Offset: 0x00148AB0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.left;
				break;
			case 1:
				greenNode = this.operatorToken;
				break;
			case 2:
				greenNode = this.right;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x0014A8EF File Offset: 0x00148AEF
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new AssignmentExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x0014A8F9 File Offset: 0x00148AF9
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAssignmentExpression(this);
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x0014A902 File Offset: 0x00148B02
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAssignmentExpression(this);
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x0014A90C File Offset: 0x00148B0C
		public AssignmentExpressionSyntax Update(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
		{
			if (left != this.Left || operatorToken != this.OperatorToken || right != this.Right)
			{
				AssignmentExpressionSyntax assignmentExpressionSyntax = SyntaxFactory.AssignmentExpression(base.Kind, left, operatorToken, right);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					assignmentExpressionSyntax = assignmentExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					assignmentExpressionSyntax = assignmentExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return assignmentExpressionSyntax;
			}
			return this;
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x0014A972 File Offset: 0x00148B72
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new AssignmentExpressionSyntax(base.Kind, this.left, this.operatorToken, this.right, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x0014A998 File Offset: 0x00148B98
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new AssignmentExpressionSyntax(base.Kind, this.left, this.operatorToken, this.right, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x0014A9C0 File Offset: 0x00148BC0
		internal AssignmentExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.left = expressionSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.operatorToken = syntaxToken;
			ExpressionSyntax expressionSyntax2 = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax2);
			this.right = expressionSyntax2;
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x0014AA29 File Offset: 0x00148C29
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.left);
			writer.WriteValue(this.operatorToken);
			writer.WriteValue(this.right);
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x0014AA56 File Offset: 0x00148C56
		static AssignmentExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(AssignmentExpressionSyntax), (ObjectReader r) => new AssignmentExpressionSyntax(r));
		}

		// Token: 0x04001622 RID: 5666
		internal readonly ExpressionSyntax left;

		// Token: 0x04001623 RID: 5667
		internal readonly SyntaxToken operatorToken;

		// Token: 0x04001624 RID: 5668
		internal readonly ExpressionSyntax right;
	}
}
