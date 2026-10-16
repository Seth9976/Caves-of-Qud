using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003F6 RID: 1014
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class GotoStatementSyntax : StatementSyntax
	{
		// Token: 0x060057BF RID: 22463 RVA: 0x00158A80 File Offset: 0x00156C80
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal GotoStatementSyntax(SyntaxKind kind, GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken gotoKeyword, SyntaxToken caseOrDefaultKeyword, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(gotoKeyword);
			this.gotoKeyword = gotoKeyword;
			if (caseOrDefaultKeyword != null)
			{
				base.AdjustFlagsAndWidth(caseOrDefaultKeyword);
				this.caseOrDefaultKeyword = caseOrDefaultKeyword;
			}
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x060057C0 RID: 22464 RVA: 0x00158AF8 File Offset: 0x00156CF8
		internal GotoStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken gotoKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken caseOrDefaultKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(gotoKeyword);
			this.gotoKeyword = gotoKeyword;
			if (caseOrDefaultKeyword != null)
			{
				base.AdjustFlagsAndWidth(caseOrDefaultKeyword);
				this.caseOrDefaultKeyword = caseOrDefaultKeyword;
			}
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x060057C1 RID: 22465 RVA: 0x00158B74 File Offset: 0x00156D74
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal GotoStatementSyntax(SyntaxKind kind, GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken gotoKeyword, SyntaxToken caseOrDefaultKeyword, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(gotoKeyword);
			this.gotoKeyword = gotoKeyword;
			if (caseOrDefaultKeyword != null)
			{
				base.AdjustFlagsAndWidth(caseOrDefaultKeyword);
				this.caseOrDefaultKeyword = caseOrDefaultKeyword;
			}
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x060057C2 RID: 22466 RVA: 0x00158BE6 File Offset: 0x00156DE6
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x060057C3 RID: 22467 RVA: 0x00158BF3 File Offset: 0x00156DF3
		public SyntaxToken GotoKeyword
		{
			get
			{
				return this.gotoKeyword;
			}
		}

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x060057C4 RID: 22468 RVA: 0x00158BFB File Offset: 0x00156DFB
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken CaseOrDefaultKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.caseOrDefaultKeyword;
			}
		}

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x060057C5 RID: 22469 RVA: 0x00158C03 File Offset: 0x00156E03
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExpressionSyntax Expression
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x060057C6 RID: 22470 RVA: 0x00158C0B File Offset: 0x00156E0B
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x060057C7 RID: 22471 RVA: 0x00158C14 File Offset: 0x00156E14
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
				greenNode = this.gotoKeyword;
				break;
			case 2:
				greenNode = this.caseOrDefaultKeyword;
				break;
			case 3:
				greenNode = this.expression;
				break;
			case 4:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060057C8 RID: 22472 RVA: 0x00158C6D File Offset: 0x00156E6D
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new GotoStatementSyntax(this, parent, position);
		}

		// Token: 0x060057C9 RID: 22473 RVA: 0x00158C77 File Offset: 0x00156E77
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitGotoStatement(this);
		}

		// Token: 0x060057CA RID: 22474 RVA: 0x00158C80 File Offset: 0x00156E80
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitGotoStatement(this);
		}

		// Token: 0x060057CB RID: 22475 RVA: 0x00158C8C File Offset: 0x00156E8C
		public GotoStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken gotoKeyword, SyntaxToken caseOrDefaultKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || gotoKeyword != this.GotoKeyword || caseOrDefaultKeyword != this.CaseOrDefaultKeyword || expression != this.Expression || semicolonToken != this.SemicolonToken)
			{
				GotoStatementSyntax gotoStatementSyntax = SyntaxFactory.GotoStatement(base.Kind, attributeLists, gotoKeyword, caseOrDefaultKeyword, expression, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					gotoStatementSyntax = gotoStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					gotoStatementSyntax = gotoStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return gotoStatementSyntax;
			}
			return this;
		}

		// Token: 0x060057CC RID: 22476 RVA: 0x00158D0F File Offset: 0x00156F0F
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new GotoStatementSyntax(base.Kind, this.attributeLists, this.gotoKeyword, this.caseOrDefaultKeyword, this.expression, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x00158D41 File Offset: 0x00156F41
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new GotoStatementSyntax(base.Kind, this.attributeLists, this.gotoKeyword, this.caseOrDefaultKeyword, this.expression, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x00158D74 File Offset: 0x00156F74
		internal GotoStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 5;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.gotoKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken2 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken2);
				this.caseOrDefaultKeyword = syntaxToken2;
			}
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			if (expressionSyntax != null)
			{
				base.AdjustFlagsAndWidth(expressionSyntax);
				this.expression = expressionSyntax;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.semicolonToken = syntaxToken3;
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x00158E20 File Offset: 0x00157020
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.gotoKeyword);
			writer.WriteValue(this.caseOrDefaultKeyword);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x00158E70 File Offset: 0x00157070
		static GotoStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(GotoStatementSyntax), (ObjectReader r) => new GotoStatementSyntax(r));
		}

		// Token: 0x0400171C RID: 5916
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x0400171D RID: 5917
		internal readonly SyntaxToken gotoKeyword;

		// Token: 0x0400171E RID: 5918
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken caseOrDefaultKeyword;

		// Token: 0x0400171F RID: 5919
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ExpressionSyntax expression;

		// Token: 0x04001720 RID: 5920
		internal readonly SyntaxToken semicolonToken;
	}
}
