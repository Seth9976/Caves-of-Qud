using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000414 RID: 1044
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CatchFilterClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x060059CA RID: 22986 RVA: 0x0015FBB8 File Offset: 0x0015DDB8
		internal CatchFilterClauseSyntax(SyntaxKind kind, SyntaxToken whenKeyword, SyntaxToken openParenToken, ExpressionSyntax filterExpression, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(whenKeyword);
			this.whenKeyword = whenKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(filterExpression);
			this.filterExpression = filterExpression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x060059CB RID: 22987 RVA: 0x0015FC14 File Offset: 0x0015DE14
		internal CatchFilterClauseSyntax(SyntaxKind kind, SyntaxToken whenKeyword, SyntaxToken openParenToken, ExpressionSyntax filterExpression, SyntaxToken closeParenToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(whenKeyword);
			this.whenKeyword = whenKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(filterExpression);
			this.filterExpression = filterExpression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x060059CC RID: 22988 RVA: 0x0015FC74 File Offset: 0x0015DE74
		internal CatchFilterClauseSyntax(SyntaxKind kind, SyntaxToken whenKeyword, SyntaxToken openParenToken, ExpressionSyntax filterExpression, SyntaxToken closeParenToken)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(whenKeyword);
			this.whenKeyword = whenKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(filterExpression);
			this.filterExpression = filterExpression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x060059CD RID: 22989 RVA: 0x0015FCCB File Offset: 0x0015DECB
		public SyntaxToken WhenKeyword
		{
			get
			{
				return this.whenKeyword;
			}
		}

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x060059CE RID: 22990 RVA: 0x0015FCD3 File Offset: 0x0015DED3
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x060059CF RID: 22991 RVA: 0x0015FCDB File Offset: 0x0015DEDB
		public ExpressionSyntax FilterExpression
		{
			get
			{
				return this.filterExpression;
			}
		}

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x060059D0 RID: 22992 RVA: 0x0015FCE3 File Offset: 0x0015DEE3
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x060059D1 RID: 22993 RVA: 0x0015FCEC File Offset: 0x0015DEEC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.whenKeyword;
				break;
			case 1:
				greenNode = this.openParenToken;
				break;
			case 2:
				greenNode = this.filterExpression;
				break;
			case 3:
				greenNode = this.closeParenToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060059D2 RID: 22994 RVA: 0x0015FD38 File Offset: 0x0015DF38
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new CatchFilterClauseSyntax(this, parent, position);
		}

		// Token: 0x060059D3 RID: 22995 RVA: 0x0015FD42 File Offset: 0x0015DF42
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCatchFilterClause(this);
		}

		// Token: 0x060059D4 RID: 22996 RVA: 0x0015FD4B File Offset: 0x0015DF4B
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCatchFilterClause(this);
		}

		// Token: 0x060059D5 RID: 22997 RVA: 0x0015FD54 File Offset: 0x0015DF54
		public CatchFilterClauseSyntax Update(SyntaxToken whenKeyword, SyntaxToken openParenToken, ExpressionSyntax filterExpression, SyntaxToken closeParenToken)
		{
			if (whenKeyword != this.WhenKeyword || openParenToken != this.OpenParenToken || filterExpression != this.FilterExpression || closeParenToken != this.CloseParenToken)
			{
				CatchFilterClauseSyntax catchFilterClauseSyntax = SyntaxFactory.CatchFilterClause(whenKeyword, openParenToken, filterExpression, closeParenToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					catchFilterClauseSyntax = catchFilterClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					catchFilterClauseSyntax = catchFilterClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return catchFilterClauseSyntax;
			}
			return this;
		}

		// Token: 0x060059D6 RID: 22998 RVA: 0x0015FDC0 File Offset: 0x0015DFC0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new CatchFilterClauseSyntax(base.Kind, this.whenKeyword, this.openParenToken, this.filterExpression, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060059D7 RID: 22999 RVA: 0x0015FDEC File Offset: 0x0015DFEC
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new CatchFilterClauseSyntax(base.Kind, this.whenKeyword, this.openParenToken, this.filterExpression, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060059D8 RID: 23000 RVA: 0x0015FE18 File Offset: 0x0015E018
		internal CatchFilterClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.whenKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.openParenToken = syntaxToken2;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.filterExpression = expressionSyntax;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.closeParenToken = syntaxToken3;
		}

		// Token: 0x060059D9 RID: 23001 RVA: 0x0015FE9B File Offset: 0x0015E09B
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.whenKeyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.filterExpression);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x060059DA RID: 23002 RVA: 0x0015FED4 File Offset: 0x0015E0D4
		static CatchFilterClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(CatchFilterClauseSyntax), (ObjectReader r) => new CatchFilterClauseSyntax(r));
		}

		// Token: 0x040017AC RID: 6060
		internal readonly SyntaxToken whenKeyword;

		// Token: 0x040017AD RID: 6061
		internal readonly SyntaxToken openParenToken;

		// Token: 0x040017AE RID: 6062
		internal readonly ExpressionSyntax filterExpression;

		// Token: 0x040017AF RID: 6063
		internal readonly SyntaxToken closeParenToken;
	}
}
