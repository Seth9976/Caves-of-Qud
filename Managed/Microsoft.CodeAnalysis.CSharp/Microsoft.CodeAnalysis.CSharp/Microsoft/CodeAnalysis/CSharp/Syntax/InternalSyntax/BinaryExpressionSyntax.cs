using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000399 RID: 921
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BinaryExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x0600526D RID: 21101 RVA: 0x0014A51F File Offset: 0x0014871F
		internal BinaryExpressionSyntax(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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

		// Token: 0x0600526E RID: 21102 RVA: 0x0014A560 File Offset: 0x00148760
		internal BinaryExpressionSyntax(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right, SyntaxFactoryContext context)
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

		// Token: 0x0600526F RID: 21103 RVA: 0x0014A5AF File Offset: 0x001487AF
		internal BinaryExpressionSyntax(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
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

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06005270 RID: 21104 RVA: 0x0014A5EB File Offset: 0x001487EB
		public ExpressionSyntax Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06005271 RID: 21105 RVA: 0x0014A5F3 File Offset: 0x001487F3
		public SyntaxToken OperatorToken
		{
			get
			{
				return this.operatorToken;
			}
		}

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06005272 RID: 21106 RVA: 0x0014A5FB File Offset: 0x001487FB
		public ExpressionSyntax Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x0014A604 File Offset: 0x00148804
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

		// Token: 0x06005274 RID: 21108 RVA: 0x0014A643 File Offset: 0x00148843
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new BinaryExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x0014A64D File Offset: 0x0014884D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBinaryExpression(this);
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x0014A656 File Offset: 0x00148856
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBinaryExpression(this);
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x0014A660 File Offset: 0x00148860
		public BinaryExpressionSyntax Update(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
		{
			if (left != this.Left || operatorToken != this.OperatorToken || right != this.Right)
			{
				BinaryExpressionSyntax binaryExpressionSyntax = SyntaxFactory.BinaryExpression(base.Kind, left, operatorToken, right);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					binaryExpressionSyntax = binaryExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					binaryExpressionSyntax = binaryExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return binaryExpressionSyntax;
			}
			return this;
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x0014A6C6 File Offset: 0x001488C6
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new BinaryExpressionSyntax(base.Kind, this.left, this.operatorToken, this.right, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x0014A6EC File Offset: 0x001488EC
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new BinaryExpressionSyntax(base.Kind, this.left, this.operatorToken, this.right, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x0014A714 File Offset: 0x00148914
		internal BinaryExpressionSyntax(ObjectReader reader)
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

		// Token: 0x0600527B RID: 21115 RVA: 0x0014A77D File Offset: 0x0014897D
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.left);
			writer.WriteValue(this.operatorToken);
			writer.WriteValue(this.right);
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x0014A7AA File Offset: 0x001489AA
		static BinaryExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(BinaryExpressionSyntax), (ObjectReader r) => new BinaryExpressionSyntax(r));
		}

		// Token: 0x0400161F RID: 5663
		internal readonly ExpressionSyntax left;

		// Token: 0x04001620 RID: 5664
		internal readonly SyntaxToken operatorToken;

		// Token: 0x04001621 RID: 5665
		internal readonly ExpressionSyntax right;
	}
}
