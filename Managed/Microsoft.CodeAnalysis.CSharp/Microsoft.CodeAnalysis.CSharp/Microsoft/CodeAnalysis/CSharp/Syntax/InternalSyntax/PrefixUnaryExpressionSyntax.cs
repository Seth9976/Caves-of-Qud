using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000390 RID: 912
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PrefixUnaryExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060051E5 RID: 20965 RVA: 0x0014919A File Offset: 0x0014739A
		internal PrefixUnaryExpressionSyntax(SyntaxKind kind, SyntaxToken operatorToken, ExpressionSyntax operand, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(operand);
			this.operand = operand;
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x001491CA File Offset: 0x001473CA
		internal PrefixUnaryExpressionSyntax(SyntaxKind kind, SyntaxToken operatorToken, ExpressionSyntax operand, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(operand);
			this.operand = operand;
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x001491FE File Offset: 0x001473FE
		internal PrefixUnaryExpressionSyntax(SyntaxKind kind, SyntaxToken operatorToken, ExpressionSyntax operand)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(operand);
			this.operand = operand;
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x060051E8 RID: 20968 RVA: 0x0014922A File Offset: 0x0014742A
		public SyntaxToken OperatorToken
		{
			get
			{
				return this.operatorToken;
			}
		}

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x060051E9 RID: 20969 RVA: 0x00149232 File Offset: 0x00147432
		public ExpressionSyntax Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x0014923C File Offset: 0x0014743C
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
					greenNode = this.operand;
				}
			}
			else
			{
				greenNode = this.operatorToken;
			}
			return greenNode;
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x00149267 File Offset: 0x00147467
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new PrefixUnaryExpressionSyntax(this, parent, position);
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x00149271 File Offset: 0x00147471
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPrefixUnaryExpression(this);
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x0014927A File Offset: 0x0014747A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPrefixUnaryExpression(this);
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x00149284 File Offset: 0x00147484
		public PrefixUnaryExpressionSyntax Update(SyntaxToken operatorToken, ExpressionSyntax operand)
		{
			if (operatorToken != this.OperatorToken || operand != this.Operand)
			{
				PrefixUnaryExpressionSyntax prefixUnaryExpressionSyntax = SyntaxFactory.PrefixUnaryExpression(base.Kind, operatorToken, operand);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					prefixUnaryExpressionSyntax = prefixUnaryExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					prefixUnaryExpressionSyntax = prefixUnaryExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return prefixUnaryExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x001492E0 File Offset: 0x001474E0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new PrefixUnaryExpressionSyntax(base.Kind, this.operatorToken, this.operand, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x00149300 File Offset: 0x00147500
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new PrefixUnaryExpressionSyntax(base.Kind, this.operatorToken, this.operand, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x00149320 File Offset: 0x00147520
		internal PrefixUnaryExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.operatorToken = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.operand = expressionSyntax;
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x0014936F File Offset: 0x0014756F
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.operatorToken);
			writer.WriteValue(this.operand);
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x00149390 File Offset: 0x00147590
		static PrefixUnaryExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(PrefixUnaryExpressionSyntax), (ObjectReader r) => new PrefixUnaryExpressionSyntax(r));
		}

		// Token: 0x0400160C RID: 5644
		internal readonly SyntaxToken operatorToken;

		// Token: 0x0400160D RID: 5645
		internal readonly ExpressionSyntax operand;
	}
}
