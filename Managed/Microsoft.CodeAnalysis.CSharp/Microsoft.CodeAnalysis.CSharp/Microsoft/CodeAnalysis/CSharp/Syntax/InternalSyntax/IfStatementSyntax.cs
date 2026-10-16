using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000407 RID: 1031
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class IfStatementSyntax : StatementSyntax
	{
		// Token: 0x060058F7 RID: 22775 RVA: 0x0015D248 File Offset: 0x0015B448
		internal IfStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken ifKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(2)] ElseClauseSyntax @else, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 7;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(ifKeyword);
			this.ifKeyword = ifKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
			if (@else != null)
			{
				base.AdjustFlagsAndWidth(@else);
				this.@else = @else;
			}
		}

		// Token: 0x060058F8 RID: 22776 RVA: 0x0015D2DC File Offset: 0x0015B4DC
		internal IfStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken ifKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(2)] ElseClauseSyntax @else, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 7;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(ifKeyword);
			this.ifKeyword = ifKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
			if (@else != null)
			{
				base.AdjustFlagsAndWidth(@else);
				this.@else = @else;
			}
		}

		// Token: 0x060058F9 RID: 22777 RVA: 0x0015D374 File Offset: 0x0015B574
		internal IfStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken ifKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(2)] ElseClauseSyntax @else)
			: base(kind)
		{
			base.SlotCount = 7;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(ifKeyword);
			this.ifKeyword = ifKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
			if (@else != null)
			{
				base.AdjustFlagsAndWidth(@else);
				this.@else = @else;
			}
		}

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x060058FA RID: 22778 RVA: 0x0015D402 File Offset: 0x0015B602
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x060058FB RID: 22779 RVA: 0x0015D40F File Offset: 0x0015B60F
		public SyntaxToken IfKeyword
		{
			get
			{
				return this.ifKeyword;
			}
		}

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x060058FC RID: 22780 RVA: 0x0015D417 File Offset: 0x0015B617
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x060058FD RID: 22781 RVA: 0x0015D41F File Offset: 0x0015B61F
		public ExpressionSyntax Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x060058FE RID: 22782 RVA: 0x0015D427 File Offset: 0x0015B627
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x060058FF RID: 22783 RVA: 0x0015D42F File Offset: 0x0015B62F
		public StatementSyntax Statement
		{
			get
			{
				return this.statement;
			}
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06005900 RID: 22784 RVA: 0x0015D437 File Offset: 0x0015B637
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ElseClauseSyntax Else
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.@else;
			}
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x0015D440 File Offset: 0x0015B640
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
				greenNode = this.ifKeyword;
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
			case 6:
				greenNode = this.@else;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005902 RID: 22786 RVA: 0x0015D4B3 File Offset: 0x0015B6B3
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new IfStatementSyntax(this, parent, position);
		}

		// Token: 0x06005903 RID: 22787 RVA: 0x0015D4BD File Offset: 0x0015B6BD
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitIfStatement(this);
		}

		// Token: 0x06005904 RID: 22788 RVA: 0x0015D4C6 File Offset: 0x0015B6C6
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitIfStatement(this);
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x0015D4D0 File Offset: 0x0015B6D0
		public IfStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken ifKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, ElseClauseSyntax @else)
		{
			if (attributeLists != this.AttributeLists || ifKeyword != this.IfKeyword || openParenToken != this.OpenParenToken || condition != this.Condition || closeParenToken != this.CloseParenToken || statement != this.Statement || @else != this.Else)
			{
				IfStatementSyntax ifStatementSyntax = SyntaxFactory.IfStatement(attributeLists, ifKeyword, openParenToken, condition, closeParenToken, statement, @else);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					ifStatementSyntax = ifStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					ifStatementSyntax = ifStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return ifStatementSyntax;
			}
			return this;
		}

		// Token: 0x06005906 RID: 22790 RVA: 0x0015D568 File Offset: 0x0015B768
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new IfStatementSyntax(base.Kind, this.attributeLists, this.ifKeyword, this.openParenToken, this.condition, this.closeParenToken, this.statement, this.@else, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005907 RID: 22791 RVA: 0x0015D5B4 File Offset: 0x0015B7B4
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new IfStatementSyntax(base.Kind, this.attributeLists, this.ifKeyword, this.openParenToken, this.condition, this.closeParenToken, this.statement, this.@else, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005908 RID: 22792 RVA: 0x0015D600 File Offset: 0x0015B800
		internal IfStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 7;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.ifKeyword = syntaxToken;
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
			ElseClauseSyntax elseClauseSyntax = (ElseClauseSyntax)reader.ReadValue();
			if (elseClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(elseClauseSyntax);
				this.@else = elseClauseSyntax;
			}
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x0015D6E4 File Offset: 0x0015B8E4
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.ifKeyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.condition);
			writer.WriteValue(this.closeParenToken);
			writer.WriteValue(this.statement);
			writer.WriteValue(this.@else);
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x0015D74C File Offset: 0x0015B94C
		static IfStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(IfStatementSyntax), (ObjectReader r) => new IfStatementSyntax(r));
		}

		// Token: 0x0400177A RID: 6010
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x0400177B RID: 6011
		internal readonly SyntaxToken ifKeyword;

		// Token: 0x0400177C RID: 6012
		internal readonly SyntaxToken openParenToken;

		// Token: 0x0400177D RID: 6013
		internal readonly ExpressionSyntax condition;

		// Token: 0x0400177E RID: 6014
		internal readonly SyntaxToken closeParenToken;

		// Token: 0x0400177F RID: 6015
		internal readonly StatementSyntax statement;

		// Token: 0x04001780 RID: 6016
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ElseClauseSyntax @else;
	}
}
