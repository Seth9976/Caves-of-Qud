using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002D0 RID: 720
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class JoinClauseSyntax : QueryClauseSyntax
	{
		// Token: 0x060040A7 RID: 16551 RVA: 0x0010E0AD File Offset: 0x0010C2AD
		internal JoinClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x060040A8 RID: 16552 RVA: 0x0010E0B8 File Offset: 0x0010C2B8
		public SyntaxToken JoinKeyword
		{
			get
			{
				return new SyntaxToken(this, ((JoinClauseSyntax)base.Green).joinKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x060040A9 RID: 16553 RVA: 0x0010E0D7 File Offset: 0x0010C2D7
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public TypeSyntax Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 1);
			}
		}

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x060040AA RID: 16554 RVA: 0x0010E0E6 File Offset: 0x0010C2E6
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((JoinClauseSyntax)base.Green).identifier, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x060040AB RID: 16555 RVA: 0x0010E10C File Offset: 0x0010C30C
		public SyntaxToken InKeyword
		{
			get
			{
				return new SyntaxToken(this, ((JoinClauseSyntax)base.Green).inKeyword, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x060040AC RID: 16556 RVA: 0x0010E132 File Offset: 0x0010C332
		public ExpressionSyntax InExpression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.inExpression, 4);
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x060040AD RID: 16557 RVA: 0x0010E141 File Offset: 0x0010C341
		public SyntaxToken OnKeyword
		{
			get
			{
				return new SyntaxToken(this, ((JoinClauseSyntax)base.Green).onKeyword, this.GetChildPosition(5), base.GetChildIndex(5));
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x060040AE RID: 16558 RVA: 0x0010E167 File Offset: 0x0010C367
		public ExpressionSyntax LeftExpression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.leftExpression, 6);
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x060040AF RID: 16559 RVA: 0x0010E176 File Offset: 0x0010C376
		public SyntaxToken EqualsKeyword
		{
			get
			{
				return new SyntaxToken(this, ((JoinClauseSyntax)base.Green).equalsKeyword, this.GetChildPosition(7), base.GetChildIndex(7));
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x060040B0 RID: 16560 RVA: 0x0010E19C File Offset: 0x0010C39C
		public ExpressionSyntax RightExpression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.rightExpression, 8);
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x060040B1 RID: 16561 RVA: 0x0010E1AB File Offset: 0x0010C3AB
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public JoinIntoClauseSyntax Into
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<JoinIntoClauseSyntax>(ref this.into, 9);
			}
		}

		// Token: 0x060040B2 RID: 16562 RVA: 0x0010E1BC File Offset: 0x0010C3BC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 1:
				return base.GetRed<TypeSyntax>(ref this.type, 1);
			case 4:
				return base.GetRed<ExpressionSyntax>(ref this.inExpression, 4);
			case 6:
				return base.GetRed<ExpressionSyntax>(ref this.leftExpression, 6);
			case 8:
				return base.GetRed<ExpressionSyntax>(ref this.rightExpression, 8);
			case 9:
				return base.GetRed<JoinIntoClauseSyntax>(ref this.into, 9);
			}
			return null;
		}

		// Token: 0x060040B3 RID: 16563 RVA: 0x0010E24C File Offset: 0x0010C44C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 1:
				return this.type;
			case 4:
				return this.inExpression;
			case 6:
				return this.leftExpression;
			case 8:
				return this.rightExpression;
			case 9:
				return this.into;
			}
			return null;
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x0010E2B7 File Offset: 0x0010C4B7
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitJoinClause(this);
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x0010E2C0 File Offset: 0x0010C4C0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitJoinClause(this);
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x0010E2CC File Offset: 0x0010C4CC
		public JoinClauseSyntax Update(SyntaxToken joinKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax inExpression, SyntaxToken onKeyword, ExpressionSyntax leftExpression, SyntaxToken equalsKeyword, ExpressionSyntax rightExpression, [global::System.Runtime.CompilerServices.Nullable(2)] JoinIntoClauseSyntax into)
		{
			if (!(joinKeyword != this.JoinKeyword) && type == this.Type && !(identifier != this.Identifier) && !(inKeyword != this.InKeyword) && inExpression == this.InExpression && !(onKeyword != this.OnKeyword) && leftExpression == this.LeftExpression && !(equalsKeyword != this.EqualsKeyword) && rightExpression == this.RightExpression && into == this.Into)
			{
				return this;
			}
			JoinClauseSyntax joinClauseSyntax = SyntaxFactory.JoinClause(joinKeyword, type, identifier, inKeyword, inExpression, onKeyword, leftExpression, equalsKeyword, rightExpression, into);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return joinClauseSyntax;
			}
			return joinClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x0010E384 File Offset: 0x0010C584
		public JoinClauseSyntax WithJoinKeyword(SyntaxToken joinKeyword)
		{
			return this.Update(joinKeyword, this.Type, this.Identifier, this.InKeyword, this.InExpression, this.OnKeyword, this.LeftExpression, this.EqualsKeyword, this.RightExpression, this.Into);
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x0010E3D0 File Offset: 0x0010C5D0
		public JoinClauseSyntax WithType([global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type)
		{
			return this.Update(this.JoinKeyword, type, this.Identifier, this.InKeyword, this.InExpression, this.OnKeyword, this.LeftExpression, this.EqualsKeyword, this.RightExpression, this.Into);
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x0010E41C File Offset: 0x0010C61C
		public JoinClauseSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.JoinKeyword, this.Type, identifier, this.InKeyword, this.InExpression, this.OnKeyword, this.LeftExpression, this.EqualsKeyword, this.RightExpression, this.Into);
		}

		// Token: 0x060040BA RID: 16570 RVA: 0x0010E468 File Offset: 0x0010C668
		public JoinClauseSyntax WithInKeyword(SyntaxToken inKeyword)
		{
			return this.Update(this.JoinKeyword, this.Type, this.Identifier, inKeyword, this.InExpression, this.OnKeyword, this.LeftExpression, this.EqualsKeyword, this.RightExpression, this.Into);
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x0010E4B4 File Offset: 0x0010C6B4
		public JoinClauseSyntax WithInExpression(ExpressionSyntax inExpression)
		{
			return this.Update(this.JoinKeyword, this.Type, this.Identifier, this.InKeyword, inExpression, this.OnKeyword, this.LeftExpression, this.EqualsKeyword, this.RightExpression, this.Into);
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x0010E500 File Offset: 0x0010C700
		public JoinClauseSyntax WithOnKeyword(SyntaxToken onKeyword)
		{
			return this.Update(this.JoinKeyword, this.Type, this.Identifier, this.InKeyword, this.InExpression, onKeyword, this.LeftExpression, this.EqualsKeyword, this.RightExpression, this.Into);
		}

		// Token: 0x060040BD RID: 16573 RVA: 0x0010E54C File Offset: 0x0010C74C
		public JoinClauseSyntax WithLeftExpression(ExpressionSyntax leftExpression)
		{
			return this.Update(this.JoinKeyword, this.Type, this.Identifier, this.InKeyword, this.InExpression, this.OnKeyword, leftExpression, this.EqualsKeyword, this.RightExpression, this.Into);
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x0010E598 File Offset: 0x0010C798
		public JoinClauseSyntax WithEqualsKeyword(SyntaxToken equalsKeyword)
		{
			return this.Update(this.JoinKeyword, this.Type, this.Identifier, this.InKeyword, this.InExpression, this.OnKeyword, this.LeftExpression, equalsKeyword, this.RightExpression, this.Into);
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x0010E5E4 File Offset: 0x0010C7E4
		public JoinClauseSyntax WithRightExpression(ExpressionSyntax rightExpression)
		{
			return this.Update(this.JoinKeyword, this.Type, this.Identifier, this.InKeyword, this.InExpression, this.OnKeyword, this.LeftExpression, this.EqualsKeyword, rightExpression, this.Into);
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x0010E630 File Offset: 0x0010C830
		public JoinClauseSyntax WithInto([global::System.Runtime.CompilerServices.Nullable(2)] JoinIntoClauseSyntax into)
		{
			return this.Update(this.JoinKeyword, this.Type, this.Identifier, this.InKeyword, this.InExpression, this.OnKeyword, this.LeftExpression, this.EqualsKeyword, this.RightExpression, into);
		}

		// Token: 0x04001498 RID: 5272
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x04001499 RID: 5273
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax inExpression;

		// Token: 0x0400149A RID: 5274
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax leftExpression;

		// Token: 0x0400149B RID: 5275
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax rightExpression;

		// Token: 0x0400149C RID: 5276
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private JoinIntoClauseSyntax into;
	}
}
