using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003F3 RID: 1011
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ExpressionStatementSyntax : StatementSyntax
	{
		// Token: 0x0600578F RID: 22415 RVA: 0x00158238 File Offset: 0x00156438
		internal ExpressionStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, ExpressionSyntax expression, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005790 RID: 22416 RVA: 0x00158288 File Offset: 0x00156488
		internal ExpressionStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, ExpressionSyntax expression, SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005791 RID: 22417 RVA: 0x001582DA File Offset: 0x001564DA
		internal ExpressionStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, ExpressionSyntax expression, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x06005792 RID: 22418 RVA: 0x00158319 File Offset: 0x00156519
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x06005793 RID: 22419 RVA: 0x00158326 File Offset: 0x00156526
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x06005794 RID: 22420 RVA: 0x0015832E File Offset: 0x0015652E
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005795 RID: 22421 RVA: 0x00158338 File Offset: 0x00156538
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
				greenNode = this.expression;
				break;
			case 2:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005796 RID: 22422 RVA: 0x00158377 File Offset: 0x00156577
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ExpressionStatementSyntax(this, parent, position);
		}

		// Token: 0x06005797 RID: 22423 RVA: 0x00158381 File Offset: 0x00156581
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitExpressionStatement(this);
		}

		// Token: 0x06005798 RID: 22424 RVA: 0x0015838A File Offset: 0x0015658A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitExpressionStatement(this);
		}

		// Token: 0x06005799 RID: 22425 RVA: 0x00158394 File Offset: 0x00156594
		public ExpressionStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || expression != this.Expression || semicolonToken != this.SemicolonToken)
			{
				ExpressionStatementSyntax expressionStatementSyntax = SyntaxFactory.ExpressionStatement(attributeLists, expression, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					expressionStatementSyntax = expressionStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					expressionStatementSyntax = expressionStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return expressionStatementSyntax;
			}
			return this;
		}

		// Token: 0x0600579A RID: 22426 RVA: 0x001583F9 File Offset: 0x001565F9
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ExpressionStatementSyntax(base.Kind, this.attributeLists, this.expression, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600579B RID: 22427 RVA: 0x0015841F File Offset: 0x0015661F
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ExpressionStatementSyntax(base.Kind, this.attributeLists, this.expression, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600579C RID: 22428 RVA: 0x00158448 File Offset: 0x00156648
		internal ExpressionStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.semicolonToken = syntaxToken;
		}

		// Token: 0x0600579D RID: 22429 RVA: 0x001584B4 File Offset: 0x001566B4
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x0600579E RID: 22430 RVA: 0x001584E1 File Offset: 0x001566E1
		static ExpressionStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ExpressionStatementSyntax), (ObjectReader r) => new ExpressionStatementSyntax(r));
		}

		// Token: 0x04001713 RID: 5907
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001714 RID: 5908
		internal readonly ExpressionSyntax expression;

		// Token: 0x04001715 RID: 5909
		internal readonly SyntaxToken semicolonToken;
	}
}
