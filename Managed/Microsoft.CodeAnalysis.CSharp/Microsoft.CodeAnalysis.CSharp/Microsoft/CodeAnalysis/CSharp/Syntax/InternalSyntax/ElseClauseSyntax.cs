using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000408 RID: 1032
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ElseClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600590B RID: 22795 RVA: 0x0015D76D File Offset: 0x0015B96D
		internal ElseClauseSyntax(SyntaxKind kind, SyntaxToken elseKeyword, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(elseKeyword);
			this.elseKeyword = elseKeyword;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x0600590C RID: 22796 RVA: 0x0015D79D File Offset: 0x0015B99D
		internal ElseClauseSyntax(SyntaxKind kind, SyntaxToken elseKeyword, StatementSyntax statement, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(elseKeyword);
			this.elseKeyword = elseKeyword;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x0600590D RID: 22797 RVA: 0x0015D7D1 File Offset: 0x0015B9D1
		internal ElseClauseSyntax(SyntaxKind kind, SyntaxToken elseKeyword, StatementSyntax statement)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(elseKeyword);
			this.elseKeyword = elseKeyword;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x0600590E RID: 22798 RVA: 0x0015D7FD File Offset: 0x0015B9FD
		public SyntaxToken ElseKeyword
		{
			get
			{
				return this.elseKeyword;
			}
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x0600590F RID: 22799 RVA: 0x0015D805 File Offset: 0x0015BA05
		public StatementSyntax Statement
		{
			get
			{
				return this.statement;
			}
		}

		// Token: 0x06005910 RID: 22800 RVA: 0x0015D810 File Offset: 0x0015BA10
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			if (index != 0)
			{
				if (index != 1)
				{
					greenNode = null;
				}
				else
				{
					greenNode = this.statement;
				}
			}
			else
			{
				greenNode = this.elseKeyword;
			}
			return greenNode;
		}

		// Token: 0x06005911 RID: 22801 RVA: 0x0015D83B File Offset: 0x0015BA3B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ElseClauseSyntax(this, parent, position);
		}

		// Token: 0x06005912 RID: 22802 RVA: 0x0015D845 File Offset: 0x0015BA45
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitElseClause(this);
		}

		// Token: 0x06005913 RID: 22803 RVA: 0x0015D84E File Offset: 0x0015BA4E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitElseClause(this);
		}

		// Token: 0x06005914 RID: 22804 RVA: 0x0015D858 File Offset: 0x0015BA58
		public ElseClauseSyntax Update(SyntaxToken elseKeyword, StatementSyntax statement)
		{
			if (elseKeyword != this.ElseKeyword || statement != this.Statement)
			{
				ElseClauseSyntax elseClauseSyntax = SyntaxFactory.ElseClause(elseKeyword, statement);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					elseClauseSyntax = elseClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					elseClauseSyntax = elseClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return elseClauseSyntax;
			}
			return this;
		}

		// Token: 0x06005915 RID: 22805 RVA: 0x0015D8AE File Offset: 0x0015BAAE
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ElseClauseSyntax(base.Kind, this.elseKeyword, this.statement, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x0015D8CE File Offset: 0x0015BACE
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ElseClauseSyntax(base.Kind, this.elseKeyword, this.statement, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005917 RID: 22807 RVA: 0x0015D8F0 File Offset: 0x0015BAF0
		internal ElseClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.elseKeyword = syntaxToken;
			StatementSyntax statementSyntax = (StatementSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(statementSyntax);
			this.statement = statementSyntax;
		}

		// Token: 0x06005918 RID: 22808 RVA: 0x0015D93F File Offset: 0x0015BB3F
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.elseKeyword);
			writer.WriteValue(this.statement);
		}

		// Token: 0x06005919 RID: 22809 RVA: 0x0015D960 File Offset: 0x0015BB60
		static ElseClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ElseClauseSyntax), (ObjectReader r) => new ElseClauseSyntax(r));
		}

		// Token: 0x04001781 RID: 6017
		internal readonly SyntaxToken elseKeyword;

		// Token: 0x04001782 RID: 6018
		internal readonly StatementSyntax statement;
	}
}
