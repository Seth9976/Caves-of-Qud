using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000403 RID: 1027
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class FixedStatementSyntax : StatementSyntax
	{
		// Token: 0x060058B1 RID: 22705 RVA: 0x0015C394 File Offset: 0x0015A594
		internal FixedStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken fixedKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SyntaxToken closeParenToken, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 6;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(fixedKeyword);
			this.fixedKeyword = fixedKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(declaration);
			this.declaration = declaration;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x060058B2 RID: 22706 RVA: 0x0015C414 File Offset: 0x0015A614
		internal FixedStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken fixedKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SyntaxToken closeParenToken, StatementSyntax statement, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 6;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(fixedKeyword);
			this.fixedKeyword = fixedKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(declaration);
			this.declaration = declaration;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x060058B3 RID: 22707 RVA: 0x0015C498 File Offset: 0x0015A698
		internal FixedStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken fixedKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SyntaxToken closeParenToken, StatementSyntax statement)
			: base(kind)
		{
			base.SlotCount = 6;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(fixedKeyword);
			this.fixedKeyword = fixedKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(declaration);
			this.declaration = declaration;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x060058B4 RID: 22708 RVA: 0x0015C512 File Offset: 0x0015A712
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x060058B5 RID: 22709 RVA: 0x0015C51F File Offset: 0x0015A71F
		public SyntaxToken FixedKeyword
		{
			get
			{
				return this.fixedKeyword;
			}
		}

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x060058B6 RID: 22710 RVA: 0x0015C527 File Offset: 0x0015A727
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x060058B7 RID: 22711 RVA: 0x0015C52F File Offset: 0x0015A72F
		public VariableDeclarationSyntax Declaration
		{
			get
			{
				return this.declaration;
			}
		}

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x060058B8 RID: 22712 RVA: 0x0015C537 File Offset: 0x0015A737
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x060058B9 RID: 22713 RVA: 0x0015C53F File Offset: 0x0015A73F
		public StatementSyntax Statement
		{
			get
			{
				return this.statement;
			}
		}

		// Token: 0x060058BA RID: 22714 RVA: 0x0015C548 File Offset: 0x0015A748
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
				greenNode = this.fixedKeyword;
				break;
			case 2:
				greenNode = this.openParenToken;
				break;
			case 3:
				greenNode = this.declaration;
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

		// Token: 0x060058BB RID: 22715 RVA: 0x0015C5AE File Offset: 0x0015A7AE
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new FixedStatementSyntax(this, parent, position);
		}

		// Token: 0x060058BC RID: 22716 RVA: 0x0015C5B8 File Offset: 0x0015A7B8
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFixedStatement(this);
		}

		// Token: 0x060058BD RID: 22717 RVA: 0x0015C5C1 File Offset: 0x0015A7C1
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFixedStatement(this);
		}

		// Token: 0x060058BE RID: 22718 RVA: 0x0015C5CC File Offset: 0x0015A7CC
		public FixedStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken fixedKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			if (attributeLists != this.AttributeLists || fixedKeyword != this.FixedKeyword || openParenToken != this.OpenParenToken || declaration != this.Declaration || closeParenToken != this.CloseParenToken || statement != this.Statement)
			{
				FixedStatementSyntax fixedStatementSyntax = SyntaxFactory.FixedStatement(attributeLists, fixedKeyword, openParenToken, declaration, closeParenToken, statement);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					fixedStatementSyntax = fixedStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					fixedStatementSyntax = fixedStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return fixedStatementSyntax;
			}
			return this;
		}

		// Token: 0x060058BF RID: 22719 RVA: 0x0015C658 File Offset: 0x0015A858
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new FixedStatementSyntax(base.Kind, this.attributeLists, this.fixedKeyword, this.openParenToken, this.declaration, this.closeParenToken, this.statement, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060058C0 RID: 22720 RVA: 0x0015C69C File Offset: 0x0015A89C
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new FixedStatementSyntax(base.Kind, this.attributeLists, this.fixedKeyword, this.openParenToken, this.declaration, this.closeParenToken, this.statement, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060058C1 RID: 22721 RVA: 0x0015C6E0 File Offset: 0x0015A8E0
		internal FixedStatementSyntax(ObjectReader reader)
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
			this.fixedKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.openParenToken = syntaxToken2;
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(variableDeclarationSyntax);
			this.declaration = variableDeclarationSyntax;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.closeParenToken = syntaxToken3;
			StatementSyntax statementSyntax = (StatementSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(statementSyntax);
			this.statement = statementSyntax;
		}

		// Token: 0x060058C2 RID: 22722 RVA: 0x0015C7A0 File Offset: 0x0015A9A0
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.fixedKeyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.declaration);
			writer.WriteValue(this.closeParenToken);
			writer.WriteValue(this.statement);
		}

		// Token: 0x060058C3 RID: 22723 RVA: 0x0015C7FC File Offset: 0x0015A9FC
		static FixedStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(FixedStatementSyntax), (ObjectReader r) => new FixedStatementSyntax(r));
		}

		// Token: 0x04001768 RID: 5992
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001769 RID: 5993
		internal readonly SyntaxToken fixedKeyword;

		// Token: 0x0400176A RID: 5994
		internal readonly SyntaxToken openParenToken;

		// Token: 0x0400176B RID: 5995
		internal readonly VariableDeclarationSyntax declaration;

		// Token: 0x0400176C RID: 5996
		internal readonly SyntaxToken closeParenToken;

		// Token: 0x0400176D RID: 5997
		internal readonly StatementSyntax statement;
	}
}
