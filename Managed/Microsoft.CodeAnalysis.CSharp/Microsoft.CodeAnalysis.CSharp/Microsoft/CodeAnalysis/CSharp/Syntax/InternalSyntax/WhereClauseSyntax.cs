using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003C9 RID: 969
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class WhereClauseSyntax : QueryClauseSyntax
	{
		// Token: 0x06005530 RID: 21808 RVA: 0x00152209 File Offset: 0x00150409
		internal WhereClauseSyntax(SyntaxKind kind, SyntaxToken whereKeyword, ExpressionSyntax condition, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(whereKeyword);
			this.whereKeyword = whereKeyword;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x00152239 File Offset: 0x00150439
		internal WhereClauseSyntax(SyntaxKind kind, SyntaxToken whereKeyword, ExpressionSyntax condition, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(whereKeyword);
			this.whereKeyword = whereKeyword;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x0015226D File Offset: 0x0015046D
		internal WhereClauseSyntax(SyntaxKind kind, SyntaxToken whereKeyword, ExpressionSyntax condition)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(whereKeyword);
			this.whereKeyword = whereKeyword;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x06005533 RID: 21811 RVA: 0x00152299 File Offset: 0x00150499
		public SyntaxToken WhereKeyword
		{
			get
			{
				return this.whereKeyword;
			}
		}

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x06005534 RID: 21812 RVA: 0x001522A1 File Offset: 0x001504A1
		public ExpressionSyntax Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x001522AC File Offset: 0x001504AC
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
					greenNode = this.condition;
				}
			}
			else
			{
				greenNode = this.whereKeyword;
			}
			return greenNode;
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x001522D7 File Offset: 0x001504D7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new WhereClauseSyntax(this, parent, position);
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x001522E1 File Offset: 0x001504E1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitWhereClause(this);
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x001522EA File Offset: 0x001504EA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitWhereClause(this);
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x001522F4 File Offset: 0x001504F4
		public WhereClauseSyntax Update(SyntaxToken whereKeyword, ExpressionSyntax condition)
		{
			if (whereKeyword != this.WhereKeyword || condition != this.Condition)
			{
				WhereClauseSyntax whereClauseSyntax = SyntaxFactory.WhereClause(whereKeyword, condition);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					whereClauseSyntax = whereClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					whereClauseSyntax = whereClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return whereClauseSyntax;
			}
			return this;
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x0015234A File Offset: 0x0015054A
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new WhereClauseSyntax(base.Kind, this.whereKeyword, this.condition, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x0015236A File Offset: 0x0015056A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new WhereClauseSyntax(base.Kind, this.whereKeyword, this.condition, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x0015238C File Offset: 0x0015058C
		internal WhereClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.whereKeyword = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.condition = expressionSyntax;
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x001523DB File Offset: 0x001505DB
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.whereKeyword);
			writer.WriteValue(this.condition);
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x001523FC File Offset: 0x001505FC
		static WhereClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(WhereClauseSyntax), (ObjectReader r) => new WhereClauseSyntax(r));
		}

		// Token: 0x040016AF RID: 5807
		internal readonly SyntaxToken whereKeyword;

		// Token: 0x040016B0 RID: 5808
		internal readonly ExpressionSyntax condition;
	}
}
