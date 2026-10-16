using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000309 RID: 777
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class AttributeArgumentSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600432F RID: 17199 RVA: 0x00112272 File Offset: 0x00110472
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal AttributeArgumentSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x06004330 RID: 17200 RVA: 0x0011227D File Offset: 0x0011047D
		public NameEqualsSyntax NameEquals
		{
			get
			{
				return base.GetRedAtZero<NameEqualsSyntax>(ref this.nameEquals);
			}
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x06004331 RID: 17201 RVA: 0x0011228B File Offset: 0x0011048B
		public NameColonSyntax NameColon
		{
			get
			{
				return base.GetRed<NameColonSyntax>(ref this.nameColon, 1);
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x06004332 RID: 17202 RVA: 0x0011229A File Offset: 0x0011049A
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public ExpressionSyntax Expression
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 2);
			}
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x001122AC File Offset: 0x001104AC
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 0:
				syntaxNode = base.GetRedAtZero<NameEqualsSyntax>(ref this.nameEquals);
				break;
			case 1:
				syntaxNode = base.GetRed<NameColonSyntax>(ref this.nameColon, 1);
				break;
			case 2:
				syntaxNode = base.GetRed<ExpressionSyntax>(ref this.expression, 2);
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x00112300 File Offset: 0x00110500
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 0:
				syntaxNode = this.nameEquals;
				break;
			case 1:
				syntaxNode = this.nameColon;
				break;
			case 2:
				syntaxNode = this.expression;
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x0011233F File Offset: 0x0011053F
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAttributeArgument(this);
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x00112348 File Offset: 0x00110548
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAttributeArgument(this);
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x00112354 File Offset: 0x00110554
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public AttributeArgumentSyntax Update([global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax nameEquals, [global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon, ExpressionSyntax expression)
		{
			if (nameEquals == this.NameEquals && nameColon == this.NameColon && expression == this.Expression)
			{
				return this;
			}
			AttributeArgumentSyntax attributeArgumentSyntax = SyntaxFactory.AttributeArgument(nameEquals, nameColon, expression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return attributeArgumentSyntax;
			}
			return attributeArgumentSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x0011239E File Offset: 0x0011059E
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public AttributeArgumentSyntax WithNameEquals([global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax nameEquals)
		{
			return this.Update(nameEquals, this.NameColon, this.Expression);
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x001123B3 File Offset: 0x001105B3
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public AttributeArgumentSyntax WithNameColon([global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon)
		{
			return this.Update(this.NameEquals, nameColon, this.Expression);
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x001123C8 File Offset: 0x001105C8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public AttributeArgumentSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.NameEquals, this.NameColon, expression);
		}

		// Token: 0x040014D9 RID: 5337
		private NameEqualsSyntax nameEquals;

		// Token: 0x040014DA RID: 5338
		private NameColonSyntax nameColon;

		// Token: 0x040014DB RID: 5339
		private ExpressionSyntax expression;
	}
}
