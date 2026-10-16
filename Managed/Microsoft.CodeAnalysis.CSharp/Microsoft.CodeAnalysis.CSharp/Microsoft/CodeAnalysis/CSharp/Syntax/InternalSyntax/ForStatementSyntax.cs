using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003FE RID: 1022
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ForStatementSyntax : StatementSyntax
	{
		// Token: 0x0600584D RID: 22605 RVA: 0x0015A924 File Offset: 0x00158B24
		internal ForStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken forKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode initializers, SyntaxToken firstSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax condition, SyntaxToken secondSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode incrementors, SyntaxToken closeParenToken, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 11;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(forKeyword);
			this.forKeyword = forKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (declaration != null)
			{
				base.AdjustFlagsAndWidth(declaration);
				this.declaration = declaration;
			}
			if (initializers != null)
			{
				base.AdjustFlagsAndWidth(initializers);
				this.initializers = initializers;
			}
			base.AdjustFlagsAndWidth(firstSemicolonToken);
			this.firstSemicolonToken = firstSemicolonToken;
			if (condition != null)
			{
				base.AdjustFlagsAndWidth(condition);
				this.condition = condition;
			}
			base.AdjustFlagsAndWidth(secondSemicolonToken);
			this.secondSemicolonToken = secondSemicolonToken;
			if (incrementors != null)
			{
				base.AdjustFlagsAndWidth(incrementors);
				this.incrementors = incrementors;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x0600584E RID: 22606 RVA: 0x0015AA04 File Offset: 0x00158C04
		internal ForStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken forKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode initializers, SyntaxToken firstSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax condition, SyntaxToken secondSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode incrementors, SyntaxToken closeParenToken, StatementSyntax statement, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 11;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(forKeyword);
			this.forKeyword = forKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (declaration != null)
			{
				base.AdjustFlagsAndWidth(declaration);
				this.declaration = declaration;
			}
			if (initializers != null)
			{
				base.AdjustFlagsAndWidth(initializers);
				this.initializers = initializers;
			}
			base.AdjustFlagsAndWidth(firstSemicolonToken);
			this.firstSemicolonToken = firstSemicolonToken;
			if (condition != null)
			{
				base.AdjustFlagsAndWidth(condition);
				this.condition = condition;
			}
			base.AdjustFlagsAndWidth(secondSemicolonToken);
			this.secondSemicolonToken = secondSemicolonToken;
			if (incrementors != null)
			{
				base.AdjustFlagsAndWidth(incrementors);
				this.incrementors = incrementors;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x0600584F RID: 22607 RVA: 0x0015AAE8 File Offset: 0x00158CE8
		internal ForStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken forKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode initializers, SyntaxToken firstSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax condition, SyntaxToken secondSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode incrementors, SyntaxToken closeParenToken, StatementSyntax statement)
			: base(kind)
		{
			base.SlotCount = 11;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(forKeyword);
			this.forKeyword = forKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (declaration != null)
			{
				base.AdjustFlagsAndWidth(declaration);
				this.declaration = declaration;
			}
			if (initializers != null)
			{
				base.AdjustFlagsAndWidth(initializers);
				this.initializers = initializers;
			}
			base.AdjustFlagsAndWidth(firstSemicolonToken);
			this.firstSemicolonToken = firstSemicolonToken;
			if (condition != null)
			{
				base.AdjustFlagsAndWidth(condition);
				this.condition = condition;
			}
			base.AdjustFlagsAndWidth(secondSemicolonToken);
			this.secondSemicolonToken = secondSemicolonToken;
			if (incrementors != null)
			{
				base.AdjustFlagsAndWidth(incrementors);
				this.incrementors = incrementors;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x06005850 RID: 22608 RVA: 0x0015ABC3 File Offset: 0x00158DC3
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x06005851 RID: 22609 RVA: 0x0015ABD0 File Offset: 0x00158DD0
		public SyntaxToken ForKeyword
		{
			get
			{
				return this.forKeyword;
			}
		}

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x06005852 RID: 22610 RVA: 0x0015ABD8 File Offset: 0x00158DD8
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06005853 RID: 22611 RVA: 0x0015ABE0 File Offset: 0x00158DE0
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public VariableDeclarationSyntax Declaration
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.declaration;
			}
		}

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x06005854 RID: 22612 RVA: 0x0015ABE8 File Offset: 0x00158DE8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<ExpressionSyntax> Initializers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<ExpressionSyntax>(new SyntaxList<CSharpSyntaxNode>(this.initializers));
			}
		}

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x06005855 RID: 22613 RVA: 0x0015ABFF File Offset: 0x00158DFF
		public SyntaxToken FirstSemicolonToken
		{
			get
			{
				return this.firstSemicolonToken;
			}
		}

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x06005856 RID: 22614 RVA: 0x0015AC07 File Offset: 0x00158E07
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExpressionSyntax Condition
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.condition;
			}
		}

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x06005857 RID: 22615 RVA: 0x0015AC0F File Offset: 0x00158E0F
		public SyntaxToken SecondSemicolonToken
		{
			get
			{
				return this.secondSemicolonToken;
			}
		}

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x06005858 RID: 22616 RVA: 0x0015AC17 File Offset: 0x00158E17
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<ExpressionSyntax> Incrementors
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<ExpressionSyntax>(new SyntaxList<CSharpSyntaxNode>(this.incrementors));
			}
		}

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x06005859 RID: 22617 RVA: 0x0015AC2E File Offset: 0x00158E2E
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x0600585A RID: 22618 RVA: 0x0015AC36 File Offset: 0x00158E36
		public StatementSyntax Statement
		{
			get
			{
				return this.statement;
			}
		}

		// Token: 0x0600585B RID: 22619 RVA: 0x0015AC40 File Offset: 0x00158E40
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
				greenNode = this.forKeyword;
				break;
			case 2:
				greenNode = this.openParenToken;
				break;
			case 3:
				greenNode = this.declaration;
				break;
			case 4:
				greenNode = this.initializers;
				break;
			case 5:
				greenNode = this.firstSemicolonToken;
				break;
			case 6:
				greenNode = this.condition;
				break;
			case 7:
				greenNode = this.secondSemicolonToken;
				break;
			case 8:
				greenNode = this.incrementors;
				break;
			case 9:
				greenNode = this.closeParenToken;
				break;
			case 10:
				greenNode = this.statement;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600585C RID: 22620 RVA: 0x0015ACE7 File Offset: 0x00158EE7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ForStatementSyntax(this, parent, position);
		}

		// Token: 0x0600585D RID: 22621 RVA: 0x0015ACF1 File Offset: 0x00158EF1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitForStatement(this);
		}

		// Token: 0x0600585E RID: 22622 RVA: 0x0015ACFA File Offset: 0x00158EFA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitForStatement(this);
		}

		// Token: 0x0600585F RID: 22623 RVA: 0x0015AD04 File Offset: 0x00158F04
		public ForStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken forKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> initializers, SyntaxToken firstSemicolonToken, ExpressionSyntax condition, SyntaxToken secondSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> incrementors, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			if (!(attributeLists != this.AttributeLists) && forKeyword == this.ForKeyword && openParenToken == this.OpenParenToken && declaration == this.Declaration)
			{
				SeparatedSyntaxList<ExpressionSyntax> separatedSyntaxList = this.Initializers;
				if (!((in initializers) != (in separatedSyntaxList)) && firstSemicolonToken == this.FirstSemicolonToken && condition == this.Condition && secondSemicolonToken == this.SecondSemicolonToken)
				{
					SeparatedSyntaxList<ExpressionSyntax> separatedSyntaxList2 = this.Incrementors;
					if (!((in incrementors) != (in separatedSyntaxList2)) && closeParenToken == this.CloseParenToken && statement == this.Statement)
					{
						return this;
					}
				}
			}
			ForStatementSyntax forStatementSyntax = SyntaxFactory.ForStatement(attributeLists, forKeyword, openParenToken, declaration, initializers, firstSemicolonToken, condition, secondSemicolonToken, incrementors, closeParenToken, statement);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				forStatementSyntax = forStatementSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				forStatementSyntax = forStatementSyntax.WithAnnotationsGreen(annotations);
			}
			return forStatementSyntax;
		}

		// Token: 0x06005860 RID: 22624 RVA: 0x0015ADE0 File Offset: 0x00158FE0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ForStatementSyntax(base.Kind, this.attributeLists, this.forKeyword, this.openParenToken, this.declaration, this.initializers, this.firstSemicolonToken, this.condition, this.secondSemicolonToken, this.incrementors, this.closeParenToken, this.statement, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005861 RID: 22625 RVA: 0x0015AE44 File Offset: 0x00159044
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ForStatementSyntax(base.Kind, this.attributeLists, this.forKeyword, this.openParenToken, this.declaration, this.initializers, this.firstSemicolonToken, this.condition, this.secondSemicolonToken, this.incrementors, this.closeParenToken, this.statement, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005862 RID: 22626 RVA: 0x0015AEA8 File Offset: 0x001590A8
		internal ForStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 11;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.forKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.openParenToken = syntaxToken2;
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)reader.ReadValue();
			if (variableDeclarationSyntax != null)
			{
				base.AdjustFlagsAndWidth(variableDeclarationSyntax);
				this.declaration = variableDeclarationSyntax;
			}
			GreenNode greenNode2 = (GreenNode)reader.ReadValue();
			if (greenNode2 != null)
			{
				base.AdjustFlagsAndWidth(greenNode2);
				this.initializers = greenNode2;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.firstSemicolonToken = syntaxToken3;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			if (expressionSyntax != null)
			{
				base.AdjustFlagsAndWidth(expressionSyntax);
				this.condition = expressionSyntax;
			}
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.secondSemicolonToken = syntaxToken4;
			GreenNode greenNode3 = (GreenNode)reader.ReadValue();
			if (greenNode3 != null)
			{
				base.AdjustFlagsAndWidth(greenNode3);
				this.incrementors = greenNode3;
			}
			SyntaxToken syntaxToken5 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken5);
			this.closeParenToken = syntaxToken5;
			StatementSyntax statementSyntax = (StatementSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(statementSyntax);
			this.statement = statementSyntax;
		}

		// Token: 0x06005863 RID: 22627 RVA: 0x0015B00C File Offset: 0x0015920C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.forKeyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.declaration);
			writer.WriteValue(this.initializers);
			writer.WriteValue(this.firstSemicolonToken);
			writer.WriteValue(this.condition);
			writer.WriteValue(this.secondSemicolonToken);
			writer.WriteValue(this.incrementors);
			writer.WriteValue(this.closeParenToken);
			writer.WriteValue(this.statement);
		}

		// Token: 0x06005864 RID: 22628 RVA: 0x0015B0A4 File Offset: 0x001592A4
		static ForStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ForStatementSyntax), (ObjectReader r) => new ForStatementSyntax(r));
		}

		// Token: 0x04001742 RID: 5954
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001743 RID: 5955
		internal readonly SyntaxToken forKeyword;

		// Token: 0x04001744 RID: 5956
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001745 RID: 5957
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly VariableDeclarationSyntax declaration;

		// Token: 0x04001746 RID: 5958
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode initializers;

		// Token: 0x04001747 RID: 5959
		internal readonly SyntaxToken firstSemicolonToken;

		// Token: 0x04001748 RID: 5960
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ExpressionSyntax condition;

		// Token: 0x04001749 RID: 5961
		internal readonly SyntaxToken secondSemicolonToken;

		// Token: 0x0400174A RID: 5962
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode incrementors;

		// Token: 0x0400174B RID: 5963
		internal readonly SyntaxToken closeParenToken;

		// Token: 0x0400174C RID: 5964
		internal readonly StatementSyntax statement;
	}
}
