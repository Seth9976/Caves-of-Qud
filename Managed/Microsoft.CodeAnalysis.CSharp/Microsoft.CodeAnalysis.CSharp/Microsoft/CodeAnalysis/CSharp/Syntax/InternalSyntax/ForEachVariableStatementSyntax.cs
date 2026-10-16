using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000401 RID: 1025
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ForEachVariableStatementSyntax : CommonForEachStatementSyntax
	{
		// Token: 0x06005886 RID: 22662 RVA: 0x0015B798 File Offset: 0x00159998
		internal ForEachVariableStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 9;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (awaitKeyword != null)
			{
				base.AdjustFlagsAndWidth(awaitKeyword);
				this.awaitKeyword = awaitKeyword;
			}
			base.AdjustFlagsAndWidth(forEachKeyword);
			this.forEachKeyword = forEachKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(variable);
			this.variable = variable;
			base.AdjustFlagsAndWidth(inKeyword);
			this.inKeyword = inKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x06005887 RID: 22663 RVA: 0x0015B84C File Offset: 0x00159A4C
		internal ForEachVariableStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 9;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (awaitKeyword != null)
			{
				base.AdjustFlagsAndWidth(awaitKeyword);
				this.awaitKeyword = awaitKeyword;
			}
			base.AdjustFlagsAndWidth(forEachKeyword);
			this.forEachKeyword = forEachKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(variable);
			this.variable = variable;
			base.AdjustFlagsAndWidth(inKeyword);
			this.inKeyword = inKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x06005888 RID: 22664 RVA: 0x0015B904 File Offset: 0x00159B04
		internal ForEachVariableStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
			: base(kind)
		{
			base.SlotCount = 9;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (awaitKeyword != null)
			{
				base.AdjustFlagsAndWidth(awaitKeyword);
				this.awaitKeyword = awaitKeyword;
			}
			base.AdjustFlagsAndWidth(forEachKeyword);
			this.forEachKeyword = forEachKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(variable);
			this.variable = variable;
			base.AdjustFlagsAndWidth(inKeyword);
			this.inKeyword = inKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x06005889 RID: 22665 RVA: 0x0015B9B2 File Offset: 0x00159BB2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x0600588A RID: 22666 RVA: 0x0015B9BF File Offset: 0x00159BBF
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxToken AwaitKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.awaitKeyword;
			}
		}

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x0600588B RID: 22667 RVA: 0x0015B9C7 File Offset: 0x00159BC7
		public override SyntaxToken ForEachKeyword
		{
			get
			{
				return this.forEachKeyword;
			}
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x0600588C RID: 22668 RVA: 0x0015B9CF File Offset: 0x00159BCF
		public override SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x0600588D RID: 22669 RVA: 0x0015B9D7 File Offset: 0x00159BD7
		public ExpressionSyntax Variable
		{
			get
			{
				return this.variable;
			}
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x0600588E RID: 22670 RVA: 0x0015B9DF File Offset: 0x00159BDF
		public override SyntaxToken InKeyword
		{
			get
			{
				return this.inKeyword;
			}
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x0600588F RID: 22671 RVA: 0x0015B9E7 File Offset: 0x00159BE7
		public override ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x06005890 RID: 22672 RVA: 0x0015B9EF File Offset: 0x00159BEF
		public override SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x06005891 RID: 22673 RVA: 0x0015B9F7 File Offset: 0x00159BF7
		public override StatementSyntax Statement
		{
			get
			{
				return this.statement;
			}
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x0015BA00 File Offset: 0x00159C00
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
				greenNode = this.awaitKeyword;
				break;
			case 2:
				greenNode = this.forEachKeyword;
				break;
			case 3:
				greenNode = this.openParenToken;
				break;
			case 4:
				greenNode = this.variable;
				break;
			case 5:
				greenNode = this.inKeyword;
				break;
			case 6:
				greenNode = this.expression;
				break;
			case 7:
				greenNode = this.closeParenToken;
				break;
			case 8:
				greenNode = this.statement;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x0015BA8D File Offset: 0x00159C8D
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ForEachVariableStatementSyntax(this, parent, position);
		}

		// Token: 0x06005894 RID: 22676 RVA: 0x0015BA97 File Offset: 0x00159C97
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitForEachVariableStatement(this);
		}

		// Token: 0x06005895 RID: 22677 RVA: 0x0015BAA0 File Offset: 0x00159CA0
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitForEachVariableStatement(this);
		}

		// Token: 0x06005896 RID: 22678 RVA: 0x0015BAAC File Offset: 0x00159CAC
		public ForEachVariableStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			if (attributeLists != this.AttributeLists || awaitKeyword != this.AwaitKeyword || forEachKeyword != this.ForEachKeyword || openParenToken != this.OpenParenToken || variable != this.Variable || inKeyword != this.InKeyword || expression != this.Expression || closeParenToken != this.CloseParenToken || statement != this.Statement)
			{
				ForEachVariableStatementSyntax forEachVariableStatementSyntax = SyntaxFactory.ForEachVariableStatement(attributeLists, awaitKeyword, forEachKeyword, openParenToken, variable, inKeyword, expression, closeParenToken, statement);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					forEachVariableStatementSyntax = forEachVariableStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					forEachVariableStatementSyntax = forEachVariableStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return forEachVariableStatementSyntax;
			}
			return this;
		}

		// Token: 0x06005897 RID: 22679 RVA: 0x0015BB5C File Offset: 0x00159D5C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ForEachVariableStatementSyntax(base.Kind, this.attributeLists, this.awaitKeyword, this.forEachKeyword, this.openParenToken, this.variable, this.inKeyword, this.expression, this.closeParenToken, this.statement, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005898 RID: 22680 RVA: 0x0015BBB4 File Offset: 0x00159DB4
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ForEachVariableStatementSyntax(base.Kind, this.attributeLists, this.awaitKeyword, this.forEachKeyword, this.openParenToken, this.variable, this.inKeyword, this.expression, this.closeParenToken, this.statement, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005899 RID: 22681 RVA: 0x0015BC0C File Offset: 0x00159E0C
		internal ForEachVariableStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 9;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			if (syntaxToken != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken);
				this.awaitKeyword = syntaxToken;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.forEachKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.openParenToken = syntaxToken3;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.variable = expressionSyntax;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.inKeyword = syntaxToken4;
			ExpressionSyntax expressionSyntax2 = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax2);
			this.expression = expressionSyntax2;
			SyntaxToken syntaxToken5 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken5);
			this.closeParenToken = syntaxToken5;
			StatementSyntax statementSyntax = (StatementSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(statementSyntax);
			this.statement = statementSyntax;
		}

		// Token: 0x0600589A RID: 22682 RVA: 0x0015BD28 File Offset: 0x00159F28
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.awaitKeyword);
			writer.WriteValue(this.forEachKeyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.variable);
			writer.WriteValue(this.inKeyword);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.closeParenToken);
			writer.WriteValue(this.statement);
		}

		// Token: 0x0600589B RID: 22683 RVA: 0x0015BDA8 File Offset: 0x00159FA8
		static ForEachVariableStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ForEachVariableStatementSyntax), (ObjectReader r) => new ForEachVariableStatementSyntax(r));
		}

		// Token: 0x04001757 RID: 5975
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001758 RID: 5976
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken awaitKeyword;

		// Token: 0x04001759 RID: 5977
		internal readonly SyntaxToken forEachKeyword;

		// Token: 0x0400175A RID: 5978
		internal readonly SyntaxToken openParenToken;

		// Token: 0x0400175B RID: 5979
		internal readonly ExpressionSyntax variable;

		// Token: 0x0400175C RID: 5980
		internal readonly SyntaxToken inKeyword;

		// Token: 0x0400175D RID: 5981
		internal readonly ExpressionSyntax expression;

		// Token: 0x0400175E RID: 5982
		internal readonly SyntaxToken closeParenToken;

		// Token: 0x0400175F RID: 5983
		internal readonly StatementSyntax statement;
	}
}
