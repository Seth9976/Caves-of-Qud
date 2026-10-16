using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003FD RID: 1021
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DoStatementSyntax : StatementSyntax
	{
		// Token: 0x06005838 RID: 22584 RVA: 0x0015A38C File Offset: 0x0015858C
		internal DoStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 8;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(doKeyword);
			this.doKeyword = doKeyword;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
			base.AdjustFlagsAndWidth(whileKeyword);
			this.whileKeyword = whileKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x0015A42C File Offset: 0x0015862C
		internal DoStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 8;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(doKeyword);
			this.doKeyword = doKeyword;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
			base.AdjustFlagsAndWidth(whileKeyword);
			this.whileKeyword = whileKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x0015A4D0 File Offset: 0x001586D0
		internal DoStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 8;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(doKeyword);
			this.doKeyword = doKeyword;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
			base.AdjustFlagsAndWidth(whileKeyword);
			this.whileKeyword = whileKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x0600583B RID: 22587 RVA: 0x0015A56A File Offset: 0x0015876A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x0600583C RID: 22588 RVA: 0x0015A577 File Offset: 0x00158777
		public SyntaxToken DoKeyword
		{
			get
			{
				return this.doKeyword;
			}
		}

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x0600583D RID: 22589 RVA: 0x0015A57F File Offset: 0x0015877F
		public StatementSyntax Statement
		{
			get
			{
				return this.statement;
			}
		}

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x0600583E RID: 22590 RVA: 0x0015A587 File Offset: 0x00158787
		public SyntaxToken WhileKeyword
		{
			get
			{
				return this.whileKeyword;
			}
		}

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x0600583F RID: 22591 RVA: 0x0015A58F File Offset: 0x0015878F
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x06005840 RID: 22592 RVA: 0x0015A597 File Offset: 0x00158797
		public ExpressionSyntax Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x06005841 RID: 22593 RVA: 0x0015A59F File Offset: 0x0015879F
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x06005842 RID: 22594 RVA: 0x0015A5A7 File Offset: 0x001587A7
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005843 RID: 22595 RVA: 0x0015A5B0 File Offset: 0x001587B0
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
				greenNode = this.doKeyword;
				break;
			case 2:
				greenNode = this.statement;
				break;
			case 3:
				greenNode = this.whileKeyword;
				break;
			case 4:
				greenNode = this.openParenToken;
				break;
			case 5:
				greenNode = this.condition;
				break;
			case 6:
				greenNode = this.closeParenToken;
				break;
			case 7:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005844 RID: 22596 RVA: 0x0015A630 File Offset: 0x00158830
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new DoStatementSyntax(this, parent, position);
		}

		// Token: 0x06005845 RID: 22597 RVA: 0x0015A63A File Offset: 0x0015883A
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDoStatement(this);
		}

		// Token: 0x06005846 RID: 22598 RVA: 0x0015A643 File Offset: 0x00158843
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDoStatement(this);
		}

		// Token: 0x06005847 RID: 22599 RVA: 0x0015A64C File Offset: 0x0015884C
		public DoStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || doKeyword != this.DoKeyword || statement != this.Statement || whileKeyword != this.WhileKeyword || openParenToken != this.OpenParenToken || condition != this.Condition || closeParenToken != this.CloseParenToken || semicolonToken != this.SemicolonToken)
			{
				DoStatementSyntax doStatementSyntax = SyntaxFactory.DoStatement(attributeLists, doKeyword, statement, whileKeyword, openParenToken, condition, closeParenToken, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					doStatementSyntax = doStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					doStatementSyntax = doStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return doStatementSyntax;
			}
			return this;
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x0015A6F0 File Offset: 0x001588F0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new DoStatementSyntax(base.Kind, this.attributeLists, this.doKeyword, this.statement, this.whileKeyword, this.openParenToken, this.condition, this.closeParenToken, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005849 RID: 22601 RVA: 0x0015A740 File Offset: 0x00158940
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new DoStatementSyntax(base.Kind, this.attributeLists, this.doKeyword, this.statement, this.whileKeyword, this.openParenToken, this.condition, this.closeParenToken, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600584A RID: 22602 RVA: 0x0015A790 File Offset: 0x00158990
		internal DoStatementSyntax(ObjectReader reader)
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
			base.AdjustFlagsAndWidth(syntaxToken);
			this.doKeyword = syntaxToken;
			StatementSyntax statementSyntax = (StatementSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(statementSyntax);
			this.statement = statementSyntax;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.whileKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.openParenToken = syntaxToken3;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.condition = expressionSyntax;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.closeParenToken = syntaxToken4;
			SyntaxToken syntaxToken5 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken5);
			this.semicolonToken = syntaxToken5;
		}

		// Token: 0x0600584B RID: 22603 RVA: 0x0015A88C File Offset: 0x00158A8C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.doKeyword);
			writer.WriteValue(this.statement);
			writer.WriteValue(this.whileKeyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.condition);
			writer.WriteValue(this.closeParenToken);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x0600584C RID: 22604 RVA: 0x0015A900 File Offset: 0x00158B00
		static DoStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(DoStatementSyntax), (ObjectReader r) => new DoStatementSyntax(r));
		}

		// Token: 0x0400173A RID: 5946
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x0400173B RID: 5947
		internal readonly SyntaxToken doKeyword;

		// Token: 0x0400173C RID: 5948
		internal readonly StatementSyntax statement;

		// Token: 0x0400173D RID: 5949
		internal readonly SyntaxToken whileKeyword;

		// Token: 0x0400173E RID: 5950
		internal readonly SyntaxToken openParenToken;

		// Token: 0x0400173F RID: 5951
		internal readonly ExpressionSyntax condition;

		// Token: 0x04001740 RID: 5952
		internal readonly SyntaxToken closeParenToken;

		// Token: 0x04001741 RID: 5953
		internal readonly SyntaxToken semicolonToken;
	}
}
