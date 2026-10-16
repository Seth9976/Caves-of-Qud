using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003B6 RID: 950
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class InitializerExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06005418 RID: 21528 RVA: 0x0014EF08 File Offset: 0x0014D108
		internal InitializerExpressionSyntax(SyntaxKind kind, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode expressions, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (expressions != null)
			{
				base.AdjustFlagsAndWidth(expressions);
				this.expressions = expressions;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x06005419 RID: 21529 RVA: 0x0014EF58 File Offset: 0x0014D158
		internal InitializerExpressionSyntax(SyntaxKind kind, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode expressions, SyntaxToken closeBraceToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (expressions != null)
			{
				base.AdjustFlagsAndWidth(expressions);
				this.expressions = expressions;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x0600541A RID: 21530 RVA: 0x0014EFAA File Offset: 0x0014D1AA
		internal InitializerExpressionSyntax(SyntaxKind kind, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode expressions, SyntaxToken closeBraceToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (expressions != null)
			{
				base.AdjustFlagsAndWidth(expressions);
				this.expressions = expressions;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x0600541B RID: 21531 RVA: 0x0014EFE9 File Offset: 0x0014D1E9
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return this.openBraceToken;
			}
		}

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x0600541C RID: 21532 RVA: 0x0014EFF1 File Offset: 0x0014D1F1
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<ExpressionSyntax> Expressions
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<ExpressionSyntax>(new SyntaxList<CSharpSyntaxNode>(this.expressions));
			}
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x0600541D RID: 21533 RVA: 0x0014F008 File Offset: 0x0014D208
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return this.closeBraceToken;
			}
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x0014F010 File Offset: 0x0014D210
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.openBraceToken;
				break;
			case 1:
				greenNode = this.expressions;
				break;
			case 2:
				greenNode = this.closeBraceToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600541F RID: 21535 RVA: 0x0014F04F File Offset: 0x0014D24F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new InitializerExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x0014F059 File Offset: 0x0014D259
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInitializerExpression(this);
		}

		// Token: 0x06005421 RID: 21537 RVA: 0x0014F062 File Offset: 0x0014D262
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInitializerExpression(this);
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x0014F06C File Offset: 0x0014D26C
		public InitializerExpressionSyntax Update(SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken)
		{
			if (openBraceToken == this.OpenBraceToken)
			{
				SeparatedSyntaxList<ExpressionSyntax> separatedSyntaxList = this.Expressions;
				if (!((in expressions) != (in separatedSyntaxList)) && closeBraceToken == this.CloseBraceToken)
				{
					return this;
				}
			}
			InitializerExpressionSyntax initializerExpressionSyntax = SyntaxFactory.InitializerExpression(base.Kind, openBraceToken, expressions, closeBraceToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				initializerExpressionSyntax = initializerExpressionSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				initializerExpressionSyntax = initializerExpressionSyntax.WithAnnotationsGreen(annotations);
			}
			return initializerExpressionSyntax;
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x0014F0DB File Offset: 0x0014D2DB
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new InitializerExpressionSyntax(base.Kind, this.openBraceToken, this.expressions, this.closeBraceToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005424 RID: 21540 RVA: 0x0014F101 File Offset: 0x0014D301
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new InitializerExpressionSyntax(base.Kind, this.openBraceToken, this.expressions, this.closeBraceToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005425 RID: 21541 RVA: 0x0014F128 File Offset: 0x0014D328
		internal InitializerExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openBraceToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.expressions = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeBraceToken = syntaxToken2;
		}

		// Token: 0x06005426 RID: 21542 RVA: 0x0014F194 File Offset: 0x0014D394
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openBraceToken);
			writer.WriteValue(this.expressions);
			writer.WriteValue(this.closeBraceToken);
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x0014F1C1 File Offset: 0x0014D3C1
		static InitializerExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(InitializerExpressionSyntax), (ObjectReader r) => new InitializerExpressionSyntax(r));
		}

		// Token: 0x04001673 RID: 5747
		internal readonly SyntaxToken openBraceToken;

		// Token: 0x04001674 RID: 5748
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode expressions;

		// Token: 0x04001675 RID: 5749
		internal readonly SyntaxToken closeBraceToken;
	}
}
