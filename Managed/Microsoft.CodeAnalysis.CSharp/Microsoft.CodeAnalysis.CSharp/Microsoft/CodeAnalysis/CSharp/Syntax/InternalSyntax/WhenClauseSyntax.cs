using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003D3 RID: 979
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class WhenClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x060055CA RID: 21962 RVA: 0x00153971 File Offset: 0x00151B71
		internal WhenClauseSyntax(SyntaxKind kind, SyntaxToken whenKeyword, ExpressionSyntax condition, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(whenKeyword);
			this.whenKeyword = whenKeyword;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
		}

		// Token: 0x060055CB RID: 21963 RVA: 0x001539A1 File Offset: 0x00151BA1
		internal WhenClauseSyntax(SyntaxKind kind, SyntaxToken whenKeyword, ExpressionSyntax condition, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(whenKeyword);
			this.whenKeyword = whenKeyword;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
		}

		// Token: 0x060055CC RID: 21964 RVA: 0x001539D5 File Offset: 0x00151BD5
		internal WhenClauseSyntax(SyntaxKind kind, SyntaxToken whenKeyword, ExpressionSyntax condition)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(whenKeyword);
			this.whenKeyword = whenKeyword;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x060055CD RID: 21965 RVA: 0x00153A01 File Offset: 0x00151C01
		public SyntaxToken WhenKeyword
		{
			get
			{
				return this.whenKeyword;
			}
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x060055CE RID: 21966 RVA: 0x00153A09 File Offset: 0x00151C09
		public ExpressionSyntax Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x060055CF RID: 21967 RVA: 0x00153A14 File Offset: 0x00151C14
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
				greenNode = this.whenKeyword;
			}
			return greenNode;
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x00153A3F File Offset: 0x00151C3F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new WhenClauseSyntax(this, parent, position);
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x00153A49 File Offset: 0x00151C49
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitWhenClause(this);
		}

		// Token: 0x060055D2 RID: 21970 RVA: 0x00153A52 File Offset: 0x00151C52
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitWhenClause(this);
		}

		// Token: 0x060055D3 RID: 21971 RVA: 0x00153A5C File Offset: 0x00151C5C
		public WhenClauseSyntax Update(SyntaxToken whenKeyword, ExpressionSyntax condition)
		{
			if (whenKeyword != this.WhenKeyword || condition != this.Condition)
			{
				WhenClauseSyntax whenClauseSyntax = SyntaxFactory.WhenClause(whenKeyword, condition);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					whenClauseSyntax = whenClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					whenClauseSyntax = whenClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return whenClauseSyntax;
			}
			return this;
		}

		// Token: 0x060055D4 RID: 21972 RVA: 0x00153AB2 File Offset: 0x00151CB2
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new WhenClauseSyntax(base.Kind, this.whenKeyword, this.condition, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060055D5 RID: 21973 RVA: 0x00153AD2 File Offset: 0x00151CD2
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new WhenClauseSyntax(base.Kind, this.whenKeyword, this.condition, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060055D6 RID: 21974 RVA: 0x00153AF4 File Offset: 0x00151CF4
		internal WhenClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.whenKeyword = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.condition = expressionSyntax;
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x00153B43 File Offset: 0x00151D43
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.whenKeyword);
			writer.WriteValue(this.condition);
		}

		// Token: 0x060055D8 RID: 21976 RVA: 0x00153B64 File Offset: 0x00151D64
		static WhenClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(WhenClauseSyntax), (ObjectReader r) => new WhenClauseSyntax(r));
		}

		// Token: 0x040016C7 RID: 5831
		internal readonly SyntaxToken whenKeyword;

		// Token: 0x040016C8 RID: 5832
		internal readonly ExpressionSyntax condition;
	}
}
