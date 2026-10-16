using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003C5 RID: 965
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class FromClauseSyntax : QueryClauseSyntax
	{
		// Token: 0x060054E7 RID: 21735 RVA: 0x00151228 File Offset: 0x0014F428
		internal FromClauseSyntax(SyntaxKind kind, SyntaxToken fromKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(fromKeyword);
			this.fromKeyword = fromKeyword;
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(inKeyword);
			this.inKeyword = inKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x00151298 File Offset: 0x0014F498
		internal FromClauseSyntax(SyntaxKind kind, SyntaxToken fromKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(fromKeyword);
			this.fromKeyword = fromKeyword;
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(inKeyword);
			this.inKeyword = inKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x060054E9 RID: 21737 RVA: 0x0015130C File Offset: 0x0014F50C
		internal FromClauseSyntax(SyntaxKind kind, SyntaxToken fromKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression)
			: base(kind)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(fromKeyword);
			this.fromKeyword = fromKeyword;
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(inKeyword);
			this.inKeyword = inKeyword;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x060054EA RID: 21738 RVA: 0x00151376 File Offset: 0x0014F576
		public SyntaxToken FromKeyword
		{
			get
			{
				return this.fromKeyword;
			}
		}

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x060054EB RID: 21739 RVA: 0x0015137E File Offset: 0x0014F57E
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public TypeSyntax Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x060054EC RID: 21740 RVA: 0x00151386 File Offset: 0x0014F586
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x060054ED RID: 21741 RVA: 0x0015138E File Offset: 0x0014F58E
		public SyntaxToken InKeyword
		{
			get
			{
				return this.inKeyword;
			}
		}

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x060054EE RID: 21742 RVA: 0x00151396 File Offset: 0x0014F596
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x001513A0 File Offset: 0x0014F5A0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.fromKeyword;
				break;
			case 1:
				greenNode = this.type;
				break;
			case 2:
				greenNode = this.identifier;
				break;
			case 3:
				greenNode = this.inKeyword;
				break;
			case 4:
				greenNode = this.expression;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x001513F9 File Offset: 0x0014F5F9
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new FromClauseSyntax(this, parent, position);
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x00151403 File Offset: 0x0014F603
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFromClause(this);
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x0015140C File Offset: 0x0014F60C
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFromClause(this);
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x00151418 File Offset: 0x0014F618
		public FromClauseSyntax Update(SyntaxToken fromKeyword, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression)
		{
			if (fromKeyword != this.FromKeyword || type != this.Type || identifier != this.Identifier || inKeyword != this.InKeyword || expression != this.Expression)
			{
				FromClauseSyntax fromClauseSyntax = SyntaxFactory.FromClause(fromKeyword, type, identifier, inKeyword, expression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					fromClauseSyntax = fromClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					fromClauseSyntax = fromClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return fromClauseSyntax;
			}
			return this;
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x00151490 File Offset: 0x0014F690
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new FromClauseSyntax(base.Kind, this.fromKeyword, this.type, this.identifier, this.inKeyword, this.expression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x001514C2 File Offset: 0x0014F6C2
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new FromClauseSyntax(base.Kind, this.fromKeyword, this.type, this.identifier, this.inKeyword, this.expression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x001514F4 File Offset: 0x0014F6F4
		internal FromClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 5;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.fromKeyword = syntaxToken;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			if (typeSyntax != null)
			{
				base.AdjustFlagsAndWidth(typeSyntax);
				this.type = typeSyntax;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.identifier = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.inKeyword = syntaxToken3;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x00151598 File Offset: 0x0014F798
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.fromKeyword);
			writer.WriteValue(this.type);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.inKeyword);
			writer.WriteValue(this.expression);
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x001515E8 File Offset: 0x0014F7E8
		static FromClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(FromClauseSyntax), (ObjectReader r) => new FromClauseSyntax(r));
		}

		// Token: 0x0400169A RID: 5786
		internal readonly SyntaxToken fromKeyword;

		// Token: 0x0400169B RID: 5787
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly TypeSyntax type;

		// Token: 0x0400169C RID: 5788
		internal readonly SyntaxToken identifier;

		// Token: 0x0400169D RID: 5789
		internal readonly SyntaxToken inKeyword;

		// Token: 0x0400169E RID: 5790
		internal readonly ExpressionSyntax expression;
	}
}
