using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000400 RID: 1024
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ForEachStatementSyntax : CommonForEachStatementSyntax
	{
		// Token: 0x0600586F RID: 22639 RVA: 0x0015B0E4 File Offset: 0x001592E4
		internal ForEachStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 10;
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
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(inKeyword);
			this.inKeyword = inKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x06005870 RID: 22640 RVA: 0x0015B1A8 File Offset: 0x001593A8
		internal ForEachStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 10;
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
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(inKeyword);
			this.inKeyword = inKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x06005871 RID: 22641 RVA: 0x0015B270 File Offset: 0x00159470
		internal ForEachStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
			: base(kind)
		{
			base.SlotCount = 10;
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
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(inKeyword);
			this.inKeyword = inKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x06005872 RID: 22642 RVA: 0x0015B32E File Offset: 0x0015952E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x06005873 RID: 22643 RVA: 0x0015B33B File Offset: 0x0015953B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxToken AwaitKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.awaitKeyword;
			}
		}

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x06005874 RID: 22644 RVA: 0x0015B343 File Offset: 0x00159543
		public override SyntaxToken ForEachKeyword
		{
			get
			{
				return this.forEachKeyword;
			}
		}

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x06005875 RID: 22645 RVA: 0x0015B34B File Offset: 0x0015954B
		public override SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x06005876 RID: 22646 RVA: 0x0015B353 File Offset: 0x00159553
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x06005877 RID: 22647 RVA: 0x0015B35B File Offset: 0x0015955B
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x06005878 RID: 22648 RVA: 0x0015B363 File Offset: 0x00159563
		public override SyntaxToken InKeyword
		{
			get
			{
				return this.inKeyword;
			}
		}

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x06005879 RID: 22649 RVA: 0x0015B36B File Offset: 0x0015956B
		public override ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x0600587A RID: 22650 RVA: 0x0015B373 File Offset: 0x00159573
		public override SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x0600587B RID: 22651 RVA: 0x0015B37B File Offset: 0x0015957B
		public override StatementSyntax Statement
		{
			get
			{
				return this.statement;
			}
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x0015B384 File Offset: 0x00159584
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
				greenNode = this.type;
				break;
			case 5:
				greenNode = this.identifier;
				break;
			case 6:
				greenNode = this.inKeyword;
				break;
			case 7:
				greenNode = this.expression;
				break;
			case 8:
				greenNode = this.closeParenToken;
				break;
			case 9:
				greenNode = this.statement;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600587D RID: 22653 RVA: 0x0015B41E File Offset: 0x0015961E
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ForEachStatementSyntax(this, parent, position);
		}

		// Token: 0x0600587E RID: 22654 RVA: 0x0015B428 File Offset: 0x00159628
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitForEachStatement(this);
		}

		// Token: 0x0600587F RID: 22655 RVA: 0x0015B431 File Offset: 0x00159631
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitForEachStatement(this);
		}

		// Token: 0x06005880 RID: 22656 RVA: 0x0015B43C File Offset: 0x0015963C
		public ForEachStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			if (attributeLists != this.AttributeLists || awaitKeyword != this.AwaitKeyword || forEachKeyword != this.ForEachKeyword || openParenToken != this.OpenParenToken || type != this.Type || identifier != this.Identifier || inKeyword != this.InKeyword || expression != this.Expression || closeParenToken != this.CloseParenToken || statement != this.Statement)
			{
				ForEachStatementSyntax forEachStatementSyntax = SyntaxFactory.ForEachStatement(attributeLists, awaitKeyword, forEachKeyword, openParenToken, type, identifier, inKeyword, expression, closeParenToken, statement);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					forEachStatementSyntax = forEachStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					forEachStatementSyntax = forEachStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return forEachStatementSyntax;
			}
			return this;
		}

		// Token: 0x06005881 RID: 22657 RVA: 0x0015B4F8 File Offset: 0x001596F8
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ForEachStatementSyntax(base.Kind, this.attributeLists, this.awaitKeyword, this.forEachKeyword, this.openParenToken, this.type, this.identifier, this.inKeyword, this.expression, this.closeParenToken, this.statement, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005882 RID: 22658 RVA: 0x0015B554 File Offset: 0x00159754
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ForEachStatementSyntax(base.Kind, this.attributeLists, this.awaitKeyword, this.forEachKeyword, this.openParenToken, this.type, this.identifier, this.inKeyword, this.expression, this.closeParenToken, this.statement, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005883 RID: 22659 RVA: 0x0015B5B0 File Offset: 0x001597B0
		internal ForEachStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 10;
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
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.identifier = syntaxToken4;
			SyntaxToken syntaxToken5 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken5);
			this.inKeyword = syntaxToken5;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			SyntaxToken syntaxToken6 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken6);
			this.closeParenToken = syntaxToken6;
			StatementSyntax statementSyntax = (StatementSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(statementSyntax);
			this.statement = statementSyntax;
		}

		// Token: 0x06005884 RID: 22660 RVA: 0x0015B6E8 File Offset: 0x001598E8
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.awaitKeyword);
			writer.WriteValue(this.forEachKeyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.type);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.inKeyword);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.closeParenToken);
			writer.WriteValue(this.statement);
		}

		// Token: 0x06005885 RID: 22661 RVA: 0x0015B774 File Offset: 0x00159974
		static ForEachStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ForEachStatementSyntax), (ObjectReader r) => new ForEachStatementSyntax(r));
		}

		// Token: 0x0400174D RID: 5965
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x0400174E RID: 5966
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken awaitKeyword;

		// Token: 0x0400174F RID: 5967
		internal readonly SyntaxToken forEachKeyword;

		// Token: 0x04001750 RID: 5968
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001751 RID: 5969
		internal readonly TypeSyntax type;

		// Token: 0x04001752 RID: 5970
		internal readonly SyntaxToken identifier;

		// Token: 0x04001753 RID: 5971
		internal readonly SyntaxToken inKeyword;

		// Token: 0x04001754 RID: 5972
		internal readonly ExpressionSyntax expression;

		// Token: 0x04001755 RID: 5973
		internal readonly SyntaxToken closeParenToken;

		// Token: 0x04001756 RID: 5974
		internal readonly StatementSyntax statement;
	}
}
