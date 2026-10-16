using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003F5 RID: 1013
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class LabeledStatementSyntax : StatementSyntax
	{
		// Token: 0x060057AE RID: 22446 RVA: 0x00158728 File Offset: 0x00156928
		internal LabeledStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken identifier, SyntaxToken colonToken, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x060057AF RID: 22447 RVA: 0x00158788 File Offset: 0x00156988
		internal LabeledStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken identifier, SyntaxToken colonToken, StatementSyntax statement, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x060057B0 RID: 22448 RVA: 0x001587EC File Offset: 0x001569EC
		internal LabeledStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken identifier, SyntaxToken colonToken, StatementSyntax statement)
			: base(kind)
		{
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x060057B1 RID: 22449 RVA: 0x00158846 File Offset: 0x00156A46
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x060057B2 RID: 22450 RVA: 0x00158853 File Offset: 0x00156A53
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x060057B3 RID: 22451 RVA: 0x0015885B File Offset: 0x00156A5B
		public SyntaxToken ColonToken
		{
			get
			{
				return this.colonToken;
			}
		}

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x060057B4 RID: 22452 RVA: 0x00158863 File Offset: 0x00156A63
		public StatementSyntax Statement
		{
			get
			{
				return this.statement;
			}
		}

		// Token: 0x060057B5 RID: 22453 RVA: 0x0015886C File Offset: 0x00156A6C
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
				greenNode = this.identifier;
				break;
			case 2:
				greenNode = this.colonToken;
				break;
			case 3:
				greenNode = this.statement;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060057B6 RID: 22454 RVA: 0x001588B8 File Offset: 0x00156AB8
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new LabeledStatementSyntax(this, parent, position);
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x001588C2 File Offset: 0x00156AC2
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLabeledStatement(this);
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x001588CB File Offset: 0x00156ACB
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLabeledStatement(this);
		}

		// Token: 0x060057B9 RID: 22457 RVA: 0x001588D4 File Offset: 0x00156AD4
		public LabeledStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken identifier, SyntaxToken colonToken, StatementSyntax statement)
		{
			if (attributeLists != this.AttributeLists || identifier != this.Identifier || colonToken != this.ColonToken || statement != this.Statement)
			{
				LabeledStatementSyntax labeledStatementSyntax = SyntaxFactory.LabeledStatement(attributeLists, identifier, colonToken, statement);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					labeledStatementSyntax = labeledStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					labeledStatementSyntax = labeledStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return labeledStatementSyntax;
			}
			return this;
		}

		// Token: 0x060057BA RID: 22458 RVA: 0x00158945 File Offset: 0x00156B45
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new LabeledStatementSyntax(base.Kind, this.attributeLists, this.identifier, this.colonToken, this.statement, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060057BB RID: 22459 RVA: 0x00158971 File Offset: 0x00156B71
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new LabeledStatementSyntax(base.Kind, this.attributeLists, this.identifier, this.colonToken, this.statement, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060057BC RID: 22460 RVA: 0x001589A0 File Offset: 0x00156BA0
		internal LabeledStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.identifier = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.colonToken = syntaxToken2;
			StatementSyntax statementSyntax = (StatementSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(statementSyntax);
			this.statement = statementSyntax;
		}

		// Token: 0x060057BD RID: 22461 RVA: 0x00158A26 File Offset: 0x00156C26
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.colonToken);
			writer.WriteValue(this.statement);
		}

		// Token: 0x060057BE RID: 22462 RVA: 0x00158A5F File Offset: 0x00156C5F
		static LabeledStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(LabeledStatementSyntax), (ObjectReader r) => new LabeledStatementSyntax(r));
		}

		// Token: 0x04001718 RID: 5912
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001719 RID: 5913
		internal readonly SyntaxToken identifier;

		// Token: 0x0400171A RID: 5914
		internal readonly SyntaxToken colonToken;

		// Token: 0x0400171B RID: 5915
		internal readonly StatementSyntax statement;
	}
}
