using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000402 RID: 1026
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class UsingStatementSyntax : StatementSyntax
	{
		// Token: 0x0600589C RID: 22684 RVA: 0x0015BDCC File Offset: 0x00159FCC
		internal UsingStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 8;
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
			base.AdjustFlagsAndWidth(usingKeyword);
			this.usingKeyword = usingKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (declaration != null)
			{
				base.AdjustFlagsAndWidth(declaration);
				this.declaration = declaration;
			}
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x0600589D RID: 22685 RVA: 0x0015BE78 File Offset: 0x0015A078
		internal UsingStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 8;
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
			base.AdjustFlagsAndWidth(usingKeyword);
			this.usingKeyword = usingKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (declaration != null)
			{
				base.AdjustFlagsAndWidth(declaration);
				this.declaration = declaration;
			}
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x0015BF28 File Offset: 0x0015A128
		internal UsingStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
			: base(kind)
		{
			base.SlotCount = 8;
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
			base.AdjustFlagsAndWidth(usingKeyword);
			this.usingKeyword = usingKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (declaration != null)
			{
				base.AdjustFlagsAndWidth(declaration);
				this.declaration = declaration;
			}
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x0600589F RID: 22687 RVA: 0x0015BFCD File Offset: 0x0015A1CD
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x060058A0 RID: 22688 RVA: 0x0015BFDA File Offset: 0x0015A1DA
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken AwaitKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.awaitKeyword;
			}
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x060058A1 RID: 22689 RVA: 0x0015BFE2 File Offset: 0x0015A1E2
		public SyntaxToken UsingKeyword
		{
			get
			{
				return this.usingKeyword;
			}
		}

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x060058A2 RID: 22690 RVA: 0x0015BFEA File Offset: 0x0015A1EA
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x060058A3 RID: 22691 RVA: 0x0015BFF2 File Offset: 0x0015A1F2
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public VariableDeclarationSyntax Declaration
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.declaration;
			}
		}

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x060058A4 RID: 22692 RVA: 0x0015BFFA File Offset: 0x0015A1FA
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExpressionSyntax Expression
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x060058A5 RID: 22693 RVA: 0x0015C002 File Offset: 0x0015A202
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x060058A6 RID: 22694 RVA: 0x0015C00A File Offset: 0x0015A20A
		public StatementSyntax Statement
		{
			get
			{
				return this.statement;
			}
		}

		// Token: 0x060058A7 RID: 22695 RVA: 0x0015C014 File Offset: 0x0015A214
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
				greenNode = this.usingKeyword;
				break;
			case 3:
				greenNode = this.openParenToken;
				break;
			case 4:
				greenNode = this.declaration;
				break;
			case 5:
				greenNode = this.expression;
				break;
			case 6:
				greenNode = this.closeParenToken;
				break;
			case 7:
				greenNode = this.statement;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060058A8 RID: 22696 RVA: 0x0015C094 File Offset: 0x0015A294
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new UsingStatementSyntax(this, parent, position);
		}

		// Token: 0x060058A9 RID: 22697 RVA: 0x0015C09E File Offset: 0x0015A29E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitUsingStatement(this);
		}

		// Token: 0x060058AA RID: 22698 RVA: 0x0015C0A7 File Offset: 0x0015A2A7
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitUsingStatement(this);
		}

		// Token: 0x060058AB RID: 22699 RVA: 0x0015C0B0 File Offset: 0x0015A2B0
		public UsingStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			if (attributeLists != this.AttributeLists || awaitKeyword != this.AwaitKeyword || usingKeyword != this.UsingKeyword || openParenToken != this.OpenParenToken || declaration != this.Declaration || expression != this.Expression || closeParenToken != this.CloseParenToken || statement != this.Statement)
			{
				UsingStatementSyntax usingStatementSyntax = SyntaxFactory.UsingStatement(attributeLists, awaitKeyword, usingKeyword, openParenToken, declaration, expression, closeParenToken, statement);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					usingStatementSyntax = usingStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					usingStatementSyntax = usingStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return usingStatementSyntax;
			}
			return this;
		}

		// Token: 0x060058AC RID: 22700 RVA: 0x0015C154 File Offset: 0x0015A354
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new UsingStatementSyntax(base.Kind, this.attributeLists, this.awaitKeyword, this.usingKeyword, this.openParenToken, this.declaration, this.expression, this.closeParenToken, this.statement, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060058AD RID: 22701 RVA: 0x0015C1A4 File Offset: 0x0015A3A4
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new UsingStatementSyntax(base.Kind, this.attributeLists, this.awaitKeyword, this.usingKeyword, this.openParenToken, this.declaration, this.expression, this.closeParenToken, this.statement, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060058AE RID: 22702 RVA: 0x0015C1F4 File Offset: 0x0015A3F4
		internal UsingStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 8;
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
			this.usingKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.openParenToken = syntaxToken3;
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)reader.ReadValue();
			if (variableDeclarationSyntax != null)
			{
				base.AdjustFlagsAndWidth(variableDeclarationSyntax);
				this.declaration = variableDeclarationSyntax;
			}
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			if (expressionSyntax != null)
			{
				base.AdjustFlagsAndWidth(expressionSyntax);
				this.expression = expressionSyntax;
			}
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.closeParenToken = syntaxToken4;
			StatementSyntax statementSyntax = (StatementSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(statementSyntax);
			this.statement = statementSyntax;
		}

		// Token: 0x060058AF RID: 22703 RVA: 0x0015C2FC File Offset: 0x0015A4FC
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.awaitKeyword);
			writer.WriteValue(this.usingKeyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.declaration);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.closeParenToken);
			writer.WriteValue(this.statement);
		}

		// Token: 0x060058B0 RID: 22704 RVA: 0x0015C370 File Offset: 0x0015A570
		static UsingStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(UsingStatementSyntax), (ObjectReader r) => new UsingStatementSyntax(r));
		}

		// Token: 0x04001760 RID: 5984
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001761 RID: 5985
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken awaitKeyword;

		// Token: 0x04001762 RID: 5986
		internal readonly SyntaxToken usingKeyword;

		// Token: 0x04001763 RID: 5987
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001764 RID: 5988
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly VariableDeclarationSyntax declaration;

		// Token: 0x04001765 RID: 5989
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ExpressionSyntax expression;

		// Token: 0x04001766 RID: 5990
		internal readonly SyntaxToken closeParenToken;

		// Token: 0x04001767 RID: 5991
		internal readonly StatementSyntax statement;
	}
}
