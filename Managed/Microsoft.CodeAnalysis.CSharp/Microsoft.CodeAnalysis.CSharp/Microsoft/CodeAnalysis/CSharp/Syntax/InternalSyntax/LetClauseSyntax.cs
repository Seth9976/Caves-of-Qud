using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003C6 RID: 966
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class LetClauseSyntax : QueryClauseSyntax
	{
		// Token: 0x060054F9 RID: 21753 RVA: 0x0015160C File Offset: 0x0014F80C
		internal LetClauseSyntax(SyntaxKind kind, SyntaxToken letKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(letKeyword);
			this.letKeyword = letKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x00151668 File Offset: 0x0014F868
		internal LetClauseSyntax(SyntaxKind kind, SyntaxToken letKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(letKeyword);
			this.letKeyword = letKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x001516C8 File Offset: 0x0014F8C8
		internal LetClauseSyntax(SyntaxKind kind, SyntaxToken letKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(letKeyword);
			this.letKeyword = letKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x060054FC RID: 21756 RVA: 0x0015171F File Offset: 0x0014F91F
		public SyntaxToken LetKeyword
		{
			get
			{
				return this.letKeyword;
			}
		}

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x060054FD RID: 21757 RVA: 0x00151727 File Offset: 0x0014F927
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x060054FE RID: 21758 RVA: 0x0015172F File Offset: 0x0014F92F
		public SyntaxToken EqualsToken
		{
			get
			{
				return this.equalsToken;
			}
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x060054FF RID: 21759 RVA: 0x00151737 File Offset: 0x0014F937
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x00151740 File Offset: 0x0014F940
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.letKeyword;
				break;
			case 1:
				greenNode = this.identifier;
				break;
			case 2:
				greenNode = this.equalsToken;
				break;
			case 3:
				greenNode = this.expression;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x0015178C File Offset: 0x0014F98C
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new LetClauseSyntax(this, parent, position);
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x00151796 File Offset: 0x0014F996
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLetClause(this);
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x0015179F File Offset: 0x0014F99F
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLetClause(this);
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x001517A8 File Offset: 0x0014F9A8
		public LetClauseSyntax Update(SyntaxToken letKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression)
		{
			if (letKeyword != this.LetKeyword || identifier != this.Identifier || equalsToken != this.EqualsToken || expression != this.Expression)
			{
				LetClauseSyntax letClauseSyntax = SyntaxFactory.LetClause(letKeyword, identifier, equalsToken, expression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					letClauseSyntax = letClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					letClauseSyntax = letClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return letClauseSyntax;
			}
			return this;
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x00151814 File Offset: 0x0014FA14
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new LetClauseSyntax(base.Kind, this.letKeyword, this.identifier, this.equalsToken, this.expression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x00151840 File Offset: 0x0014FA40
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new LetClauseSyntax(base.Kind, this.letKeyword, this.identifier, this.equalsToken, this.expression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x0015186C File Offset: 0x0014FA6C
		internal LetClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.letKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.identifier = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.equalsToken = syntaxToken3;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x001518EF File Offset: 0x0014FAEF
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.letKeyword);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.equalsToken);
			writer.WriteValue(this.expression);
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x00151928 File Offset: 0x0014FB28
		static LetClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(LetClauseSyntax), (ObjectReader r) => new LetClauseSyntax(r));
		}

		// Token: 0x0400169F RID: 5791
		internal readonly SyntaxToken letKeyword;

		// Token: 0x040016A0 RID: 5792
		internal readonly SyntaxToken identifier;

		// Token: 0x040016A1 RID: 5793
		internal readonly SyntaxToken equalsToken;

		// Token: 0x040016A2 RID: 5794
		internal readonly ExpressionSyntax expression;
	}
}
