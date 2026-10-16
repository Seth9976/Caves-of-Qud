using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003FC RID: 1020
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class WhileStatementSyntax : StatementSyntax
	{
		// Token: 0x06005825 RID: 22565 RVA: 0x00159F00 File Offset: 0x00158100
		internal WhileStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 6;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(whileKeyword);
			this.whileKeyword = whileKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x06005826 RID: 22566 RVA: 0x00159F80 File Offset: 0x00158180
		internal WhileStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 6;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(whileKeyword);
			this.whileKeyword = whileKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x06005827 RID: 22567 RVA: 0x0015A004 File Offset: 0x00158204
		internal WhileStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement)
			: base(kind)
		{
			base.SlotCount = 6;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(whileKeyword);
			this.whileKeyword = whileKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x06005828 RID: 22568 RVA: 0x0015A07E File Offset: 0x0015827E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06005829 RID: 22569 RVA: 0x0015A08B File Offset: 0x0015828B
		public SyntaxToken WhileKeyword
		{
			get
			{
				return this.whileKeyword;
			}
		}

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x0600582A RID: 22570 RVA: 0x0015A093 File Offset: 0x00158293
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x0600582B RID: 22571 RVA: 0x0015A09B File Offset: 0x0015829B
		public ExpressionSyntax Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x0600582C RID: 22572 RVA: 0x0015A0A3 File Offset: 0x001582A3
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x0600582D RID: 22573 RVA: 0x0015A0AB File Offset: 0x001582AB
		public StatementSyntax Statement
		{
			get
			{
				return this.statement;
			}
		}

		// Token: 0x0600582E RID: 22574 RVA: 0x0015A0B4 File Offset: 0x001582B4
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
				greenNode = this.whileKeyword;
				break;
			case 2:
				greenNode = this.openParenToken;
				break;
			case 3:
				greenNode = this.condition;
				break;
			case 4:
				greenNode = this.closeParenToken;
				break;
			case 5:
				greenNode = this.statement;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600582F RID: 22575 RVA: 0x0015A11A File Offset: 0x0015831A
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new WhileStatementSyntax(this, parent, position);
		}

		// Token: 0x06005830 RID: 22576 RVA: 0x0015A124 File Offset: 0x00158324
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitWhileStatement(this);
		}

		// Token: 0x06005831 RID: 22577 RVA: 0x0015A12D File Offset: 0x0015832D
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitWhileStatement(this);
		}

		// Token: 0x06005832 RID: 22578 RVA: 0x0015A138 File Offset: 0x00158338
		public WhileStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			if (attributeLists != this.AttributeLists || whileKeyword != this.WhileKeyword || openParenToken != this.OpenParenToken || condition != this.Condition || closeParenToken != this.CloseParenToken || statement != this.Statement)
			{
				WhileStatementSyntax whileStatementSyntax = SyntaxFactory.WhileStatement(attributeLists, whileKeyword, openParenToken, condition, closeParenToken, statement);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					whileStatementSyntax = whileStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					whileStatementSyntax = whileStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return whileStatementSyntax;
			}
			return this;
		}

		// Token: 0x06005833 RID: 22579 RVA: 0x0015A1C4 File Offset: 0x001583C4
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new WhileStatementSyntax(base.Kind, this.attributeLists, this.whileKeyword, this.openParenToken, this.condition, this.closeParenToken, this.statement, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005834 RID: 22580 RVA: 0x0015A208 File Offset: 0x00158408
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new WhileStatementSyntax(base.Kind, this.attributeLists, this.whileKeyword, this.openParenToken, this.condition, this.closeParenToken, this.statement, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005835 RID: 22581 RVA: 0x0015A24C File Offset: 0x0015844C
		internal WhileStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 6;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.whileKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.openParenToken = syntaxToken2;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.condition = expressionSyntax;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.closeParenToken = syntaxToken3;
			StatementSyntax statementSyntax = (StatementSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(statementSyntax);
			this.statement = statementSyntax;
		}

		// Token: 0x06005836 RID: 22582 RVA: 0x0015A30C File Offset: 0x0015850C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.whileKeyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.condition);
			writer.WriteValue(this.closeParenToken);
			writer.WriteValue(this.statement);
		}

		// Token: 0x06005837 RID: 22583 RVA: 0x0015A368 File Offset: 0x00158568
		static WhileStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(WhileStatementSyntax), (ObjectReader r) => new WhileStatementSyntax(r));
		}

		// Token: 0x04001734 RID: 5940
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001735 RID: 5941
		internal readonly SyntaxToken whileKeyword;

		// Token: 0x04001736 RID: 5942
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001737 RID: 5943
		internal readonly ExpressionSyntax condition;

		// Token: 0x04001738 RID: 5944
		internal readonly SyntaxToken closeParenToken;

		// Token: 0x04001739 RID: 5945
		internal readonly StatementSyntax statement;
	}
}
