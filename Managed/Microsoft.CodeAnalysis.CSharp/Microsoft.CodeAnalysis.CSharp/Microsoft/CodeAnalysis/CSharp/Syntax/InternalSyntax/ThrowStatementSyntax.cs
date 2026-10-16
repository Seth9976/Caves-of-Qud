using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003FA RID: 1018
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ThrowStatementSyntax : StatementSyntax
	{
		// Token: 0x06005802 RID: 22530 RVA: 0x00159794 File Offset: 0x00157994
		internal ThrowStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken throwKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(throwKeyword);
			this.throwKeyword = throwKeyword;
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005803 RID: 22531 RVA: 0x001597F8 File Offset: 0x001579F8
		internal ThrowStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken throwKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(throwKeyword);
			this.throwKeyword = throwKeyword;
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005804 RID: 22532 RVA: 0x00159860 File Offset: 0x00157A60
		internal ThrowStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken throwKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(throwKeyword);
			this.throwKeyword = throwKeyword;
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x06005805 RID: 22533 RVA: 0x001598BE File Offset: 0x00157ABE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x06005806 RID: 22534 RVA: 0x001598CB File Offset: 0x00157ACB
		public SyntaxToken ThrowKeyword
		{
			get
			{
				return this.throwKeyword;
			}
		}

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x06005807 RID: 22535 RVA: 0x001598D3 File Offset: 0x00157AD3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExpressionSyntax Expression
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x06005808 RID: 22536 RVA: 0x001598DB File Offset: 0x00157ADB
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005809 RID: 22537 RVA: 0x001598E4 File Offset: 0x00157AE4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.attributeLists;
				break;
			case 1:
				greenNode = this.throwKeyword;
				break;
			case 2:
				greenNode = this.expression;
				break;
			case 3:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600580A RID: 22538 RVA: 0x00159930 File Offset: 0x00157B30
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ThrowStatementSyntax(this, parent, position);
		}

		// Token: 0x0600580B RID: 22539 RVA: 0x0015993A File Offset: 0x00157B3A
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitThrowStatement(this);
		}

		// Token: 0x0600580C RID: 22540 RVA: 0x00159943 File Offset: 0x00157B43
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitThrowStatement(this);
		}

		// Token: 0x0600580D RID: 22541 RVA: 0x0015994C File Offset: 0x00157B4C
		public ThrowStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken throwKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || throwKeyword != this.ThrowKeyword || expression != this.Expression || semicolonToken != this.SemicolonToken)
			{
				ThrowStatementSyntax throwStatementSyntax = SyntaxFactory.ThrowStatement(attributeLists, throwKeyword, expression, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					throwStatementSyntax = throwStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					throwStatementSyntax = throwStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return throwStatementSyntax;
			}
			return this;
		}

		// Token: 0x0600580E RID: 22542 RVA: 0x001599BD File Offset: 0x00157BBD
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ThrowStatementSyntax(base.Kind, this.attributeLists, this.throwKeyword, this.expression, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600580F RID: 22543 RVA: 0x001599E9 File Offset: 0x00157BE9
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ThrowStatementSyntax(base.Kind, this.attributeLists, this.throwKeyword, this.expression, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005810 RID: 22544 RVA: 0x00159A18 File Offset: 0x00157C18
		internal ThrowStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.throwKeyword = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			if (expressionSyntax != null)
			{
				base.AdjustFlagsAndWidth(expressionSyntax);
				this.expression = expressionSyntax;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.semicolonToken = syntaxToken2;
		}

		// Token: 0x06005811 RID: 22545 RVA: 0x00159AA1 File Offset: 0x00157CA1
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.throwKeyword);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005812 RID: 22546 RVA: 0x00159ADA File Offset: 0x00157CDA
		static ThrowStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ThrowStatementSyntax), (ObjectReader r) => new ThrowStatementSyntax(r));
		}

		// Token: 0x0400172B RID: 5931
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x0400172C RID: 5932
		internal readonly SyntaxToken throwKeyword;

		// Token: 0x0400172D RID: 5933
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ExpressionSyntax expression;

		// Token: 0x0400172E RID: 5934
		internal readonly SyntaxToken semicolonToken;
	}
}
