using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003FB RID: 1019
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class YieldStatementSyntax : StatementSyntax
	{
		// Token: 0x06005813 RID: 22547 RVA: 0x00159AFC File Offset: 0x00157CFC
		internal YieldStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken yieldKeyword, SyntaxToken returnOrBreakKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(yieldKeyword);
			this.yieldKeyword = yieldKeyword;
			base.AdjustFlagsAndWidth(returnOrBreakKeyword);
			this.returnOrBreakKeyword = returnOrBreakKeyword;
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005814 RID: 22548 RVA: 0x00159B70 File Offset: 0x00157D70
		internal YieldStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken yieldKeyword, SyntaxToken returnOrBreakKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(yieldKeyword);
			this.yieldKeyword = yieldKeyword;
			base.AdjustFlagsAndWidth(returnOrBreakKeyword);
			this.returnOrBreakKeyword = returnOrBreakKeyword;
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005815 RID: 22549 RVA: 0x00159BE8 File Offset: 0x00157DE8
		internal YieldStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken yieldKeyword, SyntaxToken returnOrBreakKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(yieldKeyword);
			this.yieldKeyword = yieldKeyword;
			base.AdjustFlagsAndWidth(returnOrBreakKeyword);
			this.returnOrBreakKeyword = returnOrBreakKeyword;
			if (expression != null)
			{
				base.AdjustFlagsAndWidth(expression);
				this.expression = expression;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x06005816 RID: 22550 RVA: 0x00159C56 File Offset: 0x00157E56
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x06005817 RID: 22551 RVA: 0x00159C63 File Offset: 0x00157E63
		public SyntaxToken YieldKeyword
		{
			get
			{
				return this.yieldKeyword;
			}
		}

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06005818 RID: 22552 RVA: 0x00159C6B File Offset: 0x00157E6B
		public SyntaxToken ReturnOrBreakKeyword
		{
			get
			{
				return this.returnOrBreakKeyword;
			}
		}

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x06005819 RID: 22553 RVA: 0x00159C73 File Offset: 0x00157E73
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExpressionSyntax Expression
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x0600581A RID: 22554 RVA: 0x00159C7B File Offset: 0x00157E7B
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x00159C84 File Offset: 0x00157E84
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
				greenNode = this.yieldKeyword;
				break;
			case 2:
				greenNode = this.returnOrBreakKeyword;
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

		// Token: 0x0600581C RID: 22556 RVA: 0x00159CDD File Offset: 0x00157EDD
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new YieldStatementSyntax(this, parent, position);
		}

		// Token: 0x0600581D RID: 22557 RVA: 0x00159CE7 File Offset: 0x00157EE7
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitYieldStatement(this);
		}

		// Token: 0x0600581E RID: 22558 RVA: 0x00159CF0 File Offset: 0x00157EF0
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitYieldStatement(this);
		}

		// Token: 0x0600581F RID: 22559 RVA: 0x00159CFC File Offset: 0x00157EFC
		public YieldStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken yieldKeyword, SyntaxToken returnOrBreakKeyword, ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || yieldKeyword != this.YieldKeyword || returnOrBreakKeyword != this.ReturnOrBreakKeyword || expression != this.Expression || semicolonToken != this.SemicolonToken)
			{
				YieldStatementSyntax yieldStatementSyntax = SyntaxFactory.YieldStatement(base.Kind, attributeLists, yieldKeyword, returnOrBreakKeyword, expression, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					yieldStatementSyntax = yieldStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					yieldStatementSyntax = yieldStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return yieldStatementSyntax;
			}
			return this;
		}

		// Token: 0x06005820 RID: 22560 RVA: 0x00159D7F File Offset: 0x00157F7F
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new YieldStatementSyntax(base.Kind, this.attributeLists, this.yieldKeyword, this.returnOrBreakKeyword, this.expression, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005821 RID: 22561 RVA: 0x00159DB1 File Offset: 0x00157FB1
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new YieldStatementSyntax(base.Kind, this.attributeLists, this.yieldKeyword, this.returnOrBreakKeyword, this.expression, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005822 RID: 22562 RVA: 0x00159DE4 File Offset: 0x00157FE4
		internal YieldStatementSyntax(ObjectReader reader)
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
			this.yieldKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.returnOrBreakKeyword = syntaxToken2;
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

		// Token: 0x06005823 RID: 22563 RVA: 0x00159E8C File Offset: 0x0015808C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.yieldKeyword);
			writer.WriteValue(this.returnOrBreakKeyword);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x00159EDC File Offset: 0x001580DC
		static YieldStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(YieldStatementSyntax), (ObjectReader r) => new YieldStatementSyntax(r));
		}

		// Token: 0x0400172F RID: 5935
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001730 RID: 5936
		internal readonly SyntaxToken yieldKeyword;

		// Token: 0x04001731 RID: 5937
		internal readonly SyntaxToken returnOrBreakKeyword;

		// Token: 0x04001732 RID: 5938
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ExpressionSyntax expression;

		// Token: 0x04001733 RID: 5939
		internal readonly SyntaxToken semicolonToken;
	}
}
