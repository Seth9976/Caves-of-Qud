using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000406 RID: 1030
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class LockStatementSyntax : StatementSyntax
	{
		// Token: 0x060058E4 RID: 22756 RVA: 0x0015CDBC File Offset: 0x0015AFBC
		internal LockStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken lockKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 6;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(lockKeyword);
			this.lockKeyword = lockKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x0015CE3C File Offset: 0x0015B03C
		internal LockStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken lockKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 6;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(lockKeyword);
			this.lockKeyword = lockKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x0015CEC0 File Offset: 0x0015B0C0
		internal LockStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken lockKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
			: base(kind)
		{
			base.SlotCount = 6;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(lockKeyword);
			this.lockKeyword = lockKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x060058E7 RID: 22759 RVA: 0x0015CF3A File Offset: 0x0015B13A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x060058E8 RID: 22760 RVA: 0x0015CF47 File Offset: 0x0015B147
		public SyntaxToken LockKeyword
		{
			get
			{
				return this.lockKeyword;
			}
		}

		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x060058E9 RID: 22761 RVA: 0x0015CF4F File Offset: 0x0015B14F
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x060058EA RID: 22762 RVA: 0x0015CF57 File Offset: 0x0015B157
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x060058EB RID: 22763 RVA: 0x0015CF5F File Offset: 0x0015B15F
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x060058EC RID: 22764 RVA: 0x0015CF67 File Offset: 0x0015B167
		public StatementSyntax Statement
		{
			get
			{
				return this.statement;
			}
		}

		// Token: 0x060058ED RID: 22765 RVA: 0x0015CF70 File Offset: 0x0015B170
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
				greenNode = this.lockKeyword;
				break;
			case 2:
				greenNode = this.openParenToken;
				break;
			case 3:
				greenNode = this.expression;
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

		// Token: 0x060058EE RID: 22766 RVA: 0x0015CFD6 File Offset: 0x0015B1D6
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new LockStatementSyntax(this, parent, position);
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x0015CFE0 File Offset: 0x0015B1E0
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLockStatement(this);
		}

		// Token: 0x060058F0 RID: 22768 RVA: 0x0015CFE9 File Offset: 0x0015B1E9
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLockStatement(this);
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x0015CFF4 File Offset: 0x0015B1F4
		public LockStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken lockKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			if (attributeLists != this.AttributeLists || lockKeyword != this.LockKeyword || openParenToken != this.OpenParenToken || expression != this.Expression || closeParenToken != this.CloseParenToken || statement != this.Statement)
			{
				LockStatementSyntax lockStatementSyntax = SyntaxFactory.LockStatement(attributeLists, lockKeyword, openParenToken, expression, closeParenToken, statement);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					lockStatementSyntax = lockStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					lockStatementSyntax = lockStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return lockStatementSyntax;
			}
			return this;
		}

		// Token: 0x060058F2 RID: 22770 RVA: 0x0015D080 File Offset: 0x0015B280
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new LockStatementSyntax(base.Kind, this.attributeLists, this.lockKeyword, this.openParenToken, this.expression, this.closeParenToken, this.statement, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060058F3 RID: 22771 RVA: 0x0015D0C4 File Offset: 0x0015B2C4
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new LockStatementSyntax(base.Kind, this.attributeLists, this.lockKeyword, this.openParenToken, this.expression, this.closeParenToken, this.statement, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060058F4 RID: 22772 RVA: 0x0015D108 File Offset: 0x0015B308
		internal LockStatementSyntax(ObjectReader reader)
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
			this.lockKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.openParenToken = syntaxToken2;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.closeParenToken = syntaxToken3;
			StatementSyntax statementSyntax = (StatementSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(statementSyntax);
			this.statement = statementSyntax;
		}

		// Token: 0x060058F5 RID: 22773 RVA: 0x0015D1C8 File Offset: 0x0015B3C8
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.lockKeyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.closeParenToken);
			writer.WriteValue(this.statement);
		}

		// Token: 0x060058F6 RID: 22774 RVA: 0x0015D224 File Offset: 0x0015B424
		static LockStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(LockStatementSyntax), (ObjectReader r) => new LockStatementSyntax(r));
		}

		// Token: 0x04001774 RID: 6004
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001775 RID: 6005
		internal readonly SyntaxToken lockKeyword;

		// Token: 0x04001776 RID: 6006
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001777 RID: 6007
		internal readonly ExpressionSyntax expression;

		// Token: 0x04001778 RID: 6008
		internal readonly SyntaxToken closeParenToken;

		// Token: 0x04001779 RID: 6009
		internal readonly StatementSyntax statement;
	}
}
