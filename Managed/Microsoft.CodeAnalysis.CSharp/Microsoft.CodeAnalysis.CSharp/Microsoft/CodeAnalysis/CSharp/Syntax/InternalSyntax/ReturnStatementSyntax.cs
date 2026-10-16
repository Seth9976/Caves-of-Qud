using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003F9 RID: 1017
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ReturnStatementSyntax : StatementSyntax
	{
		// Token: 0x060057F1 RID: 22513 RVA: 0x0015942C File Offset: 0x0015762C
		internal ReturnStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken returnKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(returnKeyword);
			this.returnKeyword = returnKeyword;
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x060057F2 RID: 22514 RVA: 0x00159490 File Offset: 0x00157690
		internal ReturnStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken returnKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(returnKeyword);
			this.returnKeyword = returnKeyword;
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x060057F3 RID: 22515 RVA: 0x001594F8 File Offset: 0x001576F8
		internal ReturnStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken returnKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(returnKeyword);
			this.returnKeyword = returnKeyword;
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x060057F4 RID: 22516 RVA: 0x00159556 File Offset: 0x00157756
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x060057F5 RID: 22517 RVA: 0x00159563 File Offset: 0x00157763
		public SyntaxToken ReturnKeyword
		{
			get
			{
				return this.returnKeyword;
			}
		}

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x060057F6 RID: 22518 RVA: 0x0015956B File Offset: 0x0015776B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExpressionSyntax Expression
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x060057F7 RID: 22519 RVA: 0x00159573 File Offset: 0x00157773
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x060057F8 RID: 22520 RVA: 0x0015957C File Offset: 0x0015777C
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
				greenNode = this.returnKeyword;
				break;
			case 2:
				greenNode = this.expression;
				break;
			case 3:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060057F9 RID: 22521 RVA: 0x001595C8 File Offset: 0x001577C8
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ReturnStatementSyntax(this, parent, position);
		}

		// Token: 0x060057FA RID: 22522 RVA: 0x001595D2 File Offset: 0x001577D2
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitReturnStatement(this);
		}

		// Token: 0x060057FB RID: 22523 RVA: 0x001595DB File Offset: 0x001577DB
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitReturnStatement(this);
		}

		// Token: 0x060057FC RID: 22524 RVA: 0x001595E4 File Offset: 0x001577E4
		public ReturnStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken returnKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || returnKeyword != this.ReturnKeyword || expression != this.Expression || semicolonToken != this.SemicolonToken)
			{
				ReturnStatementSyntax returnStatementSyntax = SyntaxFactory.ReturnStatement(attributeLists, returnKeyword, expression, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					returnStatementSyntax = returnStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					returnStatementSyntax = returnStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return returnStatementSyntax;
			}
			return this;
		}

		// Token: 0x060057FD RID: 22525 RVA: 0x00159655 File Offset: 0x00157855
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ReturnStatementSyntax(base.Kind, this.attributeLists, this.returnKeyword, this.expression, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060057FE RID: 22526 RVA: 0x00159681 File Offset: 0x00157881
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ReturnStatementSyntax(base.Kind, this.attributeLists, this.returnKeyword, this.expression, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060057FF RID: 22527 RVA: 0x001596B0 File Offset: 0x001578B0
		internal ReturnStatementSyntax(ObjectReader reader)
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
			this.returnKeyword = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			if (expressionSyntax != null)
			{
				base.AdjustFlagsAndWidth(expressionSyntax);
				this.expression = expressionSyntax;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.semicolonToken = syntaxToken2;
		}

		// Token: 0x06005800 RID: 22528 RVA: 0x00159739 File Offset: 0x00157939
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.returnKeyword);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005801 RID: 22529 RVA: 0x00159772 File Offset: 0x00157972
		static ReturnStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ReturnStatementSyntax), (ObjectReader r) => new ReturnStatementSyntax(r));
		}

		// Token: 0x04001727 RID: 5927
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001728 RID: 5928
		internal readonly SyntaxToken returnKeyword;

		// Token: 0x04001729 RID: 5929
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ExpressionSyntax expression;

		// Token: 0x0400172A RID: 5930
		internal readonly SyntaxToken semicolonToken;
	}
}
